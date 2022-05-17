// Server side of player connect sequence

// Server:
// Connection::onChildConnect()

// Client:
// Connection::onParentConnect()

// Client:
// function dataFinished()

function remoteDataFinished(%clientId) {
   Client::setDataFinished(%clientId);
   startGhosting(%clientId);
}

// Client:
// onClientGhostAlwaysDone()

function remoteCGADone(%clientId) {
  Client::setGuiMode(%clientId, 1);

  Server::onClientDoneLoading(%clientId);
}
