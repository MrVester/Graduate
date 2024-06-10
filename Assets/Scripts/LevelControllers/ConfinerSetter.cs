using Cinemachine;
using UnityEngine;

public class ConfinerSetter : MonoBehaviour
{
    CinemachineConfiner2D confiner;
    private void Awake()
    {
        confiner=GetComponent<CinemachineConfiner2D>();
      
    }
    private void Start()
    {
        GameEvents.current.onLevelColliderChanged += SetCollider;
    }
    private void SetCollider(PolygonCollider2D coll)
    {
        confiner.m_BoundingShape2D = coll;
    }
}
