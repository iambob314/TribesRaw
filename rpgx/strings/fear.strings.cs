// Anomalous inspector tags
IDSTR_RADIO_SET				     = 00131901, "Radio Set:";
IDSTR_AI_ACTIVE			        = 00131411, "ACTIVE?";

////-------------------------------------- Actions
IDACTION_FIRE1				        = 00220001;
IDACTION_ROLL				        = 00220002;
IDACTION_PITCH				        = 00220003;
IDACTION_YAW				        = 00220004;
IDACTION_STOP				        = 00220005;
IDACTION_SETSPEED			        = 00220006;
IDACTION_MOVELEFT			        = 00220007;
IDACTION_MOVERIGHT		        = 00220008;
IDACTION_MOVEBACK			        = 00220009;
IDACTION_MOVEFORWARD		        = 00220010;
IDACTION_MOVEUP			        = 00220011;
IDACTION_MOVEDOWN			        = 00220012;
IDACTION_SHOTGUN			        = 00220013;
IDACTION_PLASMA			        = 00220014;
IDACTION_ROCKET			        = 00220015;
IDACTION_GRENADE			        = 00220016;
IDACTION_VIEW				        = 00220017;
IDACTION_FIRE2				        = 00220018;
IDACTION_BREAK1			        = 00220019;
IDACTION_CHAINGUN			        = 00220020;
IDACTION_SNIPER			        = 00220021;
IDACTION_TOGGLE			        = 00220100;
IDACTION_TURN_ON			        = 00220101;
IDACTION_TURN_OFF			        = 00220102;
IDACTION_EMPTY				        = 00220103;
IDACTION_RUN				        = 00220104;
IDACTION_STRAFE			        = 00220105;
IDACTION_USEITEM			        = 00220106;
IDACTION_JET				        = 00220107;
IDACTION_SEND_VALUE		        = 00220108;
IDACTION_WAVE				        = 00220109;
IDACTION_CROUCH			        = 00220110;
IDACTION_STAND				        = 00220111;
IDACTION_TOGGLE_CROUCH	        = 00220112;
IDACTION_SNIPER_FOV		        = 00220113;
IDACTION_INC_SNIPER_FOV	        = 00220114;
IDACTION_USE_PACK			        = 00220115;
IDACTION_MINE				        = 00220116;
IDACTION_CHAT					     = 00220200;
IDACTION_CHAT_DISP_SIZE		     = 00220201;
IDACTION_CHAT_DISP_PAGE		     = 00220202;
IDACTION_MENU_PAGE			     = 00220203;
IDACTION_ESCAPE_PRESSED		     = 00220204;
IDACTION_PLAY_MODE			     = 00220205;
IDACTION_COMMAND_MODE		     = 00220206;
IDACTION_ZOOM_IN                = 00220207;
IDACTION_ZOOM_OUT               = 00220208;
IDACTION_ISSUE_COMMAND          = 00220209;
IDACTION_ZOOM_MODE_ON           = 00220210;
IDACTION_ZOOM_MODE_OFF          = 00220211;
IDACTION_OBSERVER_NEXT          = 00220212;
IDACTION_OBSERVER_PREV          = 00220213;
IDACTION_OBSERVER_TOGGLE        = 00220214;
IDACTION_CMD_ACKNOWLEGED	     = 00220215;
IDACTION_CMD_UNABLE			     = 00220216;
IDACTION_CMD_DONE				     = 00220217;
IDACTION_CMD_CANCEL			     = 00220218;
IDACTION_CENTER_MODE_ON         = 00220219;
IDACTION_CENTER_MODE_OFF        = 00220220;
IDACTION_LOOKUP                 = 00220221;
IDACTION_LOOKDOWN               = 00220222;
IDACTION_TURNLEFT               = 00220223;
IDACTION_TURNRIGHT              = 00220224;
IDACTION_PITCHSPEED             = 00220225;
IDACTION_YAWSPEED               = 00220226;
IDACTION_ME_MOD1                = 00220227;
IDACTION_ME_MOD2                = 00220228;
IDACTION_ME_MOD3                = 00220229;
IDACTION_CENTERVIEW		= 00220230;

//--- IRC stuff
IDIRC_MENUOPT_KICK               = 00220340, "";
IDIRC_MENUOPT_BAN                = 00220341, "";
IDIRC_MENUOPT_PRIVATE_CHAT       = 00220342, "";
IDIRC_MENUOPT_PING_USER          = 00220343, "";
IDIRC_MENUOPT_WHOIS_USER         = 00220344, "";
IDIRC_MENUOPT_AWAY               = 00220345, "";
IDIRC_MENUOPT_IGNORE             = 00220346, "";
IDIRC_MENUOPT_OPER               = 00220347, "";
IDIRC_MENUOPT_SPKR               = 00220348, "";
IDIRC_MENUOPT_SPEC               = 00220349, "";
IDIRC_MENUOPT_LEAVE              = 00220350, "";
IDIRC_MENUOPT_CHANNEL_PROPERTIES = 00220351, "";
IDIRC_MENUOPT_INVITE             = 00220352, "";
IDIRC_BTN_JOIN                   = 00220353, "";
IDIRC_CTL_VIEW                   = 00220354, "";

function comment() {

// sky DMLs
//IDDML_SKY    	                 = 00190001, "sky.dml";
IDDML_LITESKY					     = 00190002, "litesky.dml";
//IDDML_ELUSHSKY					     = 00190003, "e_lushsky.dml";
//IDDML_DESERTTANSKY			     = 00190004, "tansky.dml";
IDDML_ICEGREYSKY				     = 00190005, "greysky.dml";
//IDDML_LUSHBLUESKY				     = 00190006, "bsky.dml";
//IDDML_DESERTBLUESKY			     = 00190007, "desbluesky.dml";
//IDDML_ICEBLUESKY				     = 00190008, "icebluesky.dml";
//IDDML_LUSHPURPLESKY			     = 00190009, "sunsetsky.dml";
//IDDML_LUSHDARKSKY				     = 00190010, "darkskycrash.dml";
IDDML_DESERTSUNSETSKY				= 00190011, "nitesky.dml";


//--------------------------------------Item related strings
//
//00210001 IDFILE_BULLET					  'bullet.dat'
//00210002 IDDTS_PR_BULLET				     'bullet.dts'
IDDTS_WP_SHOTGUN				     = 00210003, "arm";
IDDTS_WP_PLASMA				     = 00210004, "plasma";
IDDTS_AM_SHOTGUN				     = 00210005, "ammo1";
IDDTS_PL_LARMOR				     = 00210006, "larmor";
IDDTS_OB_TELEPORT				     = 00210007, "teleporter";
IDNAME_WP_SHOTGUN				     = 00210008, "Energy Glove";
IDNAME_WP_PLASMA				     = 00210009, "Plasma";
IDNAME_AM_SHOTGUN				     = 00210010, "Chaingun Shells";
IDNAME_AM_PLASMA				     = 00210011, "Plasma Rounds";
IDDTS_AM_PLASMA				     = 00210012, "plasammo";
IDDTS_PL_LFEMALE 				     = 00210013, "lfemale";
//00210014 IDDTS_PR_PLASMA				     'plasmabolt.dts'

IDDTS_PR_TURRET				     = 00210015, "fusionbolt.dts";
//00210016 IDFILE_ROCKET					  'rocket.dat'
IDNAME_WP_ROCKET				     = 00210017, "Rocket Launcher";
IDDTS_WP_ROCKET				     = 00210018, "disc";
IDNAME_AM_ROCKET				     = 00210019, "Explosive Disks";
IDDTS_AM_ROCKET				     = 00210020, "discammo";
IDDTS_OB_FIRSTAID				     = 00210022, "medpack";
IDDTS_OB_HEALING				     = 00210023, "medpack";
IDNAME_OB_FIRSTAID			     = 00210024, "First Aid";
IDNAME_OB_HEALING				     = 00210025, "Healing Kit";
IDDTS_OB_FLAG					     = 00210028, "flag";
IDNAME_OB_FLAG					     = 00210029, "Flag";
IDDTS_PL_HARMOR				     = 00210030, "harmor";
IDNAME_WP_CHAINGUN 			     = 00210031, "Chaingun";
IDDTS_WP_CHAINGUN				     = 00210032, "chaingun";
IDNAME_WP_GRENADE				     = 00210033, "Grenade Launcher";
IDNAME_AM_GRENADE				     = 00210034, "Grenades";
IDDTS_WP_GRENADE				     = 00210035, "grenadel";
IDDTS_AM_GRENADE				     = 00210036, "grenammo";
IDNAME_WP_SNIPER				     = 00210037, "Sniper Rifle";
IDDTS_WP_SNIPER				     = 00210038, "sniper";
IDNAME_AM_SNIPER				     = 00210039, "Sniper Bullets";
IDDTS_AM_SNIPER				     = 00210040, "snipammo";
IDNAME_PACK_MEDICAL			     = 00210042, "Medical Pack";
IDNAME_PACK_FUEL				     = 00210043, "Fuel Pack";
IDNAME_PACK_AMMO				     = 00210044, "Super Ammo Pack";
IDDTS_PACK_FUEL				     = 00210045, "jetpack";
IDDTS_PACK_SHIELD				     = 00210046, "shieldpack";
IDNAME_POWER_CRYSTAL			     = 00210047, "Power Crystal";
IDDTS_PACK_MEDICAL			     = 00210049, "armorpack";
IDNAME_PACK_SHIELD  			     = 00210052, "Shield Pack";
IDDTS_POWER_CRYSTALL			     = 00210053, "cryst_l";
IDNAME_WP_MINE					     = 00210054, "Anti-personel mine";
IDDTS_WP_MINE					     = 00210055, "mine";
IDNAME_WP_MINE2				     = 00210056, "Anti-vehicle mine";
IDDTS_WP_MINE2					     = 00210057, "mine2";
IDDTS_WP_MINEBOX				     = 00210058, "mineammo";
IDDTS_FLIER                     = 00210059, "flyer";
IDDTS_FM_PLATFORM               = 00210060, "platform";
IDDTS_FM_DOOR1L                 = 00210061, "door_left";
IDDTS_FM_DOOR1R                 = 00210062, "door_right";
IDDTS_FM_ELEVPAD2               = 00210063, "elevpad2";
IDDTS_FM_ELEVPAD3               = 00210064, "elevpad3";
IDDTS_FM_W64ELEVPAD             = 00210065, "w64elevpad";
IDDTS_FM_DOOR_TOP               = 00210066, "door_top";
IDDTS_FM_DOOR_BOT               = 00210067, "door_bot";
IDDTS_PACK_AMMO				     = 00210068, "ammopack";

//------------------------------------------------------------------------------
//-------------------------------------- Explosions
//
IDDTS_EXP_1					        = 00210100, "bluex.dts";
IDDTS_EXP_2					        = 00210101, "plasmaex.dts";
IDDTS_EXP_3					        = 00210102, "chainspk.dts";
IDDTS_EXP_4					        = 00210103, "fiery.dts";
IDDTS_EXP_5					        = 00210104, "shockwave.dts";
IDDTS_EXP_6					        = 00210105, "enex.dts";
IDDTS_EXP_7					        = 00210106, "tumult.dts";
IDDTS_EXP_8					        = 00210107, "fusionex.dts";

//-------------------------------------- General Explosions
IDFEAR_EXPLOSIONS_START		     = 00210200, "";
IDEXP_ROCKET					     = 00210201, "";
IDEXP_PLASMA					     = 00210202, "";
IDEXP_GRENADE					     = 00210205, "";
IDEXP_SHOCKWAVE				     = 00210206, "";
IDEXP_ENERGY					     = 00210207, "";
IDEXP_DEBRIS					     = 00210208, "";
IDEXP_TURRET					     = 00210209, "";

//-------------------------------------- Sniper randomized explosions
IDEXP_BULLETSNIPER_RICO_1		  = 00210210, "";
IDEXP_BULLETSNIPER_RICO_2		  = 00210211, "";
IDEXP_BULLETSNIPER_RICO_3		  = 00210212, "";

//-------------------------------------- BulletNormal randomized explosions
IDEXP_BULLET_RICO_1			     = 00210230, "";
IDEXP_BULLET_RICO_2			     = 00210231, "";
IDEXP_BULLET_RICO_3			     = 00210232, "";


//-------------------------------------- Shape Explosions
IDEXP_TSSHAPE_TEMP1			     = 00210300, "";
IDEXP_TSSHAPE_TEMP2			     = 00210301, "";
IDEXP_ITRSHAPE_TEMP1			     = 00210302, "";
IDEXP_ITRSHAPE_TEMP2			     = 00210303, "";

//-------------------------------------- shapes...
//IDDTS_GENERATOR			        = 00210500, "generator";
IDDTS_POWERSTATION  		        = 00210501, "enerpad";
IDDTS_AMMOSTATION	  		        = 00210502, "ammopad";
IDDTS_ITEMSTATION	  		        = 00210503, "mainpad";
//IDDTS_RADAR	  				        = 00210504, "radar";
IDDTS_TURRET	  			        = 00210505, "hellfiregun";
//IDDTS_AIR					        = 00210506, "air";
//IDDTS_ANTARRAY				        = 00210507, "anten_lava";
//IDDTS_ANTSMALL				        = 00210508, "anten_small";
//IDDTS_ANTMED				        = 00210509, "anten_med";
//IDDTS_ANTLARGE				        = 00210510, "anten_lrg";
//IDDTS_CDRCHAIR				        = 00210511, "chair";
//IDDTS_FBEACON				        = 00210512, "force";
IDDTS_MEDSUP				        = 00210513, "medical";
//IDDTS_HOVER					        = 00210514, "bridge";
IDDTS_BIGSAT				        = 00210515, "sat_big";
IDDTS_SATUP				           = 00210516, "sat_up";
IDDTS_SQPANEL				        = 00210517, "teleport_square";
IDDTS_VERTPANEL			        = 00210518, "teleport_vertical";
//IDDTS_MAGCARGO				        = 00210519, "magcargo";
//IDDTS_LIQCYL				        = 00210520, "liqcyl";
IDDTS_BLUEPANEL			        = 00210521, "panel_blue";
IDDTS_SETPANEL				        = 00210522, "panel_set";
IDDTS_VERTPANEL2			        = 00210523, "panel_vertical";
IDDTS_YELLOWPANEL			        = 00210524, "panel_yellow";
IDDTS_GAPC					        = 00210525, "gapc";
IDDTS_COMMANDTERM			        = 00210526, "command";
IDDTS_DISPLAYONE			        = 00210527, "display_one";
IDDTS_DISPLAYTWO			        = 00210528, "display_two";
IDDTS_DISPLAYTHREE			     = 00210529, "display_three";
IDDTS_ASSAULTAPC			        = 00210530, "assaultapc";
//IDDTS_ANTENROD				        = 00210531, "anten_rod";
IDDTS_CLOAKPACK			        = 00210532, "cloakpack";
IDDTS_RMTSENSOR			        = 00210533, "rmtsensor";
IDDTS_REPRPACK				        = 00210534, "reprpack";
IDDTS_SOLAR				           = 00210535, "solar";
IDDTS_PLATFORM1			        = 00210536, "platform1";
IDDTS_PLATFORM2			        = 00210537, "platform2";
IDDTS_PLATFORM3			        = 00210538, "platform3";
IDDTS_PLATFORM4			        = 00210539, "platform4";
IDDTS_DOOR1LEFT			        = 00210540, "newdoor1_l";
IDDTS_DOOR1RIGHT			        = 00210541, "newdoor1_r";
IDDTS_DOOR2LEFT			        = 00210542, "newdoor2_l";
IDDTS_DOOR2RIGHT			        = 00210543, "newdoor2_r";
IDDTS_DOOR3LEFT			        = 00210544, "newdoor3_l";
IDDTS_DOOR3RIGHT			        = 00210545, "newdoor3_r";
IDDTS_DOOR4LEFT			        = 00210546, "newdoor4_l";
IDDTS_DOOR5				           = 00210547, "newdoor5";
IDDTS_DOOR6LEFT			        = 00210548, "newdoor6_l";
IDDTS_DOOR6RIGHT			        = 00210549, "newdoor6_r";
IDDTS_CHAINTURRET			        = 00210550, "chainturret";
IDDTS_INDOORGUN			        = 00210551, "indoorgun";
IDDTS_DOOR4RIGHT			        = 00210552, "newdoor4_r";
IDDTS_MISLTURRET			        = 00210553, "missileturret";
IDDTS_PLATFORM5			        = 00210554, "platform5";
IDDTS_AMMOUNIT				        = 00210555, "ammounit";
IDDTS_CMDPNL				        = 00210556, "cmdpnl";
IDDTS_MINE_FLRG			        = 00210557, "mine_flrg";
IDDTS_TELEPORT				        = 00210558, "teleport";
IDDTS_DISPH1						= 00210559, "dsply_h1";
IDDTS_DISPH2						= 00210560, "dsply_h2";
IDDTS_DISPS1						= 00210561, "dsply_s1";
IDDTS_DISPS2						= 00210562, "dsply_s2";
IDDTS_DISPV1						= 00210563, "dsply_v1";
IDDTS_DISPV2						= 00210564, "dsply_v2";
IDDTS_ICECRYST						= 00210565, "crystal_ice";

}
