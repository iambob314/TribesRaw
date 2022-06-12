//-----------------------------------
//
// Tribes startup (stripped down to absolute minimum)
//
//-----------------------------------

//
// If you want to add always-loaded mods, uncomment below and add your mods to $modList:
// $modList = "yourmod yourothermod";
//

// Initialize client and server objects
newClient();
newServer();

// Initialize scheduler for client and server; end with server focus
focusClient();
newObject(ConsoleScheduler, SimConsoleScheduler);
focusServer();
newObject(ConsoleScheduler, SimConsoleScheduler);

// Execute base.cs (which provides more init and mod loading support)
$ConsoleWorld::DefaultSearchPath = "base"; // temporary, to load base
exec("base.cs");
