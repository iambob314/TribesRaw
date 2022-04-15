function ME::setMoveSpeed(%speed, %rot) {
  if (%speed == "") %speed = 2;
  if (%rot == "") %rot = 0.2;
  $ME::CameraMoveSpeed = %speed;
  $ME::CameraRotateSpeed = %rot;
}

newActionMap("editorCamera.sae");

// Movement binds
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

// Look binds
bindAction(mouse, xaxis, TO, IDACTION_YAW, scale, 0.002, flip);
bindAction(mouse, yaxis, TO, IDACTION_PITCH, scale, 0.002, flip);

// Bookmark binds
for (%i = 0; %i < 10; %i++) {
  bindCommand(keyboard, make, control, %i, to, "ME::PlaceBookmark(" @ %i @ ");");
  bindCommand(keyboard, make, alt, %i, to, "ME::GotoBookmark(" @ %i @ ");");
}

// Movement speed binds
bindCommand(keyboard, make, 0, to, "ME::setMoveSpeed(1024);");
for (%i = 1; %i < 10; %i++)
  bindCommand(keyboard, make, %i, to, "ME::setMoveSpeed(" @ (1 << %i) @ ");");

ME::setMoveSpeed(); // default move speed

// mod keys
bindAction(keyboard, make, control, TO, IDACTION_ME_MOD1, 1);
bindAction(keyboard, break, control, TO, IDACTION_ME_MOD1, 0);
bindAction(keyboard, make, shift, TO, IDACTION_ME_MOD2, 1);
bindAction(keyboard, break, shift, TO, IDACTION_ME_MOD2, 0);
bindAction(keyboard, make, alt, TO, IDACTION_ME_MOD3, 1);
bindAction(keyboard, break, alt, TO, IDACTION_ME_MOD3, 0);
