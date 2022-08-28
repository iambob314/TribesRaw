//
// How to use arrays:
//
// An array is global and is referenced by name. The "default" array is named ""; otherwise, valid array
// names are valid variable names except underscore is prohibited.
//
// Arrays are accessed via the functions below. These parameter names are standard:
// * %a:     the array name; always last and optional (defaults to "default" array)
// * %i, %j: an array index
// * %v:     a value
//
// N.B.: *always* clean up unused arrays using adel, as otherwise they will accumulate indefinitely.
//
// Functions (all take optional final arg. for array name, or none for default array):
// * alen()       : length
// * adel()       : delete array
// * aget(%i)     : get element %i
// * aset(%i,%v)  : set element %i to %v
// * aclr(%i)     : clear element %i
// * aswap(%i,%j) : swap elements %i and %j
// * apush(%v)    : append %v as last
// * apop()       : pop last
// * afind(%v)    : first index of %v, or -1 if not found
//
// * acompact()   : shift non-"" elements left to replace "" elements
// * asort()      : sort array
// * astr()       : stringify array (space-separated elements)
//
// * aitfirst()         : start iteration, return first
// * aitnext()          : continue iteration, return next
// * aitdone()          : is iteration done?
// * ait()              : iteration next index
// * ado(%f,%a,...)     : iterate %a, call %f with ... args followed by the element
// * ado2(%f,%k,%a,...) : iterate %a, call %f with ... args but replace %k'th arg with the element
//                        (NB: %a arg precedes ... args, not last as usual)
//
// * afromwords(%s)    : make array from the words in %s (clears array first)
// * afromvar(%vn,%vl) : make array from global var named %vn (if %vn == "$abc", use values $abc[%i]), up to len %vl (or if omitted, up to first "" value)
//
// Internal storage for array %a:
// $_a[%a] = len;  // if "", then array is unused
// $_a[%a, ""] = cur idx (for iteration)
// $_a[%a, %i] = %i'th value;
//

function alen(%a) { return def($_a[%a], 0); }

// aget gets the value at an array index (or "" if invalid index)
function aget(%i, %a) {
	if ((%i = aidx(%i, %a)) == "") return "";
	return $_a[%a, %i];
}

// aset sets the value at an array index (cannot set past end of array; no-op if invalid index)
function aset(%i, %v, %a) {
	if ((%i = aidx(%i, %a)) != "")
		return $_a[%a, %i];
}

// adel clears a value at an array index to ""; equivalent to aset(%i, "", %a)
function aclr(%i, %a) { aset(%i, "", %a); }

// aswap swaps two elements in an array (no-op if invalid either index is invalid)
function aswap(%i, %j, %a) {
	if ((%i = aidx(%i, %a)) == "" || (%j = aidx(%j, %a)) == "" || %i == %j) return;
	%tmp = $_a[%a, %i];
	$_a[%a, %i] = $_a[%a, %j];
	$_a[%a, %j] = %tmp;
}

// apush appends a new last element to an array
function apush(%v, %a) {
	$_a[%a, $_a[%a]++ - 1] = %v;
}

// apop removes and returns the last element of an array (or "" if empty)
function apop(%a) {
	if (alen(%a) == 0) return "";
	%v = $_a[%a, $_a[%a]--];
	return %v;
}

// afind finds value %v in an array, returning its index or -1 if not found
function afind(%v, %a) {
	%l = alen(%a);
	for (%i = 0; %i < %l; %i++)
		if (aget(%i, %a) == %v)
			return %i;
	return -1;
}



// aitfirst starts a new iteration on an array and returns the first value
// Note: only one iteration may be active on an array at a time; code iterating should be careful that a called
//       function does not inadvertently clobber the iterator.
function aitfirst(%a) {
	$_a[%a, ""] = "";
	return aget(ait(%a), %a);
}

// aitnext advances an array's iterator and returns the new value (no-op if iteration is done)
function aitnext(%a) {
	if (aitdone(%a)) return "";
	$_a[%a, ""]++;
	return aget(ait(%a), %a);
}

// aitdone returns true iff after aitfirst/aitnext has stepped past the end of the iterator
function aitdone(%a) { return ait(%a) == alen(%a); }

// ait returns current index of the active iterator (or 0 if none active)
function ait(%a) { return def($_a[%a, ""], 0); }

// ado iterates over an array, calling %f(%a0, %a1, ..., %v) for each element %v
function ado(%f, %a, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9) {
	%insertIdx = 0;
	for (%i = 0; %i < 10; %i++) {
		if (%a[%i] != "") { %insertIdx = %i+1; }
	}
	assert(%insertIdx < 10, "ado overflow");
	ado2(%f, %insertIdx, %a, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9);
}

// ado iterates over an array, calling %f(%a0, %a1, ...) with %a[%k] replaced with %v for
// each element %v
function ado2(%f, %k, %a, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9) {
	for (%v = aitfirst(%a); !aitdone(%a); %v = aitnext(%a)) {
		%a[%k] = %v;
		invoke(%f, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9);
	}
}


// adel deletes an entire array
function adel(%a) {
	%l = alen(%a);
	for (%i = 0; %i < %l; %i++) $_a[%a, %i] = "";
	$_a[%a] = "";
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
	$_a[%a] = %nl;
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


//
// Mass constructors
//

// afromwords clears %a and loads it with the words from %s as elements.
function afromwords(%s, %a) {
	adel(%a);
	for (%i = 0; (%w = getWord(%s, %i)) != -1; %i++) apush(%w, %a);
}


// afromvar clears %a and loads it with the elements from the array-variable named %vn, up
// to %vl elements (or if omitted, up to first "" value).
//
// Example: afromvar("$abc", 3, %a) loads $abc[0], $abc[1], $abc[2] into %a
function afromvar(%vn, %vl, %a) {
	adel(%a);
	for (%i = 0; %vl == "" || %i < %vl; %i++) {
		%v = eval("%_ = " @ %vn @ "[" @ %i @ "];");
		if (%vl == "" && %v == "") break;
		apush(%v, %a);
	}
}

