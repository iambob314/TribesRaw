function Player::onKilled(%this) {
  if (Player::isAIControlled(%this) && %this.aiName != "") {
    AI::onAIKilled(%this.aiName, %this);
  }
}

function Player::onDamage(%this, %type, %value, %pos, %vec, %mom, %vertPos, %quadrant, %object) {
  if (Player::isAIControlled(%this) && %this.aiName != "") {
    AI::onAIDamaged(%this.aiName, %type, %value, %pos, %vec, %mom, %vertPos, %quadrant, %object);
  }

  echo("DAMAGE TYPE: " @ %type);

  %health = 1.0;

  %damage = Player::calculateDamage(%this, %type, %value, %pos, %vec, %mom, %vertPos, %quadrant, %object);
  if (%damage == 0) return;

  %damageLevel = GameBase::getDamageLevel(%this) + %damage;
  GameBase::setDamageLevel(%this, %damageLevel);

  if (%damageLevel >= %health) {
    Player::blowUp(%this);
  }
}

function Player::calculateDamage(%this, %type, %value, %pos, %vec, %mom, %vertPos, %quadrant, %object) {
  return %value;
}
