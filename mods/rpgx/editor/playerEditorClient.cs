exec2("editor\\controls\\playerEditorControls.cs");

$PlayerEditor::PLAY_MODE = 0;
$PlayerEditor::EDIT_MODE = 1;

$PlayerEditor::NUDGE_AMT = 1.0;
$PlayerEditor::NUDGE_ROT_AMT = $PI/4;
$PlayerEditor::SMALL_NUDGE_AMT = 0.125;
$PlayerEditor::SMALL_NUDGE_ROT_AMT = $PI/12;

// $PlayerEditor::modShift
// $PlayerEditor::modCtrl
// $PlayerEditor::modAlt

function remoteLoadPlayerEditor(%conn) {
  if (%conn != 2048) return;

  PlayerEditor::init();
}

function PlayerEditor::init() {
  if ($PlayerEditor::init) return;

  GuiEditMode(MainWindow);
  GuiSetSelection(MainWindow, playGui);
  GuiLoadSelection(MainWindow, "temp\\pep.cs");
  GuiEditMode(MainWindow);

  $PlayerEditor::init = true;
  $PlayerEditor::CurrentMode = $PlayerEditor::PLAY_MODE;
  PlayerEditor::enterPlayMode();
}

function PlayerEditor::SwitchModes(%newMode) {
  if (%newMode == $PlayerEditor::CurrentMode) return;

  if ($PlayerEditor::CurrentMode == $PlayerEditor::PLAY_MODE) PlayerEditor::exitPlayMode();
  else if ($PlayerEditor::CurrentMode == $PlayerEditor::EDIT_MODE) PlayerEditor::exitEditMode();

  $PlayerEditor::CurrentMode = %newMode;

  if ($PlayerEditor::CurrentMode == $PlayerEditor::PLAY_MODE) PlayerEditor::enterPlayMode();
  else if ($PlayerEditor::CurrentMode == $PlayerEditor::EDIT_MODE) PlayerEditor::enterEditMode();
  else echo("Unknown PlayerEditor mode: " @ %newMode);
}



function PlayerEditor::nudge(%dir) {
  %scale = tern($PlayerEditor::modAlt, $PlayerEditor::SMALL_NUDGE_AMT, $PlayerEditor::NUDGE_AMT);
  remoteEval(2048, PE::Nudge, Vector::mul(%dir, %scale), %scale);
}

function PlayerEditor::nudgeRot(%dir) {
  %scale = tern($PlayerEditor::modAlt, $PlayerEditor::SMALL_NUDGE_ROT_AMT, $PlayerEditor::NUDGE_ROT_AMT);
  remoteEval(2048, PE::NudgeRot, Vector::mul(%dir, %scale), %scale);
}



// Mode enter/exit functions
function PlayerEditor::enterEditMode() {
  Control::setValue(PlayerEditorModeText, "EDIT");
  pushActionMap("playerEditorEditMap.sae");
}

function PlayerEditor::exitEditMode() {
  popActionMap("playerEditorEditMap.sae");
}

function PlayerEditor::enterPlayMode() {
  Control::setValue(PlayerEditorModeText, "PLAY");
  pushActionMap("playerEditorPlayMap.sae");
}

function PlayerEditor::exitPlayMode() {
  popActionMap("playerEditorPlayMap.sae");
}