
// Editor::defaultMEOptions initializes the "common" ME options bound to the OptionsCtrl
// modal, used by both loopback and remote editors.
function Editor::defaultMEOptions() {
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
}