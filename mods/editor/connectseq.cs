// CONNECTION SEQUENCE

// Server
function Server::onClientConnect(%clientId) {
	echo("Server::onClientConnect ", %clientId);
}

// Client
function onConnection(%message) {
   echo("onConnection ", %message);
   if (%message == "Accepted") {
	   resetSimTime();
	   resetPlayDelegate();
   }
}
function onConnectionError(%server, %b, %c) {
  echo("onConnectionError ", %server, " ", %b, " ", %c);
}

function dataGotBlock(%name, %pct) {
	echo("dataGotBlock ", %name, " ", %pct);
}

function dataFinished() {
	echo("dataFinished");
	flushTextureCache(); // ??? RPGX does
	remoteEval(2048, dataFinished);
}

// Server
function remoteDataFinished(%clientId) {
	echo("remoteDataFinished ", %clientId);
	Client::setDataFinished(%clientId);
	startGhosting(%clientId);
}

// Client (done)
function onClientGhostAlwaysDone() {
	echo("onClientGhostAlwaysDone");
	// rebuildCommandMap()
	if(!isObject("commandGui"))
		loadObject("commandGui", "gui\\command.gui");

	flushTextureCache(); // ??? probably just needed for rebuildCommandMap
	purgeResources(true);
	remoteEval(2048, CGADone);
	
	if ($Editor::isLoopback) Editor::initMELoopback();
	else                     Editor::initMERemote();
}

// Server (done)
function onServerGhostAlwaysDone(%clientId) { echo("onServerGhostAlwaysDone ", %clientId); }
function remoteCGADone(%clientId) {
	echo("remoteCGADone ", %clientId);
	
	Editor::spawn(%clientId);
}