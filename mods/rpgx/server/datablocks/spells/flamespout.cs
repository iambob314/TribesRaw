ExplosionData FlamespoutExp {
   shapeName = "plasmaex.dts";
//   soundId   = explosion4;

   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 4.0;

   timeZero = 0.200;
   timeOne  = 0.950;

   colors[0]  = { 1.0, 1.0,  0.0 };
   colors[1]  = { 1.0, 1.0, 0.75 };
   colors[2]  = { 1.0, 1.0, 0.75 };
   radFactors = { 0.375, 1.0, 0.9 };
};

GrenadeData FlamespoutFlame {
   bulletShapeName    = "plasmabolt.dts";
   explosionTag       = FlamespoutExp;
   collideWithOwner   = True;
   ownerGraceMS       = 250;
   collisionRadius    = 0.2;
   mass               = 1.0;
   elasticity         = 0;

   damageClass        = 1;       // 0 impact, 1, radius
   damageValue        = 0.1;
   damageType         = $PlasmaDamageType;

   explosionRadius    = 5;

   maxLevelFlightDist = 75;
   totalTime          = 1.0;    // special meaning for grenades...
   liveTime           = 0.05;
   projSpecialTime    = 0.05;

   inheritedVelocityScale = 1;

   smokeName              = "blueflame2.dts";
};

RocketData FlamespoutFlame2 {
   bulletShapeName = "blueflame2.dts";
   explosionTag    = FlamespoutExp;

   collisionRadius = 0.0;
   mass            = 2.0;

   damageClass      = 1;       // 0 impact, 1, radius
   damageValue      = 0.1;
   damageType       = $PlasmaDamageType;

   explosionRadius  = 5;

   muzzleVelocity   = 100.0;
   terminalVelocity = 100.0;

   totalTime        = 0.35;
   liveTime         = 0.35;

   accuFire = true;
   inheritedVelocityScale = 1;

   // rocket specific
   trailType   = 2;                // smoke trail
   trailString = "blueflame2.dts";
   smokeDist   = 3.5;
};

BulletData FlamespoutFlame3 {
   bulletShapeName    = "blueflame2.dts";
   explosionTag       = FlamespoutExp;

   damageClass      = 1;       // 0 impact, 1, radius
   damageValue      = 0.1;
   damageType       = $PlasmaDamageType;

   explosionRadius  = 5;

//   aimDeflection      = 0.005;
   muzzleVelocity     = 100.0;
   totalTime          = 0.35;
   inheritedVelocityScale = 1.0;
};

function testFlamespout() { focusServer(); Flamespout::doFlamespout(Client::getownedObject(2049)); }

$Spell::Flamespout::NUM_FLAMES = 30;
function Flamespout::doFlamespout(%player) {
  %trans = GameBase::getMuzzleTransform(%player);
  %pos = Matrix::subMatrix(%trans, 3, 4, 3, 1, 0, 3);
  %transMat = Matrix::subMatrix(%trans, 3, 4, 3, 3);

  for (%i = 0; %i < $Spell::Flamespout::NUM_FLAMES; %i++) {
    %fireTrans = Matrix::mul(Matrix::rotZ($PI/36*rand(-1, 1)), 3, 3, Matrix::rotX($PI/36*rand(-1, 1)), 3, 3);
    %fireTrans = Matrix::mul(%transMat, 3, 3, %fireTrans, 3, 3);

    schedule("Flamespout::doFire(\"" @ %fireTrans @ " " @ %pos @ "\", " @ %player @ ");", %i * 0.1);
  }
}

function Flamespout::doFire(%trans, %player) {
  Projectile::spawnProjectile(FlamespoutFlame, %trans, %player, 0);
}

////////////////////////////////////////////////////////////////////////////////////////////////////////

ItemImageData FlamespoutSpellImage {
  shapeFile  = "plasmatrail";
  mountPoint = 0;

  weaponType = 0; // Single Shot
  reloadTime = 0;
  fireTime = 3;

  minEnergy = 0;
  maxEnergy = 0;

  accuFire = true;

  mountOffset = "-0.1 0 -0.3";
//  sfxFire = SoundFireBlaster;
//  sfxActivate = SoundPickUpWeapon;
};

ItemData FlamespoutSpell {
  shapeFile  = "plasmatrail";
  imageType = FlamespoutSpellImage;
};

function FlamespoutSpellImage::onFire(%player, %slot) {
  Flamespout::doFlamespout(%player);
}