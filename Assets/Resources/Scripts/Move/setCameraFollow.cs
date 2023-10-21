using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class setCameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    // …Ë÷√æµÕ∑∏˙ÀÊ
    public void FollowPlayer() {
        CinemachineVirtualCamera cinemachineVirtual = GetComponent<CinemachineVirtualCamera>();
        GameObject player = GameObject.FindGameObjectWithTag("Player").transform.gameObject;
        if (!cinemachineVirtual.Follow) {
            cinemachineVirtual.Follow = player.transform;
        }
        
    }
}
