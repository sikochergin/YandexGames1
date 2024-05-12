using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlusMinusText : MonoBehaviour
{
    [SerializeField] private RectTransform _position;
    [SerializeField] private Animator _anim;
    [SerializeField] private TMP_Text _text;
    private int a = 0;

    void Start()
    {
        a = Random.Range(-350, -180);
        _position.anchoredPosition = new Vector2(a, 40);
    }

    public void Doing(int mon, bool po)
    {
        if (po)
        {
            _anim.SetTrigger("Green");
            _text.text = "+" + mon.ToString();
        }
        else
        {
            _anim.SetTrigger("Red");
            _text.text = "-" + mon.ToString();
        }
        StartCoroutine(Dest());
    }

    private IEnumerator Dest()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
