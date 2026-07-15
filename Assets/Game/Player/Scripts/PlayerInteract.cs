using UnityEngine;

public class PlayerInteract : MonoBehaviour//使用物品
{
    public PlayerController playerController;        //引用：PlayerController玩家控制脚本
    


    public string interactState;            //声明GameObject变量interactState互动状态
    private GameObject interactTarget;          //声明GameObject变量inspectTarget检视目标
    private string interactName;            //声明GameObject变量interactName互动目标的名字

    private void Awake()//场景加载时调用的函数
    { 
        playerController = GetComponent<PlayerController>();  // 获取自身对象的 PlayerController 组件并赋值给 controller
    }

    //函数；当进入触发器时调用（Unity的碰撞体类型 变量名）
    void OnTriggerEnter2D(Collider2D other)
    {   //若 碰到的物体.查询标签（interact）
        if (other.CompareTag("interact"))
        {
            //当碰到的物体有interact互动标签时，则获取数据
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
        if (interactTarget == null)
            return;
        // 若当前是Interact互动状态，则关闭互动，反之进行互动
        if (playerController.State == "Interact")
        {
            interactTarget.SendMessage("EndInteract" + interactName);//结束互动的函数
            EventBus.RaisePlay();//调用事件总线函数，通知全局状态机切换Play状态
            return;
        }
        interactTarget.SendMessage("Interact" + interactName);//调用被互动的物品的 Interact前缀的函数
        EventBus.RaiseInteract();//调用事件总线函数，通知全局状态机切换Interact状态
        return;
    }
}
