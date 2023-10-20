using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catControoler : MonoBehaviour
{
    // �ƶ��ٶ�
    public float speed = 120;
    // ����
    private Rigidbody2D rigidbody;
    // ����
    private Vector2 vector2;
    // �任����ĳ���ʱ��
    public float continueTime = (float)1.8;
    // 
    private float totalTime;
    // �жϷ���
    public float x = 0;
    public float y = 1;
    // �任�����±�
    private int index = 0;
    // ����
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
        // �Զ��ƶ�
        AutoMove();
    }

    // �ƶ����� // �Զ��ƶ�
    public void AutoMove()
    {
        // Input.GetAxisRaw("Vertical")
        // Input.GetAxisRaw("Horizontal")
        // ����ʱ��
        totalTime += Time.fixedDeltaTime;
        // ��������
        vector2.x = x;
        vector2.y = y;
        vector2.Normalize();
        // �������ƶ�
        rigidbody.velocity = speed * vector2 * Time.fixedDeltaTime;
        if (totalTime >= continueTime)
        {
            // xΪ-1������xΪ1������
            // yΪ-1�����£�yΪ1������
            switch (index)
            {
                case 0:
                    // ����
                    x = -1;
                    y = 0;
                    index = 1;
                    break;
                case 1:
                    // ����
                    x = 0;
                    y = -1;
                    index = 2;
                    break;
                case 2:
                    // ����
                    x = 1;
                    y = 0;
                    index = 3;
                    break;
                case 3:
                    // ����
                    x = 0;
                    y = 1;
                    index = 0;
                    break;
            }
            totalTime = 0;
        }
    }
    // �������
    public void ChangeAnim()
    {
        // ����
        if (index == 0)
        {
            animator.SetBool("lefting", false);
            animator.SetBool("righting", false);
            animator.SetBool("uping", true);
            animator.SetBool("downing", false);
        }
        // ����
        if (index == 1)
        {
            animator.SetBool("lefting", true);
            animator.SetBool("righting", false);
            animator.SetBool("uping", false);
            animator.SetBool("downing", false);
        }
        // ����
        if (index == 2)
        {
            animator.SetBool("lefting", false);
            animator.SetBool("righting", false);
            animator.SetBool("uping", false);
            animator.SetBool("downing", true);
        }
        // ����
        if (index == 3)
        {
            animator.SetBool("lefting", false);
            animator.SetBool("righting", true);
            animator.SetBool("uping", false);
            animator.SetBool("downing", false);
        }
    }
}
