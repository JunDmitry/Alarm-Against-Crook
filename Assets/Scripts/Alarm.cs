using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    private const float MaxVolume = 1.0f;
    private const float MinVolume = 0f;

    [SerializeField] private House _house;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _secondsToMaxVolume;

    private Coroutine _volumeСoroutine;

    private void OnEnable()
    {
        _house.EnteredCrook += OnEnteredCrook;
        _house.ExitedCrook += OnExitedCrook;
    }

    private void OnDisable()
    {
        _house.EnteredCrook -= OnEnteredCrook;
        _house.ExitedCrook -= OnExitedCrook;
    }

    private void OnEnteredCrook()
    {
        if (_volumeСoroutine != null)
            StopCoroutine(_volumeСoroutine);

        if (_sound.isPlaying == false)
            _sound.Play();

        float smoothDuration = _secondsToMaxVolume - _sound.volume / MaxVolume * _secondsToMaxVolume;
        _volumeСoroutine = StartCoroutine(ChangeVolumeSmoothly(MaxVolume, smoothDuration));
    }

    private void OnExitedCrook()
    {
        if (_volumeСoroutine != null)
            StopCoroutine(_volumeСoroutine);

        float smoothDuration = _secondsToMaxVolume - (MaxVolume - _sound.volume) / MaxVolume * _secondsToMaxVolume;
        _volumeСoroutine = StartCoroutine(ChangeVolumeSmoothly(MinVolume, smoothDuration));
    }

    private IEnumerator ChangeVolumeSmoothly(float targetVolume, float smoothDuration)
    {
        float sourceVolume = _sound.volume;
        float elapsedSeconds = 0f;

        while (elapsedSeconds < smoothDuration && enabled)
        {
            elapsedSeconds += Time.deltaTime;

            _sound.volume = Mathf.MoveTowards(sourceVolume, targetVolume, elapsedSeconds / _secondsToMaxVolume);
            yield return null;
        }

        _sound.volume = targetVolume;

        if (Mathf.Approximately(_sound.volume, MinVolume))
            _sound.Stop();
    }
}