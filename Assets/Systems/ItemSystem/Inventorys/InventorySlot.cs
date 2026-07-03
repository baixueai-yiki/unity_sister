using UnityEngine;


//指令：这个类可以被显示或保存
[System.Serializable]
public class InventorySlot  //声明一个名为InventorySlot容器格 的全局类。用于储存从物品类读取到的数据
{
    public string itemId;   //物品ID
    public int amount;      //物品数量
}