
function std::loadStdVols() {
	addToSet(newObject(StdVolumes, SimGroup),
		newObject(FontsVolume, SimVolume, "stdlib\\res\\core\\fonts.vol"),
		newObject(DarkstarVolume, SimVolume, "stdlib\\res\\core\\darkstar.vol"),
		newObject(InterfaceVolume, SimVolume, "stdlib\\res\\core\\interface.vol"),
		newObject(ShellVolume, SimVolume, "stdlib\\res\\core\\shell.vol"),
		newObject(ShellCommonVolume, SimVolume, "stdlib\\res\\core\\shellcommon.vol")
	);
	return nameToID(StdVolumes);
}

function std::loadSoundVols() {
	return newObject(SoundVolume, SimVolume, "stdlib\\res\\sound\\sound.vol");
}

function std::loadVoiceVols() {
	%voices = newObject(VoiceVolumes, SimGroup);
	%pat = "stdlib\\res\\voices\\*.vol";
	for (%volfile = File::findFirst(%pat); %volfile != ""; %volfile = File::findNext(%pat)) {
		%volname = File::getBase(%volfile) @ "VoiceVolume";
		%vol = newObject(%volname, SimVolume, %volfile);
		addToSet(%voices, %vol);
	}
	return %voices;
}
