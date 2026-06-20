using UnityEngine;                  //导入Unity常用库

public class PlayerRest : MonoBehaviour
{
    public PlayerController controller;        //引用：PlayerController玩家控制脚本
    public PlayerStats stats;                  //引用：PlayerStats玩家属性
    public TimeSystem timeSystem;              //引用：TimeSystem时间系统
    public float restDurationHours = 1f;       // 每次休息消耗的时间（小时）
    public float staminaRecovered = 50f;       // 每次休息回复的体力
    //private bool isResting = false;            // 当前是否正在休息

    private void Awake()//场景加载时调用的函数
    {
        timeSystem = GameObject.Find("TimeSystem").GetComponent<TimeSystem>();// 获取外部对象的 TimeSystem 组件并赋值给 timeSystem
        
        controller = GetComponent<PlayerController>();  // 获取自身对象的 PlayerController 组件并赋值给 controller
        stats = GetComponent<PlayerStats>();            // 获取自身对象的 PlayerStats 组件并赋值给 stats
        //rest = GetComponent<PlayerRest>();              // 获取自身对象的 PlayerRest 组件并赋值给 rest
        //PCS = GetComponent<PlayerInputState>();         // 获取自身对象的 PlayerInputState 组件并赋值给 PCS
        //rb2d = GetComponent<Rigidbody2D>();             // 获取自身对象的 Rigidbody2D 组件并赋值给 rb2d
 
    }


    public void StartRest()
    {
        //isResting = true;
        // 这里可以播放躺下动画，不过又感觉应该做成监听事件然后播放动画的样子

        // 回复体力
        stats.RecoverStamina(staminaRecovered);

        // 推进时间
        if (timeSystem != null)
            timeSystem.AddHours((int)restDurationHours);

        //isResting = false;
    }
}