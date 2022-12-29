using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class VerticalScrollElement : MonoBehaviour
{
    [SerializeField] private ParticleSystem _destroyParticles;
    public VerticalElementModel model;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ClickMe);
    }

    private void ClickMe()
    {
        Extensions.PlaySFX("buttonClick");
        InstantiateDestroyParticles();
        FadeObject();
    }

    void InstantiateDestroyParticles()
    {
        var material = new Material(Shader.Find(_destroyParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial.shader.name));
        material.mainTexture = GetComponent<Image>().sprite.texture;
        _destroyParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial = material;
        Instantiate(_destroyParticles, gameObject.transform);
    }

    void FadeObject()
    {
        GetComponent<Image>().DOFade(0, 1f);
        Destroy(this.gameObject, 1f);
    }

}
