using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionMenuManager : MonoBehaviour
{
    [SerializeField] private LevelObject[] arr_levelObjects;
    [SerializeField] private Sprite goldenStarSprite;

    public static int currLevel;
    public static int UnlockedLevels;
    public void OnClickLevel(int levelNum)
    {
        currLevel = levelNum;
        SceneManager.LoadScene("Level" + levelNum);
    }
    private void Start()
    {
        UnlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);
        for(int i = 0; i < arr_levelObjects.Length; i++)
        {
            if(UnlockedLevels >= i)
            {
                arr_levelObjects[i].levelButton.interactable = true;
                int stars = PlayerPrefs.GetInt("stars" + i.ToString(),0);
                for(int j = 0; j < stars; j++)
                {
                    arr_levelObjects[i].stars[j].sprite = goldenStarSprite;
                }
            }
        }
    }
}
