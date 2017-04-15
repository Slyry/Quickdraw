using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class JoinScreenCrossHairController : MonoBehaviour
{
    [SerializeField]
    int playerNumber;
    [SerializeField]
    RectTransform crossHair;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    JoinScreen joinScreen;

    Vector3 crossHairStart = new Vector3(0f, 0f, 0f);
    public Transform ReadyTargetUI;
    string FireInput;
    string HammerAxisInput;
    float hammerReset = 0;
    bool isCocked;
    bool wasFired = true;
    bool isShootingCoroutineRunning;
    bool isOnTarget = false;
    public int Ammo;

    void Start()
    {
        crossHairStart = crossHair.localPosition;
        //joinScreen = gameObject.GetComponent<JoinScreen>();
        FireInput = "Fire" + playerNumber;
        HammerAxisInput = "Vertical" + playerNumber;
    }

    void Update()
    {
        if (Input.GetAxis(HammerAxisInput) != 0 && hammerReset == 0)
        {
            hammerReset = 1;
            if (!isCocked && wasFired)
            {
                isCocked = true;
                wasFired = false;
            }
        }
        else if (Input.GetAxis(HammerAxisInput) == 0)
        {
            hammerReset = 0;
            isCocked = false;
        }

        if (Input.GetAxis(FireInput) != 0 && Ammo > 0)
        {
            if (!isCocked && !wasFired)
            {
                if (!isShootingCoroutineRunning)
                    StartCoroutine(ShotDelayIfNotCocked());
            }
            else if (hammerReset == 1 && !wasFired)
            {
                //audio.clip = shootingSound;
                //audio.Play();
                Debug.Log("Fire Gun");
                if (isOnTarget)
                    joinScreen.isReady = true;
                //bulletImages[Ammo - 1].enabled = false;
                //Ammo--;
                isCocked = false;
                wasFired = true;
            }
        }
        
    }

    IEnumerator ShotDelayIfNotCocked()
    {
        isShootingCoroutineRunning = true;

        yield return new WaitForSeconds(.5f);

        Debug.Log("Fire Gun");
        //audio.clip = shootingSound;
        //audio.Play();
        if (ReadyTargetUI != null)
            joinScreen.isReady = true;
        isCocked = false;
        wasFired = true;
        if(isOnTarget)
        {
            joinScreen.isReady = true;
        }
        //bulletImages[Ammo - 1].enabled = false;
        //bulletImages[Ammo - 1].color = new Color(bulletImages[Ammo - 1].color.r, bulletImages[Ammo - 1].color.g, bulletImages[Ammo - 1].color.b, 50f);
        //Ammo--;
        isShootingCoroutineRunning = false;
    }

    void FixedUpdate()
    {
        HandleInput();
    }

    void HandleInput()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("CrosshairXAxis" + playerNumber);
        float verticalInput = CrossPlatformInputManager.GetAxis("CrosshairYAxis" + playerNumber);

        float horizontalPosition = crossHair.localPosition.x;
        float verticalPosition = crossHair.localPosition.y;

        float speed = 10f;
        Debug.Log(crossHair.position.x);

        float xDistanceAttempt = Mathf.Abs(Mathf.Abs(horizontalPosition + (horizontalInput * speed)) - crossHairStart.x);
        float yDistanceAttempt = Mathf.Abs(Mathf.Abs(verticalPosition + (verticalInput * speed)) - crossHairStart.y);

        //Debug.Log(crossHair.localPosition.x);

        if (xDistanceAttempt < 500 && yDistanceAttempt < 400)
            crossHair.localPosition = new Vector3(horizontalPosition + (horizontalInput * speed), verticalPosition + (verticalInput * speed), 0f);

        if (crossHair.localPosition.y <= -270f)
            crossHair.localPosition = new Vector3(crossHair.localPosition.x, -270f, 0f);
        else if (crossHair.localPosition.y >= 270f)
            crossHair.localPosition = new Vector3(crossHair.localPosition.x, 270f, 0f);

        if (crossHair.localPosition.x <= -480f)
            crossHair.localPosition = new Vector3(-480f, crossHair.localPosition.y, 0f);
        else if
            (crossHair.localPosition.x >= 480f)
            crossHair.localPosition = new Vector3(480f, crossHair.localPosition.y, 0f);

        //crossHair.position = new Vector3(horizontalPosition + (horizontalInput * speed), verticalPosition + (verticalInput * speed), 0f);
        //transform.Translate(horizontalInput * speed, verticalInput * speed, 0);

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(crossHair.position);

        if (Physics.Raycast(mainCamera.transform.position, crossHair.position - mainCamera.transform.position, out hit))
        {
            if (hit.transform.tag == "Target")
            {
                isOnTarget = true;
                ReadyTargetUI = hit.transform;
                Debug.Log(ReadyTargetUI.name);
                //Debug.Log("Parent " + playerInCrossHairs.Health);
                //Debug.Log("Parent " + playerInCrossHairs.PlayerController);
            }
            else
                isOnTarget = false;
            }
        }
        
    }
