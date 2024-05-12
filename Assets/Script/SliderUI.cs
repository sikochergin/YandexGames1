using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private WallCounter _counter;

    // Start is called before the first frame update
    void Start()
    {
        _text.text = "Сломано стен: 0/" + _counter.WallsAtAll;
        _slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = "Сломано стен: " + Progress.Instance.CurrentProgressData.Walls + "/" + _counter.WallsAtAll;
        _slider.value = (Progress.Instance.CurrentProgressData.Walls * 1.0f) / (_counter.WallsAtAll * 1.0f); 
    }
}
