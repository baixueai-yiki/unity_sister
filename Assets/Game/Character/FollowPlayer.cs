using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;                       // 玩家
    public Vector3 offset = new Vector3(0, 2.5f, 0); // UI偏移

    private Camera mainCamera;
    private PlayerStateMachine PSM;
    private Vector3 originalScale;

    private void Awake()
    {
        mainCamera = Camera.main;//把世界里的坐标转换成屏幕上的像素位置
        PSM = player.GetComponent<PlayerStateMachine>();
        originalScale = transform.localScale;// 记录UI原本的缩放
    }

    private void LateUpdate()
    {
        if (player == null || PSM == null)
            return;

        // 玩家位置 + 偏移
        Vector3 targetPosition = player.position + offset;
        // 世界坐标转换为UI坐标
        transform.position = mainCamera.WorldToScreenPoint(targetPosition);


        // 根据玩家朝向翻转UI
        if (PSM.Facing < 0)
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }
        else
        {
            transform.localScale = new Vector3(
                Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }
    }
}