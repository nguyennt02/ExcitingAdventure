using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCtrl : MonoBehaviour
{
    private void Start()
    {
        AudioStart.Instance.PlayMusic("GameStart");
    }
    public void OnClickBackOptions()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(false);
    }
    public void OnClickOptions()
    {
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
    }
    public void OnClickBackCredits()
    {
    /*    transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.SetActive(false);*/
    }
    public void OnClickCredits()
    {
        PlayerPrefs.SetInt("UnlockedLevels", 0);
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt("stars" + i.ToString(), 0);
        }
    }
    public void OnClickPlay()
    {
        LevelSelectionMenuManager.currLevel = LevelSelectionMenuManager.UnlockedLevels;
        SceneManager.LoadScene("Level" + LevelSelectionMenuManager.currLevel);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
