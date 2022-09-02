function tern(%cond, %tval, %fval) {
	if (%cond) return %tval;
	return %fval;
}

function def(%val, %defval) { return tern(%val != "", %val, %defval); }

function echos(%a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19) {
	%end = 0;
	for (%i = 0; %i < 20; %i++) if (%a[%i] != "") %end = %i+1;
	
	%msg = "";
	for (%i = 0; %i < %end; %i++) {
		if (%i > 0) %msg = %msg @ " ";
		%msg = %msg @ %a[%i];
	}
	
	echo(%msg);
}

// argliststr returns %a0-%a19 (except dropping any trailing "" values), quoted and properly
// escaped, as a concatenated comma-separated list in a string. It is safe to use as a
// function argument list as part of an eval command.
function argliststr(%a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19) {
	%end = 0;
	for (%i = 0; %i < 20; %i++) if (%a[%i] != "") %end = %i+1;

	%list = "";
	for (%i = 0; %i < %end; %i++) {
		if (%i > 0) %list = %list @ ",";
		%list = %list @ "\"" @ String::escape(%a[%i]) @ "\"";
	}
	return %list;
}

// invokestr returns an eval-able string for invoking function %f with arguments %a0-%a19
// (it passes all %a[%i] up until the last non-"" value). It string-escapes the arguments for safety.
function invokestr(%f, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19) {
	%args = argliststr(%a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19);
	return %f @ "(" @ %args @ ");";
}

// invoke is equivalent to eval'ing the return of invokestr.
function invoke(%f, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19) {
	%cmd = invokestr(%f, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19);
	return eval(%cmd);
}

function assert(%cond, %msg) {
	if (%cond == "" || !%cond) { echo("ASSERTION FAILED: ", %msg); trace(); quit(); }
}