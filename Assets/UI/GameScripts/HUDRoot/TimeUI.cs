using TMPro;                        //导入TextMeshPro库，用于处理文本显示
using UnityEngine;                  //导入Unity常用库

public class TimeUI : MonoBehaviour
{
    public TimeSystem timeSystem;           //引用：TimeSystem时间系统
    public TextMeshProUGUI Text_Time;       //引用：Canvas中的文本组件Text_Time

    private void Awake()//场景加载时调用的函数
    {   //这些都是让组件在运行游戏时自动绑定的，可以让这些组件没必要在编辑器里手动选定
        timeSystem = GameObject.Find("TimeSystem").GetComponent<TimeSystem>();// 获取指定对象的 GameStateMachine 组件并赋值给 GSM
        
    }

    void Update()
    {
        if (timeSystem == null) return;     // 若 时间系统不存在 则 直接返回
        //将Text_Time的文本赋值为 当前天数 + " day  " + 当前小时数       + " :00"
        Text_Time.text = timeSystem.days + " day  " + timeSystem.hours + " :00";
    }
}