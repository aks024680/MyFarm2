using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playertest : MonoBehaviour
{
    //速度
    public float speed = 100.0f;
    //2d鋼體
    private Rigidbody2D rb;
    //去接收我們輸入的屬性直
    Vector2 vector2 = new Vector2();
    private Animator animator;
    //生命值
    public float maxHealth = 100;
    //當前生命值
    public float currentHealth = 100;
    //飢餓度
    public float maxHunger = 100;
    //當前飢餓度
    public float currentHunger = 100;
    //耐力值
    public float maxEndurance = 100;
    //當前耐力值
    public float currentEndurance = 100;
    //玩家 1正常 2生病 3幸福 4悲傷
    public int playerState = 1;
    private float totalCoverTime = 0;

    //玩家等級
    public int level = 1;
    //釣魚等級
    public int fishingLevel = 1;
    //養殖等級
    public int farmingLevel = 1;
    //種田等級
    public int fieldLevel = 1;
    //挖礦等級
    public int digLevel = 1 ;

    //需要獲取到預置體
    private GameObject UITestPrefab;

    //獲取加載后的預置體
    private GameObject UITest;

    private bool uiControll = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        UITestPrefab = Resources.Load<GameObject>("Prefabs/UI/Board/UITest");
        Instantiate(UITestPrefab);
        
        UITest = GameObject.FindGameObjectWithTag("uiSystem");
       

    }

    // Update is called once per frame
    void Update()
    {
        UIController();
        ChangeAnim();
    }
    private void FixedUpdate()
    {
        //角色移動
        Move();
        //更新飢餓度
        UpdateHunger();
        //更新生命值
        UpdateHealth();
        //恢復和減少耐力
        ReduceAndRecoverEndurance();
    }
    void MoveUp()
    {
        //移動狀態
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if(currentEndurance < 10)
                {
                    speed = 100f;
                    animator.speed = 1;
                }
                else
                {
                    speed = 300f;
                    animator.speed = 3;
                }
            }
            else
            {
                speed = 100f;
                animator.speed = 1;
            }
        }
        ReduceAndRecoverEndurance();

    }

    void UIController()
    {
        //
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiControll = !uiControll;
            UITest.transform.GetChild(0).gameObject.SetActive(uiControll);
        }
    }
    //恢復和減少耐力值屬性
    void ReduceAndRecoverEndurance()
    {
        totalCoverTime += Time.fixedDeltaTime;
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (currentEndurance > 0)
                {
                    if(totalCoverTime >= 0.1f) { 
                    currentEndurance -= 1;
                    totalCoverTime = 0;
                    }
                }
            }
        }
        if (currentEndurance < maxEndurance)
        {
            if(totalCoverTime >= 0.3f) { 
            currentEndurance += 1;
                totalCoverTime = 0;        
            }
        }
    }
    //更新飢餓度
    void UpdateHunger()
    {
        
        //24分鐘/3  480秒
        totalCoverTime += Time.fixedDeltaTime;
        if(totalCoverTime >= 1)
        {
            
            if (currentHunger > 0)
            {
                
                currentHunger -= 0.1f;
                
            }
        }
    }
    //更新生命值
    void UpdateHealth()
    {
        
        totalCoverTime += Time.fixedDeltaTime;
        if (totalCoverTime >= 1 )
        {
            
            if ( currentHealth > 0)
            {
                
                //飢餓度是不是等於0
                if (currentHunger <= 0)
                {
                    currentHealth -= 0.1f;
                    
                }
            }
            else
            {
                //進入醫院
            }
        }
    }
    void Move()
        {
            vector2.x = Input.GetAxisRaw("Horizontal");
            vector2.y = Input.GetAxisRaw("Vertical");

            //移動
            rb.velocity = speed * vector2 * Time.fixedDeltaTime;
            MoveUp();
            
        }
        //更新動畫
        void ChangeAnim()
        {
            //獲取鍵盤輸入
            //變更動畫條件
            //上
            if (Input.GetKeyDown(KeyCode.W))
            {
                animator.SetBool("uping", true);
                animator.SetBool("downing", false);
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("Idle", false);
                animator.SetBool("faceup", false);
                animator.SetBool("facedown", false);
                animator.SetBool("faceleft", false);
                animator.SetBool("faceright", false);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("Idle", true);
                animator.SetBool("faceup", true);
                animator.SetBool("facedown", false);
                animator.SetBool("faceleft", false);
                animator.SetBool("faceright", false);
            }
            //下
            if (Input.GetKeyDown(KeyCode.S))
            {
                animator.SetBool("uping", false);
                animator.SetBool("downing", true);
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("Idle", false);
                animator.SetBool("faceup", false);
                animator.SetBool("facedown", false);
                animator.SetBool("faceleft", false);
                animator.SetBool("faceright", false);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("Idle", true);
                animator.SetBool("faceup", false);
                animator.SetBool("facedown", true);
                animator.SetBool("faceleft", false);
                animator.SetBool("faceright", false);
            }
            //左
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("lefting", true);
                animator.SetBool("righting", false);
                animator.SetBool("Idle", false);
                animator.SetBool("faceup", false);
                animator.SetBool("facedown", false);
                animator.SetBool("faceleft", false);
                animator.SetBool("faceright", false);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("Idle", true);
                animator.SetBool("faceup", false);
                animator.SetBool("facedown", false);
                animator.SetBool("faceleft", true);
                animator.SetBool("faceright", false);
            }
            //右
            if (Input.GetKeyDown(KeyCode.D))
            {
                print("按下D");
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("lefting", false);
                animator.SetBool("righting", true);
                animator.SetBool("Idle", false);
                animator.SetBool("faceup", false);
                animator.SetBool("facedown", false);
                animator.SetBool("faceleft", false);
                animator.SetBool("faceright", false);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                animator.SetBool("uping", false);
                animator.SetBool("downing", false);
                animator.SetBool("lefting", false);
                animator.SetBool("righting", false);
                animator.SetBool("Idle", true);
                animator.SetBool("faceup", false);
                animator.SetBool("facedown", false);
                animator.SetBool("faceleft", false);
                animator.SetBool("faceright", true);
            }
        }
    }

