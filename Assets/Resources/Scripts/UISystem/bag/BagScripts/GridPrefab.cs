using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GridPrefab : MonoBehaviour
{
    // 本脚本用于将预制体格子信息进行更新
    // 
    // 获取itemsCreations类型的物品
    public itemsCreations item;
    // 获取需要更新的图片组件
    public Image itemImage;
    // 获取需要更新的文本（更新的内容是物品数量）
    public Text itemNum;
    // 保存当前装备物品的下标
    public static int clickItemIndex = -1;


    // Update is called once per frame
    void Update()
    {
        
    }

    // 点击显示详情
    public void clickItemToShowDetails() {
        // 获取点击的物品的下标
        int index = transform.GetSiblingIndex();
        addToBag.showItem(item,index);
    }
    // 点击装备按钮
    public void ItemEquip() {
        // 获取点击的物品的下标
        int index = transform.GetSiblingIndex();
        
        // 获取背包ui
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // 获取状态ui
        GameObject state = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).gameObject;
        // 获取背包UI父级物体下的网格子物体
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // 显示装备标识，将点击的物品的装备标识
        for (int i = 0; i<bagGrid.transform.childCount;i++) {
            // 判断是否为自己点击的物品并且不是重复点击已装备的物品
            if (i == index && i != clickItemIndex)
            {
                // 显示装备标识
                bagGrid.transform.GetChild(i).GetChild(4).gameObject.SetActive(true);
                // 设置下标
                clickItemIndex = index;
            }
            // 判断是否为自己点击的物品并且是重复点击已装备的物品
            else if (i == index && i== clickItemIndex) {
                // 不显示
                bagGrid.transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
                // 重置下标
                clickItemIndex = -1;
            }
            else {
                // 关闭装备标识
                bagGrid.transform.GetChild(i).GetChild(4).gameObject.SetActive(false);
            }
        }
        // 根据下标设置装备物品状态
        if (clickItemIndex == -1)
        {
            // 将装备的道具添加到状态栏
            state.transform.GetChild(19).gameObject.GetComponent<Image>().sprite = null;
            // 激活装备栏
            state.transform.GetChild(19).gameObject.SetActive(false);
        }
        else {
            // 将装备的道具添加到状态栏
            state.transform.GetChild(19).gameObject.GetComponent<Image>().sprite = bagGrid.transform.GetChild(clickItemIndex).GetComponent<Image>().sprite;
            // 激活装备栏
            state.transform.GetChild(19).gameObject.SetActive(true);
        }
    }
    // 点击使用按钮
    public void ItemUse() {
        // 获取点击的物品的下标
        int index = transform.GetSiblingIndex();
        // 更新或删除背包物品数据
        ItemDiscard();
        // 根据物品的血量恢复值和饥饿度恢复值恢复生命或饥饿度
        // 获取玩家背包
        bagCreations bagList = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        // 获取玩家身上的脚本
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").transform.gameObject.GetComponent<PlayerController>();
        print("玩家生命值:"+playerController.currentHealth);
        print("玩家饥饿度:" + playerController.currentHunger);
        // 定义变量接收玩家使用道具之后的生命值
        float useHealth = bagList.bag[index].health + playerController.currentHealth;
        // 定义变量接收玩家使用道具之后的饥饿度
        float useHunger = bagList.bag[index].hunger + playerController.currentHunger;

        // 判断当前生命值是否为最大生命值
        if (playerController.currentHealth == playerController.maxHealth)
        {
            // 弹文本窗口提示生命值已满或直接让当前生命值等于最大生命值
            playerController.currentHealth = playerController.maxHealth;
        }
        else {
            // 判断使用道具之后的生命值是否大于最大生命值
            if (useHealth > playerController.maxHealth)
            {
                playerController.currentHealth = playerController.maxHealth;
            }
            else {
                // 否则让当前生命值等于加血后的值
                playerController.currentHealth = useHealth;
            }
        }

        // 判断当前饥饿度是否为最大饥饿度
        if (playerController.currentHunger == playerController.maxHunger)
        {
            // 弹文本框提示饥饿度已满或者直接让当前饥饿度等于最大饥饿度
            playerController.currentHunger = playerController.maxHunger;
        }
        else {
            // 判断使用道具之后的饥饿度是否大于最大饥饿度
            if (useHunger > playerController.maxHunger)
            {
                playerController.currentHunger = playerController.maxHunger;
            }
            else {
                playerController.currentHunger = useHunger;
            }
        }
        print("------------------------------------------------------");
        print("玩家生命值-使用道具后:" + playerController.currentHealth);
        print("玩家饥饿度-使用道具后:" + playerController.currentHunger);
        print("------------------------------------------------------");

    }

    // 点击丢弃
    public void ItemDiscard() {
        // 获取点击的物品的下标
        int index = transform.GetSiblingIndex();
        // 获取玩家背包
        bagCreations bagList = Resources.Load<bagCreations>("Prefabs/bag/itemData/bagCreation/PlayerBag");
        // 获取背包ui
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // 获取背包UI父级物体下的网格子物体
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // 获取状态ui
        GameObject state = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).gameObject;
        // 判断当前点击的物品数量是否为1
        if (bagList.bag[index].itemNumber == 1)
        {
            // 如果数量为1则摧毁物体，并移除背包数据
            // 摧毁背包ui的点击的物品
            Destroy(bagGrid.transform.GetChild(index).gameObject);
            // 把物品从背包数据移除
            bagList.bag.Remove(bagList.bag[index]);
            // 将装备的物品进行取消
            clickItemIndex = -1;
            // 在移除完背包数据后会导致显示异常，需要调用以下刷新物品的代码
            addToBag.RefreshItem();
        }
        else {
            // 否则物品数据数量减少1，并更新显示
            bagList.bag[index].itemNumber -= 1;
            // 为了比对文本上的数量是否为真实的数量，需要刷新以进行比对
            addToBag.RefreshItem();
            if (bagList.bag[index].itemNumber % getItem.maxCumulative == 0)
            {
                // 判断当前点击的数量显示文本为最大累加数量,是则查找不为最大累加数量的相同类型的物品进行摧毁,否则直接摧毁当前点击的物品
                if (bagGrid.transform.GetChild(index).GetChild(0).GetComponent<Text>().text.Equals(getItem.maxCumulative.ToString()))
                {
                    // 定义集合接收相同的物品的下标
                    List<int> someItemIndexList = new List<int>();
                    for (int i = 0; i < bagList.bag.Count; i++)
                    {
                        if (bagList.bag[index] == bagList.bag[i])
                        {
                            someItemIndexList.Add(i);
                        }
                    }
                    // 遍历查找当前点击物品的相同物品
                    for (int i = 0; i < someItemIndexList.Count; i++)
                    {
                        if (!bagGrid.transform.GetChild(someItemIndexList[i]).GetChild(0).GetComponent<Text>().text.Equals(getItem.maxCumulative.ToString()))
                        {
                            // 把物品从背包数据移除
                            bagList.bag.Remove(bagList.bag[someItemIndexList[i]]);
                            // 摧毁背包ui的点击的物品
                            Destroy(bagGrid.transform.GetChild(someItemIndexList[i]).gameObject);
                        }
                    }
                    clickItemIndex = -1;
                }
                else
                {
                    // 把物品从背包数据移除
                    bagList.bag.Remove(bagList.bag[index]);
                    // 摧毁背包ui的点击的物品
                    Destroy(bagGrid.transform.GetChild(index).gameObject);
                    // 取消装备物品
                    clickItemIndex = -1;
                }
                // 在移除完背包数据后会导致显示异常，需要调用以下刷新物品的代码
                addToBag.RefreshItem();
            }
            else {
                // 在移除完背包数据后会导致显示异常，需要调用以下刷新物品的代码
                addToBag.RefreshItem();
            }
            
        }
    }

    // 减少物品
    public void ItemDiscardRedurce(bagCreations playerBag,itemsCreations item)
    {
        // 获取背包ui
        GameObject Grid = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(1).gameObject;
        // 获取背包UI父级物体下的网格子物体
        GameObject bagGrid = Grid.transform.GetChild(1).gameObject;
        // 获取状态ui
        GameObject state = GameObject.FindGameObjectWithTag("mainUI").transform.GetChild(0).GetChild(0).gameObject;
        // 判断当前点击的物品数量是否为1
        if (item.itemNumber == 1)
        {
            // 如果数量为1则摧毁物体，并移除背包数据
            // 把物品从背包数据移除
            playerBag.bag.Remove(item);
            // 将装备的物品进行取消
            clickItemIndex = -1;
            // 在移除完背包数据后会导致显示异常，需要调用以下刷新物品的代码
            addToBag.RefreshItem();
        }
        else
        {
            // 否则物品数据数量减少1，并更新显示
            item.itemNumber -= 1;
            // 为了比对文本上的数量是否为真实的数量，需要刷新以进行比对
            addToBag.RefreshItem();

        }
    }



    public void ItemDiscardTest(bagCreations playerBag,itemsCreations item)
    {
        if (item.itemNumber == 1)
        { 
            // 把物品从背包数据移除
            playerBag.bag.Remove(item);
            // 将装备的物品进行取消
            clickItemIndex = -1;
            // 在移除完背包数据后会导致显示异常，需要调用以下刷新物品的代码
            addToBag.RefreshItem();
        }
        else
        {
            // 否则物品数据数量减少1，并更新显示
            item.itemNumber -= 1;
               // 在移除完背包数据后会导致显示异常，需要调用以下刷新物品的代码
           addToBag.RefreshItem();
        }
    }
}
