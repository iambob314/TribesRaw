//
// Object and position/rotation manipulation
//

function Editor::centroid(%posArr) {
	if (alen(%posArr) == 1)
		return aget(%posArr, 0); // special case for one position
	
	// Sum all positions and divide by the count for the average
	return Vector::scale(areduce(%posArr, Vector::add), 1/alen(%posArr));
}	

function Editor::rotateZAbout(%outArr, %posArr, %centroid, %rotZ) {
	%rotMat = Matrix::rotZ(%rotZ);
	// Rotate each point around the centroid
	return amap(%outArr, %posArr, Editor::rotateZAboutSingle, %centroid, %rotMat);
}

// relativize around %centroid -> rotate by %rotMat -> de-relativize from %centroid
function Editor::rotateZAboutSingle(%centroid, %rotMat, %pos) {
	%pos = Vector::sub(%pos, %centroid);
	%pos = Matrix::mul(%rotMat, 3, 3, %pos, 3);
	%pos = Vector::add(%pos, %centroid);
	return %pos;
}