using UnityEngine;

public class PositionClamp : MonoBehaviour
{
    public Vector2 minBounds;   //声明一个二维坐标，命名为minBounds最小边界（这里的最小指负数坐标）
    public Vector2 maxBounds;     //声明一个二维坐标，命名为maxBounds最大边界

    void LateUpdate()//每一帧调用的函数，优先级比Update低
    {
        //transform.position获取当前位置 赋值给 Vector3三维坐标 pos（position的缩写）
        Vector3 pos = transform.position;

        //若pos.x/y 小于min/大于max 则将pos.x/y赋值为min/max（限制可活动空间）
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);

        transform.position = pos;           //将当前坐标赋值为pos(前面重置过的坐标)
    }
}