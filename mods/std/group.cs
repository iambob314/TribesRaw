function Group::contains(%g, %obj) {
	if (getGroup(%obj) == %g)
		return true;
	for (%i = 0; (%obj2 = Group::getObject(%g, %i)) != -1; %i++)
		if (%obj == %obj2)
			return true;
	return false;
}

function Group::len(%g) {
	if (Group::getObject(%g, 0) == -1) return 0;
	
	%lb = 0; // invariant: %lb <= len
	%ub = 1;
	
	// Gallop forward
	while (Group::getObject(%g, %ub) != -1) {
		%lb = %ub;
		%ub *= 2;
	}

	// Binary search back
	while (%lb < %ub - 1) {
		%mb = (%lb + %ub) / 2 | 0;
		if (Group::getObject(%g, %mb) != -1) %lb = %mb;
		else                                 %ub = %mb;
	}
	return %ub;
}
