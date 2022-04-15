pushFocusClient();

exec2("controls.cs");
exec2("ClientPrefs.cs");
exec2("client\\connection.cs");
exec2("client\\remoteInterface.cs");
exec2("client\\playerConnectSeq.cs");

exec2("editor\\playerEditorClient.cs");

function loadPlayGui() {
	if (File::FindFirst("play.gui") != "")
		GuiLoadContentCtrl(MainWindow, "play.gui");
	else
		GuiLoadContentCtrl(MainWindow, "gui\\play.gui");
}

function createClient() {

   if (!isObject(ConsoleScheduler)) newObject(ConsoleScheduler, SimConsoleScheduler);

   function Game::endFrame() {}

   // Create the main window with a gui in it
   newObject(MainWindow, SimGui::Canvas, "RPGX", 640, 480, True, "512 384", "1024 768");

   inputActivate(keyboard0);
   inputActivate(mouse0);

   if (!isObject(clientDelegate)) newObject(clientDelegate, FearCSDelegate, false, "IP", 0, "IPX", 0, "LOOPBACK", 0);

   GuiLoadContentCtrl(MainWindow, "gui\\empty.gui");
   GuiLoadContentCtrl(MainWindow, "mainmenu.gui");
   setCursor(MainWindow, "Cur_Arrow.bmp");
   cursorOn(MainWindow);

   setFullscreenDevice(MainWindow, $pref::VideoFullScreenDriver);
   setFSResolution(MainWindow, $pref::VideoFullScreenRes);
   flushTextureCache();

   echo("Client Initialized");

}

createClient();

popFocus();
