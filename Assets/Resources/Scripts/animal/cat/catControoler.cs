using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catControoler : MonoBehaviour
{
    // 移动速度
    public float speed = 120;
    // 刚体
    private Rigidbody2D rigidbody;
    // 向量
    private Vector2 vector2;
    // 变换方向的持续时间
    public float continueTime = (float)1.8;
    // 
    private float totalTime;
    // 判断方向
    public float x = 0;
    public float y = 1;
    // 变换方向下标
    private int index = 0;
    // 动画
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAnim();
    }

    private void FixedUpdate()
    {
        // 自动移动
        AutoMove();
    }

    // 移动方法 // 自动移动
    public void AutoMove()
    {
        // Input.GetAxisRaw("Vertical")
        // Input.GetAxisRaw("Horizontal")
        // 加总时间
        totalTime += Time.fixedDeltaTime;
        // 设置向量
        vector2.x = x;
        vector2.y = y;
        vector2.Normalize();
        // 让物体移动
        rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
        if (totalTime >= continueTime)
        {
            // x为-1是往左，x为1是往右
            // y为-1是往下，y为1是往上
            switch (index)
            {
                case 0:
                    // 向左
                    x = -1;
                    y = 0;
                    index = 1;
                    break;
                case 1:
                    // 向下
                    x = 0;
                    y = -1;
                    index = 2;
                    break;
                case 2:
                    // 向右
                    x = 1;
                    y = 0;
                    index = 3;
                    break;
                case 3:
                    // 向上
                    x = 0;
                    y = 1;
                    index = 0;
                    break;
            }
            totalTime = 0;
        }
    }
    // 变更动画
    public void ChangeAnim()
    {
        // 向上
        if (index == 0)
        {
            animator.SetBool("lefting", false);
            animator.SetBool("righting", false);
            animator.SetBool("uping", true);
            animator.SetBool("downing", false);
        }
        // 向左
        if (index == 1)
        {
            animator.SetBool("lefting", true);
            animator.SetBool("righting", false);
            animator.SetBool("uping", false);
            animator.SetBool("downing", false);
        }
        // 向下
        if (index == 2)
        {
            animator.SetBool("lefting", false);
            animator.SetBool("righting", false);
            animator.SetBool("uping", false);
            animator.SetBool("downing", true);
        }
        // 向右
        if (index == 3)
        {
            animator.SetBool("lefting", false);
            animator.SetBool("righting", true);
            animator.SetBool("uping", false);
            animator.SetBool("downing", false);
        }
    }
}
