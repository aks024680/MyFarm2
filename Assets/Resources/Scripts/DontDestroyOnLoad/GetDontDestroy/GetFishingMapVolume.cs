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

    // д������������ֵ
    public void GetVolumeValue() {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("audioSliderValue");
        print("��ֵ������:"+ GetComponent<AudioSource>().volume);
    }
}
