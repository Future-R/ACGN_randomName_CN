using System;
using System.Text.RegularExpressions;
using XQ.Net.SDK.Attributes;
using XQ.Net.SDK.EventArgs;
[assembly: XQPlugin("二次元起名器", "未来", "1.0.4", "味儿很冲の二次元作品名&网名随机作成！")]
namespace 二次元起名器
{
    [Plugin]
    public class Root
    {
        static string[] 默认前缀
        {
            get
            {
                return new[] { "少女", "旅人", "勇者", "魔神", "终焉", "灰烬", "空想", "弑神", "奥术", "虚空", "神授", "诸神", "灵子", "不灭", "湮灭", "超载", "奇迹", "共鸣", "嗜血", "灵魂", "灾厄", "究极", "超越", "光明", "混沌", "希望", "绝望", "灭亡", "无限", "无想", "悲鸣", "原初", "心象", "星屑", "白夜", "零点", "绝对", "幻想", "沉默", "熔解", "弥生", "水无", "千年", "暴走", "青枫", "浮世", "胧月", "红莲", "黄泉", "鬼杀", "妖怪", "人间", "伊甸", "银河", "破碎", "真理", "空想", "天命", "死亡", "爆裂", "异邦", "の用户昵称" };
            }
        }
        static string[] 默认后缀
        {
            get
            {
                return new[] { "代码", "计划", "指令", "结界", "领域", "地带", "力量", "王冠", "次元", "风暴", "黎明", "地狱", "枷锁", "梦境", "轮回", "沙漏", "残响", "之花", "之魂", "之翼", "之心", "默示录", "序曲", "终章", "诗篇", "战车", "阳炎", "红炎", "失格", "宣告", "审判", "契约", "乐队", "杀手", "使徒", "统领", "纹章", "深渊", "鸟居", "特攻", "游戏", "英雄", "余晖", "纪元", "核心", "武装", "战争", "此端", "彼端", "纷争", "教会", "禁止", "转生", "三日月", "圆舞曲", "协奏曲", "奏鸣曲", "狂想曲", "华尔兹", "理想乡", "决战兵器" };
            }
        }

        [PrivateMsgEvent]
        public static void onPrivateMsg(object sender, XQAppPrivateMsgEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Regex.Match(e.Message.Text, @"^(起名|取名)( )?\d{0,2}$").Value))
            {
                e.FromQQ.SendMsg(e.RobotQQ, 起名(e.Message.Text).Replace("用户昵称", $"{e.FromQQ.GetNick(e.RobotQQ)}"));
            }
            else
            {
                return;
            }
        }
        [GroupMsgEvent]
        public static void onGroupMsg(object sender, XQAppGroupMsgEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Regex.Match(e.Message.Text, @"^(起名|取名)( )?\d{0,2}$").Value))
            {
                e.FromGroup.SendMessage(e.RobotQQ, 起名(e.Message.Text).Replace("用户昵称", $"{e.FromQQ.GetNick(e.RobotQQ)}"));
            }
            else
            {
                return;
            }
        }

        static string 起名(string 输入文本)
        {
            try
            {
                if (输入文本.Trim().Length == 2)
                {
                    return 随机组合();
                }
                else
                {
                    int 起名数量 = Convert.ToInt32(输入文本.Trim().Substring(2));
                    string 返回值 = "";
                    for (int i = 0; i < 起名数量; i++)
                    {
                        返回值 += $"、{随机组合()}";
                    }
                    return 返回值.TrimStart('、');
                }
            }
            catch (Exception 报错)
            {
                return "二次元起名器发生错误:" + Environment.NewLine + 报错.StackTrace;
            }
        }

        static string 随机组合()
        {
            int 前缀序号 = new Random(Guid.NewGuid().GetHashCode()).Next(0, Convert.ToInt32(默认前缀.Length));
            int 后缀序号 = new Random(Guid.NewGuid().GetHashCode()).Next(0, Convert.ToInt32(默认后缀.Length));
            string 代号 = "";
            string 物语 = "";
            switch (new Random(Guid.NewGuid().GetHashCode()).Next(0, 200))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    代号 = "代号:";
                    break;
                case 6:
                case 7:
                    代号 = "Re:";
                    break;
                case 8:
                    代号 = "The ";
                    break;
                case 9:
                    代号 = "此乃";
                    break;
                case 10:
                    代号 = "其为";
                    break;
                case 11:
                case 12:
                    物语 = "物语";
                    break;
                case 13:
                case 14:
                case 15:
                    物语 = "传说";
                    break;
                case 16:
                    物语 = "与猫";
                    break;
                case 17:
                    物语 = "战线";
                    break;
                case 18:
                    物语 = "战纪";
                    break;
                case 19:
                    物语 = "英雄传";
                    break;
                default:
                    break;
            }
            //3%概率追加前缀
            var 追加前缀 = "";
            if (new Random(Guid.NewGuid().GetHashCode()).Next(0, 100) < 4)
            {
                追加前缀 = 默认前缀[new Random(Guid.NewGuid().GetHashCode()).Next(0, Convert.ToInt32(默认前缀.Length))];
            }
            var 星星 = "";
            //3%概率加☆
            if (new Random(Guid.NewGuid().GetHashCode()).Next(0, 100) < 4)
            {
                星星 = "☆";
            }
            //5%概率出前后反转
            if (new Random(Guid.NewGuid().GetHashCode()).Next(0, 100) < 5)
            {
                //介词不能放在最前
                if (默认后缀[后缀序号].StartsWith("の") || 默认后缀[后缀序号].StartsWith("之"))
                {
                    return 代号 + 追加前缀 + 默认后缀[后缀序号].Substring(1) + "の" + 默认前缀[前缀序号] + 物语;
                }
                return 代号 + 追加前缀 + 默认后缀[后缀序号] + 星星 + 默认前缀[前缀序号] + 物语;
            }
            //正常输出
            if (默认前缀[前缀序号].StartsWith("の") || 默认前缀[前缀序号].StartsWith("之"))
            {
                return 代号 + 追加前缀 + 默认前缀[前缀序号].Substring(1) + "の" + 默认后缀[后缀序号] + 物语;
            }
            return 代号 + 追加前缀 + 默认前缀[前缀序号] + 星星 + 默认后缀[后缀序号] + 物语;
        }
    }
}