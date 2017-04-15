using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoinScreen : MonoBehaviour
{
    [SerializeField]
    int playerNumber;
    [SerializeField]
    GameObject playerReadyPanel;
    [SerializeField]
    GameObject readyText;

    public bool hasJoined;
    public bool isReady;
    public int playersReady = 0;
	// Use this for initialization
	void Start ()
    {
        readyText.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("JoinButton" + playerNumber))
        {
            playerReadyPanel.SetActive(true);
            hasJoined = true;
        }
        if(Input.GetButtonDown("CancelButton" + playerNumber))
        {
            if(isReady)
            {
                isReady = false;
                readyText.SetActive(false);
            }
            if(isReady == false && hasJoined)
            {
                hasJoined = false;
                playerReadyPanel.SetActive(false);
            }
        }

        CheckIfReady();
	}

    private void CheckIfReady()
    {
        if(isReady == true)
        {
            readyText.SetActive(true);
        }
    }
}
