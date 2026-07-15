using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 这是一个拖拽物品时的显示UI，可以显示自己正在拖拽的物品图标和数量，
// 这种拖动本质上就是一个视觉效果，里面不存物品数据，只存容器和格子数据
public class DragSystem : MonoBehaviour
{
    private IItemSystem iItemSystem;        //声明接口InventorySlots的变量inventorySlots，用于引用容器脚本
    public Image Drag_Icon;                 //引用：图像对象 拖动图标（Art/Textures/Items/apple.png）
    public TextMeshProUGUI Drag_Amount;     //引用：文本对象 拖动数量
    public bool dragState = false;          // 拖动状态
    public string dragInventoryID;          // 拖动容器id
    public InventorySlot[] dragInventory;   // 拖动容器
    public int dragInventoryIndex;          // 拖动容器索引
    public string dragItemID;               // 拖动物品id
    public Sprite dragItemSprite;           // 拖动物品精灵（图标）
    public int dragItemAmount;              // 拖动物品数量
    void Awake()
    {
        
        //Drag_Icon.blocksRaycasts = false;//拖动的物品图标不阻挡射线检测，这样鼠标就可以点击到下面的格子UI
    }
    void Update()//每帧调用的函数，刷新拖拽物品UI的位置
    {
        if (dragState)//若处于拖动状态，则让拖拽物品UI跟随鼠标位置
            FollowMouse();
    }
    // 设置拖动数据的函数
    public void SetDragData
        (
        bool state,                         //参数：当前是否处于拖动状态
        string inventoryID,                 //参数：被拖动的容器ID
        InventorySlot[] inventory,          //参数：被拖动的容器数组
        int inventoryIndex,                 //参数：被拖动的容器索引
        string itemId,                      //参数：拖动的物品id
        Sprite sprite,                      //参数：拖动的物品精灵
        int amount                          //参数：拖动的物品数量
        )
    {
        dragState = state;                  //赋值 当前是否处于拖动状态
        dragInventoryID = inventoryID;      //赋值 被拖动的容器id
        dragInventory = inventory;          //赋值 被拖动的容器
        dragInventoryIndex = inventoryIndex;//赋值 被拖动的容器索引
        dragItemID = itemId;                //赋值 被拖动的物品id
        dragItemSprite = sprite;            //赋值 被拖动的物品精灵
        dragItemAmount = amount;            //赋值 被拖动的物品数量
        Drag_Icon.sprite = sprite;          //设置拖动的图标
        Drag_Amount.text = amount.ToString();//设置拖动的数量
        gameObject.SetActive(true);         //设置激活状态，显示拖拽者对象
        //Drag_Icon.enabled = true;           //设置激活状态，使图标显示//这玩意本就要始终显示吧
    }
    // 清除拖动数据的函数
    public void ClearDragData()
    {
        dragState = false;                  //赋值 当前是否处于拖动状态
        dragInventoryID = "";               //赋值 被拖动的容器ID
        dragInventory = null;               //赋值 被拖动的容器
        dragInventoryIndex = 0;             //赋值 被拖动的容器索引
        dragItemID = "";                    //赋值 被拖动的物品id
        dragItemSprite = null;              //赋值 被拖动的物品精灵
        dragItemAmount = 0;                 //赋值 被拖动的物品数量
        Drag_Icon.sprite = null;            //赋值 拖动的图标
        Drag_Amount.text = "";              //赋值 拖动的数量
        gameObject.SetActive(false);        //关闭激活状态，显示拖拽者对象
    }
    //让拖拽物品UI跟随鼠标位置的函数
    public void FollowMouse()
    {
        transform.position = Input.mousePosition;
    }
}