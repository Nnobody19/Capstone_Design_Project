using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateResseter : MonoBehaviour, IResetable
{
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Vector3 _initialScale;

    private DoorInteraction _doorInteraction;

    void Awake()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _initialScale = transform.localScale;

        _doorInteraction = GetComponent<DoorInteraction>();
    }

    public void ResetState()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _initialScale = transform.localScale;

        if (_doorInteraction != null && _doorInteraction.IsOpen()) _doorInteraction.ToggleDoor();
    }
}
