// FearGUI::FGBitmapCtrl: displays a PBMP, stretched to fit
// SimGui::TextEdit
// SimGui::SimpleText: Alignable, setValue()able
// METextEdit
// MEButton

function newControl(%parent, %name, %className, %pos, %extent) {
  %x = newObject(%name, %className);
  addToSet(%parent, %x);
  Control::setPosition(%name, getWord(%pos, 0), getWord(%pos, 1));
  Control::setExtent(%name, getWord(%extent, 0), getWord(%extent, 1));
  return %x;
}

function makeit() {
GuiNewContentCtrl(MainWindow, FearGUI::FGBitmapCtrl);
GuiSaveContentCtrl(MainWindow, "temp\\screen.gui");
$ConsoleWorld::SearchPath = $ConsoleWorld::SearchPath;
GuiLoadContentCtrl(MainWindow, "screen.gui");

%contentPane = nameToID("ScreenGUI");

%buttonGroup = newControl(%contentPane, "Buttons", SimGui::Control, "0 340", "640 140");

%button1 = newControl(%buttonGroup, "PlayGameButton", FearGui::FGUniversalButton, "150 0", "160 60");
%button2 = newControl(%buttonGroup, "PlayerButton", FearGui::FGUniversalButton, "150 80", "160 60");
%button3 = newControl(%buttonGroup, "RecordingsButton", FearGui::FGUniversalButton, "330 0", "160 60");
%button4 = newControl(%buttonGroup, "WebsiteButton", FearGui::FGUniversalButton, "330 80", "160 60");

tree();

}

$MainWindow = MainWindow;
function GuiEditor::load() {
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

function GuiEditor::save() {
  GuiSaveContentCtrl(ToolTrayWindow, "temp\\guiEditorToolTray.gui");
}

function GuiEditor::newGUI(%guiName, %compType) {
  while (Group::objectCount("GuiEditorGui") > 0) deleteObject(Group::getObject("GuiEditorGui", 0));

  %gui = newObject(%guiName @ "Gui", %compType);

  addToSet("GuiEditorGui", %gui);
}

function GuiEditor::loadGUI(%file) {
  while (Group::objectCount("GuiEditorGui") > 0) deleteObject(Group::getObject("GuiEditorGui", 0));

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

// --- INIT ---
focusClient();