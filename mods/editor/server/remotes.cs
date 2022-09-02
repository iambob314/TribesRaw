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

function remoteEditor::setControlMode(%clientId, %mode) { REditor::setMode(%clientId, %mode); }
function remoteEditor::setDropMode(%clientId, %mode) { REditor::setDropMode(%clientId, %mode); }

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
			REditor::sel::add(%clientId, %obj);
			REditor::msg(%clientId, "sel add " @ %objName);
		}
	} else if (%mode == rem) {
		if (%hit) {
			REditor::sel::remove(%clientId, %obj);
			REditor::msg(%clientId, "sel remove " @ %objName);
		}
	} else {
		if (%hit) {
			REditor::sel::set(%clientId, %obj);
			REditor::msg(%clientId, "sel set " @ %objName);
		} else {
			REditor::sel::clear(%clientId);
			REditor::msg(%clientId, "sel clear");
		}
	}
}

function remoteEditor::createObject(%clientId, %group, %name) {
	%arglist = EditorRegistry::getArglist(%group, %name);
	if (%arglist == "") { REditor::msgError(%clientId, "bad object " @ %group @ "/" @ %name); return; }

	%x = eval("newObject(" @ %arglist @ ");");
	if (!isObject(%x)) { REditor::msgError(%clientId, "error creating object"); return; }

	addToSet(MissionGroup, %x);
	
	REditor::sel::set(%clientId, %x);
	REditor::dropSelection(%clientId);
}