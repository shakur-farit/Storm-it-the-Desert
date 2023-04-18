using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HumanRescue : MonoBehaviour
{
    public float rescueTime = 5f;
    public Image timerUI;
    public UnityEvent onRescued;

    private GameObject player;


    private void Start()
    {
        timerUI.fillAmount = 0f;

        LevelManager.instance.RegisterRescue();

        if (GameObject.FindGameObjectWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player");
    }


    private IEnumerator Rescuing(float time)
    {
        while (time > 0f)
        {
            time -= Time.deltaTime;
            UpdateUI(time);
            yield return null;

            if (!player.activeInHierarchy)
            {
                StopAllCoroutines();
                timerUI.fillAmount = 0;
            }       
        }

        onRescued.Invoke();
        LevelManager.instance.AddRescue();
        Destroy(gameObject, 1.5f);
    }

    private void UpdateUI(float time)
    {
        if(timerUI != null)
        {
            float value = 1f - (time / rescueTime);
            timerUI.fillAmount = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Rescuing(rescueTime));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            timerUI.fillAmount = 0f;
        }
    }
}
