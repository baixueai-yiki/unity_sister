using UnityEngine;

public class PlayerInteract : MonoBehaviour//使用物品
{
    public PlayerController playerController;        //引用：PlayerController玩家控制脚本
    private GameObject interactTarget;          //声明GameObject变量inspectTarget检视目标
    private string interactName;            //声明GameObject变量inspectName检视目标的名字



    public PlayerInventory playerInventory;        //引用：PlayerInventory玩家背包脚本
    public ItemDatabase itemDatabase;   //引用：物品数据库

    private void Awake()//场景加载时调用的函数
    { 
        playerController = GetComponent<PlayerController>();  // 获取自身对象的 PlayerController 组件并赋值给 controller
        //playerInventory = GetComponent<PlayerInventory>();  // 获取自身对象的 PlayerInventory 组件并赋值给 playerInventory
        //itemDatabase = GetComponent<ItemDatabase>();  // 获取自身对象的 ItemDatabase 组件并赋值给 itemDatabase
        //dialogueUI = GameObject.Find("GamePlayUI").GetComponent<DialogueUI>();     //获取指定对象的DialogueUI组件并赋值给dialogueUI
    }

    //函数；当进入触发器时调用（Unity的碰撞体类型 变量名）
    void OnTriggerEnter2D(Collider2D other)
    {   //若 碰到的物体.查询标签（interact）
        if (other.CompareTag("interact"))//当碰到的物体有interact互动 标签时
        {
            interactTarget = other.gameObject;//赋值互动对象
            interactName =  other.gameObject.name;//赋值互动名字
        }
    }
    //函数；当离开触发器时调用（Unity的碰撞体类型 变量名）
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == interactTarget)
        {
            interactTarget = null;
            interactName = null;
        }
    }

    public void StartInteract()
    {
        if (interactTarget != null)
        {
            
            //EventBus.RaiseInteractEvent(interactName, interactTarget);//广播互动事件
            
            //playerInventory = GetComponent<PlayerInventory>();  // 获取自身对象的 PlayerInventory 组件

            InventorySystem.AddItem(playerInventory.slots, itemDatabase, "apple", 1);
        }



    }
}
