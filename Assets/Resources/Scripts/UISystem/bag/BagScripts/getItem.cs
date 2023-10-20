using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class getItem : MonoBehaviour
{
    // 本方法用于获取物品

    // 获取当前挂载脚本的物体
    private Transform thisObject;
    // 当前碰撞的物品数据
    private itemsCreations thisItem;
    // 添加到对应的背包
    private bagCreations playerBag;
    // 物品可以叠加的最大数量
    public static int maxCumulative = 10;


    void Start()
    {
        // 通过名称获取对应的物品
        thisObject = GetComponent<Transform>();
        if (thisObject.name.Contains("(") && thisObject.name.Contains(" ")) {
            string[] strArr = thisObject.name.Split(new char[] { ' ' });
            thisObject.name = strArr[0];
        }
        if (thisObject.name.Contains("(") && !thisObject.name.Contains(" ")) {
            string[] strArr = thisObject.name.Split(new char[] { '(' });
            thisObject.name = strArr[0];
        }

        thisItem = Resources.Load<itemsCreations>("Prefabs/bag/itemData/itemsCreation/" + thisObject.name);
        playerBag = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");

    }


    // 碰撞器检测，添加物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            AddNewItem(thisItem,playerBag);
        }
    }

    private void Update()
    {

    }


    // 添加新物品到物品数据
    public void AddNewItem(itemsCreations thisItem,bagCreations playerBag) {
        // 赋值图片
        //thisItem.itemImage = thisObject.GetComponent<Image>().sprite;
        // 背包没有该物品进行添加，有则添加数量
        // 如果有名称相同的
        int sameItemCount = 0;
        for (int i = 0; i < playerBag.bag.Count; i++)
        {
            if (thisItem.name == playerBag.bag[i].itemName)
            {
                sameItemCount++;
            }
        }
        if (sameItemCount==0)
        {

            // 在ui添加物品，判断背包格数是否满了
            if (playerBag.bag.Count < 10)
            {
                // 往背包添加物品
                playerBag.bag.Add(thisItem);
                //addToBag.CreateNewItem(thisItem);
                Destroy(thisObject.gameObject);
            }
            else {
                // 提示文本框背包已满
                print("背包已满");
                //showBagFullMessage();
                ShowBagFull();
            }

        }
        else
        {
            // 判断背包是否已满
            if (playerBag.bag.Count < 10)
            {
                // 判断是否可以叠加
                if (thisItem.cumulative)
                {
                    // 每10个物品一组 
                    if (thisItem.itemNumber % maxCumulative == 0)
                    {
                        // 累加数量已满
                        // 满了新增
                        playerBag.bag.Add(thisItem);
                        // 数量加1
                        thisItem.itemNumber += 1;
                        Destroy(thisObject.gameObject);
                    }
                    else {
                        thisItem.itemNumber += 1;
                        Destroy(thisObject.gameObject);
                    }
                }
                else {
                    // 如果不能叠加，新增
                    playerBag.bag.Add(thisItem);
                    Destroy(thisObject.gameObject);
                }
            }
            else {
                // 背包格满了就不能新增，判断是否可以累加
                if (thisItem.cumulative)
                {
                    // 每10个物品一组 
                    if (thisItem.itemNumber % maxCumulative == 0)
                    {
                        // 累加数量已满
                        // 满了提示背包已满
                        //playerBag.bag.Add(thisItem);
                        print("物品累加数量已满");
                        //showBagFullMessage();
                        ShowBagFull();
                    }
                    else
                    {
                        thisItem.itemNumber += 1;
                        Destroy(thisObject.gameObject);
                    }
                }
                else {
                    print("背包已满");
                    //showBagFullMessage();
                    ShowBagFull();
                }
            }
        }
        
        addToBag.RefreshItem();
    }

    // 添加钓鱼物品

    // 添加新物品到物品数据
    public int AddNewFishItem(itemsCreations thisItem, bagCreations playerBag)
    {
        // 定义变量判断背包是否已满 为0未满，为1已满
        int isFull = 0;
        // 赋值图片
        //thisItem.itemImage = thisObject.GetComponent<Image>().sprite;
        // 背包没有该物品进行添加，有则添加数量
        // 如果有名称相同的
        int sameItemCount = 0;
        for (int i = 0;i<playerBag.bag.Count;i++) {
            if (thisItem.name == playerBag.bag[i].itemName) {
                sameItemCount++;
            }
        }
        if (sameItemCount==0)
        {

            // 在ui添加物品，判断背包格数是否满了
            if (playerBag.bag.Count < 10)
            {
                // 往背包添加物品
                playerBag.bag.Add(thisItem);
                //addToBag.CreateNewItem(thisItem);
                //Destroy(thisObject.gameObject);
            }
            else
            {
                // 提示文本框背包已满
                print("背包已满1");
                //showBagFullMessage();
                //ShowBagFull();
                isFull = 1;
            }

        }
        else
        {
            // 判断背包是否已满
            if (playerBag.bag.Count < 10)
            {
                // 判断是否可以叠加
                if (thisItem.cumulative)
                {
                    // 每10个物品一组 
                    if (thisItem.itemNumber % maxCumulative == 0)
                    {
                        // 累加数量已满
                        // 满了新增
                        playerBag.bag.Add(thisItem);
                        // 数量加1
                        thisItem.itemNumber++;
                        //Destroy(thisObject.gameObject);
                    }
                    else
                    {
                        thisItem.itemNumber++; ;
                        //Destroy(thisObject.gameObject);
                    }
                }
                else
                {
                    // 如果不能叠加，新增
                    playerBag.bag.Add(thisItem);
                    //Destroy(thisObject.gameObject);
                }
            }
            else
            {
                // 背包格满了就不能新增，判断是否可以累加
                if (thisItem.cumulative)
                {
                    // 每10个物品一组 
                    if (thisItem.itemNumber % maxCumulative == 0)
                    {
                        // 累加数量已满
                        // 满了提示背包已满
                        //playerBag.bag.Add(thisItem);
                        print("物品累加数量已满2");
                        //showBagFullMessage();
                        // ShowBagFull();
                        isFull = 1;
                    }
                    else
                    {
                        thisItem.itemNumber ++;
                        //Destroy(thisObject.gameObject);
                    }
                }
                else
                {
                    print("背包已满3");
                    //showBagFullMessage();
                    //ShowBagFull();
                    isFull = 1;
                }
            }
        }

        addToBag.RefreshItem();
        return isFull;
    }


    // 提示背包已满
    public void showBagFullMessage() {
        // 获取背包已满提示框
        GameObject bagFull = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(1).gameObject;
        bagFull.SetActive(true);
    }

    // 背包已满信息提示
    public void ShowBagFull() {
        // 获取到背包已满提示
        GameObject dialog = GameObject.FindGameObjectWithTag("DialogPrefab");
        dialog.transform.GetChild(0).GetComponent<DialogPrefab>().message = new string[] { "背包已满" };
        dialog.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

}
