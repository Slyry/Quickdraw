using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReducingBulletUI : MonoBehaviour
{
    public int bulletcount = 6;
    public GameObject UIbullet1;
    public GameObject UIbullet2;
    public GameObject UIbullet3;
    public GameObject UIbullet4;
    public GameObject UIbullet5;
    public GameObject UIbullet6;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButtonDown(0))
        {
            bulletcount--;
            if(bulletcount == 5)
            {
                UIbullet1.SetActive(false);
            }
            else if (bulletcount == 4)
            {
                UIbullet2.SetActive(false);
            }
            else if (bulletcount == 3)
            {
                UIbullet3.SetActive(false);
            }
            else if (bulletcount == 2)
            {
                UIbullet4.SetActive(false);
            }
            else if (bulletcount == 1)
            {
                UIbullet5.SetActive(false);
            }
            else if (bulletcount == 0)
            {
                UIbullet6.SetActive(false);
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            bulletcount = 6;
            UIbullet1.SetActive(true);
            UIbullet2.SetActive(true);
            UIbullet3.SetActive(true);
            UIbullet4.SetActive(true);
            UIbullet5.SetActive(true);
            UIbullet6.SetActive(true);
        }
	
	}
}
