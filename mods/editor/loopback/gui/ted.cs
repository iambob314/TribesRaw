
function EditorUI::Ted::show() {
	if (EditorUI::loadGUI()) {
		Editor::focusInput(TED);
		EditorUI::Ted::refresh();
	}
	EditorUI::showOnlyControls("TedBar SaveBar"); // hide all controls
}

function EditorUI::refreshTed() {
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

//
// GUI controls
//
