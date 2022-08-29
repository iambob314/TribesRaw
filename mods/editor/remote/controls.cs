NewActionMap("remoteEditor.sae");
// bindAction(keyboard, make, e, TO, IDACTION_MOVEUP, 1); // TODO: convert to remote calls with Observer::setFlyMode
// bindAction(keyboard, break, e, TO, IDACTION_MOVEUP, 0);
// bindAction(keyboard, make, c, TO, IDACTION_MOVEDOWN, 1);
// bindAction(keyboard, break, c, TO, IDACTION_MOVEDOWN, 0);

bindCommand(keyboard0, make, tab, TO, "cursorToggle(MainWindow);");

bindCommand(keyboard, make, f1, to, "EditorUI::showMode(Camera);");
bindCommand(keyboard, make, f2, to, "EditorUI::showMode(Inspect);");
bindCommand(keyboard, make, f3, to, "EditorUI::showMode(Create);");
bindCommand(keyboard, make, f4, to, "EditorUI::showMode(Ted);");
bindCommand(keyboard, make, f5, to, "EditorUI::hide();");
bindCommand(keyboard, make, f9, to, "EditorUI::toggleHelp();");
bindCommand(keyboard, make, "?", to, "EditorUI::toggleHelp();");
bindCommand(keyboard, make, o, to, "EditorUI::toggleOptions();");

bindCommand(mouse0, make, button0, TO, "Editor::castSelect();");
