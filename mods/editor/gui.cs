exec("editor\\guimodal.cs");

//
// Defined in loopback.cs or remote.cs:
// * $EditorUI::validMode[%mode]
// * EditorUI::<mode>::show()      (for valid modes below)
// * EditorUI::refreshControls()   (called when GUI is (re)loaded and lists need repop)
//
// * Editor::focusInput(%mode)  (%mode = Player always valid; other modes loopback/remote-specific)
//
// * $EditorUI::guiObject       (e.g. EditorGui)
// * $EditorUI::guiPath         (e.g. "gui\\editor.gui")
// * $EditorUI::allControls     (space-separated UI control names)

// Editor modes: see $EditorUI::validMode in loopback.cs or remote.cs
$EditorUI::mode = Camera; // current mode, or last used if editor UI not displayed
function EditorUI::getMode() {
	if (isObject($EditorUI::guiObject)) return $EditorUI::mode;
	else return "";
}

// EditorUI::showMode displays the given editor mode. It delegates to EditorUI::<mode>::show()
// when the mode changes.
function EditorUI::showMode(%mode) {
	if (%mode == $EditorUI::mode) return;     // already shown
	if (%mode == "") %mode = $EditorUI::mode; // if mode not specified, show last used
	
	assert($EditorUI::validMode[%mode], "bad mode '" @ %mode @ "'");

	$EditorUI::mode = %mode;
	invoke("EditorUI::" @ $EditorUI::mode @ "::show");
}

function EditorUI::hide() {
	if (!isObject($EditorUI::guiObject)) return false; // already hidden
	GuiLoadContentCtrl(MainWindow, "gui\\play.gui");
	Editor::focusInput(Player);
	return true;
}

//
// Helper functions for editor modes
//

// EditorUI::showOnlyControls shows controls %ctrls and hides all others (as per $EditorUI::allControls)
function EditorUI::showOnlyControls(%ctrls) {
	for (%i = 0; (%c = getWord($EditorUI::allControls, %i)) != -1; %i++) Control::setVisible(%c, false);
	for (%i = 0; (%c = getWord(%ctrls, %i)) != -1; %i++)                 Control::setVisible(%c, true);
}

// EditorUI::loadGUI loads the editor GUI, returning true iff not already loaded.
// EditorUI::<mode>::show will generally call this, and Editor::focus as well as
// refresh lists, etc. if it returns true.
function EditorUI::loadGUI(%mode) {
	if (isObject($EditorUI::guiObject)) return false; // already shown; nothing to do
	
	GuiLoadContentCtrl(MainWindow, $EditorUI::guiPath);
	EditorUI::refreshControls(); // repopulate lists, etc.
	
	return true;
}
