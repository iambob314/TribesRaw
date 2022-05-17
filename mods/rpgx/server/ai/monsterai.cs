$AIStateSystem::eventRediect[MonsterAI, "", onAIKilled] = true;

function MonsterAI::init(%aiName, %marker) {
  AI::SetAutomaticTargets(%aiName);
  Player::mountItem(AI::getID(%aiName), Shotgun, 0);
  AI::setVar(%aiName, iq, 1);
  AI::setVar(%aiName, triggerPct, 1);
}
function MonsterAI::onTargetDied(%aiName, %target) {}
function MonsterAI::onTargetLOSAcquired(%aiName, %target) {}
function MonsterAI::onTargetLOSLost(%aiName, %target) {}
function MonsterAI::onAIKilled(%aiName, %this) {
  echo("SuperAI::Delete(\""@%aiName@"\");SuperAI::Spawn(\""@%aiName@"\", HumanMale, \"0 0 45\", 0, MonsterAI);");

  schedule("SuperAI::Delete(\""@%aiName@"\");", 1);
  schedule("SuperAI::Spawn(\""@%aiName@"\", HumanMale, \"0 0 45\", 0, MonsterAI);", 2);
}
function MonsterAI::onAIDamaged(%aiName,%type,%value,%pos,%vec,%mom,%vertPos,%quadrant,%object) {}