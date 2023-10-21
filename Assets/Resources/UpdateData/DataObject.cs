using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataObject : MonoBehaviour
{
    // 记录需要进行数据获取的对象
}
// 定义赋值玩家属性的对象(添加可序列化)

public class playerData
{
    // 定义速度
    public float speed;
    // 当前生命值
    public float currentHealth;
    // 当前饥饿度
    public float currentHunger;
    // 耐力值
    public float currentEndurance;
    // 玩家状态 // 1正常  2生病  3受伤   4幸福  5悲伤
    public int playerState;
    // 玩家等级
    public int level;
    // 钓鱼等级
    public int fishingLevel;
    // 养殖等级
    public int farmingLevel;
    // 种田等级
    public int fieldLevel;
    // 挖矿等级
    public int digLevel;
    //当前人物经验
    public int currentPlayerExp;
    // 当前钓鱼经验
    public int currentFishExp;
    // 当前养殖经验
    public int currentFarmExp;
    // 当前种田经验
    public int currentFieldExp;
    // 当前挖矿经验
    public int currentDigExp;
    // 跳转前的场景名称
    public string beforeSceneName;
}

// 定义赋值时间系统的属性
[System.Serializable]
public class timeSystemData
{
    public int showMinute;
    // 秒钟
    public int showSeconds;
    // 季节切换判断,1春天，2夏天，3秋天，4冬天
    public int seasonTime;
}