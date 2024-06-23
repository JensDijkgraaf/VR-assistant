using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateStartMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuNL;
    [SerializeField] private GameObject menuEN;

    [SerializeField] private GameObject advancedSettingsNL;
    [SerializeField] private GameObject advancedSettingsEN;

    public void ToAdvancedNL() {
        menuNL.SetActive(false);
        advancedSettingsNL.SetActive(true);
    }

    public void ToAdvancedEN() {
        menuEN.SetActive(false);
        advancedSettingsEN.SetActive(true);
    }

    public void ToMenuNL() {
        advancedSettingsNL.SetActive(false);
        menuNL.SetActive(true);
    }

    public void ToMenuEN() {
        advancedSettingsEN.SetActive(false);
        menuEN.SetActive(true);
    }
}
