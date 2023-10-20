using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 创建菜单栏 fileName :默认新创建的物品名称， menuName 菜单栏功能名称
[CreateAssetMenu(fileName = "itemName",menuName = "bag/createNewItem")]
public class itemsCreations : ScriptableObject
{
    // 本脚本专门用于创建物品数据库（物品信息）

    // 物品名称
    public string itemName;
    // 物品图片信息
    public Sprite itemImage;
    // 物品数量
    public int itemNumber;
    // 物品价值
    public int price;
    // 物品恢复的血量值
    public float health = 0;
    // 物品恢复的饥饿度
    public float hunger = 0;
    // 物品描述
    [TextArea]
    public string itemScription;
    // 判断是否加入到循环列表
    public bool isAddFishing;
    // 可获取的经验
    public int Exp;

    // 是否可以装备
    public bool equip;
    // 是否可以累加
    public bool cumulative;
    // 是否可以使用
    public bool use;
    // 好感度
    public int npcFavorNumber;

}
