using UnityEngine;

public class PlayerInteract : MonoBehaviour//使用物品
{
    public ItemDatabase itemDatabase;       //引用：物品数据库
    public PlayerController playerController;//引用：PlayerController玩家控制脚本
    public PlayerInventory playerInventory; //引用：PlayerInventory玩家背包脚本


    
    public string interactState;            //声明GameObject变量interactState互动状态
    private GameObject interactTarget;      //声明GameObject变量inspectTarget检视目标
    //private string interactName;            //声明GameObject变量interactName互动目标的名字
    //public string InteractID;              //声明string变量InteractID互动目标的互动ID
    public ItemData interactItem;           //声明ItemData变量interactItem(玩家手里的)互动物品

    private void Awake()//场景加载时调用的函数
    { 
        playerController = GetComponent<PlayerController>();// 获取自身对象的 PlayerController 组件并赋值给 controller
        playerInventory = GetComponent<PlayerInventory>();// 获取自身对象的 PlayerInventory 组件并赋值给 playerInventory
    }

    //函数；当进入触发器时调用（Unity的碰撞体类型 变量名）
    void OnTriggerEnter2D(Collider2D other)
    {   //若 碰到的物体.查询标签（interact）
        if (other.CompareTag("interact"))
        {
            //当碰到的物体有interact互动标签时，则获取数据
            interactTarget = other.gameObject;//赋值互动对象
            //interactName =  other.gameObject.name;//赋值互动名字
        }
    }
    //函数；当离开触发器时调用（Unity的碰撞体类型 变量名）
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == interactTarget)
        {
            interactTarget = null;
            //interactName = null;
        }
    }

    public void StartInteract()
    {
        if (interactTarget == null)
            return;
        //获取互动对象的互动接口
        IInteractActor iInteractActor = interactTarget.GetComponent<IInteractActor>();
        // 若没有获取到互动对象的互动接口，则直接返回
        if(iInteractActor == null)
            return;
        //获取玩家手里的互动物品数据
        interactItem = itemDatabase.GetItemData(playerInventory.slots[0].itemId);
        //若 互动对象的互动ID 不等于 玩家容器格的物品的互动ID，则 直接返回
        if(iInteractActor.GetInteractID() == null && iInteractActor.GetInteractID() != interactItem.interactActor)
            return;
        //调用物品系统的替换物品（合成系统）的函数
        // (inventoryID容器ID，slots数组，index索引，resultItemId替换物品id)
        InventorySystem.ReplaceItem
        (
        playerInventory.inventoryID,
        playerInventory.slots,
        0,
        interactItem.interactResult
        );
        // 若当前不是Interact互动状态，则开启互动，反之关闭互动
        if (playerController.State != "Interact")
        {
            //Debug.Log(iInteractActor.GetType().Name);
            //interactTarget.SendMessage("EndInteract" + interactName);//结束互动的函数//改用接口了
            iInteractActor.Interact();
            EventBus.RaiseInteract();//调用事件总线函数，通知全局状态机切换Interact状态
            return;
        }
        //interactTarget.SendMessage("Interact" + interactName);//调用被互动的物品的 Interact前缀的函数//改用接口了
        iInteractActor.EndInteract();
        EventBus.RaisePlay();//调用事件总线函数，通知全局状态机切换Play状态
        return;
    }
}
