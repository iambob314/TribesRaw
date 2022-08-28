function tern(%cond, %tval, %fval) {
	if (%cond != "") return %tval;
	return %fval;
}

function def(%val, %defval) { return tern(%val, %val, %defval); }

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

// invokestr returns an eval-able string for invoking function %f with arguments %a0-%a19
// (it passes all %a[%i] up until the last non-"" value). It string-escapes the arguments for safety.
function invokestr(%f, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19) {
	%end = 0;
	for (%i = 0; %i < 20; %i++) if (%a[%i] != "") %end = %i+1;

	%eval = %f @ "(";
	for (%i = 0; %i < %end; %i++) {
		if (%i > 0) %eval = %eval @ ",";
		%eval = %eval @ "\"" @ String::escape(%a[%i]) @ "\"";
	}
	%eval = %eval @ ");";
	return %eval;
}

// invoke is equivalent to eval'ing the return of invokestr.
function invoke(%f, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19) {
	%cmd = invokestr(%f, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19);
	return eval(%cmd);
}

function assert(%cond, %msg) {
	if (%cond == "" || !%cond) { echo("ASSERTION FAILED: ", %msg); trace(); quit(); }
}