// Two events are registered by connection.cs:
//
// Connection::onChildConnect(%clientId);
// Connection::onParentConnect();
//
// Connection::onChildConnect will be called in serverspace, and Connection::onParentConnect will
// be called in clientspace
//

Event::registerEvent("Connection::onParentConnect");
Event::registerEvent("Connection::onChildConnect");

function Server::onClientConnect(%clientId) {
  ServerNetwork::onChildConnect(%clientId);
  Connection::onChildConnect(%clientId);

  Event::fireEvent("Connection::onChildConnect", %clientId);
}

function onConnection(%message) {
   echo("Connection: ", %message);
   if (%message == "Accepted") {
      ServerNetwork::onParentConnect();
      Connection::onParentConnect();

      Event::fireEvent("Connection::onParentConnect");
   }
}

function onConnectionError(%server, %b, %c) {
  echo("Connection error (", %b, "): ", %c);
}



function Connection::openParentConnection(%addr, %name) {
  pushFocusClient();
  if (%name != "") $PCFG::Name = %name;
  connect(%addr);
  popFocus();
}
