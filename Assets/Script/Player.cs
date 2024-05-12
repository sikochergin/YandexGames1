using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float sens = 10f;
    [SerializeField] private float sensP = 10f;
    [SerializeField] private Joystick _joystick;
    [SerializeField] public Animator Anim;
    [SerializeField] private ActiveItems _activeItems;
    [SerializeField] private TMP_Text _monText;
    [SerializeField] public TMP_Text _paText;
    [SerializeField] public TMP_Text _amText;
    [SerializeField] public GameObject _winWindow;
    private CameraControllerPanel cameraControllerPanel;
    private Rigidbody _rigidbody;
    private Transform _playerPos;
    private MobileInputManager _isMob;
    private Pause _pause;
    private bool flagtouch = true;
    private bool flagrunW = false;
    private bool flagrunA = false;
    private bool flagrunS = false;
    private bool flagrunD = false;
    private bool flagstop = false;
    private bool butDown = false;
    public bool MouseOn = false;
    public bool IsSooting = false;
    public bool IsSootingGGG = true;

    public int Money = 0;

    private float _pa = 0;
    private float _timenow = 0.8f;
    private float _timePA = 1f;


    // Start is called before the first frame update
    void Awake()
    {
        cameraControllerPanel = FindFirstObjectByType<CameraControllerPanel>();
        Money = Progress.Instance.CurrentProgressData.Money;
        _rigidbody = GetComponent<Rigidbody>();
        _playerPos = GetComponent<Transform>();
        _isMob = FindFirstObjectByType<MobileInputManager>();
        _pause = FindFirstObjectByType<Pause>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_pause.IsPause)
        {
            if (Progress.Instance.CurrentProgressData.PALevel > 21)
            {
                _pa = Mathf.Pow(2, Progress.Instance.CurrentProgressData.PALevel);
                _paText.text = Mathf.Floor(_pa / 1000).ToString() + (_pa % 1000).ToString() + " / удар";
            }
            else
            {
                _paText.text = Mathf.Pow(2, Progress.Instance.CurrentProgressData.PALevel).ToString() + " / удар";
            }
            if (Progress.Instance.CurrentProgressData.AMlevel > 0) { _amText.text = (Mathf.Pow(2, Progress.Instance.CurrentProgressData.AMlevel) / 2).ToString() + " / сек"; }
            else { _amText.text = "0 / сек"; }
            _monText.text = Money.ToString();
            _timePA += Time.deltaTime;
            if (!_isMob.isMobile)
            {
                if (Cursor.lockState == CursorLockMode.None) { Cursor.lockState = CursorLockMode.Locked; }

                float turnx = Input.GetAxis("Horizontal");
                float turny = Input.GetAxis("Vertical");
                Vector3 inVect = new Vector3(turnx, 0f, turny);
                Vector3 WorldVect = _playerPos.TransformVector(inVect) * _speed;
                _rigidbody.velocity = new Vector3(WorldVect.x, _rigidbody.velocity.y, WorldVect.z);
                if (_timePA > 1.35f)
                {
                    IsSooting = false;
                    if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
                    {
                        if (!flagrunW)
                        {
                            Anim.SetTrigger("Run");
                            flagrunW = true;
                            flagstop = false;
                            flagrunD = false;
                            flagrunA = false;
                            flagrunS = false;
                        }
                    }
                    else if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
                    {
                        if (!flagrunA)
                        {
                            Anim.SetTrigger("Left");
                            flagrunA = true;
                            flagstop = false;
                            flagrunD = false;
                            flagrunS = false;
                            flagrunW = false;
                        }
                    }
                    else if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
                    {
                        if (!flagrunS)
                        {
                            Anim.SetTrigger("Back");
                            flagrunS = true;
                            flagstop = false;
                            flagrunD = false;
                            flagrunA = false;
                            flagrunW = false;
                        }
                    }
                    else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
                    {
                        if (!flagrunD)
                        {
                            Anim.SetTrigger("Right");
                            flagrunD = true;
                            flagstop = false;
                            flagrunS = false;
                            flagrunA = false;
                            flagrunW = false;
                        }
                    }
                    if (!Input.GetKey("w") && !Input.GetKey("d") && !Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow))
                    {
                        if (!flagstop)
                        {
                            Anim.SetTrigger("Stop");
                            flagstop = true;
                            flagrunD = false;
                            flagrunS = false;
                            flagrunA = false;
                            flagrunW = false;
                        }
                    }
                    if (Input.GetMouseButton(0))
                    {
                        ShotPA();
                    }
                }

                

                float mousex = Input.GetAxis("Mouse X");
                _playerPos.localEulerAngles += new Vector3(0f, mousex * sensP, 0f);

            }
            else
            {
                float horizontalInput = _joystick.Horizontal;
                float verticalInput = _joystick.Vertical;
                Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
                _rigidbody.velocity = new Vector3(moveDirection.x, 0, moveDirection.z) * _speed;

                if (butDown)
                {
                    ShotPA();
                }
                if (_timePA > 1f) IsSooting = false;

                if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
                {

                    if (Mathf.Abs(_joystick.Horizontal) < Mathf.Abs(_joystick.Vertical) && _joystick.Vertical > 0)
                    {
                        if (!flagrunW)
                        {
                            Anim.SetTrigger("Run");
                            flagrunW = true;
                            flagstop = false;
                            flagrunA = false;
                            flagrunS = false;
                            flagrunD = false;
                        }
                    }
                    else if (Mathf.Abs(_joystick.Horizontal) < Mathf.Abs(_joystick.Vertical) && _joystick.Vertical < 0)
                    {
                        if (!flagrunS)
                        {

                            Anim.SetTrigger("Back");
                            flagrunS = true;
                            flagstop = false;
                            flagrunA = false;
                            flagrunW = false;
                            flagrunD = false;

                        }
                    }
                    else if (Mathf.Abs(_joystick.Horizontal) > Mathf.Abs(_joystick.Vertical) && _joystick.Horizontal > 0)
                    {
                        if (!flagrunD)
                        {
                            Anim.SetTrigger("Right");
                            flagrunD = true;
                            flagstop = false;
                            flagrunA = false;
                            flagrunS = false;
                            flagrunW = false;
                        }
                    }
                    else if (Mathf.Abs(_joystick.Horizontal) > Mathf.Abs(_joystick.Vertical) && _joystick.Horizontal < 0)
                    {
                        if (!flagrunA)
                        {

                            Anim.SetTrigger("Left");
                            flagrunA = true;
                            flagstop = false;
                            flagrunS = false;
                            flagrunW = false;
                            flagrunD = false;

                        }
                    }
                    else
                    {
                        if (!flagstop)
                        {
                            Anim.SetTrigger("Stop");
                            flagstop = true;
                            flagrunD = false;
                            flagrunS = false;
                            flagrunA = false;
                            flagrunW = false;
                        }
                    }

                }
                else
                {
                    if (!flagstop)
                    {
                        Anim.SetTrigger("Stop");
                        flagstop = true;
                        flagrunW = false;
                    }
                }
                float mouseX = 0;
                if (cameraControllerPanel.pressed)
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (touch.fingerId == cameraControllerPanel.fingerId)
                        {
                            if (touch.phase == TouchPhase.Moved)
                            {
                                mouseX = -1 * touch.deltaPosition.x * sens;
                            }

                            if (touch.phase == TouchPhase.Stationary)
                            {
                                mouseX = 0;
                            }
                        }
                    }
                }
                _playerPos.localEulerAngles += new Vector3(0f, mouseX * sens, 0f);

            }
        }
    }

    public void ShotPA()
    {
        if (_timePA > 1.35f) 
        {
            Anim.SetTrigger("Shot");
            _timePA = 0f;
            StartCoroutine(WaitShot());
            IsSootingGGG = true;
        }
    }

    private IEnumerator WaitShot()
    {
        yield return new WaitForSeconds(0.25f);
        IsSooting = true;
        IsSootingGGG = false;
    }


    public void PointerDown()
    {
        butDown = true;
    }

    public void PointerUp()
    {
        butDown = false;
    }

    public void Win()
    {
        _winWindow.SetActive(true);
        _pause.IsPause = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
