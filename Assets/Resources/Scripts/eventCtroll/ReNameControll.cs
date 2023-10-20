using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReNameControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var allObject = Resources.FindObjectsOfTypeAll(typeof(Transform)) as Transform[];
        foreach (var item in allObject) {
            if (item.name.Contains("Clone")) {
                item.name = item.name.Replace("(Clone)","");
            }
            if (item.name.Contains("(")) {
                string[] strArr = item.name.Split(new char[] { '(' });
                print(strArr[0]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
