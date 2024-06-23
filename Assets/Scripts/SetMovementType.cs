using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMovementType : MonoBehaviour
{
    public static int movementIndex;

    void Start()
    {
        movementIndex = 0;
    }
    public void SetMovementFromIndex(int index) {
        movementIndex = index;
    }
}
