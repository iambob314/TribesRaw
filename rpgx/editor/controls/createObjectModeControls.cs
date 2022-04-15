newActionMap("createObjectModeMap.sae");

//
bindCommand(keyboard, make, control, delete, to, "ME::DeleteSelection();");
bindCommand(keyboard, make, control, c, to, "ME::CopySelection();");
bindCommand(keyboard, make, control, x, to, "ME::CutSelection();");
bindCommand(keyboard, make, control, v, to, "ME::PasteSelection();");
bindCommand(keyboard, make, control, d, to, "ME::DropSelection();");

bindCommand(keyboard, make, control, n, to, "ME::CreateGroup();");

//
bindCommand(keyboard, make, control, z, to, "ME::Undo();");
bindCommand(keyboard, make, control, y, to, "ME::Redo();");
//bindCommand(keyboard, make, control, s, to, "ME::Save();");

// toggle key for plane movement
bindCommand(keyboard, make, capslock, to, "METogglePlaneMovement();");



//
bindCommand(keyboard, make, f7, to, "MECreateObjectMode::toggleCreateObjectGui();");
bindCommand(keyboard, make, f8, to, "MECreateObjectMode::toggleInspectObjectGui();");