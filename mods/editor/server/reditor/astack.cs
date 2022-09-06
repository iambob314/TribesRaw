//
// Undo/redo stack and action management
//
// An "action" is a string "actionName [args]", with actionName a valid identifier
// (args is the rest of the string; it may be multiple words or may be omitted entirely).
//
// Each action "actionName" defines these functions (%c = client ID, %args = as above):
// * REditor::action::actionName::do(%c, %args)      : execute, cleanup, return inverse action
// * REditor::action::actionName::cleanup(%c, %args) : cleanup action without executing
//
// It's also idiomatic to have:
// * REditor::action::actionName::make(%c, ...)      : return new action (args action-specific)
//
// TODO: max undo stack depth, action augmenting (i.e. multiple moves)
//

$REditor::undo = 0;
$REditor::redo = 1;
function REditor::astack(%c, %which) {
	%which = def(%which, $REditor::undo);
	if (%c.astack[%which] == "") %c.astack[%which] = anew();
	return %c.astack[%which];
}

function REditor::astack::doAndPush(%c, %action) {
	REditor::astack::clear(%c, $REditor::redo);
	
	%inv = REditor::action::invokeFunc(do, %c, %action);
	if (%inv != "") apush(%inv, REditor::astack(%c, $REditor::undo));
}

function REditor::astack::popAndDo(%c, %which) {
	if ((%action = apop(REditor::astack(%c, %which))) == "") return;

	%inv = REditor::action::invokeFunc(do, %c, %action);
	if (%inv != "") apush(%inv, REditor::astack(%c, 1 - %which));
}

function REditor::astack::clear(%c, %which) {
	%astack = REditor::astack(%c, %which);
	ado(REditor::action::invokeFunc, cleanup, %c, %astack);
	adel(%astack);
}

function REditor::astack::print(%c, %which) {
	%arr = REditor::astack(%c, %which);
	ado(echo, %arr);
}

// REditor::action::invokeFunc invokes delegate %funcBase (do, cleanup) for %action.
function REditor::action::invokeFunc(%funcBase, %c, %action) {
	%func = "REditor::action::" @ String::prefix(%action, " ") @ "::" @ %funcBase;
	return invoke(%func, %c, String::suffix(%action, " "));
}
