using System.Collections.Generic;   //现代List<类型>工具箱
using UnityEngine;                  //导入Unity常用库
public static class StoryData
{
    // 根据天数返回剧情
    public static List<string> GetDialogue(int day)
    {
        switch (day)
        {
            case 1:
                return new List<string>
                {
                    "你醒了吗？我回来了哦。",
                    "今天是第1天呢……我们终于开始了。",
                    "不要乱跑，好吗？"
                };

            case 2:
                return new List<string>
                {
                    "你今天好像很安静。",
                    "是不是在偷偷想别的事情？",
                    "我会一直看着你的。"
                };

            case 3:
                return new List<string>
                {
                    "我今天回来得有点晚，你有没有想我？",
                    "如果你没想我，我会有点难过的。",
                    "只是有一点点哦。"
                };

            case 4:
                return new List<string>
                {
                    "你最近越来越不听话了呢。",
                    "不过没关系，我会慢慢教你的。",
                    "一点一点来就好。"
                };

            case 5:
                return new List<string>
                {
                    "今天路上我一直在想你。",
                    "想你在做什么，想你有没有看别的地方。",
                    "这种感觉……让我有点开心。"
                };

            case 6:
                return new List<string>
                {
                    "你有没有发现，我回家的时间变快了？",
                    "因为我想早点见到你。",
                    "只是这样而已。"
                };

            case 7:
                return new List<string>
                {
                    "已经第7天了呢。",
                    "你有没有习惯我的存在？",
                    "如果没有，我会让你习惯的。"
                };

            case 8:
                return new List<string>
                {
                    "今天我差点没忍住提前回来。",
                    "因为我想看看你在做什么。",
                    "不过我还是忍住了哦，乖吗？"
                };

            case 9:
                return new List<string>
                {
                    "你有没有对别人笑过？",
                    "如果有的话……也没关系。",
                    "我会让你只对我笑。"
                };

            case 10:
                return new List<string>
                {
                    "你最近越来越重要了。",
                    "重要到我有点不想让你离开视线。",
                    "一点点也不行。"
                };

            case 11:
                return new List<string>
                {
                    "我开始讨厌你不在的时候了。",
                    "房间变得很安静，让人不舒服。",
                    "所以你要一直在才行。"
                };

            case 12:
                return new List<string>
                {
                    "你已经习惯我了吗？",
                    "我希望是的。",
                    "因为我已经离不开你了。"
                };

            case 13:
                return new List<string>
                {
                    "终于到第13天了呢。",
                    "你已经没有办法离开我了，对吧？",
                    "因为我也不会让你离开了。"
                };

            default:
                return new List<string>
                {
                    "今天没有特别的事情发生。",
                    "但我一直在你身边。"
                };
        }
    }
}