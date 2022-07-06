$EditorUI::ModeCamera = 0;
$EditorUI::ModeCreate = 1;
$EditorUI::ModeInspect = 2;
$EditorUI::ModeTED = 3;

$EditorUI::lastMode = $EditorUI::ModeCreate;

function EditorUI::show(%mode) {
	if (%mode == "") %mode = $EditorUI::lastMode;

	EditorUI::focus(%mode);

	Control::setVisible("MEObjectList", %mode == $EditorUI::ModeCreate || %mode == $EditorUI::ModeInspect);
	Control::setVisible("Inspector", %mode == $EditorUI::ModeInspect);
	Control::setVisible("Creator", %mode == $EditorUI::ModeCreate);
	Control::setVisible("TedBar", %mode == $EditorUI::ModeTED);
	Control::setVisible("SaveBar", %mode != $EditorUI::ModeCamera);
	// Control::setVisible("AddVolume", false); // TODO: is this useful? (dialog to add vol file)

	// TODO: move to Group/NameList refresh funcs
	TextList::clear("GroupList");
	TextList::clear("NameList");
	
	if (%mode == $EditorUI::ModeTED) EditorUI::refreshTEDUI();
}

function EditorUI::focus(%mode) {
	if (!isObject(EditorGui)) GuiLoadContentCtrl(MainWindow, "gui\\editor.gui");
	
	unfocus(playDelegate);
	if (%mode != $EditorUI::ModeTED) {
		unfocus(TedObject);
		focus(MissionEditor);
	} else {
		unfocus(MissionEditor);
		focus(TedObject);
	}
	focus(editCamera);
	postAction(EditorGui, Attach, editCamera);
	cursorOn(MainWindow);
}

function EditorUI::unfocus() {
	if (!isObject(playGui)) GuiLoadContentCtrl(MainWindow, "gui\\play.gui");

	unfocus(TedObject);
	unfocus(MissionEditor);
	unfocus(editCamera);
	focus(playDelegate);
	cursorOff(MainWindow);
}

function EditorUI::refreshTEDUI() {
	return;
	// TODO: all of this is straight copied from MEShowTed(); it's junky and references $TED vars we haven't defined
	
	// set the right button to nothing
	TED::setRButtonAction( "" );

	// set the terrain type popup
	Popup::clear(TerrainTypes);

	%i = Ted::getNumTerrainTypes();
	if( $TED::success == true )
		for( %j = 0; %j < %i; %j++ )
			Popup::addLine( TerrainTypes, Ted::getTerrainTypeName(%j), %j );
	Popup::setSelected(TerrainTypes, Ted::getTerrainType() );

	// set the detail level popup
	Popup::clear(BrushDetail);
	%i = Ted::getMaxBrushDetail();
	if( $TED::success == true )
		for( %j = 0; %j < %i; %j++ )
			Popup::addLine( BrushDetail, "Level " @ %j, %j );
	Popup::setSelected(BrushDetail, Ted::getBrushDetail() );

	// setup the material list popup
	Popup::clear(MaterialList);
	%i = Ted::getMaterialCount();
	if($TED::success == true)
		for(%j = 0; %j < %i; %j++)
			Popup::addLine(MaterialList, Ted::getMaterialName(%j), %j);
	Popup::setSelected(MaterialList, Ted::getMaterialIndex());

	// setup the buttons with default's if no pref
	MESetupTedButton( TEDModeOne, select, true );
	MESetupTedButton( TEDModeTwo, deselect, true );

	// setup the terrain generation stuff
	Control::setText(SeedTerrain, "Gen: " @ $ME::terrainSeed);

	// get the terrain name (obj 8)
	focusServer();
	%typeName = 8.terrainType;
	focusClient();

	Popup::clear(Terrains);
	%select = 0;
	for(%i = 0; $terrainTypes[%i, type] != ""; %i++)
	{
		if($terrainTypes[%i, type] == %typeName)
			%select = %i;
		Popup::addLine(Terrains, $terrainTypes[%i, description], %i);
	}
	Popup::setSelected(Terrains, %select);

	// select the first button
	PopupButton::makeCurrent(TEDModeOne);
	TEDModeOne::onPressed();
	  
	MESetupTedButton( TEDProcessAction, "" );
	Control::setText( TEDProcessAction, " -- Selection Action --" );
}