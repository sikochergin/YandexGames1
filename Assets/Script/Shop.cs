using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject _predlozenie;
    [SerializeField] private GameObject _predlozenieMOB;
    [SerializeField] private GameObject _shopWindow;
    [SerializeField] private GameObject _PAbutton;
    [SerializeField] private GameObject _AMbutton;
    [SerializeField] private Player _player;
    [SerializeField] private ShopPickaxe _shopPA;
    [SerializeField] private ShopAutoMiner _shopAM;
    [SerializeField] private TMP_Text _textMon;
    [SerializeField] private TMP_Text _textPricePA;
    [SerializeField] private TMP_Text _textPriceAM;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Animator _pa;
    [SerializeField] private Animator _am;
    private MobileInputManager _isMob;
    private AutoMiner _autoMiner;
    private PickAxe _pichAxe;
    private Pause _pause;
    private AudioSource _audioSource;
    private SoundVol _soundVol;
    [SerializeField] private PlusMinusText _pmtext;
    public bool InZone=false;
    public bool WinOpen=false;
    public int PickaxeLevel = 0;
    public int AutoMinerLevel = 0;

    private bool maxPA = false;
    private bool maxAM = false;


    private void Start()
    {
        _isMob = FindFirstObjectByType<MobileInputManager>();
        _pause = FindFirstObjectByType<Pause>();
        _pichAxe = FindFirstObjectByType<PickAxe>();
        _autoMiner = FindFirstObjectByType<AutoMiner>();
        _soundVol = FindFirstObjectByType<SoundVol>();
        PickaxeLevel = Progress.Instance.CurrentProgressData.PALevel;
        AutoMinerLevel = Progress.Instance.CurrentProgressData.AMlevel;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            if (!_isMob.isMobile) { _predlozenie.SetActive(true); }
            else { _predlozenieMOB.SetActive(true); }
            InZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            if (!_isMob.isMobile) { _predlozenie.SetActive(false); }
            else { _predlozenieMOB.SetActive(false); }
            InZone = false;
        }
    }

    private void Update()
    {
        if (InZone)
        {
            if (!_isMob.isMobile)
            {
                if (Input.GetKeyDown("b"))
                {
                    _player.Anim.SetTrigger("Stop");
                    Open();
                }
            }
        }
        if (maxPA)
        {
            _PAbutton.SetActive(false);
        }
        if (maxAM)
        {
            _AMbutton.SetActive(false);
        }
    }

    public void Open()
    {
        WinOpen = true;
        if (!_isMob.isMobile) { _predlozenie.SetActive(false); }
        else { _predlozenieMOB.SetActive(false); }
        
        _shopWindow.SetActive(true);
        _pause.IsPause = true;
        if (!_isMob.isMobile) Cursor.lockState = CursorLockMode.None;
        if (AutoMinerLevel >= 20)
        {
            maxAM = true;
        }
        if (PickaxeLevel >= 25)
        {
            maxPA = true;
        }
        Stat();
    }

    public void Close()
    {
        WinOpen = false;
        if (!_isMob.isMobile) { _predlozenie.SetActive(true); }
        else { _predlozenieMOB.SetActive(true); }
        _shopWindow.SetActive(false);
        _pause.IsPause = false;
        if (!_isMob.isMobile) Cursor.lockState = CursorLockMode.Locked;
    }

    public void BuyP()
    {
        if (_player.Money >= _shopPA.Price[PickaxeLevel])
        {

            if (_soundVol.IsSound) _audioSource.volume = 0.3f;
            else _audioSource.volume = 0f;
            _audioSource.Play();
            PlusMinusText pmt = Instantiate(_pmtext, _rectTransform);
            pmt.Doing(_shopPA.Price[PickaxeLevel], false);
            _player.Money -= _shopPA.Price[PickaxeLevel];
            _pichAxe.Power = _shopPA.Skill[PickaxeLevel];
            PickaxeLevel += 1;
            
            if (PickaxeLevel >= 25)
            {
                maxPA = true;
                StatMax();
            }
            else
            {
                Stat();
            }
            Progress.Instance.CurrentProgressData.Money = _player.Money;
            Progress.Instance.CurrentProgressData.PALevel++;
            Progress.Instance.Save();
            _textMon.text = _player.Money.ToString();
            _player._paText.text = Mathf.Pow(2, Progress.Instance.CurrentProgressData.PALevel).ToString() + " / удар";
            if (Progress.Instance.CurrentProgressData.AMlevel > 0) { _player._amText.text = (Mathf.Pow(2, Progress.Instance.CurrentProgressData.AMlevel) / 2).ToString() + " / сек"; }
            else { _player._amText.text = "0 / сек"; }
        }
        else
        {
            _pa.SetTrigger("No");
        }
    }

    public void BuyA()
    {
        if (_player.Money >= _shopAM.Price[AutoMinerLevel])
        {
            if (_soundVol.IsSound) _audioSource.volume = 0.3f;
            else _audioSource.volume = 0f;
            _audioSource.Play();
            PlusMinusText pmt = Instantiate(_pmtext, _rectTransform);
            pmt.Doing(_shopAM.Price[AutoMinerLevel], false);
            _player.Money -= _shopAM.Price[AutoMinerLevel];
            _autoMiner.Power = _shopAM.Skill[AutoMinerLevel];
            AutoMinerLevel += 1;
            if (AutoMinerLevel >= 20)
            {
                maxAM = true;
                StatMax();
            }
            else
            {
                Stat();
            }
            Progress.Instance.CurrentProgressData.Money = _player.Money;
            Progress.Instance.CurrentProgressData.AMlevel++;
            Progress.Instance.Save();
            _textMon.text = _player.Money.ToString();
            _player._paText.text = Mathf.Pow(2, Progress.Instance.CurrentProgressData.PALevel).ToString() + " / удар";
            if (Progress.Instance.CurrentProgressData.AMlevel > 0) { _player._amText.text = (Mathf.Pow(2, Progress.Instance.CurrentProgressData.AMlevel) / 2).ToString() + " / сек"; }
            else { _player._amText.text = "0 / сек"; }
        }
        else
        {
            _am.SetTrigger("No2");
        }
    }

    private void Stat()
    {
        if (AutoMinerLevel >= 20 || PickaxeLevel >= 25)
        {
            StatMax();
        }
        else 
        {
            _textPriceAM.text = _shopAM.Price[AutoMinerLevel].ToString();
            _textPricePA.text = _shopPA.Price[PickaxeLevel].ToString();
        }
        
    }

    private void StatMax()
    {
        if (AutoMinerLevel < 20) 
        { 
            _textPriceAM.text = _shopAM.Price[AutoMinerLevel].ToString(); 
        }
        else
        {
            _textPriceAM.text = "Максимум";
        }

        if (PickaxeLevel < 25)
        {
            _textPricePA.text = _shopPA.Price[PickaxeLevel].ToString();
        }
        else
        {
            _textPricePA.text = "Максимум";
        }
        
    }
}
