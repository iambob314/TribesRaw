Module::begin("ClientConnection");
Module::addEventListener("ClientConnection", "Connection::onParentConnect");
Module::addEventListener("ClientConnection", "Connection::onChildConnect");

function ClientConnection::Connection::onParentConnect() {
  echo("RESET");
  resetSimTime();
  resetPlayDelegate();
}

function ClientConnection::Connection::onChildConnect() {}

Module::end();