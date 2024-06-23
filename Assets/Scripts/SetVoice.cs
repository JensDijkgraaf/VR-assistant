using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVoice : MonoBehaviour
{
    public static int voiceIndex;

    void Start()
    {
        voiceIndex = 0;
    }
    public void SetVoiceFromIndex(int index) {
        voiceIndex = index;
    }
}
