using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoinScreen : MonoBehaviour
{
    [SerializeField]
    int playerNumber;
    [SerializeField]
    GameObject playerReadyPanel;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("JoinButton" + playerNumber))
        {
            playerReadyPanel.SetActive(true);
        }
	
	}
}
