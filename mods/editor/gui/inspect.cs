
function EditorUI::Inspect::show() {
	if (EditorUI::loadGUI()) {
		Editor::focusInput(ME);
	}
	EditorUI::showOnlyControls("MEObjectList Inspector SaveBar");
}

//
// GUI controls
//

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


function ObjectToCamera::onAction()       { ME::ObjectToCamera(); }
function CameraToObject::onAction()       { ME::CameraToObject(); }
function ObjectToScreenCenter::onAction() { ME::ObjectToSC(); }
