using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HowToPlay : MonoBehaviour
{
    public Button button;
    public GameObject Controller;

    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Controller.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
