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
    public List<VerticalScrollElement> currentElements = new List<VerticalScrollElement>();
    public int columnIndex;

    public void InstantiateElement(VerticalElementModel model)
    {
        var element = Instantiate(_elementPrefab, transform);
        element.model = model;
        currentElements.Add(element);
        element.GetComponent<Image>().sprite = AssetLoader.VerticalImages.FirstOrDefault(img => img.name == model.imageName);
        element.GetComponent<Button>().onClick.AddListener(() => DestroyElement(currentElements.IndexOf(element)));
    }

    private void DestroyElement(int index)
    {
        StartCoroutine(BlockScrolling());
        for (int i = index + 1 ; i < currentElements.Count; i++)
        {
            currentElements[i].model.inColumnIndex--;
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
