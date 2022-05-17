function Vector::mul(%vec, %c) {
  return (getWord(%vec, 0) * %c) @ " " @ (getWord(%vec, 1) * %c) @ " " @ (getWord(%vec, 2) * %c);
}

function Vector::length(%vec) { return Vector::getDistance(%vec, 0); }
function Vector::lengthSquared(%vec) { return Vector::dot(%vec, %vec); }

function Vector::resize(%vec, %len) {
  return Vector::mul(%vec, %len / Vector::length(%vec));
}

function Vector::floor(%vec) {
  return floor(getWord(%vec, 0)) @ " " @ floor(getWord(%vec, 1)) @ " " @ floor(getWord(%vec, 2));
}

function Vector::ceil(%vec) {
  return ceil(getWord(%vec, 0)) @ " " @ ceil(getWord(%vec, 1)) @ " " @ ceil(getWord(%vec, 2));
}

function Vector::round(%vec) {
  return round(getWord(%vec, 0)) @ " " @ round(getWord(%vec, 1)) @ " " @ round(getWord(%vec, 2));
}

function Vector::randomVec(%ax, %bx, %ay, %by, %az, %bz) {
  return (getRandom() * (%bx - %ax) + %ax) @ " " @
         (getRandom() * (%by - %ay) + %ay) @ " " @
         (getRandom() * (%bz - %az) + %az);
}

function Vector::randomVec2(%vecA, %vecB) {
  return (getRandom() * (getWord(%vecB,0) - getWord(%vecA,0)) + getWord(%vecA,0)) @ " " @
         (getRandom() * (getWord(%vecB,1) - getWord(%vecA,1)) + getWord(%vecA,1)) @ " " @
         (getRandom() * (getWord(%vecB,2) - getWord(%vecA,2)) + getWord(%vecA,2));
}

function Vector::randomRotVec(%rxa, %rxb, %rya, %ryb, %rza, %rzb, %minR, %maxR) {
  return Vector::getFromRot(Vector::randomVec(%rxa, %rxb, %rya, %ryb, %rza, %rzb), (%maxR - %minR) * getRandom() + %minR);
}

function Vector::randomRotVecQWeight(%rxa, %rxb, %rya, %ryb, %rza, %rzb, %minR, %maxR) {
  return Vector::getFromRot(Vector::randomVec(%rxa, %rxb, %rya, %ryb, %rza, %rzb), (%maxR - %minR) * (1-getRandom()*getRandom()) + %minR);
}
function Vector::rotate(%vec, %rot) {
  %len = Vector::length(%vec);
  return Vector::getFromRot(Vector::add(Vector::getRotation(%vec), Vector::add("1.57079 0 0", %rot)), %len);
}

function Vector::getRot(%vec) {
  return Vector::add(Vector::getRotation(%vec), "1.57079 0 0");
}

function Vector::cross(%vec1, %vec2) {
  %a1 = getWord(%vec1, 0);
  %a2 = getWord(%vec1, 1);
  %a3 = getWord(%vec1, 2);
  %b1 = getWord(%vec2, 0);
  %b2 = getWord(%vec2, 1);
  %b3 = getWord(%vec2, 2);
  return (%a2*%b3-%b2*%a3) @ " " @ (%a3*%b1-%b3*%a1) @ " " @ (%a1*%b2-%b1*%a2);
}
