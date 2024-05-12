using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject _startWindow;
    [SerializeField] private WallCounter WC;
    private Pause _pause;
    private MobileInputManager _isMob;
    public bool Starter = true;


    private void Awake()
    {
        _pause = FindFirstObjectByType<Pause>();
        _isMob = FindFirstObjectByType<MobileInputManager>();
        _pause.IsPause = true;
        Cursor.lockState = CursorLockMode.None;
        Starter = true;
    }

    void Start()
    {
        Starter = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Close()
    {
        _startWindow.SetActive(false);
        _pause.IsPause = false;
        Starter = false;
        if (!_isMob.isMobile)
        {
            if (Progress.Instance.CurrentProgressData.Walls != WC.WallsAtAll)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                _pause.IsPause = true;
                Debug.Log("AAAAAAAA");
            }

        }
    }
}
