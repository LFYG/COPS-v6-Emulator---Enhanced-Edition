--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 5/10/2015 3:10:15 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function Monster3146_OnDie(self, client)
    name = "NagaLord"

    action = randomAction(client, 1, 8)
    if action == 1 then
        dropItem(self, client, 131436)
    elseif action == 2 then
        dropItem(self, client, 133626)
    elseif action == 3 then
        dropItem(self, client, 130836)
    elseif action == 4 then
        dropItem(self, client, 134536)
    elseif action == 5 then
        dropItem(self, client, 111346)
    elseif action == 6 then
        dropItem(self, client, 114946)
    elseif action == 7 then
        dropItem(self, client, 117346)
    elseif action == 8 then
        dropItem(self, client, 721542)
    end

end
