using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class CrossHairController : MonoBehaviour
{
    [SerializeField]
    int playerNumber;
    [SerializeField]
    RectTransform crossHair;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    Camera playerCamera;

    bool hasPlayerInCrossHairs = false;
    public bool HasPlayerInCrossHairs
    {
        get { return hasPlayerInCrossHairs; }
    }
    PlayerHealth playerInCrossHairs;
    public PlayerHealth PlayerInCrossHairs
    {
        get { return playerInCrossHairs; }
    }

    Vector3 crossHairStart = new Vector3(0f, 0f, 0f);
    // Use this for initialization
    void Start ()
    {
        crossHairStart = crossHair.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void FixedUpdate()
    {
        if (gameManager.GameState == "Shooting")
        {
            HandleInput();
        }
        else
        {
            crossHair.localPosition = crossHairStart;
        }
            
    }

    void HandleInput()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("CrosshairXAxis" + playerNumber);
        float verticalInput = CrossPlatformInputManager.GetAxis("CrosshairYAxis" + playerNumber);

        float horizontalPosition = crossHair.localPosition.x;
        float verticalPosition = crossHair.localPosition.y;

        float speed = 10f;

        float xDistanceAttempt = Mathf.Abs(Mathf.Abs(horizontalPosition + (horizontalInput * speed)) - crossHairStart.x); 
        float yDistanceAttempt = Mathf.Abs(Mathf.Abs(verticalPosition + (verticalInput * speed)) - crossHairStart.y);

        //Debug.Log(crossHair.localPosition.x);

        if (xDistanceAttempt < 550 && yDistanceAttempt < 400)
            crossHair.localPosition = new Vector3(horizontalPosition + (horizontalInput * speed), verticalPosition + (verticalInput * speed), 0f);

        if (crossHair.localPosition.y <= -275f)
            crossHair.localPosition = new Vector3(crossHair.localPosition.x, -275f, 0f);
        else if (crossHair.localPosition.y >= 275f)
            crossHair.localPosition = new Vector3(crossHair.localPosition.x, 275f, 0f);

        if (crossHair.localPosition.x <= -490f)
            crossHair.localPosition = new Vector3(-490f, crossHair.localPosition.y, 0f);
        else if
            (crossHair.localPosition.x >= 490f)
            crossHair.localPosition = new Vector3(490f, crossHair.localPosition.y, 0f);


        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, crossHair.position - playerCamera.transform.position, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                hasPlayerInCrossHairs = true;
                playerInCrossHairs = hit.transform.GetComponentInChildren<PlayerHealth>();
                Debug.Log(playerInCrossHairs.name);
            }
            else if (hit.transform.tag == "Hat")
            {
                playerInCrossHairs = null;
                hasPlayerInCrossHairs = false;

                Debug.Log(hit.transform.GetComponent<HatController>());
                hit.transform.GetComponent<HatController>().HatShotOff();
            }
            else
            {
                playerInCrossHairs = null;
                hasPlayerInCrossHairs = false;
            }
        }
    }
}
