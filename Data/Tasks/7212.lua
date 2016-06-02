--
-- ------ COPS v6 Emulator - Closed Source ------
-- Copyright (C) 2014 - 2015 Jean-Philippe Boivin
--
-- Generated from official database (cq_4351@192.168.1.114)
-- the 3/30/2015 7:46:38 PM.
--
-- Please read the WARNING, DISCLAIMER and PATENTS
-- sections in the LICENSE file.
--

function processTask7212(client, idx)
    name = "null"
    face = 1

    if (idx == 0) then

        text(client, "    ????????????????????????????????????????????????????????????????????????????????????????????")
        text(client, "??????")
        text(client, "    ?????????�????????????????????????????????�??????????�?????????????????????????????????????????")
        text(client, "???????????????????????????�????????????????")
        link(client, "??????????????", 1)
        link(client, "??????????????", 2)
        link(client, "???????????????", 3)
        link(client, "??????????????", 4)
        link(client, "????????�???????", 5)
        link(client, "?????????", 6)
        link(client, "??????????", 7)
        link(client, "??????????", 8)
        pic(client, 175)
        create(client)

    elseif (idx == 1) then

        if getMoney(client) < 5000 then

            text(client, "???�??????????????5000????")
            link(client, "?????", 255)
            pic(client, 175)
            create(client)

        else

            awardItem(client, "721505", 1)
            spendMoney(client, 5000)
            text(client, "    ??????????????????????????????????????????????????????????????????????????????????�????????????????")
            text(client, "?????????????????????")
            link(client, "??", 255)
            pic(client, 175)
            create(client)

        end

    elseif (idx == 2) then

        if getMoney(client) < 10000 then

            text(client, "???�??????????????10000????")
            link(client, "?????", 255)
            pic(client, 175)
            create(client)

        else

            awardItem(client, "721506", 1)
            spendMoney(client, 10000)
            text(client, "    ??????????????????????????????????????????????????????????????????????????????????�????????????????")
            text(client, "?????????????????????")
            link(client, "??", 255)
            pic(client, 175)
            create(client)

        end

    elseif (idx == 3) then

        if getMoney(client) < 50000 then

            text(client, "???�??????????????50000????")
            link(client, "?????", 255)
            pic(client, 175)
            create(client)

        else

            awardItem(client, "721507", 1)
            spendMoney(client, 50000)
            text(client, "    ??????????????????????????????????????????????????????????????????????????????????�????????????????")
            text(client, "?????????????????????")
            link(client, "??", 255)
            pic(client, 175)
            create(client)

        end

    elseif (idx == 4) then

        if getMoney(client) < 100000 then

            text(client, "???�??????????????100000????")
            link(client, "?????", 255)
            pic(client, 175)
            create(client)

        else

            awardItem(client, "721508", 1)
            spendMoney(client, 100000)
            text(client, "    ??????????????????????????????????????????????????????????????????????????????????�????????????????")
            text(client, "?????????????????????")
            link(client, "??", 255)
            pic(client, 175)
            create(client)

        end

    elseif (idx == 5) then

        if getMoney(client) < 1000000 then

            text(client, "???�??????????????1000000????")
            link(client, "?????", 255)
            pic(client, 175)
            create(client)

        else

            awardItem(client, "721509", 1)
            spendMoney(client, 1000000)
            text(client, "    ??????????????????????????????????????????????????????????????????????????????????�????????????????")
            text(client, "?????????????????????")
            link(client, "??", 255)
            pic(client, 175)
            create(client)

        end

    elseif (idx == 6) then

        if hasItem(client, 1088001, 1) then

            spendItem(client, 1088001, 1)
            awardItem(client, "721510", 1)
            text(client, "    ??????????????????????????????????????????????????????????????????????????????????�????????????????")
            text(client, "?????????????????????")
            link(client, "??", 255)
            pic(client, 175)
            create(client)

        else

            text(client, "?????????�???????")
            link(client, "?????", 255)
            pic(client, 175)
            create(client)

        end

    elseif (idx == 7) then

        if hasItem(client, 1088000, 1) then

            spendItem(client, 1088000, 1)
            awardItem(client, "721511", 1)
            text(client, "    ??????????????????????????????????????????????????????????????????????????????????�????????????????")
            text(client, "?????????????????????")
            link(client, "??", 255)
            pic(client, 175)
            create(client)

        else

            text(client, "?????????�??????")
            link(client, "?????", 255)
            pic(client, 175)
            create(client)

        end

    end

end