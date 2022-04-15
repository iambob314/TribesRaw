//============================================================================================
// DarkStar Region reserves tags 0 - 99,999
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
IDRGN_BEG_DARKSTAR         = 00000001, "- DarkStar REGION reserves tags 0 - 99,999 -";

//--------------------------------------------------------------------------------------------
// System Group reserves tags 2 - 19,999
//
// Used for SimTags specific to darkstar
//--------------------------------------------------------------------------------------------
IDRES_BEG_SYSTEM           = 00000002, "-- System RESOURCE group reserves tags 2 - 19,999 --";
IDSYS_SIBLING_DISABLE      = 00000003, "Sibling disable";
IDRES_END_SYSTEM           = 00019999, "-- System RESROUCE group reserves tags 2 - 19,999 --";

//--------------------------------------------------------------------------------------------
// Inspect Group reserves tags 20,000 - 39,999
//
// Used for desciption labels in the Inspect window
//--------------------------------------------------------------------------------------------
IDDAT_BEG_INSP             = 00020000, "-- Inspect DATA group reserves tags 20,000 - 39,999 --";
// these tags have migrated to editor.strings.ttag
IDDAT_END_INSP             = 00039999, "-- Inspect DATA group reserves tags 20,000 - 39,999 --";

IDRGN_END_DARKSTAR         = 00099999, "- DarkStar REGION reserves tags 0 - 99,999 -";

//============================================================================================
// SimGui Region reserves tags 100,000 - 199,999
//
// Defines ranges that must followed when setting user defined GUI tags.  The SimGui
// engine operates using these ranges.
//============================================================================================
IDRGN_BEG_SIMGUI           = 00100000, "- SimGui REGION reserves tags 100,000 - 199,999 -";

// Defined in gui.strings.cs

IDRGN_END_SIMGUI           = 00199999, "- SimGui REGION reserves tags 100,000 - 199,999 -";



//============================================================================================
// SimObjects Region reserves tags 1.15M - 1.2M
//
// Whatever additional definitions are required by SimObjects
//============================================================================================
IDRGN_BEG_SIMOBJ           = 01150000, "- SimObject REGION reserves tags 1.15M - 1.2M -";


IDSIMOBJ_BEG_USERWAV       = 01160000, "- User-Defined WavDefIds reserve 1.16M to 1.161M -";
IDSIMOBJ_END_USERWAV       = 01160999, "- User-Defined WavDefIds reserve 1.16M to 1.161M -";


IDRGN_END_SIMOBJ           = 01199999, "- SimObject REGION reserves tags 1.15M - 1.2M -";

