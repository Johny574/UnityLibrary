using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;
using System.Linq;

public class TextMeshProSwitcherMixerBehaviour : PlayableBehaviour
{
    // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI trackBinding = playerData as TextMeshProUGUI;

        if (!trackBinding)
            return;

        int inputCount = playable.GetInputCount ();
    
        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);

            ScriptPlayable<TextMeshProSwitcherBehaviour> inputPlayable = (ScriptPlayable<TextMeshProSwitcherBehaviour>)playable.GetInput(i);
            TextMeshProSwitcherBehaviour input = inputPlayable.GetBehaviour();

            double csplit = input.clipDuration / input.Text.Count();
            trackBinding.text = "";
            if (inputWeight > 0)
            {
                double currentTime = playable.GetTime() - input.clipStart;
                var j = (int)Mathf.Ceil((float)currentTime / (float)csplit);

                trackBinding.text = input.Text[..j];
            }
        }
    }
}
