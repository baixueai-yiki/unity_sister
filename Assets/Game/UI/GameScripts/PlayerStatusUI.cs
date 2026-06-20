using TMPro;
using UnityEngine.UI;               //导入Unity UI库，用于处理UI组件
using UnityEngine;                  //导入Unity常用库


//定义一个名为PlayerStatusUI的类，继承自MonoBehaviour
//这个类负责在UI上显示玩家的状态信息，如体力值
public class PlayerStatusUI : MonoBehaviour
{
    public PlayerStats stats;               //声明一个变量，命名为stats，指向场景中的PlayerStats
    public Slider Bar_Stamina;               //声明一个Slider变量，命名为Bar_Stamina，用于显示体力值的进度条
    public TextMeshProUGUI Text_Stamina;     //声明一个TextMeshProUGUI变量，命名为Text_Stamina，用于显示体力值
    //public TextMeshProUGUI temperatureText; //声明一个TextMeshProUGUI变量，命名为temperatureText，用于显示温度值
    //public TextMeshProUGUI hungerText;      //声明一个TextMeshProUGUI变量，命名为hungerText，用于显示饥饿值

    void Update()    //每一帧都调用的函数
    {
        if (stats == null) return;   // 若 变量stats没有被赋值（即没有找到PlayerStats组件）则 直接返回


        Bar_Stamina.maxValue = stats.maxStamina;   // 同步最大值
        Bar_Stamina.value = stats.stamina;         // 直接用当前体力
        //temperatureText.text = "体温：" + ((int)stats.temperature); //将温度值转换为整数并显示在temperatureText上，前面加上"体温："的标签。但我希望体温是隐藏数据
        //hungerText.text = "饥饿：" + ((int)stats.hunger);           //将饥饿值转换为整数并显示在hungerText上，前面加上"饥饿："的标签
    }

}
