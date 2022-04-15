//------------------------------------
//
// Network functions
//
//------------------------------------

function selectNewMaster() {
  translateMasters();
}

function checkMasterTranslation() {
  for (%i = 0; %i < $Server::numMasters; %i++) {
    %mstr = DNet::getResolvedMaster(%i);
    if (%mstr != "")
      $Server::XLMasterN[%i] = %mstr;
    $inet::master[%i+1] = $Server::XLMasterN[%i];
  }
}

function translateMasters() {
  for (%i = 0; (%word = getWord($Server::MasterAddressN[$Server::CurrentMaster], %i)) != -1; %i++)
    %mlist[%i] = %word;

  $Server::numMasters = DNet::resolveMasters(%mlist0, %mlist1, %mlist2, %mlist3, %mlist4, %mlist5, %mlist6, %mlist7, %mlist8, %mlist9);
}
