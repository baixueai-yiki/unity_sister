using UnityEngine;

public interface IItemSystem//声明一个接口IItemSystem
{
    //接口是一个约束，要求实现这个接口的类必须实现GetSlots方法
    //任何InventorySlot[]类都可以使用，和int转字符串是同一种东西
    string GetInventoryID();
    InventorySlot[] GetInventorySlots();
}