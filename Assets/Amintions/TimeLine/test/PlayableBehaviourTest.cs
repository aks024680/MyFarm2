using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class PlayableBehaviourTest : PlayableBehaviour
{
    // 物体
    public GameObject obj;
    public float _scale;
    // 开始
    public override void OnGraphStart(Playable playable)
    {
        
    }

    // 停止
    public override void OnGraphStop(Playable playable)
    {
        
    }

    // 播放
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        
    }

    // 暂停
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        
    }

    // 每帧调用
    public override void ProcessFrame(Playable playable, FrameData info,object playerData)
    {
        base.ProcessFrame(playable, info, playerData);
        if (obj) {
            obj.transform.localScale = Vector3.one * _scale;
        }
    }
}
