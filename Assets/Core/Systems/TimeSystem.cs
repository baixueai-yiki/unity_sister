using UnityEngine;  //导入Unity常用库

public class TimeSystem : MonoBehaviour
{
    public EventBus eventBus;            //引用：EventBus事件总线，用来广播或监听事件
    public int days = 1;   // 被拘禁的天数
    public int hours = 8;  // 当前时间（女主早上8点出门）
    public int baseReturnHour = 18;     // 女主标准回家时间
    public int extraDelay = 0;          // 女主回家时间浮动值
    public int ReturnHour               // 声明一个整数类型的属性
    {
        get { return baseReturnHour + extraDelay; } // 返回最终回家时间（标准事件+浮动值）
    }

    void Awake()
    {
        eventBus = GameObject.Find("EventBus").GetComponent<EventBus>();           //获取指定对象的EventBus组件并赋值给 eventBus

    }


    // 推进时间（只加小时）
    public void AddHours(int addHours)
    {
        hours += addHours;              // 增加小时数
        hours = hours % 24;             // 超过24小时后重置为0-23
        if (hours >= ReturnHour)        // 若 当前时间>=女主回家时间
        {
            EndDay();                   // 今天结束（推进到第二天，重置时间，触发事件）
        }
    }

    void EndDay()                       // 今天结束时调用的函数
    {
        days += 1;
        hours = 8;
        extraDelay = 0;
        eventBus.RaiseDayEnd(days);     // 调用EventBus广播事件，通知所有监听OnDayEnd事件的脚本,并传递当前的天数（days）作为参数
    }
}