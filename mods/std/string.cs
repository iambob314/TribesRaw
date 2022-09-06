function String::len(%str) {
  if (%str == "") return 0;
  %lb = 0;
  for (%ub = 1; %ub < (1<<16) && String::getSubStr(%str, %ub, 1) != ""; %ub <<= 1) %lb = %ub;
  while (%ub - %lb > 1) {
	  %mb = (%lb + %ub) >> 1;
	  if (String::getSubStr(%str, %mb, 1) == "") %ub = %mb;
	  else %lb = %mb;
  }
  return %lb+1;
}

function String::substr(%s, %off, %len) {
	%off = def(%off, 0);
	%len = def(%len, 1<<16);
	return String::getSubStr(%s, %off, %len);
}

for (%i=0;%i<10;%i++)$escFixTable[%i] = %i;
$escFixTable["K"] = "A";
$escFixTable["L"] = "B";
$escFixTable["M"] = "C";
$escFixTable["N"] = "D";
$escFixTable["O"] = "E";
$escFixTable["P"] = "F";

function String::escape(%str) {
  %str = escapeString(%str);
  %l = String::len(%str);
  %outstr = "";
  while ((%idx = String::findSubStr(%str, "\\x")) != -1) {
	%c1 = $escFixTable[String::getSubStr(%str, %idx+2, 1)];
	%c2 = $escFixTable[String::getSubStr(%str, %idx+3, 1)];
	%outstr = %outstr @ String::getSubStr(%str, 0, %idx+2) @ %c1 @ %c2;
	%str = String::getSubStr(%str, %idx+4, %l);
  }
  return %outstr @ %str;
}

function String::stripSuffix(%s, %suff) {
	%l = String::len(%s);
	%suffL = String::len(%suff);
	if (%suffL > %l) return %s;
	
	%preL = %l - %suffL;
	
	if (String::substr(%s, %preL, %suffL) == %suff)
		return String::substr(%s, 0, %preL);
	else
		return %s;
}

// String::prefix returns prefix of %s up to first occurrence of %split (or %s if no occurrence).
// If %split == "", %split = " " is used.
function String::prefix(%s, %split) {
	%split = def(%split, " ");
	if ((%idx = String::findSubStr(%s, %split)) == -1) return %s;
	return String::substr(%s, 0, %idx);
}
// String::suffix returns suffix of %s after first occurrence of %split (or "" if no occurrence).
// If %split == "", %split = " " is used.
function String::suffix(%s, %split) {
	%split = def(%split, " ");
	if ((%idx = String::findSubStr(%s, %split)) == -1) return "";
	return String::substr(%s, %idx + String::len(%split));
}