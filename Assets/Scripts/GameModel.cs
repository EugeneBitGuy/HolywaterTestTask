using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;


[Serializable]
public class GameModel
{

    public List<VerticalElementModel> verticalElementsModels;
    [NonSerialized] public const int NumberOfElementsInColumnOfVerticalScroll = 10;
    [NonSerialized] public const int NumberOfColumnsInVerticalScroll = 3;
    [NonSerialized] public static readonly string[] ImagesOfVerticalElements = {"1" , "2", "3" ,"4", "5", "6" };
    [NonSerialized] static Random _random = new Random();
    [SerializeField] private float horizontalElementsPosition;

    public float HorizontalElementsPosition
    {
        get => horizontalElementsPosition;
        set => horizontalElementsPosition = value;
    }


    public GameModel()
    {
        HorizontalElementsPosition = 0;
        CreateNewVerticalElements();
    }

    public static GameModel Default()
    {
        return new GameModel();
    }

    public void CreateNewVerticalElements()
    {
        if (verticalElementsModels != null)
            verticalElementsModels.Clear();
        else
            verticalElementsModels = new List<VerticalElementModel>();
        
        for (int colIdx = 0; colIdx < NumberOfColumnsInVerticalScroll; colIdx++)
        {
            for (int elIdx = 0; elIdx < NumberOfElementsInColumnOfVerticalScroll; elIdx++)
            {
                var imageName = _random.Next(1, 41).ToString()/*ImagesOfVerticalElements[_random.Next(ImagesOfVerticalElements.Length)]*/;
                verticalElementsModels.Add(new VerticalElementModel(imageName, colIdx, elIdx));
            }
        }
        
        
    }
}


[Serializable]
public class VerticalElementModel
{
    public string imageName;
    public int columnIndex;
    public int inColumnIndex;

    public VerticalElementModel(string imageName, int columnIndex, int inColumnIndex)
    {
        this.imageName = imageName;
        this.columnIndex = columnIndex;
        this.inColumnIndex = inColumnIndex;
    }
    
   
}
