NewActionMap("remoteEditor.sae");
// bindAction(keyboard, make, e, TO, IDACTION_MOVEUP, 1); // TODO: convert to remote calls with Observer::setFlyMode
// bindAction(keyboard, break, e, TO, IDACTION_MOVEUP, 0);
// bindAction(keyboard, make, c, TO, IDACTION_MOVEDOWN, 1);
// bindAction(keyboard, break, c, TO, IDACTION_MOVEDOWN, 0);

bindCommand(keyboard, make, tab, TO, "cursorToggle(MainWindow);");

bindCommand(keyboard, make,  control, TO, "$Editor::ModCtrl = True;");
bindCommand(keyboard, break, control, TO, "$Editor::ModCtrl = False;");
bindCommand(keyboard, make,  shift,   TO, "$Editor::ModShift = True;");
bindCommand(keyboard, break, shift,   TO, "$Editor::ModShift = False;");
bindCommand(keyboard, make,  alt,     TO, "$Editor::ModAlt = True;");
bindCommand(keyboard, break, alt,     TO, "$Editor::ModAlt = False;");

bindCommand(keyboard, make, f1, to, "EditorUI::showMode(Camera);");
bindCommand(keyboard, make, f2, to, "EditorUI::showMode(Inspect);");
bindCommand(keyboard, make, f3, to, "EditorUI::showMode(Create);");
bindCommand(keyboard, make, f5, to, "EditorUI::hide();");
bindCommand(keyboard, make, f9, to, "EditorUI::toggleHelp();");
bindCommand(keyboard, make, "?", to, "EditorUI::toggleHelp();");
bindCommand(keyboard, make, o, to, "EditorUI::toggleOptions();");

bindCommand(mouse, make, button0, TO, "Editor::castSelectMods();");

