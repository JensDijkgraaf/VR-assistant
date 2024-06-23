using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRole : MonoBehaviour
{
    public static string role;

    void Start()
    {
        role = "therapist";
    }
    public void SetRoleFromIndex(int index) {
        role = index == 0 ? "therapist" : "study-advisor";
    }
}
