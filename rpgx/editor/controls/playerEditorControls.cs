newActionMap("playerEditorEditMap.sae");
bindCommand(keyboard, make, f6, to, "PlayerEditor::SwitchModes($PlayerEditor::PLAY_MODE);");

bindCommand(mouse0, make, button0, to, "remoteEval(2048, PE::SelectObject);");
bindCommand(mouse0, make, button1, to, "remoteEval(2048, PE::ToggleObjectDrag);");
//bindCommand(mouse0, break, button1, to, "remoteEval(2048, PE::EndObjectDrag);");

bindCommand(keyboard, make, up,          to, "PlayerEditor::nudge(\"0 1 0\");");
bindCommand(keyboard, make, down,        to, "PlayerEditor::nudge(\"0 -1 0\");");
bindCommand(keyboard, make, left,        to, "PlayerEditor::nudge(\"-1 0 0\");");
bindCommand(keyboard, make, right,       to, "PlayerEditor::nudge(\"1 0 0\");");
bindCommand(keyboard, make, shift, up,   to, "PlayerEditor::nudge(\"0 0 1\");");
bindCommand(keyboard, make, shift, down, to, "PlayerEditor::nudge(\"0 0 -1\");");

bindCommand(keyboard, make, control, up,    to, "PlayerEditor::nudgeRot(\"1 0 0\");");
bindCommand(keyboard, make, control, down,  to, "PlayerEditor::nudgeRot(\"-1 0 0\");");
bindCommand(keyboard, make, control, left,  to, "PlayerEditor::nudgeRot(\"0 0 1\");");
bindCommand(keyboard, make, control, right, to, "PlayerEditor::nudgeRot(\"0 0 -1\");");

bindCommand(keyboard, make, capslock, to, "$PlayerEditor::modAlt = !$PlayerEditor::modAlt;");
//bindCommand(keyboard, break, alt, to, "$PlayerEditor::modAlt = false;");

newActionMap("playerEditorPlayMap.sae");
bindCommand(keyboard, make, f6, to, "PlayerEditor::SwitchModes($PlayerEditor::EDIT_MODE);");