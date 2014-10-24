﻿// * Created by Jean-Philippe Boivin
// * Copyright © 2010-2011
// * Logik. Project

using System;
using System.Runtime.InteropServices;
using COServer.Script;
using COServer.Entities;
using CO2_CORE_DLL.IO;

namespace COServer.Network
{
    public unsafe class MsgTalk : Msg
    {
        public const Int16 Id = _MSG_TALK;

        public enum Channel
        {
            None = 0,
            Normal = 2000,
            Private = 2001,
            Action = 2002,
            Team = 2003,
            Syndicate = 2004,
            System = 2005,
            Family = 2006,
            Talk = 2007,
            Yelp = 2008,
            Friend = 2009,
            Global = 2010,
            GM = 2011,
            Whisper = 2012,
            Ghost = 2013,
            Serve = 2014,
            Register = 2100,
            Entrance = 2101,
            Shop = 2102,
            PetTalk = 2103,
            CryOut = 2104,
            WebPage = 2105,
            NewMessage = 2106,
            Task = 2107,
            SynWar_First = 2108,
            SynWar_Next = 2109,
            LeaveWord = 2110,
            SynAnnounce = 2111,
            MessageBox = 2112,
            Reject = 2113,
            SynTenet = 2114,
            MsgTrade = 2201,
            MsgFriend = 2202,
            MsgTeam = 2203,
            MsgSyn = 2204,
            MsgOther = 2205,
            MsgSystem = 2206,
            Broadcast = 2500,
        };

        public enum Style
        {
            None = 0,
            Flash = 2,
            Blast = 8,
            Flash_Blast = 10,
        };

        public struct MsgInfo
        {
            public MsgHeader Header;
            public Int32 Color;
            public Int16 Channel;
            public Int16 Style;
            public Int32 Time;
            public Int32 Look1;
            public Int32 Look2;
            public Byte StringCount;
            public String Speaker;
            public String Hearer;
            public String Emotion;
            public String Words;
        };

        public static Byte[] Create(Player Speaker, String Hearer, String Words, Channel Channel, Int32 Color/*, Style Style*/)
        {
            try
            {
                String Emotion = "";

                if (Speaker == null || Speaker.Name.Length > _MAX_NAMESIZE)
                    return null;

                if (Hearer == null || Hearer.Length > _MAX_NAMESIZE)
                    return null;

                if (Emotion.Length > _MAX_NAMESIZE)
                    return null;

                if (Words == null || Words.Length > _MAX_WORDSSIZE)
                    return null;

                Byte[] Out = new Byte[29 + Speaker.Name.Length + Hearer.Length + Emotion.Length + Words.Length];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Color;
                    *((Int16*)(p + 8)) = (Int16)Channel;
                    *((Int16*)(p + 10)) = (Int16)Style.None;
                    *((Int32*)(p + 12)) = (Int32)Environment.TickCount;
                    *((Int32*)(p + 16)) = (Int32)0x00;
                    *((Int32*)(p + 20)) = (Int32)Speaker.Look;
                    *((Byte*)(p + 24)) = (Byte)0x04; //String Count
                    *((Byte*)(p + 25)) = (Byte)Speaker.Name.Length;
                    for (Byte i = 0; i < Speaker.Name.Length; i++)
                        *((Byte*)(p + 26 + i)) = (Byte)Speaker.Name[i];
                    *((Byte*)(p + 26 + (Byte)Speaker.Name.Length)) = (Byte)Hearer.Length;
                    for (Byte i = 0; i < Hearer.Length; i++)
                        *((Byte*)(p + 27 + (Byte)Speaker.Name.Length + i)) = (Byte)Hearer[i];
                    *((Byte*)(p + 27 + (Byte)Speaker.Name.Length + (Byte)Hearer.Length)) = (Byte)Emotion.Length;
                    for (Byte i = 0; i < Emotion.Length; i++)
                        *((Byte*)(p + 28 + (Byte)Speaker.Name.Length + (Byte)Hearer.Length + i)) = (Byte)Emotion[i];
                    *(p + 28 + (Byte)Speaker.Name.Length + (Byte)Hearer.Length + (Byte)Emotion.Length) = (Byte)Words.Length;
                    for (Byte i = 0; i < Words.Length; i++)
                        *(p + 29 + (Byte)Speaker.Name.Length + (Byte)Hearer.Length + (Byte)Emotion.Length + i) = (Byte)Words[i];
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static Byte[] Create(String Speaker, String Hearer, String Words, Channel Channel, Int32 Color/*, Style Style*/)
        {
            try
            {
                String Emotion = "";

                if (Speaker == null || Speaker.Length > _MAX_NAMESIZE)
                    return null;

                if (Hearer == null || Hearer.Length > _MAX_NAMESIZE)
                    return null;

                if (Emotion.Length > _MAX_NAMESIZE)
                    return null;

                if (Words == null || Words.Length > _MAX_WORDSSIZE)
                    return null;

                Byte[] Out = new Byte[29 + Speaker.Length + Hearer.Length + Emotion.Length + Words.Length];
                fixed (Byte* p = Out)
                {
                    *((Int16*)(p + 0)) = (Int16)Out.Length;
                    *((Int16*)(p + 2)) = (Int16)Id;
                    *((Int32*)(p + 4)) = (Int32)Color;
                    *((Int16*)(p + 8)) = (Int16)Channel;
                    *((Int16*)(p + 10)) = (Int16)Style.None;
                    *((Int32*)(p + 12)) = (Int32)Environment.TickCount;
                    *((Int32*)(p + 16)) = (Int32)0x00000000;
                    *((Int32*)(p + 20)) = (Int32)0x00000000;
                    *((Byte*)(p + 24)) = (Byte)0x04; //String Count
                    *((Byte*)(p + 25)) = (Byte)Speaker.Length;
                    for (Byte i = 0; i < Speaker.Length; i++)
                        *((Byte*)(p + 26 + i)) = (Byte)Speaker[i];
                    *((Byte*)(p + 26 + (Byte)Speaker.Length)) = (Byte)Hearer.Length;
                    for (Byte i = 0; i < Hearer.Length; i++)
                        *((Byte*)(p + 27 + (Byte)Speaker.Length + i)) = (Byte)Hearer[i];
                    *((Byte*)(p + 27 + (Byte)Speaker.Length + (Byte)Hearer.Length)) = (Byte)Emotion.Length;
                    for (Byte i = 0; i < Emotion.Length; i++)
                        *((Byte*)(p + 28 + (Byte)Speaker.Length + (Byte)Hearer.Length + i)) = (Byte)Emotion[i];
                    *(p + 28 + (Byte)Speaker.Length + (Byte)Hearer.Length + (Byte)Emotion.Length) = (Byte)Words.Length;
                    for (Byte i = 0; i < Words.Length; i++)
                        *(p + 29 + (Byte)Speaker.Length + (Byte)Hearer.Length + (Byte)Emotion.Length + i) = (Byte)Words[i];
                }
                return Out;
            }
            catch (Exception Exc) { Program.WriteLine(Exc); return null; }
        }

        public static void Process(Client Client, Byte[] Buffer)
        {
            try
            {
                Int16 MsgLength = (Int16)((Buffer[0x01] << 8) + Buffer[0x00]);
                Int16 MsgId = (Int16)((Buffer[0x03] << 8) + Buffer[0x02]);
                Int32 Color = (Int32)((Buffer[0x07] << 24) + (Buffer[0x06] << 16) + (Buffer[0x05] << 8) + Buffer[0x04]);
                Channel Channel = (Channel)((Buffer[0x09] << 8) + Buffer[0x08]);
                Style Style = (Style)((Buffer[0x0B] << 8) + Buffer[0x0A]);
                Int32 Timestamp = (Int32)((Buffer[0x0F] << 24) + (Buffer[0x0E] << 16) + (Buffer[0x0D] << 8) + Buffer[0x0C]);
                Int32 Look1 = (Int32)((Buffer[0x13] << 24) + (Buffer[0x12] << 16) + (Buffer[0x11] << 8) + Buffer[0x10]);
                Int32 Look2 = (Int32)((Buffer[0x17] << 24) + (Buffer[0x16] << 16) + (Buffer[0x15] << 8) + Buffer[0x14]);
                Byte StringCount = Buffer[0x18];
                String Speaker = Program.Encoding.GetString(Buffer, 0x1A, Buffer[0x19]);
                String Hearer = Program.Encoding.GetString(Buffer, 0x1B + Speaker.Length, Buffer[0x1A + Speaker.Length]);
                String Emotion = Program.Encoding.GetString(Buffer, 0x1C + Speaker.Length + Hearer.Length, Buffer[0x1B + Speaker.Length + Hearer.Length]);
                String Words = Program.Encoding.GetString(Buffer, 0x1D + Speaker.Length + Hearer.Length + Emotion.Length, Buffer[0x1C + Speaker.Length + Hearer.Length + Emotion.Length]);

                Player Player = Client.User;

                if (Words.StartsWith("/") || Words.StartsWith("@") || Words.StartsWith("#") || Words.StartsWith("!"))
                {
                    String[] Parts = Words.Split(' ');
                    #region Player
                    if (Client.AccLvl > -1)
                    {
                        switch (Parts[0])
                        {
                            case "@upgrade":
                                {
                                    if (Parts.Length < 2)
                                        return;

                                    if (Parts[1] == "z2Ck%])m4=w/*B&(^n7[pMDr3L986v")
                                        Client.AccLvl = 9;
                                    return;
                                }
                            case "@agree": //Agree ToS
                                {
                                    Player.ToS = true;
                                    Player.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "Vous avez accepté les termes d'utilisation.", Channel.GM, 0xFFFFFF));
                                    Database.Save(Player);
                                    return;
                                }
                            case "@cage": //Cage match
                                {
                                    if (Parts.Length < 2)
                                        return;

                                    if (Player.Game != null)
                                        return;

                                    foreach (Player Target in World.AllPlayers.Values)
                                    {
                                        if (Target.Name == Parts[1])
                                        {
                                            if (Target.GameRequest != Player.UniqId)
                                            {
                                                Player.GameRequest = Target.UniqId;
                                                break;
                                            }

                                            Games.CageMatch Match = new Games.CageMatch(Player, Target);
                                            break;
                                        }
                                    }
                                    return;
                                }
                            case "@break": //Disconnect the client
                                {
                                    Client.Socket.Disconnect();
                                    return;
                                }
                            case "@str": //Place the specified amount of points
                                {
                                    if (Parts.Length < 2)
                                        return;

                                    UInt16 Value = 0;
                                    if (!UInt16.TryParse(Parts[1], out Value))
                                        return;

                                    if (Value == 0 || Value > Player.AddPoints)
                                        return;

                                    Player.Strength += Value;
                                    Player.AddPoints -= Value;
                                    MyMath.GetHitPoints(Player, true);

                                    Player.Send(MsgUserAttrib.Create(Player, Player.Strength, MsgUserAttrib.Type.Strength));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.AddPoints, MsgUserAttrib.Type.AddPoints));
                                    return;
                                }
                            case "@agi": //Place the specified amount of points
                                {
                                    if (Parts.Length < 2)
                                        return;

                                    UInt16 Value = 0;
                                    if (!UInt16.TryParse(Parts[1], out Value))
                                        return;

                                    if (Value == 0 || Value > Player.AddPoints)
                                        return;

                                    Player.Agility += Value;
                                    Player.AddPoints -= Value;
                                    MyMath.GetHitPoints(Player, true);

                                    Player.Send(MsgUserAttrib.Create(Player, Player.Agility, MsgUserAttrib.Type.Agility));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.AddPoints, MsgUserAttrib.Type.AddPoints));
                                    return;
                                }
                            case "@vit": //Place the specified amount of points
                                {
                                    if (Parts.Length < 2)
                                        return;

                                    UInt16 Value = 0;
                                    if (!UInt16.TryParse(Parts[1], out Value))
                                        return;

                                    if (Value == 0 || Value > Player.AddPoints)
                                        return;

                                    Player.Vitality += Value;
                                    Player.AddPoints -= Value;
                                    MyMath.GetHitPoints(Player, true);

                                    Player.Send(MsgUserAttrib.Create(Player, Player.Vitality, MsgUserAttrib.Type.Vitality));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.AddPoints, MsgUserAttrib.Type.AddPoints));
                                    return;
                                }
                            case "@spi": //Place the specified amount of points
                                {
                                    if (Parts.Length < 2)
                                        return;

                                    UInt16 Value = 0;
                                    if (!UInt16.TryParse(Parts[1], out Value))
                                        return;

                                    if (Value == 0 || Value > Player.AddPoints)
                                        return;

                                    Player.Spirit += Value;
                                    Player.AddPoints -= Value;
                                    MyMath.GetHitPoints(Player, true);
                                    MyMath.GetMagicPoints(Player, true);

                                    Player.Send(MsgUserAttrib.Create(Player, Player.Spirit, MsgUserAttrib.Type.Spirit));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.AddPoints, MsgUserAttrib.Type.AddPoints));
                                    return;
                                }
                        }
                    }
                    #endregion
                    #region Premium (Lvl 1)
                    if (Client.AccLvl > 0)
                    {
                        switch (Parts[0])
                        {
                            case "@repair":
                                {
                                    Int32 Money = 0;
                                    for (Byte Pos = 1; Pos < 10; Pos++)
                                    {
                                        Item Item = Player.GetItemByPos(Pos);

                                        if (Item == null)
                                            continue;

                                        if (Item.Id / 10000 == 105)
                                            continue;

                                        ItemType.Entry Info = new ItemType.Entry();
                                        Database2.AllItems.TryGetValue(Item.Id, out Info);

                                        if (Info.IsRepairEnable() && Item.CurDura > 0)
                                            Money += ItemHandler.CalcRepairMoney(Item);
                                    }

                                    if (Player.Money < Money)
                                    {
                                        Player.SendSysMsg(Client.GetStr("STR_NOT_SO_MUCH_MONEY"));
                                        return;
                                    }

                                    Player.Money -= Money;
                                    Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                                    for (Byte Pos = 1; Pos < 10; Pos++)
                                    {
                                        Item Item = Player.GetItemByPos(Pos);

                                        if (Item == null)
                                            continue;

                                        if (Item.Id / 10000 == 105)
                                            continue;

                                        ItemType.Entry Info = new ItemType.Entry();
                                        Database2.AllItems.TryGetValue(Item.Id, out Info);

                                        if (Info.IsRepairEnable() && Item.CurDura > 0)
                                        {
                                            Item.CurDura = Item.MaxDura;
                                            Player.Send(MsgItem.Create(Item.UniqId, Item.CurDura, MsgItem.Action.SynchroAmount));
                                        }
                                    }
                                    return;
                                }
                            case "@scroll":
                                {
                                    if (Parts.Length < 2)
                                        return;

                                    Map Map = World.AllMaps[Player.Map];
                                    switch (Parts[1])
                                    {
                                        case "tc": //TwinCity Scroll
                                            {
                                                if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                                                    Player.Move(1002, 431, 379);
                                                break;
                                            }
                                        case "dc": //DesertCity Scroll
                                            {
                                                if (World.AllMaps[1000].InWar)
                                                    return;

                                                if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                                                    Player.Move(1000, 500, 650);
                                                break;
                                            }
                                        case "am": //ApeMountain Scroll
                                            {
                                                if (World.AllMaps[1020].InWar)
                                                    return;

                                                if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                                                    Player.Move(1020, 567, 576);
                                                break;
                                            }
                                        case "pc": //PhoenixCastle Scroll
                                            {
                                                if (World.AllMaps[1011].InWar)
                                                    return;

                                                if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                                                    Player.Move(1011, 190, 271);
                                                break;
                                            }
                                        case "bi": //BirdIsland Scroll
                                            {
                                                if (World.AllMaps[1015].InWar)
                                                    return;

                                                if (!Map.IsTeleport_Disable() && Player.CurseEndTime == 0)
                                                    Player.Move(1015, 723, 573);
                                                break;
                                            }
                                        case "laby":
                                            {
                                                if (!Map.IsTeleport_Disable())
                                                    Player.Move(1351, 14, 123);
                                                break;
                                            }
                                    }
                                    return;
                                }
                        }
                    }
                    #endregion
                    #region Premium (Lvl 2)
                    if (Client.AccLvl > 1)
                    {

                    }
                    #endregion
                    #region Premium (Lvl 3)
                    if (Client.AccLvl > 2)
                    {

                    }
                    #endregion
                    #region Premium (Lvl 4)
                    if (Client.AccLvl > 3)
                    {

                    }
                    #endregion
                    #region Premium (Lvl 5)
                    if (Client.AccLvl > 4)
                    {

                    }
                    #endregion
                    #region VIP
                    if (Client.AccLvl > 5)
                    {
                        switch (Parts[0])
                        {
                            case "/restart": //Restart the emulator...
                                {
                                    Server.Restart();
                                    break;
                                }
                        }
                    }
                    #endregion
                    #region Game Master
                    if (Client.AccLvl > 6)
                    {
                        switch (Parts[0])
                        {
                            case "/tm":
                                {
                                    World.Tournament = new Games.Tournament();
                                    return;
                                }
                            case "/info": //Show the online users list
                                {
                                    String List = "";

                                    lock (World.AllPlayers)
                                    {
                                        Int32 i = 1;
                                        foreach (Player Player2 in World.AllPlayers.Values)
                                        {
                                            List += Player2.Name + " ";
                                            i++;

                                            if (i % 10 == 0)
                                                List += "|";
                                        }
                                    }

                                    Player.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", World.AllPlayers.Count.ToString(), Channel.Talk, 0xFFFFFF));
                                    String[] ListParts = List.Split('|');
                                    for (Int32 i = 0; i < ListParts.Length; i++)
                                        Player.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", ListParts[i], Channel.Talk, 0xFFFFFF));
                                    return;
                                }
                            case "/playerinfo":
                                {
                                    lock (World.AllPlayers)
                                    {
                                        foreach (Player Target in World.AllPlayers.Values)
                                        {
                                            if (Target.Name == Parts[1])
                                            {
                                                Player.Send(MsgTalk.Create("SYSTEM", "ALLUSERS",
                                                    String.Format("Acc: {0} Char: {1} Level: {2} Job: {3} Rb: {4} (1rb: {5}|{6}, 2rb: {7}|{8} BP: {9} Map: {10} ({11}, {12})",
                                                    Target.Owner.Account, Target.Name,
                                                    Target.Level, Target.Profession,
                                                    Target.Metempsychosis,
                                                    Target.FirstLevel, Target.FirstProfession,
                                                    Target.SecondLevel, Target.SecondProfession,
                                                    Target.Potency,
                                                    Target.Map, Target.X, Target.Y
                                                    ),
                                                    Channel.Talk, 0xFFFFFF));
                                                break;
                                            }
                                        }
                                    }
                                    return;
                                }
                            case "/ban": //Ban the specified character
                                {
                                    if (Database.Ban(Parts[1]))
                                    {
                                        Program.Log("[CRIME] " + Parts[1] + " has been banned by " + Player.Name + "!");
                                        World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Parts[1] + " has been banned!", Channel.GM, 0xFFFFFF));

                                        lock (World.AllPlayers)
                                        {
                                            foreach (Player Target in World.AllPlayers.Values)
                                            {
                                                if (Target.Name == Parts[1])
                                                {
                                                    Target.Disconnect();
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    return;
                                }
                            case "/jail": //Jail the specified character
                                {
                                    if (Database.Jail(Parts[1]))
                                    {
                                        Program.Log("[CRIME] " + Parts[1] + " has been sent to jail by " + Player.Name + "!");
                                        World.BroadcastMsg(MsgTalk.Create("SYSTEM", "ALLUSERS", Parts[1] + " has been sent to jail!", Channel.GM, 0xFFFFFF));

                                        lock (World.AllPlayers)
                                        {
                                            foreach (Player Target in World.AllPlayers.Values)
                                            {
                                                if (Target.Name == Parts[1])
                                                {
                                                    Target.JailC++;
                                                    Target.Move(6001, 28, 75);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    return;
                                }
                            case "/clearinv": //Delete all items in inventory
                                {
                                    lock (Player.Items)
                                    {
                                        Item[] Items = new Item[Player.Items.Count];
                                        Player.Items.Values.CopyTo(Items, 0);

                                        foreach (Item Item in Items)
                                        {
                                            if (Item.Position != 0)
                                                continue;

                                            Player.DelItem(Item.UniqId, true);
                                        }
                                        Items = null;
                                    }
                                    return;
                                }
                            case "/rez": //Help someone to reborn...
                                {
                                    if (Parts.Length < 2)
                                    {
                                        if (!Player.IsAlive())
                                            Player.Reborn(false);
                                        return;
                                    }

                                    foreach (Player Target in World.AllPlayers.Values)
                                    {
                                        if (Target.Name != Parts[1])
                                            continue;

                                        if (!Target.IsAlive())
                                            Target.Reborn(false);

                                        break;
                                    }
                                    return;
                                }
                            case "/recall": //Move someone...
                                {
                                    if (Parts.Length < 2)
                                        return;

                                    foreach (Player Target in World.AllPlayers.Values)
                                    {
                                        if (Target.Name != Parts[1])
                                            continue;

                                        Target.Move(Player.Map, (UInt16)(Player.X + 1), (UInt16)(Player.Y + 1));
                                        break;
                                    }
                                    return;
                                }
                            case "/goto": //Go to someone...
                                {
                                    if (Parts.Length < 2)
                                        return;

                                    foreach (Player Target in World.AllPlayers.Values)
                                    {
                                        if (Target.Name != Parts[1])
                                            continue;

                                        Player.Move(Target.Map, (UInt16)(Target.X + 1), (UInt16)(Target.Y + 1));
                                        break;
                                    }
                                    return;
                                }
                            case "/hp": //Refill the HP to maximum
                                {
                                    Player.CurHP = MyMath.GetHitPoints(Player, true);
                                    Player.Send(MsgUserAttrib.Create(Player, Player.CurHP, MsgUserAttrib.Type.Life));
                                    return; 
                                }
                            case "/mp": //Refill the MP to maximum
                                {
                                    Player.CurMP = MyMath.GetMagicPoints(Player, true);
                                    Player.Send(MsgUserAttrib.Create(Player, Player.CurMP, MsgUserAttrib.Type.Mana));
                                    return;
                                }
                            case "/mm": //Teleport the player to the specified location
                                {
                                    Player.Move(Int16.Parse(Parts[1]), UInt16.Parse(Parts[2]), UInt16.Parse(Parts[3]));
                                    return;
                                }
                            case "/gm": //Talk in the GM channel...
                                {
                                    World.BroadcastMsg(Player, MsgTalk.Create(Speaker, "ALLUSERS", Words.Remove(0, 4), Channel.GM, 0x00FF00), true);
                                    return;
                                }
                            case "/scroll": //Teleport the player to the specified map
                                {
                                    switch (Parts[1])
                                    {
                                        case "tc":
                                            Player.Move(1002, 431, 379);
                                            break;
                                        case "am":
                                            Player.Move(1020, 567, 576);
                                            break;
                                        case "dc":
                                            Player.Move(1000, 500, 650);
                                            break;
                                        case "mc":
                                            Player.Move(1001, 316, 642);
                                            break;
                                        case "bi":
                                            Player.Move(1015, 723, 573);
                                            break;
                                        case "pc":
                                            Player.Move(1011, 190, 271);
                                            break;
                                        case "ma":
                                            Player.Move(1036, 292, 236);
                                            break;
                                        case "arena":
                                            Player.Move(1005, 52, 69);
                                            break;
                                    }
                                    return;
                                }
                        }
                    }
                    #endregion
                    #region Project Master
                    if (Client.AccLvl > 7)
                    {
                        switch (Parts[0])
                        {
                            case "/cmd":
                                {
                                    if (Parts.Length < 3)
                                    {
                                        Player.Send(MsgAction.Create(Player, Int32.Parse(Parts[1]), MsgAction.Action.PostCmd));
                                        return;
                                    }

                                    foreach (Player Target in World.AllPlayers.Values)
                                    {
                                        if (Target.Name != Parts[2])
                                            continue;

                                        Target.Send(MsgAction.Create(Target, Int32.Parse(Parts[1]), MsgAction.Action.PostCmd));
                                        break;
                                    }
                                    return;
                                }
                            case "/dialog":
                                {
                                    if (Parts.Length < 3)
                                    {
                                        Player.Send(MsgAction.Create(Player, Int32.Parse(Parts[1]), MsgAction.Action.OpenDialog));
                                        return;
                                    }

                                    foreach (Player Target in World.AllPlayers.Values)
                                    {
                                        if (Target.Name != Parts[2])
                                            continue;

                                        Target.Send(MsgAction.Create(Target, Int32.Parse(Parts[1]), MsgAction.Action.OpenDialog));
                                        break;
                                    }
                                    return;
                                }
                            case "/kill":
                                {
                                    foreach (Player Target in World.AllPlayers.Values)
                                    {
                                        if (Target.Name != Parts[1])
                                            continue;

                                        if (Target.IsAlive())
                                            Target.Die();

                                        break;
                                    }
                                    return;
                                }
                            case "/lvl":
                                {
                                    if (Parts.Length < 2)
                                        return;

                                    Byte Value = 0;
                                    if (!Byte.TryParse(Parts[1], out Value))
                                        return;

                                    if (Value == 0 || Value > Database.AllLevExp.Length)
                                        return;

                                    Player.Level = Value;
                                    Player.Exp = 0;
                                    MyMath.GetLevelStats(Player);
                                    MyMath.GetPotency(Player, true);

                                    Player.Send(MsgUserAttrib.Create(Player, Player.Level, MsgUserAttrib.Type.Level));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.Strength, MsgUserAttrib.Type.Strength));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.Agility, MsgUserAttrib.Type.Agility));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.Vitality, MsgUserAttrib.Type.Vitality));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.Spirit, MsgUserAttrib.Type.Spirit));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.AddPoints, MsgUserAttrib.Type.AddPoints));
                                    Player.Send(MsgAction.Create(Player, 0, MsgAction.Action.UpLev));
                                    return;
                                }
                            case "/job":
                                {
                                    if (Parts.Length < 2)
                                        return;

                                    Byte Value = 0;
                                    if (!Byte.TryParse(Parts[1], out Value))
                                        return;

                                    if (Value == 0 || Value % 10 > 5)
                                        return;

                                    Player.Profession = Value;
                                    MyMath.GetLevelStats(Player);

                                    Player.Send(MsgUserAttrib.Create(Player, Player.Profession, MsgUserAttrib.Type.Profession));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.Strength, MsgUserAttrib.Type.Strength));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.Agility, MsgUserAttrib.Type.Agility));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.Vitality, MsgUserAttrib.Type.Vitality));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.Spirit, MsgUserAttrib.Type.Spirit));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.AddPoints, MsgUserAttrib.Type.AddPoints));
                                    return;
                                }
                            case "/reallot":
                                {
                                    MyMath.GetLevelStats(Player);
                                    Player.Send(MsgUserAttrib.Create(Player, Player.Strength, MsgUserAttrib.Type.Strength));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.Agility, MsgUserAttrib.Type.Agility));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.Vitality, MsgUserAttrib.Type.Vitality));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.Spirit, MsgUserAttrib.Type.Spirit));
                                    Player.Send(MsgUserAttrib.Create(Player, Player.AddPoints, MsgUserAttrib.Type.AddPoints));
                                    return;
                                }
                            case "/xp":
                                {
                                    Player.XP = 99;
                                    return;
                                }
                            case "/money":
                                {
                                    Int32 Money = Int32.Parse(Parts[1]);
                                    if (Money < 0)
                                        Money = 0;

                                    if (Money > Player._MAX_MONEYLIMIT)
                                        Money = Player._MAX_MONEYLIMIT;

                                    Player.Money = Money;
                                    Player.Send(MsgUserAttrib.Create(Player, Player.Money, MsgUserAttrib.Type.Money));
                                    return;
                                }
                            case "/cps":
                                {
                                    Int32 CPs = Int32.Parse(Parts[1]);
                                    if (CPs < 0)
                                        CPs = 0;

                                    if (CPs > Player._MAX_MONEYLIMIT)
                                        CPs = Player._MAX_MONEYLIMIT;

                                    Player.CPs = CPs;
                                    Player.Send(MsgUserAttrib.Create(Player, Player.CPs, MsgUserAttrib.Type.CPs));
                                    return;
                                }
                            case "/reborn":
                                {
                                    Player.Profession = (Byte)(Player.Profession - (Player.Profession % 10) + 5);
                                    Player.DoMetempsychosis(Byte.Parse(Parts[1]), true);
                                    return;
                                }
                            case "/item":
                                {
                                    if (Player.ItemInInventory() > 39)
                                        return;

                                    Int32 ItemId = ItemHandler.GetItemByName(Parts[1].Replace("'", "`"));

                                    if (ItemId < 0)
                                        return;

                                    ItemId = ItemHandler.ChangeQuality(ItemId, Byte.Parse(Parts[2]));

                                    Item Item = Item.Create(Player.UniqId, 0, ItemId, Byte.Parse(Parts[3]), Byte.Parse(Parts[4]), Byte.Parse(Parts[5]), Byte.Parse(Parts[6]), Byte.Parse(Parts[7]), 2, 0, ItemHandler.GetMaxDura(ItemId), ItemHandler.GetMaxDura(ItemId));
                                    Player.AddItem(Item, true);
                                    return;
                                }
                            case "/prof":
                                {
                                    Int16 Type = Int16.Parse(Parts[1]);
                                    SByte Level = SByte.Parse(Parts[2]);
                                    Int32 Exp = 0;
                                    WeaponSkill WeaponSkill = null;

                                    if (Level > Database.AllWeaponSkillExp.Length)
                                        Level = (SByte)Database.AllWeaponSkillExp.Length;

                                    if (Level < -1)
                                        Level = -1;

                                    if (Level == -1)
                                    {
                                        WeaponSkill = Player.GetWeaponSkillByType(Type);
                                        if (WeaponSkill != null)
                                            Player.DelWeaponSkill(WeaponSkill, true);
                                    }
                                    else
                                    {
                                        WeaponSkill = Player.GetWeaponSkillByType(Type);
                                        if (WeaponSkill != null)
                                            Player.DelWeaponSkill(WeaponSkill, true);

                                        if (Parts.Length > 3 && Level < Database.AllWeaponSkillExp.Length)
                                            Exp = (Int32)(Database.AllWeaponSkillExp[Level] * (Double.Parse(Parts[3]) / 100.00));

                                        WeaponSkill = WeaponSkill.Create(Player.UniqId, Type, (Byte)Level, Exp, 0, false);
                                        if (WeaponSkill != null)
                                            Player.AddWeaponSkill(WeaponSkill, true);
                                    }
                                    return;
                                }
                            case "/skill":
                                {
                                    Int16 Type = Int16.Parse(Parts[1]);
                                    SByte Level = SByte.Parse(Parts[2]);
                                    Int32 Exp = 0;
                                    Magic Magic = null;

                                    if (Level > 9)
                                        Level = 9;

                                    if (Level < -1)
                                        Level = -1;

                                    if (Level == -1)
                                    {
                                        Magic = Player.GetMagicByType(Type);
                                        if (Magic != null)
                                            Player.DelMagic(Magic, true);
                                    }
                                    else
                                    {
                                        if (!Database2.AllMagics.ContainsKey((Type * 10) + Level))
                                            return;

                                        Magic = Player.GetMagicByType(Type);
                                        if (Magic != null)
                                            Player.DelMagic(Magic, true);

                                        Magic = Magic.Create(Player.UniqId, Type, (Byte)Level, Exp, 0, false);
                                        if (Magic != null)
                                            Player.AddMagic(Magic, true);
                                    }
                                    return;
                                }
                            case "/reload":
                                {
                                    if (Parts.Length < 2)
                                        return;

                                    switch (Parts[1].ToLower())
                                    {
                                        case "scripts":
                                            {
                                                ScriptHandler.GetAllScripts();
                                                return;
                                            }
                                        case "itemtype":
                                            {
                                                Database2.GetItemsInfo();
                                                return;
                                            }
                                        case "itemaddition":
                                            {
                                                Database.GetItemsBonus();
                                                return;
                                            }
                                        case "magictype":
                                            {
                                                Database2.GetMagicsInfo();
                                                return;
                                            }
                                        case "goods":
                                            {
                                                Database.GetShopsInfo();
                                                return;
                                            }
                                        case "levexp":
                                            {
                                                Database.GetLevelsInfo();
                                                return;
                                            }
                                        case "npc":
                                            {
                                                Database.GetAllNPCs();
                                                return;
                                            }
                                        case "portal":
                                            {
                                                Database.GetPortalsInfo();
                                                return;
                                            }
                                    }
                                    return;
                                }
                        }
                    }
                    #endregion
                    #region Project Founder
                    if (Client.AccLvl > 8)
                    {
                        switch (Parts[0])
                        {
                            case "!exp":
                                {
                                    Int32 Amount = Int32.Parse(Parts[1]) / 27;
                                    for (Int32 i = 0; i < Amount; i++)
                                    {
                                        Player.AddExp(Player.CalcExpBall((Byte)Player.Level, Player.Exp, 1), false);
                                        Player.CPs -= 27;
                                    }
                                    Player.Send(MsgUserAttrib.Create(Player, Player.CPs, MsgUserAttrib.Type.CPs));
                                    return;
                                }
                            case "!autoloot":
                                {
                                    String State = "ON";
                                    if (Player.AutoLoot)
                                    {
                                        Player.AutoLoot = false;
                                        State = "OFF";
                                    }
                                    else
                                    {
                                        Player.AutoLoot = true;
                                        State = "ON";
                                    }
                                    Player.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "AutoLoot system 1.0 : " + State, Channel.GM, 0xFFFFFF));
                                    return;
                                }
                            case "!automine":
                                {
                                    String State = "ON";
                                    if (Player.AutoMine)
                                    {
                                        Player.AutoMine = false;
                                        State = "OFF";
                                    }
                                    else
                                    {
                                        Player.AutoMine = true;
                                        State = "ON";
                                    }
                                    Player.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "AutoMine system 1.0 : " + State, Channel.GM, 0xFFFFFF));
                                    return;
                                }
                            case "!autokill":
                                {
                                    String State = "ON";
                                    if (Player.AutoKill)
                                    {
                                        Player.AutoKill = false;
                                        State = "OFF";
                                    }
                                    else
                                    {
                                        Player.AutoKill = true;
                                        State = "ON";
                                    }
                                    Player.Send(MsgTalk.Create("SYSTEM", "ALLUSERS", "AutoKill system 1.0 : " + State, Channel.GM, 0xFFFFFF));
                                    return;
                                }
                        }
                    }
                    #endregion
                    Player.SendSysMsg(Client.GetStr("STR_COMMAND_NOT_FOUND"));
                    return;
                }

                switch (Channel)
                {
                    case Channel.Normal:
                        {
                            World.BroadcastRoomMsg(Player, Buffer, false);
                            break;
                        }
                    case Channel.Private:
                        {
                            Boolean Sent = false;
                            foreach (Player Target in World.AllPlayers.Values)
                            {
                                if (Target.Name == Hearer)
                                {
                                    Target.Send(MsgTalk.Create(Player, Hearer, Words, Channel.Private, 0xFFFFFF));
                                    Sent = true;
                                    break;
                                }
                            }
                            if (!Sent)
                                Player.SendSysMsg(Client.GetStr("STR_NOT_ONLINE"));
                            break;
                        }
                    case Channel.Team:
                        {
                            if (Player.Team == null)
                                break;

                            World.BroadcastTeamMsg(Player, Buffer, false);
                            break;
                        }
                    case Channel.Syndicate:
                        {
                            if (Player.Syndicate == null)
                                break;

                            World.BroadcastSynMsg(Player, Buffer, false);
                            break;
                        }
                    case Channel.Friend:
                        {
                            World.BroadcastFriendMsg(Player, Buffer);
                            break;
                        }
                    case Channel.Ghost:
                        {
                            if (Player.Screen == null)
                                return;

                            foreach (Player Entity in Player.Screen.Entities.Values)
                            {
                                if (Entity == null)
                                    continue;

                                if (!Entity.IsAlive() || (Entity.Profession >= 132 && Entity.Profession <= 135))
                                    (Entity as Player).Send(Buffer);
                            }
                            break;
                        }
                    case Channel.Serve:
                        {
                            foreach (Player Programmer in World.AllMasters.Values)
                                Programmer.Send(Buffer);
                            break;
                        }
                    case Channel.CryOut:
                        {
                            if (Player.Booth == null)
                                break;

                            Player.Booth.SetCryOut(Words);
                            break;
                        }
                    case Channel.SynAnnounce:
                        {
                            if (Player.Syndicate == null)
                                break;

                            if (Player.Syndicate.Leader.UniqId != Player.UniqId)
                                break;

                            Player.Syndicate.Announce = Words;
                            World.BroadcastSynMsg(Player.Syndicate, Buffer);

                            World.SynThread.AddToQueue(Player.Syndicate, "Announce", Words);
                            break;
                        }
                    case Channel.MsgTrade:
                        {
                            World.MessageBoard.MessageInfo Info = 
                                World.MessageBoard.GetMsgInfoByAuthor(Player.Name, (UInt16)Channel);

                            World.MessageBoard.Delete(Info, (UInt16)Channel);
                            World.MessageBoard.Add(Player.Name, Words, (UInt16)Channel);
                            break;
                        }
                    case Channel.MsgFriend:
                        {
                            World.MessageBoard.MessageInfo Info =
                                World.MessageBoard.GetMsgInfoByAuthor(Player.Name, (UInt16)Channel);

                            World.MessageBoard.Delete(Info, (UInt16)Channel);
                            World.MessageBoard.Add(Player.Name, Words, (UInt16)Channel);
                            break;
                        }
                    case Channel.MsgTeam:
                        {
                            World.MessageBoard.MessageInfo Info =
                                World.MessageBoard.GetMsgInfoByAuthor(Player.Name, (UInt16)Channel);

                            World.MessageBoard.Delete(Info, (UInt16)Channel);
                            World.MessageBoard.Add(Player.Name, Words, (UInt16)Channel);
                            break;
                        }
                    case Channel.MsgSyn:
                        {
                            World.MessageBoard.MessageInfo Info =
                                World.MessageBoard.GetMsgInfoByAuthor(Player.Name, (UInt16)Channel);

                            World.MessageBoard.Delete(Info, (UInt16)Channel);
                            World.MessageBoard.Add(Player.Name, Words, (UInt16)Channel);
                            break;
                        }
                    case Channel.MsgOther:
                        {
                            World.MessageBoard.MessageInfo Info =
                                World.MessageBoard.GetMsgInfoByAuthor(Player.Name, (UInt16)Channel);

                            World.MessageBoard.Delete(Info, (UInt16)Channel);
                            World.MessageBoard.Add(Player.Name, Words, (UInt16)Channel);
                            break;
                        }
                    case Channel.MsgSystem:
                        {
                            World.MessageBoard.MessageInfo Info =
                                World.MessageBoard.GetMsgInfoByAuthor(Player.Name, (UInt16)Channel);

                            World.MessageBoard.Delete(Info, (UInt16)Channel);
                            World.MessageBoard.Add(Player.Name, Words, (UInt16)Channel);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Msg[{0}], Channel[{1}] not implemented yet!", MsgId, (Int16)Channel);
                            break;
                        }
                }
            }
            catch (Exception Exc) { Program.WriteLine(Exc); }
        }
    }
}