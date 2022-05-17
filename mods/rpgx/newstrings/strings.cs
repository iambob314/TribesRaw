$SimTag::Start[0] = 09000000;
$SimTag::Index[0] = 09000001;
$SimTag::End[0] = ~(1<<31);

$SimTag::rangeStack = Object::newObject("Stack");
$SimTag::rangeStack.push(0);

function SimTag::defineRange(%type, %name, %desc, %size) {
  %parent = $SimTag::rangeStack.peek();

  echo("--- DEFINING RANGE " @ %type @ "." @ %name @ " ---");
  echo("Parent: " @ %parent);
  echo("Tags remaining in parent: " @ (($SimTag::End[%parent] - $SimTag::Index[%parent])|0));

  if ((($SimTag::End[%parent] - $SimTag::Index[%parent])|0) < %size) {
    echo("Error defining SimTag " @ %name @ " of type " @ %type @ ": insufficient tags in parent range.");
    return;
  }

  %start = $SimTag::Index[%parent]|0;
  %end = ($SimTag::Index[%parent] + %size - 1)|0;

  echo("Start: " @ %start);
  echo("End: " @ %end);

  eval("ID"@%type@"_BEG_" @ %name @ " = " @ %start @ ",\"" @ %desc @ "\";");
  eval("ID"@%type@"_END_" @ %name @ " = " @ %end @ ",\"" @ %desc @ "\";");

  $SimTag::Start[%start] = %start;
  $SimTag::End[%start] = %end;
  $SimTag::Index[%start] = (%start + 1)|0;

  $SimTag::Index[%parent] = ($SimTag::Index[%parent] + %size)|0;

  $SimTag::rangeStack.push(%start);
}

function SimTag::defineRegion(%name, %desc, %size) { SimTag::defineRange("RGN", %name, %desc, %size); }
function SimTag::defineResource(%name, %desc, %size) { SimTag::defineRange("RES", %name, %desc, %size); }
function SimTag::defineData(%name, %desc, %size) { SimTag::defineRange("DAT", %name, %desc, %size); }

function SimTag::endRange(%type, %name) {
  %tag = eval("%x = ID"@%type@"_BEG_"@%name@";");

  if ($SimTag::rangeStack.peek() != %tag) {
    echo("Error ending range " @ %name @ " of type " @ %type @ ": unclosed range: " @ (*$SimTag::rangeStack.peek()));
    return;
  }

  $SimTag::rangeStack.pop();
}

function SimTag::endRegion(%name) { SimTag::endRange("RGN", %name); }
function SimTag::endResource(%name) { SimTag::endRange("RES", %name); }
function SimTag::endData(%name) { SimTag::endRange("DAT", %name); }

function SimTag::defineStrictRange(%type, %name, %desc, %start, %end) {
  %parent = $SimTag::rangeStack.peek();

  echo("--- DEFINING STRICT RANGE " @ %type @ "." @ %name @ " ---");
  echo("Parent: " @ %parent);

  if ($SimTag::Index[%parent] > %start || $SimTag::End[%parent] <= %end) {
    echo("Error defining SimTag " @ %name @ " of type " @ %type @ ": insufficient tags in parent range.");
    return;
  }

  echo("Start: " @ %start);
  echo("End: " @ %end);

  eval("ID"@%type@"_BEG_" @ %name @ " = " @ %start @ ",\"" @ %desc @ "\";");
  eval("ID"@%type@"_END_" @ %name @ " = " @ %end @ ",\"" @ %desc @ "\";");

  $SimTag::Start[%start] = %start;
  $SimTag::End[%start] = %end;
  $SimTag::Index[%start] = (%start + 1)|0;

  $SimTag::Index[%parent] = (%end + 1)|0;

  $SimTag::rangeStack.push(%start);
}

function SimTag::defineStrictRegion(%name, %desc, %start, %end) { SimTag::defineRange("RGN", %name, %desc, %start, %end); }
function SimTag::defineStrictResource(%name, %desc, %start, %end) { SimTag::defineRange("RES", %name, %desc, %start, %end); }
function SimTag::defineStrictData(%name, %desc, %start, %end) { SimTag::defineRange("DAT", %name, %desc, %start, %end	); }




function SimTag::getTag(%name, %type) {
  return eval("%x = ID"@%type@"_BEG_"@%name@";");
}

function SimTag::nextTag(%parent) {
  if (%parent == "") %parent = $SimTag::rangeStack.peek();

  if ($SimTag::Index[%parent] == $SimTag::End[%parent]) {
    echo("Error allocating SimTag in range " @ (*%parent) @ ": insufficient tags.");
  }

  $SimTag::Index[%parent] = ($SimTag::Index[%parent] + 1)|0;

  return ($SimTag::Index[%parent] - 1)|0;
}

function dot_op_nextTag(%parent) {
  return SimTag::nextTag(%parent);
}

exec("strings\\darkstar.newstrings.cs");
