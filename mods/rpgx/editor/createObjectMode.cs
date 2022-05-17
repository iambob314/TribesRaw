exec2("editor\\controls\\createObjectModeControls.cs");

function ME::enterCreateObjectMode() {
  ME::sharedEnterEditMode();

  focus(MissionEditor);
  MissionObjectList::refresh();

  pushActionMap("createObjectModeMap.sae");
}

function ME::exitCreateObjectMode() {
  ME::sharedExitEditMode();

  Control::setVisible(CreateObjectGui, false);
  Control::setVisible(InspectObjectGui, false);

  unfocus(MissionEditor);

  popActionMap("createObjectModeMap.sae");
}



function MECreateObjectMode::toggleCreateObjectGui() {
  Control::setVisible(CreateObjectGui, !Control::getVisible(CreateObjectGui));
}

function MECreateObjectMode::toggleInspectObjectGui() {
  Control::setVisible(InspectObjectGui, !Control::getVisible(InspectObjectGui));
}

