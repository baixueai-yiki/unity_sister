using UnityEngine;



public class PlayerAnimation : MonoBehaviour
{
    public PlayerController controller;                 //引用：PlayerController玩家控制脚本
    public PlayerStateMachine PSM;                      //引用：PlayerStateMachine玩家输入状态
    public Animator animator;                           // 声明Animator动画播放组件
    private float Facing = 1f;                      // 声明lastFacing变量用于临时判断，阻止重复赋值
    
    private void Awake()//场景加载时调用的函数
    {
        controller = GetComponent<PlayerController>();  // 获取自身对象的 PlayerController 组件并赋值给 controller
        //stats = GetComponent<PlayerStats>();            // 获取自身对象的 PlayerStats 组件并赋值给 stats
        //rest = GetComponent<PlayerRest>();              // 获取自身对象的 PlayerRest 组件并赋值给 rest
        PSM = GetComponent<PlayerStateMachine>();       // 获取自身对象的 PlayerControlState 组件并赋值给 PSM
        //rb2d = GetComponent<Rigidbody2D>();             // 获取自身对象的 Rigidbody2D 组件并赋值给 rb2d
        animator = GetComponentInChildren<Animator>();  // 获取自身及子对象的 Animator 组件并赋值给 animator
    }

    void Update()//每一帧调用的函数
    {
        UpdateFacing(PSM.Facing);    //将精灵的朝向设置为x轴的移动向量
        UpdateAnimator(PSM.CurrentState);  //
    }
    void UpdateFacing(float moveFacing)
    {
        if (moveFacing == 0 || moveFacing == Facing)
            return;
        Facing = moveFacing;
    //变量animator.三种参数结合.缩放参数=创建 三维坐标（面朝方向，1，1）
        animator.transform.localScale = new Vector3(Facing, 1, 1);
    }
    void UpdateAnimator(PlayerStateMachine.PlayerState CurrentState)
    {
        switch (CurrentState)
        {
            case PlayerStateMachine.PlayerState.Idle:
                animator.SetBool("isMoving", false);
                break;

            case PlayerStateMachine.PlayerState.Walk:
                animator.SetBool("isMoving", true);
                break;
        }
    }
}
