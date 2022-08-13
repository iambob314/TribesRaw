// Basic map editor, intended to be used standalone (no other client/server mod).
//
// TODO:
// * Client-side map editor
requireMod(std);
requireMod(gui);

exec("editor\\connectseq.cs");
exec("editor\\server\\server.cs");

exec("editor\\actions.cs");
exec("editor\\me.cs");

exec("editor\\gui\\gui.cs");
exec("editor\\shapelist.cs");

exec("editor\\editorcontrols.cs");
exec("editor\\playercontrols.cs");

function checkMasterTranslation() {} // called periodically after newObject(..., FearCSDelegate, true); TODO: implement for real

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

function Editor::initMERemote() {}

function Editor::initMELoopback() {
	// ME + editCamera init
	ME::Create(MainWindow);

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
