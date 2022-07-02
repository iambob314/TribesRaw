exec("std\\util\\string.cs");

function tern(%cond, %tval, %fval) {
	if (%cond != "") return %tval;
	return %fval;
}

function def(%val, %defval) { return tern(%val, %val, %defval); }

function echos(%a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19) {
	%end = 0;
	for (%i = 0; %i < 20; %i++) if (%a[%i] != "") %end = %i;
	
	%msg = "";
	for (%i = 0; %i < %end; %i++) {
		if (%i > 0) %msg = %msg @ " ";
		%msg = %msg @ %a[%i];
	}
	
	echo(%msg);
}