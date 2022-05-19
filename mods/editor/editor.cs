requireMod(std);
requireMod(gui);

exec("editor\\guieditor.cs");

// Experiments
focusClient();
std::initDefaultsClient();
GUI::newWindow();

schedule("GuiEditor::loadEditor();", 1);
schedule("GuiEditor::editEditor();", 2);