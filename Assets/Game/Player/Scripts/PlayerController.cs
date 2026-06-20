using System.Collections;           //老式工具箱，现在只用到协程
using System.Collections.Generic;   //现代List<类型>工具箱
using UnityEngine;                  //导入Unity常用库


//定义了一个类:PlayerController
//public表示这个类可以在Unity引擎的 Inspector 面板修改
//: MonoBehaviour 表示它继承自 Unity 的基础组件类
//只有继承后才能挂在物体上、使用 Start(), Update() 等生命周期函数
public class PlayerController : MonoBehaviour
{
    public GameStateMachine GSM;                    //引用：GameStateMachine全局游戏状态机
    public PlayerRest rest;                         //引用：PlayerRest玩家休息脚本
    public PlayerStats stats;                       //引用：PlayerStats玩家属性
    public PlayerControlState PCS;                  //引用：PlayerControlState玩家输入状态
    
    public float moveSpeed = 5f;                    // 声明小数变量 命名为moveSpeed = 玩家移动速度
    private Rigidbody2D rb2d;                       // 声明Rigidbody2D 物理组件，命名为rb
    private Vector2 moveInput;                      // 声明Vector2 二维向量，命名为moveInput

  

    private void Awake()//场景加载时调用的函数
    {
        //这些都是让组件在运行游戏时自动绑定的，可以让这些组件没必要在编辑器里手动选定
        //但总的来说比手动绑定更麻烦，本项目只是练习这种用法
        GSM = GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>();// 获取指定对象的 GameStateMachine 组件并赋值给 GSM
        
        //controller = GetComponent<PlayerController>();  // 获取自身对象的 PlayerController 组件并赋值给 controller
        stats = GetComponent<PlayerStats>();            // 获取自身对象的 PlayerStats 组件并赋值给 stats
        rest = GetComponent<PlayerRest>();              // 获取自身对象的 PlayerRest 组件并赋值给 rest
        PCS = GetComponent<PlayerControlState>();       // 获取自身对象的 PlayerControlState 组件并赋值给 PCS
        rb2d = GetComponent<Rigidbody2D>();             // 获取自身对象的 Rigidbody2D 组件并赋值给 rb2d
    }
    private void Start()    // 初始化时调用的函数
    {


    }

    
    void Update()           //每一帧都调用的函数
    {
        if (GSM == null)    //不存在GSM则返回
            return;

        MoveInput();        //移动输入
        RestInput();        //休息输入
        MoveApply();        //移动执行
        StaminaApply();     //体力（消耗）执行
    }

    void MoveInput()         //移动输入
    {
        if (!GSM.canMove)    //不允许移动则返回
            return;
        //浮点数变量moveX = 水平（x轴）输入
        //"Horizontal"是Unity的一个标签，代表输入轴（Input Axis），也就是x轴
        float moveX = Input.GetAxisRaw("Horizontal");
        //转成二维向量
        moveInput = new Vector2(moveX, 0f);
    }

    void RestInput()         //休息输入
    {
        if (!GSM.canRest)    //不允许休息则返回
            return;
        //若 unity输入系统.按下瞬间（s键） 
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (rest != null)
                rest.StartRest();//调用休息脚本的休息函数
        }
    }

    void MoveApply()        //移动执行
    {
        //刚体rb2d.当前（移动）速度 = 创建一个二维速度（x轴输入 * 移动速度变量，垂直速度保持不变）
        rb2d.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb2d.linearVelocity.y);
    }

    void StaminaApply()      //体力（消耗）执行
    {
        if (stats == null)   //不存在stats玩家属性则返回
            return;

        if (moveInput.x != 0)//若 移动输入.x 正在移动
        {
            //每帧扣体力 调用体力脚本的减少体力函数（每秒移动消耗的体力 乘以 Time.deltaTime）
            stats.ConsumeStamina(stats.moveCostPerSecond * Time.deltaTime);
        }
    }
}
