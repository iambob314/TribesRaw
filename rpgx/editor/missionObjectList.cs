pushFocusServer();
newObject(MESelectionSet, SimSet);
popFocus();

function MissionObjectList::refresh() {
  MissionObjectList::ClearDisplayGroups();
  MissionObjectList::ClearSelection();
  MissionObjectList::AddDisplayGroup(1, MissionGroup);
  MissionObjectList::SetSelMode(1);
}

function MissionObjectList::onSelected(%world, %obj) {
  echo("SELECT " @ %obj);

  if ($ME::InspectObject == "") {
    $ME::InspectObject = %obj;
    $ME::InspectWorld = %world;
    MissionObjectList::Inspect(%world, %obj);
    MissionObjectList::ExpandToObject(%world, %obj);
  }

  pushFocusServer();
  addToSet(MESelectionSet, %obj);
  popFocus();

  ME::onSelected(%world, %obj);
}

function MissionObjectList::onSelectionCleared() {
  echo("SELECTION CLEARED");

  if ($ME::InspectObject != "") {
    MissionObjectList::Inspect($ME::InspectWorld, -1);
    $ME::InspectObject = "";
    $ME::InspectWorld = "";
  }

  pushFocusServer();
  deleteObject(MESelectionSet);
  newObject(MESelectionSet, SimSet);
  popFocus();

  ME::onSelectionCleared();
}

function MissionObjectList::onUnselected(%world, %obj) {
  echo("UNSELECT " @ %obj);

  if ($ME::InspectObject == %obj && $ME::InspectWorld == %world) {
    MissionObjectList::Inspect($ME::InspectWorld, -1);
    $ME::InspectObject = "";
    $ME::InspectWorld = "";
  }

  pushFocusServer();
  removeFromSet(MESelectionSet, %obj);
  popFocus();
   
  ME::onUnselected(%world, %obj);
}