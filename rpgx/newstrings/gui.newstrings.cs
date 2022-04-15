SimTag::defineResource("GUI", "-- Gui RESOURCE group reserves 10,000 --", 10000-2);
IDGUI_MAIN_MENU		= SimTag::nextTag(), "gui\\MainMenu.gui";
IDGUI_LOAD_GAME		= SimTag::nextTag(), "gui\\LoadGame.gui";
IDGUI_MULTI_PLAYER	= SimTag::nextTag(), "gui\\MultiPlayer.gui";
IDGUI_JOIN_GAME		= SimTag::nextTag(), "gui\\JoinGame.gui";
IDGUI_ADDRESS		= SimTag::nextTag(), "gui\\Address.gui";
IDGUI_CREATE_SERVER	= SimTag::nextTag(), "gui\\CreateServer.gui";
IDGUI_MULTI_SETUP	= SimTag::nextTag(), "gui\\MultiSetup.gui";
IDGUI_OPTIONS		= SimTag::nextTag(), "gui\\Options.gui";
IDGUI_VIDEO		= SimTag::nextTag(), "gui\\Video.gui";
IDGUI_SOUND		= SimTag::nextTag(), "gui\\Sound.gui";
IDGUI_CONTROLS		= SimTag::nextTag(), "gui\\Controls.gui";
IDGUI_PERFORMANCE	= SimTag::nextTag(), "gui\\Performance.gui";
IDGUI_GAME_SHELL	= SimTag::nextTag(), "gui\\GameShell.gui";
IDGUI_RECORDINGS	= SimTag::nextTag(), "gui\\Recordings.gui";
IDGUI_FLYINGCAM		= SimTag::nextTag(), "gui\\flyingcam.gui";
IDGUI_PLAYER_SETUP	= SimTag::nextTag(), "gui\\PlayerSetup.gui";
IDGUI_CONNECT		= SimTag::nextTag(), "gui\\Connect.gui";
IDGUI_IRC_CHAT		= SimTag::nextTag(), "gui\\IRCChat.gui";
IDGUI_TEMP_OPTIONS	= SimTag::nextTag(), "gui\\TempOptions.gui";

IDGUI_TEMP		= SimTag::nextTag(), "gui\\Temp.gui";
SimTag::endResource("GUI");

SimTag::defineResource("DLG", "-- Dlg RESOURCE group reserves 10,000 tags --", 10000);
IDDLG_OK			= SimTag::nextTag(), "Reserved dlg command";
IDDLG_CANCEL			= SimTag::nextTag(), "Reserved dlg command";

IDDLG_ADD_ADDRESS		= SimTag::nextTag(), "gui\\NewAddr.gui";
IDDLG_EDIT_ADDRESS		= SimTag::nextTag(), "gui\\EditAddr.gui";
IDDLG_ADD_CONDITION		= SimTag::nextTag(), "gui\\NewCond.gui";
IDDLG_ADD_FILTER        	= SimTag::nextTag(), "gui\\NewFilter.gui";
IDDLG_EDIT_FILTER        	= SimTag::nextTag(), "gui\\EditFilter.gui";
IDDLG_ADD_PLAYER        	= SimTag::nextTag(), "gui\\NewPlayer.gui";
IDDLG_SERVER_INFO        	= SimTag::nextTag(), "gui\\ServerInfo.gui";
IDDLG_MASTER_MOTD        	= SimTag::nextTag(), "gui\\MasterMOTD.gui";
IDDLG_ADD_IRC_SERVER      	= SimTag::nextTag(), "gui\\NewIRCServer.gui";
IDDLG_IRC_AWAY			= SimTag::nextTag(), "gui\\IRCAway.gui";
IDDLG_IRC_CHANNEL_PROPERTIES	= SimTag::nextTag(), "gui\\IRCChannelProperties.dlg";
IDDLG_IRC_KICK			= SimTag::nextTag(), "gui\\IRCKick.dlg";
IDDLG_IRC_BAN			= SimTag::nextTag(), "gui\\IRCBan.dlg";
IDDLG_IRC_INVITE		= SimTag::nextTag(), "gui\\IRCInvite.dlg";
IDDLG_IRC_INVITED		= SimTag::nextTag(), "gui\\IRCInvited.dlg";
IDDLG_IRC_CHANNELS		= SimTag::nextTag(), "gui\\IRCChannels.dlg";
IDDLG_IRC_OPTIONS		= SimTag::nextTag(), "gui\\IRCOptions.dlg";
IDDLG_TEMP			= SimTag::nextTag(), "gui\\TempDlg.gui";
SimTag::endResource("DLG");

SimTag::defineResource("CMD", "-- CMD RESOURCE group reserves 10,000 tags --", 10000);
SimTag::endResource("CMD");

SimTag::defineData("GUI_STR", "-- GUI STR DATA group reserves 10,000 tags --", 10000);
IDSTR_STRING_DEFAULT= SimTag::nextTag(), "TestString";
SimTag::endData("GUI_STR");

SimTag::defineResource("GUI_MSC", "-- GUI Misc. group reserves 10,000 tags --", 10000);
SimTag::endResource("GUI_MSC");

SimTag::defineResource("GUI_FNT", "-- GUI FONT RESOURCE group reserves 10,000 tags --", 10000);
IDFNT_EDITOR		= SimTag::nextTag(), "mefont.pft";
IDFNT_EDITOR_HILITE	= SimTag::nextTag(), "mefonthl.pft";
IDFNT_FONT_DEFAULT	= SimTag::nextTag(), "darkfont.pft";

IDFNT_HILITE          	= SimTag::nextTag(), "hilite.pft";
IDFNT_LOLITE		= SimTag::nextTag(), "lolite.pft";
IDFNT_CONSOLE		= SimTag::nextTag(), "console.pft";
IDFNT_YELLOW		= SimTag::nextTag(), "yellow.pft";
IDFNT_TINY		= SimTag::nextTag(), "if_small.pft";
IDFNT_W_8		= SimTag::nextTag(), "if_w_8.pft";
IDFNT_W_10		= SimTag::nextTag(), "if_w_10.pft";
IDFNT_W_12B		= SimTag::nextTag(), "if_w_12B.pft";
IDFNT_MR_18B		= SimTag::nextTag(), "if_mr_18b.pft";
IDFNT_DR_18B		= SimTag::nextTag(), "if_dr_18b.pft";

IDFNT_MR_14B		= SimTag::nextTag(), "if_y_14b.pft";
IDFNT_DR_14B		= SimTag::nextTag(), "if_y_14b.pft";
IDFNT_MR_12B		= SimTag::nextTag(), "if_y_12b.pft";
IDFNT_DR_12B		= SimTag::nextTag(), "if_y_12b.pft";
IDFNT_MR_10B		= SimTag::nextTag(), "if_y_10b.pft";
IDFNT_DR_10B		= SimTag::nextTag(), "if_y_10b.pft";
IDFNT_R_10B	        = SimTag::nextTag(), "if_w_10.pft";
IDFNT_R_12B	        = SimTag::nextTag(), "if_w_12b.pft";

IDFNT_MR_36B            = SimTag::nextTag(), "if_mr_36b.pft";
IDFNT_GR_8            	= SimTag::nextTag(), "if_gr_8.pft";
IDFNT_R_8            	= SimTag::nextTag(), "if_r_8.pft";
IDFNT_DR_8            	= SimTag::nextTag(), "if_dr_8.pft";
IDFNT_GR_10            	= SimTag::nextTag(), "if_gr_10.pft";
IDFNT_GR_12            	= SimTag::nextTag(), "if_gr_12.pft";
IDFNT_GR_10B            = SimTag::nextTag(), "if_gr_10b.pft";
IDFNT_GR_12B            = SimTag::nextTag(), "if_gr_12b.pft";

IDFNT_Y_10B          	= SimTag::nextTag(), "if_gr_10b.pft";
IDFNT_Y_12B            	= SimTag::nextTag(), "if_gr_12b.pft";

IDFNT_8_STATIC         	= SimTag::nextTag(), "if_gr_8b.pft";
IDFNT_10_STATIC		= SimTag::nextTag(), "sf_grey200_10b.pft";
IDFNT_12_STATIC		= SimTag::nextTag(), "if_gr_12b.pft";
IDFNT_14_STATIC		= SimTag::nextTag(), "if_gr_14b.pft";
IDFNT_9_STATIC         	= SimTag::nextTag(), "sf_grey200_9b.pft";
IDFNT_6_STATIC         	= SimTag::nextTag(), "sf_grey200_6.pft";
IDFNT_7_STATIC         	= SimTag::nextTag(), "sf_white_7.pft";

IDFNT_8_STANDARD	= SimTag::nextTag(), "sf_orange214_8.pft";
IDFNT_10_STANDARD	= SimTag::nextTag(), "sf_orange214_10.pft";
IDFNT_12_STANDARD	= SimTag::nextTag(), "if_y_12b.pft";
IDFNT_14_STANDARD	= SimTag::nextTag(), "if_y_14b.pft";
IDFNT_9_STANDARD	= SimTag::nextTag(), "sf_yellow200_9b.pft";
IDFNT_6_STANDARD	= SimTag::nextTag(), "sf_yellow200_6.pft";
IDFNT_7_STANDARD	= SimTag::nextTag(), "sf_orange214_7.pft";

IDFNT_8_HILITE          = SimTag::nextTag(), "sf_orange255_8.pft";
IDFNT_10_HILITE		= SimTag::nextTag(), "sf_orange255_10.pft";
IDFNT_12_HILITE		= SimTag::nextTag(), "if_y_12b.pft";
IDFNT_14_HILITE		= SimTag::nextTag(), "if_y_14b.pft";
IDFNT_9_HILITE          = SimTag::nextTag(), "sf_white_9b.pft";
IDFNT_6_HILITE          = SimTag::nextTag(), "sf_white_6.pft";
IDFNT_7_HILITE          = SimTag::nextTag(), "sf_orange255_7.pft";

IDFNT_8_DISABLED	= SimTag::nextTag(), "if_gr_8b.pft";
IDFNT_10_DISABLED	= SimTag::nextTag(), "sf_grey100_10b.pft";
IDFNT_12_DISABLED	= SimTag::nextTag(), "if_gr_12b.pft";
IDFNT_14_DISABLED	= SimTag::nextTag(), "if_gr_14b.pft";
IDFNT_9_DISABLED	= SimTag::nextTag(), "sf_grey100_9b.pft";
IDFNT_6_DISABLED	= SimTag::nextTag(), "sf_grey100_6.pft";
IDFNT_7_DISABLED	= SimTag::nextTag(), "sf_grey100_7.pft";

IDFNT_8_SELECTED	= SimTag::nextTag(), "sf_white_9.pft";
IDFNT_10_SELECTED	= SimTag::nextTag(), "sf_white_10.pft";
IDFNT_12_SELECTED	= SimTag::nextTag(), "if_y_12b.pft";
IDFNT_14_SELECTED	= SimTag::nextTag(), "if_y_14b.pft";
IDFNT_9_SELECTED	= SimTag::nextTag(), "sf_white_9b.pft";
IDFNT_6_SELECTED	= SimTag::nextTag(), "sf_white_6.pft";
IDFNT_7_SELECTED	= SimTag::nextTag(), "sf_green_7.pft";

IDFNT_7_BLACK           = SimTag::nextTag(), "sf_black_7.pft";
IDFNT_8_BLACK           = SimTag::nextTag(), "sf_black_8.pft";
IDFNT_9_BLACK           = SimTag::nextTag(), "sf_black_9b.pft";
IDFNT_10_BLACK          = SimTag::nextTag(), "sf_black_10.pft";

IDFNT_HUD_6_STANDARD	= SimTag::nextTag(), "if_g_6.pft";
IDFNT_HUD_6_HILITE	= SimTag::nextTag(), "if_w_6.pft";
IDFNT_HUD_6_DISABLED	= SimTag::nextTag(), "if_dg_6.pft";
IDFNT_HUD_6_SPECIAL    	= SimTag::nextTag(), "if_r_6.pft";
IDFNT_HUD_6_OTHER     	= SimTag::nextTag(), "if_gr_6.pft";

IDFNT_HUD_10_STANDARD	= SimTag::nextTag(), "if_g_10b.pft";
IDFNT_HUD_10_HILITE	= SimTag::nextTag(), "if_w_10b.pft";
IDFNT_HUD_10_DISABLED	= SimTag::nextTag(), "if_dg_10b.pft";
IDFNT_HUD_10_SPECIAL	= SimTag::nextTag(), "if_r_10b.pft";
IDFNT_HUD_10_OTHER    	= SimTag::nextTag(), "if_gr_10b.pft";

IDFNT_HUD_8_STANDARD	= SimTag::nextTag(), "if_g_8b.pft";
IDFNT_HUD_8_HILITE	= SimTag::nextTag(), "if_w_8b.pft";
IDFNT_HUD_8_DISABLED	= SimTag::nextTag(), "if_dg_8b.pft";
IDFNT_HUD_8_SPECIAL	= SimTag::nextTag(), "if_r_8b.pft";
SimTag::endResource("GUI_FNT");

SimTag::defineResource("GUI_BMP", "-- GUI BMP RESOURCE group reserves 10,000 tags --", 10000);
IDBMP_BITMAP_DEFAULT= SimTag::nextTag(), "darkbmp.bmp";

// background bitmaps
IDBMP_BG1			= SimTag::nextTag(), "BrightLogo.bmp";
IDBMP_BG2			= SimTag::nextTag(), "Connecting.bmp";
IDBMP_BG3			= SimTag::nextTag(), "Loading.bmp";
IDBMP_BG4			= SimTag::nextTag(), "Dedicated.bmp";
IDBMP_BG5			= SimTag::nextTag(), "Background0.bmp";
IDBMP_BG6			= SimTag::nextTag(), "Background1.bmp";
IDBMP_BG7			= SimTag::nextTag(), "Background2.bmp";
//IDBMP_SKY_DESERT_1		= SimTag::nextTag(), "planet1.bmp";
//IDBMP_SKY_DESERT_2		= SimTag::nextTag(), "planet2.bmp";
//IDBMP_SKY_DESERT_3		= SimTag::nextTag(), "planet3.bmp";
//IDBMP_SKY_DESERT_4		= SimTag::nextTag(), "planet4.bmp";
IDBMP_BG_DIVIDE1		= SimTag::nextTag(), "horz_divide1.bmp";
IDBMP_BG_DIVIDE2		= SimTag::nextTag(), "horz_divide2.bmp";
IDBMP_BG_DIALOG			= SimTag::nextTag(), "TAB_JoinRight.bmp";
IDBMP_SKIN1			= SimTag::nextTag(), "TempSkin.bmp";
IDBMP_SKY_DESERT_N256		= SimTag::nextTag(), "niteplanet_256.bmp";
IDBMP_SKY_DESERT_N64		= SimTag::nextTag(), "niteplanet_64.bmp";
//IDBMP_SKY_DESERT_N1		= SimTag::nextTag(), "niteplanet1.bmp";
//IDBMP_SKY_DESERT_N2		= SimTag::nextTag(), "niteplanet2.bmp";
//IDBMP_SKY_DESERT_D1		= SimTag::nextTag(), "dayplanet1.bmp";
//IDBMP_SKY_DESERT_D2		= SimTag::nextTag(), "dayplanet2.bmp";
//IDBMP_SKY_DESERT_D64		= SimTag::nextTag(), "dayplanet_64.bmp";
//IDBMP_SKY_DESERT_D256		= SimTag::nextTag(), "dayplanet_256.bmp";
IDBMP_SKY_ICE_GREY1		= SimTag::nextTag(), "greyplanet1.bmp";
IDBMP_SKY_ICE_GREY2		= SimTag::nextTag(), "greyplanet2.bmp";
IDBMP_SKY_ICE_GREY256		= SimTag::nextTag(), "greyplanet_256.bmp";
IDBMP_SKY_ICE_GREY64		= SimTag::nextTag(), "greyplanet_64.bmp";
//IDBMP_SKY_DESERT_TANSUN	= SimTag::nextTag(), "deserttansun.bmp";
//IDBMP_SKY_LUSH_BLUESUN	= SimTag::nextTag(), "bsun.bmp";
IDBMP_SKY_LUSH_DAYSUN		= SimTag::nextTag(), "lushdaysun.bmp";
//IDBMP_SKY_ICE_DAY1		= SimTag::nextTag(), "iceniteplanet1.bmp";
//IDBMP_SKY_ICE_DAY2		= SimTag::nextTag(), "iceniteplanet2.bmp";
//IDBMP_SKY_ICE_DAY64		= SimTag::nextTag(), "iceniteplanet_64.bmp";
//IDBMP_SKY_ICE_DAY256		= SimTag::nextTag(), "iceniteplanet_256.bmp";
//IDBMP_SKY_LUSHCLOUD		= SimTag::nextTag(), "bcloud.bmp";
IDBMP_SKY_ICEHAZYSUN		= SimTag::nextTag(), "iceskysun.bmp";

//SimTag::nextTag() IDBMP_E3_JOINUS				     'Join_Us_Small_Alpha.bmp'
IDBMP_E3_JOINUS			= SimTag::nextTag(), "joinus3.bmp";
IDBMP_E3_REDFLAG		= SimTag::nextTag(), "REDFLAG.bmp";
IDBMP_E3_BLUEFLAG		= SimTag::nextTag(), "BLUEFLAG.bmp";


//cursors
IDBMP_CURSOR_DEFAULT		= SimTag::nextTag(), "CUR_Arrow.bmp, 0, 0";
IDBMP_CURSOR_HAND		= SimTag::nextTag(), "CUR_Hand.bmp, 8, 2";
IDBMP_CURSOR_OPENHAND		= SimTag::nextTag(), "CUR_OpenHand.bmp, 8, 2";
IDBMP_CURSOR_GRAB		= SimTag::nextTag(), "CUR_Grab.bmp, 8, 11";
IDBMP_CURSOR_IBEAM		= SimTag::nextTag(), "CUR_IBeam.bmp, 4, 10";
IDBMP_CURSOR_HADJUST		= SimTag::nextTag(), "CUR_HAdjust.bmp, 10, 8";
IDBMP_CURSOR_WAYPOINT		= SimTag::nextTag(), "CUR_WayPoint.bmp, 11, 13";
IDBMP_CURSOR_ZOOM		= SimTag::nextTag(), "CUR_Zoom.bmp, 7, 10";
IDBMP_CURSOR_WAYPOINT_ARROW	= SimTag::nextTag(), "CUR_WayPointArrow.bmp, 6, 8";
IDBMP_CURSOR_WAYPOINT_WAIT	= SimTag::nextTag(), "CUR_WayPointWait.bmp, 5, 5";
IDBMP_CURSOR_CENTERING		= SimTag::nextTag(), "CUR_Centering.bmp, 11, 13";
IDBMP_CURSOR_ROTATE		= SimTag::nextTag(), "cur_rotate.bmp, 11, 18";

IDBMP_BEG_SWITCH		= SimTag::nextTag(), "";

IDBMP_BIGFEAR_ON		= SimTag::nextTag(), "i_bigfear";
IDBMP_BIGFEAR_OFF		= SimTag::nextTag(), "i_bigfear";
IDBMP_BACKPACK_ON		= SimTag::nextTag(), "i_backpack";
IDBMP_BACKPACK_OFF		= SimTag::nextTag(), "i_backpack";
IDBMP_WEAPON_ON			= SimTag::nextTag(), "i_weapon";
IDBMP_WEAPON_OFF		= SimTag::nextTag(), "i_weapon";
IDBMP_CLOCK_ON			= SimTag::nextTag(), "i_clock";
IDBMP_CLOCK_OFF			= SimTag::nextTag(), "i_clock";
IDBMP_COMPASS_ON		= SimTag::nextTag(), "i_compass";
IDBMP_COMPASS_OFF		= SimTag::nextTag(), "i_compass";
IDBMP_FEAR_ON			= SimTag::nextTag(), "i_fear";
IDBMP_FEAR_OFF			= SimTag::nextTag(), "i_fear";
IDBMP_RETICLE_ON		= SimTag::nextTag(), "i_reticle";
IDBMP_RETICLE_OFF		= SimTag::nextTag(), "i_reticle";
IDBMP_SCROLL_ON			= SimTag::nextTag(), "i_scroll";
IDBMP_SCROLL_OFF		= SimTag::nextTag(), "i_scroll";
IDBMP_SCROLLEND_ON		= SimTag::nextTag(), "i_scrollend";
IDBMP_SCROLLEND_OFF		= SimTag::nextTag(), "i_scrollend";

IDBMP_ZOOM_ON			= SimTag::nextTag(), "MO_zoom";
IDBMP_ZOOM_OFF			= SimTag::nextTag(), "MO_zoom";
IDBMP_CENTER_ON			= SimTag::nextTag(), "MO_center";
IDBMP_CENTER_OFF		= SimTag::nextTag(), "MO_center";
IDBMP_EXTRA_ON			= SimTag::nextTag(), "MO_extra";
IDBMP_EXTRA_OFF			= SimTag::nextTag(), "MO_extra";
IDBMP_ITEMS_ON			= SimTag::nextTag(), "MO_items";
IDBMP_ITEMS_OFF			= SimTag::nextTag(), "MO_items";
IDBMP_OBJECTS_ON		= SimTag::nextTag(), "MO_obj";
IDBMP_OBJECTS_OFF		= SimTag::nextTag(), "MO_obj";
IDBMP_PLAYERS_ON		= SimTag::nextTag(), "MO_players";
IDBMP_PLAYERS_OFF		= SimTag::nextTag(), "MO_players";
IDBMP_RADAR_ON			= SimTag::nextTag(), "MO_radar";
IDBMP_RADAR_OFF			= SimTag::nextTag(), "MO_radar";
IDBMP_TURRETS_ON		= SimTag::nextTag(), "MO_turret";
IDBMP_TURRETS_OFF		= SimTag::nextTag(), "MO_turret";

IDBMP_ATTACK			= SimTag::nextTag(), "I_attack";
IDBMP_ATTACK_OFF		= SimTag::nextTag(), "I_attack";
IDBMP_DEFEND_ON			= SimTag::nextTag(), "I_defend";
IDBMP_DEFEND_OFF		= SimTag::nextTag(), "I_defend";
IDBMP_ESCORT_ON			= SimTag::nextTag(), "I_escort";
IDBMP_ESCORT_OFF		= SimTag::nextTag(), "I_escort";
IDBMP_GO_TO_ON			= SimTag::nextTag(), "I_go_to";
IDBMP_GO_TO_OFF			= SimTag::nextTag(), "I_go_to";
IDBMP_GET_ON			= SimTag::nextTag(), "I_get";
IDBMP_GET_OFF			= SimTag::nextTag(), "I_get";
IDBMP_RECON_ON			= SimTag::nextTag(), "I_recon";
IDBMP_RECON_OFF			= SimTag::nextTag(), "I_recon";
IDBMP_REPAIR_ON			= SimTag::nextTag(), "I_repair";
IDBMP_REPAIR_OFF		= SimTag::nextTag(), "I_repair";
IDBMP_DEPLOY_ON			= SimTag::nextTag(), "I_deploy";
IDBMP_DEPLOY_OFF		= SimTag::nextTag(), "I_deploy";

IDBMP_REPORT_ON			= SimTag::nextTag(), "I_report";
IDBMP_REPORT_OFF		= SimTag::nextTag(), "I_report";
IDBMP_UNREPORT_ON		= SimTag::nextTag(), "I_un_report";
IDBMP_UNREPORT_OFF		= SimTag::nextTag(), "I_un_report";

IDBMP_CLOSE_ON			= SimTag::nextTag(), "I_ok";
IDBMP_CLOSE_OFF			= SimTag::nextTag(), "I_ok";

IDBMP_SHELL_RADIO_ON		= SimTag::nextTag(), "RD_Check";
IDBMP_SHELL_RADIO_OFF		= SimTag::nextTag(), "RD_Check";

IDBMP_MAP_ON			= SimTag::nextTag(), "CMD_Map";
IDBMP_MAP_OFF			= SimTag::nextTag(), "CMD_Map";
IDBMP_OBJECTIVES_ON		= SimTag::nextTag(), "CMD_Objectives";
IDBMP_OBJECTIVES_OFF		= SimTag::nextTag(), "CMD_Objectives";
IDBMP_HUD_MANAGER_ON		= SimTag::nextTag(), "CMD_HudManager";
IDBMP_HUD_MANAGER_OFF		= SimTag::nextTag(), "CMD_HudManager";
IDBMP_PLAYER_STATS_ON		= SimTag::nextTag(), "CMD_PlayerStats";
IDBMP_PLAYER_STATS_OFF		= SimTag::nextTag(), "CMD_PlayerStats";

IDBMP_CMDP_PLAYER_ON		= SimTag::nextTag(), "CMDP_Players";
IDBMP_CMDP_PLAYER_OFF		= SimTag::nextTag(), "CMDP_Players";
IDBMP_CMDP_TURRETS_ON		= SimTag::nextTag(), "CMDP_Turrets";
IDBMP_CMDP_TURRETS_OFF		= SimTag::nextTag(), "CMDP_Turrets";
IDBMP_CMDP_ITEMS_ON		= SimTag::nextTag(), "CMDP_Items";
IDBMP_CMDP_ITEMS_OFF		= SimTag::nextTag(), "CMDP_Items";
IDBMP_CMDP_ALL_ON		= SimTag::nextTag(), "CMDP_All";
IDBMP_CMDP_ALL_OFF		= SimTag::nextTag(), "CMDP_All";

IDBMP_LR_CMDP_PLAYER_ON		= SimTag::nextTag(), "LR_CMDP_Players";
IDBMP_LR_CMDP_PLAYER_OFF	= SimTag::nextTag(), "LR_CMDP_Players";
IDBMP_LR_CMDP_TURRETS_ON	= SimTag::nextTag(), "LR_CMDP_Turrets";
IDBMP_LR_CMDP_TURRETS_OFF	= SimTag::nextTag(), "LR_CMDP_Turrets";
IDBMP_LR_CMDP_ITEMS_ON		= SimTag::nextTag(), "LR_CMDP_Items";
IDBMP_LR_CMDP_ITEMS_OFF		= SimTag::nextTag(), "LR_CMDP_Items";
IDBMP_LR_CMDP_ALL_ON		= SimTag::nextTag(), "LR_CMDP_All";
IDBMP_LR_CMDP_ALL_OFF		= SimTag::nextTag(), "LR_CMDP_All";

IDBMP_LR_ATTACK_ON		= SimTag::nextTag(), "LR_I_attack";
IDBMP_LR_ATTACK_OFF		= SimTag::nextTag(), "LR_I_attack";
IDBMP_LR_DEFEND_ON		= SimTag::nextTag(), "LR_I_defend";
IDBMP_LR_DEFEND_OFF		= SimTag::nextTag(), "LR_I_defend";
IDBMP_LR_ESCORT_ON		= SimTag::nextTag(), "LR_I_escort";
IDBMP_LR_ESCORT_OFF		= SimTag::nextTag(), "LR_I_escort";
IDBMP_LR_GO_TO_ON		= SimTag::nextTag(), "LR_I_go_to";
IDBMP_LR_GO_TO_OFF		= SimTag::nextTag(), "LR_I_go_to";
IDBMP_LR_GET_ON			= SimTag::nextTag(), "LR_I_get";
IDBMP_LR_GET_OFF		= SimTag::nextTag(), "LR_I_get";
IDBMP_LR_RECON_ON		= SimTag::nextTag(), "LR_I_recon";
IDBMP_LR_RECON_OFF		= SimTag::nextTag(), "LR_I_recon";
IDBMP_LR_REPAIR_ON		= SimTag::nextTag(), "LR_I_repair";
IDBMP_LR_REPAIR_OFF		= SimTag::nextTag(), "LR_I_repair";
IDBMP_LR_DEPLOY_ON		= SimTag::nextTag(), "LR_I_deploy";
IDBMP_LR_DEPLOY_OFF		= SimTag::nextTag(), "LR_I_deploy";

IDBMP_LR_REPORT_ON		= SimTag::nextTag(), "LR_I_report";
IDBMP_LR_REPORT_OFF		= SimTag::nextTag(), "LR_I_report";
IDBMP_LR_UNREPORT_ON		= SimTag::nextTag(), "LR_I_un_report";
IDBMP_LR_UNREPORT_OFF		= SimTag::nextTag(), "LR_I_un_report";

IDBMP_LR_CLOSE_ON		= SimTag::nextTag(), "LR_I_ok";
IDBMP_LR_CLOSE_OFF		= SimTag::nextTag(), "LR_I_ok";

IDBMP_LR_CENTER_ON		= SimTag::nextTag(), "LR_MO_center";
IDBMP_LR_CENTER_OFF		= SimTag::nextTag(), "LR_MO_center";
IDBMP_LR_EXTRA_ON		= SimTag::nextTag(), "LR_MO_extra";
IDBMP_LR_EXTRA_OFF		= SimTag::nextTag(), "LR_MO_extra";
IDBMP_LR_ITEMS_ON		= SimTag::nextTag(), "LR_MO_items";
IDBMP_LR_ITEMS_OFF		= SimTag::nextTag(), "LR_MO_items";
IDBMP_LR_OBJECTS_ON		= SimTag::nextTag(), "LR_MO_obj";
IDBMP_LR_OBJECTS_OFF		= SimTag::nextTag(), "LR_MO_obj";
IDBMP_LR_PLAYERS_ON		= SimTag::nextTag(), "LR_MO_players";
IDBMP_LR_PLAYERS_OFF		= SimTag::nextTag(), "LR_MO_players";
IDBMP_LR_RADAR_ON		= SimTag::nextTag(), "LR_MO_radar";
IDBMP_LR_RADAR_OFF		= SimTag::nextTag(), "LR_MO_radar";
IDBMP_LR_TURRETS_ON		= SimTag::nextTag(), "LR_MO_turret";
IDBMP_LR_TURRETS_OFF		= SimTag::nextTag(), "LR_MO_turret";
IDBMP_LR_ZOOM_ON		= SimTag::nextTag(), "LR_MO_zoom";
IDBMP_LR_ZOOM_OFF		= SimTag::nextTag(), "LR_MO_zoom";

IDBMP_HUD_CHAIN_ON		= SimTag::nextTag(), "i_chain_on.bmp";
IDBMP_HUD_DISC_ON		= SimTag::nextTag(), "i_disk_on.bmp";
IDBMP_HUD_GRENADE_ON		= SimTag::nextTag(), "i_grenade_on.bmp";
IDBMP_HUD_PLASMA_ON		= SimTag::nextTag(), "i_plasma_on.bmp";
IDBMP_HUD_PULSE_ON		= SimTag::nextTag(), "i_pulse_on.bmp";
IDBMP_HUD_SNIPER_ON		= SimTag::nextTag(), "i_sniper_on.bmp";
IDBMP_HUD_CHAIN_OFF		= SimTag::nextTag(), "i_chain_off.bmp";
IDBMP_HUD_DISC_OFF		= SimTag::nextTag(), "i_disk_off.bmp";
IDBMP_HUD_GRENADE_OFF		= SimTag::nextTag(), "i_grenade_off.bmp";
IDBMP_HUD_PLASMA_OFF		= SimTag::nextTag(), "i_plasma_off.bmp";
IDBMP_HUD_PULSE_OFF		= SimTag::nextTag(), "i_pulse_off.bmp";
IDBMP_HUD_SNIPER_OFF		= SimTag::nextTag(), "i_sniper_off.bmp";

IDBMP_HUD_LR_CHAIN_ON		= SimTag::nextTag(), "lr_chain_on.bmp";
IDBMP_HUD_LR_DISC_ON		= SimTag::nextTag(), "lr_disk_on.bmp";
IDBMP_HUD_LR_GRENADE_ON		= SimTag::nextTag(), "lr_grenade_on.bmp";
IDBMP_HUD_LR_PLASMA_ON		= SimTag::nextTag(), "lr_plasma_on.bmp";
IDBMP_HUD_LR_PULSE_ON		= SimTag::nextTag(), "lr_pulse_on.bmp";
IDBMP_HUD_LR_SNIPER_ON		= SimTag::nextTag(), "lr_sniper_on.bmp";
IDBMP_HUD_LR_CHAIN_OFF		= SimTag::nextTag(), "lr_chain_off.bmp";
IDBMP_HUD_LR_DISC_OFF		= SimTag::nextTag(), "lr_disk_off.bmp";
IDBMP_HUD_LR_GRENADE_OFF	= SimTag::nextTag(), "lr_grenade_off.bmp";
IDBMP_HUD_LR_PLASMA_OFF		= SimTag::nextTag(), "lr_plasma_off.bmp";
IDBMP_HUD_LR_PULSE_OFF		= SimTag::nextTag(), "lr_pulse_off.bmp";
IDBMP_HUD_LR_SNIPER_OFF		= SimTag::nextTag(), "lr_sniper_off.bmp";

IDBMP_SHELL_BORDER_PLAY_ON	= SimTag::nextTag(), "SBB_Play";
IDBMP_SHELL_BORDER_PLAY_OFF	= SimTag::nextTag(), "SBB_Play";
IDBMP_SHELL_BORDER_CHAT_ON	= SimTag::nextTag(), "SBB_Chat";
IDBMP_SHELL_BORDER_CHAT_OFF	= SimTag::nextTag(), "SBB_Chat";
IDBMP_SHELL_BORDER_OPTIONS_ON	= SimTag::nextTag(), "SBB_Options";
IDBMP_SHELL_BORDER_OPTIONS_OFF	= SimTag::nextTag(), "SBB_Options";
IDBMP_SHELL_BORDER_CANCEL_ON	= SimTag::nextTag(), "SBB_Cancel";
IDBMP_SHELL_BORDER_CANCEL_OFF	= SimTag::nextTag(), "SBB_Cancel";

IDBMP_MENU_CIRC_NORMAL		= SimTag::nextTag(), "circ_n.bmp";
IDBMP_MENU_CIRC_SELECTED	= SimTag::nextTag(), "circ_s.bmp";
IDBMP_MENU_CIRC_DISABLED	= SimTag::nextTag(), "circ_d.bmp";
SimTag::endResource("GUI_BMP");

SimTag::defineResource("GUI_PBA", "-- GUI PBA RESOURCE group reserves 10,000 tags --", 10000);
IDPBA_BOX_DEFAULT	= SimTag::nextTag();

IDPBA_BOX1		= SimTag::nextTag(), "gui\fearbox1.pba";
IDPBA_SCROLL2_SHELL	= SimTag::nextTag(), "ShellScrollCtrl.PBA";
IDPBA_BUTTON		= SimTag::nextTag(), "ButtonCtrl.PBA";
IDPBA_BOX_STANDARD	= SimTag::nextTag(), "BoxCtrl.PBA";
IDPBA_SCROLL_HUD	= SimTag::nextTag(), "HudScrollCtrl.PBA";
IDPBA_SHELL_TAB		= SimTag::nextTag(), "ShellTabCtrl.PBA";
IDPBA_SHELL_COLUMNS	= SimTag::nextTag(), "ShellCHeaderCtrl.PBA";
IDPBA_POPUP		= SimTag::nextTag(), "ShellPopUpCtrl.PBA";
IDPBA_BOX_DIALOG	= SimTag::nextTag(), "DlgBoxCtrl.PBA";
IDPBA_BOX_WINDOW	= SimTag::nextTag(), "WinBoxCtrl.PBA";
IDPBA_TEXT_EDIT		= SimTag::nextTag(), "ShellTextCtrl.PBA";
IDPBA_COMMAND_VIEW	= SimTag::nextTag(), "CmdViewCtrl.PBA";
IDPBA_COMMAND_LIST	= SimTag::nextTag(), "CmdListCtrl.PBA";
IDPBA_LR_COMMAND_VIEW	= SimTag::nextTag(), "LRCmdViewCtrl.PBA";
IDPBA_LR_COMMAND_LIST	= SimTag::nextTag(), "LRCmdListCtrl.PBA";
IDPBA_OPENING		= SimTag::nextTag(), "Opening.PBA";
IDPBA_WHERE_AM_I	= SimTag::nextTag(), "CmdWhereAmI.PBA";
IDPBA_SHELL_BORDER	= SimTag::nextTag(), "ShellBorderCtrl.PBA";
IDPBA_SCROLL_SHELL	= SimTag::nextTag(), "ShellGreenScrollCtrl.PBA";
IDPBA_SLIDER_SHELL	= SimTag::nextTag(), "ShellSliderCtrl.PBA";
SimTag::endResource("GUI_PBA");

SimTag::defineResource("GUI_PAL", "-- GUI PAL RESOURCE group reserves 10,000 tags --", 10000);
IDPAL_PAL_DEFAULT	= SimTag::nextTag(), "darkpal.pal";
IDPAL_SHELL		= SimTag::nextTag(), "Shell.ppl";
SimTag::endResource("GUI_PAL");
