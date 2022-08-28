exec("editor\\loopback\\gui\\camera.cs");
exec("editor\\loopback\\gui\\create.cs");
exec("editor\\loopback\\gui\\inspect.cs");
exec("editor\\loopback\\gui\\ted.cs");

function EditorUI::refreshControls() {
	EditorUI::refreshCreatorLists(); // in create.cs
	EditorUI::refreshMissionObjectList();
	EditorUI::refreshTed(); // in ted.cs
}

function EditorUI::refreshMissionObjectList() {
	MissionObjectList::ClearDisplayGroups();
	MissionObjectList::AddDisplayGroup(1, "MissionGroup");
	MissionObjectList::AddDisplayGroup(1, "MissionCleanup");
	MissionObjectList::SetSelMode(1);
	
	if ($ME::InspectObject != "")
		MissionObjectList::Inspect($ME::InspectWorld, $ME::InspectObject);
}
