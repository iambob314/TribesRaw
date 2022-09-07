//
// Matrix functions
//
// Matrices are stored in column-major order (column elements are contiguous), to match
// GameBase::getMuzzleTransform().
//

// Matrix::at returns the element in %m (with %mr rows) at row %r, col %c
function Matrix::at(%m, %mr, %r, %c) {
	return getWord(%m, %c * %mr + %r);
}

// Matrix::mul multiplies matrices %m1 (%m1r rows, %mc1r2 cols) and %m2 (%m1c2r rows, %m2c cols).
// The returned matrix has %m1r rows, %m2c cols.
function Matrix::mul(%m1, %m1r, %m1c2r, %m2, %m2c) {
	for (%c = 0; %c < %m2c; %c++) {
		for (%r = 0; %r < %m1r; %r++) {
			%sum = 0;
			for (%i = 0; %i < %m1c2r; %i++)
				%sum += Matrix::at(%m1, %m1r, %r, %i) * Matrix::at(%m2, %m1c2r, %i, %c);
			
			if (%r + %c != 0) %m3 = %m3 @ " ";
			%m3 = %m3 @ %sum;
		}
	}
	return %m3;
}

// Affine transform matricies
function Matrix::rotX(%x) {
	%sx = sin(%x); %cx = cos(%x);
	return 1 @ " " @ 0    @ " " @ 0   @ " " @
	       0 @ " " @ %cx  @ " " @ %sx @ " " @
	       0 @ " " @ -%sx @ " " @ %cx;
}
function Matrix::rotY(%x) {
	%sx = sin(%x); %cx = cos(%x);
	return %cx  @ " " @ 0 @ " " @ %sx @ " " @
	       0    @ " " @ 1 @ " " @ 0   @ " " @
	       -%sx @ " " @ 0 @ " " @ %cx;
}
function Matrix::rotZ(%x) {
	%sx = sin(%x); %cx = cos(%x);
	return %cx  @ " " @ %sx @ " " @ 0 @ " " @
	       -%sx @ " " @ %cx @ " " @ 0 @ " " @
	       0    @ " " @ 0   @ " " @ 1;
}

// Note: rotXYZ = rotZ*rotY*rotX (rotate X then Y then Z)
function Matrix::rotXYZ(%x, %y, %z) {
	%sx = sin(%x); %cx = cos(%x);
	%sy = sin(%y); %cy = cos(%y);
	%sz = sin(%z); %cz = cos(%z);
	return %cy*%cz                @ " " @ %cy*%sz                @ " " @ %sy     @ " " @
	       (-%sx*%sy*%cz-%cx*%sz) @ " " @ (-%sx*%sy*%sz+%cx*%cz) @ " " @ %sx*%cy @ " " @
	       (-%cx*%sy*%cz+%sx*%sz) @ " " @ (-%cx*%sy*%sz-%sx*%cz) @ " " @ %cx*%cy;
}
