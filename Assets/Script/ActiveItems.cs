using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItems : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private Transform _posPl;
    [SerializeField] private Transform _posPA;

    private Quaternion _rotation;

    [ContextMenu("Hit")]
    virtual public void Hit(int pow)
    {
        _rotation = _posPl.rotation;
        _rotation.eulerAngles = new Vector3(0, _rotation.eulerAngles.y + 90, 0);
        ParticleSystem _ps = Instantiate(_particles, _posPA.position,  _rotation);
    }
}
