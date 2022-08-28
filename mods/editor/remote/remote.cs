exec("editor\\remote\\controls.cs");

function Editor::onConnect() {
	Editor::downloadData();
}

// Defining hooks for main editor code
$EditorUI::validMode[Camera] = true;
$EditorUI::validMode[Create] = true;
$EditorUI::validMode[Inspect] = true;

$EditorUI::guiObject = RemoteEditorGui;
$EditorUI::guiPath = "remoteEditor.gui"; //"editor\\remote\\remoteEditor.gui"; // TODO: move temp\remoteEditor.gui to this permanent location
$EditorUI::allControls = "MEObjectList Inspector Creator";

// Editor::focusInput focuses client/GUI input on one of these modes:
// * Player: player
// * Observer: observer camera
function Editor::focusInput(%m) {
	assert($Editor::validInputMode[%m], "bad mode '" @ %m @ "'");

	if (%m == Player) popActionMap("remoteEditor.sae");
	else              pushActionMap("remoteEditor.sae");

	Editor::setInputMode(%m);
}
$Editor::validInputMode[Player] = true;
$Editor::validInputMode[Observer]  = true;
