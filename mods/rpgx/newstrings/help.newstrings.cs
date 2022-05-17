SimTag::defineData("GUI_HELP", "-- GUI HELP DATA group reserves 10,000 tags --", 10000);
  IDHELP_TEST           	= SimTag::nextTag(), "-1,This is a test of the auto-help system.  It uses the default width.";
  IDHELP_WIDTH_TEST     	= SimTag::nextTag(), "100,This is a different test using a specified width of 100.";
SimTag::endData("GUI_HELP");
