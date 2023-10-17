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

        private Text level;

        private void Start()
        {
            stateUI = GetComponent<Transform>();
            level = stateUI.Find("Level").GetComponent<Text>();
            
        }
        private void Update()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
           
        }
    }
}

