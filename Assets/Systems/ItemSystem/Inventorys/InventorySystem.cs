using UnityEngine;

//全局 类，命名为InventorySystem容器系统，用于操作容器数据
public static class InventorySystem
{

    //public ItemDatabase itemDatabase;//引用：物品数据库//静态不支持实例化，现在改成把参数传进来用


    
    // 所有格子数据
    //public InventorySlot[] slots;

    /*/ 构造函数：创建不同容量的容器 我现在不知道这个是干啥的
    public InventorySystem(int size)
    {
        slots = new InventorySlot[size];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new InventorySlot();
        }
    }*/

    // 添加物品的函数(slots数组本体，itemDatabase物品数据库，itemId物品id，amount添加数量)
    public static bool AddItem(string inventoryID, InventorySlot[] slots, ItemDatabase itemDatabase, string addItemId, int addAmount)
    {
        // 最大堆叠数量 = 物品库.实例.获取物品数据(添加物品id).最大堆叠
        int maxStack = itemDatabase.GetItemData(addItemId).maxStack;
        // 寻找可堆叠格子
        for (int i = 0; i < slots.Length; i++)
        {
            //若 物品id相同，尝试堆叠物品
            if (slots[i].itemId == addItemId)
            {
                // 差值 = 最大数量 - 当前数量
                int canAdd = maxStack - slots[i].amount;
                if (canAdd > 0)
                {
                    // 使用Mathf.Min选择更小的值并赋值给add
                    // 如果差值更小就说明空位不够，进入下个循环选空位
                    // 如果加值更小就说明空位足够，就此结束
                    // AI这么聪明的思路我一辈子想不出来
                    int add = Mathf.Min(canAdd, addAmount);

                    slots[i].amount += add;//当前物品数量增加
                    EventBus.RaiseInventoryChanged(inventoryID, i);//传入id实现单容器刷新，传入索引实现单格刷新
                    addAmount -= add;//需要添加的物品数量减少
                    Debug.Log("apple");
                    if (addAmount <= 0)
                        return true;
                }
            }
        }
        // 寻找往空格子
        for (int i = 0; i < slots.Length; i++)
        {
            // string.IsNullOrEmpty是系统为string类提供的静态方法，同时判断null和空字符串
            // 若 slots数组i的物品id是空的，则 将空格子的id和数量设置为物品id和数量，往空格子放物品
            if (string.IsNullOrEmpty(slots[i].itemId))
            {
                int add = Mathf.Min(maxStack, addAmount);

                slots[i].itemId = addItemId;
                slots[i].amount = add;
                addAmount -= add;
                EventBus.RaiseInventoryChanged(inventoryID, i);//传入id实现单容器刷新，传入索引实现单格刷新

                if (addAmount <= 0)
                    return true;
            }
        }
        //EventBus.RaiseInventoryChanged();
        return false;// 没有空间无法添加则返回false
    }

    // 减少物品的函数(slots数组本体，index数组索引(需要修改的数组位置)，removeAmount物品数量)
    // 这个是玩家操作层的减少物品，通过与UI互动拿走物品
    public static bool PlayerRemoveItem(string inventoryID, InventorySlot[] slots, int index, int removeAmount)
    {
        //若 索引错误则返回
        if (index < 0 || index >= slots.Length)
            return false;
        //若 格子是空的则返回
        if (string.IsNullOrEmpty(slots[index].itemId))
            return false;
        //若 数量小于减少的物品则返回
        if (slots[index].amount < removeAmount)
            return false;

        // 使当前物品数量减少
        slots[index].amount -= removeAmount;
        //若 当前物品数量小于等于0则设置为空格子
        if (slots[index].amount <= 0)
        {
            slots[index].itemId = null;
            slots[index].amount = 0;
        }
        EventBus.RaiseInventoryChanged(inventoryID, index);//传入id实现单容器刷新，传入索引实现单格刷新
        return true;
    }

    // 这个是系统操作层的减少物品，用于合成、任务、商场等直接规模性减少的情况
    public static bool RemoveItem(string inventoryID, InventorySlot[] slots, string itemId, int removeAmount)
    {
        // 先检查总量够不够
        int total = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemId == itemId)
            {
                total += slots[i].amount;
            }
        }

        if (total < removeAmount)
            return false;

        // 2. 开始跨格扣除
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemId == itemId)
            {
                int take = Mathf.Min(slots[i].amount, removeAmount);

                slots[i].amount -= take;
                removeAmount -= take;

                // 清空格子
                if (slots[i].amount <= 0)
                {
                    slots[i].itemId = null;
                    slots[i].amount = 0;
                }

                if (removeAmount <= 0)
                    return true;
            }
            EventBus.RaiseInventoryChanged(inventoryID, i);//传入id实现单容器刷新，传入索引实现单格刷新
        }
        return true;
    }
}
