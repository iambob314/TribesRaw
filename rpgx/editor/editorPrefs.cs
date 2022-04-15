// ME VARS
$ME::varsInitialized = true;

$ME::ShowEditObjects = true;
$ME::ShowGrabHandles = true;
$ME::SnapToGrid = false;

$ME::XGridSnap = 1;
$ME::YGridSnap = 1;
$ME::ZGridSnap = 0.001;

$ME::ConstrainX = false;
$ME::ConstrainY = false;
$ME::ConstrainZ = false;

$ME::RotateXAxis = false;
$ME::RotateYAxis = false;
$ME::RotateZAxis = true;
$ME::RotationSnap = 90.0;
$ME::SnapRotations = false;

$ME::DropAtCamera = false;
$ME::DropWithRotAtCamera = false;
$ME::DropBelowCamera = false;
$ME::DropToScreenCenter = true;
$ME::DropToSelectedObject = false;

$ME::ObjectsSnapToTerrain = false;
$ME::UsePlaneMovement = false;

$ME::LightQuick = false;
$ME::terrainSeed = 0;
$ME::genRandSeed = true;
$ME::newVolFile = "";

// sensitivity
$ME::MoveSensitivity 	= 0.2;
$ME::RotateSensitivity 	= 0.02;

$ME::loaded = True;

// TED VARS
$TED::flagCorner                 = false;
$TED::flagEdit                   = false;
$TED::flagEmpty1                 = false;
$TED::flagEmpty2                 = false;
$TED::flagEmpty3                 = false;
$TED::flagFlipX                  = false;
$TED::flagFlipY                  = false;
$TED::flagRotate                 = false;
   
// paste
$TED::pasteMaterial              = true;
$TED::pasteHeight                = true;
    
// values
$TED::heightVal                  = 50;
$TED::adjustVal                  = 5;
$TED::scaleVal                   = 1;
$TED::pinDetailVal               = 0;
$TED::pinMaxVal                  = 12;
$TED::matIndexVal                = 0;
$TED::smoothVal                  = 0.5;

// display
$TED::selectionDisplayFrame      = true;
$TED::selectionDisplayOutline    = false;
$TED::selectionDisplayFill       = false;
$TED::selectionColorFrame        = 3;
$TED::selectionColorFill         = 4;
$TED::hilightDisplayFrame        = true;
$TED::hilightDisplayOutline      = false;
$TED::hilightDisplayFill         = false;
$TED::hilightColorFrame          = 2;
$TED::hilightColorFill           = 8;
$TED::shadowDisplayFrame         = true;
$TED::shadowDisplayOutline       = false;
$TED::shadowDisplayFill          = false;
$TED::shadowColorFrame           = 6;
$TED::shadowColorFill            = 12;
$TED::blockDisplayOutline        = false;

// misc
$TED::brushSnap                  = true;    
$TED::brushFeather               = true;

// system
$TED::castInteriorShadows        = true;
$TED::success                    = false;
$TED::diskName                   = "";
$TED::currFile                   = "";
$TED::currPath                   = "";
$TED::terrainNameChange          = false;
$TED::terrainExt                 = "ted";
$TED::editValue                  = "";

// Main window
$TED::mainWindow = "MainWindow";
