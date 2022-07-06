// base::initConsts loads several tables of constants into global variables, which
// represent core, instrinsic constants of the Tribes/DarkStar/Fear engine.
// Universal enough to be worth always loading.
function base::initConsts() {
	%pat = "baseres\\consts\\*.cs";
	for (%f = File::findFirst(%pat); %f != ""; %f = File::findNext(%pat)) exec(%f);
}