//
// Remote functions/wrappers for remote editor: download editor config, fetch state, etc.
//

//
// Remote registry/options stuff
//

// Editor::downloadRegistry downloads the editor object registry from the server.
// It is returned via remote calls to remoteEditor::addRegistryEntry, item by item
function Editor::downloadRegistry() {
	EditorRegistry::clear();
	remoteEval(2048, Editor::downloadRegistry);
}
function remoteEditor::addRegistryEntry(%serverId, %group, %name) {
	EditorRegistry::addDummy(%group, %name);
}
function remoteEditor::downloadComplete(%serverId) { echo("editor registry downloaded"); }

// Editor::setOptions update's server's ME vars for this remote editor
function Editor::setOptions(%m) {
	// TODO: $ME::Rotate{X,Y,Z}Axis not passed because I have no idea what they actually do...
	%snaps = Editor::getGridSnaps();
	%rsnap = Editor::getRotationSnap();
	%constrs = Editor::getConstraints();
	%dropMode = Editor::getDropMode();
	%plane = $ME::UsePlaneMovement;
	remoteEval(2048, Editor::setOptions, %snaps, %rsnap, %constrs, %dropMode, %plane);
}

// Editor::useTerrainGrid requests the server send latest terrain grid spacing,
// and when it comes back on remoteEditor::setTerrainGrid, updates ME vars/OptionsCtrl
function Editor::useTerrainGrid() { remoteEval(2048, Editor::getTerrainGrid); }
function remoteEditor::updateTerrainGrid(%serverId, %x, %y) {
	Control::setValue(XGridSnapCtrl, $ME::XGridSnap = %x);
	Control::setValue(YGridSnapCtrl, $ME::YGridSnap = %y);
}

//
// Remote editor control stuff
//

function Editor::setMode(%m)    { remoteEval(2048, Editor::setMode, %m); }

// Editor::castSelect does a remote editor raycast selection
function Editor::castSelect(%m) { remoteEval(2048, Editor::castSelect, %m); }
function Editor::castSelectMods() {
	if ($Editor::ModShift)     Editor::castSelect(add);
	else if ($Editor::ModCtrl) Editor::castSelect(rem);
	else                       Editor::castSelect();
}

function Editor::createObject(%group, %name) { remoteEval(2048, Editor::createObject, %group, %name); }

function Editor::deleteSelection() { remoteEval(2048, Editor::deleteSelection); }
function Editor::cutSelection()    { remoteEval(2048, Editor::cutSelection); }
function Editor::copySelection()   { remoteEval(2048, Editor::copySelection); }
function Editor::pasteSelection()  { remoteEval(2048, Editor::pasteSelection); }
function Editor::dupSelection()    { remoteEval(2048, Editor::dupSelection); }
function Editor::dropSelection()   { remoteEval(2048, Editor::dropSelection); }

function Editor::undo()   { remoteEval(2048, Editor::undo); }
function Editor::redo()   { remoteEval(2048, Editor::redo); }

function Editor::nudge(%x, %y, %z, %big) {
	%scale = $Editor::nudge;
	if (%big) %scale *= 8;

	%v = Vector::scale(%x @ " " @ %y @ " " @ %z, %scale);
	remoteEval(2048, Editor::nudge, %v, "");
}
function Editor::nudgeRot(%x, %y, %z, %big) {
	%scale = deg2rad($Editor::nudgeRot);
	if (%big) %scale *= 8;
	
	%v = Vector::scale(%x @ " " @ %y @ " " @ %z, %scale);
	remoteEval(2048, Editor::nudge, "", %v);
}

//
// Utilities
//
