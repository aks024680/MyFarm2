using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 创建背包 fileName: 创建背包的初始名称 menuName 菜单栏创建背包的功能栏名称
[CreateAssetMenu(fileName = "bagName" ,menuName ="bag/createNewBag")]
public class bagCreations : ScriptableObject
{
    // 创建背包\背包存放的内容为itemsCreations创建的物品类型
    public List<itemsCreations> bag = new List<itemsCreations>();


}
