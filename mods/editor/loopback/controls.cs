newActionMap("editor.sae");
bindAction(keyboard, make, a, TO, IDACTION_MOVELEFT, 1);
bindAction(keyboard, break, a, TO, IDACTION_MOVELEFT, 0);
bindAction(keyboard, make, d, TO, IDACTION_MOVERIGHT, 1);
bindAction(keyboard, break, d, TO, IDACTION_MOVERIGHT, 0);
bindAction(keyboard, make, s, TO, IDACTION_MOVEBACK, 1);
bindAction(keyboard, break, s, TO, IDACTION_MOVEBACK, 0);
bindAction(keyboard, make, w, TO, IDACTION_MOVEFORWARD, 1);
bindAction(keyboard, break, w, TO, IDACTION_MOVEFORWARD, 0);
bindAction(keyboard, make, e, TO, IDACTION_MOVEUP, 1);
bindAction(keyboard, break, e, TO, IDACTION_MOVEUP, 0);
bindAction(keyboard, make, c, TO, IDACTION_MOVEDOWN, 1);
bindAction(keyboard, break, c, TO, IDACTION_MOVEDOWN, 0);

bindCommand(keyboard, make, f1, to, "EditorUI::showMode(Camera);");
bindCommand(keyboard, make, f2, to, "EditorUI::showMode(Inspect);");
bindCommand(keyboard, make, f3, to, "EditorUI::showMode(Create);");
bindCommand(keyboard, make, f4, to, "EditorUI::showMode(Ted);");
bindCommand(keyboard, make, f5, to, "EditorUI::hide();");
bindCommand(keyboard, make, f9, to, "EditorUI::toggleHelp();");
bindCommand(keyboard, make, "?", to, "EditorUI::toggleHelp();");
bindCommand(keyboard, make, o, to, "EditorUI::toggleOptions();");

//
bindCommand(keyboard, make, control, delete, to, "Editor::deleteSelection();");
bindCommand(keyboard, make, control, c, to, "Editor::copySelection();");
bindCommand(keyboard, make, control, x, to, "Editor::cutSelection();");
bindCommand(keyboard, make, control, v, to, "Editor::pasteSelection();");
bindCommand(keyboard, make, control, d, to, "Editor::dupSelection();");
bindCommand(keyboard, make, control, g, to, "Editor::dropSelection();");

bindCommand(keyboard, make, control, n, to, "Editor::createGroup();");

//
bindCommand(keyboard, make, control, z, to, "Editor::undo();");
bindCommand(keyboard, make, control, s, to, "Editor::save();");

// 
bindAction(keyboard, make, control, TO, IDACTION_ME_MOD1, 1);
bindAction(keyboard, break, control, TO, IDACTION_ME_MOD1, 0);
bindAction(keyboard, make, shift, TO, IDACTION_ME_MOD2, 1);
bindAction(keyboard, break, shift, TO, IDACTION_ME_MOD2, 0);
bindAction(keyboard, make, alt, TO, IDACTION_ME_MOD3, 1);
bindAction(keyboard, break, alt, TO, IDACTION_ME_MOD3, 0);

//
bindAction(mouse, xaxis, TO, IDACTION_YAW, scale, 0.002, flip);
bindAction(mouse, yaxis, TO, IDACTION_PITCH, scale, 0.002, flip);

// bookmark binds
for(%i = 0; %i < 10; %i++) {
	bindCommand(keyboard, make, control, %i, to, "Editor::placeBookmark(" @ %i @ ");");
	bindCommand(keyboard, make, alt, %i, to, "Editor::gotoBookmark(" @ %i @ ");");
}

// movement binds
bindCommand(keyboard, make, 0, to, "Editor::setMoveSpeed(512);");
for (%i = 1; %i < 10; %i++)
	bindCommand(keyboard, make, %i, to, "Editor::setMoveSpeed(" @ (1 << (%i - 1)) @ ");");

function Editor::setMoveSpeed(%speed, %rot) {
	$ME::CameraMoveSpeed = def(%speed, 2);
	$ME::CameraRotateSpeed = def(%rot, 0.2);
}

// toggle key for plane movement
bindCommand(keyboard, make, capslock, to, "Editor::togglePlaneMovement();");