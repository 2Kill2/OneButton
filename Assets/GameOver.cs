using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Button button;

    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    //sends player back to main menu
    void TaskOnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
