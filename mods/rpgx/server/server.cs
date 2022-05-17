exec2("server\\connection.cs");
exec2("server\\remoteInterface.cs");
exec2("server\\clientspace.cs");
exec2("server\\playerConnectSeq.cs");

exec2("server\\player.cs");

function createServer(%port) {
   purgeResources();

   pushFocusServer();

   $Server::port = 28001;
   if (%port != "") $Server::port = %port;

   newObject(serverDelegate, FearCSDelegate, true, "IP", $Server::Port, "IPX", $Server::Port, "LOOPBACK", $Server::Port);

   exec2("server\\datablocks\\datablocks.cs");
   exec2("server\\ai\\ai.cs");

   preloadServerDataBlocks();

   resetPlayerManager();
   resetGhostManagers();
   resetSimTime();
   newObject(ConsoleScheduler, SimConsoleScheduler);

   newObject(MissionCleanup, SimGroup);

   purgeResources(true);

   popFocus();
}

function Server::onClientDoneLoading(%clientId) {
  %player = newObject("", Player, HumanMale);

  Client::setOwnedObject(%clientId, %player);
  Client::setControlObject(%clientId, %player);

  GameBase::setPosition(%player, "0 0 200");
  GameBase::setTeam(%player, 0);
}

function onServerGhostAlwaysDone() {}



function Server::loadMission(%mission) {
  setInstantGroup(0);
  exec2("missions\\" @ %mission @ ".mis");
  newObject("", TeamGroup);
}

// --- INIT ---
