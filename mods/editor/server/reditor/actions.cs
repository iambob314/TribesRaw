//
// Actions (for use in action stack)
//

//
// Delete
//

// Note: does not take ownership of %objArr
function REditor::action::delete::make(%c, %objArr) {
	%vobjArr = ObjTracker::toVObjs(%objArr, anew());
	return "delete " @ %vobjArr;
}

function REditor::action::delete::do(%c, %vobjArr) {
	%objArr = ObjTracker::fromVObjs(%vobjArr, atmp()); // drop any non-existent/recreated objects

	// Save the objects for the inverse first, since we're about to delete them
	%filebase = REditor::nextTmpFile(%c);
	REditor::saveObjects(%c, %filebase, false, %objArr);

	ado(deleteObject, %objArr);

	REditor::action::delete::cleanup(%c, %vobjArr);
	return REditor::action::paste::make(%c, %filebase);
}

function REditor::action::delete::cleanup(%c, %vobjArr) { adel(%vobjArr); }

//
// Paste action
//
function REditor::action::paste::make(%c, %filebase, %keepFile) {
	return "paste " @ %filebase @ " " @ def(%keepFile, false);
}

function REditor::action::paste::do(%c, %args) {
	%filebase = getWord(%args, 0);
	%objArr = REditor::loadObjects(%c, %filebase);
	
	REditor::sel::set(%c, %objArr); // select newly-pasted objects

	REditor::action::paste::cleanup(%c, %args);
	return REditor::action::delete::make(%c, %objArr);
}

function REditor::action::paste::cleanup(%c, %args) {
	%filebase = getWord(%args, 0);
	%keepFile = getWord(%args, 1);
	if (!%keepFile) REditor::deleteObjectFile(%c, %filebase);
}

//
// Create object action
//
function REditor::action::create::make(%c, %arglist) {
	return "create " @ %arglist;
}

function REditor::action::create::do(%c, %arglist) {
	%x = eval("newObject(" @ %arglist @ ");");
	if (!isObject(%x)) { REditor::msgErr(%c, "error creating object"); return; }

	addToSet(REditor::sandboxGroup(%c), %x);
	%objArr = afromval(%x);
	
	REditor::sel::set(%c, %objArr);

	if ((%pos = REditor::getDropPos(%c)) != "") GameBase::setPosition(%x, %pos);
	if ((%rot = REditor::getDropRot(%c)) != "") GameBase::setRotation(%x, %rot);

	REditor::action::create::cleanup(%c, %arglist);
	return REditor::action::delete::make(%c, %objArr);
}

function REditor::action::create::cleanup(%c, %arglist) {}
