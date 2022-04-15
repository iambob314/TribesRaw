$CreateObjectList::objectTypes = "INT";

function CreateObjectList::populateFilteredINT(%filter, %wildcardBefore, %wildcardAfter) {
  %searchStr = %filter;
  if (%wildcardBefore) %searchStr = "*" @ %searchStr;
  if (%wildcardAfter) %searchStr = %searchStr @ "*";
  %searchStr = %searchStr @ ".dis";

  for (%f = File::findFirst(%searchStr); %f != ""; %f = File::findNext(%searchStr)) {
    TextList::AddLine(CreateObjectList, "INT: " @ File::getBase(%f));
  }
}

function CreateObjectList::populateFilteredERROR(%filter) {}

function CreateObjectList::populateFiltered(%filterWord, %wildcardBefore, %wildcardAfter) {
  TextList::Clear(CreateObjectList);
  for (%i = 0; (%type = getWord($CreateObjectList::objectTypes, %i)) != -1; %i++) {
    invoke2("CreateObjectList::populateFiltered" @ %type, %filterWord, %wildcardBefore, %wildcardAfter);
  }
}



function CreateObjectList::createINT(%objName) {
  %file = File::findFirst(%objName @ ".dis");
  MissionCreateObject(%objName, InteriorShape, %file);
}

function CreateObjectList::createERROR() {}



function CreateObjectSearchButton::onAction() {
  %filterWord = Control::getValue(CreateObjectSearchField);

  while ((%ind = String::findSubStr(%filterWord, "*")) != -1) {
    %len = String::length(%filterWord);
    if (%ind == 0) {
      %wildcardBefore = true;
      %filterWord = String::getSubStr(%filterWord, 1, 1024);
    } else if (%ind == %len - 1) {
      %wildcardAfter = true;
      %filterWord = String::getSubStr(%filterWord, 0, %len - 1);
    } else {
      //TextList::Clear(CreateObjectList);
      //TextList::AddLine(CreateObjectList, "ERROR: Wildcards are only allowed at the beginning or end of the search string");
      break; // Cry now
    }
  }

  CreateObjectList::populateFiltered(%filterWord, %wildcardBefore, %wildcardAfter);
}

function CreateObjectList::onAction() {
  %item = Control::getValue(CreateObjectList);
  %ind = String::findSubStr(%item, ": ");
  %type = tern(%ind != -1, String::getSubStr(%item, 0, %ind), "");
  %obj = tern(%ind != -1, String::getSubStr(%item, %ind+2, 1024), %item);

  echo(%type, ": ", %obj);

  invoke2("CreateObjectList::create" @ %type, %obj);
}
