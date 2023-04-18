using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIScreen[] screenArray;

    private UIScreen currentScreen, previousScreen;

    private bool isChanging;

    private void Start()
    {
        screenArray = GetComponentsInChildren<UIScreen>(true);

        Init(0);
    }

    private void Init(int defaultUI)
    {
        for (int i = 0; i < screenArray.Length; i++)
        {
            if (i == defaultUI)
            {
                screenArray[i].Init(true);
                currentScreen = screenArray[i];
            }
            else
            {
                screenArray[i].Init(false);
            }
        }
    } 

    public void ChangeScreen(UIScreen newScreen)
    {

        if (isChanging || currentScreen == newScreen)
            return;

        if (newScreen)
        {
            isChanging= true;

            newScreen.DoneChange -= DoneSwitching;
            newScreen.DoneChange += DoneSwitching;

            if (currentScreen)
            {
                previousScreen = currentScreen;

                previousScreen.Hide();
            }

            currentScreen = newScreen;

            currentScreen.Show();
        }
    }

    private void DoneSwitching()
    {
        isChanging = false;
    }
}
