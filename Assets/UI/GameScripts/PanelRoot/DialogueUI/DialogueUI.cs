using TMPro;                        //导入TextMeshPro库，用于处理文本显示
using System.Collections.Generic;   //现代List<类型>工具箱
using UnityEngine;                  //导入Unity常用库

public class DialogueUI : MonoBehaviour
{
    //public EventBus eventBus;               //引用：EventBus事件总线，用来广播或监听事件
    public GameObject Panel_Dialogue;       //引用：Canvas中的UI面板组件Panel_Dialogue
    public TextMeshProUGUI Text_Dialogue;   //引用：Canvas中的文本组件Text_Dialogue
    private List<string> lines;             // 声明字符串列表变量lines，用来存储当前正在播放的对话内容（多句话）
    private int index;                      // 声明整数变量index，用来记录当前正在显示的对话内容是第几句（lines列表中的索引）

    void Awake()
    {
        //eventBus = GameObject.Find("EventBus").GetComponent<EventBus>();//获取指定对象的EventBus组件并赋值给 eventBus

    }


    void Start()
    {
        Panel_Dialogue.SetActive(false);    // 游戏开始时，默认隐藏Panel_Dialogue面板
    }

    // 函数Show,输入的参数是一个名为dialogue的字符串列表List<string>，表示要显示的对话内容（多句话）
    public void Show(List<string> dialogue)
    {
        lines = dialogue;                   // 保存传入的对话内容
        index = 0;                          // 从第0句开始播放
        Panel_Dialogue.SetActive(true);     // 显示对话框UI
        Text_Dialogue.text = lines[index];  // 显示第一句话
    }

    void Update()
    {
        // 若 如果对话框没有显示，就不执行任何逻辑
        if (!Panel_Dialogue.activeSelf) 
            return;

        // 若 鼠标左键点击 或 按空格
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            index++;// 显示下一句

            // 如果已经超过最后一句
            if (index >= lines.Count)
            {
                Hide(); // 关闭对话框
            }
            else
            {
                Text_Dialogue.text = lines[index];// 显示下一句内容
            }
        }
    }

    void Hide()     // 隐藏Panel_Dialogue面板
    {
        Panel_Dialogue.SetActive(false);
        EventBus.RaiseDialogueEnd();          // 调用EventBus广播事件，用来通知外部对话结束
    }
}