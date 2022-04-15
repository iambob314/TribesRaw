//-----------------------------------
//
// Tribes startup
//
//-----------------------------------

//
// --- THE MOST RETARDED THING EVER ---
//
// When a function or file calls exec("something.cs");, that executed file shares the same local variable table
// as the calling file/function, which means it can do really annoying things. For instance:
//
// <<<a.cs>>>
// %x = 1;
// exec("b.cs");
// echo("X: ", %x);
// <<<b.cs>>>
// %x = 2;
//
// Calling exec("a.cs"); will print "X: 2".
//
// To avoid this annoying behavior, replace all exec() calls with exec2() calls, which shields the local variable
// table from modification in other files.
//
function exec2(%file) { exec(%file); }

// test if this is a dedicated server
$dedicated = false;

$Console::logMode = 1;
$Console::printLevel = 1;
$Console::GFXFont = "interface.pft";

$numExecFiles = 0;

for (%i = 1; $cargv[%i] != ""; %i++) {
  if ($cargv[%i] == "-mod") {
    %mod = $cargv[%i+1];
    %i = %i + 1;
    $modList = %mod @ " " @ $modList;
    $modCount++;
  } else if ($cargv[%i] == "-edit") {
    %i++;
    $editMission = $cargv[%i];
  } else if ($cargv[%i] == "-dumb") {
    $dumbTerminal = true;
  } else if ($cargv[%i] == "+server") {
    $dedicated = true;
  } else if ($cargv[%i] == "+debug") {
    $debugMode = true;
  } else if ($cargv[%i] == "+viewobject") {
    $viewObject = $cargv[%i+1];
    %i++;
  } else if ($cargv[%i] == "+exec") {
    $execFile[$numExecFiles] = $cargv[%i+1];
    %i++;
    $numExecFiles++;
  }
}

$WinConsoleEnabled = $dedicated || $dumbTerminal;
$Console::logBufferEnabled = !$dedicated;
$Console::Prompt = "=> ";
$Console::History = 50;

newClient();
newServer();

if ($dedicated) {
  focusServer();
} else {
  focusClient();
  $Console::LastLineTimeout = 0;
}

function EvalSearchPath() {
  // search path always contains the config and base directory
  %searchPath = "config";

  for (%i = 0; (%word = getWord($modList, %i)) != -1; %i++) {
    %searchPath = %searchPath @ ";" @ %word;
    // If this mod has a special search path, add that as well
    if ($SearchPath[%word] != "") {
      %searchPath = %searchPath @ ";" @ $SearchPath[%word];
    }
  }

   %searchPath = %searchPath @ ";base;base\\missions";

   %searchPath = %searchPath @ ";recordings;temp";
   echo(%searchPath);

   $ConsoleWorld::DefaultSearchPath = %searchPath;
}

function LoadModVolumes() {
  // Load mod volumes in the order they were declared
  for (%i = $modCount - 1; %i >= 0; %i--) {
    %word = getWord($modList, %i);
    %vol = %word @ ".vol";
    if (isFile(%word @ "\\" @ %vol))
      newObject(%word @ "Volume", SimVolume, %vol);
  }
}

function ExecModScripts() {
  echo("EXECUTING MOD SCRIPTS (" @ $modCount @ " total)");
  // Exec mod scripts in the order they were declared
  for (%j = $modCount - 1; %j >= 0; %j--) {
    %mod = getWord($modList, %j);
    exec2(%mod @ ".cs");
  }
}

//
EvalSearchPath();
LoadModVolumes();
newObject(FontsVolume, SimVolume, "fonts.vol");
//newObject(ScriptsVolume, SimVolume, "scripts.vol");
//newObject(GuiVolume, SimVolume, "gui.vol");
newObject(DarkstarVolume, SimVolume, "darkstar.vol");
newObject(InterfaceVolume, SimVolume, "interface.vol");
newObject(ShellVolume, SimVolume, "shell.vol");
newObject(ShellCommonVolume, SimVolume, "shellcommon.vol");

newObject("", IRCClient);

ExecModScripts();

//
// Default keys
//
bind(keyboard, make, control, o, to, "messageCanvasDevice(MainWindow, outline);");
bind(keyboard, make, sysreq, to, "screenShot(MainWindow);");
bind(keyboard, make, control, "-", to, "prevRes(MainWindow);");
bind(keyboard, make, control, "+",  to, "nextRes(MainWindow);");

if (isFile("autoexec.cs")) exec2("autoexec.cs");

for (%i = 0; %i < $numExecFiles; %i++)
  exec2($execFile[%i]);
