//
// Functions for remote-editor actions
//

//
// Messaging
//

function REditor::msg(%c, %msg) {
	Client::sendMessage(%c, $MsgType::Green, %msg);
}
function REditor::msgError(%c, %msg) {
	Client::sendMessage(%c, $MsgType::Red, %msg);
}

//
// Modes
//

// REditor::getMode returns %c's editor mode, Observer or Player
function REditor::getMode(%c) {
	return tern(Client::getControlObject(%c) == Client::getOwnedObject(%c), Player, Observer);
}
function REditor::setMode(%c, %mode) {
	if (%mode == Player)
		%obj = Client::getOwnedObject(%c);
	else if (%mode == Observer)
		%obj = Client::getObserverCamera(%c);

	if (%obj != "") Client::setControlObject(%c, %obj);
}

$REditor::dropModes[Cam] = true;
$REditor::dropModes[CamWithRot] = true;
$REditor::dropModes[BelowCam] = true;
$REditor::dropModes[ScreenCenter] = true;
$REditor::dropModes[SelectedObject] = true;
function REditor::setDropMode(%c, %mode) {
	if ($REditor::dropModes[%mode])	%c.dropMode = %mode;
}

//
// Selection sets
//

function REditor::sel(%c) {
	if (%c.sel == "") addToSet(MissionCleanup, %c.sel = newObject(SelectionSet, SimSet));
	return %c.sel;
}

function REditor::sel::arr(%c, %arr) {
	return afromset(REditor::sel(%c), %arr);
}

function REditor::sel::len(%c) {
	return Group::len(REditor::sel(%c));
}

function REditor::sel::clear(%c) {
	%sel = REditor::sel(%c);
	while ((%obj = Group::getObject(%sel, 0)) != -1)
		removeFromSet(%sel, %obj);
}

function REditor::sel::add(%c, %objArr) {
	atoset(REditor::sel(%c), %objArr);
}

function REditor::sel::remove(%c, %objArr) {
	%sel = REditor::sel(%c);
	ado(removeFromSet, %sel, %objArr);
}

function REditor::sel::set(%c, %objArr) {
	REditor::sel::clear(%c);
	REditor::sel::add(%c, %objArr);
}

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

//
// Undo/redo stack and action management
//
// An "action" is a string "actionName [args]", with actionName a valid identifiers
// (args is the rest of the string).
//
// Each action "actionName" must define these functions (%c = client ID, %args = as above):
// * REditor::action::actionName::do(%c, %args)      : execute, cleanup, return inverse action
// * REditor::action::actionName::cleanup(%c, %args) : cleanup action (e.g. delete arrays in %args)
//
// It's also idiomatic to have:
// * REditor::action::actionName::make(%c, ...)      : return new action (args action-specific)
//

$REditor::undo = 0;
$REditor::redo = 1;
function REditor::astack(%c, %which) {
	%which = def(%which, $REditor::undo);
	if (%c.astack[%which] == "") %c.astack[%which] = anew();
	return %c.astack[%which];
}

function REditor::astack::doAndPush(%c, %action) {
	REditor::astack::clear(%c, $REditor::redo);
	
	%inv = REditor::action::invokeFunc(do, %c, %action);
	apush(%inv, REditor::astack(%c, $REditor::undo));
}

function REditor::astack::popAndDo(%c, %which) {
	if ((%action = apop(REditor::astack(%c, %which))) == "") return;

	%inv = REditor::action::invokeFunc(do, %c, %action);
	apush(%inv, REditor::astack(%c, 1 - %which));
}

function REditor::astack::clear(%c, %which) {
	%astack = REditor::astack(%c, %which);
	ado(REditor::action::invokeFunc, cleanup, %c, %astack);
	adel(%astack);
}

function REditor::astack::print(%c, %which) {
	%arr = REditor::astack(%c, %which);
	ado(echo, %arr);
}

// REditor::action::invokeFunc invokes delegate %funcBase (do, cleanup) for %action.
function REditor::action::invokeFunc(%funcBase, %c, %action) {
	%func = "REditor::action::" @ String::prefix(%action, " ") @ "::" @ %funcBase;
	return invoke(%func, %c, String::suffix(%action, " "));
}

//
// Edit actions
//

// DELETE
function REditor::action::delete::make(%c, %objArr) {
	atoset(%set = newObject("", SimSet), %objArr);
	return "delete " @ %set;
}
function REditor::action::delete::do(%c, %set) {
	// Make inverse first, because we're about to delete these objects
	%filebase = REditor::nextTmpFile(%c);
	REditor::saveObjects(%c, %filebase, false, afromset(%set));
	%inv = REditor::action::paste::make(%c, %filebase);

	while ((%obj = Group::getObject(%set, 0)) != -1) deleteObject(%obj);

	REditor::action::delete::cleanup(%c, %set);
	return %inv;
}
function REditor::action::delete::cleanup(%c, %set) { deleteObject(%set); }

// PASTE
function REditor::action::paste::make(%c, %filebase, %keepFile) {
	return "paste " @ %filebase @ " " @ def(%keepFile, false);
}
function REditor::action::paste::do(%c, %args) {
	%filebase = getWord(%args, 0);
	%objArr = REditor::loadObjects(%c, %filebase);
	
	REditor::sel::set(%c, %objArr); // select newly-pasted objects

	REditor::action::paste::cleanup(%c, %args);
	return REditor::action::delete::make(%c, %objArr);
}
function REditor::action::paste::cleanup(%c, %args) {
	%filebase = getWord(%args, 0);
	%keepFile = getWord(%args, 1);
	if (!%keepFile) REditor::deleteObjectFile(%c, %filebase);
}

function REditor::copyDelSel(%c, %copy, %del) {
	%objArr = afromset(REditor::sel(%c));

	if (%copy) REditor::saveObjects(%c, "clipbuffer", false, %objArr);
	if (%del)  REditor::astack::doAndPush(%c, REditor::action::delete::make(%c, %objArr));
}

function REditor::deleteSelection(%c) { REditor::copyDelSel(%c, false, true); }
function REditor::cutSelection(%c)    { REditor::copyDelSel(%c, true, true);  }
function REditor::copySelection(%c)   { REditor::copyDelSel(%c, true, false); }
function REditor::pasteSelection(%c)  {
	REditor::astack::doAndPush(%c, REditor::action::paste::make(%c, "clipbuffer", true));
}
function REditor::dupSelection(%c)    { REditor::copySelection(%c); REditor::pasteSelection(%c); }

function REditor::undo(%c)            { REditor::astack::popAndDo(%c, $REditor::undo); }
function REditor::redo(%c)            { REditor::astack::popAndDo(%c, $REditor::redo); }

function REditor::dropSelection(%c) {
	if ((%l = REditor::sel::len(%c)) != 1) {
		if (%l > 1) REditor::msgError(%c, "cannot drop multiple objects");
		return;
	}
	
	%obj = Group::getObject(REditor::sel(%c), 0);
	
	%dm = %c.dropMode;
	if (%dm == ScreenCenter) {
		if (REditor::LOS(%c, true)) {
			GameBase::setPosition(%obj, $los::position);
		} else {
			REditor::msgError(%c, "nothing hit");
		}
	} else {
		REditor::msg(%c, "drop mode " @ %dm @ " not implemented yet");
	}
}

//
// Misc.
//

function REditor::LOS(%c, %includeTerrain) {
	%mask = ~0;
	if (!%includeTerrain)
		%mask = ~$ObjectType::Terrain;
	
	$los::object = $los::position = $los::normal = "";
	%obj = Client::getControlObject(%c);

	if (REditor::getMode(%c) == Observer) {
		%pos = GameBase::getPosition(%obj);
		%dir = Vector::getFromRot(GameBase::getRotation(%obj), 1000);
		%pos2 = Vector::add(%pos, %dir);
		
		%hit = getLOSInfo(%pos, %pos2, %mask);
	} else {
		%hit = GameBase::getLOSInfo(%obj, 1000) &&
			(%includeTerrain || getObjectType($los::object) != SimTerrain);
	}
	
	return %hit;
}

// REditor::cleanup is called on any disconnecting client (even if not a remote editor)
// and should clean up remote editor stuff (if any)
function REditor::cleanup(%c) {
	if (%c.sel) deleteObject(%c.sel);
	adel(REditor::astack(%c, $REditor::undo));
	adel(REditor::astack(%c, $REditor::redo));
}