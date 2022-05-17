//
// Each marker in the finished graph will have the following instance variables:
//
//  %marker.edges : number of edges
//  %marker.edges[%i] : the %ith edge, the node id of the destination marker
//  %marker.edges[%i, "type"] : the type of the %ith edge, "" if it is a normal edge, or the type otherwise
//  %marker.edges[%i, "other vars"] : type-specific variables of the %ith edge
//

////////////////////
// Utility functions
////////////////////

function AIGraph::linkMarkers(%marker1, %marker2) {
  %marker1.edges[%marker1.edges++ - 1] = %marker2.id;
  %marker2.edges[%marker2.edges++ - 1] = %marker1.id;
}

function AIGraph::unlinkMarkers(%marker1, %marker2) {
  for (%i = 0; %i < %marker1.edges; %i++) {
    if (%marker1.edges[%i] == %marker2.id) {
      %marker1.edges[%i] = "";
      %marker1.edges--;
      break;
    }
  }

  for (1; %i < %marker1.edges; %i++) {
    %marker1.edges[%i] = %marker1.edges[%i+1];
  }

  for (%i = 0; %i < %marker2.edges; %i++) {
    if (%marker2.edges[%i] == %marker1.id) {
      %marker2.edges[%i] = "";
      %marker2.edges--;
      break;
    }
  }

  for (1; %i < %marker2.edges; %i++) {
    %marker2.edges[%i] = %marker2.edges[%i+1];
  }
}

///////////////////////
// Generation functions
///////////////////////

function AIGraph::generateGraph(%markerGroup) {
  AIGraph::assignIDs(%markerGroup);

  %connectedGroups = AIGraph::generateConnectedGroups(%markerGroup);

  %edgeHeap = AIGraph::calculateEdges(%markerGroup);

  AIGraph::buildGraph(%markerGroup, %connectedGroups, %edgeHeap);

  deleteObject(%connectedGroups);
}

function AIGraph::assignIDs(%markerGroup) {
  for (%i = 0; %i < Group::objectCount(%markerGroup); %i++) {
    Group::getObject(%markerGroup, %i).id = %i;
  }
}

function AIGraph::generateConnectedGroups(%markerGroup) {
  %cgroups = newObject("", SimSet);

  function addit(%object, %set) {
    %set2 = newObject("", SimSet);
    addToSet(%set2, %object);
    addToSet(%set, %set2);

    %object.connectedGroup = %set2;
  }

  Group::iterateRecursive(%markerGroup, "addit", %cgroups);

  deleteFunctions("addit");

  return %cgroups;
}

function AIGraph::calculateEdges(%markerGroup) {
  %edgeHeap = Heap::new();

  %n = Group::objectCount(%markerGroup);
  for (%i = 0; %i < %n; %i++) {
    %v1 = Group::getObject(%markerGroup, %i);
    %pos1 = GameBase::getPosition(%v1);
    for (%j = %i + 1; %j < %n; %j++) {
      %v2 = Group::getObject(%markerGroup, %j);
      %pos2 = GameBase::getPosition(%v2);
      %dist = Vector::getDistance(%pos1, %pos2);

      Heap::add(%edgeHeap, %dist @ " " @ %v1 @ " " @ %v2);
    }
  }

  return %edgeHeap;
}

function AIGraph::buildGraph(%markerGroup, %connectedGroups, %edgeHeap) {

  function moveObjectTo(%obj, %set) {
    addToSet(%set, %obj);
    %obj.connectedGroup = %set;
  }

  %los = newObject("", StaticShape, LOSer, true);

  while (Group::objectCount(%connectedGroups) > 1) {
    %edge = Heap::removeMin(%edgeHeap);
    if (%edge == "") {
      echo("Could not complete AI graph :(");
      break;
    }

    %dist = getWord(%edge, 0);
    %v1 = getWord(%edge, 1);
    %v2 = getWord(%edge, 2);

    if (%v1.connectedGroup != %v2.connectedGroup) {
      %pos1 = GameBase::getPosition(%v1);
      %pos2 = GameBase::getPosition(%v2);

      GameBase::setPosition(%los, %pos1);
      if (GameBase::getLOSInfo(%los, %dist, Vector::getRot(Vector::sub(%pos2, %pos1)))) {
        if (Vector::getDistance(%pos1, $los::position) < %dist) continue;
      }

      %oldGroup = %v1.connectedGroup;
      Group::iterateRecursive(%v1.connectedGroup, "moveObjectTo", %v2.connectedGroup);
      deleteObject(%oldGroup);

      AIGraph::linkMarkers(%v1, %v2);
    }
  }

  for (%i = 0; %i < Group::objectCount(%markerGroup); %i++) Group::getObject(%markerGroup, %i).connectedGroup = "";

  deleteObject(%edgeHeap);
  deleteObject(%los);

  deleteFunctions("moveObject");
}

///////////////////
// Editor functions
///////////////////

function AIGraphEditor::linkMarkers() {
  if (!$ME::SelectionSet) return;
  for (%i = 0; %i < Group::objectCount($ME::SelectionSet); %i++) {
    %o = Group::getObject($ME::SelectionSet, %i);
    if (getObjectType(%o) != "Marker" || Object::getName(getGroup(%o)) != "AIGraph") return;
  }

  for (%i = 0; %i < Group::objectCount($ME::SelectionSet); %i++) {
    for (%j = %i+1; %j < Group::objectCount($ME::SelectionSet); %j++) {
      AIGraph::linkMarkers(Group::getObject($ME::SelectionSet, %i), Group::getObject($ME::SelectionSet, %j));
    }
  }

  echo("LINKED");
}

function AIGraphEditor::makeGrappleEdge(%targetWidth, %targetHeight) {
  
}

///////////////////////
// Inspection functions
///////////////////////

// Node id, not object id
function AIGraph::getNodeWithID(%markerGroup, %id) {
  return Group::getObject(%markerGroup, %id);
}

function AIGraph::getNumEdges(%marker) { return %marker.edges; }
function AIGraph::getEdgeMarker(%marker, %edge) { return AIGraph::getNodeWithID(getGroup(%marker), %marker.edges[%edge]); }
function AIGraph::getEdgeType(%marker, %edge) { return %marker.edges[%edge, "type"]; }
function AIGraph::getEdgeArg(%marker, %edge, %arg) { return %marker.edges[%edge, %arg]; }

function AIGraph::getHelperObject(%marker, %helper) { return Group::getObject(getGroup(%marker) @ "\\HelperObjects", %helper); }

////////////////////////
// Exploration functions
////////////////////////

function AIGraph::getConnectedMarkers(%markerGroup, %marker) {
  %set = newObject("", SimSet);

  for (%i = 0; %i < AIGraph::getNumEdges(%marker); %i++) {
    addToSet(%set, AIGraph::getEdgeMarker(%marker, %i));
  }

  return %set;
}

// TODO:
function AIGraph::findPath(%markerGroup, %marker1, %marker2) {
  
}

// TODO:
function AIGraph::colorPath(%marker) {
  for (%i = 0; %i < AIGraph::getNumEdges(%marker); %i++) {
    
  }
}

//////////////////
// Other functions
//////////////////

$AIGraph::displayGroup = "";
function AIGraph::displayGraph(%markerGroup) {
  %los = newObject("", StaticShape, LOSer, true);
  %block = newObject("", StaticShape, CargoCrate, true);

  if ($AIGraph::displayGroup) {
    deleteObject($AIGraph::displayGroup);
    $AIGraph::displayGroup = "";
  }

  %g = newObject("LasersAndMarkers", SimGroup);

  for (%i = 0; %i < Group::objectCount(%markerGroup); %i++) {
    %m = Group::getObject(%markerGroup, %i);
    %pos1 = GameBase::getPosition(%m);
    GameBase::setPosition(%los, %pos1);

    %mark = newObject("", StaticShape, SpyTestage::CoffeeCup, true);
    GameBase::setPosition(%mark, %pos1);
    addToSet(%g, %mark);

    for (%j = 0; %j < %m.edges; %j++) {
      %pos2 = GameBase::getPosition(AIGraph::getNodeWithID(%markerGroup, %m.edges[%j]));

      GameBase::setPosition(%block, Vector::add(%pos2, "0 0 -1"));
      %p = Projectile::spawnProjectile(RedLaser, "1 0 0 " @ Vector::sub(%pos2, %pos1) @ " 0 0 1 " @ %pos1, %los, 0);
      addToSet(%g, %p);
    }
  }
  deleteObject(%los);
  deleteObject(%block);

  $AIGraph::displayGroup = %g;
  if (isObject(MissionCleanup)) addToSet(MissionCleanup, %g);
}

function AIGraph::clearGraph(%markerGroup) {
  function clearit(%object) {
    for (%i = 0; %i < %object.edges; %i++) %object.edges[%i] = "";
    %object.edges = "";
    %object.id = "";
  }
  Group::iterateRecursive(%markerGroup, "clearit");
  deleteFunctions("clearit");
}
