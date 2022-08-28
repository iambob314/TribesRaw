// Basic map editor, intended to be used standalone (no other client/server mod).
//
// TODO:
// * Client-side map editor
requireMod(std);
requireMod(gui);

//
// Defined in loopback.cs or remote.cs:
// * Editor::onConnect()   (called when connection complete)
//

$Editor::isLoopback = (afind("-server", argv) != -1); // -server command-line flag means we're server+loopback

exec("editor\\connectseq.cs");
exec("editor\\client.cs");
exec("editor\\gui\\gui.cs");

if ($Editor::isLoopback) {
	exec("editor\\server\\server.cs");
	exec("editor\\loopback\\loopback.cs");
} else {
	exec("editor\\remote\\remote.cs");
}

exec("editor\\playercontrols.cs");
exec("editor\\shapelist.cs"); // TODO: should this be server-side only?


// TODO: this tail is experimental; clean up later

focusClient();	
Editor::initClient(); // required: must create clientDelegate BEFORE serverDelegate, or LOOPBACK does not work

if ($Editor::isLoopback) {
	focusServer();
	Editor::initServer(28001);
	focusClient();
}

Editor::clientConnect(tern($Editor::isLoopback, "LOOPBACK:28001", "IP:localhost:28001"));
