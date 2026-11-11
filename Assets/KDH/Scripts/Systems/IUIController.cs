using System;
using UnityEngine;

public interface IUIController
{
    public GameObject TargetUI { get; }
    public event Action OnActiveChange;

    void HandleChangeActive();
}
