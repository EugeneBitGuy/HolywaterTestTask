using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class VerticalScrollElement : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroyParticles;

    public void ClickMe()
    {
        Instantiate(_destroyParticles, this.gameObject.transform);
        this.GetComponent<Image>().DOFade(0, 1f);
        Destroy(this.gameObject, 1f);
    }
    

}
