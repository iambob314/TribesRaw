//
// Basic math functions
//

function min(%x, %y) { return tern(%x < %y, %x, %y); }
function max(%x, %y) { return tern(%x > %y, %x, %y); }

$PI = 3.1415926535897932;
function deg2rad(%d) { return %d * ($PI / 180); }
function rad2deg(%r) { return %r * (180 / $PI); }
