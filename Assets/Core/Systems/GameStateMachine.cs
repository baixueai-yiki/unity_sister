using UnityEngine;


public class GameStateMachine : MonoBehaviour
{
    public EventBus eventBus;            //引用：EventBus事件总线，用来监听事件
    public PlayerController controller;  //引用：PlayerController玩家控制脚本
    
    public bool canMove = true;
    public bool canRest = true;
    public bool canInteract = true;


    //声明一个枚举，命名为GameState，这是一种互斥的游戏状态
    public enum GameState
    {
        Play,
        Dialogue,
        Pause
    }
    // 声明一个GameState，命名为CurrentState，作为当前状态使用{ 外部可以读取 ; 外部不能修改 }
    public GameState CurrentState { get; private set; }

    //场景加载时调用一次的函数
    void Awake()
    {
        eventBus = GameObject.Find("EventBus").GetComponent<EventBus>();  // 获取外部对象的EventBus组件并赋值给 eventBus
        controller = GameObject.Find("Player").GetComponent<PlayerController>();  // 获取外部对象的PlayerController组件并赋值给 controller
        


        if (eventBus != null)
        //监听OnDayEnd今天结束事件 += Dialogue对话开始
        eventBus.OnDayEnd += OnEnterDialogue;
        //OnRequestPause += HandlePauseRequest; //现在还没有暂停这种事件

        if (eventBus != null)
        //监听OnDialogueFinishedd对话结束事件 += Dialogue对话结束
        eventBus.OnDialogueEnd += OnExitDialogue;
    }

    //初始化时调用的函数，游戏默认设置为Play状态
    void Start()
    {
        SetState(GameState.Play);
    }


    // ====== 下面是通过事件监听调用的函数 ======
    void OnEnterDialogue(int day)
    {
        SetState(GameState.Dialogue);
    }


    void OnExitDialogue()
    {
        SetState(GameState.Play);
    }
    // void HandlePauseRequest()//现在还没有地方需要暂停
    // {
    //     SetState(GameState.Pause);
    // }

    // 监听不同事件最终指向的是同一个函数，并且传入了GameState枚举的值，用来精准修改状态
    private void SetState(GameState newState)
    {
        if (CurrentState == newState)
            return;

        OnExit(CurrentState);       //结束状态时执行

        CurrentState = newState;    //将旧状态设置为新状态

        OnEnter(newState);          //开始状态时执行
    }

    //开始一个状态时进行的修改
    void OnEnter(GameState state)
    {
        switch (state)
        {
            case GameState.Play:
                Time.timeScale = 1f; //这玩意并不是我自己的事件系统，是unity自己的

                canMove = true;
                canInteract = true;
                canRest = true;
                break;

            case GameState.Dialogue:
                Time.timeScale = 0f;

                canMove = false;
                canInteract = false;
                canRest = false;
                //Panel_Dialogue.SetActive(true);     // 这里不应该管UI
                break;

            case GameState.Pause:
                Time.timeScale = 0f;

                canMove = false;
                canInteract = false;
                canRest = false;
                break;
        }
    }

    //结束一个状态时进行的修改
    void OnExit(GameState state)
    {
        switch (state)
        {
            case GameState.Play:
                // 我目前想不到Play结束需要做些什么
                break;

            case GameState.Dialogue:
                // 对话结束清理
                // 例如：
                //Panel_Dialogue.SetActive(false);// 这里不应该管UI
                break;

            case GameState.Pause:
                // 防止异常状态残留（可选）
                Time.timeScale = 1f;
                break;
        }
    }

    // ====== 外部只读接口 ======（我还不明白这是干啥用的）
    public bool Is(GameState state)
    {
        return CurrentState == state;
    }
}