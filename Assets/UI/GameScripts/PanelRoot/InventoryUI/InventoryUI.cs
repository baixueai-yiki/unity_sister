using UnityEngine;



public interface IInventorySource//声明一个接口InventorySource
{
    //接口是一个约束，要求实现这个接口的类必须实现GetSlots方法
    InventorySlot[] GetSlots();
}
public class InventoryUI : MonoBehaviour
{
    [Header("在这里拖容器对象的引用")]
    //public PlayerInventory inventory;//引用：玩家背包的容器
    public MonoBehaviour source;//引用：一个空的组件，通过手动拖引用绑定到容器脚本上//不行，这玩意没用
    
    public InventorySlot[] slots;//声明一个格子数组，后面用于引用容器的格子数据
    //public InventorySlot[] slots;//不再引用容器了，现在要手动拖引用//搞错了，这玩意根本不能拖
    public ItemDatabase itemDatabase;
    public InventorySlotUI[] slotUIs;

    void Awake()//自动抓所有子对象 SlotUI
    {
        var inventory = source as IInventorySource;
        if (inventory == null)
            return;
        //slots = inventory.slots;//引用：容器的格子数据
        slots = inventory.GetSlots();
        //按顺序分别把所有子对象的SlotUI组件赋值给UISlot数组
        //如SlotUI0、SlotUI1、SlotUI2赋值给UISlot[0]、UISlot[1]、UISlot[2]
        slotUIs = GetComponentsInChildren<InventorySlotUI>();

        for (int i = 0; i < slotUIs.Length; i++)
        {
            slotUIs[i].index = i;
            //slotUIs[i].parentUI = this;//UI格子数组的父容器 = 当前容器UI
        }
    }

    /*换成容器本身监听事件，这样就能准确只让对应的容器类型刷新了
    void OnEnable()// 组件被启用时调用的函数，订阅事件
    {
        EventBus.OnPlayerInventoryChanged += InventoryChanged;
    }
    void OnDisable()// 组件被关闭时调用的函数，取消订阅
    {
        EventBus.OnPlayerInventoryChanged -= InventoryChanged;
    }*/

    /*/ 刷新容器的函数（全体刷新）
    private void InventoryChanged(int index)
    {
        // 计算UI格子数量和玩家背包格子数量的最小值，避免数组越界
        int count = Mathf.Min(UISlot.Length, playerInventory.slots.Length);
        for (int i = 0; i < count; i++)
        {
            InventoryChangedSlot(i);
        }
    }*/

    // 刷新容器的函数（单格刷新）
    public void InventoryChanged(int index)
    {
        if (slots == null)
            return;
        if (index < 0 || index >= slots.Length)
            return;
        if (index >= slotUIs.Length)
            return;
        // 获取玩家背包索引的格子数据，并赋值给slot临时格子
        InventorySlot slot = slots[index];
        

        // 若 格子为空，则将UI格子清空
        if (string.IsNullOrEmpty(slot.itemId))
        {
            slotUIs[index].GetSlotIcon(null);
            slotUIs[index].GetSlotAmount(0);
            return;
        }

        // var读取集合：根据Slot临时格子的ItemId物品id从数据库拿物品信息
        var itemData = itemDatabase.GetItemData(slot.itemId);

        // 将格子UI设置为物品信息
        slotUIs[index].GetSlotIcon(itemData.icon);
        slotUIs[index].GetSlotAmount(slot.amount);
    }
}
