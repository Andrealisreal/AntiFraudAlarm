using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Detector))]
[RequireComponent(typeof(AudioSource))]
public class Signaler : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;

    private Detector _detector;
    private AudioSource _audioSource;

    private float _speed = 0.1f;
    private float _maxVolume = 1f;
    private float _minVolume;
    private float _currentVolume;

    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _detector = GetComponent<Detector>();
        
        _audioSource.clip = _audioClip;
        _audioSource.loop = true;
        _audioSource.volume = _currentVolume;
    }

    private void OnDisable()
    {
        if (_currentCoroutine != null)
            StopAllCoroutines();
    }

    public void StartIncreaseVolume()
    {
        StopAllCoroutines();
        _audioSource.Play();
        _currentCoroutine = StartCoroutine(ChangeVolumeCoroutine(_maxVolume));
    }

    public void StartDecreaseVolume()
    {
        StopAllCoroutines();
        _currentCoroutine = StartCoroutine(ChangeVolumeCoroutine(_minVolume));
        
        if(Mathf.Approximately(_currentVolume, _minVolume))
            _audioSource.Stop();
    }

    private IEnumerator ChangeVolumeCoroutine(float targetVolume)
    {
        while (Mathf.Approximately(_currentVolume, targetVolume) == false)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, targetVolume, _speed * Time.deltaTime);
            _audioSource.volume = _currentVolume;
            
            yield return null;
        }
    }
}