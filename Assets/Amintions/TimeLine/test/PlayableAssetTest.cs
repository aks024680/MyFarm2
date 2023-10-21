using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class PlayableAssetTest : PlayableAsset
{
    // 
    public ExposedReference<GameObject> obj;
    public float _scale;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var playable = ScriptPlayable<PlayableBehaviourTest>.Create(graph);
        var beha = playable.GetBehaviour();

        //graph.GetResolver()的功能：将 ExposedReference数据进行返回，Resolve是将类型返回，如GameObject
        beha.obj = obj.Resolve(graph.GetResolver());
        beha._scale = _scale;
        return playable;
    }
}
