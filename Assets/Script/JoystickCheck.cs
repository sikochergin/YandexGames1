using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickCheck : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Player _player;

    [SerializeField] private bool bbb;
    

    private void Start()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _player.MouseOn = bbb;
    }

}
