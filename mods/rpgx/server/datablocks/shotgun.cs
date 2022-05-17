ExplosionData bulletExp0 {
   shapeName = "chainspk.dts";
//   soundId   = ricochet1;

   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 1.0;

   timeZero = 0.100;
   timeOne  = 0.900;

   colors[0]  = { 0.0, 0.0, 0.0 };
   colors[1]  = { 1.0, 1.0, 1.0 };
   colors[2]  = { 1.0, 1.0, 1.0 };
   radFactors = { 0.0, 1.0, 0.0 };

   shiftPosition = True;
};

ExplosionData bulletExp1 {
   shapeName = "chainspk.dts";
//   soundId   = ricochet2;

   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 1.0;

   timeZero = 0.100;
   timeOne  = 0.900;

   colors[0]  = { 0.0, 0.0, 0.0 };
   colors[1]  = { 1.0, 1.0, 0.5 };
   colors[2]  = { 1.0, 1.0, 0.5 };
   radFactors = { 0.0, 1.0, 0.0 };

   shiftPosition = True;
};

ExplosionData bulletExp2 {
   shapeName = "chainspk.dts";
//   soundId   = ricochet3;

   faceCamera = true;
   randomSpin = true;
   hasLight   = true;
   lightRange = 1.0;

   timeZero = 0.100;
   timeOne  = 0.900;

   colors[0]  = { 0.0,  0.0, 0.0 };
   colors[1]  = { 0.75, 1.0, 1.0 };
   colors[2]  = { 0.75, 1.0, 1.0 };
   radFactors = { 0.0, 1.0, 0.0 };

   shiftPosition = True;
};

SoundProfileData Profile3dNear
{
   baseVolume = 0;
   minDistance = 5.0;
   maxDistance = 40.0;
   flags = SFX_IS_HARDWARE_3D;
};

SoundData SoundFireShotgun
{
   wavFileName = "mine_exp.wav";
   profile = Profile3dNear;
};

BulletData ShotgunBullet {
   bulletShapeName    = "bullet.dts";
   explosionTag       = bulletExp0;
   expRandCycle       = 3;

   damageClass        = 0;                 // 0 = impact, 1 = radius
   damageValue        = 0.1;
   damageType         = $PlasmaDamageType;

   muzzleVelocity     = 400.0;
   totalTime          = 0.5;
   liveTime           = 0.5;
   lightRange         = 3.0;
   lightColor         = { 1, 1, 0 };
   inheritedVelocityScale = 0.3;
   isVisible          = False;

   aimDeflection      = 0.02;
   tracerPercentage   = 1.0;
   tracerLength       = 30;

   //soundId = SoundJetLight;
};

ItemImageData ShotgunImage {
   shapeFile  = "energygun";
	mountPoint = 0;

	weaponType = 0; // Single Shot
	reloadTime = 0;
	fireTime = 0.75;

	maxEnergy = 0;
	minEnergy = 0;

	accuFire = true;

	sfxFire = SoundFireShotgun;
	//sfxActivate = SoundPickUpWeapon;
};

ItemData Shotgun {
	description = "Shotgun";
	shapeFile  = "energygun";
	hudIcon = "Shotgun";
	shadowDetailMask = 4;
	imageType = ShotgunImage;
	showWeaponBar = true;
};

function ShotgunImage::onFire(%this, %slot) {
  echo(Q);
  for (%i = 0; %i < 8; %i++) {
    Projectile::spawnProjectile(ShotgunBullet, GameBase::getMuzzleTransform(%this), %this, 0);
  }
}
