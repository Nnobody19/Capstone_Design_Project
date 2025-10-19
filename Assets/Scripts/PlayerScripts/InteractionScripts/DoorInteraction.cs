using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour, IResetable
{
    private Vector3 _doorClosePosition;
    private bool _isDoorOpen = false;
    private bool _isDoorMove = false;

    private AudioSource _audioSource;
    public AudioClip[] DoorSounds;
    public float DoorSoundPitch = 1.2f;

    public Vector3 DoorOpenOffset = new Vector3(0, 0, -1.4f);
    public float DoorSlideSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        _doorClosePosition = transform.position;
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource != null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }
    }

    public bool IsOpen()
    {
        if (_isDoorOpen) return true;
        else return false;
    }

    public void ToggleDoor()
    {
        if (_isDoorMove) return;

        StartCoroutine(SlideDoor());
    }

    public void ResetState()
    {
        StopAllCoroutines();
        _audioSource.Stop();
        transform.position = _doorClosePosition;
        _isDoorOpen = false;
        _isDoorMove = false;
    }

    private IEnumerator SlideDoor()
    {
        _isDoorMove = true;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition;
        Vector3 worldOffset = transform.rotation * DoorOpenOffset;

        AudioClip clip;
        if (_isDoorOpen)
        {
            clip = DoorSounds[0];
            _audioSource.pitch = DoorSoundPitch;
            targetPosition = _doorClosePosition;
        }

        else
        {
            clip = DoorSounds[1];
            _audioSource.pitch = 1f;
            targetPosition = _doorClosePosition + worldOffset;
        }

        _audioSource.PlayOneShot(clip);

        float time = 0;

        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            time += Time.deltaTime * DoorSlideSpeed;
            yield return null;
        }

        transform.position = targetPosition;
        _isDoorOpen = !_isDoorOpen;
        _isDoorMove = false;
    }
}
