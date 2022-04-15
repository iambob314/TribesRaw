//SimTag::defineData("MISC_ITG", "-- Misc. ITG tag DATA group reserves 10,000 tags --", 10000);

// Misc. ITG Tags
  IDITG_REGEN_DELAY		= SimTag::nextTag(), "Regen Delay:";
  IDITG_DROPPOINT_NAME		= SimTag::nextTag(), "Drop Point:";
  IDITG_TEAM_NUMBER		= SimTag::nextTag(), "Team:";
  IDITG_COUNT			= SimTag::nextTag(), "Quantity:";
  IDITG_ENCUMBERANCE		= SimTag::nextTag(), "Encumberance:";
  IDITG_WEIGHT			= SimTag::nextTag(), "Weight:";
  IDITG_EXCLUSIVE		= SimTag::nextTag(), "Exclusive:";
  IDITG_AUTO			= SimTag::nextTag(), "Auto use:";
  IDITG_FTS_SHAPECOLLFACES	= SimTag::nextTag(), "Shape uses Face collision:";
  IDITG_FTS_HULKCOLLFACES	= SimTag::nextTag(), "Hulk uses Face collision:";
  IDITG_FTS_ISSHAPETRANSPARENT	= SimTag::nextTag(), "Shape is Transparent (or Trans.):";
  IDITG_DROPPTS_RANDOM		= SimTag::nextTag(), "Random Selection:";
  IDITG_TURRET_SPEED		= SimTag::nextTag(), "Turret speed:";
  IDITG_TURRET_RANGE		= SimTag::nextTag(), "Visual range:";
  IDITG_FTS_DISABLECOLLISIONS	= SimTag::nextTag(), "Disable Collisions:";
  IDITG_TRIGGER_ID		= SimTag::nextTag(), "Trigger ID:";
  IDITG_NAME			= SimTag::nextTag(), "Name:";
  IDITG_DATA			= SimTag::nextTag(), "Script Data:";
  IDITG_BMP_ROOT_TAG		= SimTag::nextTag(), "BMP root name:";
  IDITG_OFF_BMP_TAG		= SimTag::nextTag(), "OBSOLETE";
  IDITG_TARGET_CONTROL_TAG	= SimTag::nextTag(), "Target Ctrl ID:";

  IDITG_IS_A_SWITCH		= SimTag::nextTag(), "Is an ON/OFF switch:";
  IDITG_LR_OFF_BMP_TAG		= SimTag::nextTag(), "OBSOLETE";

  IDITG_CHAT_DISPLAY_LINES	= SimTag::nextTag(), "# of Lines:";
  IDITG_MENU_FIXED_SIZE		= SimTag::nextTag(), "Max Extent:";

  IDITG_SNOW_INTENSITY		= SimTag::nextTag(), "Snowfall Intensity";
  IDITG_SNOW_WIND		= SimTag::nextTag(), "Snowfall wind direction";

  IDITG_TAB_PAGE_CTRL		= SimTag::nextTag(), "Tab Page Control:";
  IDITG_TAB_SET			= SimTag::nextTag(), "Tab On:";
  IDITG_TAB_ROW_COUNT		= SimTag::nextTag(), "Number Of Rows:";

  IDITG_INV_SHOW_BUY		= SimTag::nextTag(), "Show available?";

  IDITG_LR_POSITION		= SimTag::nextTag(), "Lo-Res Position:";
  IDITG_LR_EXTENT		= SimTag::nextTag(), "Lo-Res Extent:";

  IDITG_BORDER_WIDTH		= SimTag::nextTag(), "Border Width:";

  IDITG_OPT_EXPANDED		= SimTag::nextTag(), "Expanded:";

  IDITG_OBJECT_NAME		= SimTag::nextTag(), "Menu Object name:";

  IDITG_MIRROR_CONS_VAR		= SimTag::nextTag(), "Mirror console var?";

  IDITG_ACTION_MAP		= SimTag::nextTag(), "Action Map:";
  IDITG_ACTION_OR_COMMAND	= SimTag::nextTag(), "Action or Command?";
  IDITG_ACTION_MAKE		= SimTag::nextTag(), "Make Action:";
  IDITG_ACTION_BREAK		= SimTag::nextTag(), "Break Action:";
  IDITG_ACTION_MAKE_VALUE	= SimTag::nextTag(), "Make Value:";
  IDITG_ACTION_BREAK_VALUE	= SimTag::nextTag(), "Break Value:";
  IDITG_ACTION_FLAGS		= SimTag::nextTag(), "Action";
  IDITG_DEVICE			= SimTag::nextTag(), "Device:";
  IDITG_DEVICE_KEYBOARD		= SimTag::nextTag(), "Keyboard";
  IDITG_DEVICE_MOUSE		= SimTag::nextTag(), "Mouse";
  IDITG_DEVICE_JOYSTICK		= SimTag::nextTag(), "Joystick";
  IDITG_DEVICE_OTHER		= SimTag::nextTag(), "Other";

// Smacker ITG tags
  IDITG_SMACK_TAG		= SimTag::nextTag(), "Smacker Tag:";
  IDITG_LOOP			= SimTag::nextTag(), "Loop:";
  IDITG_PRELOAD			= SimTag::nextTag(), "Preload:";
  IDITG_PALSTART		= SimTag::nextTag(), "Place at pal index:";
  IDITG_PALCOLORS		= SimTag::nextTag(), "Colors used:";
  IDITG_MOVIE_DONE_TAG		= SimTag::nextTag(), "Movie Done Tag:";
  IDITG_STRETCH			= SimTag::nextTag(), "Stretch to Fit:";

// Moving shape ITG tags
  IDITG_FM_SHAPE		= SimTag::nextTag(), "Shape:";
  IDITG_FM_COLLISIONFORWARD	= SimTag::nextTag(), "Collision Forward:";
  IDITG_FM_AUTOFORWARD		= SimTag::nextTag(), "Auto Forward:";
  IDITG_FM_AUTOBACKWARD		= SimTag::nextTag(), "Auto Backward:";
  IDITG_FM_TIMESCALE		= SimTag::nextTag(), "Time Scale:";
  IDITG_FM_FORWARDDELAY		= SimTag::nextTag(), "Forward Delay:";
  IDITG_FM_BACKWARDDELAY	= SimTag::nextTag(), "Backward Delay:";
  IDITG_FM_PATHID		= SimTag::nextTag(), "Path ID:";

//SimTag::endData("MISC_ITG");

//SimTag::defineData("STD_ITG", "-- Standard ITG tag DATA group reserves 10,000 tags --", 10000);

  IDEDIT_BEG_ITG		= SimTag::nextTag(); //, "-- Darkstar Inspect Window tags reserves 1.1m to 1.11m --";

  IDITG_VOLUME_NAME		= SimTag::nextTag(), "Volume Name:";
  IDITG_WILL_GHOST		= SimTag::nextTag(), "Will Ghost:";
  IDITG_USE_SCOPING		= SimTag::nextTag(), "Use For Scoping:";
  IDITG_USE_GHOST_SCOPING	= SimTag::nextTag(), "Use Ghost For Scoping:";
  IDITG_POS_TOP_LEFT		= SimTag::nextTag(), "Pos TopLeft (x,y):";
  IDITG_EXTENT_DIM		= SimTag::nextTag(), "Extent (width,height):";
  IDITG_CONTROL_ID		= SimTag::nextTag(), "Control ID:";
  IDITG_HORIZONTAL_SIZING	= SimTag::nextTag(), "Horizontal Sizing:";
  IDITG_VERTICAL_SIZING		= SimTag::nextTag(), "Vertical Sizing:";
  IDITG_VISIBLE			= SimTag::nextTag(), "Visible:";
  IDITG_ACTIVE			= SimTag::nextTag(), "Active:";
  IDITG_MESSAGE_TAG		= SimTag::nextTag(), "Message Tag:";
  IDITG_BMP_TAG			= SimTag::nextTag(), "Bmp Tag:";
  IDITG_BMP_TRANSPARENT		= SimTag::nextTag(), "Bmp Transparent:";
  IDITG_PAL_TAG			= SimTag::nextTag(), "Pal Tag:";
  IDITG_PAL_STRING		= SimTag::nextTag(), "Pal String:";
  IDITG_FONTNAME_TAG		= SimTag::nextTag(), "FontName Tag:";
  IDITG_HL_FONTNAME_TAT		= SimTag::nextTag(), "HL FontName Tag:";
  IDITG_TEXT_TAG		= SimTag::nextTag(), "Text Tag:";
  IDITG_TEXT			= SimTag::nextTag(), "Text:";
  IDITG_ALIGNMENT		= SimTag::nextTag(), "Alignment (0-2):";
  IDITG_TEXT_V_POS_DELTA	= SimTag::nextTag(), "Text V Pos Delta:";
  IDITG_PRODUCE_3D_EVENTS	= SimTag::nextTag(), "Produce 3D Events:";
  IDITG_INITIAL_ALARM		= SimTag::nextTag(), "Initial Alarm:";
  IDITG_REPEAT_ALARM		= SimTag::nextTag(), "Repeat Alarm:";
  IDITG_PBA_TAG			= SimTag::nextTag(), "Bitmap Array Tag:";
  IDITG_HANDLE_ARROW_KEYS	= SimTag::nextTag(), "Handle Arrow Keys:";
  IDITG_ENUMERATE_LIST		= SimTag::nextTag(), "Enumerate List:";
  IDITG_NO_DUPLICATES		= SimTag::nextTag(), "Don't allow Duplicates:";
  IDITG_VISIBILITY_DIST		= SimTag::nextTag(), "Visible Distance:";
  IDITG_HAZE_DIST		= SimTag::nextTag(), "Haze Distance:";
  IDITG_PERSPECTIVE_DIST	= SimTag::nextTag(), "Perspective Distance:";
  IDITG_SCREEN_SIZE		= SimTag::nextTag(), "Screen Size:";
  IDITG_GRID_FILE_NAME		= SimTag::nextTag(), "Grid File Name:";
  IDITG_MATERIAL_LIST_TAG	= SimTag::nextTag(), "Material List Tag:";
  IDITG_TERRAIN_CONTEXT_POS	= SimTag::nextTag(), "Context Position:";
  IDITG_TERRAIN_CONTEXT_ROT	= SimTag::nextTag(), "Context Rotation:";
  IDITG_TERRAIN_GRAVITY		= SimTag::nextTag(), "Gravity:";
  IDITG_TERRAIN_DRAG		= SimTag::nextTag(), "Drag:";
  IDITG_TERRAIN_HEIGHT		= SimTag::nextTag(), "Height:";
  IDITG_PALETTE_NAME		= SimTag::nextTag(), "Palette Name:";
  IDITG_OWN_OBJECTS		= SimTag::nextTag(), "Owns Objects:";
  IDITG_HAZE_VMINDIST		= SimTag::nextTag(), "Haze Vertical Min:";
  IDITG_HAZE_VMAXDIST		= SimTag::nextTag(), "Haze Vertical Max:";
  IDITG_DELETE_ON_LOSE		= SimTag::nextTag(), "Delete On Lose Content:";
  IDITG_DML_FILE_NAME		= SimTag::nextTag(), "Material List DML:";
  IDITG_MAX_STR_LEN		= SimTag::nextTag(), "Max Str Len:";
  IDITG_DISABLED_FONTNAME_TAG	= SimTag::nextTag(), "Disabled FontName Tag:";
  IDITG_BMA_FRAME_TIME		= SimTag::nextTag(), "Time per frame:";
  IDITG_BMA_CONTINUOUS_PLAY	= SimTag::nextTag(), "Continuous Play?";
  IDITG_BMA_LOOP_WAIT_TIME	= SimTag::nextTag(), "Time between loops";
  IDITG_HELP_TAG		= SimTag::nextTag(), "Help Tag:";
  IDITG_BULLET_TYPE_TAG		= 0110062, "Bullet type:";
  IDITG_BULLET_SIZE_TAG		= 0110063, "Bullet size:";
  IDITG_BULLET_SPACING_TAG	= 0110064, "Bullet spacing:";
  IDITG_BMP_FILE_NAME		= 0110065, "BMP File Name:";
  IDITG_SIMOBJECT_NAME		= 0110066, "Object Name:";

  IDITG_SKY_ROTATION		= SimTag::nextTag(), "Rotation (deg.):";
  IDITG_SKY_TEXTUREINDEX_1	= SimTag::nextTag(), "Texture 1 Index:";
  IDITG_SKY_TEXTUREINDEX_2	= SimTag::nextTag(), "Texture 2 Index:";
  IDITG_SKY_TEXTUREINDEX_3	= SimTag::nextTag(), "Texture 3 Index:";
  IDITG_SKY_TEXTUREINDEX_4	= SimTag::nextTag(), "Texture 4 Index:";
  IDITG_SKY_TEXTUREINDEX_5	= SimTag::nextTag(), "Texture 5 Index:";
  IDITG_SKY_TEXTUREINDEX_6	= SimTag::nextTag(), "Texture 6 Index:";
  IDITG_SKY_TEXTUREINDEX_7	= SimTag::nextTag(), "Texture 7 Index:";
  IDITG_SKY_TEXTUREINDEX_8	= SimTag::nextTag(), "Texture 8 Index:";
  IDITG_SKY_TEXTUREINDEX_9	= SimTag::nextTag(), "Texture 9 Index:";
  IDITG_SKY_TEXTUREINDEX_10	= SimTag::nextTag(), "Texture 10 Index:";
  IDITG_SKY_TEXTUREINDEX_11	= SimTag::nextTag(), "Texture 11 Index:";
  IDITG_SKY_TEXTUREINDEX_12	= SimTag::nextTag(), "Texture 12 Index:";
  IDITG_SKY_TEXTUREINDEX_13	= SimTag::nextTag(), "Texture 13 Index:";
  IDITG_SKY_TEXTUREINDEX_14	= SimTag::nextTag(), "Texture 14 Index:";
  IDITG_SKY_TEXTUREINDEX_15	= SimTag::nextTag(), "Texture 15 Index:";
  IDITG_SKY_TEXTUREINDEX_16	= SimTag::nextTag(), "Texture 16 Index:";
  IDITG_SKY_PERSPECTIVE		= SimTag::nextTag(), "Perspective Correct:";
  IDITG_SKY_BACKGROUNDCOLOR	= SimTag::nextTag(), "Background Color: ";
  IDITG_SKY_AZIMUTH		= SimTag::nextTag(), "Azimuth:";
  IDITG_SKY_INCIDENCE		= SimTag::nextTag(), "Incidence:";
  IDITG_SKY_SHADOWS		= SimTag::nextTag(), "Casts Shadows:";
  IDITG_SKY_LENSFLARE		= SimTag::nextTag(), "Lens Flare:";
  IDITG_SKY_LIGHTINTENSITY	= SimTag::nextTag(), "Directional Intensity (RGB):";
  IDITG_SKY_LIGHTAMBIENT	= SimTag::nextTag(), "Ambient Intensity (RGB):";
  IDITG_SKY_BRIGHTSTAR		= SimTag::nextTag(), "Bright Star Color (RGB):";
  IDITG_SKY_MEDIUMSTAR		= SimTag::nextTag(), "Medium Star Color (RGB):";
  IDITG_SKY_DIMSTAR		= SimTag::nextTag(), "Dim Star Color (RGB):";
  IDITG_SKY_NOSTARS		= SimTag::nextTag(), "No. Stars:";
  IDITG_SIZE			= SimTag::nextTag(), "Size:";
  IDITG_DISTANCE		= SimTag::nextTag(), "Distance:";
  IDITG_STARS_IN_FRONT		= SimTag::nextTag(), "Stars render in front of textured sky:";

  IDITG_MIN_VALUE		= SimTag::nextTag(), "Min Value:(float)";
  IDITG_MAX_VALUE		= SimTag::nextTag(), "Max Value:(float)";
  IDITG_BUILD_LIGHTMAP		= SimTag::nextTag(), "Recalculate Light Map";
  IDITG_SAVE_TERRAIN		= SimTag::nextTag(), "Save As: Ted Vol";
  IDITG_POSITION		= SimTag::nextTag(), "Position:";
  IDITG_ROTATION		= SimTag::nextTag(), "Rotation:";
  IDITG_FILENAME		= SimTag::nextTag(), "Filename:";
  IDITG_ISCONTAINER		= SimTag::nextTag(), "Is Container:";
  IDITG_FILENAMEORTAG		= SimTag::nextTag(), "Filename or Tag:";
  IDITG_CONST_THUMB		= SimTag::nextTag(), "Constant Sized Thumb:";
  IDITG_HORIZ_BAR		= SimTag::nextTag(), "Horizontal Scrollbar:";
  IDITG_VERT_BAR		= SimTag::nextTag(), "Vertical Scrollbar:";
  IDITG_SBAR_ALWAYS_ON		= SimTag::nextTag(), "Always On";
  IDITG_SBAR_ALWAYS_OFF		= SimTag::nextTag(), "Always Off";
  IDITG_SBAR_DYNAMIC		= SimTag::nextTag(), "Dynamic";
  IDITG_CONSOLE_VAR		= SimTag::nextTag(), "Console Variable:";
  IDITG_LINE_SPACING		= SimTag::nextTag(), "Line Spacing:";
  IDITG_HORIZ_RESIZE_RIGHT	= SimTag::nextTag(), "Resize Right";
  IDITG_HORIZ_RESIZE_WIDTH	= SimTag::nextTag(), "Resize Width";
  IDITG_HORIZ_RESIZE_LEFT	= SimTag::nextTag(), "Resize Left";
  IDITG_HORIZ_RESIZE_CENTER	= SimTag::nextTag(), "Resize Center";
  IDITG_VERT_RESIZE_BOTTOM	= SimTag::nextTag(), "Resize Bottom";
  IDITG_VERT_RESIZE_HEIGHT	= SimTag::nextTag(), "Resize Height";
  IDITG_VERT_RESIZE_TOP		= SimTag::nextTag(), "Resize Top";
  IDITG_VERT_RESIZE_CENTER	= SimTag::nextTag(), "Resize Center";
  IDITG_CONSOLE_CMD		= SimTag::nextTag(), "Console Command:";
  IDITG_HEADER_SIZES		= SimTag::nextTag(), "Header Dimensions:";
  IDITG_OPAQUE			= SimTag::nextTag(), "Opaque?";
  IDITG_FILL_COLOR		= SimTag::nextTag(), "Fill Color:";
  IDITG_BOARDER			= SimTag::nextTag(), "Boarder?";
  IDITG_BOARDER_COLOR		= SimTag::nextTag(), "Boarder Color:";
  IDITG_PC_RANGEHI		= SimTag::nextTag(), "Range Upper Limit:";
  IDITG_PC_RANGELO		= SimTag::nextTag(), "Range Lower Limit:";
  IDITG_PC_INCREMENT		= SimTag::nextTag(), "Stepping Value:";
  IDITG_SELECT_FILL_COLOR	= SimTag::nextTag(), "Select Fill Color:";
  IDITG_GHOST_FILL_COLOR	= SimTag::nextTag(), "Disabled Fill Color:";
  IDITG_SELECT_BOARDER_COLOR	= SimTag::nextTag(), "Select Boarder Color:";
  IDITG_GHOST_BOARDER_COLOR	= SimTag::nextTag(), "Disabled Boarder Color:";
  IDITG_ALT_CONSOLE_CMD		= SimTag::nextTag(), "Alt Console Command:";
  IDITG_TEXT_NUMBERS_ONLY	= SimTag::nextTag(), "Numeric field only:";
  IDITG_HORIZ_RESIZE_RELATIVE	= SimTag::nextTag(), "Resize Relative";
  IDITG_VERT_RESIZE_RELATIVE	= SimTag::nextTag(), "Resize Relative";

     
// SimMovingInterior
  IDITG_MITR_SHAPE		= SimTag::nextTag(), "Shape:";
  IDITG_MITR_COLLISIONFORWARD	= SimTag::nextTag(), "Collision Forward:";
  IDITG_MITR_AUTOFORWARD	= SimTag::nextTag(), "Auto Forward:";
  IDITG_MITR_AUTOBACKWARD	= SimTag::nextTag(), "Auto Backward:";
  IDITG_MITR_TIMESCALE		= SimTag::nextTag(), "Time Scale:";
  IDITG_MITR_COLLISIONDELAY	= SimTag::nextTag(), "Collision Delay:";
  IDITG_MITR_FORWARDDELAY	= SimTag::nextTag(), "Forward Delay:";
  IDITG_MITR_BACKWARDDELAY	= SimTag::nextTag(), "Backward Delay:";

// SimLight
  IDITG_SIMLT_TYPE		= SimTag::nextTag(), "Type:";
  IDITG_SIMLT_TYPE_POINT	= SimTag::nextTag(), "Point";
  IDITG_SIMLT_TYPE_DIRECTIONAL	= SimTag::nextTag(), "Directional";
  IDITG_SIMLT_TYPE_WRAP		= SimTag::nextTag(), "Wrap";
  IDITG_SIMLT_LOCATION		= SimTag::nextTag(), "Location:";
  IDITG_SIMLT_DIRECTION		= SimTag::nextTag(), "Direction:";
  IDITG_SIMLT_INTENSITY		= SimTag::nextTag(), "Intensity:";
  IDITG_SIMLT_AMBIENTINTENSITY	= SimTag::nextTag(), "Ambient Intensity:";
  IDITG_SIMLT_RANGE		= SimTag::nextTag(), "Range:";
  IDITG_SIMLT_STATICLIGHT	= SimTag::nextTag(), "Static Light:";

// Simpath
  IDITG_SIMPATH_PATHID		= SimTag::nextTag(), "Path ID:";

  IDEDIT_END_ITG		= SimTag::nextTag(); // '-- Darkstar Inspect Window tags reserves 1.1m to 1.11m --'

  IDEDIT_BEG_SHAPE		= SimTag::nextTag(); //'-- Darkstar Editor Shapes reserve 1.11m to 1.12m --'

  IDEDIT_END_SHAPE		= SimTag::nextTag(); //'-- Darkstar Editor Shapes reserve 1.11m to 1.12m --'

//SimTag::endData("STD_ITG");