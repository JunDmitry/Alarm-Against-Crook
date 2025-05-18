using System;
using UnityEngine;

public class House : MonoBehaviour
{
    public event Action EnteredCrook;

    public event Action ExitedCrook;

    private void OnTriggerEnter(Collider other)
    {
        if (IsCrook(other))
            EnteredCrook?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsCrook(other))
            ExitedCrook?.Invoke();
    }

    private bool IsCrook(Collider collider)
    {
        return collider.gameObject.TryGetComponent(out Crook _);
    }
}