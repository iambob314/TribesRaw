//
// Selection sets
//

function REditor::sel(%c) {
	if (%c.sel == "") addToSet(MissionCleanup, %c.sel = newObject(SelectionSet, SimSet));
	return %c.sel;
}

function REditor::sel::arr(%c, %arr) {
	return afromset(REditor::sel(%c), %arr);
}

function REditor::sel::len(%c) {
	return Group::len(REditor::sel(%c));
}

function REditor::sel::clear(%c) {
	%sel = REditor::sel(%c);
	while ((%obj = Group::getObject(%sel, 0)) != -1)
		removeFromSet(%sel, %obj);
}

function REditor::sel::add(%c, %objArr) {
	atoset(REditor::sel(%c), %objArr);
}

function REditor::sel::remove(%c, %objArr) {
	%sel = REditor::sel(%c);
	ado(removeFromSet, %sel, %objArr);
}

function REditor::sel::set(%c, %objArr) {
	REditor::sel::clear(%c);
	REditor::sel::add(%c, %objArr);
}
