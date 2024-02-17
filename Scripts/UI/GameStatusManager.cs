using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatusManager : MonoBehaviour
{
    private static GameStatusManager instance;
    public static GameStatusManager Instance => instance;
    protected virtual void Awake()
    {
        if (instance) Debug.LogError("GameManager da ton tai", this);
        instance = this;
    }
    public void Gameover()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void Gamewiner()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public virtual void ButtonRestart()
    {
        // Lấy tên của cảnh hiện tại
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Tải lại cảnh hiện tại
        SceneManager.LoadScene(currentSceneName);
    }
    public virtual void ButtonHome()
    {
        SceneManager.LoadScene("Start");
    }
    public virtual void ButtonNextLevel() 
    {
        LevelSelectionMenuManager.currLevel = LevelSelectionMenuManager.UnlockedLevels;
        // Lấy chỉ số của cảnh hiện tại
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        // Tải cảnh tiếp theo (tăng chỉ số cảnh hiện tại lên 1)
        SceneManager.LoadScene(currentIndex + 1);
    }
}
