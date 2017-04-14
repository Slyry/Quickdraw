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

    public Transform ReadyTargetUI;

    void FixedUpdate()
    {
        HandleInput();
    }

    void HandleInput()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Xaxis" + playerNumber);
        float verticalInput = CrossPlatformInputManager.GetAxis("Yaxis" + playerNumber);

        float horizontalPosition = crossHair.position.x;
        float verticalPosition = crossHair.position.y;

        float speed = 7f;
        Debug.Log(crossHair.position.x);

        //crossHair.position = new Vector3(horizontalPosition + (horizontalInput * speed), verticalPosition + (verticalInput * speed), 0f);
        transform.Translate(horizontalInput * speed, verticalInput * speed, 0);

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
