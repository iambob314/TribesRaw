function ME::sharedEnterEditMode() {
   //if (!isObject(EditorToolsGui))
   //  loadEditorToolWindow(); // For developmental purposes

   GuiLoadContentCtrl(MainWindow, "editor.gui");

   postAction(EditorGui, Attach, editCamera);
   focus(editCamera);
   cursorOn(MainWindow);

   //ME::GetConsoleOptions();
}

function ME::sharedExitEditMode() {
  unfocus(editCamera);
  cursorOff(MainWindow);
}



$MESaveWorldDisabled = "";
function ME::SaveWorld() {
  if ($MESaveWorldDisabled) return;
  $MESaveWorldDisabled = true;

  MEEditMode::saveWorld($editMission);
  Client::centerPrint("<jc>Saved world as <f2>" @ $editMission, 1);
  schedule("Client::centerPrint(\"\", 1); $MESaveWorldDisabled = \"\";", 2);
}

function MEEditMode::saveWorld(%worldName) {
  EvalSearchPath();

  %versionFileSearch = "missions\\" @ %worldName @ ".mis.*";

  %lastVersion = 0;
  for (%versionFile = File::findFirst(%versionFileSearch); %versionFile != ""; %versionFile = File::findNext(%versionFileSearch)) {
    %dotIndex = String::findSubStr(%versionFile, ".");
    %suffix = String::getSubStr(%versionFile, %dotIndex + 1, 1024);
    %suffixDotIndex = String::findSubStr(%suffix, ".");

    %version = String::getSubStr(%suffix, %suffixDotIndex + 1, 1024);

    if (%version > %lastVersion) %lastVersion = %version;
  }

  %newVersion = %lastVersion + 1;
  %worldFile = "base\\missions\\" @ %worldName @ ".mis";
  %worldVersionFile = %worldFile @ "." @ %newVersion;

  pushFocusServer();

  exportObjectToScript("MissionGroup", %worldFile);

  popFocus();

  File::copy(%worldFile, %worldVersionFile); 
}

function MEEditMode::cleanUpWorldVersions(%worldName) {
  %versionFileSearch = "missions\\" @ %worldName @ ".mis.*";

  for (%versionFile = File::findFirst(%versionFileSearch); %versionFile != ""; %versionFile = File::findNext(%versionFileSearch)) {
    File::delete(%versionFile);
  }
}
