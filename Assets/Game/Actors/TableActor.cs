using UnityEngine;

public class TableActor : MonoBehaviour, IInteractActor
{
    //public TableInventory tableInventory;//引用：桌子容器
    public string InteractID = "TableInteract";  //桌子（工作台类型）的唯一ID

    public string GetInteractID()//通过接口把InteractID互动id传给InventoryUI
    {
        return InteractID;
    }
    public void Interact()// 玩家与桌子互动时调用的函数
    {
        
    }
    public void EndInteract()// 结束互动时调用的函数
    {
        
    }

}
