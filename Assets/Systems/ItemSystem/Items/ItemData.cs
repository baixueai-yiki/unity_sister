using UnityEngine;


//指令：这个类可以被显示或保存
[System.Serializable]

//命名规范：
//目前根据用途为三种物品添加不同的前缀(core指游戏本体物品，lf和fd是life和food的缩写)
//测试类（core_test_）、生活类（core_lf_）、饮食类（core_fd_）
public class ItemData//声明一个名为ItemData物品数据 的全局类。里面包含一些信息
{
    public string id = "";           // 唯一ID（apple），默认为空字符串
    public string name = "";         // 显示名字（苹果），默认为空字符串
    public string description = "";  // 简介（白雪的苹果），默认为空字符串
    public Sprite icon = null;         // 图标，默认为null
    public int maxStack = 1;    // 最大堆叠数量，默认为1
    public int calorie = 0;    // 卡路里，默认为0
    public int protein = 0;    // 蛋白质，默认为0
    public int vitamin = 0;    // 维生素，默认为0
    public int disgust = 0;    // 恶心感，默认为0
    public int healthImpact = 0;  // 健康修正，默认为0
}