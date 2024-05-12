using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVol : MonoBehaviour
{
    public bool IsSound = true;
    [SerializeField] private AudioSource music;
    public bool muted = false;
    [SerializeField] private GameObject _sOff;
    [SerializeField] private GameObject _sOn;
    [SerializeField] private Pause _pause;
    [SerializeField] private StartMenu _startMenu;

    private bool wasPaused;

    private void Start()
    {
        music.Play();
    }

    private void Update()
    {
        if (IsSound ) music.volume = 0.1f;
        else music.volume = 0f;
    }

    void OnApplicationFocus(bool hasFocus)
    {

        if (!_startMenu.Starter)
        {
            if (!hasFocus)
            {
                if (_pause.IsPause)
                {
                    wasPaused = true;
                }
                else
                {
                    wasPaused = false;
                }
                IsSound = false;
                music.volume = 0f;
                _pause.IsPause = true;
            }
            else
            {
                if (!muted) { music.volume = 0.1f; IsSound = true; }
                if (!wasPaused) _pause.IsPause = false;
            }
        }
    }

    public void SON()
    {
        muted = !muted;
        if (!muted) { IsSound = true; _sOn.SetActive(true); _sOff.SetActive(false); }
        else { IsSound = false; _sOn.SetActive(false); _sOff.SetActive(true); }
    }
}
