//
// Undo/redo stack and action management
//
// An "action" is a string "actionName [args]", with actionName a valid identifier
// (args is the rest of the string; it may be multiple words or may be omitted entirely).
//
// Each action "actionName" defines two functions (%c = client ID, %args = as above):
// * REditor::action::actionName::do(%c, %args, %prevAction):
//
//   Execute and cleanup action, then return inverse action, to be pushed on the undo/redo stack.
//   Return "" to indicate no undo/redo to push. The previous top-of-stack is passed as
//   %prevAction (or "" if stack empty); it may be used to decide whether to return "" (e.g.
//   to coalesce with previous undo/redo.
//
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
	
	%undoStack = REditor::astack(%c, $REditor::undo);
	%prev = alast(%undoStack);
	
	%inv = REditor::action::invokeFunc(do, %c, %action, %prev);
	if (%inv != "") apush(%undoStack, %inv);
}

function REditor::astack::popAndDo(%c, %which) {
	if ((%action = apop(REditor::astack(%c, %which))) == "") return;

	%otherStack = REditor::astack(%c, 1 - %which);
	%prev = alast(%otherStack);

	%inv = REditor::action::invokeFunc(do, %c, %action, %prev);
	if (%inv != "") apush(%otherStack, %inv);
}

function REditor::astack::clear(%c, %which) {
	%astack = REditor::astack(%c, %which);
	ado(%astack, REditor::action::invokeFunc, cleanup, %c);
	adel(%astack);
}

function REditor::astack::print(%c, %which) {
	%arr = REditor::astack(%c, %which);
	ado(%arr, echo);
}

// REditor::action::invokeFunc invokes delegate %funcBase (do, cleanup) for %action.
function REditor::action::invokeFunc(%funcBase, %c, %action, %prevAction) {
	%func = "REditor::action::" @ String::prefix(%action, " ") @ "::" @ %funcBase;
	return invoke(%func, %c, String::suffix(%action, " "), %prevAction);
}
