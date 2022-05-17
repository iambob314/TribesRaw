$Event::events = 0;
// $Event::events[%i] = %i'th event

// $Event::isValid[%eventName] = T/F

// $Event::listeners[%eventName] = num listeners
// $Event::listeners[%eventName, %i] = %i'th listener

function Event::registerEvent(%eventName) {
  $Event::events[$Event::events] = %eventName;
  $Event::events++;

  $Event::isValid[%eventName] = true;

  $Event::listeners[%eventName] = 0;
}

function Event::isValidEvent(%event) {
  return $Event::isValid[%event];
}



function Event::registerListener(%event, %module) {
  if (!Module::isValidModule(%module)) {
    echo("Error: attempting to add invalid module ", %module, " as listener to event ", %event);
    return;
  }

  if (!Event::isValidEvent(%event)) {
    echo("Error: attempting to add module ", %module, " as listener to invalid event ", %event);
    return;
  }

  $Event::listeners[%event, $Event::listeners[%event]] = %module;
  $Event::listeners[%event]++;
}

function Event::fireEvent(%event, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7) {
  for (%i = 0; %i < $Event::listeners[%event]; %i++) {
    %listener = $Event::listeners[%event, %i];
    invoke2(%listener @ "::" @ %event, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7);
  }
}



function Event::listEvents() {
  for (%i = 0; %i < $Event::events; %i++) {
    echo($Event::events[%i]);
  }
}
