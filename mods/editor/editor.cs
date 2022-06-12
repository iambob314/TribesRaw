requireMod(std);
requireMod(gui);

exec("editor\\guieditor.cs");

// Experiments
focusClient();
GUI::newWindow();

schedule("GuiEditor::loadEditor();", 1);
schedule("GuiEditor::editEditor();", 2);