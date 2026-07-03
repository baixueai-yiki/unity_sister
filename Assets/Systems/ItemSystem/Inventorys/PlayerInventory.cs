using UnityEngine;



//声明一个名为PlayerInventory的类，继承自MonoBehaviour类，实现IInventorySource接口
public class PlayerInventory : MonoBehaviour, IInventorySource
{

    public ItemDatabase itemDatabase;   //引用：物品数据库
    public InventoryUI inventoryUI;//引用：容器UI
    public string InventoryID = "PlayerInventory";//玩家背包的唯一ID


// 声明InventorySlot类的数组slots 赋值为 创建一个长度为1的 InventorySlot容器格类 的数组
    public InventorySlot[] slots = new InventorySlot[1];//声明一个容器数组，赋值为一个新的长度为1的容器格类的数组
    public InventorySlot[] GetSlots()                   //通过接口把这个数组传给InventoryUI
    {
        return slots;
    }

    void Awake()//给每个格子创建真实对象
    {
        inventoryUI = GetComponent<InventoryUI>();  // 获取自身对象的PlayerInventoryUI组件并赋值给inventoryUI
        //将数组的第 i 个位置 赋值为 创建一个新的InventorySlot容器格类
        slots[0] = new InventorySlot();   //旧代码不要了 //现在要了
    
        //声明 变量playerInventory玩家容器 赋值为 创建一个新的背包系统类（有1个格子）
        //playerInventory = new InventorySystem(1);//新写法不要了
    }
        void OnEnable()// 组件被启用时调用的函数，订阅事件
    {
        EventBus.OnPlayerInventoryChanged += PlayerInventoryChanged;
    }
    void OnDisable()// 组件被关闭时调用的函数，取消订阅
    {
        EventBus.OnPlayerInventoryChanged -= PlayerInventoryChanged;
    }

    // 添加物品的函数(slots数组本体，itemDatabase物品数据库，itemId物品id，amount物品数量)
    // 由于静态的InventorySystem容器系统不支持实例化，所以只能把itemDatabase当成参数传过去
    private bool AddItem(string InventoryID, InventorySlot[] slots, ItemDatabase itemDatabase, string addItemId, int addAmount)
    {
        // 返回值是bool，直接返回InventorySystem.AddItem的返回值
        bool result = InventorySystem.AddItem(InventoryID, slots, itemDatabase,addItemId, addAmount);
        //EventBus.RaiseInventoryChanged();//改用容器UI调用,可是这样就不能传索引实现单格刷新了
        return result;
    }

    // 减少物品的函数(slots数组本体，index数组索引(需要修改的数组位置)，amount物品数量)
    private void PlayerRemoveItem(string InventoryID, InventorySlot[] slots, int index, int removeAmount)
    {
        InventorySystem.PlayerRemoveItem(InventoryID, slots, index, removeAmount);
        //EventBus.RaiseInventoryChanged();//改用容器UI调用,可是这样就不能传索引实现单格刷新了
    }

    private void PlayerInventoryChanged(int index)
    {
        inventoryUI.InventoryChanged(index);
    }
}