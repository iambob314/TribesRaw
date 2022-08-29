exec("editor\\remote\\gui\\camera.cs");
exec("editor\\remote\\gui\\create.cs");
exec("editor\\remote\\gui\\inspect.cs");

function EditorUI::refreshControls() {
	EditorUI::refreshCreatorLists(); // in create.cs
	//EditorUI::refreshMissionObjectList(); // TODO: with mirrored MisObjList
}

// function EditorUI::refreshMissionObjectList() {
// 	MissionObjectList::ClearDisplayGroups();
// 	MissionObjectList::AddDisplayGroup(1, "MissionGroup");
// 	MissionObjectList::AddDisplayGroup(1, "MissionCleanup");
// 	MissionObjectList::SetSelMode(1);
// 	
// 	if ($ME::InspectObject != "")
// 		MissionObjectList::Inspect($ME::InspectWorld, $ME::InspectObject);
// }
