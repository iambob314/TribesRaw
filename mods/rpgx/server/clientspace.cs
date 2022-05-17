function dataGotBlock() {}

function dataFinished() {}

function onClientGhostAlwaysDone() {}

function Client::onParentConnect() {
  resetSimTime();
  resetPlayDelegate();
}

function initClientspace() {
   if (!isObject(ConsoleScheduler)) newObject(ConsoleScheduler, SimConsoleScheduler);

   if (!isObject(clientDelegate)) newObject(clientDelegate, FearCSDelegate, false, "IP", 0, "IPX", 0, "LOOPBACK", 0);

   echo("Clientspace Initialized");
}

// --- INIT ---

pushFocusClient();
initClientspace();
popFocus();
