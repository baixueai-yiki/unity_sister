using UnityEngine;                  //导入Unity常用库

public class PlayerInspect : MonoBehaviour//检视物品
{
    public PlayerController playerController;        //引用：PlayerController玩家控制脚本
    public CharacterAttributes characterAttributes;  //引用：CharacterAttributes玩家属性
    public DialogueUI dialogueUI;        //引用：DialogueUI对话UI
    public float restDurationHours = 1f;       // 每次休息消耗的时间（小时）
    public float staminaRecovered = 50f;       // 每次休息回复的体力

    private GameObject inspectTarget;          //声明GameObject变量inspectTarget检视目标
    private string inspectName;            //声明GameObject变量inspectName检视目标的名字
    private void Awake()//场景加载时调用的函数
    {
        playerController = GetComponent<PlayerController>();  // 获取自身对象的 PlayerController 组件并赋值给 controller
        characterAttributes = GetComponent<CharacterAttributes>();// 获取自身对象的 PlayerStats 组件并赋值给 stats
        dialogueUI = GameObject.Find("GamePlayUI").GetComponent<DialogueUI>();     //获取指定对象的DialogueUI组件并赋值给dialogueUI

        //rest = GetComponent<PlayerRest>();                    // 获取自身对象的 PlayerRest 组件并赋值给 rest
        //PSM = GetComponent<PlayerInputState>();               // 获取自身对象的 PlayerInputState 组件并赋值给 PSM
        //rb2d = GetComponent<Rigidbody2D>();                   // 获取自身对象的 Rigidbody2D 组件并赋值给 rb2d
    }

    //函数；当进入触发器时调用（Unity的碰撞体类型 变量名）
    void OnTriggerEnter2D(Collider2D other)
    {   //若 碰到的物体.查询标签（interact）
        if (other.CompareTag("inspect"))//当碰到的物体有inspect检视 标签时
        {
            inspectTarget = other.gameObject;
            inspectName =  other.gameObject.name;
        }
    }
    //函数；当离开触发器时调用（Unity的碰撞体类型 变量名）
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == inspectTarget)
        {
            inspectTarget = null;
            inspectName = null;
        }
    }

    public void StartInspect()
    {
        if (inspectTarget == null)
            return;
    //获取变量otherInspect赋值为other（碰到的物体）的Inspect组件
            //otherInspect = other.GetComponent<Inspect>();
        var toast = ToastData.GetToast(inspectName);
        //otherInspect.PlayerInspect();
        if (dialogueUI != null)
            dialogueUI.Show(toast);
    }
}