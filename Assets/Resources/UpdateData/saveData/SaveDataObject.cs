using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataObject : MonoBehaviour
{
}
// 判断是否需要加载存档
public class NeedLoadData {
    // 是否需要加载存档
    public bool isNeedLoadData;
    // 需要加载的存档文件名为
    public string needLoadFileName;
    // 判断是否通过传送跳转场景
    public bool isChangeScene;
    // 跳转前的场景为什么
    public string beforeChangeSceneName;
}
// 需要保存的玩家数据
public class DataForPlayer {
    // 定义速度
    public float speed;
    // 基础速度
    public float baseSpeed;
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
    // 当前场景名称
    public string currentSceneName;
    // 玩家坐标
    public float playerX;
    public float playerY;
    public float playerZ;
    // 存档名称
    public string dataName;
    // 存档时间
    public string dataSaveTime;

}
// 需要保存的时间系统数据
public class DataForTimeSystem {
    // 分钟
    public int showMinute;
    // 秒钟
    public int showSeconds;
    // 季节切换判断,1春天，2夏天，3秋天，4冬天
    public int seasonTime;
}
// 需要保存的npc数据
public class DataForNpc1 { }
