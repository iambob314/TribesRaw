function testRemote() {
  $startTime = getIntegerTime(true);
  $test = 0;
  remoteEval(Client::getFirst(), testMiddle);
}

function testNRemote() {
  $startTime = getIntegerTime(true);
  $test = 0;
  nremoteEval("1/1", testBottom);
}

function remoteTestTop(%c) {
  $test++;
  if ($test >= 500) {
    echo("Total time: " @ (getIntegerTime(true) - $startTime));
  } else {
    remoteEval(%c, testMiddle);
  }
}

function remoteTestMiddle(%c) {
  if (%c == 2048) {
    focusserver();
    remoteEval(Client::getFirst(), testBottom);
  } else {
    focusClient();
    remoteEval(2048, testTop);
  }
}

function remoteTestBottom(%c) {
  remoteEval(%c, testMiddle);
}



function nremoteTestTop(%addr) {
  $test++;
  if ($test == 500) {
    echo("Total time: " @ (getIntegerTime(true) - $startTime));
  } else {
    nremoteEval(%addr, testBottom);
  }
}

function nremoteTestBottom(%addr) {
  nremoteEval(%addr, testTop);
}
