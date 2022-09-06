//
// Remote functions/wrappers for remote editor: download editor config, fetch state, etc.
//

//
// Remote registry stuff
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

//
// Remote editor control stuff
//

function Editor::setControlMode(%m) { remoteEval(2048, Editor::setControlMode, %m); }
function Editor::setDropMode(%m)    { remoteEval(2048, Editor::setDropMode, %m); }

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

