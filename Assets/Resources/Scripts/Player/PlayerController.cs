using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{

    // ��ȡ��ɫ�ø���
    private Rigidbody2D playerRid;
    // ʵ������ά�������ս�ɫ����
    public Vector2 playerVector2 = new Vector2();
    // ��ȡ�������
    private Animator playerAnimator;


    // �����ٶ�
    public float speed = 200;
    // ��������ٶ�
    public float baseSpeed;
    // ����������ֵ
    public float maxHealth = 100;
    // ��ǰ����ֵ
    public float currentHealth = 100;
    // ������
    public float maxHunger = 100;
    // ��ǰ������
    public float currentHunger = 100;
    // ����ֵ
    public float maxEndurance = 100;
    public float currentEndurance = 100;
    // ���״̬ // 1����  2����  3����   4�Ҹ�  5����
    public int playerState = 1;
    // ��ҵȼ�
    public int level = 1;
    // ����ȼ�
    public int fishingLevel = 1;
    // ��ֳ�ȼ�
    public int farmingLevel = 1;
    // ����ȼ�
    public int fieldLevel = 1;
    // �ڿ�ȼ�
    public int digLevel = 1;

    // �����������������
    // --------------------------------------//
    // ������������ȼ�
    public int maxLevel = 20;
    // ������������ȼ�
    public int maxFishingLevel = 30;
    // ��ֳ���������ȼ�
    public int maxFarmingLevel = 30;
    // ������������ȼ�
    public int maxFieldLevel = 30;
    // �ڿ���������ȼ�
    public int maxDigLevel = 30;
    //��ǰ���ﾭ��
    public int currentPlayerExp = 0;
    // ����������Ҫ�ľ���
    public int maxPlayerExp = 0;
    // ��ǰ���㾭��
    public int currentFishExp = 0;
    // ��������ȼ���Ҫ�ľ���
    public int maxFishExp = 0;
    // ��ǰ��ֳ����
    public int currentFarmExp = 0;
    // ��������ȼ���Ҫ�ľ���
    public int maxFarmExp = 0;
    // ��ǰ���ﾭ��
    public int currentFieldExp = 0;
    // ��������ȼ���Ҫ����
    public int maxFieldExp = 0;
    // ��ǰ�ڿ���
    public int currentDigExp = 0;
    // �����ڿ�ȼ���Ҫ�ľ���
    public int maxDigExp = 0;
    // --------------------------------------//

    // ��������ֵ�仯�ٶ�
    private float totalEnduranceTime = 0;
    // ����ֵ����ʱ����
    public float enduranceReduceTime;
    // ����ֵ�ָ�ʱ����
    public float enduranceRecoverTime;
    // ��������ֵ�任�ٶ�
    private float totalHealthTime = 0;
    // ���Ƽ����ȱ任ʱ��
    private float totalHungerTime =0;

    // ����uiϵͳ�������ʾ
    private bool showUiSystem = false;
    // ��ȡuiϵͳԤ����
    private GameObject uiSystemPrefab;
    // ---------------------------------
    // ��ȡnpc
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
        // ϵͳ�Ĵ���ر�
        openAndCloseUIsystem();
        // ��������ϵ��
         ExpProgress();
        // ���������ȼ�����������
        PlayerValueWithLevel();
    }

    private void FixedUpdate()
    {
        // ��ɫ�ƶ�
        playerMove();
        // �ָ��ͼ�������
        recoverAndReduceEndurance();
        // ���¼�����
        UpdateHunger();
        // ��������ֵ
        UpdateHealth();
        // �رձ���������ʾ
        //CloseBagFullMessage();
        CloseBagFull();

    }

    // ϵͳ�Ĵ򿪺͹ر�
    void openAndCloseUIsystem() {
        // ������Esc���Ҵ��������û�д򿪲�����Ϸ����û�д�
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameObject UISystem = GameObject.FindGameObjectWithTag("mainUI");
            showUiSystem = !showUiSystem;
            // �򿪺͹ر�ϵͳ
            UISystem.transform.GetChild(0).gameObject.SetActive(showUiSystem);
            // ��ϵͳ�򿪵�ʱ��رյ�����ʾ
            // ���Ƶ���ͼ����ʾ��ʾ������
             FishingMessageControll();
            //FishMessageControllTest();
        }
    }

    // �ƶ�����
    void playerMove() {
        // ��ȡˮƽ������������
        playerVector2.x = Input.GetAxisRaw("Horizontal");
        // ��ȡ��ֱ������������
        playerVector2.y = Input.GetAxisRaw("Vertical");
        // ��׼������
        playerVector2.Normalize();
        // ��������ֵ������
        playerRid.velocity = speed * playerVector2 * Time.fixedDeltaTime;
        // ����
        UpMove();

    }
    // ����
    void UpMove()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {

                // �ж� ��ǰ�����Ƿ�С�ڵ���10
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

    // �����ָ��ͼ���
    void recoverAndReduceEndurance() {
        // ���ûָ�ʱ��
        totalEnduranceTime += Time.fixedDeltaTime;
        // �ж��Ƿ��ڼ���״̬
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (currentEndurance > 0) {
                    // ԭ����>=0.1
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
            // ԭ��:>=0.3
            if (totalEnduranceTime >= enduranceRecoverTime)
            {
                currentEndurance += 1;
                totalEnduranceTime = 0;
            }
        }


    }

    // ���¼�����
    void UpdateHunger() {
        // 24���� / 3    480��
        totalHungerTime += Time.fixedDeltaTime;
        if (totalHungerTime >= 4.8) {
            if (currentHunger > 0) {
                currentHunger -= 1;
                
            }
            totalHungerTime = 0;
        }
    }

    // ��������ֵ
    void UpdateHealth() {
        totalHealthTime += Time.fixedDeltaTime;
        if (totalHealthTime >= 1) {
            if (currentHealth > 0)
            {
                // �����Ȼ᲻��=0
                if (currentHunger == 0) {
                    currentHealth -= 1;
                }
            }
            else
            {
                // ���뵽ҽԺ
            }
            totalHealthTime = 0;
        }
    }
    // �任����
    void ChangePlayerAnim() {
        // ��
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
        // ��
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

        // ��
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
        // ��
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

    // �رձ���������ʾ
    public void CloseBagFullMessage() {
        // ������E���ұ���������ʾ����ʾ��״̬��
        // ��ȡ�����������
        GameObject bagFull = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(1).gameObject;
        
        if (Input.GetKey(KeyCode.E) && bagFull.activeSelf == true) {
            // ���ùرձ���������ʾ
            bagFull.SetActive(false);
        }
    }


    //�رձ��������ı���ʾ
    public void CloseBagFull() {
        // E���ұ���������ʾ����ʾ״̬
        // ��ȡ������������ʾ
        GameObject bagFullTest = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(2).gameObject;
        if (Input.GetKey(KeyCode.E) && bagFullTest.activeSelf == true) {
            bagFullTest.SetActive(false);
        }
    }

    // ���Ƶ�����ʾͼ�����غ���ʾ
    public void FishingMessageControll() {
        if (SceneManager.GetActiveScene().name != "fishingMap") {
            return;
        }
        // �жϼ���
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

        // ֻ�����￿�������ʱִ��
        if (goFishing.OpenFishing == true)
        {
            // ��ȡ��������
            GameObject Player = GameObject.FindGameObjectWithTag("Player");

            if (showUiSystem == true)
            {
                // �ر�������ϵĵ������
                Player.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                // �رյ���ű�
                goFishing.enabled = false;
            }
            else
            {
                Player.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                goFishing.enabled = true;
            }
        }
    }

    // ���þ���������ϵ
    public void ExpProgress() {
        // ���ﾭ��
        maxPlayerExp = 10 + level * (level - 1) * level * 5;
        // ���㾭��
        maxFishExp = 10 + level * (level - 1) * (level + 1);
        // ��ֳ����
        maxFarmExp = 10 + level * (level - 1) * (level + 1);
        // ���ﾭ��
        maxFieldExp = 10 + level * (level - 1) * (level + 1);
        // �ڿ���
        maxDigExp = 10 + level * (level - 1) * (level + 1);


        /// 
        // ������������
        if (currentPlayerExp >= maxPlayerExp) {
            // �жϵ�ǰ�����Ƿ���������
            if (currentPlayerExp > maxPlayerExp)
            {
                // ��ǰ������ڵ�ǰ�����ȥ�����
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
                // �ж��Ƿ����ȼ�
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
        // ��������
        if (currentFishExp >= maxFishExp)
        {
            // �жϵ�ǰ�����Ƿ���������
            if (currentFishExp > maxFishExp)
            {
                // ��ǰ������ڵ�ǰ�����ȥ�����
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
                // �ж��Ƿ����ȼ�
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

        // ��ֳ����
        if (currentFarmExp >= maxFarmExp)
        {
            // �жϵ�ǰ�����Ƿ���������
            if (currentFarmExp > maxFarmExp)
            {
                // ��ǰ������ڵ�ǰ�����ȥ�����
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
                // �ж��Ƿ����ȼ�
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
        // ��������
        if (currentFieldExp >= maxFieldExp)
        {
            // �жϵ�ǰ�����Ƿ���������
            if (currentFieldExp > maxFieldExp)
            {
                // ��ǰ������ڵ�ǰ�����ȥ�����
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
                // �ж��Ƿ����ȼ�
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
        // �ڿ�����
        if (currentDigExp >= maxDigExp)
        {
            // �жϵ�ǰ�����Ƿ���������
            if (currentDigExp > maxDigExp)
            {
                // ��ǰ������ڵ�ǰ�����ȥ�����
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
                // �ж��Ƿ����ȼ�
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

    // �������������ȼ��������仯
    public void PlayerValueWithLevel() {
        // ��������ٶ���ȼ�����������
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
            // ����ֵ
            maxHealth = 100 + level * 5;
            // ������
            maxHunger = 100 + level * 2;
            // ����ֵ�����ٶ�
            enduranceReduceTime = (float)0.1 + level * ((float)0.005);
            // ����ֵ�ָ��ٶ�
            enduranceRecoverTime = (float)0.5 - level * ((float)0.008);
           

        }

    }


}
