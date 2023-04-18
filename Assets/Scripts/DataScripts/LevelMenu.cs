using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelMenu : MonoBehaviour
{
    public string sceneTarget, requiredLevel;

    public Button playButton;
    public TMP_Text killText, rescueText, untouchedText;

    public Color disabledColor, enabledColor;

    private Medals sceneMedal;

    private void Start()
    {
        //if (StatsManager.instance.achievemntList.ContainsKey(sceneTarget))
        //    sceneMedal = StatsManager.instance.achievemntList[sceneTarget];
        UpdateMenu();

        playButton.onClick.AddListener(GoToLevel);
        CheckIfUnlock();
        playButton.GetComponentInChildren<TMP_Text>().text = (transform.GetSiblingIndex() + 1).ToString("00");
    }

    public void UpdateMenu()
    {
        if (StatsManager.instance.achievemntList.ContainsKey(sceneTarget))
            sceneMedal = StatsManager.instance.achievemntList[sceneTarget];

        if (sceneMedal != null)
        {
            killText.color = sceneMedal.kill ? enabledColor : disabledColor;
            rescueText.color = sceneMedal.rescue ? enabledColor : disabledColor;
            untouchedText.color = sceneMedal.untouched ? enabledColor : disabledColor;
        }
        else
        {
            killText.color = disabledColor;
            rescueText.color = disabledColor;
            untouchedText.color = disabledColor;
        }

        CheckIfUnlock();
    }

    private void CheckIfUnlock()
    {
        bool unlockCondition = StatsManager.instance.levelCompleted.ContainsKey(requiredLevel) && 
            StatsManager.instance.levelCompleted[requiredLevel];
        playButton.interactable = unlockCondition || string.IsNullOrEmpty(requiredLevel) ? true : false;
    }

    private void GoToLevel()
    {
        SceneLoader.instance.ChangeScene(sceneTarget);
    }
}
