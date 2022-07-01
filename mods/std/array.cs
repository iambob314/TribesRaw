//
// How to use arrays:
//
// An array is global and is referenced by name. The "default" array is named ""; otherwise, valid array
// names are valid variable names, except understore is prohibited.
//
// Arrays are accessed via the functions below. Parameter naming is standard:
// * %a:     the array name; always last and optional (defaults to "default" array)
// * %i, %j: an array index
// * %v:     a value
//
// N.B.: *always* clean up unused arrays using adel, as otherwise they will accumulate indefinitely.
//
// Storage for array %X:
// $a[%X] = len;  // if "", then array is unused
// $a[%X, ""] = cur idx (for iteration)
// $a[%X, %i] = %i'th value;
//

function alen(%a) { return def($a[%a], 0); }

// aget gets the value at an array index (or "" if invalid index)
function aget(%i, %a) {
	if ((%i = aidx(%i, %a)) == "") return "";
	return $a[%a, %i];
}

// aset sets the value at an array index (cannot set past end of array; no-op if invalid index)
function aset(%i, %v, %a) {
	if ((%i = aidx(%i, %a)) != "")
		return $a[%a, %i];
}

// adel clears a value at an array index to ""; equivalent to aset(%i, "", %a)
function aclr(%i, %a) { aset(%i, "", %a); }

// aswap swaps two elements in an array (no-op if invalid either index is invalid)
function aswap(%i, %j, %a) {
	if ((%i = aidx(%i, %a)) == "" || (%j = aidx(%j, %a)) == "" || %i == %j) return;
	%tmp = $a[%a, %i];
	$a[%a, %i] = $a[%a, %j];
	$a[%a, %j] = %tmp;
}

// apush appends a new last element to an array
function apush(%v, %a) {
	$a[%a, $a[%a]++ - 1] = %v;
}

// apop removes and returns the last element of an array (or "" if empty)
function apop(%a) {
	if (alen(%a) == 0) return "";
	%v = $a[%a, $a[%a]--];
	return %v;
}



// aitfirst starts a new iteration on an array and returns the first value
// Note: only one iteration may be active on an array at a time; code iterating should be careful that a called
//       function does not inadvertently clobber the iterator.
function aitfirst(%a) {
	$a[%a, ""] = "";
	return aget(ait(%a), %a);
}

// aitnext advances an array's iterator and returns the new value (no-op if iteration is done)
function aitnext(%a) {
	if (aitdone(%a)) return;
	$a[%a, ""]++;
	return aget(ait(%a), %a);
}

// aitdone returns true iff an array's iterator is is at the end if the array
function aitdone(%a) { return ait(%a) == alen(%a); }

// ait returns current index of the active iterator (or 0 if none active)
function ait(%a) { return def($a[%a, ""], 0); }



// adel deletes an entire array
function adel(%a) {
	%l = alen(%a);
	for (%i = 0; %i < %l; %i++) $a[%a, %i] = "";
	$a[%a] = "";
}

// acompact "compacts" an array, removing all "" values by shifting non-"" values left to fill
function acompact(%a) {
	%l = alen(%a);
	%nl = 0;
	for (%i = 0; %i < %l; %i++) {
		if ((%v = aget(%i, %a)) != "") {
			aset(%nl, %v, %a);
			%nl++;
		}
	}
	$a[%a] = %nl;
}

// asort sorts an array
function asort(%a) { asubsort(0, alen(%a), %a); }

// asort sorts a range of an array, from index %i incl. to index %j excl.
function asubsort(%i, %j, %a) {
	// stop if invalid indices or less than 2 elements
	if ((%i = aidx(%i, %a)) == "" || (%j = aidx(%j, %a)) == "" || %j - %i < 2) return;
	
	// Sift with pivot
	%pv = aget(%j-1, %a);
	%pi = %i;
	for (%k = %i; %k < %j-1; %k++) {
		if (aget(%k, %a) < %pv) {
			aswap(%pi, %k, %a);
			%pi++;
		}
	}
	aswap(%pi, %j-1, %a); // put pivot in final location
	
	// Recursively sort left and right of the pivot
	asubsort(%i, %pi, %a);
	asubsort(%pi+1, %j, %a);
}

// astr returns the content of an array as a string (space-separated values)
function astr(%a) {
	%l = alen(%a);
	%s = "" @ aget(0, %a);
	for (%i = 1; %i < %l; %i++) %s = %s @ " " @ aget(%i, %a);
	return %s;
}

// aidx coerces %i to a valid index (integer-ifying), returning "" if not valid for the array (out of bounds)
// (this is primarily a helper function for this file)
function aidx(%i, %a) {
	%i |= 0; // integer-ify
	return tern(%i >= 0 && %i < alen(%a), %i, "");
}
