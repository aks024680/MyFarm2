using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class PlayableBehaviourTest : PlayableBehaviour
{
    // ����
    public GameObject obj;
    public float _scale;
    // ��ʼ
    public override void OnGraphStart(Playable playable)
    {
        
    }

    // ֹͣ
    public override void OnGraphStop(Playable playable)
    {
        
    }

    // ����
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        
    }

    // ��ͣ
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        
    }

    // ÿ֡����
    public override void ProcessFrame(Playable playable, FrameData info,object playerData)
    {
        base.ProcessFrame(playable, info, playerData);
        if (obj) {
            obj.transform.localScale = Vector3.one * _scale;
        }
    }
}
