Object::registerObjectType("Stack", 1000);

function Stack::Stack(%this) {
  $Objects[%this, "index"] = 0;
}

function Stack::push(%this, %val) {
  $Objects[%this, "data", $Objects[%this, "index"]] = %val;
  $Objects[%this, "index"]++;
}

function Stack::pop(%this) {
  if ($Objects[%this, "index"] == 0) return "";

  $Objects[%this, "index"]--;
  return $Objects[%this, "data", $Objects[%this, "index"]];
}

function Stack::peek(%this) {
  if ($Objects[%this, "index"] == 0) return "";

  return $Objects[%this, "data", $Objects[%this, "index"]-1];
}

function Stack::size(%this) {
  return $Objects[%this, "index"];
}

Object::registerConstructor("Stack", 0);
Object::registerMemberMethod("Stack", "push", 1);
Object::registerMemberMethod("Stack", "pop", 0);
Object::registerMemberMethod("Stack", "peek", 0);
Object::registerMemberMethod("Stack", "size", 0);


