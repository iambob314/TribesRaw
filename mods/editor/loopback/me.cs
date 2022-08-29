// Direct interaction with the ME system (magic vars, callbacks, etc.)

function Editor::initMESettings() {
	// TODO: init other "special" $ME vars and call ME::GetConsoleOptions()
	
	// Set up ME modes/variables
	ME::SetGrabMask(~($ObjectType::Terrain | $ObjectType::Container | $ObjectType::Default));
	ME::SetDefaultPlaceMask($ObjectType::Terrain | $ObjectType::Interior);

	$ME::MoveSensitivity 	= 0.2;
	$ME::RotateSensitivity 	= 0.02;
	
	$ME::ShowEditObjects = true;
	$ME::ShowGrabHandles = true;
	
	$ME::SnapToGrid = true;
	$ME::XGridSnap = $ME::YGridSnap = $ME::ZGridSnap = 0.125;
	$ME::ConstrainX = $ME::ConstrainY = $ME::ConstrainZ = false;
	$ME::SnapRotations = false;
	$ME::RotationSnap = 90;
	$ME::RotateXAxis = $ME::RotateYAxis = false;
	$ME::RotateZAxis = true;
	
	$ME::DropAtCamera = $ME::DropWithRotAtCamera = false;
	$ME::DropBelowCamera = $ME::DropToSelectedObject = false;
	$ME::DropToScreenCenter = true;
	
	$ME::UsePlaneMovement = true;
	
	ME::GetConsoleOptions(); // load vars into ME internals
}

// missionSaveObject and missionLoadObject are called directly by ME::CutSelection, etc.
function missionSaveObject(%objName, %fileName) {
	if (%c = (getManagerId() != 2048)) focusServer();
	exportObjectToScript(%objName, "temp\\" @ %fileName);
	base::refreshSearchPath();
	if (%c) focusClient();
}
function missionLoadObject(%objParentName, %fileName) {
	if (%c = (getManagerId() != 2048)) focusServer();
	setInstantGroup(%objParentName);
	exec(%fileName); // temp is in the path.
	if (%c) focusClient();
}

// Magic vars loaded during ME::GetConsoleOptions():
// (see https://github.com/AltimorTASDK/TribesRebirth/blob/1105fd0890c19c13f816b91e51b9cf0658ffc63c/program/code/FearMissionEditor.cpp#L1306)
// $ME::ShowEditObjects
// $ME::ShowGrabHandles
// $ME::SnapToGrid
// $ME::XGridSnap
// $ME::YGridSnap
// $ME::ZGridSnap
// $ME::ConstrainX
// $ME::ConstrainY
// $ME::ConstrainZ
// $ME::RotateXAxis
// $ME::RotateYAxis
// $ME::RotateZAxis
// $ME::RotationSnap
// $ME::SnapRotations
// $ME::DropAtCamera
// $ME::DropWithRotAtCamera
// $ME::DropBelowCamera
// $ME::DropToScreenCenter
// $ME::DropToSelectedObject
// $ME::UsePlaneMovement
// $ME::ObjectsSnapToTerrain
