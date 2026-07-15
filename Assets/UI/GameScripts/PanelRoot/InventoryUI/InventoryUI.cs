using UnityEngine;





public class InventoryUI : MonoBehaviour
{
    //InventorySystem inventorySystem;            //引用：容器系统
    private IItemSystem iItemSystem;            //接口:InventoryID的变量inventoryID，用于引用容器脚本
    public PlayerController playerController;   //引用：PlayerController玩家控制脚本
    public ItemDatabase itemDatabase;           //引用：物品数据库
    public DragSystem dragSystem;               //引用：拖动物品UI

    [Header("容器对象")]
    //public PlayerInventory inventory;//引用：玩家背包的容器
    public MonoBehaviour monoBehaviour;//引用：一个空的组件，通过手动拖引用绑定到容器脚本上
    //public InventorySlot[] slots;//声明一个格子数组，后面用于引用容器的格子数据//现在直接引用不需要声明了
    //public InventorySlot[] slots;//不再引用容器了，现在要手动拖引用//搞错了，这玩意根本不能拖
    public InventorySlotUI[] slotUIs;
    private string inventoryID;     //声明一个字符串 用于接收容器id
    //private int dragIndex;
    void Awake()//场景加载时执行
    {
        iItemSystem = monoBehaviour as IItemSystem;
        var slots = iItemSystem.GetInventorySlots();
        inventoryID = iItemSystem.GetInventoryID();
        //抓取所有子对象的InventorySlotUI组件并按顺序赋值给slotUIs数组（没激活的也抓取）
        //这是旧方法，需要主动抓取子对象，新方法是子对象主动注册//新方法不靠谱放弃了
        slotUIs = GetComponentsInChildren<InventorySlotUI>(true);
        for (int i = 0; i < slots.Length; i++)
        {
            slotUIs[i].index = i;//UI格子数组的索引
            slotUIs[i].inventoryUI = this;//UI格子数组的父容器 = 当前容器UI
        }
        //a版本：声明后直接引用：容器的格子数据
        // slots = inventory.slots;
        //目前在用的是b版本的方法：通过接口接受容器数组
        //c版本：将容器通过接口给的Slots数组赋值给slotUIs//这个用法也不要了
        // slotUIs = new InventorySlotUI[inventory.GetSlots().Length];
        //d版本：创建一个新数组并赋值给slotUIs，这个数组的长度是transform.childCount
        //transform.childCount用于查询当前物体下面有多少个子物体//应该换成与容器同步数组
        // slotUIs = new InventorySlotUI[transform.childCount];
    }

    /*/ 注册格子的函数（参数：容器格类的slotUI，int类的排序编号）//还让容器自己抓取
    public void RegisterSlot(InventorySlotUI slotUI, int index)
    {
        slotUIs[index] = slotUI;
        slotUI.inventoryUI = this;
        slotUI.index = index;
    }*/

    // 刷新容器的函数（尝试进行循环刷新）
    public void InventoryChanged(int index)
    {
        //var读取集合：重新获取容器数据，避免容器被替换后引用失效
        var slots = iItemSystem.GetInventorySlots();
        // Debug.Log("slots"+slots.Length);
        // Debug.Log("slotUIs"+slotUIs.Length);
        if (slots == null || slotUIs == null)
            return;
        if (index >= 0)//若索引大于等于0，则进行单格容器。反之进行全容量刷新循环
        {
            InventorySlotChanged(index);
            return;
        }
        //int count = Mathf.Min(slots.Length, slotUIs.Length);
        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlotChanged(i);
        }
        return;
    }
    // 刷新容器的函数（单格刷新）
    public void InventorySlotChanged(int index)
    {
        var slots = iItemSystem.GetInventorySlots();
        if (slots == null || index < 0 || index >= slots.Length)//格子不存在或索引有问题时返回
            return;
        // Debug.Log($"inventory={inventoryID}");
        // Debug.Log($"index={index}");
        // Debug.Log($"slots={slots.Length}");
        // Debug.Log($"slotUIs={slotUIs.Length}");
        
        //Debug.Log("格子为空，则将UI格子清空");
        // var读取集合：获取玩家背包索引的格子数据，并赋值给item物品变量
        var item = slots[index];
        if (string.IsNullOrEmpty(item.itemId))// 若 格子为空，则将UI格子清空
        {
            slotUIs[index].GetSlotIcon(null);
            slotUIs[index].GetSlotAmount(0);
            return;
        }
        // var读取集合：根据Slot临时格子的ItemId物品id从数据库拿物品信息
        //Debug.Log("数据查不到，则将UI格子清空");
        var itemData = itemDatabase.GetItemData(item.itemId);
        if (itemData == null)// 若 数据查不到，则将UI格子清空
        {
            slotUIs[index].GetSlotIcon(null);
            slotUIs[index].GetSlotAmount(0);
            return;
        }
        // 将格子UI设置为物品信息
        //Debug.Log("将格子UI设置为物品信息");
        slotUIs[index].GetSlotIcon(itemData.icon);
        slotUIs[index].GetSlotAmount(item.amount);
        return;
    }


    //鼠标左键点击(索引index用于指引哪个是被拖动的格子)
    public void LeftClick(int index)
    {
        //var读取集合：重新获取容器数据，避免容器被替换后引用失效
        var slots = iItemSystem.GetInventorySlots();
        //若当前 正在按shift键
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //若当前 正处于互动（双容器）模式 则 任何鼠标左键都切换为向另一个容器移动物品
            if (playerController.State == "Interact")
            {
                //MoveItem(index);//Move移动物品函数
                return;
            }
            else//反之 任何鼠标左键都切换为丢弃物品
            {
                //DropItem(index);//Drop丢弃物品函数
                return;
            }
        }
        //若当前 正处于拖动模式
        if (dragSystem.dragState)
        {
            if (slots[index].amount == 0)//若 当前格子是空的，则 尝试放下物品
            {
                PutItem(index);//Put放置物品函数
                return;
            }
            else//反之 尝试交换物品
            {
                // 最大堆叠数量 = 物品库.实例.获取物品数据(容器内物品id).最大堆叠
                int maxStack = itemDatabase.GetItemData(slots[index].itemId).maxStack;
                //若 当前容器当中的物品id等于被拖动的物品id 且 不到最大堆叠数量，则 尝试堆叠
                if (slots[index].itemId == dragSystem.dragItemID && slots[index].amount < maxStack)
                {
                    StackItem(index, maxStack);//Stack堆叠物品函数（若放不下则留在鼠标上）
                    return;
                }
                else//反之 尝试交换
                {
                    SwapItem(index);//Swap交换物品函数
                    return;
                }
            }
        }
        else//若当前没有拖动物品，则开始拖动
        {
            DragItem(index);//Drag拖动物品函数
            return;
        }
    }
    //鼠标右键点击(索引index用于指引哪个是被拖动的格子)
    public void RightClick(int index)
    {
        //UseItem(index); //Use使用物品函数
        return;
    }




    

    //放置物品的函数
    public void PutItem(int index)
    {
        //var读取集合：重新获取容器数据，避免容器被替换后引用失效
        var slots = iItemSystem.GetInventorySlots();
        if (slots == null || index < 0 || index >= slots.Length)//格子不存在或索引有问题时返回
            return;
        if (string.IsNullOrEmpty(slots[index].itemId))//若格子为空，则放置并删除原格子的物品
        {
            InventorySystem.AddSlotItem(slots, index, dragSystem.dragItemID, dragSystem.dragItemAmount);
            InventorySystem.ClearItem(dragSystem.dragInventory, dragSystem.dragInventoryIndex);
            EventBus.RaiseInventoryChanged(inventoryID, index);//传入id实现单容器刷新，传入索引实现单格刷新
            EventBus.RaiseInventoryChanged(dragSystem.dragInventoryID, dragSystem.dragInventoryIndex);
            dragSystem.ClearDragData();
            return;
        }
    }
    
    //堆叠物品的函数
    public void StackItem(int index, int maxStack)
    {
        
    }
    //交换物品的函数
    public void SwapItem(int index)
    {
        //var读取集合：重新获取容器数据，避免容器被替换后引用失效
        var slots = iItemSystem.GetInventorySlots();
        if (slots == null || index < 0 || index >= slots.Length)//格子不存在或索引有问题时返回
            return;
        //若 当前容器与被拖动的容器相同 且 当前索引与被拖动的容器索引相同，则不交换（放回原位）
        if (dragSystem.dragInventory == slots && dragSystem.dragInventoryIndex == index)
        {
            //slotUIs[index].GetSlotIcon(dragSystem.dragItemSprite);//放回原位
            //slotUIs[index].GetSlotAmount(dragSystem.dragItemAmount);
            EventBus.RaiseInventoryChanged(inventoryID, index);//传入id实现单容器刷新，传入索引实现单格刷新
            dragSystem.ClearDragData();//清除拖动者的函数
            return;
        }
        string tempId = slots[index].itemId;
        int tempAmount = slots[index].amount;
        //将当前点击的数组索引数据 替换为 被拖动的数据，并刷新现在的格子
        slots[index].itemId = dragSystem.dragItemID;
        slots[index].amount = dragSystem.dragItemAmount;
        EventBus.RaiseInventoryChanged(inventoryID, index);
        //将被拖动的数组索引数据 替换为 当前点击的数组索引数据，并刷新原本的格子
        dragSystem.dragInventory[dragSystem.dragInventoryIndex].itemId = tempId;
        dragSystem.dragInventory[dragSystem.dragInventoryIndex].amount = tempAmount;
        EventBus.RaiseInventoryChanged(dragSystem.dragInventoryID, dragSystem.dragInventoryIndex);
        //dragSystem.SetDrag(null, 0);//将被拖动的物品数据删除
        dragSystem.ClearDragData();
        return;
    }

    //拖动物品的函数（索引index）
    public void DragItem(int index)
    {
        //var读取集合：重新获取容器数据，避免容器被替换后引用失效
        var slots = iItemSystem.GetInventorySlots();
        if (slots == null || index < 0 || index >= slots.Length)//格子不存在或索引有问题时返回
            return;
        // var读取集合：获取玩家背包索引的格子数据，并赋值给item物品变量
        var item = slots[index];
        if (string.IsNullOrEmpty(item.itemId))//若格子为空，则不拖动
            return;
        // var读取集合：根据Slot临时格子的ItemId物品id从数据库拿物品信息
        var itemData = itemDatabase.GetItemData(item.itemId);
        if (itemData == null)//若物品数据不存在，则不拖动
            return;
        //将被拖动的 容器格数据、物品数据 赋值给dragSystem拖动系统，临时存储
        dragSystem.SetDragData
        (
            true,           //参数：当前是否处于拖动状态
            inventoryID,    //参数：被拖动的容器ID
            slots,          //参数：被拖动的容器数组
            index,          //参数：被拖动的容器索引
            item.itemId,    //参数：拖动的物品id
            itemData.icon,  //参数：拖动的物品精灵
            item.amount     //参数：拖动的物品数量
        );
        slotUIs[index].GetSlotIcon(null);
        slotUIs[index].GetSlotAmount(0);//临时将被拖动的格子UI隐藏，但不是真的删数据
        return;
    }
    /*/全局全局拖拽状态
    public static class DragContext
    {
        public static InventorySlot data;
        public static bool isDragging;
    }*/
}
