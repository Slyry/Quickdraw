using UnityEngine;
using System.Collections;

public class HatController : MonoBehaviour
{
    [SerializeField]
    GameObject hatGameObject;
    //Vector3 hatUpDirection = new Vector3(0f, );
    [SerializeField]
    float thrust = 2f;
    [SerializeField]
    string hatNumber = "Hat";
    public string HatNumber
    {
        get { return hatNumber; }
    }

    public GameObject HatGameObject
    {
        get { return HatGameObject; }
    }

    public void HatShotOff()
    {
        transform.parent = null;

        hatGameObject.GetComponent<Rigidbody>().AddForce(0, 0, thrust, ForceMode.Impulse);
    }
}
