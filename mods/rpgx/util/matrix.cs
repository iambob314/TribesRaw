function Matrix::mul(%m1, %r1, %c1, %m2, %r2, %c2) {
  if (%c1 != %r2) return -1;

  for (%col = 0; %col < %c2; %col++) {
    for (%row = 0; %row < %r1; %row++) {
      %sum = 0;
      for (%i = 0; %i < %c1; %i++)
        %sum += Matrix::getElem(%m1, %row, %i, %r1) * Matrix::getElem(%m2, %i, %col, %r2);
      %words[%row, %col] = %sum;
      if (%row == 0 && %col == 0) %returnM = %words[%row, %col];
      else                        %returnM = %returnM @ " " @ %words[%row, %col];
    }
  }
  return %returnM;

}

function Matrix::getElem(%m, %r, %c, %mr) {
  return getWord(%m, %c * %mr + %r);
}

function Matrix::subMatrix(%m, %mr, %mc, %r, %c, %sr, %sc) {

  if (%sr + %r > %mr || %sc + %c > %mc) return -1;

  for (%col = %sc; %col < %sc + %c; %col++) {
    for (%row = %sr; %row < %sr + %r; %row++) {
      if (%row == %sr && %col == %sc) %returnM = Matrix::getElem(%m, %row, %col, %mr);
      else                            %returnM = %returnM @ " " @ Matrix::getElem(%m, %row, %col, %mr);
    }
  }
  return %returnM;
}

// Transform matricies
function Matrix::identity(%r, %c) {
  %mat = "";
  for (%row = 0; %row < %r; %row++) {
    for (%col = 0; %col < %c; %col++) {
      if (%row == %col) %mat = %mat @ "1 ";
      else              %mat = %mat @ "0 ";
    }
  }
  return %mat;
}

function Matrix::rotX(%x) {
  return 1 @ " " @ 0        @ " " @ 0       @ " " @
         0 @ " " @ cos(%x)  @ " " @ sin(%x) @ " " @
         0 @ " " @ -sin(%x) @ " " @ cos(%x);
}
function Matrix::rotY(%x) {
  return cos(%x)  @ " " @ 0 @ " " @ sin(%x) @ " " @
         0        @ " " @ 1 @ " " @ 0       @ " " @
         -sin(%x) @ " " @ 0 @ " " @ cos(%x);
}
function Matrix::rotZ(%x) {
  return cos(%x)  @ " " @ sin(%x) @ " " @ 0 @ " " @
         -sin(%x) @ " " @ cos(%x) @ " " @ 0 @ " " @
         0        @ " " @ 0       @ " " @ 1;
}

function Matrix::rotXYZ(%x, %y, %z) {
  %cx = cos(%x);
  %cy = cos(%y);
  %cz = cos(%z);
  %sx = sin(%x);
  %sy = sin(%y);
  %sz = sin(%z);
  return %cy*%cz                @ " " @ %cy*%sz                @ " " @ %sy     @ " " @
         (-%sx*%sy*%cz-%cx*%sz) @ " " @ (-%sx*%sy*%sz+%cx*%cz) @ " " @ %sx*%cy @ " " @
         (-%cx*%sy*%cz+%sx*%sz) @ " " @ (-%cx*%sy*%sz-%sx*%cz) @ " " @ %cx*%cy;
}

function Matrix::transpose(%m, %mr, %mc) {
  %newM = "";
  for (%c = 0; %c < %mc; %c++) {
    for (%r = 0; %r < %mr; %r++) {
      if (%r == 0 && %c == 0) %newM = getWord(%m, 0);
      else                    %newM = %newM @ " " @ Matrix::getElem(%m, %c, %r, %mr);
    }
  }
  return %newM;
}


function Matrix::transformMuzzle(%muzzle, %trans) {
  %muzzleTrans = Matrix::subMatrix(%muzzle, 3, 4, 3, 3);
  %newTrans = Matrix::mul(%trans, %muzzleTrans);
  return %newTrans @ " " @ getWord(%muzzle, 9) @ " " @ getWord(%muzzle, 10) @ " " @ getWord(%muzzle, 11);
}

function Matrix::lookAt(%start, %dest, %up) {
  if (%up == "") %up = "0 0 1";
  %y = Vector::normalize(Vector::sub(%dest, %start));
  %x = Vector::cross(%y, %up);
  %z = Vector::cross(%x, %y);
  return %x @ " " @ %y @ " " @ %z @ " " @ %start;
}

function Matrix::aimCorrect(%player, %matrix) {
  if (GameBase::getLOSInfo(%player, 300)) {
    %start = Matrix::subMatrix(%matrix, 3, 4, 3, 1, 0, 3);
    %matVecN = Matrix::subMatrix(%matrix, 3, 4, 3, 1, 0, 1);
    %aimVecN = Vector::normalize(Vector::sub($los::position, %start));
    if (Vector::dot(%matVecN, %aimVecN) > 0.95)
      return Matrix::lookAt(%start, $los::position, "0 1 0");
    else
      return %matrix;
  } else return %matrix;
}