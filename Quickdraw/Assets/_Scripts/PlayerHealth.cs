using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour
{

    public int Health = 1;
    public GameObject PlayerController;
    public FirstPersonController FPSController;
    private Rigidbody playerRigidbody;
    [SerializeField]
    CrossHairController crossHairController;
    float pokeForce = 10f;

	// Use this for initialization
	void Start ()
    {
        PlayerController.SetActive(true);
        FPSController.enabled = true;
        playerRigidbody = PlayerController.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (Health <= 0)
        {
            FPSController.enabled = false;
            //PlayerController.SetActive(false);
            playerRigidbody.isKinematic = false;
            playerRigidbody.useGravity = true;
            playerRigidbody.AddForce(Physics.gravity * playerRigidbody.mass * 10f);
            PlayerController.GetComponent<CharacterController>().enabled = false;
            crossHairController.GetImmediateRaycastInfo();
            playerRigidbody.AddForceAtPosition(crossHairController.RayDirection * pokeForce, crossHairController.RayHitPoint);
        }
	}

    public void TakeDamage()
    {
        Health--;
    }
}
