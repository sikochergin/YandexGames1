using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wall : ActiveItems
{
    [SerializeField] private ParticleSystem _particleSystemWall;
    [SerializeField] private TextMesh _text;
    public GameObject TextWall;
    public int Health;
    private WallCounter WC;

    private void Start()
    {
        WC = FindFirstObjectByType<WallCounter>();
        WC.flagWC = true;
    }

    private void Update()
    {
        _text.text = Health.ToString();
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        ParticleSystem _ps = Instantiate(_particleSystemWall, transform.position - new Vector3(0, 1.2f, 0), transform.rotation);
        _ps.Play();
        WC.flagWC = true;
        Progress.Instance.CurrentProgressData.Walls++;
        Progress.Instance.Save();
        WC.Audio.Play();
        Destroy(gameObject);
    }

    public override void Hit(int pow)
    {
        base.Hit(pow);
        Health -= pow;
    }
}
