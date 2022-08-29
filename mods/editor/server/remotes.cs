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

function remoteEditor::setControlMode(%clientId, %mode) {
	if (%mode == Player) {
		%obj = Client::getOwnedObject(%clientId);
	} else if (%mode == Observer) {
		%obj = Client::getObserverCamera(%clientId);
	}

	if (%obj != "") Client::setControlObject(%clientId, %obj);
}


function remoteEditor::castSelect(%clientId) {
	%obj = Client::getControlObject(%clientId);
	
	if (%obj == Client::getObserverCamera(%clientId)) {
		%pos = GameBase::getPosition(%obj);
		%dir = Vector::getFromRot(GameBase::getRotation(%obj), 1000);
		%pos2 = Vector::add(%pos, %dir);
		
		%mask = ~($ObjectType::Terrain | $ObjectType::Container | $ObjectType::Default);
		%hit = getLOSInfo(%pos, %pos2, %mask);
	} else {
		%hit = GameBase::getLOSInfo(%obj, 1000) && getObjectType($los::object) != SimTerrain;
	}
	
	if (!%hit) $los::object = $los::position = "";
	echos("HIT", getObjectType($los::object), $los::object, $los::position);
	
	%clientId.selected = $los::object;
	remoteEval(%clientId, Editor::onSelect, %clientId.selected);
}