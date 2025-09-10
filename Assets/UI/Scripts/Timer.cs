using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public float StartTime = 60f;
    private float CurrentTime;
    public TextMeshProUGUI TimerText;
    public bool TimerOn = false;



    private void Start()
    {
        CurrentTime = StartTime;
        TimerOn = true;
        UpdateText();
    }

    private void Update()
    {
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
                //lose event
            }
        }
    }

    private void UpdateText()
    {
        TimerText.text = CurrentTime.ToString("00");
    }

}
