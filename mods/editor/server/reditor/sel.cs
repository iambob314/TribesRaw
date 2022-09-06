//
// Selection sets
//

function REditor::sel(%c) {
	if (%c.sel == "") %c.sel = anew();
	return %c.sel;
}

function REditor::sel::arr(%c, %outArr) {
	ObjTracker::fromVObjs(REditor::sel(%c), %outArr); // also prunes
	return %outArr;
}

function REditor::sel::prune(%c) {
	ObjTracker::pruneVObjs(REditor::sel(%c));
}

function REditor::sel::find(%c, %obj) {
	%sel = REditor::sel(%c);
	for (%vobj = aitfirst(%sel); !aitdone(%sel); %vobj = aitnext(%sel))
		if (ObjTracker::fromVObj(%vobj) == %obj)
			return ait(%sel);
	return -1;
}

function REditor::sel::add(%c, %obj) {
	%sel = REditor::sel(%c);
	%vobj = ObjTracker::toVObj(%obj);
	if ((%at = REditor::sel::find(%c, %obj)) != -1)
		aset(%at, %vobj, %sel); // update version, in case it changed
	else
		apush(%vobj, %sel);
}

function REditor::sel::remove(%c, %obj) {
	if ((%at = REditor::sel::find(%c, %obj)) != -1)
		asetpop(%at, %sel);
}

function REditor::sel::set(%c, %objArr) {
	ObjTracker::toVObjs(%objArr, REditor::sel(%c));
}
