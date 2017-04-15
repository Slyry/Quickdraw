using UnityEngine;
using System.Collections;

public class HatController : MonoBehaviour
{
    [SerializeField]
    GameObject hatGameObject;
    public GameObject HatGameObject
    {
        get { return HatGameObject; }
    }
    //Vector3 hatUpDirection = new Vector3(0f, );
    [SerializeField]
    float thrust = 2f;
    [SerializeField]
    string hatName = "Hat";
    public string HatName
    {
        get { return hatName; }
    }

    WaitForSeconds waitForHatToFlyUp = new WaitForSeconds(1f);

    public void HatShotOff()
    {
        transform.parent = null;

        //hatGameObject.GetComponent<Rigidbody>().AddForce(0f, thrust, 0f, ForceMode.Impulse);

        StartCoroutine(WaitThenMakePhysical());
    }

    IEnumerator WaitThenMakePhysical()
    {
        yield return waitForHatToFlyUp;

        hatGameObject.GetComponent<Collider>().isTrigger = false;

        hatGameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
