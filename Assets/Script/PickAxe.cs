using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AudioSource _audio;
    private SoundVol _vol;

    private bool _shoted = false;
    private float _timedel;

    public int Power = 1;

    private void Awake()
    {
        Power = (int)Mathf.Pow(2, Progress.Instance.CurrentProgressData.PALevel);
    }

    private void Start()
    {
        _vol = FindAnyObjectByType<SoundVol>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ActiveItems>())
        {
            if (!_shoted && _player.IsSooting) 
            {
                if (_vol.IsSound) _audio.volume = 2f;
                else _audio.volume = 0f;
                _audio.Play();
                _shoted = true;
                other.GetComponent<ActiveItems>().Hit(Power);
                _timedel = 0f;
            }
        }
    }

    private void Update()
    {
        _timedel += Time.deltaTime;
        if (_timedel > 0.7f && _shoted)
        {
            _timedel = 0;
            _shoted = false;
        }
    }
}
