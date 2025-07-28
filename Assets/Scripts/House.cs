using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Detector _detector;
    [SerializeField] private Signaler _signaler;

    private void OnEnable()
    {
        _detector.EnterDetecting += _signaler.StartIncreaseVolume;
        _detector.ExitDetecting += _signaler.StartDecreaseVolume;
    }

    private void OnDisable()
    {
        _detector.EnterDetecting -= _signaler.StartIncreaseVolume;
        _detector.ExitDetecting -= _signaler.StartDecreaseVolume;
    }
}
