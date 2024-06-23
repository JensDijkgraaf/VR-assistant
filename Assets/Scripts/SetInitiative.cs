using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetInitiative : MonoBehaviour
{
    // [SerializeField] private Toggle toggle;
    public static bool initiative;
    // Start is called before the first frame update
    void Start()
    {
        initiative = true;
        // toggle.onValueChanged.AddListener(SetInitiativeFromValue);  
    }

    public void SetInitiativeFromValue()
    {
        if (initiative)
        {
            initiative = false;
        }
        else
        {
            initiative = true;
        }
    }
}
