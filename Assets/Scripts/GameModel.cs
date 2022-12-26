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
    [NonSerialized] public static readonly string[] ImagesOfVerticalElements = new string[] {"0" , "1" };
    [NonSerialized] static Random _random = new Random();

    public float HorizontalelementsPosition;


    public GameModel()
    {
        HorizontalelementsPosition = 0;
        CreateNewVerticalElements();
    }

    public GameModel Default()
    {
        return new GameModel();
    }

    public void CreateNewVerticalElements()
    {
        verticalElementsModels.Clear();

        for (int colIdx = 0; colIdx < NumberOfColumnsInVerticalScroll; colIdx++)
        {
            for (int elIdx = 0; elIdx < NumberOfElementsInColumnOfVerticalScroll; elIdx++)
            {
                var imageName = ImagesOfVerticalElements[_random.Next(ImagesOfVerticalElements.Length)];
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
