$PlayerEditor::DRAG_UPDATE_TIME = 0.25;
$PlayerEditor::NUM_SNAP_AXES = 8;

%axis0 = "1 0 0";
%axis1 = sqrt(2)/2  @ " " @ sqrt(2)/2  @ " 0";
%axis2 = "0 1 0";
%axis3 = -sqrt(2)/2 @ " " @ sqrt(2)/2  @ " 0";
%axis4 = "-1 0 0";
%axis5 = -sqrt(2)/2 @ " " @ -sqrt(2)/2 @ " 0";
%axis6 = "0 -1 0";
%axis7 = sqrt(2)/2  @ " " @ -sqrt(2)/2 @ " 0";

for (%i = 0; %i < $PlayerEditor::NUM_SNAP_AXES; %i++) {
  $PlayerEditor::SNAP_AXIS_MATRIX[%i] = %axis[%i] @ " " @ %axis[(%i + 2) % $PlayerEditor::NUM_SNAP_AXES] @ " 0 0 1";
}

function remotePE::SelectObject(%conn) {
  if (%conn == 2048) return;

  %player = Client::getControlObject(%conn);
  if (GameBase::getLOSInfo(%player, 1000)) {
    if (getObjectType($los::object) == "SimTerrain") return;
    if (getObjectType($los::object) == "Player") return;

    PlayerEditor::deselect(%conn);
    PlayerEditor::select(%conn, $los::object);
  }
}

function remotePE::Nudge(%conn, %nudge, %roundTo) {
  if (%conn == 2048) return;

  if (PlayerEditor::isDragging(%conn)) {
    %conn.dragObjectDotVec = Vector::add(%conn.dragObjectDotVec, %nudge);
  } else {
    if (!PlayerEditor::hasSelectedObject(%conn)) return;

    %player = Client::getControlObject(%conn);
    %muzVec = Matrix::subMatrix(GameBase::getMuzzleTransform(%player), 3, 4, 3, 1, 0, 1);

    %localMat = PlayerEditor::getClosestSnapAxis(%muzVec);
    %nudge = Matrix::mul(%localMat, 3, 3, %nudge, 3, 1);

    %pos = GameBase::getPosition(%conn.selectedObject);

    %posRound = Vector::mul(%pos, 1/%roundTo);
    %posRound = Vector::round(%posRound);
    %posRound = Vector::mul(%posRound, %roundTo);

    GameBase::setPosition(%conn.selectedObject, Vector::add(%posRound, %nudge));
  }
}

function remotePE::NudgeRot(%conn, %nudge, %roundTo) {
  if (%conn == 2048) return;
  if (!PlayerEditor::hasSelectedObject(%conn)) return;

  %rot = GameBase::getRotation(%conn.selectedObject);

  %rotRound = Vector::mul(%rot, 1/%roundTo);
  %rotRound = Vector::round(%rotRound);
  %rotRound = Vector::mul(%rotRound, %roundTo);

  GameBase::setRotation(%conn.selectedObject, Vector::add(%rotRound, %nudge));
}

function remotePE::DeleteObject(%conn) {
  if (%conn == 2048) return;
  if (!PlayerEditor::hasSelectedObject(%conn)) return;

  deleteObject(%conn.selectedObject);
  %conn.selectedObject = "";
}

function remotePE::BeginObjectDrag(%conn) {
  if (%conn == 2048) return;
  if (PlayerEditor::isDragging(%conn)) return;

  %player = Client::getControlObject(%conn);
  if (GameBase::getLOSInfo(%player, 100)) {
    if (!PlayerEditor::isDraggable($los::object)) return;

    PlayerEditor::dragObject(%conn, $los::object);
  }
}

function remotePE::EndObjectDrag(%conn) {
  if (%conn == 2048) return;
  if (!PlayerEditor::isDragging(%conn)) return;

  %conn.dragObject.draggedBy = "";
  %conn.dragObject = "";
  %conn.dragObjectDotVec = "";
}

function remotePE::ToggleObjectDrag(%conn) {
  if (%conn == 2048) return;

  if (PlayerEditor::isDragging(%conn)) {
    remotePE::EndObjectDrag(%conn);
  } else {
    remotePE::BeginObjectDrag(%conn);
  }
}

// Helper functions

function PlayerEditor::deselect(%conn) {
  %conn.selectedObject = "";
}

function PlayerEditor::select(%conn, %obj) {
  %conn.selectedObject = %obj;

  %pos = GameBase::getPosition(%obj);
  for (%i = 0; %i < 10; %i++) {
    %offset = Vector::randomVec(-0.2,0.2,-0.2,0.2,-0.2,0.2);
    schedule("GameBase::setPosition("@%obj@",\"" @ Vector::add(%pos, %offset) @ "\");", (%i/10)*0.5);
  }
  schedule("GameBase::setPosition("@%obj@",\"" @ %pos @ "\");", 0.5);
}

function PlayerEditor::hasSelectedObject(%conn) {
  return %conn.selectedObject != "" && isObject(%conn.selectedObject);
}

function PlayerEditor::getClosestSnapAxis(%vec) {
  %bestDot = -2;
  %bestAxis = -1;

  for (%i = 0; %i < $PlayerEditor::NUM_SNAP_AXES; %i++) {
    %axis = Matrix::subMatrix($PlayerEditor::SNAP_AXIS_MATRIX[%i], 3, 3, 3, 1, 0, 1);
    %dot = Vector::dot(%axis, %vec);
    if (%dot > %bestDot) {
      %bestDot = %dot;
      %bestAxis = %i;
    }
  }

  return $PlayerEditor::SNAP_AXIS_MATRIX[%bestAxis];
}

function PlayerEditor::getDragObject(%conn) {
  if (%conn.dragObject == "" || %conn.dragObject.draggedBy != %conn) return "";

  return %conn.dragObject;
}

function PlayerEditor::isDragging(%conn) {
  return %conn.dragObject != "" && %conn.dragObject.draggedBy == %conn;
}

function PlayerEditor::isBeingDragged(%obj) {
  return %obj.draggedBy != "" && %obj.draggedBy.dragObject == %obj;
}

function PlayerEditor::isDraggable(%obj) {
  if (getObjectType(%obj) == "SimTerrain") return false;
  if (getObjectType(%obj) == "Player") return false;
  if (PlayerEditor::isBeingDragged(%obj)) return false;
  return true;
}

function PlayerEditor::dragObject(%conn, %obj) {
  %conn.dragObject = %obj;
  %obj.draggedBy = %conn;

  %player = Client::getControlObject(%conn);
  %trans = GameBase::getMuzzleTransform(%player);
  %muzPos = Matrix::subMatrix(%trans, 3, 4, 3, 1, 0, 3);
  %objPos = GameBase::getPosition(%obj);
  %vec = Vector::sub(%objPos, %muzPos);

  %ttrans = Matrix::transpose(Matrix::subMatrix(%trans, 3, 4, 3, 3), 3, 3);
  %dotVec = Matrix::mul(%ttrans, 3, 3, %vec, 3, 1);

  %conn.dragObjectDotVec = %dotVec;

  PlayerEditor::doDrag(%obj);
}

function PlayerEditor::doDrag(%obj) {
  if (!isObject(%obj) || %obj.draggedBy == "") return;

  %dragger = %obj.draggedBy;
  %player = Client::getControlObject(%dragger);

  %dotVec = %dragger.dragObjectDotVec;
  %trans = GameBase::getMuzzleTransform(%player);
  %muzMat = Matrix::subMatrix(%trans, 3, 4, 3, 3);
  %muzPos = Matrix::subMatrix(%trans, 3, 4, 3, 1, 0, 3);

  %newVec = Matrix::mul(%muzMat, 3, 3, %dotVec, 3, 1);

  GameBase::setPosition(%obj, Vector::add(%newVec, %muzPos));

  schedule("PlayerEditor::doDrag(" @ %obj @ ");", $PlayerEditor::DRAG_UPDATE_TIME);
}
