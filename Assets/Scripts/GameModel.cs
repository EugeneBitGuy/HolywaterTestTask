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
    [NonSerialized] public const int NumberOfElementsInHorizontalScroll = 3;
    [NonSerialized] public static readonly string[] ImagesOfVerticalElements = {"0", "1" , "2", "3" ,"4", "5", "6" , "7", "8", "9", "10" ,"11" , "12", "13" ,"14", "15", "16" , "17", "18", "19", "20", "21" , "22", "23" ,"24", "25", "26" , "27", "28", "29"};
    [NonSerialized] public static readonly string[] ImagesOfHorizontalElements = {"0", "1" , "2"};
    [SerializeField] private float horizontalScrollPosition;
    [SerializeField] private float verticalScrollPosition;

    public float HorizontalScrollPosition
    {
        get => horizontalScrollPosition;
        set => horizontalScrollPosition = value;
    }
    
    public float VerticalScrollPosition
    {
        get => verticalScrollPosition;
        set => verticalScrollPosition = value;
    }


    public GameModel()
    {
        HorizontalScrollPosition = 0;
        VerticalScrollPosition = 0;
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
                var imageName = ImagesOfVerticalElements[colIdx * NumberOfColumnsInVerticalScroll + elIdx];
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
