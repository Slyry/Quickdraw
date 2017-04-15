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

    Vector3 crossHairStart = new Vector3(0f, 0f, 0f);
    public PlayerHealth playerInCrossHairs;
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

        float speed = 7f;

        float xDistanceAttempt = Mathf.Abs(Mathf.Abs(horizontalPosition + (horizontalInput * speed)) - crossHairStart.x); 
        float yDistanceAttempt = Mathf.Abs(Mathf.Abs(verticalPosition + (verticalInput * speed)) - crossHairStart.y);

        //Debug.Log(crossHair.localPosition.x);

        if (xDistanceAttempt < 350 && yDistanceAttempt < 200)
            crossHair.localPosition = new Vector3(horizontalPosition + (horizontalInput * speed), verticalPosition + (verticalInput * speed), 0f);

        if (crossHair.localPosition.y <= 0f)
            crossHair.localPosition = new Vector3(crossHair.localPosition.x, 0f, 0f);
        else if (crossHair.localPosition.y >= 360)
            crossHair.localPosition = new Vector3(crossHair.localPosition.x, 360f, 0f);

        if (crossHair.localPosition.x <= 0f)
            crossHair.localPosition = new Vector3(0f, crossHair.localPosition.y, 0f);
        else if
            (crossHair.localPosition.x >= 640f)
            crossHair.localPosition = new Vector3(640f, crossHair.localPosition.y, 0f);


        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, crossHair.position - playerCamera.transform.position, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                playerInCrossHairs = hit.transform.parent.GetComponent<PlayerHealth>();
                Debug.Log(playerInCrossHairs.name);
            }
            else
                playerInCrossHairs = null;
        }
    }
}
