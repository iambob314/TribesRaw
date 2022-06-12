//
// requireMod(%modname) - load mod %modname
//
// Loading mod %modname:
// 1) creates %modname\\%modname.vol as SimVolume, if it exists
// 2) executes %modname\\%modname.cs, if it exists
//
// A mod can require another mod by calling requireMod("othermod);
// Each mod is only ever loaded once, the first time requireMod is called.
//
// NOTE: We never add mods to the search path; they must always use "modname\\file.cs" paths.
//
// Globals:
// $modList = space-separated list of loaded mods, with each mod's dependencies listed after it
// $modLoaded["modname"] = true iff modname has been loaded
// $modLoading["modname"] = true while requireMod is running; only used to detect cycles, cleaned up afterward
function requireMod(%modname) {
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
		exec_safe(%scriptfile);
	} else {
		echo("Warning: main script file for mod ", %modname, " (", %scriptfile, ") not found");
	}
	
	// Mark as loaded
	$modLoading[%modname] = "";
	$modLoaded[%modname] = true;
}

function exec_safe(%file) { exec(%file); } // for safely executing a file without allowing it to clobber local variables
