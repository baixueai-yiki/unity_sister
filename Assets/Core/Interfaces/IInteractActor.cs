using UnityEngine;

public interface IInteractActor//声明一个接口IInteractActor
{
    // 算是一种数据中转站
    //接口是一个约束，要求实现这个接口的类必须实现GetSlots方法
    string GetInteractID();//获取互动对象的互动ID（工作台类型）
}