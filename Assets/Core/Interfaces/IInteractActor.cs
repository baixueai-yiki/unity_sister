using UnityEngine;

public interface IInteractActor//声明一个接口IInteractActor
{
    // 算是一种数据中转站
    string GetInteractID();//获取互动对象的互动ID（工作台类型）
    void Interact();
    void EndInteract();
}