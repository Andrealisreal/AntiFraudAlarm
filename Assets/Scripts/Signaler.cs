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
    private float _currentVolume;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;

    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _detector = GetComponent<Detector>();
        _audioSource.clip = _audioClip;
        _audioSource.loop = true;
        _audioSource.volume = _currentVolume;
    }

    private void OnEnable()
    {
        _detector.OnEnterDetected += StartIncreaseVolume;
        _detector.OnExitDetected += StartDecreaseVolume;
    }

    private void OnDisable()
    {
        _detector.OnEnterDetected -= StartIncreaseVolume;
        _detector.OnExitDetected -= StartDecreaseVolume;
        
        if (_currentCoroutine != null)
            StopAllCoroutines();
    }

    private void StartIncreaseVolume()
    {
        StopCurrentCoroutine();
        _currentCoroutine = StartCoroutine(IncreaseCoroutine());
    }

    private void StartDecreaseVolume()
    {
        StopCurrentCoroutine();
        _currentCoroutine = StartCoroutine(DecreaseCoroutine());
    }

    private IEnumerator IncreaseCoroutine()
    {
        _audioSource.Play();
        
        while (Mathf.Approximately(_currentVolume, _maxVolume) == false)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _maxVolume, _speed * Time.deltaTime);
            _audioSource.volume = _currentVolume;
            
            yield return null;
        }
    }

    private IEnumerator DecreaseCoroutine()
    {
        while (Mathf.Approximately(_currentVolume, _minVolume) == false)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _minVolume, _speed * Time.deltaTime);
            _audioSource.volume = _currentVolume;
            
            yield return null;
        }
        
        _audioSource.Stop();
    }
    
    private void StopCurrentCoroutine()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }
}