exec("editor\\loopback\\actions.cs");
exec("editor\\loopback\\me.cs");

exec("editor\\loopback\\controls.cs");

exec("editor\\loopback\\gui\\gui.cs");

// Defining hooks for main editor code
$EditorUI::validMode[Camera] = true;
$EditorUI::validMode[Create] = true;
$EditorUI::validMode[Inspect] = true;
$EditorUI::validMode[Ted] = true;

$EditorUI::guiObject = EditorGui;
$EditorUI::guiPath = "gui\\editor.gui";
$EditorUI::allControls = "MEObjectList Inspector Creator TedBar SaveBar";

// Editor::focusInput focuses client/GUI input on one of these modes:
// * Player: player delegate
// * ME: mission editor
// * TED: terrain editor
function Editor::focusInput(%m) {
	assert($Editor::validInputMode[%m], "bad mode '" @ %m @ "'");

	setFocus(playDelegate,  %m == Player);
	setFocus(editCamera,    %m != Player);
	setFocus(MissionEditor, %m == ME);
	setFocus(TedObject,     %m == TED);
	if (%m == Player) {
		cursorOff(MainWindow);
	} else {
		cursorOn(MainWindow);
		postAction($EditorUI::guiObject, Attach, editCamera); // $EditorUI::guiObject defined in editor\gui.cs
	}
}
$Editor::validInputMode[Player] = true;
$Editor::validInputMode[ME]  = true;
$Editor::validInputMode[TED] = true;

function setFocus(%thing, %on) { if (%on) focus(%thing); else unfocus(%thing); }
