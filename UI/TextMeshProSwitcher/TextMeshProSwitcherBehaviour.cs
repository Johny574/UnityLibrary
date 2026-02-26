using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;

[Serializable]
public class TextMeshProSwitcherBehaviour : PlayableBehaviour
{
    public string Text;
    public double clipStart;
    public double clipDuration;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }
}
