Object::registerObjectType("Queue", 1000);

// --- Double-ended queue ---
//
//  Front: 0-A-B-C-0 :Back
//         F     B
// (F,B]

function Queue::Queue(%this) {
  $Objects[%this, "front"] = -1;
  $Objects[%this, "back"] = -1;
}

function Queue::pushBack(%this, %val) {
  $Objects[%this, "back"]++;
  $Objects[%this, "data", $Objects[%this, "back"]] = %val;
}

function Queue::pushFront(%this, %val) {
  $Objects[%this, "data", $Objects[%this, "front"]] = %val;
  $Objects[%this, "front"]--;
}

function Queue::popBack(%this) {
  if (!Queue::size(%this)) return "";

  %v = $Objects[%this, "data", $Objects[%this, "back"]];
  $Objects[%this, "data", $Objects[%this, "back"]] = "";

  $Objects[%this, "back"]--;

  return %v;
}

function Queue::popFront(%this) {
  if (!Queue::size(%this)) return "";

  $Objects[%this, "front"]++;

  %v = $Objects[%this, "data", $Objects[%this, "front"]];
  $Objects[%this, "data", $Objects[%this, "front"]] = "";

  return %v;
}

function Queue::peekBack(%this) {
  if (!Queue::size(%this)) return "";

  return $Objects[%this, "data", $Objects[%this, "back"]];
}

function Queue::peekFront(%this) {
  if (!Queue::size(%this)) return "";

  return $Objects[%this, "data", $Objects[%this, "front"]+1];
}

function Queue::size(%this) {
  return $Objects[%this, "back"] - $Objects[%this, "front"];
}

Object::registerConstructor("Queue", 0);
Object::registerMemberMethod("Queue", "pushFront", 1);
Object::registerMemberMethod("Queue", "pushBack", 1);
Object::registerMemberMethod("Queue", "popFront", 0);
Object::registerMemberMethod("Queue", "popBack", 0);
Object::registerMemberMethod("Queue", "peekFront", 0);
Object::registerMemberMethod("Queue", "peekBack", 0);
Object::registerMemberMethod("Queue", "size", 0);


