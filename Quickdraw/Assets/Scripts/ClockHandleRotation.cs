using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClockHandleRotation : MonoBehaviour {

    [SerializeField]
    GameObject ClockHand;

    float zRotation = -1440;

   public float time = 6;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

       // time -= 1 * Time.deltaTime;
        zRotation = -100f * Time.deltaTime;


         ClockHand.transform.Rotate(0, 0, zRotation);

        print(time);


        if (0 == 0)
        { //ClockHand.transform.Rotate(0, 0, -1440);
            Debug.Log("i suppose to stop");
            StartCoroutine(Wait());
        }

	
	}

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
    }
}
