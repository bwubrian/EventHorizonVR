using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour {

    public TextToSpeech tts;

    public float time;
    public int stage = 0;

    // Use this for initialization
    void Start() {
        time = Time.time;
    }

    // Update is called once per frame
    void Update() {
        time = Time.time;
        //Debug.Log(stage);

        if (time > 3 && stage == 0)
        {
            tts.SpeechPlayback("Hey, how was your nap? You've been in hibernation for 42 years now!" +
            " Looks like your ship is being sucked into a black hole! We need to turn on the warp" +
            " engine as soon as possible! We'll need to access the computer first. Can you go to " +
            "the command center and look for an accesscode? Just shout it out when you got it.");
            stage++;
        }
        if (stage == 1)
        {
            tts.triggerPhrases.Add("G 7 S 5 F", "enter stage 3");
            stage++;
        }
        if (stage == 3)
        {
            tts.SpeechPlayback("Nice! We're logged on. Now, go to the engine module to turn on the engine!");
            stage++;
        }
        if (stage == 5)
        {
            tts.SpeechPlayback("Oops. Looks like the power source is depleted. We need to change the" +
                " generator core. There should be a backup in the hibernation room. Go look for it now; we're running out of time.");
            stage++;
        }
    }
    
}
