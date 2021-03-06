﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGunControls : MonoBehaviour
{
    public int Ammo = 6;
    public int PlayerNumber;

    string FireInput;
    string ReloadInput;
    string HammerAxisInput;
    bool isCocked;
    bool wasFired = false;
    bool isShootingCoroutineRunning;
    bool isReloadCoroutineRunning;
    

    [SerializeField]
    Image[] bulletImages;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    CrossHairController playerAimInformation;
    [SerializeField]
    Animator gunAnimator;
    [SerializeField]
    AudioClip shootingSound;
    [SerializeField]
    AudioSource[] shootingAudio = new AudioSource[6];

    float hammerReset = 0;
	// Use this for initialization
	void Start ()
    {
        FireInput = "Fire" + PlayerNumber;
        ReloadInput = "Reload" + PlayerNumber;
        HammerAxisInput = "Vertical" + PlayerNumber;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxis(HammerAxisInput) != 0 && hammerReset == 0)
        {
            hammerReset = 1;
            if (!isCocked && wasFired)
            {
                isCocked = true;
                wasFired = false;
            }
        }
        else if (Input.GetAxis(HammerAxisInput) == 0)
        {
            hammerReset = 0;
            isCocked = false;
        }

        if (gameManager.GameState == "Shooting")
        {
            if (Input.GetAxis(FireInput) == 0)
                wasFired = false;

            if (Input.GetAxis(FireInput) != 0 && Ammo > 0)
            {
                if (!isCocked && !wasFired)
                {
                    if (true) //this is firing if the player hasn't "cocked" their gun.
                    {
                        Debug.Log("Fire Gun");
                        

                        for (int i = 0; i < shootingAudio.Length - 1; i++)
                        {
                            Debug.Log(shootingAudio[i]);
                            if (!shootingAudio[i].isPlaying)
                            {
                                shootingAudio[i].clip = shootingSound;
                                shootingAudio[i].Play();
                                break;
                            }
                        }
                        //shootingAudio.clip = shootingSound;
                        //shootingAudio.Play();
                        
                        gunAnimator.SetTrigger("wasFired");

                        if (playerAimInformation.HasPlayerInCrossHairs)
                            playerAimInformation.PlayerInCrossHairs.GetComponentInChildren<PlayerHealth>().TakeDamage();
                        
                        isCocked = false;
                        wasFired = true;
                        bulletImages[Ammo - 1].enabled = false;
                        //bulletImages[Ammo - 1].color = new Color(bulletImages[Ammo - 1].color.r, bulletImages[Ammo - 1].color.g, bulletImages[Ammo - 1].color.b, 50f);
                        Ammo--;
                    }
                    //if (!isShootingCoroutineRunning)
                    //    StartCoroutine(ShotDelayIfNotCocked());
                }
                else if (hammerReset == 1 && !wasFired)
                {
                    //shootingAudio.clip = shootingSound;
                    //shootingAudio.Play();
                    Debug.Log("Fire Gun");
                    if (playerAimInformation.HasPlayerInCrossHairs)
                    {
                        playerAimInformation.PlayerInCrossHairs.TakeDamage();
                    }

                    bulletImages[Ammo - 1].enabled = false;
                    
                    Ammo--;
                    isCocked = false;
                    wasFired = true;
                }
            }
            else if (Ammo == 0)
            {
                //todo play miss-fire sound.
            }

            if (Input.GetButtonDown(ReloadInput) && Ammo < 6)
            {
                if (!isReloadCoroutineRunning)
                {
                    StartCoroutine(ReloadGun());
                    StartCoroutine(ReloadAnimation());
                }
            }
        }
	}

    IEnumerator ReloadGun()
    {
        Debug.Log("Reload Gun");
        isReloadCoroutineRunning = true;
        float timeToWait = .25f * (6 - Ammo);
        yield return new WaitForSeconds(timeToWait);

        Ammo = 6;
        isReloadCoroutineRunning = false;
    }

    IEnumerator ReloadAnimation()
    {
        
        foreach (Image bulletImage in bulletImages)
        {
            if (!bulletImage.enabled)
                yield return new WaitForSeconds(.25f);
            //todo play reload bullet sound
            bulletImage.enabled = true;
            //bulletImage.color = new Color(bulletImage.color.r, bulletImage.color.g, bulletImage.color.b, 255f);
        }
        yield return null;
    }

    IEnumerator ShotDelayIfNotCocked()
    {
        isShootingCoroutineRunning = true;

        yield return new WaitForSeconds(.5f);

        Debug.Log("Fire Gun");
        //shootingAudio.clip = shootingSound;
        //shootingAudio.Play();
        if (playerAimInformation.PlayerInCrossHairs != null)
            playerAimInformation.PlayerInCrossHairs.GetComponentInChildren<PlayerHealth>().TakeDamage();
        isCocked = false;
        wasFired = true;
        bulletImages[Ammo - 1].enabled = false;
        //bulletImages[Ammo - 1].color = new Color(bulletImages[Ammo - 1].color.r, bulletImages[Ammo - 1].color.g, bulletImages[Ammo - 1].color.b, 50f);
        Ammo--;
        isShootingCoroutineRunning = false;
    }
}
