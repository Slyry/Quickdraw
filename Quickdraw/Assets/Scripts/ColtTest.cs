using UnityEngine;
using System.Collections;

public class ColtTest : MonoBehaviour {

    public Animator animTest;

    private bool canAnimate;

	// Use this for initialization
	void Start ()
    {
        animTest = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Fire();
	}

    void Fire()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Setting fire to true!");
            animTest.SetBool("isIdle", false);
            animTest.SetBool("isFiring", true);
        }
        if (Input.GetKeyUp("space"))
        {
            Debug.Log("Resetting to idle.");
            animTest.SetBool("isFiring", false);
            animTest.SetBool("isIdle", true);
        }
    }
}
