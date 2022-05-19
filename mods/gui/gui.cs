requireMod(std);

function Gui::newWindow(%name, %gui, %title) {
	%win = newObject(def(%name, MainWindow), SimGui::Canvas, def(%title, "Tribes"), 640, 480, True, "512 384", "1024 768");

	inputActivate(keyboard0);
	inputActivate(mouse0);

	GuiNewContentCtrl(%win, SimGui::BitmapCtrl);
	setCursor(%win, "Cur_Arrow.bmp");
	cursorOn(%win);

	setFullscreenDevice(%win, $pref::VideoFullScreenDriver);
	setFSResolution(%win, $pref::VideoFullScreenRes);
	flushTextureCache();
	
	return %win;
}
