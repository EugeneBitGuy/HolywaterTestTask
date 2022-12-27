using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button socialLinkButton;
    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            
            SwitchVisibility(false);
            StartCoroutine(SetActiveWithDelay(false, 0.5f));
        });
        
        socialLinkButton.onClick.AddListener(() =>
        {
            Application.OpenURL("https://www.linkedin.com/in/yevhenii-postoienko-29882b228/");
        });
    }
    
    private IEnumerator SetActiveWithDelay(bool isActive, float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(isActive);
    }
    
    public void SwitchVisibility(bool isVisible)
    {
        var fadeValue = isVisible ? 1 : 0;

        GetComponent<CanvasGroup>().DOFade(fadeValue, 0.5f);
    }
}
