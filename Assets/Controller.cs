using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //visual
    [SerializeField] GameObject pourStream;
    [SerializeField] GameObject fillMask;

    //error ui
    [SerializeField] GameObject e1;
    [SerializeField] GameObject e2;
    [SerializeField] GameObject e3;

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

    void Start()
    {
        //setup game on start
        audioSource = GetComponent<AudioSource>();
        fillMask.transform.position = new Vector3(0.0f, -1.024f, 0.0f);
        SetGoal();
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
                    //Debug.Log("FAILED");
                }
                else
                {
                    PourResult(true);
                    //Debug.Log("PASSED");
                }
            }
            
        }
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
        if (result == true)
        {
            cupsCompleted += 1;
            audioSource.PlayOneShot(complete);
            score.text = cupsCompleted.ToString("0");
        }
        else
        {
            cupsFailed += 1;
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
            Debug.Log("GAME OVER");
        }
        else
        {
            SetGoal();
        }
    }

}
