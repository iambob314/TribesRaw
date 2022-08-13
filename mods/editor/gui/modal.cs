// Editor modals (ME options, TED options, help)

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
function OptionsCtrl::onHide() { ME::GetConsoleOptions(); }

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

function UseTerrainGrid::onAction() {
	ME::onUseTerrainGrid(); // Makes ME load grid spacing into $ME::{X,Y}GridSnap
	Control::setValue(XGridSnapCtrl, $ME::XGridSnap);
	Control::setValue(YGridSnapCtrl, $ME::YGridSnap);
	Control::setValue(ZGridSnapCtrl, $ME::ZGridSnap);
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
