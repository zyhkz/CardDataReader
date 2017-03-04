using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDataReader
{
    class CardDataStore
    {
        JObject cardstore;
        JObject runestore;
        JObject skillstore;
        JObject mapstore;

        public CardDataStore()
        {
            cardstore = JsonDataReader.ReadJsonFile(@"data\allcard");
            runestore = JsonDataReader.ReadJsonFile(@"data\allrune");
            skillstore = JsonDataReader.ReadJsonFile(@"data\allskill");
            mapstore = JsonDataReader.ReadJsonFile(@"data\allstage");
        }

        public string CardDataToString(string datastring)
        {
            StringBuilder builder = new StringBuilder();
            string[] properties = datastring.Split('_');
            string id = properties[0];
            if (id == "0")
            {
                return string.Empty;
            }
            JObject card = (JObject)cardstore["data"]["Cards"].Where(p => p["CardId"].ToString() == id).First();
            builder.Append(card["CardName"].ToString());
            if (properties.Length >= 3)
            {
                string skillid = properties[2];
                string SkillName = SkillDataToString(skillid);
                builder.Append("+" + SkillName);
            }
            if (properties.Length >= 2)
            {
                string level = properties[1];
                builder.Append("-" + level);
            }
            return builder.ToString();
        }

        public string SkillDataToString(string datastring)
        {
            JObject skill = (JObject)skillstore["data"]["Skills"].Where(p => p["SkillId"].ToString() == datastring).First();
            return skill["Name"].ToString();
        }

        public string RuneDataToString(string datastring)
        {
            StringBuilder builder = new StringBuilder();
            string[] properties = datastring.Split('_');
            string id = properties[0];
            JObject rune = (JObject)runestore["data"]["Runes"].Where(p => p["RuneId"].ToString() == id).First();
            builder.Append(rune["RuneName"].ToString());
            if (properties.Length >= 2)
            {
                string level = properties[1];
                builder.Append("-" + level);
            }
            return builder.ToString();
        }

        public string MapDataToDeck(int map, int stage, int level)
        {
            JObject mapdata = (JObject)mapstore["data"].ElementAt(map)["MapStageDetails"].ElementAt(stage)["Levels"].ElementAt(level);
            string cards = CardListToString(mapdata["CardList"].ToString());
            string runes = RuneListToString(mapdata["RuneList"].ToString());
            cards = cards.Replace("[", "");
            cards = cards.Replace("]", "");
            cards = cards.Replace("·", "");
            if (!string.IsNullOrWhiteSpace(runes))
            {
                return runes + "," + cards;
            }
            else
            {
                return cards;
            }
        }

        private string RuneListToString(string runelist)
        {
            StringBuilder builder = new StringBuilder();
            string[] list = runelist.Split(',');
            foreach (string rune in list)
            {
                if (string.IsNullOrWhiteSpace(rune)) continue;
                builder.Append(RuneDataToString(rune));
                builder.Append(",");
            }
            if (builder.Length >= 1)
                builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private string CardListToString(string cardlist)
        {
            StringBuilder builder = new StringBuilder();
            string[] list = cardlist.Split(',');
            foreach (string card in list)
            {
                if (string.IsNullOrWhiteSpace(card)) continue;
                builder.Append(CardDataToString(card));
                builder.Append(",");
            }
            if (builder.Length >= 1)
                builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private string ConvertAchievementIdToCondition(string id)
        {
            switch (id)
            {
                case "0":
                    return "Any";
                case "1":
                    return "Round:40";
                case "2":
                    return "Round:36";
                case "3":
                    return "Round:32";
                case "4":
                    return "Round:28";
                case "5":
                    return "Round:24";
                case "6":
                    return "Round:20";
                case "7":
                    return "Round:16";
                case "8":
                    return "Round:14";
                case "11":
                    return "MyDeadCard:5";
                case "12":
                    return "MyDeadCard:4";
                case "13":
                    return "MyDeadCard:3";
                case "14":
                    return "MyDeadCard:2";
                case "15":
                    return "MyDeadCard:1";
                case "16":
                    return "MyHeroHP:60";
                case "17":
                    return "MyHeroHP:70";
                case "18":
                    return "MyHeroHP:80";
                case "19":
                    return "MyHeroHP:90";
                case "20":
                    return "MyHeroHP:100";
                case "21":
                    return "CardOfRace:K:2";
                case "22":
                    return "CardOfRace:K:3";
                case "23":
                    return "CardOfRace:K:4";
                case "24":
                    return "CardOfRace:K:5";
                case "25":
                    return "CardOfRace:K:6";
                case "26":
                    return "CardOfRace:F:2";
                case "27":
                    return "CardOfRace:F:3";
                case "28":
                    return "CardOfRace:F:4";
                case "29":
                    return "CardOfRace:F:5";
                case "30":
                    return "CardOfRace:F:6";
                case "31":
                    return "CardOfRace:H:2";
                case "32":
                    return "CardOfRace:H:3";
                case "33":
                    return "CardOfRace:H:4";
                case "34":
                    return "CardOfRace:H:5";
                case "35":
                    return "CardOfRace:H:6";
                case "36":
                    return "CardOfRace:S:2";
                case "37":
                    return "CardOfRace:S:3";
                case "38":
                    return "CardOfRace:S:4";
                case "39":
                    return "CardOfRace:S:5";
                case "40":
                    return "CardOfRace:S:6";
                case "41":
                    return "CardOfStar:1:1";
                case "42":
                    return "CardOfStar:1:2";
                case "43":
                    return "CardOfStar:1:3";
                case "44":
                    return "CardOfStar:2:1";
                case "45":
                    return "CardOfStar:2:2";
                case "46":
                    return "CardOfStar:2:3";
                case "47":
                    return "CardOfStar:3:1";
                case "48":
                    return "CardOfStar:3:2";
                case "49":
                    return "CardOfStar:3:3";
                case "50":
                    return "CardOfStar:4:1";
                case "51":
                    return "CardOfStar:4:2";
                case "52":
                    return "CardOfStar:4:3";
                case "56":
                    return "HasRune:I";
                case "57":
                    return "HasRune:F";
                case "58":
                    return "HasRune:W";
                case "59":
                    return "HasRune:G";
                case "60":
                    return "NoRune:A";
                case "61":
                    return "EnemyHeroDie";
                case "62":
                    return "EnemyAllCardsDie";
                default:
                    throw new Exception("Unknown AchievementId: " + id);
            }
        }

        public string LoadAllMaps()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
            builder.AppendLine(@"<Maps>");
            for (int map = 0; map < mapstore["data"].Count(); map++)
            {
                for (int stage = 0; stage < mapstore["data"].ElementAt(map)["MapStageDetails"].Count(); stage++)
                {
                    for (int level = 0; level < mapstore["data"].ElementAt(map)["MapStageDetails"].ElementAt(stage)["Levels"].Count(); level++)
                    {
                        JObject mapdata = (JObject)mapstore["data"].ElementAt(map)["MapStageDetails"].ElementAt(stage)["Levels"].ElementAt(level);
                        string victory = ConvertAchievementIdToCondition(mapdata["AchievementId"].ToString());
                        string herohp = GetHeroHpByLevel(int.Parse(mapdata["HeroLevel"].ToString())).ToString();
                        string mapid = GetMapId(map, stage, level);
                        string deck = MapDataToDeck(map, stage, level);
                        builder.AppendLine(string.Format(@"    <Map victory=""{0}"" heroHP=""{1}"" id=""{2}"">{3}</Map>", victory, herohp, mapid, deck));
                    }
                    for (int level = 0; level < mapstore["data"].ElementAt(map)["MapStageDetails"].ElementAt(stage)["Levels"].Count(); level++)
                    {
                        string mapid = GetMapId(map, stage, level);
                        String wpDeck = GetWPDeck(mapid);
                        if (!string.IsNullOrWhiteSpace(wpDeck))
                        {
                            builder.AppendLine(wpDeck);
                        }
                    }
                }
            }
            builder.AppendLine(@"</Maps>");
            return builder.ToString();
        }

        private static Dictionary<string, string> wpDecks = new Dictionary<string, string>{
            {@"16-H-1", @"    <Map victory=""Any"" heroHP=""24900"" id=""16-H-wp-1"">玄石-4,寒伤-4,鬼步-4,龙吟-4,怒雪咆哮+不动-15,魔幻人偶师+免疫-15,陨星魔法使+全体加速2-15,圆月魔女+冰甲8-15,圆月魔女+回魂3-15,星夜女神+弱点攻击-11,圣灵大祭司+免疫-15,天界守护者+横扫-15,死域军神+不动-15,堕落天使+战争怒吼-12,星辰主宰+不动-15,星辰主宰+免疫-15,战场扫荡者+免疫-13,星夜女神+弱点攻击-15,赤面天狗+免疫-15,蛮荒大毒汁怪-10</Map>"},
            {@"16-H-2", @"    <Map victory=""CardOfStar:1:1"" heroHP=""25660"" id=""16-H-wp-2"">玄石-4,寒伤-4,鬼步-4,龙吟-4,怒雪咆哮+不动-15,魔幻人偶师+免疫-15,陨星魔法使+全体加速2-15,圆月魔女+冰甲8-15,圆月魔女+回魂3-15,星夜女神+弱点攻击-15,圣灵大祭司+免疫-11,天界守护者+横扫-15,死域军神+不动-15,堕落天使+战争怒吼-15,星辰主宰+不动-15,星辰主宰+免疫-15,战场扫荡者+免疫-15,星夜女神+弱点攻击-12,赤面天狗+免疫-15,蛮荒大毒汁怪-10</Map>"},
            {@"16-H-3", @"    <Map victory=""MyHeroHP:90"" heroHP=""26420"" id=""16-H-wp-3"">玄石-4,寒伤-4,鬼步-4,龙吟-4,怒雪咆哮+不动-15,魔幻人偶师+免疫-15,陨星魔法使+全体加速2-15,圆月魔女+冰甲8-15,圆月魔女+回魂3-15,星夜女神+弱点攻击-15,圣灵大祭司+免疫-15,天界守护者+横扫-15,死域军神+不动-15,堕落天使+战争怒吼-15,星辰主宰+不动-15,星辰主宰+免疫-15,战场扫荡者+免疫-15,星夜女神+弱点攻击-15,赤面天狗+免疫-15,蛮荒大毒汁怪-10</Map>"},
            {@"17-5-1", @"    <Map victory=""Any"" heroHP=""26420"" id=""17-5-wp-1"">龙吟-4,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,谎言之神+复活</Map>"},
            {@"17-5-2", @"    <Map victory=""MyDeadCard:3"" heroHP=""26420"" id=""17-5-wp-2"">龙吟-4,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,谎言之神+复活,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10</Map>"},
            {@"17-5-3", @"    <Map victory=""NoRune:A"" heroHP=""26420"" id=""17-5-wp-3"">龙吟-4,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,谎言之神+复活,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10,蛮荒大毒汁之王+冰甲10</Map>"},
            {@"18-6-1", @"    <Map victory=""Any"" heroHP=""26420"" id=""18-6-wp-1"">龙吟-4,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,谎言之神+末世术9-15</Map>"},
            {@"18-6-2", @"    <Map victory=""EnemyAllCardsDie"" heroHP=""26420"" id=""18-6-wp-2"">龙吟-4,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,谎言之神+末世术9-15</Map>"},
            {@"18-6-3", @"    <Map victory=""NoRune:A"" heroHP=""26420"" id=""18-6-wp-3"">龙吟-4,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,蛮荒大毒汁之王+降临沉默-15,谎言之神+末世术9-15</Map>"},
            {@"18-H2-1", @"    <Map victory=""Any"" heroHP=""26420"" id=""18-H2-wp-1"">龙吟-4,蛮荒大毒汁怪+转生8-15,蛮荒大毒汁怪+星云锁链-15,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10</Map>"},
            {@"18-H2-2", @"    <Map victory=""NoRune:A"" heroHP=""26420"" id=""18-H2-wp-2"">龙吟-4,蛮荒大毒汁怪+转生8-15,蛮荒大毒汁怪+星云锁链-15,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10</Map>"},
            {@"18-H2-3", @"    <Map victory=""Round:20"" heroHP=""26420"" id=""18-H2-wp-3"">龙吟-4,蛮荒大毒汁怪+转生8-15,蛮荒大毒汁怪+星云锁链-15,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10,蛮荒大毒汁怪-10</Map>"},
            {@"19-6-1", @"    <Map victory=""Any"" heroHP=""29500"" id=""19-6-wp-1"">狂战-4,鹰眼-4,鬼步-4,龙吟-4,蛮荒大毒汁之王+精准打击10-15,蛮荒大毒汁怪+全体沉默-15,蛮荒大毒汁怪+魔法毁灭-15,蛮荒大毒汁怪+魔法毁灭-15,蛮荒大毒汁之王+连击-15,蛮荒大毒汁之王+转生10-15,蛮荒大毒汁之王+镜面装甲-15,蛮荒大毒汁之王+镜面装甲-15,蛮荒大毒汁之王+传送-15,蛮荒大毒汁之王+圣母回声-15,蛮荒大毒汁之王+扼杀-15,蛮荒大毒汁之王+连击-15,蛮荒大毒汁之王+摧毁-15,蛮荒大毒汁之王+月神的护佑9-15,谎言之神+祈福10-15</Map>"},
            {@"19-6-2", @"    <Map victory=""CardOfStar:1:1"" heroHP=""29500"" id=""19-6-wp-2"">狂战-4,鹰眼-4,鬼步-4,龙吟-4,蛮荒大毒汁之王+精准打击10-15,蛮荒大毒汁怪+全体沉默-15,蛮荒大毒汁怪+魔法毁灭-15,蛮荒大毒汁怪+魔法毁灭-15,蛮荒大毒汁之王+连击-15,蛮荒大毒汁之王+转生10-15,蛮荒大毒汁之王+镜面装甲-15,蛮荒大毒汁之王+镜面装甲-15,蛮荒大毒汁之王+传送-15,蛮荒大毒汁之王+圣母回声-15,蛮荒大毒汁之王+扼杀-15,蛮荒大毒汁之王+连击-15,蛮荒大毒汁之王+摧毁-15,蛮荒大毒汁之王+月神的护佑9-15,谎言之神+祈福10-15</Map>"},
            {@"19-6-3", @"    <Map victory=""CardOfStar:3:3"" heroHP=""29500"" id=""19-6-wp-3"">狂战-4,鹰眼-4,鬼步-4,龙吟-4,蛮荒大毒汁之王+精准打击10-15,蛮荒大毒汁怪+全体沉默-15,蛮荒大毒汁怪+魔法毁灭-15,蛮荒大毒汁怪+魔法毁灭-15,蛮荒大毒汁之王+连击-15,蛮荒大毒汁之王+转生10-15,蛮荒大毒汁之王+镜面装甲-15,蛮荒大毒汁之王+镜面装甲-15,蛮荒大毒汁之王+传送-15,蛮荒大毒汁之王+圣母回声-15,蛮荒大毒汁之王+扼杀-15,蛮荒大毒汁之王+连击-15,蛮荒大毒汁之王+摧毁-15,蛮荒大毒汁之王+月神的护佑9-15,谎言之神+祈福10-15</Map>"}
        };

        private string GetWPDeck(string mapid)
        {
            if (wpDecks.ContainsKey(mapid))
            {
                return wpDecks[mapid];
            }
            else
            {
                return null;
            }
        }

        private string GetMapId(int map, int stage, int level)
        {
            map = map + 1;
            stage = stage + 1;
            level = level + 1;
            String mapid = map + "-" + stage + "-" + level;

            mapid = mapid.Replace("-13-", "-H2-");
            int lastStage = 0;
            if (map >= 11)
            {
                lastStage = 12;
            }
            else if (map >= 8)
            {
                lastStage = 11;
            }
            else if (map == 7)
            {
                lastStage = 10;
            }
            else if (map >= 5)
            {
                lastStage = 9;
            }
            else if (map >= 3)
            {
                lastStage = 8;
            }

            if (lastStage != 0)
            {
                mapid = mapid.Replace(string.Format("-{0}-", lastStage), "-H-");
            }
            return mapid;
        }

        private static int[] hps = new int[] {
        0,
        1000,  1070,  1140,  1210,  1280,  1350,  1420,  1490,  1560,  1630,
        1800,  1880,  1960,  2040,  2120,  2200,  2280,  2360,  2440,  2520,
        2800,  2890,  2980,  3070,  3160,  3250,  3340,  3430,  3520,  3610,
        4000,  4100,  4200,  4300,  4400,  4500,  4600,  4700,  4800,  4900,
        5400,  5510,  5620,  5730,  5840,  5950,  6060,  6170,  6280,  6390,
        7000,  7120,  7240,  7360,  7480,  7600,  7720,  7840,  7960,  8080,
        8800,  8930,  9060,  9190,  9320,  9450,  9580,  9710,  9840,  9970,
        10800, 10940, 11080, 11220, 11360, 11500, 11640, 11780, 11920, 12060,
        13000, 13180, 13360, 13540, 13720, 13900, 14080, 14260, 14440, 14620,
        15700, 15920, 16140, 16360, 16580, 16800, 17020, 17240, 17460, 17680,
        18900, 19200, 19500, 19800, 20100, 20400, 20700, 21000, 21300, 21600,
        23000, 23380, 23760, 24140, 24520, 24900, 25280, 25660, 26040, 26420,
        26800, 27100, 27500, 27800, 28200, 28600, 29000, 29500, 30000, 30500
        };

        private int GetHeroHpByLevel(int level)
        {
            if (level >= 0 && level < hps.Length)
            {
                return hps[level];
            }
            else
            {
                return level * 200;
            }
        }

        public string LoadAllMapOptions()
        {
            throw new NotImplementedException();
        }
    }
}
