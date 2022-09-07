//
// Common code for the "creator" editor controls (GroupList/NameList)
//

//
// Defined in {loopback,remote}/gui/create.cs:
// * Editor::onCreateObject(%group, %name)
//

function EditorUI::refreshCreatorLists() {
	TextList::clear(GroupList);
	TextList::clear(NameList);
	ado(EditorRegistry::groups(), TextList::addLine, GroupList); 
}

function GroupList::onAction() {
	TextList::clear(NameList);
	%group = Control::getValue(GroupList);
	ado(EditorRegistry::groupNames(%group), TextList::addLine, NameList);
}

function NameList::onAction() {
	%group = Control::getValue(GroupList);
	%name = Control::getValue(NameList);
	Editor::onCreateObject(%group, %name);
}
