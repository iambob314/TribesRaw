exec("std\\util\\string.cs");

function tern(%cond, %tval, %fval) {
	if (%cond != "") return %tval;
	return %fval;
}

function def(%val, %defval) { return tern(%val, %val, %defval); }