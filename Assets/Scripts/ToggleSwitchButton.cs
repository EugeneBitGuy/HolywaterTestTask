using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleSwitchButton : Button
{
    public bool State { get; private set; }

    protected override void Start()
    {
        base.Start();
        State = true;
        BrushImage(State);
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        State = !State;
        base.OnPointerClick(eventData);
        BrushImage(State);
    }

    private void BrushImage(bool state)
    {
        GetComponent<Image>().DOColor(state ? Color.white : Color.grey, 0.5f);
    }
}
