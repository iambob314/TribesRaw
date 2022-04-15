ExplosionData FirestormExp {
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

BulletData FirestormFlame {
   bulletShapeName    = "plasmatrail.dts";
   explosionTag       = FirestormExp;
   mass               = 0.05;

   damageClass        = 1;       // 0 impact, 1, radius
   damageValue        = 0.1;
   damageType         = $PlasmaDamageType;

   explosionRadius    = 5;

   muzzleVelocity     = 150.0;
   totalTime          = 6.0;
   liveTime           = 4.0;
   isVisible          = True;

   rotationPeriod = 0.05;

//   soundId = SoundJetHeavy;
};



$Spell::Firestorm::NUM_TWISTS = 24;
$Spell::Firestorm::TOTAL_TWIST_ANGLE = 4*$PI;
$Spell::Firestorm::SPIRAL_ANGLE = ($PI/12)/24;
$Spell::Firestorm::TWIST_ANGLE = $Spell::Firestorm::TOTAL_TWIST_ANGLE / $Spell::Firestorm::NUM_TWISTS;

$Spell::Firestorm::SPIRAL_MATRIX = Matrix::rotX($Spell::Firestorm::SPIRAL_ANGLE);
$Spell::Firestorm::TWIST_MATRIX = Matrix::rotY($Spell::Firestorm::TWIST_ANGLE);

function Firestorm::doFirestorm(%player) {
  %trans = GameBase::getMuzzleTransform(%player);
  %pos = Matrix::subMatrix(%trans, 3, 4, 3, 1, 0, 3);
  %transMat = Matrix::subMatrix(%trans, 3, 4, 3, 3);

  %twistMat = %spiralMat = %firestormTrans = Matrix::identity(3, 3);
  for (%i = 0; %i < $Spell::Firestorm::NUM_TWISTS; %i++) {
    if (%i > 0) {
      %firestormTrans = Matrix::mul($Spell::Firestorm::TWIST_MATRIX, 3, 3, %firestormTrans, 3, 3);
      %firestormTrans = Matrix::mul(%firestormTrans, 3, 3, $Spell::Firestorm::SPIRAL_MATRIX, 3, 3);
    }

    %curTransMat = Matrix::mul(%transMat, 3, 3, %firestormTrans, 3, 3);
    %flipTransMat = Matrix::mul(%transMat, 3, 3, "-1 0 0 0 1 0 0 0 -1", 3, 3);
    %flipTransMat = Matrix::mul(%flipTransMat, 3, 3, %firestormTrans, 3, 3);

    schedule("Firestorm::doFire(\"" @ %curTransMat @ " " @ %pos @ "\", " @ %player @ ");" @
             "Firestorm::doFire(\"" @ %flipTransMat @ " " @ %pos @ "\", " @ %player @ ");", %i * 0.03125);
  }
}

function Firestorm::doFire(%trans, %player) {
  Projectile::spawnProjectile(FirestormFlame, %trans, %player, 0);
}

////////////////////////////////////////////////////////////////////////////////////////////////////////

ItemImageData FirestormSpellImage {
  shapeFile  = "plasmabolt";
  mountPoint = 0;

  weaponType = 0; // Single Shot
  reloadTime = 0;
  fireTime = 3;

  minEnergy = 0;
  maxEnergy = 0;

  accuFire = true;

//  sfxFire = SoundFireBlaster;
//  sfxActivate = SoundPickUpWeapon;
};

ItemData FirestormSpell {
  shapeFile  = "plasmabolt";
  imageType = FirestormSpellImage;
};

function FirestormSpellImage::onFire(%player, %slot) {
  Firestorm::doFirestorm(%player);
}