using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;

[Serializable]
public class TextMeshProSwitcherClip : PlayableAsset, ITimelineClipAsset
{
    public TextMeshProSwitcherBehaviour template = new TextMeshProSwitcherBehaviour ();

    // public double clipStart;
    // public double clipDuration;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TextMeshProSwitcherBehaviour>.Create (graph, template);
        TextMeshProSwitcherBehaviour clone = playable.GetBehaviour ();

        return playable;
    }
}
