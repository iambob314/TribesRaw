
//============================================================================================
// DarkStar Region
// 
// *IMPORTANT, PLEASE READ*
//     Do not define SimTag id zero.  Darkstar relies on the fact 
//     that SimTag id zero is never defined.
//
//     The SimTags are grouped into 3 sections: REGION, RESOURCE, and DATA.
//     A REGION holds one more more RESOURCE and DATA sections.
//     Tags in a DATA group will be translated during a foriegn language conversion.
//     Tags in the RESOURCE group will NOT.
//
//============================================================================================

SimTag::defineStrictRegion("DARKSTAR", "- DarkStar REGION reserves tags 0 through 100,000 -", 1, 100000);

  SimTag::defineResource("SYSTEM", "-- System RESOURCE group reserves 19,998 tags", 20000-2);
    IDSYS_SIBLING_DISABLE = IDRES_BEG_SYSTEM.nextTag(), "Sibling disable";
  SimTag::endResource("SYSTEM");

  SimTag::defineData("INSP", "-- Inspect DATA group reserves 20,000 tags --", 20000);
  SimTag::endData("INSP");

SimTag::endRegion("DARKSTAR");

//============================================================================================
// SimGui Region
//
// Defines ranges that must followed when setting user defined GUI tags.  The SimGui
// engine operates using these ranges.
//============================================================================================

SimTag::defineRegion("SIMGUI", "- SimGui REGION reserves 200,000 tags -", 200000);

  exec("strings\\gui.newstrings.cs");

  exec("strings\\help.newstrings.cs");

SimTag::endRegion("SIMGUI");

//============================================================================================
// Game Region
//
// Contains SimTags relating to game functions; for instance, IDACTION tags.
//============================================================================================

SimTag::defineRegion("SIMGAME", "- SimGame REGION reserves 100,000 tags -", 100000);
  exec("strings\\game.newstrings.cs");
SimTag::endRegion("SIMGAME");

//============================================================================================
// Editor Region
//
// Contains SimTags assigning strings to different properties displayed in the editor.
//============================================================================================

//SimTag::defineRegion("EDITOR", "- SimGame REGION reserves 100,000 tags -", 100000);
//  exec("strings\\editor.newstrings.cs");
//SimTag::endRegion("EDITOR");

//============================================================================================
// SimObjects Region
//
// Whatever additional definitions are required by SimObjects
//============================================================================================

SimTag::defineRegion("SIMOBJ", "- SimObject REGION reserves 50,000 tags -", 50000);

  IDSIMOBJ_BEG_USERWAV	= SimTag::nextTag(), "- User-Defined WavDefIds reserved range -";
  IDSIMOBJ_END_USERWAV	= SimTag::nextTag(), "- User-Defined WavDefIds reserved range -";

SimTag::endRegion("SIMOBJ");

SimTag::defineRegion("MISC", "- Misc. REGION reservese 10,000 tags -", 10000);

  IDSMK_DYNLOGO = SimTag::nextTag(), "DynLogoF.SMK";

  IDIRC_MENUOPT_KICK               = SimTag::nextTag(), "";
  IDIRC_MENUOPT_BAN                = SimTag::nextTag(), "";
  IDIRC_MENUOPT_PRIVATE_CHAT       = SimTag::nextTag(), "";
  IDIRC_MENUOPT_PING_USER          = SimTag::nextTag(), "";
  IDIRC_MENUOPT_WHOIS_USER         = SimTag::nextTag(), "";
  IDIRC_MENUOPT_AWAY               = SimTag::nextTag(), "";
  IDIRC_MENUOPT_IGNORE             = SimTag::nextTag(), "";
  IDIRC_MENUOPT_OPER               = SimTag::nextTag(), "";
  IDIRC_MENUOPT_SPKR               = SimTag::nextTag(), "";
  IDIRC_MENUOPT_SPEC               = SimTag::nextTag(), "";
  IDIRC_MENUOPT_LEAVE              = SimTag::nextTag(), "";
  IDIRC_MENUOPT_CHANNEL_PROPERTIES = SimTag::nextTag(), "";
  IDIRC_MENUOPT_INVITE             = SimTag::nextTag(), "";
  IDIRC_BTN_JOIN                   = SimTag::nextTag(), "";
  IDIRC_CTL_VIEW                   = SimTag::nextTag(), "";

SimTag::endRegion("MISC");

//exec("fear.newstrings.cs");
