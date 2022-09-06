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

bindCommand(keyboard, make, control, delete, to, "Editor::deleteSelection();");
bindCommand(keyboard, make, control, c, to, "Editor::copySelection();");
bindCommand(keyboard, make, control, x, to, "Editor::cutSelection();");
bindCommand(keyboard, make, control, v, to, "Editor::pasteSelection();");
bindCommand(keyboard, make, control, d, to, "Editor::dupSelection();");
bindCommand(keyboard, make, control, g, to, "Editor::dropSelection();");

bindCommand(keyboard, make, control, z, to, "Editor::undo();");
bindCommand(keyboard, make, control, y, to, "Editor::redo();");

bindCommand(keyboard, make,          up,    to, "Editor::nudge(0, 1, 0);");
bindCommand(keyboard, make,          down,  to, "Editor::nudge(0, -1, 0);");
bindCommand(keyboard, make,          right, to, "Editor::nudge(1, 0, 0);");
bindCommand(keyboard, make,          left,  to, "Editor::nudge(-1, 0, 0);");
bindCommand(keyboard, make, control, up,    to, "Editor::nudge(0, 0, 1);");
bindCommand(keyboard, make, control, down,  to, "Editor::nudge(0, 0, -1);");

bindCommand(keyboard, make, shift,          up,    to, "Editor::nudge(0, 1, 0, true);");
bindCommand(keyboard, make, shift,          down,  to, "Editor::nudge(0, -1, 0, true);");
bindCommand(keyboard, make, shift,          right, to, "Editor::nudge(1, 0, 0, true);");
bindCommand(keyboard, make, shift,          left,  to, "Editor::nudge(-1, 0, 0, true);");
bindCommand(keyboard, make, shift, control, up,    to, "Editor::nudge(0, 0, 1, true);");
bindCommand(keyboard, make, shift, control, down,  to, "Editor::nudge(0, 0, -1, true);");

bindCommand(keyboard, make, alt, up,    to, "Editor::nudgeRot(1, 0, 0);");
bindCommand(keyboard, make, alt, down,  to, "Editor::nudgeRot(-1, 0, 0);");
bindCommand(keyboard, make, alt, right, to, "Editor::nudgeRot(0, 0, 1);");
bindCommand(keyboard, make, alt, left,  to, "Editor::nudgeRot(0, 0, -1);");

bindCommand(keyboard, make, shift, alt, up,    to, "Editor::nudgeRot(1, 0, 0, true);");
bindCommand(keyboard, make, shift, alt, down,  to, "Editor::nudgeRot(-1, 0, 0, true);");
bindCommand(keyboard, make, shift, alt, right, to, "Editor::nudgeRot(0, 0, 1, true);");
bindCommand(keyboard, make, shift, alt, left,  to, "Editor::nudgeRot(0, 0, -1, true);");

bindCommand(mouse, make, button0, TO, "Mouse::down();");
bindCommand(mouse, break, button0, TO, "Mouse::up();");

function Mouse::onClick() { Editor::castSelectMods(); }
function Mouse::onDrag(%start) {}