
function std::loadStdVols() {
	addToSet(newObject(StdVolumes, SimGroup),
		newObject(FontsVolume, SimVolume, "std\\res\\core\\fonts.vol"),
		newObject(DarkstarVolume, SimVolume, "std\\res\\core\\darkstar.vol"),
		newObject(InterfaceVolume, SimVolume, "std\\res\\core\\interface.vol"),
		newObject(ShellVolume, SimVolume, "std\\res\\core\\shell.vol"),
		newObject(ShellCommonVolume, SimVolume, "std\\res\\core\\shellcommon.vol"),
		newObject(EditVolume, SimVolume, "std\\res\\core\\edit.vol"),
		newObject(EditorVolume, SimVolume, "std\\res\\core\\editor.vol")
	);
	return nameToID(StdVolumes);
}

function std::loadSoundVols() {
	return newObject(SoundVolume, SimVolume, "std\\res\\sound\\sound.vol");
}

function std::loadVoiceVols() {
	%voices = newObject(VoiceVolumes, SimGroup);
	%pat = "std\\res\\voices\\*.vol";
	for (%volfile = File::findFirst(%pat); %volfile != ""; %volfile = File::findNext(%pat)) {
		%volname = File::getBase(%volfile) @ "VoiceVolume";
		%vol = newObject(%volname, SimVolume, %volfile);
		addToSet(%voices, %vol);
	}
	return %voices;
}
