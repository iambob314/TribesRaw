function testRPC(%addr) {
  %s = RPC::Call(%addr, "rpcTestReturn", "mul", 3, 4);
  %s.x = 3;
  %s.y = 4;
  %s.time = getIntegerTime(true);
}

function rpcTestReturn(%s, %retVal) { echo(%s.x, " * ", %s.y, " = ", %retVal); echo("Time: ", getIntegerTime(true) - %s.time); }

function rpc_mul(%x, %y) { return %x * %y; }