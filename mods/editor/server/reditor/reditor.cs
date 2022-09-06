//
// Functions for remote-editor actions
//

exec("editor\\server\\reditor\\sel.cs");
exec("editor\\server\\reditor\\loadsave.cs");
exec("editor\\server\\reditor\\astack.cs");
exec("editor\\server\\reditor\\actions.cs");

//
// Messaging
//

function REditor::msg(%c, %msg)    { Client::sendMessage(%c, $MsgType::Green, %msg); }
function REditor::msgErr(%c, %msg) { Client::sendMessage(%c, $MsgType::Red, %msg); }

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

function REditor::setOptions(%c, %snaps, %rsnap, %constrs, %dropMode, %plane) {
	if (%dropMode != Cam          && %dropMode != CamWithRot &&
	    %dropMode != BelowCam     && %dropMode != ScreenCenter &&
		%dropMode != SelectedObject) {
		REditor::msgErr(%c, "bad drop mode " @ %dropMode); return;
	}

	%c.gridSnaps = %snaps;
	%c.rotSnap = %rsnap;
	%c.constraints = %constrs;
	%c.dropMode = %dropMode;
	%c.planeMovement = %plane;
}

//
// Edit actions
//

function REditor::copyDelSel(%c, %copy, %del) {
	%objArr = REditor::sel::arr(%c, atmp());
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
	%objArr = REditor::sel::arr(%c, atmp());
	if (alen(%objArr) != 1) {
		if (%l > 1) REditor::msgErr(%c, "cannot drop multiple objects");
		return;
	}
	%obj = aget(0, %objArr);

	if ((%pos = REditor::getDropPos(%c)) != "") GameBase::setPosition(%obj, %pos);
	if ((%rot = REditor::getDropRot(%c)) != "") GameBase::setRotation(%obj, %rot);
}

function REditor::nudge(%c, %dpos, %drot) {
	%objArr = REditor::sel::arr(%c, atmp());
	REditor::astack::doAndPush(%c, REditor::action::move::make(%c, %objArr, %dpos, %drot));
}

function REditor::createObject(%c, %group, %name) {
	%arglist = EditorRegistry::getArglist(%group, %name);
	if (%arglist == "") { REditor::msgErr(%c, "bad object " @ %group @ "/" @ %name); return; }
	
	REditor::astack::doAndPush(%c, REditor::action::create::make(%c, %arglist));
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

function REditor::getDropPos(%c) {
	%dm = def(%c.dropMode, ScreenCenter);
	if (%dm == ScreenCenter) {
		if (REditor::LOS(%c, true)) return $los::position;
		else return "";
	} else {
		return "";
	}
}

function REditor::getDropRot(%c) {
	%dm = def(%c.dropMode, ScreenCenter);	
	if (%dm == ScreenCenter) {
		return "";
	} else {
		return "";
	}
}

// REditor::cleanup is called on any disconnecting client (even if not a remote editor)
// and should clean up remote editor stuff (if any)
function REditor::cleanup(%c) {
	if (%c.sel) deleteObject(%c.sel);
	adel(REditor::astack(%c, $REditor::undo));
	adel(REditor::astack(%c, $REditor::redo));
}