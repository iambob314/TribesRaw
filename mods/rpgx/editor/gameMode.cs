exec2("editor\\controls\\gameModeControls.cs");

function ME::enterGameMode() {
   if(%doRebuild != false)
      ME::RebuildCommandMap();

   //unfocus(TedObject);
   //unfocus(MissionEditor);
   //unfocus(EditCamera);
   loadPlayGui(); //GuiLoadContentCtrl(MainWindow, "play.gui");
   cursorOff(MainWindow);

   pushActionMap("gameModeMap.sae");
   //focus(playDelegate);
}

function ME::exitGameMode() {
  popActionMap("gameModeMap.sae");
}