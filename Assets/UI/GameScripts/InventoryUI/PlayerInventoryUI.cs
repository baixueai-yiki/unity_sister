using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    //[Header("数据源")]
    public PlayerInventory playerInventory;
    public ItemDatabase itemDatabase;

    //[Header("UI格子")]
    public InventorySlotUI[] UISlot;



    void Awake()//自动抓所有子对象 SlotUI
    {
        //按顺序分别把所有子对象的SlotUI组件赋值给UISlot数组
        //如SlotUI0、SlotUI1、SlotUI2赋值给UISlot[0]、UISlot[1]、UISlot[2]
        UISlot = GetComponentsInChildren<InventorySlotUI>();

        for (int i = 0; i < UISlot.Length; i++)
        {
            UISlot[i].index = i;
            //UISlot[i].parentUI = this;//UI格子数组的父容器 = 当前容器UI
        }
    }

    void OnEnable()// 组件被启用时调用的函数，订阅事件
    {
        EventBus.OnInventoryChanged += InventoryChanged;
    }
    void OnDisable()// 组件被关闭时调用的函数，取消订阅
    {
        EventBus.OnInventoryChanged -= InventoryChanged;
    }

    // 刷新容器的函数（全体刷新）
    private void InventoryChanged()
    {
        // 计算UI格子数量和玩家背包格子数量的最小值，避免数组越界
        int count = Mathf.Min(UISlot.Length, playerInventory.slots.Length);
        for (int i = 0; i < count; i++)
        {
            InventoryChangedSlot(i);
        }
    }

    // 刷新容器的函数（单格刷新）
    private void InventoryChangedSlot(int index)
    {
        if (index < 0 || index >= playerInventory.slots.Length)
            return;

        // 获取玩家背包索引的格子数据，并赋值给Slot临时格子
        InventorySlot Slot = playerInventory.slots[index];
        

        // 若 格子为空，则将UI格子清空
        if (string.IsNullOrEmpty(Slot.ItemId))
        {
            UISlot[index].GetSlotIcon(null);
            UISlot[index].GetSlotAmount(0);
            return;
        }

        // var读取集合：根据Slot临时格子的ItemId物品id从数据库拿物品信息
        var ItemData = itemDatabase.GetItemData(Slot.ItemId);

        // 将UI格子设置为物品信息
        UISlot[index].GetSlotIcon(ItemData.Icon);
        UISlot[index].GetSlotAmount(Slot.Amount);
    }
}
