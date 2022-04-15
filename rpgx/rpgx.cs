$SearchPath[rpgx] = "resources";
EvalSearchPath();

exec2("util\\util.cs");
exec2("strings\\strings.cs");

if ($debugMode) {
    $Console::printLevel = 3;
    function echo(%a, %b, %c, %d, %e, %f, %g, %h) {
      dbecho(0, tern(isServerFocused(), "(Server) ", "(Client) "), %a, %b, %c, %d, %e, %f, %g, %h);
      trace();
    }
}

exec2("common\\common.cs");

if (!$dumbTerminal) {

exec2("resources\\resources.cs");

if ($editMission != "") {
  exec2("editor\\editor.cs");
} else if ($dedicated) {
  exec2("server\\server.cs");
} else {
  exec2("client\\client.cs");
}

}
