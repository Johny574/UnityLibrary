using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;

[TrackColor(0.855f, 0.8623f, 0.87f)]
[TrackClipType(typeof(TextMeshProSwitcherClip))]
[TrackBindingType(typeof(TextMeshProUGUI))]
public class TextMeshProSwitcherTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
         foreach (var clip in GetClips())
        {
            if (clip.asset is TextMeshProSwitcherClip switcherClip)
            {
                switcherClip.template.clipStart = clip.start;
                switcherClip.template.clipDuration = clip.duration;
            }
        }

        return ScriptPlayable<TextMeshProSwitcherMixerBehaviour>.Create (graph, inputCount);
    }
}
