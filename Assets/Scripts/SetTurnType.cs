using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTurnType : MonoBehaviour
{
    public static int turnIndex;

    void Start()
    {
        turnIndex = 0;
    }
    public void SetTurnFromIndex(int index) {
        turnIndex = index;
    }
}
