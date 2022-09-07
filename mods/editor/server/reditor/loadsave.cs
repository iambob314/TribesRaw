//
// Load/save objects and sandbox group
//

// Note: _must_ return a SimGroup
function REditor::sandboxGroup(%c) {
	%sbname = "Sandbox" @ %c;
	%g = nameToID("MissionGroup\\" @ %sbname);
	if (%g == -1) addToSet(MissionGroup, %g = newObject(%sbname, SimGroup));
	return %g;
}

function REditor::nextTmpFile(%c) { return "undobuf" @ (%c.tmpFileIdx++ -1); }

function REditor::deleteObjectFile(%c, %filebase) {
	%filename = "temp\\" @ %filebase @ "." @ %c @ ".cs";
	File::delete(%filename);
}
function REditor::saveObjects(%c, %filebase, %append, %objArr) {
	%filename = "temp\\" @ %filebase @ "." @ %c @ ".cs";
	
	// WOW: exportObjectToScript *only* recognizes literal "true", so coerce truthy value
	%clearFile = tern(!%append, true, false);
	
	for (%obj = aitfirst(%objArr); !aitdone(%objArr); %obj = aitnext(%objArr)) {
		%oldLevel = $Console::printLevel;
		$Console::printLevel = 0; // silence logging due to "return string: 0" spam
		exportObjectToScript(%obj, %filename, %clearFile, true, false);
		$Console::printLevel = %oldLevel;
		
		if (%clearFile) base::refreshSearchPath();
		%clearFile = false; // stop clearing after first object
	}
	
	return %objArr;
}
function REditor::loadObjects(%c, %filebase, %objArr) {
	%sb = REditor::sandboxGroup(%c);
	adel(%objArr);

	setInstantGroup(0);
	%start = Group::len(0); // all new objects will be added after this
	exec(%filebase @ "." @ %c @ ".cs");

	// Capture all new objects
	for (%i = %start; (%obj = Group::getObject(0, %i)) != -1; %i++)
		apush(%objArr, %obj);

	atoset(%objArr, %sb); // add all objects to %sb; also removes from set 0
	return %objArr;
}
