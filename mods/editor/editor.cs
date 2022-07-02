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

function checkMasterTranslation() {} // called periodically after newObject(..., FearCSDelegate, true);

function Editor::createServer(%port) {
	$Server::Port = 28001;
	if (%port != "") $Server::Port = %port;

	newObject(serverDelegate, FearCSDelegate, true, "IP", $Server::Port, "IPX", $Server::Port, "LOOPBACK", $Server::Port);

	newObject(EntitesVolume, SimVolume, "baseres\\shapes\\Entities.vol");
	base::refreshSearchPath();
	
	exec("editor\\armor.cs");
	
	preloadServerDataBlocks();

	resetSimTime();
	resetPlayerManager();
	resetGhostManagers();
	
	newObject(MissionCleanup, SimGroup); // required: magic group name, used by DarkStar for projectiles, AI::Object, etc.

	purgeResources(true);
	
	// TODO: move barebones world to elsewhere
	setInstantGroup(0);
	Editor::newMission(testmis, lush, 1, true, 64, 123);
	//exec("testmis.mis");
	// instant SimGroup "MissionGroup" {
	// instant SimVolume "lushWorld" {
		// fileName = "baseres\\world\\lushWorld.vol";
	// };
	// instant SimVolume "lushTerrain" {
		// fileName = "baseres\\world\\lushTerrain.vol";
	// };
	// instant SimVolume "lushDML" {
		// fileName = "baseres\\world\\lushDML.vol";
	// };
	// instant SimPalette "Palette" {
		// fileName = "lush.day.ppl";
	// };
	// };
}

function Editor::preparePlayGui() {
	newObject(EntitesVolume, SimVolume, "baseres\\gui\\gui.vol");
	newObject("", IRCClient); // required: crash on load play.gui otherwise
	newObject(PlayChatMenu, ChatMenu, "Root Menu:"); // required: DarkStar calls setCMMode(PlayChatMenu, 0); on load play.gui
	newObject(CommandChatMenu, ChatMenu, "Command Menu"); // required: DarkStar calls setCMMode(PlayChatMenu, 0); on load play.gui
}

function Editor::createClient() {
	Editor::preparePlayGui();

	exec(controls);
	newObject(EntitesVolume, SimVolume, "baseres\\shapes\\Entities.vol");
	base::refreshSearchPath();

	newObject(clientDelegate, FearCSDelegate, false, "IP", 0, "IPX", 0, "LOOPBACK", 0);
	//translateMasters(); // ???
}

function Editor::clientConnect(%hostname) {
	purgeResources();
	$Server::Address = %hostname;
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
GUI::newWindow();
GuiLoadContentCtrl(MainWindow, "mainmenu.gui");
flushTextureCache();
	
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