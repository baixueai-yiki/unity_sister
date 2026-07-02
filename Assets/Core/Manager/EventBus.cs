using UnityEngine;  //导入Unity常用库
using System;       //导入C#系统库（用来定义事件和委托）
//本来要定义事件需要写System.Action但导入了这个库就不用写System.了


//static代表不需要挂在对象上就能用,不过我这里不用static函数//还是用静态的吧
public static class EventBus
{
    //下面这些都是第一行注册事件，第二行广播事件


    // 由TimeSystem调用，由StoryManager、GameStateMachine监听。用于显示对话文本、切换对话状态
    // 传入了名为day的int变量，用来引导StoryManager获取对应天数的剧情文本
    public static event Action<int> OnDayEnd;
    public static void RaiseDayEnd(int days)
        => OnDayEnd?.Invoke(days);      //广播事件：今天结束（传参数days天数)
    
    // 由DialogueUI调用，由GameStateMachine监听。用于切换游戏状态
    public static event Action OnDialogueEnd;
    public static void RaiseDialogueEnd()
        => OnDialogueEnd?.Invoke();     //广播事件：对话结束

    // 由InventorySystem调用，由InventoryUI监听。全量刷新：用于提醒全部容器格刷新UI表现
    public static event Action OnInventoryChanged;
    public static void RaiseInventoryChanged()
        => OnInventoryChanged?.Invoke();//广播事件：容器变化（物品添加或减少）

}
