using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuInteraction : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    void Start()
    {
        menu.SetActive(false);
    }

    public void GoToMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        menu.SetActive(true);
    }
}
