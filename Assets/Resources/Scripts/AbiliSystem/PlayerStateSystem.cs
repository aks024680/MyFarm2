using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateSystem : MonoBehaviour
{
    // ��ȡ����ֵ������
    Image healthBarSilder;
    // ��ȡ����ֵ������
    Image hungerBarSilder;
    // ��ȡ����ֵ������
    Image enduranceBarSilder;
    // ��ȡ��ҵĽű����
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

    // ʵʱ����Ѫ��������
    void UpdateHealth() {
        PlayerController playerController = player.GetComponent<PlayerController>();
        float SliderPercent = playerController.currentHealth / playerController.maxHealth;
        healthBarSilder.fillAmount = SliderPercent;
    }

    // ʵʱ���¼����Ȼ�����
    void UpdateHunger() {
        PlayerController playerController = player.GetComponent<PlayerController>();
        float SliderPercent = playerController.currentHunger / playerController.maxHunger;
        hungerBarSilder.fillAmount = SliderPercent;
    }

    // ʵʱ��������ֵ������
    void UpdateEndurance() {
        PlayerController playerController = player.GetComponent<PlayerController>();
        float SliderPercent = playerController.currentEndurance / playerController.maxEndurance;
        enduranceBarSilder.fillAmount = SliderPercent;
    }
}
