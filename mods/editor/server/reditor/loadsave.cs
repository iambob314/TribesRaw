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
	
	
	for (%obj = aitfirst(%objArr); !aitdone(%objArr); %obj = aitnext(%objArr)) {
		%oldLevel = $Console::printLevel;
		$Console::printLevel = 0; // silence logging due to "return string: 0" spam
		exportObjectToScript(%obj, %filename, !%append, %append, false);
		$Console::printLevel = %oldLevel; 
		
		if (!%append) base::refreshSearchPath();
		%append = true; // always append for 2nd iteration and on
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
		apush(%obj, %objArr);

	atoset(%sb, %objArr); // add all objects to %sb; also removes from set 0
	return %objArr;
}
