using UnityEngine;


// 这是一个拖拽物品时的显示UI，可以显示自己正在拖拽的物品图标和数量
// 这个东西不是一个完整容器，他是一个轻量缓存
public class DragItemUI : MonoBehaviour
{
    // public Image Img_Icon;                         // 图标（Art/Textures/Items/apple.png）
    // public TextMeshProUGUI Text_Amount;            // 数量

    // public string dragId;//拖动的物品id
    // public Sprite dragIcon;//拖动的物品图标
    // public string dragAmount;//拖动的物品数量

    // public void SetDragItemUI(string itemId, Sprite itemSprite, int itemAmount)
    // {
    //     dragId = itemId;
    //     Img_Icon.sprite = itemSprite;
    //     dragIcon.enabled = true;
    //     Text_Amount.text = itemAmount.ToString();
    // }

    public void FollowMouse()
    {
        transform.position = Input.mousePosition;
    }
}