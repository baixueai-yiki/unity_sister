using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public ItemDatabase itemDatabase;   //引用：物品数据库



// 声明InventorySlot类的数组slots 赋值为 创建一个长度为1的 InventorySlot容器格类 的数组
    public InventorySlot[] slots = new InventorySlot[1];//有一个背包格子

    void Awake()//给每个格子创建真实对象
    {
        //将数组的第 i 个位置 赋值为 创建一个新的InventorySlot容器格类
        slots[0] = new InventorySlot();   //旧代码不要了 //现在要了
    
        //声明 变量playerInventory玩家容器 赋值为 创建一个新的背包系统类（有1个格子）
        //playerInventory = new InventorySystem(1);//新写法不要了
    }


    // 添加物品的函数(slots数组本体，itemDatabase物品数据库，itemId物品id，amount物品数量)
    // 由于静态的InventorySystem容器系统不支持实例化，所以只能把itemDatabase当成参数传过去
    private bool AddItem(InventorySlot[] slots, ItemDatabase itemDatabase, string addItemId, int addAmount)
    {
        // 返回值是bool，直接返回InventorySystem.AddItem的返回值
        return InventorySystem.AddItem(slots, itemDatabase,addItemId, addAmount);
    }

    // 减少物品的函数(slots数组本体，index数组索引(需要修改的数组位置)，amount物品数量)
    private void PlayerRemoveItem(InventorySlot[] slots, int index, int removeAmount)
    {
        InventorySystem.PlayerRemoveItem(slots, index, removeAmount);
    }
}