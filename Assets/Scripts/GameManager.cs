using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour, IObserver
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    private float _hpCount = 50;
    public static GameManager Instance { get; private set; }

    public float HpCount
    {
        get => _hpCount;
        set
        {
            _hpCount = value;
            UpdateHP();

            if (_hpCount <= 0)
            {
                LoseGame();
            }
        }
    }

    private List<IObserver> observers = new List<IObserver>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateHP();
    }

    private void Update()
    {
        CheckWinCondition();
    }

    private void UpdateHP()
    {
        hpText.text = _hpCount.ToString();
    }

    public void OnHPChanged(float hpDelta)
    {
        HpCount += hpDelta;
    }

    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void UnregisterObserver(IObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnHPChanged(_hpCount);
        }
    }

    public void CheckWinCondition()
    {
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

        if (pickups.Length == 0)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("Player won the game!");
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
    private void LoseGame()
    {
        Debug.Log("Player lost the game!");
        loseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
public interface IObserver
{
    void OnHPChanged(float newHp);
}
