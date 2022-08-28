//
// Remote functions for remote editor and player editor
//

function remoteEditor::setInputMode(%clientId, %mode) {
	if (%mode == Player) {
		%obj = Client::getOwnedObject(%clientId);
	} else if (%mode == Observer) {
		%obj = Client::getObserverCamera(%clientId);
	}

	if (%obj != "") Client::setControlObject(%clientId, %obj);
}