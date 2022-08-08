


// Registration

// Arrays:
// ME::objgroups                : list of group names
// ME::objgroup_%group::names   : list of objects names in group
// ME::objgroup_%group::scripts : list of object create scripts in group

function Editor::registerScript(%group, %name, %script) {
	if (afind(%group, ME::objgroups) == -1)
		apush(%group, ME::objgroups);
	
	apush(%name, "ME::objgroup_" @ %group @ "::names");
	apush(%script, "ME::objgroup_" @ %group @ "::scripts");
}

function Editor::registerObject(%group, %name, %kind, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7) {
	%script = invokestr(MissionCreateObject, %name, %kind, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7);
	Editor::registerScript(%group, %name, %script);
}

function Editor::registerDIS(%group, %name, %disfile) {
	Editor::registerObject(%group, %name, InteriorShape, %disfile);
}

function Editor::registerStaticShape(%group, %name, %shape) {
	Editor::registerObject(%group, %name, StaticShape, %shape, false);
}

// Built-in objects

function Editor::registerClear() {
	%groupArr = ME::objgroups;
	for (%group = aitfirst(%groupArr); !aitdone(%groupArr); %group = aitnext(%groupArr)) {
		adel("ME::objgroup_" @ %group @ "::names");
		adel("ME::objgroup_" @ %group @ "::scripts");
	}
	adel(%groupArr);
}

function Editor::registerDefaults() {
	Editor::registerObject(Mission,	Group,		SimGroup);
	Editor::registerObject(Mission,	Set,		SimSet);
	Editor::registerObject(Mission,	Path,		SimPath);
	Editor::registerObject(Mission,	PointLight,	SimLight,	Point, 10, 1, 1, 1, 0, 0, 0);

	Editor::registerObject(Sky,		Sky,		Sky,		0.5, 0.5, 0.5);
	Editor::registerObject(Sky,		Planet,		Planet);
	Editor::registerObject(Sky,		Starfield,	Starfield);
	Editor::registerObject(Sky,		Snow,		Snowfall, 	1, 0, 0, 0);
	Editor::registerObject(Sky,		Rain,		Snowfall, 	1, 0, 0, 1);

	// Temp: register all DIS shapes, indiscriminantly
	for (%dis = File::findFirst("*.dis"); %dis != ""; %dis = File::findNext("*.dis"))
		Editor::registerDIS(Interior, File::getBase(%dis), %dis);
}

schedule("Editor::registerDefaults();", 0);