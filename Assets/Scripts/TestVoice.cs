using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVoice : MonoBehaviour
{
    // Include some wav sounds
    public AudioClip alloySound;
    public AudioClip echoSound;
    public AudioClip fableSound;
    public AudioClip onyxSound;
    public AudioClip novaSound;
    public AudioClip shimmerSound;
    // Include the audio source
    public AudioSource audioSource;

    public void TestVoiceFromIndex() {
        switch (SetVoice.voiceIndex) {
            case 0:
                audioSource.clip = alloySound;
                break;
            case 1:
                audioSource.clip = echoSound;
                break;
            case 2:
                audioSource.clip = fableSound;
                break;
            case 3:
                audioSource.clip = onyxSound;
                break;
            case 4:
                audioSource.clip = novaSound;
                break;
            case 5:
                audioSource.clip = shimmerSound;
                break;
        }
        audioSource.Play();
    }
}
