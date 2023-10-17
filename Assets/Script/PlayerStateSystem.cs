using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyFarm
{
    /// <summary>
    /// 玩家狀態機
    /// </summary>
    public class PlayerStateSystem : MonoBehaviour
    {
        //獲取stateUI父物件
        private Transform stateUI;
        //獲取玩家身上腳本
        private Playertest player;

        private TextMeshProUGUI level;

        private TextMeshProUGUI playerState;

        private TextMeshProUGUI hunger;

        private TextMeshProUGUI health;

        private TextMeshProUGUI fish;

        private TextMeshProUGUI farm;

        private TextMeshProUGUI field;

        private TextMeshProUGUI dig;

        private void Start()
        {
            stateUI = GetComponent<Transform>();
            level = stateUI.Find("Level").GetComponent<TextMeshProUGUI>();
            playerState = stateUI.Find("State").GetComponent <TextMeshProUGUI>();
            hunger = stateUI.Find("currentHunger").GetComponent<TextMeshProUGUI>();
            health = stateUI.Find("currentLife").GetComponent<TextMeshProUGUI>();
            fish = stateUI.Find("Fish").GetComponent<TextMeshProUGUI>();
            farm = stateUI.Find("Farm").GetComponent<TextMeshProUGUI>();
            field = stateUI.Find("field").GetComponent<TextMeshProUGUI>();
            dig = stateUI.Find("Dig").GetComponent<TextMeshProUGUI>();
        }
        private void Update()
        {
            stateControll();
            
        }
        void stateControll()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            player = playerObject.GetComponent<Playertest>();
            level.text = player.level.ToString();
            hunger.text = player.currentHunger.ToString();
            health.text = player.currentHealth.ToString();
            fish.text = player.fishingLevel.ToString();
            farm.text = player.farmingLevel.ToString();
            field.text = player.fieldLevel.ToString();
            dig.text = player.digLevel.ToString();
            switch (player.playerState)
            {
                case 1:
                    playerState.text = "正常";
                    break;
                case 2:
                    playerState.text = "生病";
                    break;
                case 3:
                    playerState.text = "幸福";
                    break;
                case 4:
                    playerState.text = "悲傷";
                    break;
            }
        }
    }
}

