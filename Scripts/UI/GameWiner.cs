using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameWiner : GameOver
{
    [SerializeField] private TextMeshProUGUI leveName;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite img_star;
    private int starNumber;
    protected override void Start()
    {
        base.Start();
        LevelComplete();
    }
    private int SetStarNumber()
    {
        int score = GameManager.Instance.GetScore();
        int MaxScore = GameManager.Instance.GetMaxScore();
        return score * 3 / MaxScore;
    }
    public override void SetUI()
    {
        base.SetUI();
        int levelNumber = GameManager.Instance.GetLevel();
        leveName.SetText("Level " + levelNumber);
        starNumber = SetStarNumber();
        for(int i = 0; i < starNumber; i++)
        {
            stars[i].sprite = img_star;
        }
    }
    private void LevelComplete()
    {
        if (LevelSelectionMenuManager.currLevel == LevelSelectionMenuManager.UnlockedLevels)
        {
            LevelSelectionMenuManager.UnlockedLevels++;
            PlayerPrefs.SetInt("UnlockedLevels", LevelSelectionMenuManager.UnlockedLevels);
        }
        if(starNumber > PlayerPrefs.GetInt("stars" + LevelSelectionMenuManager.currLevel.ToString(), 0))
        {
            PlayerPrefs.SetInt("stars" + LevelSelectionMenuManager.currLevel.ToString(), starNumber);
        }
    }
}
