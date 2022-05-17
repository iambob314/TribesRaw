//
// ----- RPGX server network theory -----
//
// Dangit I hate hiccups. Anyway, each RPGX server has 2 types of connection:
//  * A "parent" connection
//    - Exists in client space
//    - Created using the connect() function
//    - Object ID = 2048
//  * Some number of "child" connections, which
//    - Exist in the server space
//    - Created when other RPGX server make "parent" connections to this server
//    - Object IDs = [2049, inf)
//
// In this way a tree network is set up between all connected servers, and a parent-child relationship exists between any two connected
// servers.
//
// --- The connection process ---
//
// Suppose an RPGX server, say "Server A", successfully calls connect() with the IP address of another server, say "Server B". Between these two
// servers, Server A is designated as the "child", and Server B is called the "parent". When this connection occurs, several things happen:
//  * On Server A (the child):
//    - A connection object with ID 2048 is created
//    - onConnection() is called
//  * On Server B (the parent)
//    - A connection object with ID in the range [2049, inf) is created
//    - Server::onClientConnect is called, with the connection object as an argument
//
// Child 

Module::begin("ServerNetwork");

// --- EVENT LISTENER REGISTRATION ---
Module::addEventListener("ServerNetwork", "Connection::onParentConnect");
Module::addEventListener("ServerNetwork", "Connection::onChildConnect");

// --- SHARED SPACE ---
// $ServerNetwork::IDTable[%id] = %connObj;
$ServerNetwork::AbsoluteAddress = "/";

function ServerNetwork::Address::getTargetAddress(%addr) {
  %ind = String::findSubStr(%addr, "|");

  if (%ind != -1) return String::getSubStr(%addr, 0, %ind);
  else            return %addr;
}

function ServerNetwork::Address::getReturnAddress(%addr) {
  %ind = String::findSubStr(%addr, "|");

  if (%ind != -1) return String::getSubStr(%addr, %ind+1, 1024);
  else            return "";
}

function ServerNetwork::isAddressAbsolute(%addr) {
  return String::getSubStr(%addr, 0, 1) == "/";
}

function ServerNetwork::getConnectionName(%conn) {
  if (%conn == 2048) return "..";
  else               return Client::getName(%conn);
}

function ServerNetwork::RelativeAddress::getFirst(%addr) {
  %ind = String::findSubStr(%addr, "/");
  if (%ind == -1) return %addr;
  else return String::getSubStr(%addr, 0, %ind);
}

function ServerNetwork::RelativeAddress::popFirst(%addr) {
  %ind = String::findSubStr(%addr, "/");
  if (%ind == -1) return "";
  else return String::getSubStr(%addr, %ind + 1, 1<<16);
}

function ServerNetwork::RelativeAddress::pushFirst(%addr, %first) {
  if (%addr == "") return %first;
  else             return %first @ "/" @ %addr;
}

function ServerNetwork::AbsoluteAddress::isChildAddress(%addr) {
  return String::findSubStr(%addr, $ServerNetwork::AbsoluteAddress) == 0; // The address starts with our address
}

function ServerNetwork::AbsoluteAddress::getNextConnection(%addr) {
  %tail = String::getSubStr(%addr, String::length($ServerNetwork::AbsoluteAddress) + tern($ServerNetwork::AbsoluteAddress == "/", 0, 1), 1<<16);
  %ind = String::findSubStr(%tail, "/");

  if (%ind == -1) return %tail;
  else            return String::getSubStr(%tail, 0, %ind);
}

function ServerNetwork::enableNRemote(%conn) {
  %conn.allowNRemote = %conn;
}

function ServerNetwork::isNRemoteEnabled(%conn) {
  return %conn == 2048 || %conn.allowNRemote;
}

function ServerNetwork::isLoopback(%conn) {
  return String::findSubStr(Client::getTransportAddress(%conn), "LOOPBACK") != -1;
}

function nremoteEval(%addr, %func, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7) {
  if (ServerNetwork::isAddressAbsolute(%addr)) {
    if (ServerNetwork::Address::getReturnAddress(%addr) == "") %addr = %addr @ "|" @ $ServerNetwork::AbsoluteAddress;
    %targetAddr = ServerNetwork::Address::getTargetAddress(%addr);

    if (ServerNetwork::AbsoluteAddress::isChildAddress(%targetAddr)) {
      pushFocusServer();
      %next = ServerNetwork::AbsoluteAddress::getNextConnection(%targetAddr);
      %nextConn = Client::getByName(%next);
      echo("Sending absolute nremote to " @ %next);
      remoteEval(%nextConn, "NetworkEval", %addr, %func, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7);
    } else {
      pushFocusClient();
      echo("Sending absolute nremote up");
      remoteEval(2048, "NetworkEval", %addr, %func, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7);
    }
    popFocus();
  } else {
    %target = ServerNetwork::Address::getTargetAddress(%addr);
    %next = ServerNetwork::RelativeAddress::getFirst(%target);

    //echo("Calling remoteNetworkEval, target: ", %target, ", next: ", %next);

    if (%next == "..") {
      pushFocusClient();
      remoteEval(2048, "NetworkEval", %addr, %func, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7);
    } else {
      pushFocusServer();
      %nextConn = Client::getByName(%next);
      remoteEval(%nextConn, "NetworkEval", %addr, %func, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7);
    }
    popFocus();
  }
}

function remoteNetworkEval(%conn, %addr, %func, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7) {
  // Do translation for this jump

  if (!ServerNetwork::isNRemoteEnabled(%conn)) return;

  if (ServerNetwork::isAddressAbsolute(%addr)) {
    %targetAddr = ServerNetwork::Address::getTargetAddress(%addr);
    if (String::ICompare(%targetAddr, $ServerNetwork::AbsoluteAddress) == 0) { // Were HERE!
      pushFocusServer();
      echo("WERE HERE");
      invoke("nremote" @ %func, 9, ServerNetwork::Address::getReturnAddress(%addr), %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7);
      popFocus();
    } else {
      nremoteEval(%addr, %func, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7);
    }
  } else {
    %target = ServerNetwork::Address::getTargetAddress(%addr);
    %return = ServerNetwork::Address::getReturnAddress(%addr);

    %newTarget = ServerNetwork::RelativeAddress::popFirst(%target);
    %newReturn = ServerNetwork::RelativeAddress::pushFirst(%return, ServerNetwork::getConnectionName(%conn));

    //echo("Received remoteNetworkEval, addr: ", %addr, ", target: ", %target, ", return: ", %return, ", newTarget: ", %newTarget, ", newReturn: ", %newReturn);

    if (%newTarget == "") { // Were HERE!
      pushFocusServer();
      invoke("nremote" @ %func, 9, %newReturn, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7);
      popFocus();
    } else {
      %newAddr = %newTarget @ "|" @ %newReturn;
      nremoteEval(%newAddr, %func, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7);
    }
  }
}

function ServerNetwork::updateChildAbsoluteAddress(%conn) {
  if (%conn) {
    %childAddr = $ServerNetwork::AbsoluteAddress @ tern($ServerNetwork::AbsoluteAddress == "/", "", "/") @ Client::getName(%conn);
    echo("remoteEval: ", %conn, ",", "SN::updateAbsoluteAddress", ",",%childAddr);
    remoteEval(%conn, "SN::updateAbsoluteAddress", %childAddr);
  } else {
    for (%c = Client::getFirst(); %c != -1; %c = Client::getNext(%c)) {
      %childAddr = $ServerNetwork::AbsoluteAddress @ tern($ServerNetwork::AbsoluteAddress == "/", "", "/") @ Client::getName(%c);
      echo("remoteEval: ", %c, ",", "SN::updateAbsoluteAddress", ",",%childAddr);
      remoteEval(%c, "SN::updateAbsoluteAddress", %childAddr);
    }
  }
}

// --- SERVER SPACE ---

function ServerNetwork::Connection::onChildConnect(%conn) {
  if (!ServerNetwork::isLoopback(%conn))
    ServerNetwork::updateChildAbsoluteAddress(%conn);
}

function ServerNetwork::Connection::onParentConnect() {}

// --- CLIENT SPACE ---

function remoteSN::updateAbsoluteAddress(%conn, %address) {
  if (%conn != 2048) return;

  pushFocusServer();

  $ServerNetwork::AbsoluteAddress = %address;
  ServerNetwork::updateChildAbsoluteAddress();

  popFocus();
}

// --- INIT ---

Module::end();