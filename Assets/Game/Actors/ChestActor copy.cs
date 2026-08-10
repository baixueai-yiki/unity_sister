using UnityEngine;

public class ChestActor : MonoBehaviour, IInteractActor
{
    public ChestInventory chestInventory;//引用：箱子容器
    public string InteractID = "ChestInteract";  //箱子（工作台类型）的唯一ID

    public string GetInteractID()//通过接口把InteractID互动id传给InventoryUI
    {
        return InteractID;
    }




    void InteractChest()// 玩家与箱子互动时调用的函数
    {
        chestInventory.Interact();
    }
    void EndInteractChest()// 结束互动时调用的函数
    {
        
        chestInventory.EndInteract();
    }
}
