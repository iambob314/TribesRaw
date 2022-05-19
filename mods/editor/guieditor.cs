// FearGUI::FGBitmapCtrl: displays a PBMP, stretched to fit
// SimGui::TextEdit
// SimGui::SimpleText: Alignable, setValue()able
// METextEdit
// MEButton
// FearGUI::FGPaletteCtrl: force palette (good one = IDPAL_SHELL, use inspector to set)
// SimGui::ScrollCtrl: scrollbox
// FearGUI::FGTextList: selection list

$MainWindow = MainWindow;

//
// Load/save GUI editor itself
//

function GuiEditor::loadEditor() {
  GuiLoadContentCtrl($MainWindow, "temp\\GuiEditor.gui");
  GuiEditMode($MainWindow);

  newObject(ToolTrayWindow, SimGui::Canvas, "GUI Editor Tools", 160, 480, True);
  GuiLoadContentCtrl(ToolTrayWindow, "temp\\guiEditorToolTray.gui");
  windowsKeyboardEnable(ToolTrayWindow);
  windowsMouseEnable(ToolTrayWindow);
  cursorOn(ToolTrayWindow);

  GuiEditor::updateEditModeButton();
  Control::setValue(GUIFileField, "");
}

function GuiEditor::saveEditor() {
  GuiSaveContentCtrl(ToolTrayWindow, "temp\\guiEditorToolTray.gui");
}

function GuiEditor::editEditor() {
  GuiEditMode(ToolTrayWindow);
  GuiInspect(ToolTrayWindow);
  GuiEditor::openTree();
  simTreeAddSet(guiEditorTree, "GuiEditorToolTrayGui");
}

//
// Load/save a new GUI project
//

function GuiEditor::newGUI(%guiName, %compType) {
  while ((%obj = Group::getObject("GuiEditorGui", 0)) != -1) deleteObject(%obj);

  %gui = newObject(%guiName @ "Gui", %compType);

  addToSet("GuiEditorGui", %gui);
}

function GuiEditor::loadGUI(%file) {
  while ((%obj = Group::getObject("GuiEditorGui", 0)) != -1) deleteObject(%obj);

  %guiName = File::getBase(%file);
  %gui = loadObject(%guiName @ "Gui", %file);

  addToSet("GuiEditorGui", %gui);
}

function GuiEditor::saveGUI(%file) {
  echo("Saving GUI to " @ %file);

  if (Group::objectCount("GuiEditorGui") == 0) return;

  $ConsoleWorld::DefaultSearchPath = $ConsoleWorld::DefaultSearchPath;

  // If we are overwriting, create a backup
  if (File::findFirst(File::getTitle(%file)) != "") {
    echo("FILE EXISTS");
    for (%i = 1; %i < 100; %i++) {
      %fileToTry = File::getPath(%file) @ File::getBase(%file) @ %i @ "." @ File::getExt(%file);
      if (File::findFirst(File::getTitle(%fileToTry)) == "") {
        File::copy(%file, %fileToTry);
        break;
      }
    }
  }

  %gui = Group::getObject("GuiEditorGui", 0);
  storeObject(%gui, %file, true);
}

//
// GUI editor functionality (wiring to editor buttons, etc. in next section)
//

function GuiEditor::openTree() {
  if (isObject(guiEditorTree)) return;

  simTreeCreate(guiEditorTree, ToolTrayWindow);
  simTreeAddSet(guiEditorTree, "GuiEditorGui");
}

function GuiEditor::openInspector() {
  GuiInspect($MainWindow);
}

function GuiEditor::toggleEditMode() {
  GuiEditMode($MainWindow);
  GuiEditor::updateEditModeButton();
}

function newControl(%parent, %name, %className, %pos, %extent) {
  %x = newObject(%name, %className);
  addToSet(%parent, %x);
  Control::setPosition(%name, getWord(%pos, 0), getWord(%pos, 1));
  Control::setExtent(%name, getWord(%extent, 0), getWord(%extent, 1));
  return %x;
}

//
// GUI controls in editor window
//

function GuiEditor::updateEditModeButton() {
  Control::setText(GUIEditModeButton, tern(isObject(EditControl), "Enter GUI mode", "Enter EDIT mode"));
}

function GUILoadButton::onAction() {
  %file = Control::getValue(GUIFileField);
  GuiEditor::loadGUI(%file);
}

function GUISaveButton::onAction() {
  %file = Control::getValue(GUIFileField);
  GuiEditor::saveGUI(%file);
}

function GUIEditModeButton::onAction() {
  GuiEditor::toggleEditMode();
}

function GUIInspectorButton::onAction() {
  GuiEditor::openInspector();
}

function GUITreeButton::onAction() {
  GuiEditor::openTree();
}
