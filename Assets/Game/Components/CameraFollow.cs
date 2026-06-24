using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // 声明一个Transform类型的变量，命名为target目标。代表要跟随的玩家对象
    // Transform（变换）就是物体的那三种信息，包含了位置、旋转、缩放
    public Transform target;

    void LateUpdate()//每一帧调用的函数，优先级比Update低
    {
        //声明一个临时的三维坐标变量pos，赋值为当前摄像机的位置
        Vector3 pos = transform.position;

        //将pos临时变量赋值为玩家的玩家当前的位置
        pos.x = target.position.x;
        pos.y = target.position.y;
        //将摄像机的xy坐标赋值为pos临时变量，也就是玩家的坐标（z轴不变）
        transform.position = pos;
    }
}
