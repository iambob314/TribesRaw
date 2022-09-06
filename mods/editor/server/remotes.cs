//
// Remote functions for remote editor and player editor
//

//
// Remote registry stuff
//

function remoteEditor::downloadRegistry(%clientId) {
	%groupArr = EditorRegistry::groups();
	ado(Editor::sendRegGroup, %groupArr, %clientId);
	remoteEval(%clientId, Editor::downloadComplete);
}

function Editor::sendRegGroup(%clientId, %group) {
	%namesArr = EditorRegistry::groupNames(%group);
	ado(remoteEval, %namesArr, %clientId, Editor::addRegistryEntry, %group);
}

//
// Remote editor control stuff
//

function remoteEditor::setMode(%clientId, %mode) { REditor::setMode(%clientId, %mode); }
function remoteEditor::setOptions(%clientId, %dropMode) {
	REditor::setOptions(%clientId, %dropMode);
}

function remoteEditor::deleteSelection(%clientId) { REditor::deleteSelection(%clientId); }
function remoteEditor::cutSelection(%clientId)    { REditor::cutSelection(%clientId); }
function remoteEditor::copySelection(%clientId)   { REditor::copySelection(%clientId); }
function remoteEditor::pasteSelection(%clientId)  { REditor::pasteSelection(%clientId); }
function remoteEditor::dupSelection(%clientId)    { REditor::dupSelection(%clientId); }
function remoteEditor::dropSelection(%clientId)   { REditor::dropSelection(%clientId); }

function remoteEditor::undo(%clientId)   { REditor::undo(%clientId); }
function remoteEditor::redo(%clientId)   { REditor::redo(%clientId); }

function remoteEditor::createObject(%clientId, %group, %name) {
	REditor::createObject(%clientId, %group, %name);
}

// Effect of hit/miss on selection set by %mode:
// ""  -> set/clear
// add -> add/nothing
// rem -> rem/nothing
function remoteEditor::castSelect(%clientId, %mode) {
	%hit = REditor::LOS(%clientId);
	if (%hit) {
		%obj = $los::object;
		%objName = getObjectType(%obj);
		if (%dn = GameBase::getDataName(%obj)) %objName = %objName @ " " @ %dn;
		if (%name = Object::getName(%obj))     %objName = %name @ " (" @ %objName @ ")";
	}
	
	if (%mode == add) {
		if (%hit) {
			REditor::sel::add(%clientId, afromval(%obj));
			REditor::msg(%clientId, "sel add " @ %objName);
		}
	} else if (%mode == rem) {
		if (%hit) {
			REditor::sel::remove(%clientId, afromval(%obj));
			REditor::msg(%clientId, "sel remove " @ %objName);
		}
	} else {
		if (%hit) {
			REditor::sel::set(%clientId, afromval(%obj));
			REditor::msg(%clientId, "sel set " @ %objName);
		} else {
			REditor::sel::clear(%clientId);
			REditor::msg(%clientId, "sel clear");
		}
	}
}
