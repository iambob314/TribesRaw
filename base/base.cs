// Barebones bootstrapping for Tribes
// (note: we start and end with server focus)

function Game::endFrame() {} // it's *really* spammy until we do this

exec("base_require.cs");
exec("base_consts.cs");
exec("base_sound.cs");
exec("base_strings.cs");
exec("base_vols.cs");

// base::init is called at the end of this file (defined at top here for visibility)
function base::init() {
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
	if ($dedicated || $terminal) {
		$WinConsoleEnabled = true;
	}

	// Set up search path
	base::refreshSearchPath();

	// Load core resources
	base::initStrings();  // load string tables
	base::initConsts();   // load const vars (intrinsic Tribes/DarkStar/Fear values)
	base::loadBaseVols(); // load default vols
	
	// Load all mods specified by command line args
	%modsToLoad = $modList;
	for (%i = 0; (%modname = getWord(%modsToLoad, %i)) != -1; %i++) requireMod(%modname);

	echo("Tribes initialized");
}

// base::initClient is an optional initializer called for clients (i.e. non-$dedicated instances)
function base::initClient() {
	base::initSfx();
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
		} else if ($cargv[%i] == "-terminal") {
			$terminal = true;
		} else {
			$argv[%outIdx++ - 1] = $cargv[%i]; // extract unparsed args for mods to handle
		}
	}
}

// base::refreshSearchPath just adds the top-level directories to the path. We keep this function
// just to refresh for newly-appeared files.
function base::refreshSearchPath() {
	$ConsoleWorld::DefaultSearchPath = "base;config;mods;recordings;temp";
	//echo("Refreshed search path: ", $ConsoleWorld::DefaultSearchPath);
}

// Finally, actually call base::init (and for clients, also base::initClient)
base::init();
if (!$dedicated) base::initClient();