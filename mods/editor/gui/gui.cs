// Editor modes: camera create inspect ted
$EditorUI::mode = "create"; // current mode, or last used mode if not in editor UI

// Top-level UI modes

function EditorUI::getMode() {
	if (isObject(EditorGui)) return $EditorUI::mode;
	else return "";
}

function EditorUI::showMode(%mode) {
	if (%mode == "") %mode = $EditorUI::mode;
	$EditorUI::mode = %mode;

	EditorUI::show(%mode); // enter editor UI if needed

	Control::setVisible("MEObjectList", %mode == "create" || || %mode == "inspect");
	Control::setVisible("Inspector", %mode == "inspect");
	Control::setVisible("Creator", %mode == "create");
	Control::setVisible("TedBar", %mode == "ted");
	Control::setVisible("SaveBar", %mode != "camera");
	// Control::setVisible("AddVolume", false); // TODO: is this useful? (dialog to add vol file)
}

function EditorUI::show(%mode) {
	if (isObject(EditorGui)) return; // already shown

	GuiLoadContentCtrl(MainWindow, "gui\\editor.gui");
	EditorUI::focus(%mode);

	EditorUI::refreshCreator();
	EditorUI::refreshMisObjList();
	EditorUI::refreshTED();
}

function EditorUI::hide() {
	if (!isObject(EditorGui)) return; // already hidden
	
	GuiLoadContentCtrl(MainWindow, "gui\\play.gui");
	EditorUI::unfocus();
}

function EditorUI::focus(%mode) {
	unfocus(playDelegate);
	if (%mode == "ted") {
		unfocus(MissionEditor);
		focus(TedObject);
	} else {
		unfocus(TedObject);
		focus(MissionEditor);
	}
	focus(editCamera);
	postAction(EditorGui, Attach, editCamera);
	cursorOn(MainWindow);
}

function EditorUI::unfocus() {
	unfocus(TedObject);
	unfocus(MissionEditor);
	unfocus(editCamera);
	focus(playDelegate);
	cursorOff(MainWindow);
}

// Refresh lists

function EditorUI::refreshCreator() {
	TextList::clear(GroupList);
	TextList::clear(NameList);
	
	for (%group = aitfirst(ME::objgroups); !aitdone(ME::objgroups); %group = aitnext(ME::objgroups))
		TextList::addLine(GroupList, %group);
}

function EditorUI::refreshMisObjList() {
	MissionObjectList::ClearDisplayGroups();
	MissionObjectList::AddDisplayGroup(1, "MissionGroup");
	MissionObjectList::AddDisplayGroup(1, "MissionCleanup");
	MissionObjectList::SetSelMode(1);
	
	if ($ME::InspectObject != "")
		MissionObjectList::Inspect($ME::InspectWorld, $ME::InspectObject);
}

function EditorUI::refreshTED() {
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

// GUI controls

function GroupList::onAction() {
	TextList::clear(NameList);
	%group = Control::getValue(GroupList);

	%namesArr = "ME::objgroup_" @ %group @ "::names";
	for (%name = aitfirst(%namesArr); !aitdone(%namesArr); %name = aitnext(%namesArr))
		TextList::AddLine(NameList, %name);
}

function NameList::onAction() {
	%group = Control::getValue(GroupList);
	%name = Control::getValue(NameList);
	%namesArr = "ME::objgroup_" @ %group @ "::names";
	%scriptsArr = "ME::objgroup_" @ %group @ "::scripts";

	%idx = afind(%name, %namesArr);
	if (%idx == -1)
		echos("ohnoes", %group, %name);

	%script = aget(%idx, %scriptsArr);
	echos("TODO", %idx, %name, %script);
	%x = eval(%script);
	echo(%x);
}

// Note: MissionObjectList::on* are more than GUI functions: ME itself calls these selecting
// objects in the world, and they must update/call all of these for ME to work right:
// * MissionObjectList::Inspect
// * $ME::Inspect*
// * and ME::on*

function MissionObjectList::onUnselected(%world, %obj) {
	if (%obj == $ME::InspectObject && %world == $ME::InspectWorld) {
		MissionObjectList::Inspect(1, -1);
		$ME::InspectObject = "";
	}
	ME::onUnselected(%world, %obj);
}

function MissionObjectList::onSelectionCleared() {
	MissionObjectList::Inspect(1, -1);
	$ME::InspectObject = "";
	ME::onSelectionCleared();
}

function MissionObjectList::onSelected(%world, %obj) {
	if ($ME::InspectObject == "") {
		$ME::InspectObject = %obj;
		$ME::InspectWorld = %world;
		MissionObjectList::Inspect($ME::InspectWorld, %obj);

		// grab .locked from server
		focusServer(); %locked = %obj.locked; focusClient();
		Control::setText("LockButton", tern(%locked, "Unlock", "Lock"));
	}
	ME::onSelected(%world, %obj);
}

function ApplyButton::onAction() { MissionObjectList::Apply(); }

function LockButton::onAction() {
	if ($ME::InspectObject == "") return;

	%obj = $ME::InspectObject;
	focusServer(); %locked = %obj.locked = !%obj.locked; focusClient();
	Control::setText("LockButton", tern(%locked, "Unlock", "Lock"));

	MissionObjectList::Inspect($ME::InspectWorld, $ME::InspectObject); // refresh inspector
}

function EditorUI::hideOptions() {
	if (Control::getVisible("OptionsCtrl")) {
		Control::setVisible("OptionsCtrl", false);
		ME::GetConsoleOptions();
	}
	if (Control::getVisible("TedOptionsCtrl")) {
		Control::setVisible("TedOptionsCtrl", false);
		Ted::GetConsoleOptions();
		// Control::setText(SeedTerrain, "Gen: " @ $ME::terrainSeed); TODO: why?
	}
}

function EditorUI::showOptions() {
	EditorUI::hideHelp();

	%mode = EditorUI::getMode();
	if (%mode == "create" || %mode == "inspect") {
		Control::setVisible(OptionsCtrl, true);

		Control::setValue(MEUsePlaneMovement, $ME::UsePlaneMovement);
		Control::setActive(RotationSnapCtrl, $ME::SnapRotations);
		Control::setActive(XGridSnapCtrl, $ME::SnapToGrid);
		Control::setActive(YGridSnapCtrl, $ME::SnapToGrid);
		Control::setActive(ZGridSnapCtrl, $ME::SnapToGrid);
		Control::setActive(UseTerrainGrid, $ME::SnapToGrid);
	} else if (%mode == "ted") {
		Control::setVisible(TedOptionsCtrl, true);
		Control::setValue(TerrainSeedText, $ME::terrainSeed);
	}
}

function EditorUI::toggleOptions() {
	%mode = EditorUI::getMode();
	if (%mode == "create" || %mode == "inspect")
		%optionsCtrl = OptionsCtrl;
	else if (%mode == "ted")
		%optionsCtrl = TedOptionsCtrl;
      
	if (Control::getVisible(%optionsCtrl)) EditorUI::hideOptions();
	else EditorUI::showOptions();
}

//-----------------------------------------------------

function EditorUI::hideHelp() {
	Control::setVisible(HelpCtrl, false );
}

function EditorUI::showHelp() {
	EditorUI::hideOptions();
	Control::setVisible(HelpCtrl, true);
}

function EditorUI::toggleHelp() {
   if (Control::getVisible(HelpCtrl)) EditorUI::hideHelp();
   else EditorUI::showHelp();
}
