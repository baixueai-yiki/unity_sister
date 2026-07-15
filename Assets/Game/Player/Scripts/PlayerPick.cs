using UnityEngine;




public interface IPickItemData//声明一个接口IPickItemData
{
    (string itemId, int amount) PickItem();
}
public class PlayerPick : MonoBehaviour
{
    
    public PlayerInventory playerInventory;        //引用：PlayerInventory玩家背包脚本
    //public ItemData itemData;                   //引用：物品数据类（静态的）
    public ItemDatabase itemDatabase;           //引用：物品数据库（静态的）


    private GameObject pickTarget;            //声明GameObject变量pickTarget拾取对象
    private string pickName;            //声明GameObject变量pickName拾取名字
    private string pickItemId;          //声明一个字符串，拾取物品id
    private int pickAmount;             //声明一个int，拾取数量


    private void Awake()//场景加载时调用的函数
    { 

    }

    //函数；当进入触发器时调用（Unity的碰撞体类型 变量名）
    void OnTriggerEnter2D(Collider2D other)
    {   //若 碰到的物体.查询标签（interact）
        if (other.CompareTag("pick"))
        {
            //当碰到的物体有pick捡起标签时，则获取数据
            pickTarget = other.gameObject;//赋值互动对象
            pickName =  other.gameObject.name;//赋值互动名字
        }
    }
    //函数；当离开触发器时调用（Unity的碰撞体类型 变量名）
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == pickTarget)
        {
            pickTarget = null;
            pickName = null;
        }
    }

    public void PickItem()
    {
        if (pickTarget == null)//若没有碰撞目标则直接返回
            return;
        //pickTarget.GetComponent<IPickItemData>(out var pickActor);
        //在pickTarget上找实现了IPickItemData接口的组件，赋值给
        //TryGetComponent是一种若没有找到则直接返回的GetComponent，不用这个没办法进行判断
        if (pickTarget.TryGetComponent<IPickItemData>(out var pickActor))
        {
            //调用被触碰物品的拾取函数（返回一个数组）
            var pickItem = pickActor.PickItem();
            //pickItemId = pickItem.itemId;
            //pickAmount = pickItem.amount;//这个数组里包含了物品id和数量//现在不赋值直接写了
            //调用容器系统添加物品函数（参数：玩家容器ID、玩家容器数组、物品数据库、索引、物品id、物品数量）
            bool success = InventorySystem.AddItem(
                "PlayerInventory", 
                playerInventory.slots, 
                itemDatabase, 
                -1, 
                pickItem.itemId, 
                pickItem.amount
            );
            //Debug.Log("success = " + success);
            if (success)//若成功添加则销毁碰撞目标
            {
                Destroy(pickTarget);
                return;
            }
        }
    }
}
