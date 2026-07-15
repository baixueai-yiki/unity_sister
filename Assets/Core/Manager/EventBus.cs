using UnityEngine;  //导入Unity常用库
using System;       //导入C#系统库（用来定义事件和委托）
//本来要定义事件需要写System.Action但导入了这个库就不用写System.了


//static代表不需要挂在对象上就能用,不过我这里不用static函数//还是用静态的吧
public static class EventBus
{
    //下面这些都是第一行注册事件，第二行广播事件




    // 由PlayerInteract调用，由GameStateMachine监听。用于切换游戏状态
    public static event Action OnPlay;
    public static void RaisePlay()
        => OnPlay?.Invoke();        //广播事件：正在玩
    // 由PlayerInteract调用，由GameStateMachine监听。用于切换游戏状态
    public static event Action OnInteract;
    public static void RaiseInteract()
        => OnInteract?.Invoke();      //广播事件：正在互动



    // 由DialogueUI调用，由GameStateMachine监听。用于切换游戏状态
    public static event Action OnDialogueEnd;
    public static void RaiseDialogueEnd()
        => OnDialogueEnd?.Invoke();     //广播事件：对话结束
    // 由TimeSystem调用，由StoryManager、GameStateMachine监听。用于显示对话文本、切换对话状态
    // 传入了名为day的int变量，用来引导StoryManager获取对应天数的剧情文本
    public static event Action<int> OnDayEnd;
    public static void RaiseDayEnd(int days)
        => OnDayEnd?.Invoke(days);      //广播事件：今天结束（传参数days天数)
    
    // 由InventorySystem调用，由各种容器本体监听。传入id定位刷新容器，传入索引定位刷新格子
    public static event Action<int> OnPlayerInventoryChanged;
    public static event Action<int> OnTableInventoryChanged;
    public static void RaiseInventoryChanged(string InventoryID, int index)
    {
        switch (InventoryID)
        {
            case "PlayerInventory":
                OnPlayerInventoryChanged?.Invoke(index);//广播事件：玩家容器变化（玩家背包）
                break;
            case "TableInventory":
                OnTableInventoryChanged?.Invoke(index);//广播事件：桌子容器变化（桌子储存格）
                break;
            default:
                Debug.LogWarning("Unhandled inventory ID: " + InventoryID);
                return;
        }
    }
}
