using UnityEngine;


public class GameStateMachine : MonoBehaviour
{
    //public EventBus eventBus;                   //引用：EventBus事件总线，用来监听事件//现在改成静态了
    public PlayerController playerController;   //引用：PlayerController玩家控制脚本
    


    //声明一个枚举，命名为GameState，这是一种互斥的游戏状态
    public enum GameState
    {
        Play,
        Interact,
        Dialogue,
        Pause
    }
    // 声明一个GameState，命名为CurrentState，作为当前状态使用{ 外部可以读取 ; 外部不能修改 }
    public GameState CurrentState { get; private set; }

    //场景加载时调用一次的函数
    void Awake()
    {
        //eventBus = GameObject.Find("EventBus").GetComponent<EventBus>();                // 获取外部对象的EventBus组件并赋值给 eventBus
        //playerController = GameObject.Find("Player").GetComponent<PlayerController>();  // 获取外部对象的PlayerController组件并赋值给 controller
        
    }

    
    void Start()//初始化时，将游戏默认设置为Play状态
    {
        SetGameState(GameState.Play);

        //监听OnDayEnd今天结束事件 += Dialogue对话开始
        //EventBus.OnDayEnd += OnEnterDialogue;

        //监听OnDialogueEnd对话结束事件 += Dialogue对话结束
        //EventBus.OnDialogueEnd += OnExitDialogue;

        //OnRequestPause += HandlePauseRequest; //现在还没有暂停这种事件
    }


    void OnEnable()// 组件被启用时调用的函数，订阅事件
        {
            EventBus.OnPlay += Play;//正在玩事件
            EventBus.OnInteract += Interact;//正在互动事件
            EventBus.OnDayEnd += EnterDialogue;//今天结束事件
            EventBus.OnDialogueEnd += ExitDialogue;//对话结束事件
        }
    void OnDisable()// 组件被关闭时调用的函数，取消订阅
        {
            EventBus.OnPlay -= Play;
            EventBus.OnInteract -= Interact;
            EventBus.OnDayEnd -= EnterDialogue;
            EventBus.OnDialogueEnd -= ExitDialogue;
        }

    // ====== 下面是通过事件监听调用的函数 ======
    void Play()                 //互动开始，设置游戏状态：Play玩
     => SetGameState(GameState.Play);
    void Interact()               //互动开始，设置游戏状态：Interact互动
     => SetGameState(GameState.Interact);

    void EnterDialogue(int day)   //对话开始，设置游戏状态：Dialogue对话
     => SetGameState(GameState.Dialogue);

    void ExitDialogue()           //对话结束，设置游戏状态：Play玩
     => SetGameState(GameState.Play);

    // void HandlePauseRequest()    //现在还没有地方需要暂停
    //  => SetState(GameState.Pause);

    // 监听不同事件最终指向的是同一个函数，并且传入了GameState枚举的值，用来精准修改状态
    private void SetGameState(GameState newState)
    {
        // CurrentState的默认值应该是0才对，这里临时写成错误的null
        if (CurrentState == null || CurrentState == newState)
            return;
        //OnExit(CurrentState);       //结束状态时执行,迁移到了PlayerController

        CurrentState = newState;    //将旧状态设置为新状态
        //OnEnter(newState);          //开始状态时执行，不需要了
    }


    // ====== 外部只读接口 ======（我还不明白这是干啥用的）
    public bool Is(GameState state)
    {
        return CurrentState == state;
    }
}