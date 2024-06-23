using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetLanguage : MonoBehaviour
{
    [SerializeField] private GameObject english;
    [SerializeField] private GameObject dutch;

    [SerializeField] private TMP_Dropdown languageDropdownEN;
    [SerializeField] private TMP_Dropdown languageDropdownNL;
    public static string language;
    // Start is called before the first frame update
    void Start()
    {
        if (english.activeSelf)
        {
            language = "english";
        }
        else
        {
            language = "dutch";
        }
    }

    void Update()
    {
        if (language == "english")
        {
            languageDropdownEN.value = 0;
            languageDropdownNL.value = 1;
        }
        else
        {
            languageDropdownEN.value = 1;
            languageDropdownNL.value = 0;
        }
    }

    public void SetLanguageFromIndexEN(int index)
    {
        switch (index)
        {
            case 0:
                english.SetActive(true);
                dutch.SetActive(false);
                language = "english";
                break;
            case 1:
                english.SetActive(false);
                dutch.SetActive(true);
                language = "dutch";
                break;
        }
    }

    public void SetLanguageFromIndexNL(int index)
    {
        switch (index)
        {
            case 0:
                english.SetActive(false);
                dutch.SetActive(true);
                language = "dutch";
                break;
            case 1:
                english.SetActive(true);
                dutch.SetActive(false);
                language = "english";
                break;
        }
    }
}
