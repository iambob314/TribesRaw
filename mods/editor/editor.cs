// TODO: embed controls in editor mod itself
// TODO: better barebones world with flat terrain
// TODO: get basic player movement working
// TODO: get editor camera and GUI attachment
requireMod(std);
requireMod(gui);

exec("editor\\connectseq.cs");

exec("editor\\actions.cs");
exec("editor\\me.cs");

exec("editor\\gui\\gui.cs");
exec("editor\\shapelist.cs");
exec("editor\\newmission.cs");

exec("editor\\editorcontrols.cs");

function checkMasterTranslation() {} // called periodically after newObject(..., FearCSDelegate, true); TODO: implement for real

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

function Editor::initClient() {
	// Prepare play.gui
	newObject(GuiVolume, SimVolume, "baseres\\gui\\gui.vol");
	newObject("", IRCClient); // required: crash on load play.gui otherwise
	newObject(PlayChatMenu, ChatMenu, "Root Menu:"); // required: DarkStar calls setCMMode(PlayChatMenu, 0); on load play.gui
	newObject(CommandChatMenu, ChatMenu, "Command Menu"); // required: DarkStar calls setCMMode(PlayChatMenu, 0); on load play.gui
	
	// Load other vols
	newObject(EntitiesVolume, SimVolume, "baseres\\shapes\\entities.vol"); // required: vols with DTS must be explicitly loaded, don't sync from instant group it seems
	
	GUI::newWindow(MainWindow, "mainmenu.gui");

	newObject(clientDelegate, FearCSDelegate, false, "IP", 0, "IPX", 0, "LOOPBACK", 0);
	//translateMasters(); // ???
}

function Editor::clientConnect(%hostname) {
	purgeResources();
	connect(%hostname);
}

function Editor::initMERemote() {
	exec("editor\\controls.cs");
}

function Editor::initMELoopback() {
	// ME + editCamera init
	ME::Create(MainWindow);

	exec("editor\\controls.cs");
	newObject(editCamera, EditCamera, "editor.sae");
	$ME::camera = editCamera; // required: used by DarkStar internally
	
	Editor::ME::initSettings();
	
	// TED init (TODO: crashy because missing TED config funcs/vars probably)
	//Ted::initTed();
	//Ted::attachToTerrain();
	// TODO: other TED config funcs (and vars, but these may be simple mirrors of config funcs and unnecessary)
}

// Editor::focusInput focuses inputs on one of three modes:
// * Player: player delegate
// * ME: mission editor
// * TED: terrain editor
function Editor::focusInput(%m) {
	assert(%m == Player || %m == ME || %m == TED, "bad mode '" @ %m @ "'");
	
	setFocus(playDelegate,  %m == Player);
	setFocus(editCamera,    %m != Player);
	setFocus(MissionEditor, %m == ME);
	setFocus(TedObject,     %m == TED);
	if (%m == Player) {
		cursorOff(MainWindow);
	} else {
		cursorOn(MainWindow);
		postAction(EditorGui, Attach, editCamera);
	}
}

function setFocus(%thing, %on) { if (%on) focus(%thing); else unfocus(%thing); }

function Editor::spawn(%clientId) {
	%player = newObject("", Player, larmor);
	GameBase::setPosition(%player, "0 0 100");

	Client::setOwnedObject(%clientId, %player);
	Client::setControlObject(%clientId, %player);

	Client::setGuiMode(%clientId, 1);
}

// TODO: this tail is experimental; clean up later

$Editor::isLoopback = ($argv[0] == "-server");

focusClient();	
Editor::initClient(); // required: must create clientDelegate BEFORE serverDelegate, or LOOPBACK does not work

if ($Editor::isLoopback) {
	focusServer();
	Editor::initServer(28001);
	focusClient();
}

Editor::clientConnect(tern($Editor::isLoopback, "LOOPBACK:28001", "IP:localhost:28001"));
