using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    void Awake()
    {
        current = this;
    }

    public event Action onGameStop;
    public void GameStop()
    {
        onGameStop?.Invoke();
    }

    public event Action onGameStart;
    public void GameStart()
    {
        onGameStart?.Invoke();
    }

    public event Action<PolygonCollider2D> onLevelColliderChanged;
    public void LevelColliderChanged(PolygonCollider2D coll)
    {
        onLevelColliderChanged?.Invoke(coll);
    }

    public event Action onDeath;
    public void Death()
    {
        onDeath?.Invoke();
    }
}