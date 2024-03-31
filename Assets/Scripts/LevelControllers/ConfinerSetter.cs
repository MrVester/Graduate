using Cinemachine;
using System.Collections;
using System.Collections.Generic;
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
        UIEvents.current.onLevelColliderChanged += SetCollider;
    }
    private void SetCollider(PolygonCollider2D coll)
    {
        confiner.m_BoundingShape2D = coll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
