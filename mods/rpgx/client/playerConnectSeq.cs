// Client side of player connect sequence

// Server:
// Connection::onChildConnect()

// Client:
// Connection::onParentConnect()

function dataFinished() {
  echo("FINISHED!");

  remoteEval(2048, dataFinished);
  flushTextureCache();
}

// Server:
// remoteDataFinished()

function onClientGhostAlwaysDone() {
   // preload the commander gui if it's not already loaded
   if(!isObject("commandGui"))
      loadObject("commandGui", "gui\\command.gui");

   flushTextureCache();
   purgeResources(true);

  remoteEval(2048, "CGADone");
}

// Server:
// remoteCGADone()
