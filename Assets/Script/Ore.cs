using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : ActiveItems
{
    private Player _player;
    private PickAxe _pickAxe;
    [SerializeField] private PlusMinusText _pmtext;
    [SerializeField] private RectTransform _rectTransform;

    private void Awake()
    {
        _player = FindFirstObjectByType<Player>().GetComponent<Player>();
        _pickAxe = FindFirstObjectByType<PickAxe>().GetComponent<PickAxe>();
    }

    override public void Hit(int pow)
    {
        base.Hit(pow);
        _player.Money += pow;
        PlusMinusText pmt =  Instantiate(_pmtext, _rectTransform);
        pmt.Doing(pow, true);
        Progress.Instance.CurrentProgressData.Money = _player.Money;
        Progress.Instance.Save();
    }
}
