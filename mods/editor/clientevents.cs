
function onTeamAdd(%team, %name) {}

function onClientMessage(%clientId, %msg) { echo("onClientMessage ", %clientId, " ", %msg); return true; }
function onClientJoin(%clientId) { echo("onClientJoin ", %clientId); }
function onClientDrop(%clientId) { echo("onClientDrop ", %clientId); }
function onClientChangeTeam(%clientId, %team) { echo("onClientChangeTeam ", %clientId, " ", %team);}

// Client <- Server's Client::setGuiMode(%clientId, 1)
function loadPlayGui() {
	if (File::FindFirst("play.gui") != "")
		GuiLoadContentCtrl(MainWindow, "play.gui");
	else
		GuiLoadContentCtrl(MainWindow, "gui\\play.gui");
}
