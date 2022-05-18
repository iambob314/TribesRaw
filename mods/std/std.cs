exec("std\\util.cs");
exec("std\\sound.cs");
exec("std\\strings.cs");
exec("std\\vols.cs");

// std::initDefaults{Client,Server} load stuff for a default client or (dedicated) server setup.
// Only load one or the other (not both).
// If you don't call one of these, you'll need to do some legwork to get a stable setup.
function std::initDefaultsServer() {
	std::initDefaultsCommon();
}

function std::initDefaultsClient() {
	std::initDefaultsCommon();
	
	// Load other vols
	std::loadStdVols();
	std::loadSoundVols();
	std::loadVoiceVols();
	
	// Initialize sound
	std::initSfx();
}

function std::initDefaultsCommon() {
	std::initStrings(); // load string tables
	std::loadStdVols(); // load default vols
}
