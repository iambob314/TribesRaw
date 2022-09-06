//
// Click vs. click-and-drag support for action map. Call Mouse::down/up
// on mouse make/break to engage.
//
// Two cases:
// * If a click occurs (no drag), Mouse::onClick is called
// * If a drag initiaties, Mouse::onDrag is called repeatedly until either
//   it returns false (cancels the drag) or the drag completes
//
// During a drag, Mouse::onDrag(%start) is called with %start = true on the
//  first call and false afterward.
//

$Mouse::dragDelay = 0.5;
$Mouse::dragInterval = 0.25;
$Mouse::dragSched = "";
function Mouse::down() {
	Mouse::cancelDrag();
	$Mouse::dragSched = newObject("", SimSet);
	
	schedule("Mouse::drag(true);", $Mouse::dragDelay, $Mouse::dragSched);
}
function Mouse::up() {
	%dragged = $Mouse::dragSched.dragged;
	Mouse::cancelDrag();

	if (!%dragged) Mouse::onClick();
}

function Mouse::drag(%start) {
	if (!Mouse::onDrag(%start)) { Mouse::cancelDrag(); return; }
	$Mouse::dragSched.dragged = true;
	schedule("Mouse::drag(false);", $Mouse::dragInterval, $Mouse::dragSched);
}

function Mouse::cancelDrag() {
	if ($Mouse::dragSched != "") {
		deleteObject($Mouse::dragSched);
		$Mouse::dragSched = "";
	}
}