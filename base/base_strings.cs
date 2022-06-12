// base::initStrings initalizes all of the ID* tags needed by the system.
// Lots of stuff gets crashy if you don't do this...
function base::initStrings() {
	%pat = "baseres\\strings\\*.strings.cs";
	for (%f = File::findFirst(%pat); %f != ""; %f = File::findNext(%pat)) exec(%f);
}