using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private ToggleSwitchButton musicToggle;
    [SerializeField] private ToggleSwitchButton sfxToggle;
    public Button info;
    public AudioMixer mixer;

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            Extensions.PlaySFX("buttonClick");

            SwitchVisibility(false);
            StartCoroutine(SetActiveWithDelay(false, 0.5f));
        });

        musicToggle.State = PlayerPrefs.GetFloat("Music") == 0;
        musicToggle.SetIcon(musicToggle.State);
        sfxToggle.State = PlayerPrefs.GetFloat("SFX") == 0;
        sfxToggle.SetIcon(sfxToggle.State);
        
        musicToggle.onClick.AddListener((() =>
        {
            Extensions.PlaySFX("buttonClick");

            SwitchMusic(musicToggle.State);
        }));
        
        sfxToggle.onClick.AddListener((() =>
        {
            Extensions.PlaySFX("buttonClick");

            SwitchSFX(sfxToggle.State);
        }));
        
    }

    private IEnumerator SetActiveWithDelay(bool isActive, float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(isActive);
    }

    void SwitchMusic(bool state)
    {
        mixer.DOSetFloat("Music", state ? 0 : -80, 1f);
        PlayerPrefs.SetFloat("Music", state ? 0 : -80);
    }

    void SwitchSFX(bool state)
    {
        mixer.DOSetFloat("SFX", state ? 0 : -80, 1f);
        PlayerPrefs.SetFloat("SFX", state ? 0 : -80);

    }

    public void SwitchVisibility(bool isVisible)
    {
        var fadeValue = isVisible ? 1 : 0;

        GetComponent<CanvasGroup>().DOFade(fadeValue, 0.5f);
    }
}



