//
// Editor modals (ME options, TED options, help)
//
// We do a bit of loopback vs. remote editor logic here for simplicity. Could factor it out
// in the future.
//
// Some functions may be overridden by loopback/remote editor GUI code (commented below).
//

// TODO: make these more generic and loopback/remote-specific by delegating to
//       e.g. EditorUI::<mode>::optionsModal();

function EditorUI::getModal() {
	if (Control::getVisible(OptionsCtrl))         return OptionsCtrl;
	else if (Control::getVisible(TedOptionsCtrl)) return TedOptionsCtrl;
	else if (Control::getVisible(HelpCtrl))       return HelpCtrl;
	else return "";
}

function EditorUI::showModal(%newM) {
	%oldM = EditorUI::getModal();
	if (%newM == %oldM) return;
	if (%oldM != "") {
		invoke(%oldM @ "::onHide");
		Control::setVisible(%oldM, false);
	}
	if (%newM != "") {
		Control::setVisible(%newM, true);
		invoke(%newM @ "::onShow");
		
		if (!$Editor::isLoopback) cursorOn(MainWindow); // enable cursor to avoid user need to Tab
	}
}

function EditorUI::hideModal() { EditorUI::showModal(""); }

function EditorUI::toggleOptions() {
	%mode = EditorUI::getMode();
	echo(%mode);
	if (%mode == Create || %mode == Inspect) %modal = OptionsCtrl;
	else if (%mode == Ted)                   %modal = TedOptionsCtrl;
	else return; // no options modal in this editor mode

	if (EditorUI::getModal() != %modal) EditorUI::showModal(%modal);
	else                                EditorUI::hideModal();
}

function EditorUI::toggleHelp() {
	if (EditorUI::getModal() != HelpCtrl) EditorUI::showModal(HelpCtrl);
	else                                  EditorUI::hideModal();
}

// ME Options (OptionsCtrl)
function OptionsCtrl::onShow() {
	Control::setValue(MEUsePlaneMovement, $ME::UsePlaneMovement);
	Control::setActive(RotationSnapCtrl, $ME::SnapRotations);
	Control::setActive(XGridSnapCtrl, $ME::SnapToGrid);
	Control::setActive(YGridSnapCtrl, $ME::SnapToGrid);
	Control::setActive(ZGridSnapCtrl, $ME::SnapToGrid);
	Control::setActive(UseTerrainGrid, $ME::SnapToGrid);
}
function OptionsCtrl::onHide() {
	if ($Editor::isLoopback) ME::GetConsoleOptions(); // update ME options only for loopback
}

// GUI-wired callback from (un)checking "Snap to Grid"
function ME::SnapToGrid() {
   Control::setActive(XGridSnapCtrl, $ME::SnapToGrid);
   Control::setActive(YGridSnapCtrl, $ME::SnapToGrid);
   Control::setActive(ZGridSnapCtrl, $ME::SnapToGrid);
   Control::setActive(UseTerrainGrid, $ME::SnapToGrid);
}

// GUI-wired callback from (un)checking "Snap to Rotation"
function ME::SnapRotations() {
   Control::setActive(RotationSnapCtrl, $ME::SnapRotations);
}

// NOTE: this gets overridden when remote editor is loaded
function UseTerrainGrid::onAction() {
	ME::onUseTerrainGrid(); // Makes ME load grid spacing into $ME::{X,Y}GridSnap
	Control::setValue(XGridSnapCtrl, $ME::XGridSnap);
	Control::setValue(YGridSnapCtrl, $ME::YGridSnap);
}

// TED Options (TedOptionsCtrl)
function TedOptionsCtrl::onShow() {
	Control::setVisible(TedOptionsCtrl, true);
	Control::setValue(TerrainSeedText, $ME::terrainSeed);
}
function TedOptionsCtrl::onHide() {
	Ted::GetConsoleOptions();
	// Control::setText(SeedTerrain, "Gen: " @ $ME::terrainSeed); TODO: why?
}

// Help (HelpCtrl)
function HelpCtrl::onShow() {}
function HelpCtrl::onHide() {}
