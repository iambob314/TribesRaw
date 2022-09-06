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

function REditor::sel::len(%c) {
	return Group::len(REditor::sel(%c));
}

function REditor::sel::clear(%c) {
	%sel = REditor::sel(%c);
	while ((%obj = Group::getObject(%sel, 0)) != -1) removeFromSet(%sel, %obj);
}

function REditor::sel::add(%c, %objs) {
	%sel = REditor::sel(%c);
	for (%i = 0; (%obj = getWord(%objs, %i)) != -1; %i++)
		addToSet(%sel, %obj);
}

function REditor::sel::remove(%c, %objs) {
	%sel = REditor::sel(%c);
	for (%i = 0; (%obj = getWord(%objs, %i)) != -1; %i++)
		removeFromSet(%sel, %obj);
}

function REditor::sel::toggle(%c, %objs) {
	%sel = REditor::sel(%c);
	for (%i = 0; (%obj = getWord(%objs, %i)) != -1; %i++)
		if (Group::contains(%sel, %obj))
			removeFromSet(%sel, %obj);
		else
			addToSet(%sel, %obj);
}

function REditor::sel::set(%c, %objs) {
	REditor::sel::clear(%c);
	REditor::sel::add(%c, %objs);
}

//
// Edit actions
//

function REditor::deleteSelection(%c) { REditor::msgError(%c, "command not implemented yet"); }
function REditor::cutSelection(%c)    { REditor::msgError(%c, "command not implemented yet"); }
function REditor::copySelection(%c)   { REditor::msgError(%c, "command not implemented yet"); }
function REditor::pasteSelection(%c)  { REditor::msgError(%c, "command not implemented yet"); }
function REditor::dupSelection(%c)    { REditor::msgError(%c, "command not implemented yet"); }
function REditor::undo(%c)            { REditor::msgError(%c, "command not implemented yet"); }
function REditor::redo(%c)            { REditor::msgError(%c, "command not implemented yet"); }

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
}