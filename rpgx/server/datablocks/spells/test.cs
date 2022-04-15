RocketData DiscShell
{
   bulletShapeName = "discb.dts";
   explosionTag    = noExp;

   collisionRadius = 0.0;
   mass            = 2.0;

   damageClass      = 0;       // 0 impact, 1, radius
   damageValue      = 0.5;
   damageType       = $ExplosionDamageType;

   muzzleVelocity   = 1.0;
   terminalVelocity = 81.0;
   acceleration     = 5.0;

   totalTime        = 60.5;
   liveTime         = 80.0;

   lightRange       = 5.0;
   lightColor       = { 0.4, 0.4, 1.0 };

   inheritedVelocityScale = 0.5;

   // rocket specific
   trailType   = 1;
   trailLength = 15;
   trailWidth  = 0.3;
};


function Projectile::fireWithAcceleration(%projType, %trans, %shooter, %vel, %updateRate) {
  %vec = Matrix::subMatrix(%trans, 3, 4, 3, 1, 0, 1);
  %pos = Matrix::subMatrix(%trans, 3, 4, 3, 1, 0, 3);

  %proj = Projectile::fireAcceleratedProjectile(%projType, %pos, %vec, %shooter, %vel, 0);

  Projectile::schedUpdateAccel(%proj, %projType, %vec, %shooter, %vel, %time, %updateRate);
}

function Projectile::updateAccel(%proj, %projType, %vec, %shooter, %vel, %time, %updateRate) {
  if (!isObject(%proj)) return;

  %time += %updateRate;
  if (%time > %projType.totalTime) return;

  %pos = GameBase::getPosition(%proj);
  %vel = Vector::length(Item::getVelocity(%proj));
  if (getLOSInfo(%pos, Vector::add(%pos, Vector::mul(%vel, %updateRate)), -1)) {
    echo("BOOM: " @ getObjectType($los::object));
    return;
  }


  deleteObject(%proj);
  %proj = Projectile::fireAcceleratedProjectile(%projType, %pos, %vec, %shooter, %vel, %time);

  Projectile::schedUpdateAccel(%proj, %projType, %vec, %shooter, %vel, %time, %updateRate);
}

function Projectile::schedUpdateAccel(%proj, %projType, %vec, %shooter, %vel, %time, %updateRate) {
  schedule("Projectile::updateAccel(\""@%proj@"\",\""@%projType@"\",\""@%vec@"\",\""@%shooter@"\",\""@%vel@"\",\""@%time@"\",\""@%updateRate@"\");", %updateRate);
}

function Projectile::fireAcceleratedProjectile(%projType, %pos, %vec, %shooter, %vel, %timeElapsed) {
  %curSpeed = min(%projType.muzzleVelocity + %timeElapsed * %projType.acceleration, %projType.terminalVelocity);

  %vec = Vector::mul(%vec, %curSpeed / %projType.muzzleVelocity);

  return Projectile::spawnProjectile(%projType, "1 0 0 " @ %vec @ " 0 0 1 " @ %pos, %shooter, %vel);
}