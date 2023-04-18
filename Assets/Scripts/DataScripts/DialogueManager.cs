using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject panel;
    public TMP_Text message, yesText, noText;
    public Button yesButton, noButton;

    private void Awake()
    {
        instance = this;
        panel.SetActive(false);
    }

    public void ShowDialogue(string _message, UnityAction yesAction, UnityAction noAction = null, 
        string _yesText = "Yes", string _noText = "No")
    {
        noButton.gameObject.SetActive(true);

        message.text = _message;

        yesText.text = _yesText;
        noText.text = _noText;

        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        if (noAction != null)
        {
            noButton.onClick.AddListener(noAction);
        }   
        
        noButton.onClick.AddListener(DisablePanel);

        yesButton.onClick.AddListener(yesAction);
        yesButton.onClick.AddListener(DisablePanel);

        panel.SetActive(true);
    }

    public void ShowMessage(string _message)
    {
        noButton.gameObject.SetActive(false);

        message.text = _message;

        yesText.text = "Ok";
       
        yesButton.onClick.RemoveAllListeners();

        yesButton.onClick.AddListener(DisablePanel);

        panel.SetActive(true);
    }

    private void DisablePanel()
    {
        panel.SetActive(false);
    }
}
