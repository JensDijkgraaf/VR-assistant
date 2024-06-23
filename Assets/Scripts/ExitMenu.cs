using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;

    public void OnTriggerExit(Collider other)
    {
        menu.SetActive(false);
    }
}
