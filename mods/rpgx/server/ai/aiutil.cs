LaserData RedLaser {
   laserBitmapName   = "repairAdd.bmp";
   hitName           = "breath.dts";

   damageConversion  = 0.0;
   baseDamageType    = 0;

   beamTime = 10000;

   lightRange        = 0;
   lightColor        = { 0,0,0 };

   detachFromShooter = true;
};

LaserData BlueLaser {
   laserBitmapName   = "flyerflame1.bmp";
   hitName           = "breath.dts";

   damageConversion  = 0.0;
   baseDamageType    = 0;

   beamTime = 10000;

   lightRange        = 0;
   lightColor        = { 0,0,0 };

   detachFromShooter = true;
};

//////////////////////
// Utility functions
//////////////////////

function AI::getVar(%aiName, %varName) { return $AI::[%aiName @ "::" @ %varName]; }

function SuperAI::getVar(%aiName, %varName) { return $SuperAI::AIData[%aiName, %varName]; }
function SuperAI::setVar(%aiName, %varName, %value) { return $SuperAI::AIData[%aiName, %varName] = %value; }
function SuperAI::incVar(%aiName, %varname, %amt) { return $SuperAI::AIData[%aiName, %varName] += tern(%amt != "", %amt, 1); }
function SuperAI::decVar(%aiName, %varname, %amt) { return $SuperAI::AIData[%aiName, %varName] -= tern(%amt != "", %amt, 1); }

function SuperAI::getStateVar(%aiName, %varName) { return SuperAI::getStateObject(%aiName).vars[%varName]; }
function SuperAI::setStateVar(%aiName, %varName, %value) { SuperAI::getStateObject(%aiName).vars[%varName] = %value; return %value; }
function SuperAI::incStateVar(%aiName, %varName, %amt) { SuperAI::getStateObject(%aiName).vars[%varName] += tern(%amt != "", %amt, 1); return SuperAI::getStateObject(%aiName).vars[%varName]; }
function SuperAI::decStateVar(%aiName, %varName, %amt) { SuperAI::getStateObject(%aiName).vars[%varName] -= tern(%amt != "", %amt, 1); return SuperAI::getStateObject(%aiName).vars[%varName]; }

function SuperAI::getPosition(%aiName) { return GameBase::getPosition(SuperAI::getOwnedObject(%aiName)); }
function SuperAI::setPosition(%aiName, %pos) { return GameBase::setPosition(SuperAI::getOwnedObject(%aiName), %pos); }

function SuperAI::getRotation(%aiName) { return GameBase::getRotation(SuperAI::getOwnedObject(%aiName)); }
function SuperAI::setRotation(%aiName, %rot) { return GameBase::setRotation(SuperAI::getOwnedObject(%aiName), %rot); }

function SuperAI::getOwnedObject(%aiName) { return Client::getOwnedObject(AI::getID(%aiName)); }

// Inefficient if using on many objects with one AI; write custom code instead
function SuperAI::canAISee(%aiName, %object, %range) {
  %obj = SuperAI::getOwnedObject(%aiName);
  %trans = GameBase::getMuzzleTransform(%obj);
  %muzX = Matrix::subMatrix(%trans, 3, 4, 3, 1, 0, 0);
  %muzY = Matrix::subMatrix(%trans, 3, 4, 3, 1, 0, 1);
  %muzZ = Matrix::subMatrix(%trans, 3, 4, 3, 1, 0, 2);
  %pos = Vector::add(GameBase::getPosition(%obj), "0 0 1.8"); // 1.8 is the approx. height of the head
  %range = AI::getVar(%aiName, "spotDist"); // Native AI variable

  %pos2 = getBoxCenter(%object);
  %vec = Vector::sub(%pos2, %pos);

  if (Vector::dot(%vec, %vec) < %range*%range) {
    %viewVec = Vector::dot(%vec, %muzX) @ " " @ Vector::dot(%vec, %muzY) @ " " @ Vector::dot(%vec, %muzZ);
    %rot = Vector::getRot(%viewVec);
    if (GameBase::getLOSInfo(%obj, %range, %rot)) {
      if (Vector::getDistance(%pos, $los::position) >= Vector::length(%vec)) return true;
    }
  }

  return false;
}

function SuperAI::jump(%aiName) {
  %id = Client::getOwnedObject(AI::getID(%aiName));

  if (Player::getLastContactCount(%id) != 0) return;

  %armor = Player::getArmor(%id);

  Player::applyImpulse(%id, "0 0 " @ %armor.jumpImpulse);
  Player::setAnimation(%id, 6);
}

function SuperAI::getName(%id) { return %id.aiName; }

function SuperAI::isDead(%aiName) {
  %obj = SuperAI::getVar(%aiName, "ownedObject");

  if (!isObject(%obj)) return true;
  return Player::isDead(%obj);
}


