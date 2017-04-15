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

    Vector3 crossHairStart = new Vector3(0f, 0f, 0f);
    public Transform ReadyTargetUI;

    void Start()
    {
        crossHairStart = crossHair.localPosition;
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

        float speed = 7f;
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

        //if (Physics.Raycast(ray, out hit))
        //{
        //    if (hit.transform.tag == "Player")
        //    {
        //        playerInCrossHairs = hit.transform;
        //        Debug.Log(playerInCrossHairs.name);
        //        //Debug.Log("Parent " + playerInCrossHairs.Health);
        //        //Debug.Log("Parent " + playerInCrossHairs.PlayerController);
        //    }
        //}
    }
}
