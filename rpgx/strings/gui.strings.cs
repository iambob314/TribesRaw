//--------------------------------------------------------------------------------------------
// Gui Group reserves SimTags 100,001 - 109,999
//
// Used to list .gui's 
//--------------------------------------------------------------------------------------------
IDRES_BEG_GUI              = 00100001, "-- Gui RESOURCE group reserves tags 100,001-109,999 --";

IDGUI_MAIN_MENU                 = 00100100, "MainMenu.gui";

IDRES_END_GUI              = 00109999, "-- Gui RESOURCE group reserves tags 100,001-109,999 --";

//--------------------------------------------------------------------------------------------
// DLG Group reserves SimTags 110,000 - 119,999
//
// Used to list .dlgs (dialogs)
//--------------------------------------------------------------------------------------------

IDRES_BEG_DLG              = 00110000, "-- Dlg RESOURCE group reserves tags 110,000-119,999 --";

IDDLG_OK                   = 00110001, "Reserved dlg command";
IDDLG_CANCEL               = 00110002, "Reserved dlg command";

IDRES_END_DLG              = 00119999, "-- Dlg RESOURCE group reserves tags 110,000-119,999 --";

//--------------------------------------------------------------------------------------------
// CMD Group reserves SimTags 120,000 - 129,999
//
// Used to list console commands. (ie, IDCMD_)
//--------------------------------------------------------------------------------------------
IDRES_BEG_CMD              = 00120000, "-- CMD RESOURCE group reserves tags 120,000-129,999 --";

IDCMD_TEST                      = 00120001, "echo \"hello world\"";

IDRES_END_CMD              = 00129999, "-- CMD RESOURCE group reserves tags 120,000-129,999 --'";

//--------------------------------------------------------------------------------------------
// STR Group reserves SimTags 130,000 - 139,999
//
// All of the GUI strings
//--------------------------------------------------------------------------------------------

IDDAT_BEG_GUI_STR          = 00130000, "-- GUI STR DATA group reserves tags 130,000-139,999 --";

IDSTR_STRING_DEFAULT       = 00139998, "TestString";

IDDAT_END_GUI_STR          = 00139999, "-- GUI STR DATA group reserves tags 130,000-139,999 --";

//--------------------------------------------------------------------------------------------
// GUI MSC Group reserves SimTags 140,000 - 149,999
//
// Random GUI things
//--------------------------------------------------------------------------------------------

IDRES_BEG_GUI_MSC          = 00140000, "-- GUI Misc. group reserves tags 140,000-149,999 --";

// IDCTG's go here

IDRES_END_GUI_MSC          = 00149999, "-- GUI Misc. group reserves tags 140,000-149,999 --";

//--------------------------------------------------------------------------------------------
// GUI FNT Group reserves SimTags 150,000 - 159,999
//
// Lists all fonts.
//--------------------------------------------------------------------------------------------

IDRES_BEG_GUI_FNT          = 00150000, "-- GUI FONT RESOURCE group reserves tags 150,000-159,999 --";

IDFNT_EDITOR               = 00159996, "mefont.pft";
IDFNT_EDITOR_HILITE        = 00159997, "mefonthl.pft";
IDFNT_FONT_DEFAULT         = 00159998, "darkfont.pft";

// fonts 150-159
IDFNT_HILITE          	        = 00150001, "hilite.pft";
IDFNT_LOLITE              	     = 00150002, "lolite.pft";
IDFNT_CONSOLE					     = 00150003, "console.pft";
IDFNT_YELLOW                    = 00150004, "yellow.pft";
IDFNT_TINY	                    = 00150005, "if_small.pft";
IDFNT_W_8	                    = 00150006, "if_w_8.pft";
IDFNT_W_10	                    = 00150007, "if_w_10.pft";
IDFNT_W_12B	                    = 00150008, "if_w_12B.pft";
IDFNT_MR_18B	                 = 00150011, "if_mr_18b.pft";
IDFNT_DR_18B	                 = 00150012, "if_dr_18b.pft";

IDFNT_MR_14B	                 = 00150009, "if_y_14b.pft";
IDFNT_DR_14B	                 = 00150010, "if_y_14b.pft";
IDFNT_MR_12B	                 = 00150013, "if_y_12b.pft";
IDFNT_DR_12B	                 = 00150014, "if_y_12b.pft";
IDFNT_MR_10B	                 = 00150015, "if_y_10b.pft";
IDFNT_DR_10B	                 = 00150016, "if_y_10b.pft";
IDFNT_R_10B	            	     = 00150017, "if_w_10.pft";
IDFNT_R_12B	            	     = 00150018, "if_w_12b.pft";

IDFNT_MR_36B            	     = 00150019, "if_mr_36b.pft";
IDFNT_GR_8            		     = 00150020, "if_gr_8.pft";
IDFNT_R_8            		     = 00150021, "if_r_8.pft";
IDFNT_DR_8            		     = 00150022, "if_dr_8.pft";
IDFNT_GR_10            		     = 00150023, "if_gr_10.pft";
IDFNT_GR_12            		     = 00150024, "if_gr_12.pft";
IDFNT_GR_10B            	     = 00150025, "if_gr_10b.pft";
IDFNT_GR_12B            	     = 00150026, "if_gr_12b.pft";

IDFNT_Y_10B            		     = 00150027, "if_gr_10b.pft";
IDFNT_Y_12B            		     = 00150028, "if_gr_12b.pft";

IDFNT_8_STATIC            	     = 00150100, "if_gr_8b.pft";
IDFNT_10_STATIC                 = 00150101, "sf_grey200_10b.pft";
IDFNT_12_STATIC                 = 00150102, "if_gr_12b.pft";
IDFNT_14_STATIC                 = 00150103, "if_gr_14b.pft";
IDFNT_9_STATIC            	     = 00150104, "sf_grey200_9b.pft";
IDFNT_6_STATIC            	     = 00150105, "sf_grey200_6.pft";
IDFNT_7_STATIC            	     = 00150106, "sf_white_7.pft";

IDFNT_8_STANDARD                = 00150110, "sf_orange214_8.pft";
IDFNT_10_STANDARD               = 00150111, "sf_orange214_10.pft";
IDFNT_12_STANDARD               = 00150112, "if_y_12b.pft";
IDFNT_14_STANDARD               = 00150113, "if_y_14b.pft";
IDFNT_9_STANDARD                = 00150114, "sf_yellow200_9b.pft";
IDFNT_6_STANDARD                = 00150115, "sf_yellow200_6.pft";
IDFNT_7_STANDARD                = 00150116, "sf_orange214_7.pft";

IDFNT_8_HILITE            	     = 00150120, "sf_orange255_8.pft";
IDFNT_10_HILITE                 = 00150121, "sf_orange255_10.pft";
IDFNT_12_HILITE                 = 00150122, "if_y_12b.pft";
IDFNT_14_HILITE                 = 00150123, "if_y_14b.pft";
IDFNT_9_HILITE            	     = 00150124, "sf_white_9b.pft";
IDFNT_6_HILITE            	     = 00150125, "sf_white_6.pft";
IDFNT_7_HILITE            	     = 00150126, "sf_orange255_7.pft";

IDFNT_8_DISABLED                = 00150130, "if_gr_8b.pft";
IDFNT_10_DISABLED               = 00150131, "sf_grey100_10b.pft";
IDFNT_12_DISABLED               = 00150132, "if_gr_12b.pft";
IDFNT_14_DISABLED               = 00150133, "if_gr_14b.pft";
IDFNT_9_DISABLED                = 00150134, "sf_grey100_9b.pft";
IDFNT_6_DISABLED                = 00150135, "sf_grey100_6.pft";
IDFNT_7_DISABLED                = 00150136, "sf_grey100_7.pft";

IDFNT_8_SELECTED                = 00150160, "sf_white_9.pft";
IDFNT_10_SELECTED               = 00150161, "sf_white_10.pft";
IDFNT_12_SELECTED               = 00150162, "if_y_12b.pft";
IDFNT_14_SELECTED               = 00150163, "if_y_14b.pft";
IDFNT_9_SELECTED                = 00150164, "sf_white_9b.pft";
IDFNT_6_SELECTED                = 00150165, "sf_white_6.pft";
IDFNT_7_SELECTED                = 00150166, "sf_green_7.pft";

IDFNT_7_BLACK           	     = 00150170, "sf_black_7.pft";
IDFNT_8_BLACK           	     = 00150171, "sf_black_8.pft";
IDFNT_9_BLACK           	     = 00150172, "sf_black_9b.pft";
IDFNT_10_BLACK          	     = 00150173, "sf_black_10.pft";

IDFNT_HUD_6_STANDARD            = 00150140, "if_g_6.pft";
IDFNT_HUD_6_HILITE              = 00150141, "if_w_6.pft";
IDFNT_HUD_6_DISABLED            = 00150142, "if_dg_6.pft";
IDFNT_HUD_6_SPECIAL       	     = 00150143, "if_r_6.pft";
IDFNT_HUD_6_OTHER       	     = 00150144, "if_gr_6.pft";

IDFNT_HUD_10_STANDARD           = 00150150, "if_g_10b.pft";
IDFNT_HUD_10_HILITE             = 00150151, "if_w_10b.pft";
IDFNT_HUD_10_DISABLED           = 00150152, "if_dg_10b.pft";
IDFNT_HUD_10_SPECIAL            = 00150153, "if_r_10b.pft";
IDFNT_HUD_10_OTHER       	     = 00150154, "if_gr_10b.pft";

IDFNT_HUD_8_STANDARD            = 00150180, "if_g_8b.pft";
IDFNT_HUD_8_HILITE              = 00150181, "if_w_8b.pft";
IDFNT_HUD_8_DISABLED            = 00150182, "if_dg_8b.pft";
IDFNT_HUD_8_SPECIAL             = 00150183, "if_r_8b.pft";

IDRES_END_GUI_FNT          = 00159999, "-- GUI FONT RESOURCE group reserves tags 150,000-159,999 --";

//--------------------------------------------------------------------------------------------
// GUI BMP Group reserves SimTags 160,000 - 169,999
//
// Lists BMP files.
//--------------------------------------------------------------------------------------------

IDRES_BEG_GUI_BMP          = 00160000, "-- GUI BMP RESOURCE group reserves tags 160,000-169,999 --";

IDBMP_BG1                 	     = 00160001, "BrightLogo.bmp";
IDBMP_BG2                 	     = 00160002, "Connecting.bmp";
IDBMP_BG3                 	     = 00160003, "Loading.bmp";
IDBMP_BG4                 	     = 00160004, "Dedicated.bmp";
IDBMP_BG5                 	     = 00160005, "Background0.bmp";
IDBMP_BG6                 	     = 00160006, "Background1.bmp";
IDBMP_BG7                 	     = 00160007, "Background2.bmp";
IDBMP_BG_DIVIDE1          	     = 00160504, "horz_divide1.bmp";
IDBMP_BG_DIVIDE2          	     = 00160505, "horz_divide2.bmp";
IDBMP_BG_DIALOG          	     = 00160506, "TAB_JoinRight.bmp";
IDBMP_SKIN1          		     = 00160507, "TempSkin.bmp";
IDBMP_SKY_DESERT_N256		     = 00160508, "niteplanet_256.bmp";
IDBMP_SKY_DESERT_N64		        = 00160509, "niteplanet_64.bmp";
IDBMP_SKY_ICE_GREY1		        = 00160516, "greyplanet1.bmp";
IDBMP_SKY_ICE_GREY2		        = 00160517, "greyplanet2.bmp";
IDBMP_SKY_ICE_GREY256	        = 00160518, "greyplanet_256.bmp";
IDBMP_SKY_ICE_GREY64		        = 00160519, "greyplanet_64.bmp";
IDBMP_SKY_LUSH_DAYSUN		     = 00160522, "lushdaysun.bmp";
IDBMP_SKY_ICEHAZYSUN				= 00160528, "iceskysun.bmp";

IDBMP_E3_JOINUS				     = 00160530, "joinus3.bmp";
IDBMP_E3_REDFLAG				     = 00160531, "REDFLAG.bmp";
IDBMP_E3_BLUEFLAG				     = 00160532, "BLUEFLAG.bmp";

//cursors
IDBMP_CURSOR_DEFAULT            = 00160550, "CUR_Arrow.bmp, 0, 0";
IDBMP_CURSOR_HAND               = 00160551, "CUR_Hand.bmp, 8, 2";
IDBMP_CURSOR_OPENHAND           = 00160552, "CUR_OpenHand.bmp, 8, 2";
IDBMP_CURSOR_GRAB               = 00160553, "CUR_Grab.bmp, 8, 11";
IDBMP_CURSOR_IBEAM              = 00160554, "CUR_IBeam.bmp, 4, 10";
IDBMP_CURSOR_HADJUST            = 00160555, "CUR_HAdjust.bmp, 10, 8";
IDBMP_CURSOR_WAYPOINT           = 00160556, "CUR_WayPoint.bmp, 11, 13";
IDBMP_CURSOR_ZOOM      		     = 00160557, "CUR_Zoom.bmp, 7, 10";
IDBMP_CURSOR_WAYPOINT_ARROW     = 00160558, "CUR_WayPointArrow.bmp, 6, 8";
IDBMP_CURSOR_WAYPOINT_WAIT	     = 00160559, "CUR_WayPointWait.bmp, 5, 5";
IDBMP_CURSOR_CENTERING	     	  = 00160560, "CUR_Centering.bmp, 11, 13";
IDBMP_CURSOR_ROTATE             = 00160561, "cur_rotate.bmp, 11, 18";

IDBMP_BEG_SWITCH				     = 00160600, "";

IDBMP_BIGFEAR_ON				     = 00160601, "i_bigfear";
IDBMP_BIGFEAR_OFF				     = 00160602, "i_bigfear";
IDBMP_BACKPACK_ON				     = 00160603, "i_backpack";
IDBMP_BACKPACK_OFF			     = 00160604, "i_backpack";
IDBMP_WEAPON_ON				     = 00160605, "i_weapon";
IDBMP_WEAPON_OFF				     = 00160606, "i_weapon";
IDBMP_CLOCK_ON					     = 00160607, "i_clock";
IDBMP_CLOCK_OFF				     = 00160608, "i_clock";
IDBMP_COMPASS_ON				     = 00160609, "i_compass";
IDBMP_COMPASS_OFF				     = 00160610, "i_compass";
IDBMP_FEAR_ON					     = 00160611, "i_fear";
IDBMP_FEAR_OFF					     = 00160612, "i_fear";
IDBMP_RETICLE_ON				     = 00160613, "i_reticle";
IDBMP_RETICLE_OFF				     = 00160614, "i_reticle";
IDBMP_SCROLL_ON				     = 00160615, "i_scroll";
IDBMP_SCROLL_OFF				     = 00160616, "i_scroll";
IDBMP_SCROLLEND_ON			     = 00160617, "i_scrollend";
IDBMP_SCROLLEND_OFF			     = 00160618, "i_scrollend";

IDBMP_ZOOM_ON					     = 00160619, "MO_zoom";
IDBMP_ZOOM_OFF					     = 00160620, "MO_zoom";
IDBMP_CENTER_ON				     = 00160621, "MO_center";
IDBMP_CENTER_OFF				     = 00160622, "MO_center";
IDBMP_EXTRA_ON					     = 00160623, "MO_extra";
IDBMP_EXTRA_OFF				     = 00160624, "MO_extra";
IDBMP_ITEMS_ON					     = 00160625, "MO_items";
IDBMP_ITEMS_OFF				     = 00160626, "MO_items";
IDBMP_OBJECTS_ON				     = 00160627, "MO_obj";
IDBMP_OBJECTS_OFF				     = 00160628, "MO_obj";
IDBMP_PLAYERS_ON				     = 00160629, "MO_players";
IDBMP_PLAYERS_OFF				     = 00160644, "MO_players";
IDBMP_RADAR_ON					     = 00160645, "MO_radar";
IDBMP_RADAR_OFF				     = 00160646, "MO_radar";
IDBMP_TURRETS_ON				     = 00160647, "MO_turret";
IDBMP_TURRETS_OFF				     = 00160648, "MO_turret";

IDBMP_ATTACK				     	  = 00160630, "I_attack";
IDBMP_ATTACK_OFF				     = 00160631, "I_attack";
IDBMP_DEFEND_ON				     = 00160632, "I_defend";
IDBMP_DEFEND_OFF				     = 00160633, "I_defend";
IDBMP_ESCORT_ON				     = 00160634, "I_escort";
IDBMP_ESCORT_OFF				     = 00160635, "I_escort";
IDBMP_GO_TO_ON					     = 00160636, "I_go_to";
IDBMP_GO_TO_OFF				     = 00160637, "I_go_to";
IDBMP_GET_ON					     = 00160638, "I_get";
IDBMP_GET_OFF					     = 00160639, "I_get";
IDBMP_RECON_ON					     = 00160640, "I_recon";
IDBMP_RECON_OFF				     = 00160641, "I_recon";
IDBMP_REPAIR_ON				     = 00160642, "I_repair";
IDBMP_REPAIR_OFF				     = 00160643, "I_repair";
IDBMP_DEPLOY_ON				     = 00160649, "I_deploy";
IDBMP_DEPLOY_OFF				     = 00160650, "I_deploy";

IDBMP_REPORT_ON				     = 00160651, "I_report";
IDBMP_REPORT_OFF				     = 00160652, "I_report";
IDBMP_UNREPORT_ON				     = 00160653, "I_un_report";
IDBMP_UNREPORT_OFF			     = 00160654, "I_un_report";

IDBMP_CLOSE_ON					     = 00160655, "I_ok";
IDBMP_CLOSE_OFF				     = 00160656, "I_ok";

IDBMP_SHELL_RADIO_ON				  = 00160657, "RD_Check";
IDBMP_SHELL_RADIO_OFF			  = 00160658, "RD_Check";

IDBMP_MAP_ON					     = 00160660, "CMD_Map";
IDBMP_MAP_OFF					     = 00160661, "CMD_Map";
IDBMP_OBJECTIVES_ON			     = 00160662, "CMD_Objectives";
IDBMP_OBJECTIVES_OFF			     = 00160663, "CMD_Objectives";
IDBMP_HUD_MANAGER_ON			     = 00160664, "CMD_HudManager";
IDBMP_HUD_MANAGER_OFF		     = 00160665, "CMD_HudManager";
IDBMP_PLAYER_STATS_ON		     = 00160666, "CMD_PlayerStats";
IDBMP_PLAYER_STATS_OFF		     = 00160667, "CMD_PlayerStats";

IDBMP_CMDP_PLAYER_ON			     = 00160670, "CMDP_Players";
IDBMP_CMDP_PLAYER_OFF		     = 00160671, "CMDP_Players";
IDBMP_CMDP_TURRETS_ON		     = 00160672, "CMDP_Turrets";
IDBMP_CMDP_TURRETS_OFF		     = 00160673, "CMDP_Turrets";
IDBMP_CMDP_ITEMS_ON			     = 00160674, "CMDP_Items";
IDBMP_CMDP_ITEMS_OFF			     = 00160675, "CMDP_Items";
IDBMP_CMDP_ALL_ON				     = 00160676, "CMDP_All";
IDBMP_CMDP_ALL_OFF			     = 00160677, "CMDP_All";

IDBMP_LR_CMDP_PLAYER_ON		     = 00160680, "LR_CMDP_Players";
IDBMP_LR_CMDP_PLAYER_OFF	     = 00160681, "LR_CMDP_Players";
IDBMP_LR_CMDP_TURRETS_ON	     = 00160682, "LR_CMDP_Turrets";
IDBMP_LR_CMDP_TURRETS_OFF	     = 00160683, "LR_CMDP_Turrets";
IDBMP_LR_CMDP_ITEMS_ON		     = 00160684, "LR_CMDP_Items";
IDBMP_LR_CMDP_ITEMS_OFF		     = 00160685, "LR_CMDP_Items";
IDBMP_LR_CMDP_ALL_ON			     = 00160686, "LR_CMDP_All";
IDBMP_LR_CMDP_ALL_OFF		     = 00160687, "LR_CMDP_All";

IDBMP_LR_ATTACK_ON			     = 00160688, "LR_I_attack";
IDBMP_LR_ATTACK_OFF			     = 00160689, "LR_I_attack";
IDBMP_LR_DEFEND_ON			     = 00160690, "LR_I_defend";
IDBMP_LR_DEFEND_OFF			     = 00160691, "LR_I_defend";
IDBMP_LR_ESCORT_ON			     = 00160692, "LR_I_escort";
IDBMP_LR_ESCORT_OFF			     = 00160693, "LR_I_escort";
IDBMP_LR_GO_TO_ON				     = 00160694, "LR_I_go_to";
IDBMP_LR_GO_TO_OFF			     = 00160695, "LR_I_go_to";
IDBMP_LR_GET_ON				     = 00160696, "LR_I_get";
IDBMP_LR_GET_OFF				     = 00160697, "LR_I_get";
IDBMP_LR_RECON_ON				     = 00160698, "LR_I_recon";
IDBMP_LR_RECON_OFF			     = 00160699, "LR_I_recon";
IDBMP_LR_REPAIR_ON			     = 00160801, "LR_I_repair";
IDBMP_LR_REPAIR_OFF			     = 00160802, "LR_I_repair";
IDBMP_LR_DEPLOY_ON			     = 00160803, "LR_I_deploy";
IDBMP_LR_DEPLOY_OFF			     = 00160804, "LR_I_deploy";

IDBMP_LR_REPORT_ON			     = 00160805, "LR_I_report";
IDBMP_LR_REPORT_OFF			     = 00160806, "LR_I_report";
IDBMP_LR_UNREPORT_ON			     = 00160807, "LR_I_un_report";
IDBMP_LR_UNREPORT_OFF		     = 00160808, "LR_I_un_report";

IDBMP_LR_CLOSE_ON				     = 00160809, "LR_I_ok";
IDBMP_LR_CLOSE_OFF			     = 00160810, "LR_I_ok";

IDBMP_LR_CENTER_ON			     = 00160812, "LR_MO_center";
IDBMP_LR_CENTER_OFF			     = 00160813, "LR_MO_center";
IDBMP_LR_EXTRA_ON				     = 00160814, "LR_MO_extra";
IDBMP_LR_EXTRA_OFF			     = 00160815, "LR_MO_extra";
IDBMP_LR_ITEMS_ON				     = 00160816, "LR_MO_items";
IDBMP_LR_ITEMS_OFF			     = 00160817, "LR_MO_items";
IDBMP_LR_OBJECTS_ON			     = 00160818, "LR_MO_obj";
IDBMP_LR_OBJECTS_OFF			     = 00160819, "LR_MO_obj";
IDBMP_LR_PLAYERS_ON			     = 00160820, "LR_MO_players";
IDBMP_LR_PLAYERS_OFF			     = 00160821, "LR_MO_players";
IDBMP_LR_RADAR_ON				     = 00160822, "LR_MO_radar";
IDBMP_LR_RADAR_OFF			     = 00160823, "LR_MO_radar";
IDBMP_LR_TURRETS_ON			     = 00160824, "LR_MO_turret";
IDBMP_LR_TURRETS_OFF			     = 00160825, "LR_MO_turret";
IDBMP_LR_ZOOM_ON				     = 00160826, "LR_MO_zoom";
IDBMP_LR_ZOOM_OFF				     = 00160827, "LR_MO_zoom";

IDBMP_HUD_CHAIN_ON			     = 00160700, "i_chain_on.bmp";
IDBMP_HUD_DISC_ON				     = 00160701, "i_disk_on.bmp";
IDBMP_HUD_GRENADE_ON			     = 00160702, "i_grenade_on.bmp";
IDBMP_HUD_PLASMA_ON			     = 00160703, "i_plasma_on.bmp";
IDBMP_HUD_PULSE_ON			     = 00160704, "i_pulse_on.bmp";
IDBMP_HUD_SNIPER_ON			     = 00160705, "i_sniper_on.bmp";
IDBMP_HUD_CHAIN_OFF			     = 00160720, "i_chain_off.bmp";
IDBMP_HUD_DISC_OFF			     = 00160721, "i_disk_off.bmp";
IDBMP_HUD_GRENADE_OFF		     = 00160722, "i_grenade_off.bmp";
IDBMP_HUD_PLASMA_OFF			     = 00160723, "i_plasma_off.bmp";
IDBMP_HUD_PULSE_OFF			     = 00160724, "i_pulse_off.bmp";
IDBMP_HUD_SNIPER_OFF			     = 00160725, "i_sniper_off.bmp";

IDBMP_HUD_LR_CHAIN_ON			  = 00160750, "lr_chain_on.bmp";
IDBMP_HUD_LR_DISC_ON				  = 00160751, "lr_disk_on.bmp";
IDBMP_HUD_LR_GRENADE_ON			  = 00160752, "lr_grenade_on.bmp";
IDBMP_HUD_LR_PLASMA_ON			  = 00160753, "lr_plasma_on.bmp";
IDBMP_HUD_LR_PULSE_ON			  = 00160754, "lr_pulse_on.bmp";
IDBMP_HUD_LR_SNIPER_ON			  = 00160755, "lr_sniper_on.bmp";
IDBMP_HUD_LR_CHAIN_OFF			  = 00160760, "lr_chain_off.bmp";
IDBMP_HUD_LR_DISC_OFF			  = 00160761, "lr_disk_off.bmp";
IDBMP_HUD_LR_GRENADE_OFF		  = 00160762, "lr_grenade_off.bmp";
IDBMP_HUD_LR_PLASMA_OFF			  = 00160763, "lr_plasma_off.bmp";
IDBMP_HUD_LR_PULSE_OFF			  = 00160764, "lr_pulse_off.bmp";
IDBMP_HUD_LR_SNIPER_OFF			  = 00160765, "lr_sniper_off.bmp";

IDBMP_SHELL_BORDER_PLAY_ON	  	  = 00160900, "SBB_Play";
IDBMP_SHELL_BORDER_PLAY_OFF	  = 00160901, "SBB_Play";
IDBMP_SHELL_BORDER_CHAT_ON	  	  = 00160902, "SBB_Chat";
IDBMP_SHELL_BORDER_CHAT_OFF	  = 00160903, "SBB_Chat";
IDBMP_SHELL_BORDER_OPTIONS_ON	  = 00160904, "SBB_Options";
IDBMP_SHELL_BORDER_OPTIONS_OFF  = 00160905, "SBB_Options";
IDBMP_SHELL_BORDER_CANCEL_ON	  = 00160906, "SBB_Cancel";
IDBMP_SHELL_BORDER_CANCEL_OFF   = 00160907, "SBB_Cancel";

IDBMP_MENU_CIRC_NORMAL          = 00160920, "circ_n.bmp";
IDBMP_MENU_CIRC_SELECTED        = 00160921, "circ_s.bmp";
IDBMP_MENU_CIRC_DISABLED        = 00160922, "circ_d.bmp";

IDBMP_END_SWITCH        	     = 00160999, "";

IDBMP_BITMAP_DEFAULT       = 00169998, "darkbmp.bmp";

IDRES_END_GUI_BMP          = 00169999, "-- GUI BMP RESOURCE group reserves tags 160,000-169,999 --";

//--------------------------------------------------------------------------------------------
// GUI PBA Group reserves SimTags 170,000 - 179,999
//
// Lists animated bitmaps, or PBAs.
//--------------------------------------------------------------------------------------------

IDRES_BEG_GUI_PBA          = 00170000, "-- GUI PBA RESOURCE group reserves tags 170,000-179,999 --";

IDPBA_BOX_DEFAULT          = 00179997, "darkbox.pba";
IDPBA_SCROLL_DEFAULT       = 00179998, "darkscroll.pba";

IDPBA_BOX1                	     = 00170001, "gui\fearbox1.pba";
IDPBA_SCROLL2_SHELL        	  = 00170002, "ShellScrollCtrl.PBA";
IDPBA_BUTTON        			     = 00170003, "ButtonCtrl.PBA";
IDPBA_BOX_STANDARD	           = 00170004, "BoxCtrl.PBA";
IDPBA_SCROLL_HUD    			     = 00170005, "HudScrollCtrl.PBA";
IDPBA_SHELL_TAB    			     = 00170006, "ShellTabCtrl.PBA";
IDPBA_SHELL_COLUMNS 			     = 00170007, "ShellCHeaderCtrl.PBA";
IDPBA_POPUP 					     = 00170008, "ShellPopUpCtrl.PBA";
IDPBA_BOX_DIALOG	      	     = 00170009, "DlgBoxCtrl.PBA";
IDPBA_BOX_WINDOW	      	     = 00170010, "WinBoxCtrl.PBA";
IDPBA_TEXT_EDIT	      	     = 00170011, "ShellTextCtrl.PBA";
IDPBA_COMMAND_VIEW	           = 00170012, "CmdViewCtrl.PBA";
IDPBA_COMMAND_LIST	           = 00170013, "CmdListCtrl.PBA";
IDPBA_LR_COMMAND_VIEW           = 00170014, "LRCmdViewCtrl.PBA";
IDPBA_LR_COMMAND_LIST           = 00170015, "LRCmdListCtrl.PBA";
IDPBA_OPENING           		  = 00170016, "Opening.PBA";
IDPBA_WHERE_AM_I          		  = 00170017, "CmdWhereAmI.PBA";
IDPBA_SHELL_BORDER          	  = 00170018, "ShellBorderCtrl.PBA";
IDPBA_SCROLL_SHELL       	     = 00170019, "ShellGreenScrollCtrl.PBA";
IDPBA_SLIDER_SHELL       	     = 00170020, "ShellSliderCtrl.PBA";

IDRES_END_GUI_PBA          = 00179999, "-- GUI PBA RESOURCE group reserves tags 170,000-179,999 --";

//--------------------------------------------------------------------------------------------
// GUI PAL Group reserves SimTags 180,000 - 189,999
//
// Lists palette files.
//--------------------------------------------------------------------------------------------

IDRES_BEG_GUI_PAL          = 00180000, "-- GUI PAL RESOURCE group reserves tags 180,000-189,999 --";

IDPAL_PAL_DEFAULT          = 00189998, "darkpal.pal";
IDPAL_SHELL            		     = 00180001, "Shell.ppl";

IDRES_END_GUI_PAL          = 00189999, "-- GUI PAL RESOURCE group reserves tags 180,000-189,999 --";

