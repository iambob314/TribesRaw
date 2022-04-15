$PI = 3.141592653589793238;

function tern(%v, %a, %b) {
  if (%v) return %a;
  else    return %b;
}

function abs(%x) {
  if (%x >= 0) return %x;
  else return -%x;
}

function min(%a, %b) {
  if (%a < %b) return %a;
  else return %b;
}

function max(%a, %b) {
  if (%a > %b) return %a;
  else return %b;
}

function floor(%x) {
  %f = %x | 0;
  if (%f == %x || %x > 0) return %f;
  else return %f - 1;
}

function ceil(%x) {
  return -floor(-%x);
}

function round(%x) {
  return floor(%x + 0.5);
}

function rand(%a, %b) {
  return getRandom() * (%b - %a) + %a;
}

function sin(%angle) { return -getWord(Vector::getFromRot("0 0 " @ %angle), 0); }
function cos(%angle) { return getWord(Vector::getFromRot("0 0 " @ %angle), 1); }
//function atan(%x) { return %x - %x/3 + %x/5 - %x/7 + %x/9 - %x/11; }
function atan(%x) { if (%x == 1) return $PI/4; return getWord(Vector::getRotation(-%x @ " 1 0"), 2); }

// %d = dist to target, %h = height difference to target (%h>0 -> target is above)
// %g = accel due to gravity, %v = launch velocity
function calcAngle(%d, %h, %g, %v, %high, %allowDownward) {
  %d2 = %d * %d;
  %q = %g*%d2/(%v*%v);
  %deter = %d2 - %q*(%q-%h);
  if (%deter < 0) return "NaN";
  %x1 = (-%d + sqrt(%deter)) / %q;
  %x2 = (-%d - sqrt(%deter)) / %q;

  %x = tern(%high, max(%x1, %x2), min(%x1, %x2));

  if (%x < 0) %x = tern(!%high, max(%x1, %x2), min(%x1, %x2));

  if (%x < 0 && !%allowDownward) return "NaN";

  echo(%x1, ",", %x2);
  if (%x > 1) return $PI/2 - atan(1/%x);
  else        return atan(%x);
}

function randomItem(%num, %an0, %an1, %an2, %an3, %an4, %an5, %an6, %an7, %an8) {
  return %an[floor(getRandom() * %num)];
}

function String::length(%str) {
  for (%i = 1; %i <= 1<<16; %i <<= 1) {
    if (String::getSubStr(%str, %i-1, 1) == "") break;
  }

  %dir = -1;
  for (%j = %i>>2; %j > 0; %j >>= 1) {
    %i += %dir * %j;
    if (String::getSubStr(%str, %i-1, 1) == "") %dir = -1;
    else                                        %dir = 1;
  }

  return tern(%dir == 1, %i, %i-1);

  return -1;
//  for (%i = 0; %i < 10240; %i++) {
//    if (String::getSubStr(%str, %i, 1) == "") return %i;
//  }
//  return -1;
}

$String::vowels[0] = "a";
$String::vowels[1] = "e";
$String::vowels[2] = "i";
$String::vowels[3] = "o";
$String::vowels[4] = "u";
function aOrAn(%str, %caps) {
  %x = String::getSubStr(%str, 0, 1);
  for (%i = 0; $String::vowels[%i] != ""; %i++)
    if (String::ICompare(%x, $String::vowels[%i]) == 0)
      return tern(%caps, "An", "an");

  return tern(%caps, "A", "a");
}

function String::replace(%str, %a, %b) {
  %len = String::length(%a);
  %strLeft = %str;
  while ((%ind = String::findSubStr(%strLeft, %a)) != -1) {
    %newStr = %newStr @ String::getSubStr(%strLeft, 0, %ind) @ %b;
    %strLeft = String::getSubStr(%strLeft, %ind + %len, 10240);
  }
  %newStr = %newStr @ %strLeft;
  return %newStr;
}

$escapeStuff["0"] = "0";
$escapeStuff["1"] = "1";
$escapeStuff["2"] = "2";
$escapeStuff["3"] = "3";
$escapeStuff["4"] = "4";
$escapeStuff["5"] = "5";
$escapeStuff["6"] = "6";
$escapeStuff["7"] = "7";
$escapeStuff["8"] = "8";
$escapeStuff["9"] = "9";
$escapeStuff["K"] = "A";
$escapeStuff["L"] = "B";
$escapeStuff["M"] = "C";
$escapeStuff["N"] = "D";
$escapeStuff["O"] = "E";
$escapeStuff["P"] = "F";

function String::escapeGood(%str) {
  %str = escapeString(%str);
  %strLeft = %str;
  while ((%ind = String::findSubStr(%str, "\\x")) != -1) {
    %newStr = %newStr @ String::getSubStr(%str, 0, %ind) @ "\\x" @
                        $escapeStuff[String::getSubStr(%str, %ind + 2, 1)] @ 
                        $escapeStuff[String::getSubStr(%str, %ind + 3, 1)];
    %strLeft = String::getSubStr(%str, %ind + 4, 1024);
  }
  %newStr = %newStr @ %strLeft;
  return %newStr;
}

function char(%str) { return String::ICompare(%str, ""); }

function getSuffix(%i) {
  if (floor((%i % 100)/10) == 1) return "th";

  if (%i%10 == 1) return "st";
  if (%i%10 == 2) return "nd";
  if (%i%10 == 3) return "rd";
  return "th";
}

// Add *print lock functionality
function bottomprint(%clientId, %msg, %timeout, %lock) {
   %time = getSimTime();
   if (%clientId.printlockTill != "" && %clientId.printlockTill > %time) return;
   if (%timeout == "")
      %timeout = 5;
   if (%lock) %clientId.printlockTill = %time + %timeout;
   remoteEval(%clientId, "BP", %msg, %timeout);
}

function centerprint(%clientId, %msg, %timeout, %lock) {
   %time = getSimTime();
   if (%clientId.printlockTill != "" && %clientId.printlockTill > %time) return;
   if (%timeout == "")
      %timeout = 5;
   if (%lock) %clientId.printlockTill = %time + %timeout;
   remoteEval(%clientId, "CP", %msg, %timeout);
}

function clearPrintLock(%clientId) {
  %clientId.printlockTill = -1;
}

function isClient(%x) {
  return getObjectType(%x) == "Net::PacketStream";
}

function Client::getByName(%name) {
  for (%c = Client::getFirst(); %c != -1; %c = Client::getNext(%c)) {
    if (String::ICompare(Client::getName(%c), %name) == 0) return %c;
  }
  return -1;
}

function fixecho() {
function echo(%a0,%a1,%a2,%a3,%a4,%a5,%a6,%a7,%a8,%a9) {
  %str = %a0 @ %a1 @ %a2 @ %a3 @ %a4 @ %a5 @ %a6 @ %a7 @ %a8 @ %a9;
  if (String::getSubStr(%str, 1023, 1)) echo("HEX CRASH AVOIDED!");
  else dbecho(1,%str);
}
}

function invoke(%func, %numArgs, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7, %arg8, %arg9) {
  %cmd = %func @ tern(%numArgs > 0, "(\"" @ %arg0 @ "\"", "(");
  for (%i = 1; %i < %numArgs; %i++) %cmd = %cmd @ ",\"" @ %arg[%i] @ "\"";
  %cmd = %cmd @ ");";
  return eval(%cmd);
}

function invoke2(%func, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7, %arg8, %arg9) {
  %cmd = %func @ "(\"" @ %arg0 @ "\",\"" @ %arg1 @ "\",\"" @ %arg2 @ "\",\"" @ %arg3 @ "\",\"" @ %arg4 @ "\",\"" @
                         %arg5 @ "\",\"" @ %arg6 @ "\",\"" @ %arg7 @ "\",\"" @ %arg8 @ "\",\"" @ %arg9 @ "\");";
  return eval(%cmd);
}

// Focus stack
focusServer();
newObject("ServerFocus", SimSet);

function isServerFocused() {
  return isObject(ServerFocus);
}
$focus = 0;

function pushCurrentFocus() {
  pushFocus(isServerFocused());
}

function pushFocus(%isServer) {
  if (%isServer) pushFocusServer();
  else           pushFocusClient();
}

function pushFocusServer() {
  $focus[$focus] = isServerFocused();
  $focus++;
  focusServer();
}

function pushFocusClient() {
  $focus[$focus] = isServerFocused();
  $focus++;
  focusClient();
}

function popFocus() {
  $focus--;
  if ($focus[$focus]) focusServer();
  else                focusClient();
}

function getFocusCount() {
  return $focus;
}

//

deprecated("remoteEval2");
function remoteEval2(%conn, %func, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19, %a20, %a21, %a22, %a23, %a24, %a25, %a26, %a27, %a28, %a29) {
  deprecated();
  return;
  //pushCurrentFocus();
  //remoteEval(%conn, %func, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13, %a14, %a15, %a16, %a17, %a18, %a19, %a20, %a21, %a22, %a23, %a24, %a25, %a26, %a27, %a28, %a29);
  //popFocus();
}

//

function tree() {
  simTreeCreate(tree, MainWindow);
  simTreeAddSet(tree, manager);
}

exec2("util\\vector.cs");
exec2("util\\matrix.cs");
