using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour
{

    public int Health = 1;
    public GameObject PlayerController;
    public FirstPersonController FPSController;

	// Use this for initialization
	void Start ()
    {
        PlayerController.SetActive(true);
        FPSController.enabled = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (Health <= 0)
        {
            FPSController.enabled = false;
            //PlayerController.SetActive(false);
            PlayerController.GetComponent<Rigidbody>().isKinematic = false;
            PlayerController.GetComponent<CharacterController>().enabled = false;
        }
	}

    public void TakeDamage()
    {
        Health--;
    }
}
