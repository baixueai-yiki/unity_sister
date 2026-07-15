using UnityEngine;              //基础 Unity 功能
using UnityEngine.UI;
using UnityEngine.EventSystems; //UI交互系统（点击、拖拽、悬停）
using TMPro;

// 单个格子的UI脚本，用于显示物品图标和数量，IPointerClickHandler是系统自带的处理鼠标点击事件的接口
public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    //public ItemDatabase itemDatabase;//引用：物品数据库//每个格子都调用费性能，改成把itemDatabase当成参数传进来
    public int index;                              //格子索引（用于指引ui交互）
    public InventoryUI inventoryUI;                //引用：InventoryUI容器UI（自己的父容器）
    public Image Img_Icon;                         // 图标
    public Sprite appleIcon;                       // 默认图标（Art/Textures/Items/apple1.png）
    public TextMeshProUGUI Text_Amount;            // 数量
    //public string id;                               // 唯一ID（apple）这个界面不需要显示id
    //public Sprite Img_Slot;                         // 格子框 好像不需要设置格子精灵
    //public GameObject Panel_Tooltip;                // 面板（选中时展开）
        //public Sprite Img_Highlight;                // 高亮格子 好像不需要设置格子精灵
        //public string Text_Name;                    // 显示名称（苹果）感觉这个界面不需要显示名称
        //public TextMeshProUGUI Text_Description;    // 简介（白雪的苹果）
    void Awake()
    {
        
    }
    void Start()    // 初始化时调用的函数
    {
        // 注册格子的函数（参数：容器格类的this，int类的排序编号）
        // transform.GetSiblingIndex()用于获取在父物体下的排序编号
        //inventoryUI.RegisterSlot(this, transform.GetSiblingIndex());
    }
    //鼠标点击事件(IPointerClickHandler接口的实现
    //参数：PointerEventData eventData是鼠标点击事件的数据：鼠标左键 / 右键、点击位置、UI对象信息
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left://触发输入鼠标左键
                inventoryUI.LeftClick(index);
                break;
            case PointerEventData.InputButton.Right://触发输入鼠标右键
                inventoryUI.RightClick(index);
                break;
            //case PointerEventData.InputButton.Middle://触发输入鼠标中键
                //inventoryUI.MiddleClick(index);
                //break;
            default:
                return;
        }
    }



    public void GetSlotIcon(Sprite Icon)//设置图标
    {
        //if (Img_Icon == null)//现在格子总是有
        //    return;
        if (Icon == null)
        {
            Img_Icon.sprite = appleIcon;//没有图标时显示默认图标
            //Img_Icon.enabled = false;//不用的时候直接关闭以免占用渲染
            //Img_Icon.gameObject.SetActive(false);
            return;
        }
        //Img_Icon.gameObject.SetActive(true);
        Img_Icon.enabled = true;
        Img_Icon.sprite = Icon;
    }

    public void GetSlotAmount(int amount)//设置数量
    {
        if (amount <= 1)
        {
            Text_Amount.text = "";//应该设置成空字符串而不是null
            return;
        }
        Text_Amount.text = amount.ToString();//将Amount这个int转为字符串
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
