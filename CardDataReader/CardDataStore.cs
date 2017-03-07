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
            return FormatSkillForSimulator(skill["Name"].ToString());
        }

        private string FormatSkillForSimulator(string skillString)
        {
            skillString = skillString.Replace("[", "");
            skillString = skillString.Replace("]", "");
            skillString = skillString.Replace("：", "");
            skillString = skillString.Replace("!", "");
            skillString = skillString.Replace(" ", "");
            return skillString;
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

        private static Dictionary<string, string> achievementIds = new Dictionary<string, string> {
            {@"0", @"Any"},
            {@"1", @"Round:40"},
            {@"2", @"Round:36"},
            {@"3", @"Round:32"},
            {@"4", @"Round:28"},
            {@"5", @"Round:24"},
            {@"6", @"Round:20"},
            {@"7", @"Round:16"},
            {@"8", @"Round:14"},
            {@"11", @"MyDeadCard:5"},
            {@"12", @"MyDeadCard:4"},
            {@"13", @"MyDeadCard:3"},
            {@"14", @"MyDeadCard:2"},
            {@"15", @"MyDeadCard:1"},
            {@"16", @"MyHeroHP:60"},
            {@"17", @"MyHeroHP:70"},
            {@"18", @"MyHeroHP:80"},
            {@"19", @"MyHeroHP:90"},
            {@"20", @"MyHeroHP:100"},
            {@"21", @"CardOfRace:K:2"},
            {@"22", @"CardOfRace:K:3"},
            {@"23", @"CardOfRace:K:4"},
            {@"24", @"CardOfRace:K:5"},
            {@"25", @"CardOfRace:K:6"},
            {@"26", @"CardOfRace:F:2"},
            {@"27", @"CardOfRace:F:3"},
            {@"28", @"CardOfRace:F:4"},
            {@"29", @"CardOfRace:F:5"},
            {@"30", @"CardOfRace:F:6"},
            {@"31", @"CardOfRace:H:2"},
            {@"32", @"CardOfRace:H:3"},
            {@"33", @"CardOfRace:H:4"},
            {@"34", @"CardOfRace:H:5"},
            {@"35", @"CardOfRace:H:6"},
            {@"36", @"CardOfRace:S:2"},
            {@"37", @"CardOfRace:S:3"},
            {@"38", @"CardOfRace:S:4"},
            {@"39", @"CardOfRace:S:5"},
            {@"40", @"CardOfRace:S:6"},
            {@"41", @"CardOfStar:1:1"},
            {@"42", @"CardOfStar:1:2"},
            {@"43", @"CardOfStar:1:3"},
            {@"44", @"CardOfStar:2:1"},
            {@"45", @"CardOfStar:2:2"},
            {@"46", @"CardOfStar:2:3"},
            {@"47", @"CardOfStar:3:1"},
            {@"48", @"CardOfStar:3:2"},
            {@"49", @"CardOfStar:3:3"},
            {@"50", @"CardOfStar:4:1"},
            {@"51", @"CardOfStar:4:2"},
            {@"52", @"CardOfStar:4:3"},
            {@"56", @"HasRune:I"},
            {@"57", @"HasRune:F"},
            {@"58", @"HasRune:W"},
            {@"59", @"HasRune:G"},
            {@"60", @"NoRune:A"},
            {@"61", @"EnemyHeroDie"},
            {@"62", @"EnemyAllCardsDie"}
        };

        private string ConvertAchievementIdToCondition(string id)
        {
            if (achievementIds.ContainsKey(id))
            {
                return achievementIds[id];
            }
            else
            {
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
            return GetStageId(map, stage) + "-" + (level + 1);
        }

        private string GetStageId(int map, int stage)
        {
            string stageId = (stage + 1).ToString();
            switch (mapstore["data"].ElementAt(map)["MapStageDetails"].ElementAt(stage)["Type"].ToString())
            {
                case "0":
                    if (mapstore["data"].ElementAt(map)["MapStageDetails"].ElementAt(stage - 1)["Type"].ToString() != "2")
                    {
                        stageId = "H2";
                    }
                    else
                    {
                        stageId = "H";
                    }
                    break;
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                case "4":
                    stageId = "T";
                    break;
                default:
                    throw new Exception("Unknow stage type");
            }
            return (map + 1) + "-" + stageId;
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

        public string LoadMapOptions(int map)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format(@"                                    <optgroup label=""{0}-{1}"">", map + 1, mapstore["data"].ElementAt(map)["Name"]));
            for (int stage = 0; stage < mapstore["data"].ElementAt(map)["MapStageDetails"].Count(); stage++)
            {
                string stageType = mapstore["data"].ElementAt(map)["MapStageDetails"].ElementAt(stage)["Type"].ToString();
                if (stageType != "0" && stageType != "1" && stageType != "2")
                {
                    continue;
                }
                if (stage == 0)
                {
                    builder.AppendLine(string.Format(@"                                        <option value=""{0}"" selected=""selected"">{0} {1}</option>", GetStageId(map, stage), mapstore["data"].ElementAt(map)["MapStageDetails"].ElementAt(stage)["Name"]));
                }
                else
                {
                    builder.AppendLine(string.Format(@"                                        <option value=""{0}"">{0} {1}</option>", GetStageId(map, stage), mapstore["data"].ElementAt(map)["MapStageDetails"].ElementAt(stage)["Name"]));
                }
            }
            builder.AppendLine(@"                                    </optgroup>");
            return builder.ToString();
        }

        public string LoadAllMapOptions()
        {
            StringBuilder builder = new StringBuilder();
            int count = mapstore["data"].Count();
            for (int i = count - 1; i >= 0; i--)
            {
                builder.Append(LoadMapOptions(i));
            }
            return builder.ToString();
        }
    }
}
