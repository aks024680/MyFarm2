using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    // 获取角色得刚体
    private Rigidbody2D playerRid;
    // 实例化二维向量接收角色坐标
    public Vector2 playerVector2 = new Vector2();
    // 获取动画组件
    private Animator playerAnimator;


    // 定义速度
    public float speed = 200;
    // 定义基础速度
    public float baseSpeed;
    // 玩家最大生命值
    public float maxHealth = 100;
    // 当前生命值
    public float currentHealth = 100;
    // 饥饿度
    public float maxHunger = 100;
    // 当前饥饿度
    public float currentHunger = 100;
    // 耐力值
    public float maxEndurance = 100;
    public float currentEndurance = 100;
    // 玩家状态 // 1正常  2生病  3受伤   4幸福  5悲伤
    public int playerState = 1;
    // 玩家等级
    public int level = 1;
    // 钓鱼等级
    public int fishingLevel = 1;
    // 养殖等级
    public int farmingLevel = 1;
    // 种田等级
    public int fieldLevel = 1;
    // 挖矿等级
    public int digLevel = 1;

    // 加上升级经验的属性
    // --------------------------------------//
    // 人物可升级最大等级
    public int maxLevel = 20;
    // 钓鱼可升级最大等级
    public int maxFishingLevel = 30;
    // 养殖可升级最大等级
    public int maxFarmingLevel = 30;
    // 种田可升级最大等级
    public int maxFieldLevel = 30;
    // 挖矿可升级最大等级
    public int maxDigLevel = 30;
    //当前人物经验
    public int currentPlayerExp = 0;
    // 升级人物需要的经验
    public int maxPlayerExp = 0;
    // 当前钓鱼经验
    public int currentFishExp = 0;
    // 升级钓鱼等级需要的经验
    public int maxFishExp = 0;
    // 当前养殖经验
    public int currentFarmExp = 0;
    // 升级钓鱼等级需要的经验
    public int maxFarmExp = 0;
    // 当前种田经验
    public int currentFieldExp = 0;
    // 升级种田等级需要经验
    public int maxFieldExp = 0;
    // 当前挖矿经验
    public int currentDigExp = 0;
    // 升级挖矿等级需要的经验
    public int maxDigExp = 0;
    // --------------------------------------//

    // 控制耐力值变化速度
    private float totalEnduranceTime = 0;
    // 耐力值消耗时间间隔
    public float enduranceReduceTime;
    // 耐力值恢复时间间隔
    public float enduranceRecoverTime;
    // 控制生命值变换速度
    private float totalHealthTime = 0;
    // 控制饥饿度变换时间
    private float totalHungerTime =0;

    // 控制ui系统界面的显示
    private bool showUiSystem = false;
    // 获取ui系统预制体
    private GameObject uiSystemPrefab;
    // ---------------------------------
    // 获取npc
    private GameObject npc;
    void Start()
    {
        playerRid = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        uiSystemPrefab = Resources.Load<GameObject>("Prefabs/ui/uiSystem");
        GameObject uiSystem = GameObject.FindGameObjectWithTag("uiSystem").transform.gameObject;
        if (!uiSystem) {
            Instantiate(uiSystemPrefab);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //ChangePlayerAnim();
        ChangePlayerAnim();
        // 系统的打开与关闭
        openAndCloseUIsystem();
        // 升级经验系数
         ExpProgress();
        // 玩家属性随等级提升而提升
        PlayerValueWithLevel();
    }

    private void FixedUpdate()
    {
        // 角色移动
        playerMove();
        // 恢复和减少耐力
        recoverAndReduceEndurance();
        // 更新饥饿度
        UpdateHunger();
        // 更新生命值
        UpdateHealth();
        // 关闭背包已满提示
        //CloseBagFullMessage();
        CloseBagFull();

    }

    // 系统的打开和关闭
    void openAndCloseUIsystem() {
        // 当按下Esc并且存读档界面没有打开并且游戏帮助没有打开
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameObject UISystem = GameObject.FindGameObjectWithTag("mainUI");
            showUiSystem = !showUiSystem;
            // 打开和关闭系统
            UISystem.transform.GetChild(0).gameObject.SetActive(showUiSystem);
            // 当系统打开的时候关闭钓鱼提示
            // 控制钓鱼图标提示显示和隐藏
             FishingMessageControll();
            //FishMessageControllTest();
        }
    }

    // 移动方法
    void playerMove() {
        // 获取水平方向输入坐标
        playerVector2.x = Input.GetAxisRaw("Horizontal");
        // 获取垂直方向输入坐标
        playerVector2.y = Input.GetAxisRaw("Vertical");
        // 标准化坐标
        playerVector2.Normalize();
        // 将向量赋值给刚体
        playerRid.velocity = speed * playerVector2 * Time.fixedDeltaTime;
        // 加速
        UpMove();

    }
    // 加速
    void UpMove()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {

                // 判断 当前耐力是否小于等于10
                if (currentEndurance <= 10) {
                    speed = baseSpeed;
                    playerAnimator.speed = 1;
                } else {
                    speed = baseSpeed + 200;
                    playerAnimator.speed = 2;
                }
            }
            else
            {
                speed = baseSpeed;
                playerAnimator.speed = 1;
            }
        }
    }

    // 耐力恢复和减少
    void recoverAndReduceEndurance() {
        // 设置恢复时间
        totalEnduranceTime += Time.fixedDeltaTime;
        // 判断是否在加速状态
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (currentEndurance > 0) {
                    // 原本：>=0.1
                    if (totalEnduranceTime >= enduranceReduceTime)
                    {
                        currentEndurance -= 1;
                        totalEnduranceTime = 0;
                    }
                }
            }
        }
        if (currentEndurance < maxEndurance)
        {
            // 原本:>=0.3
            if (totalEnduranceTime >= enduranceRecoverTime)
            {
                currentEndurance += 1;
                totalEnduranceTime = 0;
            }
        }


    }

    // 更新饥饿度
    void UpdateHunger() {
        // 24分钟 / 3    480秒
        totalHungerTime += Time.fixedDeltaTime;
        if (totalHungerTime >= 4.8) {
            if (currentHunger > 0) {
                currentHunger -= 1;
                
            }
            totalHungerTime = 0;
        }
    }

    // 更新生命值
    void UpdateHealth() {
        totalHealthTime += Time.fixedDeltaTime;
        if (totalHealthTime >= 1) {
            if (currentHealth > 0)
            {
                // 饥饿度会不会=0
                if (currentHunger == 0) {
                    currentHealth -= 1;
                }
            }
            else
            {
                // 进入到医院
            }
            totalHealthTime = 0;
        }
    }
    // 变换动画
    void ChangePlayerAnim() {
        // 左
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            playerAnimator.SetBool("idle",false);
            playerAnimator.SetBool("lefting",true);
            playerAnimator.SetBool("righting",false);
            playerAnimator.SetBool("uping",false);
            playerAnimator.SetBool("downing",false);
            playerAnimator.SetBool("faceLeft",false);
            playerAnimator.SetBool("faceRight",false);
            playerAnimator.SetBool("faceUp",false);
            playerAnimator.SetBool("faceDown",false);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) {
            playerAnimator.SetBool("idle", false);
            playerAnimator.SetBool("lefting", false) ;
            playerAnimator.SetBool("righting", false);
            playerAnimator.SetBool("uping", false);
            playerAnimator.SetBool("downing", false);
            playerAnimator.SetBool("faceLeft", true);
            playerAnimator.SetBool("faceRight", false);
            playerAnimator.SetBool("faceUp", false);
            playerAnimator.SetBool("faceDown", false);
        }
        // 上
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerAnimator.SetBool("idle", false);
            playerAnimator.SetBool("lefting", false);
            playerAnimator.SetBool("righting", false);
            playerAnimator.SetBool("uping", true);
            playerAnimator.SetBool("downing", false);
            playerAnimator.SetBool("faceLeft", false);
            playerAnimator.SetBool("faceRight", false);
            playerAnimator.SetBool("faceUp", false);
            playerAnimator.SetBool("faceDown", false);
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            playerAnimator.SetBool("idle", false);
            playerAnimator.SetBool("lefting", false);
            playerAnimator.SetBool("righting", false);
            playerAnimator.SetBool("uping", false);
            playerAnimator.SetBool("downing", false);
            playerAnimator.SetBool("faceLeft", false);
            playerAnimator.SetBool("faceRight", false);
            playerAnimator.SetBool("faceUp", true);
            playerAnimator.SetBool("faceDown", false);
        }

        // 右
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerAnimator.SetBool("idle", false);
            playerAnimator.SetBool("lefting", false);
            playerAnimator.SetBool("righting", true);
            playerAnimator.SetBool("uping", false);
            playerAnimator.SetBool("downing", false);
            playerAnimator.SetBool("faceLeft", false);
            playerAnimator.SetBool("faceRight", false);
            playerAnimator.SetBool("faceUp", false);
            playerAnimator.SetBool("faceDown", false);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerAnimator.SetBool("idle", false);
            playerAnimator.SetBool("lefting", false);
            playerAnimator.SetBool("righting", false);
            playerAnimator.SetBool("uping", false);
            playerAnimator.SetBool("downing", false);
            playerAnimator.SetBool("faceLeft", false);
            playerAnimator.SetBool("faceRight", true);
            playerAnimator.SetBool("faceUp", false);
            playerAnimator.SetBool("faceDown", false);
        }
        // 下
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerAnimator.SetBool("idle", false);
            playerAnimator.SetBool("lefting", false);
            playerAnimator.SetBool("righting", false);
            playerAnimator.SetBool("uping", false);
            playerAnimator.SetBool("downing", true);
            playerAnimator.SetBool("faceLeft", false);
            playerAnimator.SetBool("faceRight", false);
            playerAnimator.SetBool("faceUp", false);
            playerAnimator.SetBool("faceDown", false);
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            playerAnimator.SetBool("idle", false);
            playerAnimator.SetBool("lefting", false);
            playerAnimator.SetBool("righting", false);
            playerAnimator.SetBool("uping", false);
            playerAnimator.SetBool("downing", false);
            playerAnimator.SetBool("faceLeft", false);
            playerAnimator.SetBool("faceRight", false);
            playerAnimator.SetBool("faceUp", false);
            playerAnimator.SetBool("faceDown", true);
        }

    }

    // 关闭背包已满提示
    public void CloseBagFullMessage() {
        // 当按下E并且背包已满提示是显示的状态下
        // 获取背包已满组件
        GameObject bagFull = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(1).gameObject;
        
        if (Input.GetKey(KeyCode.E) && bagFull.activeSelf == true) {
            // 设置关闭背包已满提示
            bagFull.SetActive(false);
        }
    }


    //关闭背包已满文本提示
    public void CloseBagFull() {
        // E并且背包已满提示是显示状态
        // 获取到背包已满提示
        GameObject bagFullTest = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject;
        if (Input.GetKey(KeyCode.E) && bagFullTest.activeSelf == true) {
            bagFullTest.SetActive(false);
        }
    }

    // 控制钓鱼提示图标隐藏和显示
    public void FishingMessageControll() {
        if (SceneManager.GetActiveScene().name != "fishingMap") {
            return;
        }
        // 判断季节
        TimeSystemContoller timeSystemContoller = GameObject.FindGameObjectWithTag("uiSystem").transform.GetChild(0).GetComponent<TimeSystemContoller>();
        GoFishing goFishing = null;
        if (timeSystemContoller.seasonTime == 1)
        {
            goFishing = GameObject.FindGameObjectWithTag("springMap").transform.GetChild(4).GetComponent<GoFishing>();
        }
        else if (timeSystemContoller.seasonTime == 2)
        {
            goFishing = GameObject.FindGameObjectWithTag("summerMap").transform.GetChild(4).GetComponent<GoFishing>();
        }
        else if (timeSystemContoller.seasonTime == 3)
        {
            goFishing = GameObject.FindGameObjectWithTag("fallMap").transform.GetChild(4).GetComponent<GoFishing>();
        }
        else {
            goFishing = GameObject.FindGameObjectWithTag("winterMap").transform.GetChild(4).GetComponent<GoFishing>();
        }

        // 只在人物靠近钓鱼点时执行
        if (goFishing.OpenFishing == true)
        {
            // 获取主角物体
            GameObject Player = GameObject.FindGameObjectWithTag("Player");

            if (showUiSystem == true)
            {
                // 关闭玩家身上的钓鱼组件
                Player.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                // 关闭钓鱼脚本
                goFishing.enabled = false;
            }
            else
            {
                Player.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                goFishing.enabled = true;
            }
        }
    }

    // 设置经验升级体系
    public void ExpProgress() {
        // 人物经验
        maxPlayerExp = 10 + level * (level - 1) * level * 5;
        // 钓鱼经验
        maxFishExp = 10 + level * (level - 1) * (level + 1);
        // 养殖经验
        maxFarmExp = 10 + level * (level - 1) * (level + 1);
        // 种田经验
        maxFieldExp = 10 + level * (level - 1) * (level + 1);
        // 挖矿经验
        maxDigExp = 10 + level * (level - 1) * (level + 1);


        /// 
        // 进行人物升级
        if (currentPlayerExp >= maxPlayerExp) {
            // 判断当前经验是否大于最大经验
            if (currentPlayerExp > maxPlayerExp)
            {
                // 当前经验等于当前经验减去最大经验
                if (level < maxLevel)
                {
                    currentPlayerExp = currentPlayerExp - maxPlayerExp;
                    level++;
                }
                else {
                    currentPlayerExp = maxPlayerExp;
                }
            }
            else {
                // 判断是否到最大等级
                if (level < maxLevel)
                {
                    currentPlayerExp = 0;
                    level++;
                }
                else {
                    currentPlayerExp = maxPlayerExp;
                }
            }
        }
        // 钓鱼升级
        if (currentFishExp >= maxFishExp)
        {
            // 判断当前经验是否大于最大经验
            if (currentFishExp > maxFishExp)
            {
                // 当前经验等于当前经验减去最大经验
                if (fishingLevel < maxFishingLevel)
                {
                    currentFishExp = currentFishExp - maxFishExp;
                    fishingLevel++;
                }
                else
                {
                    currentFishExp = maxFishExp;
                }
            }
            else
            {
                // 判断是否到最大等级
                if (fishingLevel < maxFishingLevel)
                {
                    currentFishExp = 0;
                    fishingLevel++;
                }
                else
                {
                    currentFishExp = maxFishExp;
                }
            }
        }

        // 养殖升级
        if (currentFarmExp >= maxFarmExp)
        {
            // 判断当前经验是否大于最大经验
            if (currentFarmExp > maxFarmExp)
            {
                // 当前经验等于当前经验减去最大经验
                if (fishingLevel < maxFishingLevel)
                {
                    currentFarmExp = currentFarmExp - maxFarmExp;
                    farmingLevel++;
                }
                else
                {
                    currentFarmExp = maxFarmExp;
                }
            }
            else
            {
                // 判断是否到最大等级
                if (fishingLevel < maxFishingLevel)
                {
                    currentFarmExp = 0;
                    farmingLevel++;
                }
                else
                {
                    currentFarmExp = maxFarmExp;
                }
            }
        }
        // 种田升级
        if (currentFieldExp >= maxFieldExp)
        {
            // 判断当前经验是否大于最大经验
            if (currentFieldExp > maxFieldExp)
            {
                // 当前经验等于当前经验减去最大经验
                if (fieldLevel < maxFieldLevel)
                {
                    currentFieldExp = currentFieldExp - maxFieldExp;
                    fieldLevel++;
                }
                else
                {
                    currentFieldExp = maxFieldExp;
                }
            }
            else
            {
                // 判断是否到最大等级
                if (fishingLevel < maxFishingLevel)
                {
                    currentFieldExp = 0;
                    fieldLevel++;
                }
                else
                {
                    currentFieldExp = maxFieldExp;
                }
            }
        }
        // 挖矿升级
        if (currentDigExp >= maxDigExp)
        {
            // 判断当前经验是否大于最大经验
            if (currentDigExp > maxDigExp)
            {
                // 当前经验等于当前经验减去最大经验
                if (digLevel < maxDigLevel)
                {
                    currentDigExp = currentDigExp - maxDigExp;
                    digLevel++;
                }
                else
                {
                    currentDigExp = maxDigExp;
                }
            }
            else
            {
                // 判断是否到最大等级
                if (digLevel < maxDigLevel)
                {
                    currentDigExp = 0;
                    digLevel++;
                }
                else
                {
                    currentDigExp = maxDigExp;
                }
            }
        }
        ///
    }

    // 设置玩家属性随等级提升而变化
    public void PlayerValueWithLevel() {
        // 设置玩家速度随等级提升而提升
        if (level == 1)
        {
            baseSpeed = 200;
            maxHealth = 100;
            maxHunger = 100;
            enduranceReduceTime = (float)0.1;
            enduranceRecoverTime = (float)0.5;
        }
        else {
            baseSpeed = 200 + level * 2;
            // 生命值
            maxHealth = 100 + level * 5;
            // 饥饿度
            maxHunger = 100 + level * 2;
            // 耐力值减少速度
            enduranceReduceTime = (float)0.1 + level * ((float)0.005);
            // 耐力值恢复速度
            enduranceRecoverTime = (float)0.5 - level * ((float)0.008);
           

        }

    }


}
