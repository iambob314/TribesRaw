
function EditorUI::Create::show() {
	if (EditorUI::loadGUI()) {
		Editor::focusInput(Observer);
	}
	EditorUI::showOnlyControls("MEObjectList Creator");
}

//
// GUI controls
//

function Editor::createObject(%group, %name) { // called from common editor\gui\create.cs
	echos("TIME TO CREATE", %group, %name);
}

// MissionObjectList in inspect.cs