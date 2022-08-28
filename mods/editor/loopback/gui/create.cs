
function EditorUI::Create::show() {
	if (EditorUI::loadGUI()) {
		Editor::focusInput(ME);
	}
	EditorUI::showOnlyControls("MEObjectList Creator SaveBar");
}

function EditorUI::refreshCreatorLists() {
	TextList::clear(GroupList);
	TextList::clear(NameList);
	
	for (%group = aitfirst(ME::objgroups); !aitdone(ME::objgroups); %group = aitnext(ME::objgroups))
		TextList::addLine(GroupList, %group);
}

//
// GUI controls
//

function GroupList::onAction() {
	TextList::clear(NameList);
	%group = Control::getValue(GroupList);

	%namesArr = "ME::objgroup_" @ %group @ "::names";
	for (%name = aitfirst(%namesArr); !aitdone(%namesArr); %name = aitnext(%namesArr))
		TextList::AddLine(NameList, %name);
}

function NameList::onAction() {
	%group = Control::getValue(GroupList);
	%name = Control::getValue(NameList);
	%namesArr = "ME::objgroup_" @ %group @ "::names";
	%scriptsArr = "ME::objgroup_" @ %group @ "::scripts";

	%idx = afind(%name, %namesArr);
	if (%idx == -1)
		echos("ohnoes", %group, %name);

	%script = aget(%idx, %scriptsArr);
	echos("TODO", %idx, %name, %script);
	%x = eval(%script);
	echo(%x);
}

// MissionObjectList in inspect.cs