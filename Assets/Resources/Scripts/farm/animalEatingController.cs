using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animalEatingController : MonoBehaviour
{
    // Start is called before the first frame update
    // 判断是否在喂食区
    public bool isPlayerInEatingArea = false;
    // 动物进入喂食区
    public bool isAnimalInEatingArea = false;
    // 判断对话框是否打开
    public bool showDialog = true;
    // 饲料数量
    public int count = 0;
    // 停留时间
    public float totalTime = 0;
    // 限制是否可以吃饲料
    public bool canEating = true;
    // 恢复可吃饲料
    public float eatingTotalTime = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 对话框
        ChangeData();
        // 玩家操作的方法
        PlayerMethod();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 玩家进入的时候
        if (collision.tag == "Player") {
            isPlayerInEatingArea = true;
        }

        if (collision.tag == "animal")
        {
            isAnimalInEatingArea = true;
            // 判断是否有饲料
            if (count !=0) {
                // 判断动物的饥饿度是否等于0
                if (collision.gameObject.GetComponent<chickenController>().hunger ==0) {
                    // 有饲料就恢复动物饥饿度
                    collision.gameObject.GetComponent<chickenController>().hunger = 100;
                    count --;
                }
                
            }
            
        }

        // 玩家进入显示面板
        if (isPlayerInEatingArea == true) {
            gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = count.ToString() + "/100";
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            isPlayerInEatingArea = false;
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }

        if (collision.tag == "animal") {
            isAnimalInEatingArea = false;
        }
        
    }

    // 变更对话框变量
    public void ChangeData() {
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        if (dialog.transform.GetChild(0).GetChild(0).gameObject.activeSelf == false)
        {
            showDialog = true;
        }
        else {
            showDialog = false;
        }
        // 更新饲料数量
        gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>().text = count.ToString() + "/100";
    }

    // 编写玩家操作的方法
    public void PlayerMethod() {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInEatingArea == true && showDialog == true) {
            // 判断是否装备饲料
            GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
            // 是否有装备物品
            GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
            if (equip.GetComponent<Image>().sprite == null || equip.GetComponent<Image>().sprite.name != "饲料")
            {
                dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "你未装备饲料!" };
                dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
            else {

                // 判断饲料是否到100(满值)
                if (count == 100)
                {
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "饲料已满!" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    count = 100;
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "已补充饲料!" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    // 为了方便，不进行操作背包物品
                }
            }
        }
    }
}
