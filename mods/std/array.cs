//
// How to use arrays:
//
// An array is global and is referenced by name. Array names must be valid variable names that
// do not contain underscores.
//
// Arrays are accessed via the functions below. These parameter names are standard:
// * %a:     the array name; always first
// * %b:     another array
// * %i, %j: an array index
// * %v:     a value
//
// N.B.: *always* clean up unused arrays using adel(), as otherwise they will accumulate
//       indefinitely (exception: atmp() and adellater() arrays auto-cleanup).
//
// For all functions below, the implicit first argument is %a, the array, except where noted.
//
// Basic functions:
// * alen()       : length
// * aget(%i)     : get element %i
// * aset(%i,%v)  : set element %i to %v
// * aclr(%i)     : clear element %i
// * aswap(%i,%j) : swap elements %i and %j
// * apush(%v)    : append %v as last
// * apop()       : pop last
// * asetpop(%i)  : set element %i to last element's value, then pop
// * afind(%v)    : first index of %v, or -1 if not found
// * afirst()     : first element
// * alast()      : last element
// * asetlen(%l)  : resize to length (pad with "" or truncate)
//
// Bulk functions:
// * aeq(%b)      : check if equal to array %b
// * acopy(%b)    : append array's elements to other array %b
// * acompact()   : shift non-"" elements left to replace "" elements
// * asort()      : sort array
//
// Iteration functions:
// * aitfirst()      : start iteration, return first
// * aitnext()       : continue iteration, return next
// * aitdone()       : is iteration done?
// * ait()           : iteration next index
// * ado(%f,...)     : for each %v, call %f(...,%v)
// * ado2(%f,%k,...) : for each %v, call %f(...,%v) but replace %k'th arg with %v
//
// Transform functions:
// * amap(%b,%f,...)     : fill %b with values %f(...,%v) for each %v in %a (%a, %b may be same)
// * amap2(%b,%f,%k,...) : fill %b with values %f(...) (but replace %k'th arg with %v) for each
//                         %v in %a (%a, %b may be same)
// * areduce(%f)         : reduce all %v in %a to %r using %r = f(%r, %v) (or "" if 0-length)
//
// Conversion functions:
// * astr()       : stringify array (space-separated elements)
// * atowords()   : synonym for astr
// * atoset(%set) : add all elements to SimSet/SimGroup/etc. %set
//
// Destructors:
// * adel()      : delete array (release all memory)
// * adellater() : delete array on schedule(..., 0) (after call stack returns)
//
// Constructors (all return an array):
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
// Internal storage for array %a:
// $_a_[%a] = len;  // if "", then array is unused
// $_a_[%a, ""] = cur idx (for iteration)
// $_a_[%a, %i] = %i'th value;
//
// Other internals:
// $_anext = next new array idx
//

function achk(%a) { if (%a == "") { echo("DEF ARRAY"); trace(); } }

// alen returns the array's length
function alen(%a) { achk(%a); return def($_a_[%a], 0); }

// aget gets the value at an array index (or "" if invalid index)
function aget(%a, %i) {
	achk(%a);
	if ((%i = aidx(%a, %i)) == "") return "";
	return $_a_[%a, %i];
}

// aset sets the value at an array index (cannot set past end of array; no-op if invalid index)
function aset(%a, %i, %v) {
	achk(%a);
	if ((%i = aidx(%a, %i)) == "") return "";
	return $_a_[%a, %i] = %v;
}

// adel clears a value at an array index to "".
// It is equivalent to aset(%a, %i, "").
// Note: this does not _remove_ the element; also call acompact, or use asetpop instead, to do so.
function aclr(%a, %i) { aset(%a, %i, ""); }

// aswap swaps two elements in an array (no-op if invalid either index is invalid)
function aswap(%a, %i, %j) {
	achk(%a);
	if ((%i = aidx(%a, %i)) == "" || (%j = aidx(%a, %j)) == "" || %i == %j) return;
	%tmp = $_a_[%a, %i];
	$_a_[%a, %i] = $_a_[%a, %j];
	$_a_[%a, %j] = %tmp;
}

// apush appends a new last element to an array
function apush(%a, %v) {
	achk(%a);
	$_a_[%a, $_a_[%a]++ - 1] = %v;
}

// apop removes and returns the last element of an array (or "" if empty)
function apop(%a) {
	achk(%a);
	if (alen(%a) == 0) return "";
	%v = $_a_[%a, $_a_[%a]--];
	return %v;
}

// asetpop sets element %i to the last element's value, then pops the last element (no-op if empty)
// It is equivalent to aset(%a, %i, apop(%a)).
// It is an efficient way to delete element %i if you don't care about the array's order.
function asetpop(%a, %i) {
	achk(%a);
	aset(%a, %i, apop(%a));
}

// afind finds value %v in an array, returning its index or -1 if not found
function afind(%a, %v) {
	achk(%a);
	%l = alen(%a);
	for (%i = 0; %i < %l; %i++)
		if (aget(%a, %i) == %v)
			return %i;
	return -1;
}

// afirst returns the first value in an array, or "" if the array is empty.
// It's equivalent to aget(%a, 0).
function afirst(%a) { return aget(%a, 0); }

// alast returns the last value in an array, or "" if the array is empty.
// It's equivalent to aget(alen(%a)-1, %a).
function alast(%a) { return aget(%a, alen(%a)-1); }

// afind finds value %v in an array, returning its index or -1 if not found
function afind(%a, %v) {
	achk(%a);
	%l = alen(%a);
	for (%i = 0; %i < %l; %i++)
		if (aget(%a, %i) == %v)
			return %i;
	return -1;
}



// aeq tests if array %a and %b have identical contents.
function aeq(%a, %b) {
	achk(%a); achk(%b);
	if (%a == %b) return true; // same array
	if ((%l = alen(%a)) != alen(%b)) return false;
	for (%i = 0; %i < %l; %i++)
		if (aget(%a, %i) != aget(%b, %i)) return false;
	return true;
}

// asetlen adjusts array %a's length to %l, truncating or extending with "" values as needed.
// No elements at indices below %l are affected.
// asetlen(alen(%a), %a) is a no-op.
function asetlen(%a, %l) {
	achk(%a);
	%l = %l | 0; // integerify len
	%ol = alen(%a);
	%from = min(%ol, %l);
	%to = max(%ol, %l);
	for (%i = %from; %i < %to; %i++) $_a_[%a, %i] = ""; // clear added/removed elements
	$_a_[%a] = %l;
}

// acopy appends all elements of array %a to array %b.
// Clear %b first if you want an exact copy of %a.
function acopy(%a, %b) {
	achk(%a); achk(%b);
	for (%v = aitfirst(%a); !aitdone(%a); %v = aitnext(%a)) apush(%b, %v);
}

// acompact "compacts" an array, removing all "" values by shifting non-"" values left to fill
function acompact(%a) {
	achk(%a);
	%l = alen(%a);
	%nl = 0;
	for (%i = 0; %i < %l; %i++) {
		if ((%v = aget(%a, %i)) != "") {
			aset(%a, %nl, %v);
			%nl++;
		}
	}
	$_a_[%a] = %nl;
}

// asort sorts an array
function asort(%a) { achk(%a); asubsort(0, alen(%a), %a); }

// asort sorts a range of an array, from index %i incl. to index %j excl.
function asubsort(%a, %i, %j) {
	// stop if invalid indices or less than 2 elements
	if ((%i = aidx(%a, %i)) == "" || (%j = aidx(%a, %j)) == "" || %j - %i < 2) return;
	
	// Sift with pivot
	%pv = aget(%a, %j-1);
	%pi = %i;
	for (%k = %i; %k < %j-1; %k++) {
		if (aget(%a, %k) < %pv) {
			aswap(%a, %pi, %k);
			%pi++;
		}
	}
	aswap(%a, %pi, %j-1); // put pivot in final location
	
	// Recursively sort left and right of the pivot
	asubsort(%a, %i, %pi);
	asubsort(%a, %pi+1, %j);
}



// aitfirst starts a new iteration on an array and returns the first value
// Note: only one iteration may be active on an array at a time; code iterating should
//       be careful that a called function does not inadvertently clobber the iterator.
function aitfirst(%a) {
	achk(%a);
	$_a_[%a, ""] = "";
	return aitval(%a);
}

// aitnext advances an array's iterator and returns the new value (no-op if iteration is done)
function aitnext(%a) {
	achk(%a);
	if (aitdone(%a)) return "";
	$_a_[%a, ""]++;
	return aitval(%a);
}

// aitdone returns true iff after aitfirst/aitnext has stepped past the end of the iterator
function aitdone(%a) { achk(%a); return ait(%a) == alen(%a); }

// ait returns current index of the active iterator (or 0 if none active)
function ait(%a) { achk(%a); return def($_a_[%a, ""], 0); }

// aitval returns current element of the active iterator (or "" if none active)
function aitval(%a) {
	achk(%a);
	return aget(%a, ait(%a)); // returns "" when ait(%a) == alen(%a) at end of iteration
}

// ado iterates over an array, calling %f(%a0, %a1, ..., %v) for each element %v
function ado(%a, %f, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9) {
	achk(%a);
	%k = 0;
	for (%i = 0; %i < 10; %i++) {
		if (%a[%i] != "") { %k = %i+1; }
	}
	assert(%k < 10, "ado overflow");
	ado2(%a, %f, %k, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9);
}

// ado iterates over an array, calling %f(%a0, %a1, ...) with %a[%k] replaced with %v for
// each element %v
function ado2(%a, %f, %k, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9) {
	achk(%a);
	for (%v = aitfirst(%a); !aitdone(%a); %v = aitnext(%a)) {
		%a[%k] = %v;
		invoke(%f, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9);
	}
}

// amap populates array %a with values %f(%a0, %a1, ..., %v) for each value %v in array %b.
// It returns array %a.
// If %a == %b, this transforms %a in-place.
function amap(%a, %b, %f, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9) {
	achk(%a);
	%k = 0;
	for (%i = 0; %i < 10; %i++) {
		if (%a[%i] != "") { %k = %i+1; }
	}
	assert(%k < 10, "amap overflow");
	return amap2(%a, %b, %f, %k, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9);
}

// amap2 populates array %a with values %f(%a0, %a1, ...) (except with %a[%k] replaced by %v)
// for each value %v in array %b.
// It returns array %a.
// If %a == %b, this transforms %a in-place.
function amap2(%a, %b, %f, %k, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9) {
	achk(%a); achk(%b);
	%l = alen(%b);
	asetlen(%a, %l); // prepare %a (no-op if %a == %b)
	for (%i = 0; %i < %l; %i++) {
		%a[%k] = aget(%b, %i);
		%v = invoke(%f, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9);
		aset(%a, %i, %v);
	}
	return %a;
}

// areduce returns the result of applying %f(%v1, %v2) as a reduction to array %a.
// If %a has length >2, it applies %f to the first two elements, then repeatedly applies
// %f to the previous result and the next element, and returns the final result.
// If %a has length 1, it returns the single element.
// If %a has length 0, it returns "".
function areduce(%a, %f) {
	if ((%l = alen(%a)) == 0) return "";
	%v = aget(%a, 0);
	for (%i = 1; %i < %l; %i++)
		%v = invoke(%f, %v, aget(%a, %i));
	return %v;
}



// adel deletes an entire array
function adel(%a) {
	achk(%a);
	%l = alen(%a);
	for (%i = 0; %i < %l; %i++) $_a_[%a, %i] = "";
	$_a_[%a, ""] = "";
	$_a_[%a] = "";
}

// adellater deletes an entire array on schedule(..., 0) and returns the array (which is still
// valid until the current call stack returns).
function adellater(%a) {
	achk(%a);
	schedule("adel(" @ %a @ ");", 0);
	return %a;
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
// It's equivalent to adel(%a); apush(%a, %v);
function afromval(%a, %v) {
	achk(%a);
	adel(%a);
	apush(%a, %v);
	return %a;
}

// afroma clears %a, loads it with the contents of %b, and returns %a.
// It's equivalent to adel(%a); acopy(%a, %b);
function afroma(%a, %b) {
	achk(%a); achk(%b);
	adel(%a);
	acopy(%a, %b);
	return %a;
}

// afromwords clears %a, loads it with the words from %s as elements, and returns %a.
function afromwords(%a, %s) {
	achk(%a);
	adel(%a);
	for (%i = 0; (%w = getWord(%s, %i)) != -1; %i++) apush(%a, %w);
	return %a;
}

// afromvar clears %a, loads it with the elements from the array-variable named %vn, up
// to %vl elements (or if omitted, up to first "" value), and returns %a.
//
// Example: afromvar(%a, "$abc", 3) loads $abc[0], $abc[1], $abc[2] into %a
function afromvar(%a, %vn, %vl) {
	achk(%a);
	adel(%a);
	for (%i = 0; %vl == "" || %i < %vl; %i++) {
		%v = eval("%_ = " @ %vn @ "[" @ %i @ "];");
		if (%vl == "" && %v == "") break;
		apush(%a, %v);
	}
	return %a;
}

// afromset clears %a, loads it with all object IDs in the given set, and returns %a.
function afromset(%a, %set) {
	achk(%a);
	adel(%a);
	for (%i = 0; (%obj = Group::getObject(%set, %i)) != -1; %i++) apush(%a, %obj);
	return %a;
}

// astr returns the content of an array as a string (space-separated values)
function astr(%a) {
	achk(%a);
	%l = alen(%a);
	%s = "" @ aget(%a, 0);
	for (%i = 1; %i < %l; %i++) %s = %s @ " " @ aget(%a, %i);
	return %s;
}

// atowords is a synonym for astr.
function atowords(%a) { achk(%a); return astr(%a); }

// atoset adds all values in %a to set %set.
// Note: this does not clear %set before adding elements.
function atoset(%a, %set) {
	achk(%a);
	for (%obj = aitfirst(%a); !aitdone(%a); %obj = aitnext(%a))
		addToSet(%set, %obj);
}

//
// Internal helpers
//

// aidx coerces %i to a valid index (integer-ifying), returning "" if out of bounds
// (this is primarily a helper function for this file)
function aidx(%a, %i) {
	achk(%a);
	%i |= 0; // integer-ify
	return tern(%i >= 0 && %i < alen(%a), %i, "");
}
