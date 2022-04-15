//$RPC::retFunc[%addr, %id]
//$RPC::stateObj[%addr, %id]
//$RPC::focus[%addr, %id];
$RPC::nextID = 0;

function RPC::Call(%addr, %retFunc, %func, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7) {
  %id = $RPC::nextID;
  $RPC::retFunc[%addr, %id] = %retFunc;
  $RPC::stateObj[%addr, %id] = newObject("", SimSet); // Created in whichever object space is currently focused (client or server)
  $RPC::focus[%addr, %id] = isServerFocused();
  $RPC::nextID++;

  nremoteEval(%addr, "RPC", %func, %id, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7);
  schedule("RPC::Cleanup(\"" @ %addr @ "\",\"" @ %id @ "\");", 30, $RPC::stateObj[%addr, %id]); // Give the RPC 30 seconds to complete

  return $RPC::stateObj[%addr, %id];
}

function nremoteRPC(%addr, %func, %id, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7) {
  %retVal = invoke2("rpc_" @ %func, %arg0, %arg1, %arg2, %arg3, %arg4, %arg5, %arg6, %arg7);
  nremoteEval(%addr, "RPCReturn", %id, %retVal);
}

function nremoteRPCReturn(%addr, %id, %retVal) {
  if ($RPC::retFunc[%addr, %id] == "") return;

  if ($RPC::focus[%addr, %id]) pushFocusServer();
  else                         pushFocusClient();

  invoke($RPC::retFunc[%addr, %id], 2, $RPC::stateObj[%addr, %id], %retVal);
  RPC::Cleanup(%addr, %id);

  popFocus();
}

function RPC::Cleanup(%addr, %id) {
  deleteObject($RPC::stateObj[%addr, %id]);
  $RPC::retFunc[%addr, %id] = "";
  $RPC::stateObj[%addr, %id] = "";
  $RPC::focus[%addr, %id] = "";
}