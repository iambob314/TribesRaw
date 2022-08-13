// CONNECTION SEQUENCE

// Server
function Server::onClientConnect(%clientId) {}

// Client
function onConnection(%message) {
   if (%message == "Accepted") {
	   resetSimTime();
	   resetPlayDelegate();
   }
}
function onConnectionError(%server, %b, %c) {
  echo("onConnectionError ", %server, " ", %b, " ", %c);
}

function dataGotBlock(%name, %pct) {}

function dataFinished() {
	flushTextureCache(); // TODO: needed?
	remoteEval(2048, dataFinished);
}

// Server
function remoteDataFinished(%clientId) {
	Client::setDataFinished(%clientId);
	startGhosting(%clientId);
}

// Client (done)
function onClientGhostAlwaysDone() {
	// rebuildCommandMap() // TODO: needed?
	if(!isObject("commandGui"))
		loadObject("commandGui", "gui\\command.gui");

	flushTextureCache(); // TODO: needed? probably for rebuildCommandMap
	purgeResources(true);
	remoteEval(2048, CGADone);
	
	if ($Editor::isLoopback) Editor::initMELoopback();
	else                     Editor::initMERemote();
}

// Server (done)
function onServerGhostAlwaysDone(%clientId) { echo("onServerGhostAlwaysDone ", %clientId); }
function remoteCGADone(%clientId) {
	Editor::spawn(%clientId);
}

// Client-side events
function onTeamAdd(%team, %name) {}

function onClientMessage(%clientId, %msg) { echo("onClientMessage ", %clientId, " ", %msg); return true; }
function onClientJoin(%clientId) { echo("onClientJoin ", %clientId); }
function onClientDrop(%clientId) { echo("onClientDrop ", %clientId); }
function onClientChangeTeam(%clientId, %team) { echo("onClientChangeTeam ", %clientId, " ", %team);}

// Client <- Server's Client::setGuiMode(%clientId, 1)
function loadPlayGui() {
	if (File::FindFirst("play.gui") != "")
		GuiLoadContentCtrl(MainWindow, "play.gui");
	else
		GuiLoadContentCtrl(MainWindow, "gui\\play.gui");
}
