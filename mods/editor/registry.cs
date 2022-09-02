//
// Editor object registry
//
// Stores a list of groups, each of which contains a list of name/arglist pairs.
// An object can be created from arglist by executing either:
// * eval("MissionCreateObject(" @ %arglist @ ");");
// * eval("newObject(" @ %arglist @ ");");
//

function EditorRegistry::groups() { return EditorRegistry::groups; }
function EditorRegistry::groupNames(%group)   { return EditorRegistry::gnames_ @ %group; }
function EditorRegistry::groupArglists(%group) { return EditorRegistry::gargs_ @ %group; }

function EditorRegistry::addItem(%group, %name, %arglist) {
	%groups = EditorRegistry::groups();
	if (afind(%group, %groups) == -1)
		apush(%group, %groups);
	
	apush(%name, EditorRegistry::groupNames(%group));
	apush(%arglist, EditorRegistry::groupArglists(%group));
}

function EditorRegistry::addObject(%group, %name, %kind, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7) {
	%arglist = argliststr(%name, %kind, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7);
	EditorRegistry::addItem(%group, %name, %arglist);
}

function EditorRegistry::addDIS(%group, %name, %disfile) {
	EditorRegistry::addObject(%group, %name, InteriorShape, %disfile);
}

function EditorRegistry::addStaticShape(%group, %name, %shape) {
	EditorRegistry::addObject(%group, %name, StaticShape, %shape, false);
}

// used by remote editor to mirror server
function EditorRegistry::addDummy(%group, %name) {
	EditorRegistry::addItem(%group, %name, "");
}

function EditorRegistry::clearGroup(%group) {
	adel(EditorRegistry::groupNames(%group));
	adel(EditorRegistry::groupArglists(%group));
}

function EditorRegistry::clear() {
	%groupArr = EditorRegistry::groups();
	ado(EditorRegistry::clearGroup, %groupArr);
	adel(%groupArr);
}

// EditorRegistry::getArglist returns the arglist for %group/%name, or "" if not exists.
function EditorRegistry::getArglist(%group, %name) {
	%namesArr = EditorRegistry::groupNames(%group);
	%arglistsArr = EditorRegistry::groupArglists(%group);
	if ((%idx = afind(%name, %namesArr)) == -1) return ""; // %group/%name not valid
	return aget(%idx, %arglistsArr);
}

//
// Defaults
//

function EditorRegistry::defaults() {
	EditorRegistry::clear();

	EditorRegistry::addObject(Mission,	Group,		SimGroup);
	EditorRegistry::addObject(Mission,	Set,		SimSet);
	EditorRegistry::addObject(Mission,	Path,		SimPath);
	EditorRegistry::addObject(Mission,	PointLight,	SimLight,	Point, 10, 1, 1, 1, 0, 0, 0);

	EditorRegistry::addObject(Sky,		Sky,		Sky,		0.5, 0.5, 0.5);
	EditorRegistry::addObject(Sky,		Planet,		Planet);
	EditorRegistry::addObject(Sky,		Starfield,	Starfield);
	EditorRegistry::addObject(Sky,		Snow,		Snowfall, 	1, 0, 0, 0);
	EditorRegistry::addObject(Sky,		Rain,		Snowfall, 	1, 0, 0, 1);

	// Temp: register all DIS shapes, indiscriminantly
	for (%dis = File::findFirst("*.dis"); %dis != ""; %dis = File::findNext("*.dis"))
		EditorRegistry::addDIS(Interior, File::getBase(%dis), %dis);
}
