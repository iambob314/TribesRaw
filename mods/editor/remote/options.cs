
function Editor::getDropMode() {
	if ($ME::DropAtCamera) return Cam;
	if ($ME::DropWithRotAtCamera) return CamWithRot;
	if ($ME::DropBelowCamera) return BelowCam;
	if ($ME::DropToScreenCenter) return ScreenCenter;
	if ($ME::DropToSelectedObject) return SelectedObject;
	return "";
}

// Editor::getGridSnaps returns the grid snap vector, or "0 0 0" if snapping is off
function Editor::getGridSnaps() {
	if (!$ME::SnapToGrid) return "0 0 0";
	return	def($ME::XGridSnap, 0) @ " " @
			def($ME::YGridSnap, 0) @ " " @
			def($ME::ZGridSnap, 0);
}

// Editor::getConstraints returns the constraint vector of true/false for each dimension
function Editor::getConstraints() {
	return	def($ME::ConstrainX, false) @ " " @
			def($ME::ConstrainY, false) @ " " @
			def($ME::ConstrainZ, false);
}

// Editor::getRotationSnap returns the rotation snap, or 0 if rotation snapping is off
function Editor::getRotationSnap() {
	return tern($ME::SnapRotations, $ME::RotationSnap, 0);
}