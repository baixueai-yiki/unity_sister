using UnityEngine;                       // Unity基础库

public class StoryManager : MonoBehaviour
{
    //public EventBus eventBus;            //引用：EventBus事件总线，用来广播或监听事件
    public GameStateMachine GSM;         //引用：输入控制器（统一控制玩家是否能操作）
    public DialogueUI dialogueUI;        //引用：DialogueUI对话UI


    void Awake()
    {
        //eventBus = GameObject.Find("EventBus").GetComponent<EventBus>();           //获取指定对象的EventBus组件并赋值给 eventBus
        GSM = GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>();//获取指定对象的GameStateMachine组件并赋值给 GSM
        dialogueUI = GameObject.Find("GamePlayUI").GetComponent<DialogueUI>();     //获取指定对象的DialogueUI组件并赋值给dialogueUI

    }
    void Start()
    {
        //if (EventBus.OnDayEnd != null)//若timeSystem不为null（已经拖进TimeSystem组件）//已经是静态的不需要判断了
        //EventBus.OnDayEnd += StartEveningStory;       // 订阅EventBus的OnDayEnd事件，当事件触发时（结束当天）调用StartEveningStory函数
    
    }

    void OnEnable()// 组件被启用时调用的函数，订阅事件
    {
        EventBus.OnDayEnd += StartEveningStory;
    }
    void OnDisable()// 组件被关闭时调用的函数，取消订阅
    {
        EventBus.OnDayEnd -= StartEveningStory;
    }
    // 监听到OnDayEnd事件以后进行触发的函数，输入参数是当前的天数（day）
    void StartEveningStory(int day)
    {
        // 获取当天剧情数据
        var dialogue = StoryData.GetDialogue(day);

        if (dialogueUI != null)
            dialogueUI.Show(dialogue);   // 调用播放对话的函数（DialogueUI的Show函数），传入当天的剧情数据（dialogue）
    }
}