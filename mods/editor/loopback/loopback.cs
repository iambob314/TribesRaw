exec("editor\\loopback\\actions.cs");
exec("editor\\loopback\\me.cs");

exec("editor\\loopback\\editorcontrols.cs");

exec("editor\\loopback\\gui\\gui.cs");

// Defining hooks for main editor code
$EditorUI::validMode[Camera] = true;
$EditorUI::validMode[Create] = true;
$EditorUI::validMode[Inspect] = true;
$EditorUI::validMode[Ted] = true;

$EditorUI::guiObject = EditorGui;
$EditorUI::guiPath = "gui\\editor.gui";
$EditorUI::allControls = "MEObjectList Inspector Creator TedBar SaveBar";
