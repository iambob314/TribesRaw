Module::begin("ServerConnection");

Module::addEventListener("ServerConnection", "Connection::onChildConnect");
Module::addEventListener("ServerConnection", "Connection::onParentConnect");

function ServerConnection::Connection::onChildConnect(%clientId) {
  echo("Client connected");
}

function ServerConnection::Connection::onParentConnect() {
  echo("Parent connection established");
}

Module::end();