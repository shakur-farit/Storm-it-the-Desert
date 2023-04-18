using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PowerUpMenu : MonoBehaviour
{
    public Button shieldButton, laserButton, megaBombButton;
    public TMP_Text shieldText, laserText, megaBombText;
    public int shieldAmount = 3, laserAmount = 3, megaBombAmount = 1;

    public Shield shield;
    public Laser laser;
    public MegaBomb megaBomb;

    private float defaultPhysicsStep;


    private void Start()
    {
        shieldText.text = "S" + shieldAmount;
        laserText.text = "L" + laserAmount;
        megaBombText.text = "M" + megaBombAmount;

        shieldButton.onClick.AddListener(EnableShield);
        laserButton.onClick.AddListener(EnableLaser);
        megaBombButton.onClick.AddListener(EnableMegaBomb);

        defaultPhysicsStep = Time.fixedDeltaTime;
    }

    private void Update()
    {
#if UNITY_EDITOR
        BulletTime(Input.GetMouseButton(1));
#elif UNITY_ANDROID
        BulletTime(Input.touchCount == 0 || EventSystem.current.IsPointerOverGameObject(0));
#endif
    }

    private void BulletTime(bool slow)
    {
        if (slow)
        {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = defaultPhysicsStep * Time.timeScale;
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = defaultPhysicsStep;
        }

        shieldButton.gameObject.SetActive(slow);
        laserButton.gameObject.SetActive(slow);
        megaBombButton.gameObject.SetActive(slow);
    }

    private void EnableShield()
    {
        if (shieldAmount == 0)
            return;

        shield.ShieldUp();

        shieldAmount--;

        shieldText.text = "S" + shieldAmount;

        if (shieldAmount == 0)
            shieldButton.interactable = false;
    }

    private void EnableLaser()
    {
        if (laserAmount == 0)
            return;

        laser.ShootLaser();

        laserAmount--;

        laserText.text = "L" + laserAmount;

        if (laserAmount == 0)
            laserButton.interactable = false;
    }

    private void EnableMegaBomb()
    {
        if (megaBombAmount == 0)
            return;

        megaBomb.DeployBomb();

        megaBombAmount--;

        megaBombText.text = "M" + megaBombAmount;

        if (megaBombAmount == 0)
            megaBombButton.interactable = false;
    }
}
