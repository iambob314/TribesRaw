// Misc.

function remoteScoresOn(%clientId) {
  if ($tehObject == "") {
    $tehObjectFile = File::findFirst("*.dis");
  } else {
    deleteObject($tehObject);
    $tehObjectFile = File::findNext("*.dis");
  }
  $tehObject = newObject("", InteriorShape, $tehObjectFile);
  GameBase::setPosition($tehObject, "0 0 50");
  Client::sendMessage(2049, 1, $tehObjectFile);
}