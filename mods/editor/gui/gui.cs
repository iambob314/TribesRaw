// Top-level UI modes
exec("editor\\gui\\camera.cs");
exec("editor\\gui\\create.cs");
exec("editor\\gui\\inspect.cs");
exec("editor\\gui\\ted.cs");

exec("editor\\gui\\modal.cs");

// Editor modes: Camera Create Inspect Ted
$EditorUI::mode = Create; // current mode, or last used if editor UI not displayed
function EditorUI::getMode() {
	if (isObject(EditorGui)) return $EditorUI::mode;
	else return "";
}

// EditorUI::showMode displays the given editor mode. It delegates to EditorUI::<mode>::show()
// when the mode changes.
function EditorUI::showMode(%mode) {
	if (%mode == $EditorUI::mode) return;     // already shown
	if (%mode == "") %mode = $EditorUI::mode; // if mode not specified, show last used
	
	assert(%mode == Camera || %mode == Create || %mode == Inspect || %mode == Ted, "bad mode '" @ %mode @ "'");

	$EditorUI::mode = %mode;
	invoke("EditorUI::" @ $EditorUI::mode @ "::show");
}

function EditorUI::hide() {
	if (!isObject(EditorGui)) return false; // already hidden
	GuiLoadContentCtrl(MainWindow, "gui\\play.gui");
	Editor::focusInput(Player);
	return true;
}

//
// Helper functions for editor modes
//

$EditorUI::allControls = "MEObjectList Inspector Creator TedBar SaveBar";
function EditorUI::showOnlyControls(%ctrls) {
	for (%i = 0; (%c = getWord($EditorUI::allControls, %i)) != -1; %i++) Control::setVisible(%c, false);
	for (%i = 0; (%c = getWord(%ctrls, %i)) != -1; %i++)                 Control::setVisible(%c, true);
}

// EditorUI::loadGUI loads the editor GUI, returning true iff not already loaded.
// EditorUI::<mode>::show will generally call this, and Editor::focus as well as
// refresh lists, etc. if it returns true.
function EditorUI::loadGUI(%mode) {
	if (isObject(EditorGui)) return false; // already shown; nothing to do
	
	GuiLoadContentCtrl(MainWindow, "gui\\editor.gui");
	EditorUI::refreshControls(); // repopulate lists, etc.
	
	return true;
}

function EditorUI::refreshControls() {
	EditorUI::refreshCreatorLists(); // in gui\create.cs
	EditorUI::refreshMissionObjectList();
	EditorUI::refreshTed(); // in gui\ted.cs
}

function EditorUI::refreshMissionObjectList() {
	MissionObjectList::ClearDisplayGroups();
	MissionObjectList::AddDisplayGroup(1, "MissionGroup");
	MissionObjectList::AddDisplayGroup(1, "MissionCleanup");
	MissionObjectList::SetSelMode(1);
	
	if ($ME::InspectObject != "")
		MissionObjectList::Inspect($ME::InspectWorld, $ME::InspectObject);
}
