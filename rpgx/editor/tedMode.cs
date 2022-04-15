exec2("editor\\controls\\tedModeControls.cs");

function ME::enterTedMode() {
  ME::sharedEnterEditMode();

  pushActionMap("tedModeMap.sae");
  focus(TedObject);
}

function ME::exitTedMode() {
  ME::sharedExitEditMode();

  popActionMap("tedModeMap.sae");
  unfocus(TedObject);
}

// ME::sharedEnterEditMode()  and ME::sharedExitEditMode() in createObjectMode.cs