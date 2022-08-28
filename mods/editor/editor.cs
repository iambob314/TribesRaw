// Basic map editor, intended to be used standalone (no other client/server mod).
//
// TODO:
// * Client-side map editor
requireMod(std);
requireMod(gui);

$Editor::isLoopback = (afind("-server", argv) != -1); // -server command-line flag means we're server+loopback

exec("editor\\connectseq.cs");
exec("editor\\client.cs");
exec("editor\\server\\server.cs");

exec("editor\\actions.cs");
exec("editor\\me.cs");

exec("editor\\gui\\gui.cs");
exec("editor\\shapelist.cs");

exec("editor\\editorcontrols.cs");
exec("editor\\playercontrols.cs");

function Editor::initMERemote() {}

function Editor::initMELoopback() {
	// ME + editCamera init
	ME::Create(MainWindow);

	newObject(editCamera, EditCamera, "editor.sae");
	$ME::camera = editCamera; // required: used by DarkStar internally
	
	Editor::initMESettings();
	
	// TED init (TODO: crashy because missing TED config funcs/vars probably)
	//Ted::initTed();
	//Ted::attachToTerrain();
	// TODO: other TED config funcs (and vars, but these may be simple mirrors of config funcs and unnecessary)
}

// TODO: this tail is experimental; clean up later

focusClient();	
Editor::initClient(); // required: must create clientDelegate BEFORE serverDelegate, or LOOPBACK does not work

if ($Editor::isLoopback) {
	focusServer();
	Editor::initServer(28001);
	focusClient();
}

Editor::clientConnect(tern($Editor::isLoopback, "LOOPBACK:28001", "IP:localhost:28001"));
