using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
    Image enduranceSlider;
    Image healthSlider;
    Image hungerSlider;
    public GameObject player;
    // Start is called before the first frame update
    private void Awake()
    {
       
        enduranceSlider = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        
        healthSlider = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        
        hungerSlider = transform.GetChild(2).GetChild(0).GetComponent<Image>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateEndurance();
        
        UpdateHealth();
        
        UpdateHunger();
        
    }
    //控制滑動條的滑動
    void UpdateEndurance()
    {
        //需要操作的屬性
        Playertest playertest = player.GetComponent<Playertest>();
        float sliderPercent = playertest.currentEndurance / playertest.maxEndurance;
        enduranceSlider.fillAmount = sliderPercent;
    }
    void UpdateHealth()
    {
        Playertest playertest = player.GetComponent<Playertest>();
        float sliderPercent = playertest.currentHealth / playertest.maxHealth;
        healthSlider.fillAmount = sliderPercent;
        
    }
    void UpdateHunger()
    {
        Playertest playertest = player.GetComponent<Playertest>();
        float sliderPercent = playertest.currentHunger / playertest.maxHunger;
        hungerSlider.fillAmount = sliderPercent;
        
    }
}
