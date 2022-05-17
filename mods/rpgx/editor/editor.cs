// Unless an explicit focus call is made, all functions run under client focus

exec2("server\\server.cs");
exec2("client\\client.cs"); // clientspace.cs and connection.cs from the client module overwrite those in the server module

pushFocusClient();

exec2("editor\\editorConstants.cs");
exec2("editor\\editorPrefs.cs");

exec2("editor\\controls\\editorCameraControls.cs");
exec2("editor\\controls\\globalControls.cs");

exec2("editor\\missionObjectList.cs");
exec2("editor\\createObjectList.cs");

exec2("editor\\editModes.cs");
exec2("editor\\createObjectMode.cs");
exec2("editor\\tedMode.cs");
exec2("editor\\gameMode.cs");

exec2("editor\\playerEditorServer.cs");

popFocus();

// Called in client when a connection is made to a server
function Connection::onParentConnect() {
  resetSimTime();
  resetPlayDelegate();
  //schedule("ME::init();", 0);
}

function Server::onClientDoneLoading(%clientId) {
  %player = newObject("", Player, HumanMale);

  Client::setOwnedObject(%clientId, %player);
  Client::setControlObject(%clientId, %player);

  GameBase::setPosition(%player, "0 0 200");
  GameBase::setTeam(%player, 0);

  if ($viewObject != "") {
    newObject("", SimVolume, $viewObject @ ".div");
    %obj = newObject("", InteriorShape, $viewObject @ ".dis");
    GameBase::setPosition(%obj, "0 0 75");
  }

  if (ServerNetwork::isLoopback(%clientId)) {
    remoteEval(%clientId, "loadME");
    remoteEval(%clientId, "loadPlayerEditor");
  }

  echo("DONE LOADING");
}

function remoteLoadME(%conn) {
  ME::init();
}

function ME::init() {
  if ($ME::init) return;

  pushFocusClient();

  newObject(EditCamera, EditCamera, "editorCamera.sae");
  pushActionMap("globalMap.sae");

  // --- ME INIT ---
  exec2("editor\\meInit.cs");
  // --- TED INIT ---
  exec2("editor\\tedInit.cs");

  $ME::init = true;
  $ME::CurrentMode = $ME::GAME_MODE;
  ME::enterGameMode();

  popFocus();
}

function ME::SwitchModes(%newMode) {
  if (%newMode == $ME::CurrentMode) return;
  if (!$ME::init) ME::init();

  if ($ME::CurrentMode == $ME::GAME_MODE) ME::exitGameMode();
  else if ($ME::CurrentMode == $ME::CREATE_OBJECT_MODE) ME::exitCreateObjectMode();
  else if ($ME::CurrentMode == $ME::TED_MODE) ME::exitTedMode();

  $ME::CurrentMode = %newMode;

  if ($ME::CurrentMode == $ME::GAME_MODE) ME::enterGameMode();
  else if ($ME::CurrentMode == $ME::CREATE_OBJECT_MODE) ME::enterCreateObjectMode();
  else if ($ME::CurrentMode == $ME::TED_MODE) ME::enterTedMode();
  else echo("Unknown ME mode: " @ %newMode);
}

function loadEditorToolWindow() {
  if (isObject("EditorTools")) return;
  newObject("EditorTools", SimGui::Canvas, "Editor Tools", "320 480", True);

  windowsKeyboardEnable(EditorTools);
  windowsMouseEnable(EditorTools);
  cursorOn(EditorTools);

  GuiLoadContentCtrl(EditorTools, "editortools.gui");
  //GuiEditMode(EditorTools);
  //GuiInspect(EditorTools);
}

//
// Now start the editor
//

//focusClient();
//ME::init();

pushFocusServer();
createServer(28001);
Server::loadMission($editMission);
popFocus();

//pushFocusClient();
focusClient();
connect("LOOPBACK:28001");
//popFocus();
