using System;
using TarodevController;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] private int levelNumber;
    [SerializeField] private Mission[] missions;
    [SerializeField] private ScriptableStats _stats;

    private Vector3 _checkPoint;
    private int _score;
    private void Awake()
    {
        if (instance) Debug.LogError("GameManager da ton tai", this);
        instance = this;
    }

    private void Start()
    {
        GameStart();
    }
    private void GameStart()
    {
        AudioMainManager.Instance.PlayMusic("GamePlay");
        _score = 0;
        _stats._canInput = false;
        _stats._canMove = true;
        _stats._canDash = false;
    }
    public void GamePlay(int progress)
    {
        switch (progress)
        {
            case 0:
                {
                    _stats._canInput = true;
                    break;
                }
            case 1:
                {
                    _stats._canInput = false;
                    break;
                }
        }
    }
    public void Gameover()
    {
        AudioMainManager.Instance.StopMusic();
        AudioMainManager.Instance.PlaySFX("GameOver");
        GameStatusManager.Instance.Gameover();
        _stats._canMove = false;
        _stats._canInput = false;
    }
    public void GameWiner()
    {
        AudioMainManager.Instance.StopMusic();
        AudioMainManager.Instance.PlaySFX("GameWiner");
        GameStatusManager.Instance.Gamewiner();
        _stats._canMove = false;
        _stats._canInput = false;
    }
    public void SetCheckPoint(Vector3 checkPoint)
    {
        _checkPoint = checkPoint;
    }
    public void ReturnToCheckPoint(GameObject player)
    {
        player.transform.position = _checkPoint;
    }
    public void FuritIncreased(GameObject furit)
    {
        foreach (var m in missions)
        {
            if (isFurits(m.furit, furit))
            {
                _score++;
                m.quantity++;
                if (m.quantity >= m.MaximumQuantity)
                {
                    m.quantity = m.MaximumQuantity;
                }
            }
        }
        AddLife.Instance.SetUI();
    }
    private bool isFurits(GameObject furit_1, GameObject furit_2)
    {
        var FURIT_1 = furit_1.GetComponentInChildren<FruitActive>().FRUITS;
        var FURIT_2 = furit_2.GetComponentInChildren<FruitActive>().FRUITS;
        return FURIT_1 == FURIT_2;
    }
    public Mission[] GetQuantityFruits()
    {
        return missions;
    }
    public int GetScore()
    {
        return _score;
    }
    public int GetMaxScore()
    {
        int Sum = 0;
        foreach(var m in missions)
        {
            Sum += m.MaximumQuantity;
        }
        return Sum;
    }
    public int GetLevel()
    {
        return levelNumber;
    }
}
[System.Serializable]
public class Mission
{
    public GameObject furit;
    public int MaximumQuantity;
    [NonSerialized] public int quantity = 0;
}
