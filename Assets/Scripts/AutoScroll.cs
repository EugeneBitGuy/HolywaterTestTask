using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    private LoopScrollRect scrollComponent;
    void Start()
    {
        scrollComponent = GetComponent<LoopScrollRect>();
    }

    void Update()
    {
        if (!scrollComponent.isManualScroll)
            scrollComponent.content.position += Vector3.left / 50;
    }
}
