// TODO: embed controls in editor mod itself
// TODO: better barebones world with flat terrain
// TODO: get basic player movement working
// TODO: get editor camera and GUI attachment
requireMod(std);
requireMod(gui);

exec("editor\\connectseq.cs");
exec("editor\\clientevents.cs");
exec("editor\\serverevents.cs");

exec("editor\\newmission.cs");

function checkMasterTranslation() {} // called periodically after newObject(..., FearCSDelegate, true); TODO: implement for real

function Editor::createServer(%port) {
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
	//exec("testmis.mis");
	newObject(MissionCleanup, SimGroup); // required: magic group name, used by DarkStar for projectiles, AI::Object, etc.
}

function Editor::preparePlayGui() {
	newObject(EntitesVolume, SimVolume, "baseres\\gui\\gui.vol");
	newObject("", IRCClient); // required: crash on load play.gui otherwise
	newObject(PlayChatMenu, ChatMenu, "Root Menu:"); // required: DarkStar calls setCMMode(PlayChatMenu, 0); on load play.gui
	newObject(CommandChatMenu, ChatMenu, "Command Menu"); // required: DarkStar calls setCMMode(PlayChatMenu, 0); on load play.gui
}

function Editor::createClient() {
	Editor::preparePlayGui();
	GUI::newWindow(MainWindow, "mainmenu.gui");

	//newObject(EntitesVolume, SimVolume, "baseres\\shapes\\Entities.vol");
	//base::refreshSearchPath();

	exec("editor\\controls.cs");

	newObject(clientDelegate, FearCSDelegate, false, "IP", 0, "IPX", 0, "LOOPBACK", 0);
	//translateMasters(); // ???
}

function Editor::clientConnect(%hostname) {
	purgeResources();
	connect(%hostname);
}

function Editor::spawn(%clientId) {
	%player = newObject("", Player, larmor);
	GameBase::setPosition(%player, "0 0 100");

	Client::setOwnedObject(%clientId, %player);
	Client::setControlObject(%clientId, %player);

	Client::setGuiMode(%clientId, 1);
}

focusClient();	
Editor::createClient(); // required: must create clientDelegate BEFORE serverDelegate, or LOOPBACK does not work

// Experiments
if ($argv[0] == "-server") {
	focusServer();
	Editor::createServer(28001);
}

focusClient();
if ($argv[0] == "-server") {
	Editor::clientConnect("LOOPBACK:28001");
} else {
	Editor::clientConnect("IP:localhost:28001");
}