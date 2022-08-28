//
// Remote functions/wrappers for remote editor: download editor config, fetch state, etc.
//

function Editor::setInputMode(%m) { remoteEval(2048, Editor::setInputMode, %m); }



// Editor::downloadRegistries downloads all global editor "registries"; currently this is:
// * Shape list
// These are returned via remote calls to remoteEditor::register
function Editor::downloadRegistries() {
	adel()
}