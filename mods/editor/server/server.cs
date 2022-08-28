// Server-side code for the map editor.
//
// Covers map creation, server-side player management, and remotes.
//
// Does not include connection sequence (that's in connectseq.cs for clarity),
// or "focusServer" jumps by loopback client.
//

exec("editor\\server\\newmission.cs");

exec("editor\\server\\remote.cs");

function Editor::initServer(%port) {
	// Create serverDelegate (server socket, etc.)
	if (%port != "") %port = 28001;
	newObject(serverDelegate, FearCSDelegate, true, "IP", %port, "IPX", %port, "LOOPBACK", %port);

	// TODO: better VOL loading routine (need RPG vols, etc.)
	newObject(EntitesVolume, SimVolume, "baseres\\shapes\\Entities.vol");
	base::refreshSearchPath();

	// Load datablocks
	exec("editor\\armor.cs");
	preloadServerDataBlocks();

	// Reset managers
	resetSimTime();
	resetPlayerManager();
	resetGhostManagers();
	purgeResources(true);

	// Create/load world
	setInstantGroup(0);
	Editor::newMission(testmis, lush, 1, true, 64, 123);
	
	newObject(MissionCleanup, SimGroup); // required: magic group name, used by DarkStar for projectiles, AI::Object, etc.
}

// Editor::spawn gives the client a player.
function Editor::spawn(%clientId) {
	%player = newObject("", Player, larmor);
	GameBase::setPosition(%player, "0 0 100");

	Client::setOwnedObject(%clientId, %player);
	Client::setControlObject(%clientId, %player);

	Client::setGuiMode(%clientId, 1);
}

// Server-side events

function Server::onClientDisconnect(%clientId) { echo("Server::onClientDisconnect ", %clientId); }
