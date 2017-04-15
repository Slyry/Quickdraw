using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JoinAndReadyManager : MonoBehaviour
{
    [SerializeField]
    GameObject startText;
    [SerializeField]
    JoinScreen joinScreenP1;
    [SerializeField]
    JoinScreen joinScreenP2;
    [SerializeField]
    JoinScreen joinScreenP3;
    [SerializeField]
    JoinScreen joinScreenP4;

    public List<JoinScreen> readyPlayersList = new List<JoinScreen>();
    public int joinedPlayers;
    public int readyPlayers;
    public bool allPlayersReady = false;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleJoinAndReady();
        CheckToStart();
	
	}

    private void CheckToStart()
    {
        //if (joinedPlayers == readyPlayers && joinedPlayers > 0 && readyPlayers > 0)
        //{
        //    startText.SetActive(true);
        //}

        foreach (JoinScreen players in readyPlayersList)
        {
            if (!players.isReady)
            {
                allPlayersReady = false;
                startText.SetActive(false);
                break;
            }
            else
            {
                allPlayersReady = true;
            }
        }
        if (allPlayersReady)
        {
            startText.SetActive(true);
        }
    }

    private void HandleJoinAndReady()
    {
        //if(joinScreenP1.isReady == true)
        //{
        //    readyPlayers = readyPlayers + 1;
        //}
        if(joinScreenP1.hasJoined)
        {
            if(!readyPlayersList.Contains(joinScreenP1))
                readyPlayersList.Add(joinScreenP1);
        }
        if(joinScreenP2.hasJoined)
        {
            if (!readyPlayersList.Contains(joinScreenP2))
                readyPlayersList.Add(joinScreenP2);
        }
        if(joinScreenP3.hasJoined)
        {
            if (!readyPlayersList.Contains(joinScreenP3))
                readyPlayersList.Add(joinScreenP3);
        }
        if(joinScreenP4.hasJoined)
        {
            if (!readyPlayersList.Contains(joinScreenP4))
                readyPlayersList.Add(joinScreenP4);
        }
    }
}
