//
// --- Object ID pool ---
//
// Object types are registered through the Object::registerObjectType function, which will allocate an ID range of the length supplied for the new type.
//
// Objects may then be constructed using the Object::newObject function by supplying a type. The new object's ID is returned.
//
// Objects should be destroyed with Object::destroyObject function when they are no longer needed, to free up memory and ID numbers.
//
//

$Object::nextID = (1<<14); // 16384 - Safely out of the Tribes object range

// $Object::IDRange[%type]
// $Object::nextID[%type]

function Object::registerObjectType(%type, %numObjs) {
  $Object::IDRange[%type] = $Object::nextID @ " " @ ($Object::nextID + %numObjs);
  $Object::nextID[%type] = $Object::nextID;
  $Object::nextID += %numObjs;

  $Object::numConstructorArgs[%type] = -1;
}

function Object::registerConstructor(%type, %numArgs) {
  $Object::numConstructorArgs[%type] = %numArgs;
}

function Object::registerMemberMethod(%type, %method, %numArgs) {
  %evalStr = "function dot_op_" @ %method @ "(%this";

  for (%i = 0; %i < %numArgs; %i++) %evalStr = %evalStr @ ",%a" @ %i;

  %evalStr = %evalStr @ ") {";

  %evalStr = %evalStr @ "Object::callOn(%this,\""@%method@"\","@%numArgs;

  for (%i = 0; %i < %numArgs; %i++) %evalStr = %evalStr @ ",%a" @ %i;

  %evalStr = %evalStr @ ");}";

  eval(%evalStr);
}

// Constructs a new object of type %type and returns the ID. If the object could not be constructed, returns -1.
function Object::newObject(%type, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9) {
  %lowerID = getWord($Object::IDRange[%type], 0);
  %upperID = getWord($Object::IDRange[%type], 1);

  for (%id = $Object::nextID[%type]; %id < %upperID; %id++)
    if ($Objects[%id] == "") break;

  if (%id == %upperID) {
    for (%id = %lowerID; %id < $Object::nextID[%type]; %id++)
      if ($Objects[%id] == "") break;

    if (%id == $Object::nextID[%type]) return -1;
  }

  $Objects[%id] = %type;

  if ($Object::numConstructorArgs[%type] != -1) {
    Object::callOn(%id, %type, $Object::constructorArgs[%type], %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9);
  }

  return %id;
}

function Object::destroyObject(%id) {
  deleteVariables("$Objects" @ %id @ "*");  
}



function Object::get(%id, %field) {
  return $Objects[%id, %field];
}

function Object::set(%id, %field, %val) {
  return $Objects[%id, %field] = %val;
}



function Object::getType(%x) {
  if (%x >= 0 && %x < (1<<14)) {
    %name = GameBase::getDataName(%x);
    if (%name == "") %name = getObjectType(%x);
    return %name;
  }

  return $Objects[%x];
}

function Object::callOn(%x, %func, %numArgs, %a0, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9) {
  %evalStr = Object::getType(%x) @ "::" @ %func @ "(" @ %x;

  for (%i = 0; %i < %numArgs; %i++) %evalStr = %evalStr @ ",\"" @ %a0 @ "\"";

  %evalStr = %evalStr @ ");";

  eval(%evalStr);
}