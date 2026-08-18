using UnityEngine;

public class ChestActor : MonoBehaviour, IInteractActor
{
    public ChestInventory chestInventory;//引用：箱子容器
    public string InteractID = "ChestInteract";  //箱子（工作台类型）的唯一ID

    public string GetInteractID()//通过接口把InteractID互动id传给InventoryUI
    {
        return InteractID;
    }
    public void Interact()// 玩家与箱子互动时调用的函数
    {
        //Debug.Log("apple");
        chestInventory.Interact();
    }
    public void EndInteract()// 结束互动时调用的函数
    {
        chestInventory.EndInteract();
    }


}
