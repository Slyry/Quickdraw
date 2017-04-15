using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour
{

    public int Health = 1;
    public GameObject PlayerController;
    public FirstPersonController FPSController;
    private Rigidbody playerRigidbody;

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
            playerRigidbody.AddForce(Physics.gravity * playerRigidbody.mass);
            PlayerController.GetComponent<CharacterController>().enabled = false;
            
        }
	}

    public void TakeDamage()
    {
        Health--;
    }
}
