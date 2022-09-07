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

	ado(%objArr, deleteObject);

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
	%objArr = REditor::loadObjects(%c, %filebase, atmp());
	
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
	%objArr = afromval(atmp(), %x);

	REditor::sel::set(%c, %objArr);

	if ((%pos = REditor::getDropPos(%c)) != "") GameBase::setPosition(%x, %pos);
	if ((%rot = REditor::getDropRot(%c)) != "") GameBase::setRotation(%x, %rot);

	REditor::action::create::cleanup(%c, %arglist);
	return REditor::action::delete::make(%c, %objArr);
}

function REditor::action::create::cleanup(%c, %arglist) {}

//
// Move object action
//
function REditor::action::move::make(%c, %objArr, %dpos, %drot) {
	%l = alen(%objArr);
	
	%multiRot = (%drot != "" && %l > 1);
	if (%multiRot) {
		if (getWord(%drot, 0) != 0 || getWord(%drot, 1) != 0) {
			REditor::msgErr(%c, "cannot multi-rot except around Z axis");
			return "";
		}
		%dpos = "0 0 0"; // since we're translating due to multi-rot, turn on translation
	}
	
	if (%dpos != "") {
		%posArr = amap(atmp(), %objArr, GameBase::getPosition);
		%posArr = amap(%posArr, %posArr, Vector::add, %dpos);
	}
	if (%drot != "") {
		%rotArr = amap(atmp(), %objArr, GameBase::getRotation);
		%rotArr = amap(%rotArr, %rotArr, Vector::add, %drot);
	}
	if (%multiRot) {
		%centroid = Editor::centroid(%posArr);
		%dzrot = getWord(%drot, 2);
		%posArr = Editor::rotateZAbout(%posArr, %posArr, %centroid, %dzrot);
	}

	%moveArr = anew();
	%newPos = %newRot = "X X X"; // placeholder for "no trans/rotation"
	for (%i = 0; %i < %l; %i++) {
		%obj = aget(%objArr, %i);
		if (%dpos != "") %newPos = aget(%posArr, %i);
		if (%drot != "") %newRot = aget(%rotArr, %i);
		%move = ObjTracker::toVObj(%obj) @ " " @ %newPos @ " " @ %newRot;
		apush(%moveArr, %move);
	}
	
	return "move " @ %moveArr;
}

function REditor::action::move::do(%c, %moveArr, %prevAction) {
	%makeInv = (getWord(%prevAction, 0) != "move"); // don't compute inverse if prev action is move
	
	if (%makeInv) %invMoveArr = anew();
	for (%move = aitfirst(%moveArr); !aitdone(%moveArr); %move = aitnext(%moveArr)) {
		if ((%obj = ObjTracker::fromVObj(%move)) == "") continue;

		%newPos = getWord(%move, 2) @ " " @ getWord(%move, 3) @ " " @ getWord(%move, 4);
		%newRot = getWord(%move, 5) @ " " @ getWord(%move, 6) @ " " @ getWord(%move, 7);
		
		if (%newPos != "X X X") {
			%pos = GameBase::getPosition(%obj);
			GameBase::setPosition(%obj, %newPos);
		} else {
			%pos = "X X X";
		}
		if (%newRot != "X X X") {
			%rot = GameBase::getRotation(%obj);
			GameBase::setRotation(%obj, %newRot);
		} else {
			%rot = "X X X";
		}
		
		if (%makeInv) {
			%invMove = ObjTracker::toVObj(%obj) @ " " @ %pos @ " " @ %rot;
			apush(%invMoveArr, %invMove);
		}
	}

	REditor::action::move::cleanup(%c, %moveArr);
	if (%makeInv) return "move " @ %invMoveArr;
	else          return "";
}

function REditor::action::move::cleanup(%c, %moveArr) { adel(%moveArr); }
