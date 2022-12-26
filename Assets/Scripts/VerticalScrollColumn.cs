using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class VerticalScrollColumn : MonoBehaviour
{
    [SerializeField] private VerticalScrollElement _elementPrefab;
    [SerializeField] private int numberOfColumnElements;
    private List<VerticalScrollElement> currentElements = new List<VerticalScrollElement>();
    private void Start()
    {
        for (int i = 0; i < numberOfColumnElements; i++)
        {
            var instantedElement = Instantiate(_elementPrefab, this.transform);
            var index = i;
            currentElements.Add(instantedElement);
            instantedElement.GetComponent<Button>().onClick.AddListener(() => DestroyElement(currentElements.IndexOf(instantedElement)));

        }

    }

    private void DestroyElement(int index)
    {
        StartCoroutine(BlockScrolling());
        for (int i = index + 1 ; i < currentElements.Count; i++)
        {
            var elementTransform = currentElements[i].transform;
            var previousElementTransform = currentElements[i - 1].transform;
            elementTransform.DOMoveY(previousElementTransform.position.y, 1f);
        }
        currentElements.RemoveAt(index);
    }

    private IEnumerator BlockScrolling()
    {
        GetComponentInParent<ScrollRect>().vertical = false;
        yield return new WaitForSeconds(1f);
        GetComponentInParent<ScrollRect>().vertical = true;
    }
}
