//
// Basic math functions
//

function min(%x, %y) { return tern(%x < %y, %x, %y); }
function max(%x, %y) { return tern(%x > %y, %x, %y); }

$PI = 3.1415926535897932;
function deg2rad(%d) { return %d * ($PI / 180); }
function rad2deg(%r) { return %r * (180 / $PI); }

function sin(%angle) { return -getWord(Vector::getFromRot("0 0 " @ %angle), 0); }
function cos(%angle) { return getWord(Vector::getFromRot("0 0 " @ %angle), 1); }
