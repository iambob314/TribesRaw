
afromvar(argv, "$argv"); // load $argv[...] into array argv

// argvFlagParam takes a flag like "-myflag", finds its first occurrence in
// $argv (command-line args), and returns the next token (its argument).
//
// Example: given command line args "-myflag foo bar -otherflag baz":
// argvFlagParam("-myflag")    == "foo"
// argvFlagParam("-otherflag") == "baz"
// argvFlagParam("foo")        == "bar" // works, but weird; don't use this except for flags
function argvFlagParam(%flag) {
	if ((%i = afind(argv, %flag)) == -1) return;
	return aget(argv, %i+1);
}
