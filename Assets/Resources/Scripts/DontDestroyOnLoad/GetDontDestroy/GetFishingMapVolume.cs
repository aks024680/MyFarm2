using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFishingMapVolume : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 写方法给音量赋值
    public void GetVolumeValue() {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("audioSliderValue");
        print("赋值后音量:"+ GetComponent<AudioSource>().volume);
    }
}
