//
// Object tracker: allows checking the "version" of an object ID, which changes whenever the
// object is deleted and a new object reuses the same ID. This allows us to keep arrays of object
// IDs safely, rather than converting to/from sets everywhere.
//
// A "versioned object" is an object ID/version pair "objID vID", and can be a useful way to
// track stable (sets of) object(s). Converters to/from normal object IDs are included.
//
// Method: basic idea is to keep a global SimSet, along with a version ID for each object ID:
// * Whenever an object is added: set version ID to current
// * Whenever an object is checked: if in set, return version; else clear version and return ""
// If an object is deleted, it is reliabily removed from the set, so check yields "".
// If it's later recreated/retracked, it will have a new version ID.
// Together, this means a check after object delete will always return a new version ID (or "").
//

$objTracker = newObject("ObjTracker", SimSet);

function ObjTracker::add(%obj) {
	assert(isObject($objTracker), "missing $objTracker");
	if (!isObject(%obj)) return "";

	if (isMember($objTracker, %obj)) return $objTracker.v[%obj];
	
	addToSet($objTracker, %obj);
	%v = $objTracker.v++;
	$objTracker.v[%obj] = %v;
	return %v;
}

function ObjTracker::check(%obj) {
	assert(isObject($objTracker), "missing $objTracker");
	if (!isObject(%obj)) return "";

	if (isMember($objTracker, %obj)) return $objTracker.v[%obj];
	
	return "";
}

//
// Conversion functions
//

// ObjTracker::toVObj returns the versioned object for an object ID, or "" if object non-existent.
function ObjTracker::toVObj(%obj) {
	if ((%v = ObjTracker::add(%obj)) == "") return "";
	return %obj @ " " @ %v;
}

// ObjTracker::fromVObj returns the object ID for a versioned object, or "" if object invalidated.
function ObjTracker::fromVObj(%vobj) {
	%obj = getWord(%vobj, 0);
	%v = getWord(%vobj, 1);
	if (ObjTracker::check(%obj) != %v) return "";
	return %obj;
}

// ObjTracker::toVObjs creates a "versioned object" array from an array of object IDs,
// skipping any non-existent objects.
function ObjTracker::toVObjs(%objArr, %vobjArr) {
	assert(%objArr != %vobjArr, "%objArr and %vobjArr must not be the same array");
	adel(%vobjArr);
	for (%obj = aitfirst(%objArr); !aitdone(%objArr); %obj = aitnext(%objArr))
		if ((%v = ObjTracker::add(%obj)) != "")
			apush(%obj @ " " @ %v, %vobjArr);
	return %vobjArr;
}

// ObjTracker::fromVObjs creates an object ID array from a "versioned object" array,
// skipping any invalidated objects.
// It only consiers first two words of each version object; any extra data is ignored.
function ObjTracker::fromVObjs(%vobjArr, %objArr) {
	assert(%objArr != %vobjArr, "%objArr and %vobjArr must not be the same array");
	adel(%objArr);
	for (%vobj = aitfirst(%vobjArr); !aitdone(%vobjArr); %vobj = aitnext(%vobjArr))
		if ((%obj = ObjTracker::fromVObj(%vobj)) != "")
			apush(%obj, %objArr);
	return %objArr;
}

// ObjTracker::pruneVObjs updates a "versioned object" array, removing invalidated objects.
// It only consiers first two words of each version object; any extra data is preserved.
function ObjTracker::pruneVObjs(%vobjArr) {
	%l = alen(%vobjArr);
	for (%i = %nl = 0; %i < %l; %i++) {
		%vobj = aget(%i, %vobjArr);
		if (ObjTracker::fromVObj(%vobj) == "") continue;
		aset(%nl, %vobj, %vobjArr);
	}
	asetlen(%nl, %vobjArr);
	return %vobjArr;
}
