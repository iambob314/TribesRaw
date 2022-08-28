//
// Editor object registry
//

// Arrays:
// ME::objgroups                : list of group names
// ME::objgroup_%group::names   : list of objects names in group
// ME::objgroup_%group::scripts : list of object create scripts in group

function EditorRegistry::groups() { return EditorRegistry::groups; }
function EditorRegistry::groupNames(%group)   { return EditorRegistry::gnames_ @ %group; }
function EditorRegistry::groupScripts(%group) { return EditorRegistry::gscripts_ @ %group; }

function EditorRegistry::addScript(%group, %name, %script) {
	%groups = EditorRegistry::groups();
	if (afind(%group, %groups) == -1)
		apush(%group, %groups);
	
	apush(%name, EditorRegistry::groupNames(%group));
	apush(%script, EditorRegistry::groupScripts(%group));
}

function EditorRegistry::addObject(%group, %name, %kind, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7) {
	%script = invokestr(MissionCreateObject, %name, %kind, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7);
	EditorRegistry::addScript(%group, %name, %script);
}

function EditorRegistry::addDIS(%group, %name, %disfile) {
	EditorRegistry::addObject(%group, %name, InteriorShape, %disfile);
}

function EditorRegistry::addStaticShape(%group, %name, %shape) {
	EditorRegistry::addObject(%group, %name, StaticShape, %shape, false);
}

function EditorRegistry::clearGroup(%group) {
	adel(EditorRegistry::groupNames(%group));
	adel(EditorRegistry::groupScripts(%group));
}

function EditorRegistry::clear() {
	%groupArr = EditorRegistry::groups();
	ado(EditorRegistry::clearGroup, %groupArr);
	adel(%groupArr);
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

schedule("EditorRegistry::addDefaults();", 0);