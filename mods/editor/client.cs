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
	
	// Create MainWindow
	GUI::newWindow(MainWindow, "mainmenu.gui");

	// Create client delegate to connect
	newObject(clientDelegate, FearCSDelegate, false, "IP", 0, "IPX", 0, "LOOPBACK", 0);
	//translateMasters(); // TODO: ???
}

function Editor::clientConnect(%hostname) {
	purgeResources();
	connect(%hostname);
}
