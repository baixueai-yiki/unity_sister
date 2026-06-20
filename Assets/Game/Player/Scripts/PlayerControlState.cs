using UnityEngine;                 //导入Unity常用库

public class PlayerControlState : MonoBehaviour
{
    public bool canMove = true;    // 是否允许移动
    public bool canInteract = true;// 是否允许互动
    public bool canRest = true;    // 是否允许休息

    // 锁定所有输入（对话中）
    public void LockAll()
    {
        canMove = false;
        canInteract = false;
        canRest = false;
    }

    // 恢复正常输入
    public void ResetState()
    {
        canMove = true;
        canInteract = true;
        canRest = true;
    }

    // 只锁移动
    public void LockMove()
    {
        canMove = false;
    }
}