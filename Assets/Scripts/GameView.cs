using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameView : MonoBehaviour
{
    public static readonly Color[] horizontalElementsColors = new[] {Color.red, Color.blue, Color.green,};
    public const int HorizontalElementsNumber = 3;
    private GameModel _model;
    [SerializeField] private Transform horizontalScroll;
    [SerializeField] private Transform verticalScrollContent;
    [SerializeField] private SettingsPanel _settingsPanel;
    [SerializeField] private InfoPanel _infoPanel;

    [SerializeField] private HorizontalScrollElement _horizontalScrollElementPrefab;
    [SerializeField] private VerticalScrollColumn _verticalScrollColumnPrefab;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button resetButton;
    
    private void Start()
    {
        LoadCacheOrCreateNewGame();
        CreateGame();
        
        settingsButton.onClick.AddListener(() =>
        {
            _settingsPanel.gameObject.SetActive(true);
            _settingsPanel.SwitchVisibility(true);
            _settingsPanel.info.onClick.AddListener(OpenInfoPanel);
        });
        
        
        exitButton.onClick.AddListener(() =>
        {
            SaveState();
            Application.Quit();
        });
        
        resetButton.onClick.AddListener(() =>
        {
            _model = GameModel.Default();
            CreateGame();
            SaveState();
        });

        OpenInfoPanel();
    }

    public void OpenInfoPanel()
    {
        _infoPanel.gameObject.SetActive(true);
        _infoPanel.SwitchVisibility(true);
    }

    public void CreateGame()
    {
        if(verticalScrollContent.childCount > 0 || horizontalScroll.childCount > 0)
            DestroyGame();
        
        InstantiateHorizontalScroll();
        InstantiateVerticalScroll();
    }

    private void InstantiateHorizontalScroll()
    {
        for (int i = 0; i < HorizontalElementsNumber + 1; i++)
        {
            var element = Instantiate(_horizontalScrollElementPrefab, horizontalScroll);

            element.image.sprite = Resources.Load<Sprite>($"Images/HorizontalImages/{i % 3}");
        }

        /*var horizontalScrollPosition = horizontalScroll.position;
        horizontalScrollPosition.x = _model.HorizontalElementsPosition;*/
        horizontalScroll.DOMoveX(_model.HorizontalElementsPosition, Single.MinValue);
    }

    private void InstantiateVerticalScroll()
    {
        for (int i = 0; i < 3; i++)
        {
            var column = Instantiate(_verticalScrollColumnPrefab, verticalScrollContent);
            column.columnIndex = i;
            var elementsForThisColumn = _model.verticalElementsModels.Where(model => model.columnIndex == i).ToList();
            for (int j = 0; j < elementsForThisColumn.Count(); j++)
            {
                column.InstantiateElement(elementsForThisColumn[j]);
            }
        }
    }

    public void SaveState()
    {
        _model.HorizontalElementsPosition = horizontalScroll.position.x;
        _model.verticalElementsModels = verticalScrollContent.GetComponentsInChildren<VerticalScrollElement>()
            .Select(el => el.model).OrderBy(model => (model.columnIndex, model.inColumnIndex)).ToList();

        File.WriteAllText(Application.persistentDataPath + "/GameState.json", JsonUtility.ToJson(_model));
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus)
            SaveState();
    }

    private void LoadCacheOrCreateNewGame()
    {
        bool hasSavedState = File.Exists(Application.persistentDataPath + "/GameState.json");

        if (hasSavedState)
        {
            var jsonState = File.ReadAllText(Application.persistentDataPath + "/GameState.json");
            _model = JsonUtility.FromJson<GameModel>(jsonState);
        }
        else
        {
            _model = GameModel.Default();
        }
    }

    void DestroyGame()
    {
        var horizontalElements = horizontalScroll.GetComponentsInChildren<HorizontalScrollElement>();
        for (int i = 0; i < horizontalElements.Length; i++)
            Destroy(horizontalElements[i].gameObject);
        
        var verticalElements = verticalScrollContent.GetComponentsInChildren<VerticalScrollColumn>();
        for (int i = 0; i < verticalElements.Length; i++)
            Destroy(verticalElements[i].gameObject);
    }
    
}
