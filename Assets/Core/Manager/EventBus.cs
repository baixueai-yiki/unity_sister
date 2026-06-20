using UnityEngine;  //导入Unity常用库
using System;       //导入C#系统库（用来定义事件和委托）
//本来要定义事件需要写System.Action但导入了这个库就不用写System.了

public class EventBus : MonoBehaviour
{
    //公有的 静态的 无返回值的 函数
    //public static void RaiseDayEnd()
    //static代表不需要挂在对象上就能用,不过我这里不用static函数


    // 由TimeSystem调用，由StoryManager、GameStateMachine监听。
    // 传入了名为day的int变量，用来引导StoryManager获取对应天数的剧情文本
    public event Action<int> OnDayEnd;
    public void RaiseDayEnd(int days)
        => OnDayEnd?.Invoke(days);      //广播事件：今天结束
    
    // 由DialogueUI调用，由GameStateMachine监听。
    public event Action OnDialogueEnd;
    public void RaiseDialogueEnd()
        => OnDialogueEnd?.Invoke();     //广播事件：对话结束


}
