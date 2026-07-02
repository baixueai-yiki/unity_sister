using UnityEngine;


//指令：这个类可以被显示或保存
[System.Serializable]
public class ItemData//声明一个名为ItemData物品数据 的全局类。里面包含一些信息
{
    public string Id;           // 唯一ID（apple）
    public string Name;         // 显示名字（苹果）
    public string Description;  // 简介（白雪的苹果）
    public Sprite Icon;         // 图标（Art/Textures/Items/apple.png）
    public int MaxStack = 1;    // 最大堆叠
}