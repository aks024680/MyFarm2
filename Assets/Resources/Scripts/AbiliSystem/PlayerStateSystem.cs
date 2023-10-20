using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateSystem : MonoBehaviour
{
    // 获取生命值滑动条
    Image healthBarSilder;
    // 获取饥饿值滑动条
    Image hungerBarSilder;
    // 获取耐力值滑动条
    Image enduranceBarSilder;
    // 获取玩家的脚本组件
    public GameObject player;

    void Awake()
    {
        healthBarSilder = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        hungerBarSilder = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        enduranceBarSilder = transform.GetChild(2).GetChild(0).GetComponent<Image>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateHunger();
        UpdateEndurance();
    }

    // 实时更新血量滑动条
    void UpdateHealth() {
        PlayerController playerController = player.GetComponent<PlayerController>();
        float SliderPercent = playerController.currentHealth / playerController.maxHealth;
        healthBarSilder.fillAmount = SliderPercent;
    }

    // 实时更新饥饿度滑动条
    void UpdateHunger() {
        PlayerController playerController = player.GetComponent<PlayerController>();
        float SliderPercent = playerController.currentHunger / playerController.maxHunger;
        hungerBarSilder.fillAmount = SliderPercent;
    }

    // 实时更新耐力值滑动条
    void UpdateEndurance() {
        PlayerController playerController = player.GetComponent<PlayerController>();
        float SliderPercent = playerController.currentEndurance / playerController.maxEndurance;
        enduranceBarSilder.fillAmount = SliderPercent;
    }
}
