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
    [SerializeField] private Button info;
    [SerializeField] private AudioMixer mixer;

    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            GetComponent<Image>().DOFade(0, 0.5f);
            foreach (var imagesInChilds in GetComponentsInChildren<Image>())
            {
                imagesInChilds.DOFade(0, 0.5f);
            }
            foreach (var textInChilds in GetComponentsInChildren<TextMeshProUGUI>())
            {
                textInChilds.DOFade(0, 0.5f);
            }

            StartCoroutine(SetActiveWithDelay(false, 0.5f));
        });
        
        musicToggle.onClick.AddListener((() =>
        {
            SwitchMusic(musicToggle.State);
        }));
        
        sfxToggle.onClick.AddListener((() =>
        {
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
        mixer.DOSetFloat("Music", state ? 0 : -80, 0.2f);
    }

    void SwitchSFX(bool state)
    {
        mixer.DOSetFloat("SFX", state ? 0 : -80, 0.2f);
    }
}
