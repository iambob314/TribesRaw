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

// Editor::castSelect does a remote editor raycast selection
function Editor::castSelect() { remoteEval(2048, Editor::castSelect); }

function remoteEditor::onSelect(%serverId, %obj) {
	echos("HIT", %obj);
}