
function EditorUI::Create::show() {
	if (EditorUI::loadGUI()) {
		Editor::focusInput(ME);
	}
	EditorUI::showOnlyControls("MEObjectList Creator SaveBar");
}

//
// GUI controls
//

function Editor::onCreateObject(%group, %name) { // called from common editor\gui\create.cs
	%arglist = EditorRegistry::getArglist(%group, %name);
	echos("About to create", %group, %name, %arglist);
	if (%arglist == "") return;
	
	%x = eval("MissionCreateObject(" @ %arglist @ ");");
	echos("Created", %x);
}

// MissionObjectList in inspect.cs