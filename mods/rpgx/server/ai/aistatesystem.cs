//---------------------------------*
// SuperAI custom scripting engine |
//---------------------------------*
//
// A SuperAI custom script must include the following functions (scriptname is the name of your script):
//   * scriptname::init(%aiName, %marker): This function is called after the the AI is spawn.  You should setup you AI here.
//                                         %marker is a marker object supplied if the ai was spawn from map data.  It should
//                                         contain arguments for the creation of the ai.  If the ai has not spawned from map
//                                         data, this will be -1.
//   * scriptname::onTargetDied(%aiName, %target): This function is called when an AI's target dies for some reason.
//   * scriptname::onTargetLOSAcquired(%aiName, %target): This function is called when AI::onTargetLOSAcquired is called.
//   * scriptname::onTargetLOSLost(%aiName, %target): This function is called when AI::onTargetLOSLost is called.
//   * scriptname::onKilled(%aiName, %this): This function is called when the AI dies.  %this is the player object id.
//   * scriptname::onDamage(%aiName, %shooter, %type, %value): This function is called when the AI is damaged.
//
// Stub:
//
//function scriptname::init(%aiName, %marker) { SuperAI::init(%aiName, %marker); }
//function scriptname::onTargetDied(%aiName, %target) { SuperAI::onTargetDied(%aiName, %target); }
//function scriptname::onTargetLOSAcquired(%aiName, %target) { SuperAI::onTargetLOSAcquired(%aiName, %target); }
//function scriptname::onTargetLOSLost(%aiName, %target) { SuperAI::onTargetLOSLost(%aiName, %target); }
//function scriptname::onAIKilled(%aiName, %this) { SuperAI::onAIKilled(%aiName, %this); }
//function scriptname::onAIDamaged(%aiName,%type,%value,%pos,%vec,%mom,%vertPos,%quadrant,%object) { SuperAI::onAIDamaged(%aiName,%type,%value,%pos,%vec,%mom,%vertPos,%quadrant,%object); }
//
//
// - AI State System
//
// Constants:
//   $AIStateSystem::eventRediect[%scriptName, %stateName, %eventName] = true/false
//
// Each state will have the following methods:
//   <ScriptName>::<StateName>State::enter(%aiName)
//   <ScriptName>::<StateName>State::exit(%aiName)
//   <ScriptName>::<StateName>State::on***(%aiName, ...) // redirected events
//
// Note: your AI's init function should change the AI to some initial state
//
// Note: You should not use SuperAI variable names that begin with an underscore ("_"), as they are reserved
//

deleteVariables("$SuperAI::AIs*");
deleteVariables("$SuperAI::AIData*");

////////////////////
// Spawn functions
////////////////////

function SuperAI::Spawn(%name, %armor, %pos, %rot, %customScript, %marker, %team) {
  if (%armor == "") {
    echo("Usage: SuperAI::Spawn(<name>, <armor type>, <position>, [rotation], [custom script], [marker], [team])");
    return;
  }

  if (%name == "") %name = SuperAI::nextName();

  if (SuperAI::isAIRegistered(%name)) {
    echo("AI with name " @ %name @ " already exists.");
    return;
  }

  SuperAI::registerAI(%name);

  AI::spawn(%name, %armor, %pos, %rot);
  AI::setScriptedTargets(%name);

  %obj = Client::getOwnedObject(AI::getID(%name));
  %obj.aiName = %name;

  SuperAI::setVar(%name, "customScript", %customScript);
  SuperAI::setVar(%name, "ownedObject", %obj);

  SuperAI::callCustom(%name, "init", 2, tern(%marker, %marker, -1), %team);

  return %name;
}

function SuperAI::nextName() { return "SuperAI" @ ($SuperAI::curCounter++); }

// Note: no wildcards!
function SuperAI::Delete(%aiName) {
  SuperAI::changeState(%aiName, "");
  AI::Delete(%aiName);
  SuperAI::deregisterAI(%aiName);
}

////////////////////
// AI name control
////////////////////

function SuperAI::registerAI(%aiName) {
  $SuperAI::AIs[%aiName] = true;
}

function SuperAI::deregisterAI(%aiName) {
  $SuperAI::AIs[%aiName] = "";
}

function SuperAI::isAIRegistered(%aiName) {
  return $SuperAI::AIs[%aiName];
}

//////////////////////////////
// AI state system functions
//////////////////////////////

function SuperAI::getState(%aiName) {
  return SuperAI::getVar(%aiName, "AIState");
}

function SuperAI::getStateObject(%aiName) {
  return SuperAI::getVar(%aiName, "AIStateObject");
}

function SuperAI::changeState(%aiName, %newState, %numArgs, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7) {
  %oldState = SuperAI::getVar(%aiName, "AIState");

  if (%oldState != "") {
    SuperAI::callCustom(%aiName, %oldState @ "State::exit");
    SuperAI::destroyState(%aiName, %oldState);
  }

  SuperAI::setVar(%aiName, "AIState", %newState);

  if (%newState != "") {
    SuperAI::initState(%aiName, %newState);
    SuperAI::callCustom(%aiName, %newState @ "State::enter", %numArgs, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7);
  }
}

function SuperAI::initState(%aiName, %newState) {
  SuperAI::setVar(%aiName, "AIStateObject", newObject("AIStateObject", SimSet));

  SuperAI::setStateVar(%aiName, "nextPeriodicID", 0);
}

function SuperAI::destroyState(%aiName, %oldState) {
  if (isObject(SuperAI::getVar(%aiName, "AIStateObject")))
    deleteObject(SuperAI::getVar(%aiName, "AIStateObject"));

  AI::DirectiveRemove(%aiName, "*");
}

////////////////////////////////////////////
// State scheduling and periodic functions
////////////////////////////////////////////

function SuperAI::stateSchedule(%aiName, %command, %time) {
  schedule(%command, %time, SuperAI::getStateObject(%aiName));
}

// If the periodic function returns a value that does not evaluate to false, %interval will be ignored and the return value will be used for the schedule time instead. If the periodic function always returns its scheduling value,
// %interval need not be specified.
function SuperAI::statePeriodic(%aiName, %funcName, %interval, %delay) {
  %id = SuperAI::incStateVar(%aiName, "nextPeriodicID");
  echo("ID: " @ %id);

  SuperAI::setStateVar(%aiName, "periodic" @ %id, true);
  SuperAI::setStateVar(%aiName, "periodic" @ %id @ "_funcName", %funcName);
  SuperAI::setStateVar(%aiName, "periodic" @ %id @ "_interval", %interval);

  SuperAI::callStatePeriodic(%aiName, %id, %delay);

  return %id;
}

function SuperAI::callStatePeriodic(%aiName, %id, %delay) {
  //SuperAI::getState(%aiName) != SuperAI::getStateVar(%aiName, "periodic" @ %id @ "_state") ||

  if (!SuperAI::getStateVar(%aiName, "periodic" @ %id) ||
      SuperAI::isDead(%aiName)) return;

  %interval = SuperAI::getStateVar(%aiName, "periodic" @ %id @ "_interval");
  %funcName = SuperAI::getStateVar(%aiName, "periodic" @ %id @ "_funcName");

  if (!%delay) {
    %customInterval = invoke(%funcName, 1, %aiName);

    if (%interval <= 0) %interval = %customInterval;
  } else if (%interval < 0) %interval = 0;

  if (%interval < 0) {
    echo("Error: state periodic function " @ %funcName @ " on AI " @ %aiName @ " neither has a specified interval nor was a valid interval returned. Terminating periodic loop.");
    return;
  }

  schedule("SuperAI::callStatePeriodic(\"" @ %aiName @ "\", " @ %id @ ");", %interval, SuperAI::getStateObject(%aiName));
}

function SuperAI::cancelStatePeriodic(%aiName, %id) {
  SuperAI::setStateVar(%aiName, "periodic" @ %id, "");
}

///////////////////////////
// AI directive functions
///////////////////////////

$SuperAI::sdc = 0;
function SuperAI::DirectiveCallback(%aiName, %callback, %directive, %numArgs, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7) {
  %funcName = "sdc" @ ($SuperAI::sdc++);

  %cmd = "function " @ %funcName @ "(%aiName) {" @
         "if (SuperAI::getState(%aiName) != \"" @ SuperAI::getState(%aiName) @ "\") return;" @
         "schedule(\"" @ %callback @ "(\\\"\"@%aiName@\"\\\"";

  for (%i = 0; %i < %numArgs; %i++) %cmd = %cmd @ ",\\\"" @ %arg[%i] @ "\\\"";
  %cmd = %cmd @ ");\",0);" @
                "}";

  eval(%cmd);

  AI::DirectiveCallback1(%aiName, %funcName, %directive);
}

// This waypoint system uses directive block "1XXXXXX"
function SuperAI::DirectiveWaypoint(%aiName, %waypoint) {
  //%directive = SuperAI::incVar(%aiName, "currentWaypointDirective");
  %directive = SuperAI::incVar(%aiName, "currentWaypointDirective");

  AI::DirectiveWaypoint(%aiName, %waypoint @ " 1", (1000000 + %directive)|0);

  return (1000000 + %directive)|0;
}

function SuperAI::DirectiveWaypointGroup(%aiName, %waypointSet) {
  for (%i = 0; %i < Group::objectCount(%waypointSet); %i++) {
    %directive = SuperAI::incVar(%aiName, "currentWaypointDirective");
    AI::DirectiveWaypoint(%aiName, GameBase::getPosition(Group::getObject(%waypointSet, %i)), (1000000 + %directive)|0);
  }

  return (1000000 + %directive)|0; // The last directive
}

function SuperAI::cancelWaypoints(%aiName) {
  AI::DirectiveRemove(%aiName, "1??????");
}

///////////////////////////////////////
// Low level custom scripting functions
///////////////////////////////////////

function SuperAI::callCustom(%aiName, %funcName, %numArgs, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7) {
  %customScript = SuperAI::getVar(%aiName, "customScript");
  if (%customScript == "") %customScript = "SuperAI";
  if (%numArgs == "") %numArgs = 0;

  invoke(%customScript @ "::" @ %funcName, %numArgs+1, %aiName, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7);
}

function SuperAI::fireEvent(%aiName, %eventName, %numArgs, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7) {
  if (SuperAI::isEventRedirected(%aiName, %eventName)) {
    %stateName = SuperAI::getState(%aiName);
    %funcName = %stateName @ "State::on" @ %eventName;
  } else {
    %funcName = "on" @ %eventName;
  }

  SuperAI::callCustom(%aiName, %funcName, %numArgs, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7);
}

function SuperAI::isEventRedirected(%aiName, %eventName) {
  %stateName = SuperAI::getState(%aiName);
  %scriptName = SuperAI::getVar(%aiName, "customScript");
  return $AIStateSystem::eventRedirect[%scriptName, %stateName, %eventName];
}

////////////////////////
// Standard AI callbacks
////////////////////////

function AI::onTargetDied(%aiName, %target) { SuperAI::fireEvent(%aiName, "targetDied", 1, %target); }
function AI::onTargetLOSAcquired(%aiName, %target) { SuperAI::fireEvent(%aiName, "targetLOSAcquired", 1, %target); }
function AI::onTargetLOSRegained(%aiName, %target) { SuperAI::fireEvent(%aiName, "targetLOSAcquired", 1, %target); }
function AI::onTargetLOSLost(%aiName, %target) { SuperAI::fireEvent(%aiName, "targetLOSLost", 1, %target); }
function AI::onAIKilled(%aiName, %this) {
  SuperAI::fireEvent(%aiName, "AIKilled", 1, %this);
  SuperAI::changeState(%aiName, "");
  SuperAI::deregisterAI(%aiName);
}
function AI::onAIDamaged(%aiName,%type,%value,%pos,%vec,%mom,%vertPos,%quadrant,%object) {
  SuperAI::fireEvent(%aiName, "AIDamaged", 8, %type,%value,%pos,%vec,%mom,%vertPos,%quadrant,%object);
}

function AI::onDroneKilled(%aiName) {}
