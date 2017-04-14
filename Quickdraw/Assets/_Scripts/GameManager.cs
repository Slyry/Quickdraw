using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public bool ShouldQuit = false;
    public string GameState;
    GameObject crossHair;

    [SerializeField]
    GameObject hourHand;
    [SerializeField]
    GameObject minuteHand;
    [SerializeField]
    Transform startHourRotation;
    [SerializeField]
    Transform startMinuteRotation;
    [SerializeField]
    FirstPersonController[] playersFPSControllers;

    float lastMinute;
    float lastHour;

    float startTime;
    float speed = .1f;
    float slerpTime = 60f;
    float transitionTime = 3f;
    float movementTime = 12f;
    float shootingTime = 5f;

    [SerializeField]
    AudioClip clockStrikes12;
    [SerializeField]
    AudioClip hawkSound;
    [SerializeField]
    AudioSource audio;
    [SerializeField]
    AudioSource audio2;
    // Use this for initialization
    void Start ()
    {
        GameState = "Movement";
        StartCoroutine(SwitchBetweenGameStates());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameState == "Movement")
        {
            StartCoroutine(UpdateMinuteArm());
            StartCoroutine(UpdateHourArm());
        }
    }

    void StopClockCoroutines()
    {
        StopCoroutine(UpdateMinuteArm());
        StopCoroutine(UpdateHourArm());
    }

    void RestartScene()
    {
        SceneManager.LoadScene("Game");
    }
    
    IEnumerator UpdateMinuteArm()
    {
        if (lastMinute != System.DateTime.Now.Second || lastMinute == -1)
        {

            minuteHand.transform.localRotation = Quaternion.Euler( 0, 0, - ((System.DateTime.Now.Second - startTime) / 12f) * 360f + 134.1f);
            lastMinute = System.DateTime.Now.Second;

        }
        yield return null;
    }

    IEnumerator UpdateHourArm()
    {
        if (lastHour != System.DateTime.Now.Second || lastHour == -1)
        {

            hourHand.transform.localRotation = Quaternion.Euler(0, 0, - (Mathf.Abs((System.DateTime.Now.Second - startTime)) / 150f) * 360f + 30.2f);
            lastHour = System.DateTime.Now.Second;

        }
        yield return null;
    }

    IEnumerator SwitchBetweenGameStates()
    {
        while (!ShouldQuit)
        {
            if (GameState == "Movement")
            {
                hourHand.transform.localRotation = startHourRotation.rotation;
                minuteHand.transform.localRotation = startMinuteRotation.rotation;
                startTime = System.DateTime.Now.Second;
                lastMinute = -1f;
                lastHour = -1f;
                yield return new WaitForSeconds(movementTime);

                foreach(FirstPersonController FPSCont in playersFPSControllers)
                {
                    FPSCont.enabled = false;
                }

                GameState = "PrepareToShoot";
            }
            if (GameState == "PrepareToShoot")
            {
                yield return new WaitForSeconds(transitionTime);



                GameState = "Shooting";
                audio.clip = clockStrikes12;
                audio.Play();
                audio2.clip = hawkSound;
                audio2.Play();
            }
            if (GameState == "Shooting")
            {
                yield return new WaitForSeconds(shootingTime);



                GameState = "RoundEnd";
            }
            if (GameState == "RoundEnd")
            {
                yield return new WaitForSeconds(transitionTime);

                foreach (FirstPersonController FPSCont in playersFPSControllers)
                {
                    FPSCont.enabled = true;
                }

                GameState = "Movement";
            }
        }

        yield return null;
    }
    
}
