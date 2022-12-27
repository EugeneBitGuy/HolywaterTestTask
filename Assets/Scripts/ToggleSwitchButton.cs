using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ToggleSwitchButton : Button
{

    public Sprite[] stateSprites;
    public bool State { get;  set; }

    protected override void Start()
    {
        base.Start();
        SetIcon(State);
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        State = !State;
        base.OnPointerClick(eventData);
        SetIcon(State);
    }

    public void SetIcon(bool state)
    {
        GetComponent<Image>().sprite = stateSprites[state ? 0 : 1];
    }
}
