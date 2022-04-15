$Transaction::curTransID = -1;
$Transaction::curUID = -1;

// 
// Each transaction is uniquely identified by two factors:
//   1. Who the other party is, and
//   2. 
//                           
// $Transaction::uniqueID[%target, %transID]
// $Transaction::transactionInfo[%uniqueID] = "%target %transID"

function Transaction::initiateTransaction(%target) {
  %uid = $Transaction::curUID++;
  %transID = $Transaction::curTransID++;

  $Transaction::transaction[%target, %transID];

  return
}

function 