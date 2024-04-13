using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnLevelLoaded;
    public static event Action OnGameStart;
    
    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }
    
    public void LevelLoaded()
    {
        OnLevelLoaded?.Invoke();
        OnGameStart?.Invoke();
    }
}
