using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    //visual
    [SerializeField] GameObject pourStream;
    [SerializeField] GameObject fillMask;

    //error ui
    [SerializeField] GameObject e1;
    [SerializeField] GameObject e2;
    [SerializeField] GameObject e3;
    [SerializeField] GameObject good;
    [SerializeField] GameObject bad;

    [SerializeField] GameObject gameOver;
    [SerializeField] TextMeshProUGUI gameOverScore;
    [SerializeField] TextMeshProUGUI gameOverTime;

    //text
    [SerializeField] TextMeshProUGUI order;
    [SerializeField] TextMeshProUGUI filled;
    [SerializeField] TextMeshProUGUI score;

    //sounds
    [SerializeField] AudioClip complete;
    [SerializeField] AudioClip pour;
    [SerializeField] AudioClip startPour;
    [SerializeField] AudioClip fail;
    [SerializeField] AudioClip newOrder;
    [SerializeField] AudioClip perfect;

    private AudioSource audioSource;

    private int cupsCompleted = 0;
    private int cupsFailed = 0;
    
    public float goal = 0.0f;
    private float poured = 0.0f;

    //logic
    private bool canPour = true;
    private bool pouring = false;

    //TIMER STUFF
    public float StartTime = 60f;
    private float CurrentTime;
    public TextMeshProUGUI TimerText;
    public bool TimerOn = false;

    void Start()
    {
        //setup game on start
        audioSource = GetComponent<AudioSource>();
        fillMask.transform.position = new Vector3(0.0f, -1.024f, 0.0f);
        SetGoal();
        CurrentTime = StartTime;
        TimerOn = true;
        UpdateText();
    }

    void SetGoal()
    {
        //resets everything, updates UI for order info
        goal = UnityEngine.Random.Range(0.0f, 3.0f);
        fillMask.transform.position = new Vector3(0.0f, -1.024f, 0.0f);
        poured = 0.0f;
        order.text = goal.ToString("F2");
        filled.text = poured.ToString("F2");
        canPour = true;
        audioSource.PlayOneShot(newOrder);
    }

    void Pour()
    {
        //checks if player started pouring, if not, play initial machine eject sound
        if (pouring == false)
        {
            audioSource.PlayOneShot(startPour);
        }
        pourStream.SetActive(true);
        pouring = true;

        //moves Mask upwards to show liquid filling visual, updates poured amount with delta time x 100
        fillMask.transform.position += new Vector3(0, 0.01f, 0) * Time.deltaTime * 100;
        poured += 0.01f * Time.deltaTime * 100;
        filled.text = poured.ToString("F2");
    }

    void Update()
    {
        //TIMER, too lazy to make it reference the other script, this is faster unfortunately
        if (TimerOn)
        {
            if (CurrentTime > 0)
            {
                CurrentTime -= Time.deltaTime;
                UpdateText();
                //Debug.Log(CurrentTime);
            }
            else
            {
                CurrentTime = 0;
                TimerOn = false;
                Debug.Log("Time has run out!");
                UpdateText();
                GameOver();
                //lose event
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (canPour == true)
            {
                Pour();
            }
        }
        else
        {
            //POURING STOPPED
            pourStream.SetActive(false);
            if (pouring == true)
            {
                pouring = false;
                canPour = false;
                if (poured < goal)
                {
                    PourResult(false);
                }
                else
                {
                    PourResult(true);
                }
            }
            
        }
        //Perfect fill sound plays
        if (order.text == filled.text)
        {
            audioSource.PlayOneShot(perfect);
        }
        if (pouring == true)
        {
            //audioSource.PlayOneShot(pour);
        }
    }

    void PourResult(bool result)
    {
        if (result == true && poured < 3.0f)
        {
            cupsCompleted += 1;
            good.SetActive(true);
            audioSource.PlayOneShot(complete);
            score.text = cupsCompleted.ToString("0");
        }
        else
        {
            cupsFailed += 1;
            bad.SetActive(true);
            //I don't care
            if (cupsFailed == 1)
            {
                e1.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (cupsFailed == 2)
            {
                e2.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if (cupsFailed == 3)
            {
                e3.GetComponent<SpriteRenderer>().color = Color.red;
            }
            audioSource.PlayOneShot(fail);
        }

        //if player doesnt have more than 3 fails, set up next order
        if (cupsFailed >= 3)
        {
            //fail
            TimerOn = false;
            GameOver();
        }
        else
        {
            StartCoroutine(ShowResult()); 
        }
    }

    IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(1);
        SetGoal();
        bad.SetActive(false);
        good.SetActive(false);
    }

    private void GameOver()
    {
        gameOverScore.text = score.text;
        gameOverTime.text = TimerText.text;
        gameOver.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void UpdateText()
    {
        TimerText.text = CurrentTime.ToString("00");
    }

}
