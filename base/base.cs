// Barebones bootstrapping for Tribes

function Game::endFrame() {} // it's *really* spammy until we do this
function exec2(%file) { exec(%file); } // for safely executing a file without allowing it to clobber local variables

// init is called at the end of this file (defined at top here for visibility)
function init() {
	// Core console variables
	$Console::logMode = 1;
	$Console::printLevel = 1;
	$Console::prompt = "$ ";
	$Console::history = 999;
	$Console::logBufferEnabled = true;
	$Console::lastLineTimeout = 0;
	$Console::GFXFont = "interface.pft";
	
	// Parse command line
	base::parseCArgv();
	if ($dedicated) {
		$WinConsoleEnabled = true;
	}

	// Set up search path
	EvalSearchPath();
	
	// Load all mods specified by command line args
	%modsToLoad = $modList;
	for (%i = 0; (%modname = getWord(%modsToLoad, %i)) != -1; %i++) RequireMod(%modname);
	
	echo("Tribes initialized");
}

// parseCArgv parses command line args into mods list and dedicated server mode/mission
// "other" args go into new var $argv[%i]
function base::parseCArgv() {
	%outIdx = 0;
	for (%i = 1; $cargv[%i] != ""; %i++) {
		if ($cargv[%i] == "-mod") {
			%mod = $cargv[%i++];
			$modList = %mod @ " " @ $modList;
		} else if ($cargv[%i] == "-dedicated") {
			$dedicated = true;
			$dedicatedMission = $cargv[%i++];
		} else {
			$argv[%outIdx++ - 1] = $cargv[%i]; // extract unparsed args for mods to handle
		}
	}
}

// EvalSearchPath just adds the top-level directories to the path. We keep this function
// just to refresh for newly-appeared files.
function EvalSearchPath() {
	$ConsoleWorld::DefaultSearchPath = "base;config;mods;recordings;temp";
	echo("Refreshed search path: ", $ConsoleWorld::DefaultSearchPath);
}

//
// RequireMod(%modname) - load mod %modname
//
// Loading mod %modname:
// 1) creates %modname\\%modname.vol as SimVolume, if it exists
// 2) executes %modname\\%modname.cs, if it exists
//
// A mod can require another mod by calling RequireMod("othermod);
// Each mod is only ever loaded once, the first time RequireMod is called.
//
// NOTE: We never add mods to the search path; they must always use "modname\\file.cs" paths.
//
// Globals:
// $modList = space-separated list of loaded mods, with each mod's dependencies listed after it
// $modLoaded["modname"] = true iff modname has been loaded
// $modLoading["modname"] = true while RequireMod is running; only used to detect cycles, cleaned up afterward
function RequireMod(%modname) {
	if ($modLoaded[%modname]) return; // do nothing if already loaded
	
	// Sanity check for load cycles
	if ($modLoading[%modname]) {
		echo("FATAL ERROR: ", %modname, " was loaded twice in a require cycle! Current $modList: ", $modList);
		quit();
	}
	$modLoading[%modname] = true;
	echo("Loading mod ", %modname, "...");

	// Add to mod list
	$modList = $modList @ " " @ %modname;
	
	// Create mod volume, if it exists
	%volfile = %modname @ "\\" @ %modname @ ".vol";
	if (isFile("mods\\" @ %volfile)) {
		if (!isObject(ModVolumes)) newObject(ModVolumes, SimGroup);
		addToSet(ModVolumes, newObject(%modname, SimVolume, %volfile));
	} else {
		dbecho(2, "Warning: main volume for mod ", %modname, " (", %volfile, ") not found (this may be intentional)");
	}
	
	// Execute mod script, if it exists
	%scriptfile = %modname @ "\\" @ %modname @ ".cs";
	if (isFile("mods\\" @ %scriptfile)) {
		exec2(%scriptfile);
	} else {
		echo("Warning: main script file for mod ", %modname, " (", %scriptfile, ") not found");
	}
	
	// Mark as loaded
	$modLoading[%modname] = "";
	$modLoaded[%modname] = true;
}

// Finally, actually call init()
init();