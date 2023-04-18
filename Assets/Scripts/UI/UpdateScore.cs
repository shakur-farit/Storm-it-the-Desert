using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{
    public static UpdateScore instance;

    public TMP_Text scoreDisplay;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scoreDisplay.text = "Score: 000000";
    }

    public void DisplayScore(int value)
    {
        scoreDisplay.text = "Score: " + value.ToString("000000");
    }
}
