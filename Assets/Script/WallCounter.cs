using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class WallCounter : MonoBehaviour
{
    [SerializeField] private GameObject[] _walls;
    public int WallsAtAll;
    public int i = 0;
    public bool flagWC = false;
    public AudioSource Audio;
    private SoundVol _soundVol;

    private void Awake()
    {
        WallsAtAll = _walls.Length;
    }

    private void Start()
    {
        _soundVol = FindAnyObjectByType<SoundVol>();
        if (Progress.Instance.CurrentProgressData.Walls > 0)
        {
            for (int i = 0; i < Progress.Instance.CurrentProgressData.Walls; i++)
            {
                Destroy(_walls[i]);
            }
            StartCoroutine(WT());
        }
    }

    private void Update()
    {
        if (flagWC)
        {
            StartCoroutine(WT());
            flagWC = false;
        }
        if (_soundVol.IsSound) Audio.volume = 0.4f;
        else Audio.volume = 0f;
    }

    public void TextC()
    {
        i = 0;
        while (_walls[i] == null)
        {
            i++;
            if (i == _walls.Length)
            {
                break;
            }
        }
        if (i < _walls.Length) { _walls[i].GetComponent<Wall>().TextWall.SetActive(true);}
        else FindFirstObjectByType<Player>().Win();
    }

    private IEnumerator WT()
    {
        yield return new WaitForSeconds(0.05f);
        TextC();
        
    }
}
