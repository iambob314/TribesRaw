
function base::loadBaseVols(%noRSP) {
	addToSet(newObject(StdVolumes, SimGroup),
		newObject(FontsVolume, SimVolume, "baseres\\core\\fonts.vol"),
		newObject(DarkstarVolume, SimVolume, "baseres\\core\\darkstar.vol"),
		newObject(InterfaceVolume, SimVolume, "baseres\\core\\interface.vol"),
		newObject(ShellVolume, SimVolume, "baseres\\core\\shell.vol"),
		newObject(ShellCommonVolume, SimVolume, "baseres\\core\\shellcommon.vol"),
		newObject(EditVolume, SimVolume, "baseres\\core\\edit.vol"),
		newObject(EditorVolume, SimVolume, "baseres\\core\\editor.vol")
	);
	if (!%noRSP) base::refreshSearchPath();
	return nameToID(StdVolumes);
}

function base::loadSoundVols(%noRSP) {
	%vol = newObject(SoundVolume, SimVolume, "baseres\\sound\\sound.vol");
	if (!%noRSP) base::refreshSearchPath();
	return %vol;
}

function base::loadVoiceVols(%noRSP) {
	%voices = newObject(VoiceVolumes, SimGroup);
	%pat = "baseres\\voices\\*.vol";
	for (%volfile = File::findFirst(%pat); %volfile != ""; %volfile = File::findNext(%pat)) {
		%volname = File::getBase(%volfile) @ "VoiceVolume";
		%vol = newObject(%volname, SimVolume, %volfile);
		addToSet(%voices, %vol);
	}
	if (!%noRSP) base::refreshSearchPath();
	return %voices;
}
