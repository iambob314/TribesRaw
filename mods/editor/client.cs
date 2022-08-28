// Common client-side setup/functions, for loopback and remote

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
	//translateMasters(); // TODO: ???
}

function Editor::clientConnect(%hostname) {
	purgeResources();
	connect(%hostname);
}

// Editor::focusInput focuses client/GUI input on one of three modes:
// * Player: player delegate
// * ME: mission editor
// * TED: terrain editor
// For non-loopback client, only Player is valid.
function Editor::focusInput(%m) {
	assert($Editor::validInputMode[%m, $Editor::isLoopback], "bad mode '" @ %m @ "'");
	
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
// $Editor::validInputMode[%mode, %isLoopback] = true iff valid
$Editor::validInputMode[Player, false] = true;
$Editor::validInputMode[Player, true ] = true;
$Editor::validInputMode[ME,     true ] = true;
$Editor::validInputMode[TED,    true ] = true;

function setFocus(%thing, %on) { if (%on) focus(%thing); else unfocus(%thing); }
