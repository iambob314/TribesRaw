// Object types, used by Map Editor, containerBoxFillSet, etc.
$ObjectType::Default		= 1 << 0;
$ObjectType::Terrain		= 1 << 1;
$ObjectType::Interior		= 1 << 2;
$ObjectType::Camera			= 1 << 3;
$ObjectType::MissionObject	= 1 << 4;
$ObjectType::Shape			= 1 << 5;
$ObjectType::Container		= 1 << 6;
$ObjectType::Player			= 1 << 7;
$ObjectType::Projectile		= 1 << 8;
$ObjectType::Vehicle 		= 1 << 9; // NOTE: Flier object -> $ObjectType::FearVehicle, not this one

// fear specific
$ObjectType::FearItem			= 1 << 31;
$ObjectType::FearPlayer    		= 1 << 30; // NOTE: Player object -> $ObjectType::Player, not this one
$ObjectType::FearTeleport  		= 1 << 29;
$ObjectType::FearCorpse    		= 1 << 28;
$ObjectType::FearStation       	= 1 << 27;
$ObjectType::FearMine      		= 1 << 26;
$ObjectType::FearMoveable  		= 1 << 25; // NOTE: Moveable object -> $ObjectType::FearMoveableBase, not this one
$ObjectType::FearVehicle   		= 1 << 24;
$ObjectType::FearStatic        	= 1 << 23;
$ObjectType::FearMoveableBase  	= 1 << 22;
$ObjectType::FearItem          	= 1 << 21;
$ObjectType::FearMarkerO        = 1 << 20;
$ObjectType::FearAIObject		= 1 << 19;
