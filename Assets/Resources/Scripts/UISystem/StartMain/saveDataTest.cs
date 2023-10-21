using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveDataTest : MonoBehaviour
{
   /// <summary>
   /// ´æµµ
   /// </summary>
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CloseSaveDataUI();
    }

    // 
    public void CloseSaveDataUI() {
        // ESC
        if (Input.GetKey(KeyCode.Escape)) {
            // Ö÷²Ëµ¥
            GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").transform.gameObject;
            // ´æµµ½çÃæ
            GameObject saveDataUI = GameObject.FindGameObjectWithTag("SaveDataUI").transform.gameObject;
            //
            mainMenu.transform.GetChild(0).gameObject.SetActive(true);
            //
            saveDataUI.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    // ´æµµ1
    public void Data1() {
        print("´æµµ1");
    }
    // ´æµµ2
    public void Data2()
    {
        print("´æµµ2");
    }
    // ´æµµ3
    public void Data3()
    {
        print("´æµµ3");
    }
}
