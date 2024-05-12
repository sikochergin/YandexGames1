using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMiner : MonoBehaviour
{
    private Player _player;
    public Progress _progress;
    private Pause _pause;
    public int Power = 0;
    private float _timeNow;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private PlusMinusText _pmtext;

    private void Start()
    {
        _player = FindFirstObjectByType<Player>();
        _pause = FindFirstObjectByType<Pause>();
        if (_progress.CurrentProgressData.AMlevel > 0)
        {
            Power = (int)Mathf.Pow(2, (_progress.CurrentProgressData.AMlevel-1));
        }
    }

    private void Update()
    {
        if (!_pause.IsPause)
        {
            _timeNow += Time.deltaTime;
            if (_timeNow >= 1 && Power > 0) 
            {
                Add();
                _timeNow = 0;
            }
        }
    }

    public void Add()
    {
        _player.Money += Power;
        PlusMinusText pmt = Instantiate(_pmtext, _rectTransform);
        pmt.Doing(Power, true);
        Progress.Instance.CurrentProgressData.Money = _player.Money;
        Progress.Instance.Save();
    }
}
