NewActionMap("remoteEditor.sae");
// bindAction(keyboard, make, e, TO, IDACTION_MOVEUP, 1); // TODO: convert to remote calls with Observer::setFlyMode
// bindAction(keyboard, break, e, TO, IDACTION_MOVEUP, 0);
// bindAction(keyboard, make, c, TO, IDACTION_MOVEDOWN, 1);
// bindAction(keyboard, break, c, TO, IDACTION_MOVEDOWN, 0);

bindCommand(keyboard0, make, "f5", TO, "EditorUI::hide();");
