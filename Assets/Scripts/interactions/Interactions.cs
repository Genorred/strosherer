using System;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;
using Vector3 = System.Numerics.Vector3;

public abstract class Interactions : MonoBehaviour
{
    public void Call()
    {
        Interact();
    }

    protected virtual void Interact()
    {
    }
}