using System.Collections;
using System.Collections.Generic;
using ReadyPlayerMe.Core;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UseSettings : MonoBehaviour
{
    public string voice;
    public string role;
    public string model;
    public bool initiative;
    public string language;
    public string prompt;
    public GameObject avatar;
    // public VoiceHandler voiceHandler;

    [SerializeField] private GameObject womanAvatar;

    [SerializeField] private GameObject manAvatar;
    
    // public Animator avatarAnimator;
    public ActionBasedContinuousTurnProvider continuousTurnProvider;
    public ActionBasedSnapTurnProvider snapTurnProvider;
    public ActionBasedContinuousMoveProvider continuousMoveProvider;
    public TeleportationProvider teleportationProvider;
    public TeleportationArea teleportationArea;

    // Start is called before the first frame update
    void Start()
    {
        SetAvatar();
        SetMovement();
        SetTurn();
        SetRoleString();
        SetModelString();
        SetInitiativeValue();
        SetLanguageString();
        SetPrompt();
    }
    private void SetAvatar()
    {
        switch (SetVoice.voiceIndex)
        {
            case 0:
                voice = "alloy";
                womanAvatar.SetActive(false);
                manAvatar.SetActive(true);
                avatar = manAvatar;
                break;
            case 1:
                voice = "echo";
                womanAvatar.SetActive(false);
                manAvatar.SetActive(true);
                avatar = manAvatar;
                break;
            case 2:
                voice = "fable";
                womanAvatar.SetActive(false);
                manAvatar.SetActive(true);
                avatar = manAvatar;
                break;
            case 3:
                voice = "onyx";
                womanAvatar.SetActive(false);
                manAvatar.SetActive(true);
                avatar = manAvatar;
                break;
            case 4:
                voice = "nova";
                womanAvatar.SetActive(true);
                manAvatar.SetActive(false);
                avatar = womanAvatar;
                break;
            case 5:
                voice = "shimmer";
                womanAvatar.SetActive(true);
                manAvatar.SetActive(false);
                avatar = womanAvatar;
                break;
        }
    }
    private void SetMovement()
    {
        if (SetMovementType.movementIndex == 0)
        {
            continuousMoveProvider.enabled = true;
            teleportationProvider.enabled = false;
            teleportationArea.enabled = false;
        }
        else
        {
            continuousMoveProvider.enabled = false;
            teleportationProvider.enabled = true;
            teleportationArea.enabled = true;
        }
    }

    private void SetTurn()
    {
        if (SetTurnType.turnIndex == 0)
        {
            continuousTurnProvider.enabled = true;
            snapTurnProvider.enabled = false;
        }
        else
        {
            continuousTurnProvider.enabled = false;
            snapTurnProvider.enabled = true;
        }
    }

    public string GetVoice()
    {
        return voice;
    }

    private void SetRoleString()
    {
        role = SetRole.role;
    }

    public string GetRole()
    {
        return role;
    }

    private void SetModelString()
    {
        model = SetModel.model;
    }

    public string GetModel()
    {
        return model;
    }
    
    public GameObject GetAvatar()
    {
        return avatar;
    }

    private void SetInitiativeValue()
    {
        initiative = SetInitiative.initiative;
    }

    public bool GetInitiative()
    {
        return initiative;
    }

    private void SetLanguageString()
    {
        language = SetLanguage.language;
    }

    public string GetLanguage()
    {
        return language;
    }

    private void SetPrompt()
    {
        prompt = FileBrowse.prompt;
    }

    public string GetPrompt()
    {
        return prompt;
    }
}
