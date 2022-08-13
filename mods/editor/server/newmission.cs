
function Editor::loadOtherWorldVols(%set, %worldType) {
	// (replaces old exec("createWorldVolumes.cs"); )
	%worldVolPath = "baseres\\world\\";
	
	%worldDMLs = eval(Terrain:: @ %worldType @ "::getDMLs();");
	for (%i = 0; (%dml = getWord(%worldDMLs, %i)) != -1; %i++) {
		%dmlName = String::stripSuffix(%dml, ".vol");
		addToSet(%set,	newObject(%dmlName, SimVolume, %worldVolPath @ %dml));
	}
	
	// If the rpgres mod exists, load vols from there TODO: unhackify
	for (%v = File::findFirst("rpgres\\*.vol"); %v != ""; %v = File::findNext("rpgres\\*.vol")) {
		%volName = String::stripSuffix(File::getBase(%v), ".vol");
		addToSet(%set,	newObject(%volName, SimVolume, %v));
	}
}

//
// %missionName = becomes base of .ted file
// %worldType = e.g. lush, desert, etc.
// %terrType = 0, 1, 2, 3 (maps to terrain.cs) TODO improve this
// %isDay = true/false (changes palette)
// %terrBlockSize = number of tiles per terrain block (must be 64, 128, 256 for now because of terrain.cs)
//
function Editor::newMission(%missionName, %worldType, %terrType, %isDay, %terrBlockSize, %seed) {
	%worldVolPath = "baseres\\world\\";
	%terrScriptPath = "baseres\\terrain\\";

	%timeOfDay = tern(%isDay, day, night);
	
	// Top-level files (needs %worldVolPath)
	%mainWorldVolPath = %worldVolPath @ %worldType @ "World.vol";
	%mainTerrVolPath = %worldVolPath @ %worldType @ "Terrain.vol";
	
	// Inside %terrVolName (no path)
	%dmlName = %worldType @ ".dml";
	%palName = %worldType @ "." @ %timeOfDay @ ".ppl";
	%rulesName = %worldType @ ".rules.dat";
	%gridName = %worldType @ ".grid.dat";

	%terrFile = %missionName @ ".dtf";
	%tedFile = %missionName @ ".ted";
	%misFile = %missionName @ ".mis";

	// Load the ruleset and other info for this world type
	exec(%terrScriptPath @ %worldType @ ".terrain.cs");
	// Defines functions:
	//   Terrain::%worldType::setTypes()
	//   Terrain::%worldType::setRules()
	//   Terrain::%worldType::createGridFile()
	//   Terrain::%worldType::getDMLs() // returns "dml1.vol dml2.vol ..."
	
	// Default groups
	addToSet(newObject("MissionGroup", SimGroup),
		newObject("Volumes", SimGroup),
		newObject("World", SimGroup),
		newObject("Landscape", SimGroup)
	);

	// Default volumes
	addToSet("MissionGroup\\Volumes",
		newObject("InterfaceVol", SimVolume, "baseres\\core\\interface.vol"),
		newObject("Entities", SimVolume, "baseres\\shapes\\entities.vol"),
		newObject("World", SimVolume, %mainWorldVolPath),
		newObject("WorldTerrain", SimVolume, %mainTerrVolPath)
	);

	// World-specific DML volumes
	base::refreshSearchPath();
	Editor::loadOtherWorldVols("MissionGroup\\Volumes", %worldType);

	// World palette
	addToSet("MissionGroup\\World", newObject("Palette", SimPalette, %palName, true));

	// add the sky
	addToSet("MissionGroup\\Landscape", newObject(Stars, StarField));
      
	%skydmlName = ""; // TODO, get from $worldTypes[%wi, skydml, %si];
	addToSet("MissionGroup\\Landscape",
		newObject(Sky, Sky, 0, 0, 0, %skydmlName, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15)
	);

	// Default sun light
	if (%timeOfDay == Night)
		newObject("Sun", Planet, 0, 0, 60, T, F, 0.4, 0.4, 0.4, 0.15, 0.15, 0.15);
	else 
		newObject("Sun", Planet, 0, 0, 60, T, F, 0.7, 0.7, 0.7, 0.3, 0.3, 0.3);
	addToSet("MissionGroup\\Landscape", "Sun");

	%polySize = 3; // (1 << this) meters per block
	%numBlocks = 1; // number of gridBlocks on a side

	%terrBlockWidth = %terrBlockSize * (1 << %polySize); // terrain width in meters
	%terrWidth = %terrBlockWidth * %numBlocks;

	echos("createTerrain",
	Create, 		// Create or Load
		%terrFile,		// Filename (to create or load)
		3,				// GFsize = number of gridBlocks on a side (GFsize^2 total)
		%polySize,		// GFscale = (1<<this) units per poly (tile)
		%terrBlockSize,	// GBsize = matrix size for each gridBlock (num polys on a side)
		0,				// GBlightscale (???)
		0,				// uniqueBlocks (each gridBlock has own heightmap if true, shared if false)
		-%terrWidth/2, -%terrWidth/2, 0,	// pos
		0, 0, 0								// rot
	);

	// create the simterrain obj
	//
	// A Terrain is made up of GFsize^2 gridblocks (GFsize to a side), each of which
	// contains GBsize polys, each of which is (1<<GFscale) meters.
	// If uniqueBlocks, each gridBlock has its own heightmap, otherwise they repeat
	//
	%terrain = newObject("Terrain", SimTerrain,
		Create, 		// Create or Load
		%terrFile,		// Filename (to create or load)
		3,				// GFsize = number of gridBlocks on a side (GFsize^2 total)
		%polySize,		// GFscale = (1<<this) units per poly (tile)
		%terrBlockSize,	// GBsize = matrix size for each gridBlock (num polys on a side)
		0,				// GBlightscale (???)
		0,				// uniqueBlocks (each gridBlock has own heightmap if true, shared if false)
		-%terrWidth/2, -%terrWidth/2, 0,	// pos
		0, 0, 0								// rot
	);

	addToSet("MissionGroup\\World",
		newObject("MissionCenter", MissionCenterPos, -%terrBlockSize/2, -%terrBlockSize/2, %terrBlockSize, %terrBlockSize)
	);

	// Load LS scripts to generate different terrain topographies
	exec(%terrScriptPath @ "terrains.cs");
	// This also loads these:
	// $terrainTypes[1, visDistance]    = 600;
	// $terrainTypes[1, hazeDistance]   = 400;
	// $terrainTypes[1, screenSize]     = 70;
	
	%visDist = $terrainTypes[%terrType, visDistance];
	%hazeDist = $terrainTypes[%terrType, hazeDistance];
	%screenSize = $terrainTypes[%terrType, screenSize];
	%perspectiveDist = 100;
	
	// set the detail (perspective distance, screensize)   
	setTerrainDetail(Terrain, %perspectiveDist, %screenSize); // (perspectiveDist, screenSquareSize)
	setTerrainVisibility(Terrain, %visDist, %hazeDist); // (visDist, hazeDist)
	SetTerrainContainer(Terrain, "0 0 -20", 0, 10000);  //  (Gravity, Drag, Height) TODO

	// apply the terrains
	LS::Create(1); // not sure what "1" does
	LS::Textures(%dmlName, %gridName);

	// execute the terrain script file and apply the rules
	eval(Terrain:: @ %worldType @ "::setRules();");
   
	// process the terrain commands for this type
	LS::flushCommands();
	eval(Terrain:: @ $terrainTypes[%terrType, type] @ "::create(" @ %terrBlockSize @ "," @ def(%seed,0) @ ");");
	%terrain.terrainType = $terrainTypes[%terrType, type];
	LS::parseCommands();

	// Finalize stuff, I guess?
	LS::ApplyLandscape();
	LS::ApplyTextures();

	// save off the terrain, add to MissionGroup, and add as vol
	saveTerrain(Terrain, "Temp\\" @ %tedFile);
	
	addToSet("MissionGroup\\Landscape", %terrain);

	base::refreshSearchPath();
	addToSet("MissionGroup\\Volumes", newObject("TedFile", SimVolume, %tedFile));

	// save the mission
	exportObjectToScript(MissionGroup, "Temp\\" @ %misFile, true);
	base::refreshSearchPath();
	
	// wipe everything and reload from instant
	// (for some reason, this is necessary or terrain file will mismatch with client)
	deleteObject(MissionGroup);
	exec(%misFile);
}