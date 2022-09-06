//
// How to use arrays:
//
// An array is global and is referenced by name. The "default" array is named ""; otherwise, valid array
// names are valid variable names except underscore is prohibited.
//
// Arrays are accessed via the functions below. These parameter names are standard:
// * %a:     the array name; always last and optional (defaults to "default" array)
// * %b:     another array
// * %i, %j: an array index
// * %v:     a value
//
// N.B.: *always* clean up unused arrays using adel, as otherwise they will
//       accumulate indefinitely (exception: atmp() arrays auto cleanup).
//
// Functions (all take optional final arg. for array name, or none for default array):
// * alen()       : length
// * aget(%i)     : get element %i
// * aset(%i,%v)  : set element %i to %v
// * aclr(%i)     : clear element %i
// * aswap(%i,%j) : swap elements %i and %j
// * apush(%v)    : append %v as last
// * apop()       : pop last
// * asetpop(%i)  : set element %i to last element's value, then pop
// * afind(%v)    : first index of %v, or -1 if not found
//
// * acopy(%b)    : append array's elements to other array %b
// * acompact()   : shift non-"" elements left to replace "" elements
// * asort()      : sort array
//
// * aitfirst()         : start iteration, return first
// * aitnext()          : continue iteration, return next
// * aitdone()          : is iteration done?
// * ait()              : iteration next index
// * ado(%f,%a,...)     : iterate %a, call %f with ... args followed by the element
//                        (NOTE: %a arg precedes ... args, not last as usual)
// * ado2(%f,%k,%a,...) : iterate %a, call %f with ... args but replace %k'th arg with the element
//                        (NOTE: %a arg precedes ... args, not last as usual)
//
// * aeq(%b) : check if equal to array %b
//
// Destructors (args as above):
// * adel()      : delete array (release all memory)
// * adellater() : delete array on schedule(..., 0) (after call stack returns)
//
// Constructors (args as above, but also return an array):
// * anew()            : returns new (unique) empty array (note: takes no args)
// * atmp()            : returns new (unique) empty temporary array. auto-deleted on
//                       schedule(..., 0) (note: takes no args)
// * afromval(%v)      : make array with single element %v
// * afroma(%b)        : make array as a copy of array %b
// * afromwords(%s)    : make array from the words in %s (clears array first)
// * afromvar(%vn,%vl) : make array from global var named %vn (if %vn == "$abc", use values
//                       $abc[%i]), up to len %vl (or if omitted, up to first "" value)
// * afromset(%set)    : make array from object IDs in SimSet/SimGroup/etc. %set
//
// Conversions:
// * astr()       : stringify array (space-separated elements)
// * atowords()   : synonym for astr
// * atoset(%set) : add all elements to SimSet/SimGroup/etc. %set
//
// Internal storage for array %a:
// $_a_[%a] = len;  // if "", then array is unused
// $_a_[%a, ""] = cur idx (for iteration)
// $_a_[%a, %i] = %i'th value;
//
// Other internals:
// $_anext = next new array idx
//

// alen returns the array's length
function alen(%a) { return def($_a_[%a], 0); }

// aget gets the value at an array index (or "" if invalid index)
function aget(%i, %a) {
	if ((%i = aidx(%i, %a)) == "") return "";
	return $_a_[%a, %i];
}

// aset sets the value at an array index (cannot set past end of array; no-op if invalid index)
function aset(%i, %v, %a) {
	if ((%i = aidx(%i, %a)) == "") return "";
	return $_a_[%a, %i] = %v;
}

// adel clears a value at an array index to "".
// It is equivalent to aset(%i, "", %a).
// Note: this does not _remove_ the element; to fully remove an element, follow with a call to
// acompact, or use asetpop instead.
function aclr(%i, %a) { aset(%i, "", %a); }

// aswap swaps two elements in an array (no-op if invalid either index is invalid)
function aswap(%i, %j, %a) {
	if ((%i = aidx(%i, %a)) == "" || (%j = aidx(%j, %a)) == "" || %i == %j) return;
	%tmp = $_a_[%a, %i];
	$_a_[%a, %i] = $_a_[%a, %j];
	$_a_[%a, %j] = %tmp;
}

// apush appends a new last element to an array
function apush(%v, %a) {
	$_a_[%a, $_a_[%a]++ - 1] = %v;
}

// apop removes and returns the last element of an array (or "" if empty)
function apop(%a) {
	if (alen(%a) == 0) return "";
	%v = $_a_[%a, $_a_[%a]--];
	return %v;
}

// asetpop sets element %i to the last element's value, then pops the last element (no-op if empty)
// It is equivalent to aset(%i, apop(%a), %a).
// It is an efficient way to delete element %i if you don't care about the array's order.
function asetpop(%i, %a) {
	aset(%i, apop(%a), %a);
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
// Note: only one iteration may be active on an array at a time; code iterating should
//       be careful that a called function does not inadvertently clobber the iterator.
function aitfirst(%a) {
	$_a_[%a, ""] = "";
	return aitval(%a);
}

// aitnext advances an array's iterator and returns the new value (no-op if iteration is done)
function aitnext(%a) {
	if (aitdone(%a)) return "";
	$_a_[%a, ""]++;
	return aitval(%a);
}

// aitdone returns true iff after aitfirst/aitnext has stepped past the end of the iterator
function aitdone(%a) { return ait(%a) == alen(%a); }

// ait returns current index of the active iterator (or 0 if none active)
function ait(%a) { return def($_a_[%a, ""], 0); }

// aitval returns current element of the active iterator (or "" if none active)
function aitval(%a) {
	if (aitdone(%a)) return "";
	return aget(ait(%a), %a);
}

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

function aeq(%b, %a) {
	if ((%l = alen(%a)) != alen(%b)) return false;
	for (%i = 0; %i < %l; %i++)
		if (aget(%i, %a) != aget(%i, %b)) return false;
	return true;
}



// adel deletes an entire array
function adel(%a) {
	%l = alen(%a);
	for (%i = 0; %i < %l; %i++) $_a_[%a, %i] = "";
	$_a_[%a, ""] = "";
	$_a_[%a] = "";
}

// adellater deletes an entire array on schedule(..., 0) and returns the array (which is still
// valid until the current call stack returns).
function adellater(%a) {
	schedule("adel(" @ %a @ ");", 0);
	return %a;
}

// acopy appends all elements of array %a to array %b.
// Clear %b first if you want an exact copy of %a.
function acopy(%b, %a) {
	for (%v = aitfirst(%a); !aitdone(%a); %v = aitnext(%a)) apush(%v, %b);
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
	$_a_[%a] = %nl;
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


//
// Constructors
//

// atmp returns a new array (arbitrary unique ID). You must clean it up (adel) yourself.
function anew() {
	// OK to use prefix underscore, because $_a_["_tmp" @ %j] cannot match
	// $_a_[%a, %i] or $_a_[%a, ""]. Users are not supposed to use _ in their
	// array names, so this keeps us in a separate namespace.
	%id = "_tmp" @ ($_anext++ - 1);
	adel(%id);
	return %id;	
}

// atmp returns a temporary array that will be automatically cleaned up as per adellater().
// This is equivalent to adellater(anew()).
function atmp() {
	return adellater(anew());
}

// afromval clears %a, loads it with a single element %v, and returns %a.
// It's equivalent to adel(%a); apush(%v, %a);
function afromval(%v, %a) {
	adel(%a);
	apush(%v, %a);
	return %a;
}

// afroma clears %a, loads it with the contents of %b, and returns %a.
// It's equivalent to adel(%a); acopy(%b, %a);
function afroma(%b, %a) {
	adel(%a);
	acopy(%b, %a);
	return %a;
}

// afromwords clears %a, loads it with the words from %s as elements, and returns %a.
function afromwords(%s, %a) {
	adel(%a);
	for (%i = 0; (%w = getWord(%s, %i)) != -1; %i++) apush(%w, %a);
	return %a;
}

// afromvar clears %a, loads it with the elements from the array-variable named %vn, up
// to %vl elements (or if omitted, up to first "" value), and returns %a.
//
// Example: afromvar("$abc", 3, %a) loads $abc[0], $abc[1], $abc[2] into %a
function afromvar(%vn, %vl, %a) {
	adel(%a);
	for (%i = 0; %vl == "" || %i < %vl; %i++) {
		%v = eval("%_ = " @ %vn @ "[" @ %i @ "];");
		if (%vl == "" && %v == "") break;
		apush(%v, %a);
	}
	return %a;
}

// afromset clears %a, loads it with all object IDs in the given set, and returns %a.
function afromset(%set, %a) {
	adel(%a);
	for (%i = 0; (%obj = Group::getObject(%set, %i)) != -1; %i++) apush(%obj, %a);
	return %a;
}

// astr returns the content of an array as a string (space-separated values)
function astr(%a) {
	%l = alen(%a);
	%s = "" @ aget(0, %a);
	for (%i = 1; %i < %l; %i++) %s = %s @ " " @ aget(%i, %a);
	return %s;
}

// atowords is a synonym for astr.
function atowords(%a) { return astr(%a); }

// atoset adds all values in %a to set %set.
// Note: this does not clear %set before adding elements.
function atoset(%set, %a) {
	for (%obj = aitfirst(%a); !aitdone(%a); %obj = aitnext(%a))
		addToSet(%set, %obj);
}

//
// Internal helpers
//

// aidx coerces %i to a valid index (integer-ifying), returning "" if out of bounds
// (this is primarily a helper function for this file)
function aidx(%i, %a) {
	%i |= 0; // integer-ify
	return tern(%i >= 0 && %i < alen(%a), %i, "");
}
