//

function GUI::loadComponentInto(%window, %parent, %file) {
  GuiEditMode(%window);
  GuiSetSelection(%window, %parent);
  GuiLoadSelection(%window, %file);
  GuiEditMode(%window);
}

function comment() {
function GUI::isGlobalName(%compname) {
  return Control::getExtent(%compname) != "";
}

function GUI::makeControlCall(%compname, %func, %arg1, %arg2) {
  %callOnObj = %compname;
  if (!GUI::isGlobalName(%callOnObj)) {
    %tempRename = true;
    %oldName = Object::getName(%
  }
}

function GUI::getActive(%control) {
  
}
function GUI::getExtent();
function GUI::getPosition();
function GUI::getText();
function GUI::getValue();
function GUI::getVisible();
function GUI::performClick();
function GUI::setActive();
function GUI::setExtent();
function GUI::setPosition();
function GUI::setText();
function GUI::setValue();
function GUI::setVisible();
}