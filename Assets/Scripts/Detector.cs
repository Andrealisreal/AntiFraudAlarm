using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Detector : MonoBehaviour
{
    public event Action OnEnterDetected;
    public event Action OnExitDetected;
    
    private Collider Collider;

    private void Awake()
    {
        Collider = GetComponent<BoxCollider>();
        Collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Thief>(out _))
            OnEnterDetected?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Thief>(out _))
            OnExitDetected?.Invoke();
    }
}