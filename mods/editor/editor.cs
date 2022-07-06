// TODO: embed controls in editor mod itself
// TODO: better barebones world with flat terrain
// TODO: get basic player movement working
// TODO: get editor camera and GUI attachment
requireMod(std);
requireMod(gui);

exec("editor\\connectseq.cs");
exec("editor\\clientevents.cs");
exec("editor\\serverevents.cs");

exec("editor\\gui.cs");
exec("editor\\newmission.cs");

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
	//exec("testmis.mis");
	newObject(MissionCleanup, SimGroup); // required: magic group name, used by DarkStar for projectiles, AI::Object, etc.
}

function Editor::preparePlayGui() {
	newObject(EntitesVolume, SimVolume, "baseres\\gui\\gui.vol");
	newObject("", IRCClient); // required: crash on load play.gui otherwise
	newObject(PlayChatMenu, ChatMenu, "Root Menu:"); // required: DarkStar calls setCMMode(PlayChatMenu, 0); on load play.gui
	newObject(CommandChatMenu, ChatMenu, "Command Menu"); // required: DarkStar calls setCMMode(PlayChatMenu, 0); on load play.gui
}

function Editor::initClient() {
	Editor::preparePlayGui();
	GUI::newWindow(MainWindow, "mainmenu.gui");

	//newObject(EntitesVolume, SimVolume, "baseres\\shapes\\Entities.vol");
	//base::refreshSearchPath();

	newObject(clientDelegate, FearCSDelegate, false, "IP", 0, "IPX", 0, "LOOPBACK", 0);
	//translateMasters(); // ???
}

function Editor::clientConnect(%hostname) {
	purgeResources();
	connect(%hostname);
}

function Editor::initMERemote() {
	
}

function Editor::initMELoopback() {
	exec("editor\\controls.cs");
	
	// ME init
	ME::Create(MainWindow);
	newObject(editCamera, EditCamera, "editor.sae");
	
	ME::SetGrabMask( ~( $ObjectType::Terrain | $ObjectType::Container | $ME::SimDefaultObject ) ); // all but SimTerrain, SimContainerObject, SimDefaultObject
	ME::SetDefaultPlaceMask( $ObjectType::Terrain | $ObjectType::Interior );

	// $ME::camera = editCamera; ???
	// $ME::Mod1 = false;		// control ???
	// $ME::Mod2 = false;		// shift ???
	// $ME::Mod3 = false;		// alt ???

	// TODO: init "special" $ME vars and call ME::GetConsoleOptions()

	$ME::MoveSensitivity 	= 0.2;
	$ME::RotateSensitivity 	= 0.02;
	
	// TED init (TODO: crashy because missing TED config funcs/vars probably)
	//Ted::initTed();
	//Ted::attachToTerrain();
	// TODO: other TED config funcs (and vars, but these may be simple mirrors of config funcs and unnecessary)
}

// Magic vars loaded during ME::GetConsoleOptions():
// (see https://github.com/AltimorTASDK/TribesRebirth/blob/1105fd0890c19c13f816b91e51b9cf0658ffc63c/program/code/FearMissionEditor.cpp#L1306)
// $ME::ShowEditObjects
// $ME::ShowGrabHandles
// $ME::SnapToGrid
// $ME::XGridSnap
// $ME::YGridSnap
// $ME::ZGridSnap
// $ME::ConstrainX
// $ME::ConstrainY
// $ME::ConstrainZ
// $ME::RotateXAxis
// $ME::RotateYAxis
// $ME::RotateZAxis
// $ME::RotationSnap
// $ME::SnapRotations
// $ME::DropAtCamera
// $ME::DropWithRotAtCamera
// $ME::DropBelowCamera
// $ME::DropToScreenCenter
// $ME::DropToSelectedObject
// $ME::UsePlaneMovement
// $ME::ObjectsSnapToTerrain

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
