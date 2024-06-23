using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScene : MonoBehaviour
{
    public static int sceneIndex;

    void Start()
    {
        sceneIndex = 1;
    }
    public void SetSceneFromIndex(int index) {
        // Adjust the scene index by 1 to account for the main menu scene
        sceneIndex = index + 1;
    }
}
