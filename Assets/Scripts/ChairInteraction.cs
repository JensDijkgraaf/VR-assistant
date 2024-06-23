using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ChairInteraction : MonoBehaviour
{
    [SerializeField] private ActionBasedContinuousMoveProvider continuousMoveProvider;
    [SerializeField] private TeleportationProvider teleportationProvider;
    [SerializeField] private TeleportationArea teleportationArea;
    [SerializeField] private InputActionProperty sitButton;
    [SerializeField] private GameObject helpMenu;

    private bool isSitting = false;
    private Vector3 originalPlayerPosition;
    private bool continuousMoveEnabled;
    private bool teleportationEnabled;
    private bool teleportationAreaEnabled;

    private void Start()
    {
        // Record original states of XR Interaction Toolkit components
        continuousMoveEnabled = continuousMoveProvider.enabled;
        teleportationEnabled = teleportationProvider.enabled;
        teleportationAreaEnabled = teleportationArea.enabled;
    }
    // public void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("chair") && !isSitting)
    //     {
    //         helpMenu.SetActive(true);
    //     }
    // }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("chair") && !isSitting)
        {
            helpMenu.SetActive(false);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("chair") && !isSitting && sitButton.action.WasPressedThisFrame())
        {
            isSitting = true;
            // Record original player position and rotation
            originalPlayerPosition = transform.position + new Vector3(0.05f, 0, 0.05f);
            // originalPlayerRotation = transform.rotation;
            // Set the player's position and rotation to the chair's position and rotation
            transform.position = other.transform.position;
            // transform.rotation = other.transform.rotation;

            // Disable XR Interaction Toolkit components while in the chair
            // continuousTurnProvider.enabled = false;
            continuousMoveProvider.enabled = false;
            // snapTurnProvider.enabled = false;
            teleportationProvider.enabled = false;
            teleportationArea.enabled = false;
        }
    }

    void Update()
    {
        if (isSitting) {
            helpMenu.SetActive(false);
        }
        if (isSitting && sitButton.action.WasPressedThisFrame())
        {

            // Restore player's original position and rotation
            transform.position = originalPlayerPosition;
            // transform.rotation = originalPlayerRotation;

            // Re-enable previously enabled XR Interaction Toolkit components
            // continuousTurnProvider.enabled = continuousTurnEnabled;
            continuousMoveProvider.enabled = continuousMoveEnabled;
            // snapTurnProvider.enabled = snapTurnEnabled;
            teleportationProvider.enabled = teleportationEnabled;
            teleportationArea.enabled = teleportationAreaEnabled;
            
            // Small delay to prevent the player from immediately sitting back down
            StartCoroutine(ResetIsSitting());            
        }
    }

    private System.Collections.IEnumerator ResetIsSitting()
    {
        yield return new WaitForSeconds(0.5f);
        isSitting = false;
    }
}
