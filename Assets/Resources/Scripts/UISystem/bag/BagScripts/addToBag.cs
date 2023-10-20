using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addToBag : MonoBehaviour
{
    // 本脚本用于将物品添加到背包UI中
    static addToBag toBag;

    // 初始化
    private void Awake()
    {
        if (toBag != null) {
            Destroy(this);
        }
        toBag = this;
    }

    private void Update()
    {
        // 调整物品数量显示
        changeItemNumberShow();
        // 显示装备图标
         showEquipItem();
        //showEquipItemTest();
    }


    // 背包激活状态
    private void OnEnable()
    {
        RefreshItem();
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        Grid.transform.GetChild(2).GetComponent<Text>().text = "";
        // 开启开关按钮（根据是否能够装备、使用）、丢弃

    }
    public static void showItem(itemsCreations item,int index) {
        // 获取到bag的ui
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        Grid.transform.GetChild(2).GetComponent<Text>().text = item.itemScription;
        // 获取背包UI父级物体下的网格子物体
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        for (int i = 0; i < bagGrid.transform.childCount; i++)
        {
            if (i == index)
            {
                if (item.equip == true)
                {
                    Grid.transform.GetChild(1).GetChild(i).GetChild(1).gameObject.SetActive(true);
                }
                if (item.use == true) {
                    Grid.transform.GetChild(1).GetChild(i).GetChild(2).gameObject.SetActive(true);
                }
                Grid.transform.GetChild(1).GetChild(i).GetChild(3).gameObject.SetActive(true);
            }
            else {
                Grid.transform.GetChild(1).GetChild(i).GetChild(1).gameObject.SetActive(false);
                Grid.transform.GetChild(1).GetChild(i).GetChild(2).gameObject.SetActive(false);
                Grid.transform.GetChild(1).GetChild(i).GetChild(3).gameObject.SetActive(false);
            }
        }

            
        
    }


    // 创建新物品方法,为了让别的类调用，需要静态static,或者实例化对象
    public static void CreateNewItem(itemsCreations item) {
        // 获取背包格预制体
        GridPrefab GridPrefabs = Resources.Load<GridPrefab>("Prefabs/bag/grid");
        // 获取背包ui父级物体
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // 获取背包UI父级物体下的网格子物体
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // 创建新物品  需要创建的物品，将该物品创建到哪里,角度
        GridPrefab newItem = Instantiate(GridPrefabs, bagGrid.transform.position,Quaternion.identity);
        // 设置新创建物品的父级物体
        newItem.gameObject.transform.SetParent(bagGrid.transform);
        // 赋值物品
        newItem.item = item;
        newItem.itemImage.sprite = item.itemImage;
        newItem.itemNum.text = item.itemNumber.ToString();

    }

    // 刷新物品
    public static void RefreshItemTest() {
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // 遍历UI网格下所有子物体,销毁所有子物体
        // 获取背包UI父级物体下的网格子物体
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        
            for (int i = 0; i < bagGrid.transform.childCount; i++)
            {
            if (bagGrid.transform.childCount == 0)
            {
                break;
            }
            else {
                Destroy(bagGrid.transform.GetChild(i).gameObject);
            }

                
            }
        // 获取玩家背包
        bagCreations bagList = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        // 将所有销毁的子物体添加回来达到刷新目的
        for (int i =0; i< bagList.bag.Count;i ++) {
            print("第"+i+"个");
            CreateNewItem(bagList.bag[i]);
        }

        
    }





    // 调整物品数量显示
    public static void changeItemNumberShow() {
        // 获取玩家背包
        bagCreations bagList = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // 获取背包UI父级物体下的网格子物体
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // 获取获取物品类的最大累加属性
        //getItem getItems = new getItem();
        // 定义接收相同的物品的下标
        List<int> someItemIndex = new List<int>();
        // 由于背包格和背包数据的顺序一样，可作遍历
        for (int i = 0; i< bagList.bag.Count;i++) {
            if (bagList.bag.Count == 0)
            {
                break;
            }
            else {
                for (int j = 0; j< bagList.bag.Count;j++) {
                    // 如果物品相同，把下标加入集合
                    if (bagList.bag[i] == bagList.bag[j]) {
                        someItemIndex.Add(j);
                    }
                }
                // 判断重复的物品是否不唯一
                if (someItemIndex.Count != 1) {
                    if (bagList.bag[i].cumulative == true)
                    {
                        for (int k = 0; k < someItemIndex.Count; k++)
                        {
                            // 不是最后一件重复物品
                            if (k != someItemIndex.Count - 1)
                            {
                                bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = getItem.maxCumulative.ToString();
                            }
                            else
                            {
                                // 判断求余是否为0
                                if (bagList.bag[someItemIndex[k]].itemNumber % getItem.maxCumulative == 0)
                                {
                                    bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = getItem.maxCumulative.ToString();
                                }
                                else
                                {
                                    bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = (bagList.bag[someItemIndex[k]].itemNumber % getItem.maxCumulative).ToString();
                                    bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = (bagList.bag[someItemIndex[k]].itemNumber % getItem.maxCumulative).ToString();
                                }
                            }
                        }
                    }

                }

                // 重置集合
                someItemIndex.Clear();
            }
        }
    }
    public static void changeItemNumberShowTest()
    {
        // 获取玩家背包
        bagCreations bagList = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // 获取背包UI父级物体下的网格子物体
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // 获取获取物品类的最大累加属性
        //getItem getItems = new getItem();
        // 定义接收相同的物品的下标
        List<int> someItemIndex = new List<int>();
        // 由于背包格和背包数据的顺序一样，可作遍历
        for (int i = 0; i < bagList.bag.Count; i++)
        {
            if (bagList.bag.Count == 0)
            {
                break;
            }
            else
            {
                for (int j = 0; j < bagList.bag.Count; j++)
                {
                    // 如果物品相同，把下标加入集合
                    if (bagList.bag[i] == bagList.bag[j])
                    {
                        someItemIndex.Add(j);
                    }
                }
                // 判断重复的物品是否不唯一
                if (someItemIndex.Count != 1)
                {
                    if (bagList.bag[i].cumulative == true)
                    {
                        for (int k = 0; k < someItemIndex.Count; k++)
                        {
                            // 不是最后一件重复物品
                            if (k != someItemIndex.Count - 1)
                            {
                                bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = getItem.maxCumulative.ToString();
                            }
                            else
                            {
                                // 判断求余是否为0
                                if (bagList.bag[someItemIndex[k]].itemNumber % getItem.maxCumulative == 0)
                                {
                                    bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = getItem.maxCumulative.ToString();
                                }
                                else
                                {
                                    bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = (bagList.bag[someItemIndex[k]].itemNumber % getItem.maxCumulative).ToString();
                                    bagGrid.transform.GetChild(someItemIndex[k]).GetChild(0).GetComponent<Text>().text = (bagList.bag[someItemIndex[k]].itemNumber % getItem.maxCumulative).ToString();
                                }
                            }
                        }
                    }

                }

                // 重置集合
                someItemIndex.Clear();
            }
        }
    }
    public static void RefreshItem()
    {
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // 遍历UI网格下所有子物体,销毁所有子物体
        // 获取背包UI父级物体下的网格子物体
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;

        for (int i = 0; i < bagGrid.transform.childCount; i++)
        {
            if (bagGrid.transform.childCount == 0)
            {
                break;
            }
            else
            {
                Destroy(bagGrid.transform.GetChild(i).gameObject);
            }


        }
        // 获取玩家背包
        bagCreations bagList = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        // 将所有销毁的子物体添加回来达到刷新目的
        for (int i = 0; i < bagList.bag.Count; i++)
        {
            CreateNewItem(bagList.bag[i]);
        }
        // 设置数量的显示

        
    }

    // 设置装备物品和装备标识的显示
    public static void showEquipItem() {
        // 获取背包ui
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // 获取状态ui
        GameObject state = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).gameObject;
        // 获取背包UI父级物体下的网格子物体
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // 判断下标是否为-1
        if (GridPrefab.clickItemIndex == -1)
        {
            
            // 将装备的道具设置null
            state.transform.GetChild(19).gameObject.GetComponent<Image>().sprite = null;
            // 失活装备栏
            state.transform.GetChild(19).gameObject.SetActive(false);
        }
        else {
            // 显示装备标识
            bagGrid.transform.GetChild(GridPrefab.clickItemIndex).GetChild(4).gameObject.SetActive(true);
            print("装备标识:"+ bagGrid.transform.GetChild(GridPrefab.clickItemIndex).GetChild(4).gameObject.activeSelf);
            // 将装备的道具添加到状态栏
            state.transform.GetChild(19).gameObject.GetComponent<Image>().sprite = bagGrid.transform.GetChild(GridPrefab.clickItemIndex).GetComponent<Image>().sprite;
            // 激活装备栏
            state.transform.GetChild(19).gameObject.SetActive(true);
        }
    }

    // 测试演示-已装备物品显示
    public static void showEquipItemTest() {
        // 获取背包ui
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // 获取状态ui
        GameObject state = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).gameObject;
        // 获取背包UI父级物体下的网格子物体
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // 
        if (GridPrefab.clickItemIndex == -1)
        {
            // 显示背包装备标识
            bagGrid.transform.GetChild(GridPrefab.clickItemIndex).GetChild(4).gameObject.SetActive(false);
            // 赋值图片
            state.transform.GetChild(19).GetComponent<Image>().sprite = null;
            // 显示装备
            state.transform.GetChild(19).gameObject.SetActive(false);
        }
        else {
            // 显示背包装备标识
            bagGrid.transform.GetChild(GridPrefab.clickItemIndex).GetChild(4).gameObject.SetActive(true);
            // 赋值图片
            state.transform.GetChild(19).GetComponent<Image>().sprite = bagGrid.transform.GetChild(GridPrefab.clickItemIndex).GetComponent<Image>().sprite;
            // 显示装备
            state.transform.GetChild(19).gameObject.SetActive(true);

        }
    }
}
