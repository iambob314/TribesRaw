ME::Create(MainWindow);

// all but SimTerrain and SimContainerObject and SimDefaultObject
ME::SetGrabMask(~($ME::SimTerrain | $ME::SimContainerObject | $ME::SimDefaultObject));

// place masks
ME::SetDefaultPlaceMask($ME::SimTerrain | $ME::SimInteriorObject);

// modifiers
$ME::Mod1 = false; // control
$ME::Mod2 = false; // shift
$ME::Mod3 = false; // alt

$ME::camera = EditCamera;
$ME::loaded = true;

ME::GetConsoleOptions();
