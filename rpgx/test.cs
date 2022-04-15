
function loadEditorToolWindow() {
  if (isObject("EditorTools")) return;
  newObject("EditorTools", SimGui::Canvas, "Editor Tools", "320 480", True);

  windowsKeyboardEnable(EditorTools);
  windowsMouseEnable(EditorTools);
  cursorOn(EditorTools);

  GuiLoadContentCtrl(EditorTools, "editortools.gui");
  //GuiEditMode(EditorTools);
  //GuiInspect(EditorTools);
}
