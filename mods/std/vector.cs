
function Vector::scale(%v, %m) {
	return (getWord(%v, 0) * %m) @ " " @ (getWord(%v, 1) * %m) @ " " @ (getWord(%v, 2) * %m);
}