exec("editor\\remote\\gui\\gui.cs");

exec("editor\\remote\\remotes.cs");

exec("editor\\remote\\clickdrag.cs");
exec("editor\\remote\\controls.cs");

// Add some tags for reticle options in the observer-camera remote editor UI
IDBMP_RETICLE1 = 00160923, "CUR_WayPoint.bmp";
IDBMP_RETICLE2 = 00160924, "LR_H_Reticle.bmp";

// Defining hooks for main editor code

function Editor::onConnect() {
	Editor::defaultMEOptions();
	Editor::downloadRegistry();
}

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

	Editor::setControlMode(%m);
}
$Editor::validInputMode[Player] = true;
$Editor::validInputMode[Observer]  = true;
