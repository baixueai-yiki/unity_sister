using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventorySlotUI : MonoBehaviour
{
    //public ItemDatabase itemDatabase;//引用：物品数据库//每个格子都调用费性能，改成把itemDatabase当成参数传进来
    public int index;
    public InventorySlotUI parentUI;    //定义一个InventorySlotUI的父容器UI parentUI
    //public string id;                               // 唯一ID（apple）这个界面不需要显示id
    //public Sprite Img_Slot;                         // 格子框 好像不需要设置格子精灵
    public Image Img_Icon;                         // 图标（Art/Textures/Items/apple.png）
    public Sprite appleIcon;                       // 默认图标（Art/Textures/Items/apple1.png）
    public TextMeshProUGUI Text_Amount;             // 数量
    //public GameObject Panel_Tooltip;                // 面板（选中时展开）
        //public Sprite Img_Highlight;                // 高亮格子 好像不需要设置格子精灵
        //public string Text_Name;                    // 显示名称（苹果）感觉这个界面不需要显示名称
        //public TextMeshProUGUI Text_Description;    // 简介（白雪的苹果）

    //public int maxStack = 1;                      // 最大堆叠
    /*private void Awake()  //场景加载时调用的函数
    {
        //Img_Slot = GetComponentInChildren<Sprite>();
        Img_Icon = GetComponentInChildren<Image>();
        Text_Amount = transform.Find("Text_Amount").GetComponent<TMP_Text>();
        Panel_Tooltip = transform.Find("Panel_Tooltip").gameObject;
            //Img_Highlight = GetComponentInChildren<Sprite>();
            Text_Description = GetComponentInChildren<TextMeshProUGUI>();
    }*/

    /*void Start()
    {
        Panel_Tooltip.SetActive(false); // 游戏开始时，默认隐藏Panel_Tooltip面板
    }*/


    public void GetSlotIcon(Sprite Icon)//设置图标
    {
        //if (Img_Icon == null)//现在格子总是有
        //    return;
        if (Icon == null)
        {
            Debug.Log("apple");
            Img_Icon.sprite = appleIcon;//没有图标时显示默认图标
            Img_Icon.enabled = false;//不用的时候直接关闭以免占用渲染
            //Img_Icon.gameObject.SetActive(false);
            return;
        }
        Debug.Log("apple");
        //Img_Icon.gameObject.SetActive(true);
        Img_Icon.enabled = true;
        Img_Icon.sprite = Icon;
    }

    public void GetSlotAmount(int Amount)//设置数量
    {
        if (Amount <= 1)
        {
            Text_Amount.text = "";//应该设置成空字符串而不是null
            return;
        }
        Text_Amount.text = Amount.ToString();//将Amount这个int转为字符串
    }

    /*public void GetSlotDescription(string Description)//设置简介
    {
        if (Description == Text_Description.text)
            return;
        if (Description == null)
            //把Img_Icon的sprite赋值为通过itemDatabase的GetItemData函数读取到的apple0的对应返回值的Icon变量
            Text_Description.text = "";
        else
            Text_Description.text = Description;
    }*/
    //设置格子UI的函数，传入的参数是一个ItemData类，命名为Item
    //这个统一设置的代码不要了，我认为由大的UI脚本分别设置更直观
    //而且留出了设置假信息欺骗玩家的空间
    /*private void GetSlotUI(ItemData Item)
    {
        Img_Icon.sprite = Item.Icon;    //将Img_Icon的精灵设置为Item的Icon
        Text_Amount.sprite = Item.Icon; //将Img_Icon的精灵设置为Item的Icon
        Img_Icon.sprite = Item.Icon;    //将Img_Icon的精灵设置为Item的Icon
        Img_Icon.sprite = Item.Icon;    //将Img_Icon的精灵设置为Item的Icon
    ItemData item = itemDatabase.Get(id);    
    name = item.name;
    }*/

    
}
