using UnityEngine;

public class TableActor : MonoBehaviour
{
    public TableInventory tableInventory;//引用：桌子容器




    void InteractTable()// 玩家与桌子互动时调用的函数
    {
        tableInventory.Interact();
    }
    void EndInteractTable()// 结束互动时调用的函数
    {
        
        tableInventory.EndInteract();
    }
}
