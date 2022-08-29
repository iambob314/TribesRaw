
function EditorUI::Create::show() {
	if (EditorUI::loadGUI()) {
		Editor::focusInput(ME);
	}
	EditorUI::showOnlyControls("MEObjectList Creator SaveBar");
}

//
// GUI controls
//

function Editor::createObject(%group, %name) { // called from common editor\gui\create.cs
	%namesArr = EditorRegistry::groupNames(%group);
	%scriptsArr = EditorRegistry::groupScripts(%group);

	if ((%idx = afind(%name, %namesArr)) == -1)
		return; // clicked something not in array?? should never happen

	%script = aget(%idx, %scriptsArr);
	echos("TODO", %idx, %name, %script);
	%x = eval(%script);
	echo(%x);
}

// MissionObjectList in inspect.cs