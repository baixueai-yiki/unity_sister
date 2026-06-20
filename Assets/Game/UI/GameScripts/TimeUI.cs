using TMPro;                        //导入TextMeshPro库，用于处理文本显示
using UnityEngine;                  //导入Unity常用库

public class TimeUI : MonoBehaviour
{
    public TimeSystem timeSystem;           //引用：TimeSystem时间系统
    public TextMeshProUGUI Text_Time;   //引用：Canvas中的文本组件Text_Time

    void Update()
    {
        if (timeSystem == null) return;     // 若 时间系统不存在 则 直接返回
        //将Text_Time的文本赋值为 当前天数 + " day  " + 当前小时数       + " :00"
        Text_Time.text = timeSystem.days + " day  " + timeSystem.hours + " :00";
    }
}