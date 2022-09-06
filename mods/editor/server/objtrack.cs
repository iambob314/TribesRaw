//
// Object tracker: allows checking the "version" of an object ID, which changes whenever the
// object is deleted and a new object reuses the same ID. This allows us to keep arrays of object
// IDs safely, rather than converting to/from sets everywhere.
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

// ObjTracker::vobjArray converts an array of object IDs to an array of "versioned objects"
// with form "objID versionID", skipping any non-existent objects.
function ObjTracker::vobjArray(%objArr, %vobjArr) {
	assert(%objArr != %vobjArr, "%objArr and %vobjArr must not be the same array");
	for (%obj = aitfirst(%objArr); !aitdone(%objArr); %obj = aitnext(%objArr)) {
		if ((%v = ObjTracker::add(%obj)) == "") continue;
		apush(%obj @ " " @ %v, %vobjArr);
	}
}

// ObjTracker::filterVObjArray updates a "versioned object" array, removing any objects that have
// been deleted since it was created.
// It only looks at the first two words of each version object; any extra data after these words
// is preserved (for elements not pruned).
function ObjTracker::filterVObjArray(%vobjArr) {
	%updated = false;
	for (%vobj = aitfirst(%vobjArr); !aitdone(%vobjArr); %vobj = aitnext(%vobjArr)) {
		%obj = getWord(%vobj, 0);
		%v = getWord(%vobj, 1);
		if (ObjTracker::check(%obj) != %v) {
			aclr(ait(%vobjArr), %vobjArr);
			%updated = true;
		}
	}
	if (%updated) acompact(%vobjArr);
	return %vobjArr;
}
