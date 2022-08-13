
function EditorUI::Camera::show() {
	if (EditorUI::loadGUI())
		Editor::focusInput(ME);

	EditorUI::showOnlyControls(""); // hide all controls
}
