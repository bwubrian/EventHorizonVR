using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextToSpeech : MonoBehaviour {

    //public SpeechToText ibegyou;

    public Dictionary<string, string> triggerPhrases = new Dictionary<string, string>();
    public List<string> heardPhrases = new List<string>();
    public SpeechManager speech;

    // Use this for initialization
    void Start () {
        triggerPhrases.Add("hi", "Greetings!");
        triggerPhrases.Add("hello", "Greetings!");

        triggerPhrases.Add("thanks", "You're welcome!");
        triggerPhrases.Add("thank you", "You're welcome!");

        triggerPhrases.Add("light", "I'm turning on the lights.");
        triggerPhrases.Add("computer", "I can log you in, but I'll need a code.");

        //SpeechPlayback("hello there");

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void addHeardPhrase(string p)
    {
        heardPhrases.Add(p);
    }

    public void analyzeHeardPhrase(string p)
    {
        foreach(KeyValuePair<string, string> item in triggerPhrases)
        {
            if (p.Contains(item.Key))
            {
                SpeechPlayback(item.Value);
                Debug.Log(item.Value);
            }
        }
    }

    public void SpeechPlayback(string saythis)
    {
        if (speech.isReady)
        {
            //string msg = input.text;
            string msg = saythis;
            //speech.voiceName = (VoiceName)voicelist.value;
            //speech.VoicePitch = int.Parse(pitch.text);
            speech.Speak(msg);
        }
        else
        {
            Debug.Log("SpeechManager is not ready. Wait until authentication has completed.");
        }
    }
}
