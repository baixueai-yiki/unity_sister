using UnityEngine;



//声明一个名为TableInventory的类，继承自MonoBehaviour类，实现IInventorySource接口
public class TableInventory : MonoBehaviour, IItemSystem
{

    public ItemDatabase itemDatabase;   //引用：物品数据库
    public InventoryUI inventoryUI;//引用：容器UI
    public string InventoryID = "TableInventory";//桌子容器的唯一ID
    public GameObject Panel_PlayerInventoryUI;         //引用：玩家容器UI面板
    public GameObject Panel_TableInventoryUI;          //引用：桌子容器UI面板
// 声明InventorySlot类的数组slots 赋值为 创建一个长度为1的 InventorySlot容器格类 的数组
    public InventorySlot[] slots = new InventorySlot[4];//声明一个容器数组，赋值为一个新的长度为1的容器格类的数组
    
    //通过接口把InventoryID容器id传给InventoryUI
    public string GetInventoryID()
    {
        return InventoryID;
    }
    //通过接口把slots数组传给InventoryUI
    public InventorySlot[] GetInventorySlots()
    {
        return slots;
    }


    void Start()
    {
        Panel_TableInventoryUI.SetActive(false);    // 游戏开始时，默认隐藏Panel_TableInventoryUI面板
    }
    void Awake()//给每个格子创建真实对象
    {
        //inventoryUI = GetComponent<InventoryUI>();  // 获取自身对象的容器UI组件并赋值//现在容器挪到对象自己身上了
        slots[0] = new InventorySlot();
        slots[1] = new InventorySlot();
        slots[2] = new InventorySlot();
        slots[3] = new InventorySlot();
    }
    void OnEnable()// 组件被启用时调用的函数，订阅事件
    {
        EventBus.OnTableInventoryChanged += TableInventoryChanged;
    }
    void OnDisable()// 组件被关闭时调用的函数，取消订阅
    {
        EventBus.OnTableInventoryChanged -= TableInventoryChanged;
    }




    public void Interact()// 玩家与桌子互动时调用的函数
    {
        
        Panel_PlayerInventoryUI.SetActive(true);    // 互动展开Panel_PlayerInventoryUI面板
        Panel_TableInventoryUI.SetActive(true);     // 互动展开Panel_TableInventoryUI面板
        //传入id实现单容器刷新，传入索引实现单格刷新，索引写成-1时刷新整个容器
        EventBus.RaiseInventoryChanged("PlayerInventory", -1);
        EventBus.RaiseInventoryChanged("TableInventory", -1);
    }
    public void EndInteract()// 结束互动时调用的函数
    {
        
        Panel_PlayerInventoryUI.SetActive(false);    // 互动展开Panel_PlayerInventoryUI面板
        Panel_TableInventoryUI.SetActive(false);     // 互动展开Panel_TableInventoryUI面板
    }

    // 添加物品的函数(slots数组本体，itemDatabase物品数据库，itemId物品id，amount物品数量)
    // 由于静态的InventorySystem容器系统不支持实例化，所以只能把itemDatabase当成参数传过去
    private bool AddItem(string InventoryID, InventorySlot[] slots, ItemDatabase itemDatabase, int index, string addItemId, int addAmount)
    {
        // 返回值是bool，直接返回InventorySystem.AddItem的返回值
        bool result = InventorySystem.AddItem(InventoryID, slots, itemDatabase, index, addItemId, addAmount);
        //EventBus.RaiseInventoryChanged();//改用容器UI调用,可是这样就不能传索引实现单格刷新了
        return result;
    }

    // 减少物品的函数(slots数组本体，index数组索引(需要修改的数组位置)，amount物品数量)
    private void TableRemoveItem(string InventoryID, InventorySlot[] slots, string itemId, int removeAmount)
    {
        InventorySystem.RemoveItem(InventoryID, slots, itemId, removeAmount);
        //EventBus.RaiseInventoryChanged();//改用容器UI调用,可是这样就不能传索引实现单格刷新了
    }

    // 刷新桌子容器
    private void TableInventoryChanged(int index)
    {
        inventoryUI.InventoryChanged(index);
    }
}
