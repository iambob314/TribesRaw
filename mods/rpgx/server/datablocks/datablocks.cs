exec2("server\\datablocks\\shadows.cs");

exec2("server\\datablocks\\basicExp.cs");
exec2("server\\datablocks\\effectProj.cs");

exec2("server\\datablocks\\dynamicObjects.cs");

exec2("server\\datablocks\\Creatures.cs");

exec2("server\\datablocks\\spells\\firestorm.cs");
exec2("server\\datablocks\\spells\\flamespout.cs");
exec2("server\\datablocks\\spells\\test.cs");

exec2("server\\datablocks\\shotgun.cs");

exec2("server\\datablocks\\test.cs");

$skip[RPGTREE1] = 1;
$skip[REALLYBIGRPGTREE] = 1;
$skip[Pistol] = 1;
$skip[fedmonster] = 1;
$skip[bigrpgtree1] = 1;
$skip[bigpotion] = 1;

$shapes = 0;
for (%f = File::findFirst("*.dts"); %f != ""; %f = File::findNext("*.dts")) {
  if (String::findSubStr(%f, "\\") >= 0 || String::findSubStr(%f, " ") >= 0) continue;

  if (%i++ > 150) break;

  %name = File::getBase(%f);
  if (%done[%name] || $skip[%name]) continue;
  %done[%name] = 1;

  $shapes[$shapes++ - 1] = %name;

  eval("StaticShapeData " @ %name @ "Shape {" @ 
         "shapeFile = \"" @ %name @ "\";" @ 
       "};");
}

function spawnAllShapes(%basePos, %spacing, %grid) {
  %nx = getWord(%grid, 0);
  %ny = getWord(%grid, 1);
  %nz = getWord(%grid, 2);

  addToSet(MissionGroup, %group = newObject("ShapeArray", SimGroup));

  %i = 0;
  for (%z = 0; %z < %nz; %z++) {
    for (%y = 0; %y < %ny; %y++) {
      for (%x = 0; %x < %nx; %x++) {
        if (%i >= $shapes) return;

        %name = $shapes[%i++ - 1];
        echo("Spawning " @ %name);
        %coord = %x @ " " @ %y @ " " @ %z;
        %pos = Vector::add(%basePos, Vector::mul(%coord, %spacing));
        addToSet(%group, %obj = newObject(%name, StaticShape, %name @ "Shape", true));
        GameBase::setPosition(%obj, %pos);
      }
    }
  }
}