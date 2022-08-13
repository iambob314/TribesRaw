// ME/TED actions, bound to controls or UI elements

function Editor::ctxAction(%me, %ted) {
	%mode = EditorUI::getMode();
	if (%me != "" && (%mode == Create || %mode == Inspect)) invoke(%me);
	if (%ted != "" && %mode == Ted) invoke(%ted);
}

function Editor::deleteSelection() { Editor::ctxAction( ME::DeleteSelection,    ""                  ); }
function Editor::cutSelection()    { Editor::ctxAction( ME::CutSelection,       ""                  ); }
function Editor::copySelection()   { Editor::ctxAction( ME::CopySelection,      Ted::copy           ); }
function Editor::pasteSelection()  { Editor::ctxAction( ME::PasteSelection,     Ted::floatPaste     ); }
function Editor::dropSelection()   { Editor::ctxAction( ME::DropSelection,      ""                  ); }
function Editor::dupSelection()    { Editor::ctxAction( ME::DuplicateSelection, Ted::floatSelection ); }
function Editor::undo()            { Editor::ctxAction( ME::Undo,               Ted::undo           ); }
function Editor::redo()            { Editor::ctxAction( ME::Redo,               Ted::redo           ); }
