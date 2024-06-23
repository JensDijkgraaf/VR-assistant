using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetModel : MonoBehaviour
{
    public static string model;
    // Start is called before the first frame update
    void Start()
    {
        model = "gpt-4-turbo";   
    }

    public void SetModelFromIndex(int index) {
        switch (index) {
            case 0:
                model = "gpt-3.5-turbo";
                break;
            case 1:
                model = "gpt-4";
                break;
            case 2:
                model = "gpt-4-turbo";
                break;
            case 3:
                model = "gpt-4o";
                break;
        }
    }
}
