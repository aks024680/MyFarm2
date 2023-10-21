using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cowEating : MonoBehaviour
{
    // Start is called before the first frame update
    // 玩家进入
    public bool playerEnter = false;
    // 奶牛进入
    public bool cowEnter = false;
    // 剩余饲料数量
    public int currentEatCount = 100;
    // 
    public int maxEatCount = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerEnterMethod();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            playerEnter = true;
        }
        if (collision.tag == "cow")
        {
            cowEnter = true;
            // 是否有饲料
            if (currentEatCount!=0) {
                collision.gameObject.GetComponent<cowControler>().currentHunger = 100;
                currentEatCount--;
            }
            
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerEnter = false;
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        if (collision.tag == "cow")
        {
            cowEnter = false;
        }
    }

    // 玩家进入
    public void playerEnterMethod() {
        gameObject.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = currentEatCount + "/100";
        if (playerEnter== true) {
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            // 是否按E
            if (Input.GetKeyDown(KeyCode.E)) {
                // 判断是否有装备饲料
                // 文本框
                GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
                // 是否有装备物品
                GameObject equip = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).GetChild(19).gameObject;
                if (equip.GetComponent<Image>().sprite == null || equip.GetComponent<Image>().sprite.name != "饲料")
                {
                    dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "你未装备饲料!" };
                    dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
                else {
                    if (currentEatCount == maxEatCount)
                    {
                        dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "饲料已满,不需要补充!" };
                        dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    }
                    else {
                        currentEatCount = 100;
                        dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "已添加饲料!" };
                        dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        // 销毁或减少背包使用的饲料物品
                    }
                }
            }
        }
    }
}
