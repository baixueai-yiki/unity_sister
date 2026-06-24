using UnityEngine;                 //导入Unity常用库


// 考虑到玩家对象的状态大多是连贯的，这里就不使用监听事件的方法调用修改状态的函数
// 而是直接从PlayerController调用函数并传入参数
public class PlayerStateMachine : MonoBehaviour
{
    public EventBus eventBus;               //引用：EventBus事件总线，用来监听事件
    public PlayerController controller;     //引用：PlayerController玩家控制脚本
    //public PlayerAnimation playerAnimation; //引用：PlayerAnimation玩家动画，加player前缀是因为animation和unity自带的重名了
    //private Rigidbody2D rb2d;               // 声明Rigidbody2D 物理组件，命名为rb

    //声明一个枚举，命名为PlayerState，这是一种互斥的游戏状态
    public enum PlayerState
    {
        Idle,
        Walk,
        Animation
    }
    // 声明一个PlayerState，命名为CurrentState，作为当前状态使用{ 外部可以读取 ; 外部不能修改 }
    public PlayerState CurrentState { get; private set; }
    public float Facing = 1f;                     // 声明一个公有变量Facing，用来调控朝向

    //场景加载时调用一次的函数
    void Awake()
    {
        eventBus = GameObject.Find("EventBus").GetComponent<EventBus>();// 获取外部对象的EventBus组件并赋值给 eventBus
        
        controller = GetComponent<PlayerController>();      // 获取自身对象的PlayerController组件并赋值给 controller
        //playerAnimation = GetComponent<PlayerAnimation>();  // 获取自身对象的 PlayerAnimation 组件并赋值给 animation
        //rb2d = GetComponent<Rigidbody2D>();                 // 获取自身对象的 Rigidbody2D 组件并赋值给 rb2d
    }
    
    void Start()//初始化时调用的函数，玩家默认设置为Idle状态
    {
        CurrentState = PlayerState.Idle;
    }

    //负责面朝方向状态的函数
    public void SetFacingState(float moveInput_x)
    {
        if (moveInput_x == 0 )
            return;
        //声明moveFacing并赋值=负的 提取方向参数（moveInput向量的x轴）
        float moveFacing = - Mathf.Sign(moveInput_x);
        Facing = moveFacing;
    }

    //声明bool变量 isMoving ,判断currentState当前状态是否是Walk。但这个代码有什么存在意义吗
    //public bool isMoving => currentState == MoveState.Walk;



    public void SetPlayerState(float moveInput_x)
    {
        if  (moveInput_x != 0)
        {
            CurrentState = PlayerState.Walk;
        }
        else
        {
            CurrentState = PlayerState.Idle;
        }
    }
}