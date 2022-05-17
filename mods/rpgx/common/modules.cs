$Module::modules = 0;
// $Module::modules[%i] = %i'th module name

// $Module::isValid[%mod] = T/F

// $Module::moduleEventListeners[%mod] = num event listeners
// $Module::moduleEventListeners[%mod, %i] = %i'th event listener

function Module::begin(%moduleName) {
  $Module::modules[$Module::modules] = %moduleName;
  $Module::modules++;

  $Module::isValid[%moduleName] = true;

  $Module::moduleEventListeners[%moduleName] = 0;
}

function Module::end() {

}

function Module::isValidModule(%module) {
  return $Module::isValid[%module];
}


function Module::addEventListener(%module, %event) {
  if (!Module::isValidModule(%module)) {
    echo("Error: attempting to add invalid module ", %module, " as listener to event ", %event);
    return;
  }

  if (!Event::isValidEvent(%event)) {
    echo("Error: attempting to add module ", %module, " as listener to invalid event ", %event);
    return;
  }

  $Module::moduleEventListeners[%module, $Module::moduleEventListeners[%module]] = %event;
  $Module::moduleEventListeners[%module]++;

  Event::registerListener(%event, %module);
}



function Module::listModules() {
  for (%i = 0; %i < $Module::modules; %i++) {
    echo($Module::modules[%i]);
  }
}
