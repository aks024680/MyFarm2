using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerState : MonoBehaviour
{

    // 获取玩家脚本
    private PlayerController player;
    // 获取状态ui
    private Transform playerStateUI;
    // 获取需要更新的玩家等级
    private Text level;
    // 获取需要更新的饥饿度
    private Text currentHunger;
    // 获取需要更新的生命值
    private Text currentLife;
    // 获取需要更新的钓鱼等级
    private Text fishing;
    // 获取需要更新的养殖等级
    private Text farming;
    // 获取需要更新的种田等级
    private Text field;
    // 获取需要更新的挖矿等级
    private Text dig;
    // 获取需要更新的状态
    private Text state;
    // 最大生命值
    private Text maxHealth;
    // 最大饥饿度
    private Text maxHunger;
    
    void Start()
    {

        // 赋值状态ui
        playerStateUI = GetComponent<Transform>();
        // 赋值等级文本
        level = playerStateUI.Find("level").GetComponent<Text>();
        // 赋值饥饿度文本
        currentHunger = playerStateUI.Find("currentHunger").GetComponent<Text>();
        // 赋值生命值文本
        currentLife = playerStateUI.Find("currentLife").GetComponent<Text>();
        // 赋值钓鱼等级文本
        fishing = playerStateUI.Find("fishingLevel").GetComponent<Text>();
        // 赋值养殖等级文本
        farming = playerStateUI.Find("farmingLevel").GetComponent<Text>();
        // 赋值种田等级文本
        field = playerStateUI.Find("fieldLevel").GetComponent<Text>();
        // 赋值挖矿等级
        dig = playerStateUI.Find("digLevel").GetComponent<Text>();
        // 赋值玩家状态
        state = playerStateUI.Find("playerState").GetComponent<Text>();
        // 赋值最大生命值
        maxHealth = playerStateUI.Find("maxLife").GetComponent<Text>();
        // 赋值最大饥饿度
        maxHunger = playerStateUI.Find("maxHunger").GetComponent<Text>();
    }

    void Update()
    {
        UpdatePlayerStateUI();
    }

    // 更新玩家状态-- UI
    void UpdatePlayerStateUI() {
        // 获取玩家
        GameObject playerGamgObject = GameObject.FindGameObjectWithTag("Player");
        // 赋值玩家控制脚本
        PlayerController player = playerGamgObject.GetComponent<PlayerController>();
        // 更新等级
        level.text = player.level.ToString();
        // 更新饥饿度
        currentHunger.text = player.currentHunger.ToString();
        // 更新生命值
        currentLife.text = player.currentHealth.ToString();
        // 更新钓鱼等级
        fishing.text = player.fishingLevel.ToString();
        // 更新养殖等级
        farming.text = player.farmingLevel.ToString();
        // 更新种田等级
        field.text = player.fieldLevel.ToString();
        // 更新挖矿等级
        dig.text = player.digLevel.ToString();
        // 更新最大生命值
        maxHealth.text = "/ " + player.maxHealth.ToString();
        // 更新最大饥饿度
        maxHunger.text = "/ " + player.maxHunger.ToString();


        // 更新玩家状态  // 玩家状态 // 1正常  2生病  3受伤   4幸福  5悲伤
        switch (player.playerState) {
            case 1:
                state.text = "正常";
                break;
            case 2:
                state.text = "生病";
                break;
            case 3:
                state.text = "受伤";
                break;
            case 4:
                state.text = "幸福";
                break;
            case 5:
                state.text = "悲伤";
                break;
        }
        
    }
}
