
function EditorUI::Camera::show() {
	if (EditorUI::loadGUI())
		Editor::focusInput(Observer);

	EditorUI::showOnlyControls(""); // hide all controls
}
