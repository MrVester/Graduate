using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public static UIEvents current;
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
}