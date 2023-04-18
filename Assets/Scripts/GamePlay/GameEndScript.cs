using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEndScript : MonoBehaviour
{
    public TMP_Text killText, rescueText, untouchedText;
    public Button exitButton;
    public Color disabledColor, enabledColor;

    private WaitForSeconds interaval = new WaitForSeconds(0.5f);

    private void OnEnable()
    {
        exitButton.onClick.AddListener(BackToMenu);
        exitButton.interactable = false;

        StartCoroutine(ShowAchievment());
    }

    private IEnumerator ShowAchievment()
    {
        yield return interaval;

        killText.color = LevelManager.instance.medals.kill ? enabledColor : disabledColor;

        yield return interaval;

        rescueText.color = LevelManager.instance.medals.rescue ? enabledColor : disabledColor;

        yield return interaval;

        untouchedText.color = LevelManager.instance.medals.untouched ? enabledColor : disabledColor;

        yield return interaval;

        exitButton.interactable = true;
    }

    private void BackToMenu()
    {
        SceneLoader.instance.ChangeScene("MainMenu");
    }
}
