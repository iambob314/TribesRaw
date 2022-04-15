Ted::initTed();
Ted::attachToTerrain();

Ted::setHeightVal( $TED::heightVal );
Ted::setAdjustVal( $TED::adjustVal );
Ted::setScaleVal( $TED::scaleVal );
Ted::setPinDetailVal( $TED::pinDetailVal );
Ted::setPinDetailMax( $TED::pinMaxVal );
Ted::setSmoothVal( $TED::smoothVal );
Ted::setMatIndexVal( $TED::matIndexVal );

Ted::setSelectShow( $TED::selectionDisplayFrame, $TED::selectionDisplayFill, $TED::selectionDisplayOutline );
Ted::setSelectFrameColor( $TED::selectionColorFrame );
Ted::setSelectFillColor( $TED::selectionColorFill );
Ted::setHilightShow( $TED::hilightDisplayFrame, $TED::hilightDisplayFill, $TED::hilightDisplayOutline );
Ted::setHilightFrameColor( $TED::hilightColorFrame );
Ted::setHilightFillColor( $TED::hilightColorFill );
Ted::setShadowShow( $TED::shadowDisplayFrame, $TED::shadowDisplayFill, $TED::shadowDisplayOutline );
Ted::setShadowFrameColor( $TED::shadowColorFrame );
Ted::setShadowFillColor( $TED::shadowColorFill );

Ted::setSnap( $TED::brushSnap );
Ted::setFeather( $TED::brushFeather );

Ted::setBlockOutline( $TED::blockDisplayOutline );

// /*
function comment() {
// only show if not in new editor
if( !$ME::Loaded )
{
Ted::window( $Ted::mainWindow );

setMainWindow( $Ted::mainWindow );

cursorOn( MainWindow );
GuiNewContentCtrl( MainWindow, SimGui::TSControl );
}
}
// */