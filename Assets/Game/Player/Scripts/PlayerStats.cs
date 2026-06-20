using System.Collections;           //老式工具箱，现在只用到协程
using System.Collections.Generic;   //现代List<类型>工具箱
using UnityEngine;                  //导入Unity常用库

//定义了一个类:PlayerController
//public表示这个类可以在Unity引擎的 Inspector 面板修改
//: MonoBehaviour 表示它继承自 Unity 的基础组件类
//只有继承后才能挂在物体上、使用 Start(), Update() 等生命周期函数
public class PlayerStats : MonoBehaviour
{
    //中括号是属于C#的一种标记，用于表示Attribute（特性）
    //Header用于在unity编辑器里添加一个叫做“体力”的标题
    //Tooltip用于在unity编辑器里给变量添加一个提示，当鼠标悬停在变量上时显示提示内容
    [Header("体力")]
    [Tooltip("当前体力值")] public float stamina = 100f;
    [Tooltip("最大体力值")] public float maxStamina = 100f;
    [Tooltip("每秒移动消耗的体力")]public float moveCostPerSecond = 5f;
    //[Tooltip("每秒恢复的体力")]public float recoverRatePerSecond = 10f;   //我不打算设计一个自动恢复体力的系统，所以先注释掉

    //游戏开始时调用的函数
    void Start()
    {
        stamina = 100f;   //将当前体力设置为100
    }

    //返回一个是否还有体力的bool值
    public bool HasStamina()
    {
        return stamina > 0f;
    }

    // 消耗体力（传入一个名为amount的float整数值）
    public void ConsumeStamina(float amount)
    {
        stamina -= amount;  //使当前体力减以传入的值
        stamina = Mathf.Clamp(stamina, 0f, maxStamina); //限定一下最终体力的值
    }

    // 恢复体力（传入一个名为amount的float整数值）
    public void RecoverStamina(float amount)
    {
        stamina += amount;  //使当前体力加以传入的值
        stamina = Mathf.Clamp(stamina, 0f, maxStamina); //限定一下最终体力的值
    }
}
