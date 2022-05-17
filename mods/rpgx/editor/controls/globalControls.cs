newActionMap("globalMap.sae");

// Editor mode change binds
bindCommand(keyboard, make, f1, to, "ME::SwitchModes($ME::GAME_MODE);");//"ME::Hide();");
bindCommand(keyboard, make, f2, to, "ME::SwitchModes($ME::CREATE_OBJECT_MODE);");//"ME::ShowInspector();");
bindCommand(keyboard, make, f3, to, "ME::SwitchModes($ME::CREATE_OBJECT_MODE);");//"ME::ShowCreator();");
bindCommand(keyboard, make, f4, to, "ME::SwitchModes($ME::TED_MODE);");//"ME::ShowTed();");
bindCommand(keyboard, make, f5, to, "ME::SwitchModes($ME::GAME_MODE);");//"ME::enterGameMode();");

bindCommand(keyboard, make, control, s, to, "ME::SaveWorld();");