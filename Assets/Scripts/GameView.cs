using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.Audio;

public class GameView : MonoBehaviour
{
    private GameModel _model;

    #region Canvas
    [SerializeField] private Transform horizontalScroll;
    [SerializeField] private Transform verticalScroll;
    [SerializeField] public SettingsPanel _settingsPanel;
    [SerializeField] private InfoPanel _infoPanel;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button resetButton;
    #endregion

    #region Prefabs
    [SerializeField] private HorizontalScrollElement _horizontalScrollElementPrefab;
    [SerializeField] private VerticalScrollColumn _verticalScrollColumnPrefab;
    #endregion
    
    
    [SerializeField] private AudioSource backgroundMusic;
    
    private void Start()
    {
        backgroundMusic.clip = AssetLoader.AudioClips.FirstOrDefault(clip => clip.name == "BackgroundMusic");
        backgroundMusic.Play();
        LoadCacheOrCreateNewGameModel();
        CreateGame();

        _settingsPanel.mixer.SetFloat("Music", PlayerPrefs.GetFloat("Music"));
        _settingsPanel.mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX"));

        
        settingsButton.onClick.AddListener(() =>
        {
            Extensions.PlaySFX("buttonClick");
            
            _settingsPanel.gameObject.SetActive(true);
            _settingsPanel.SwitchVisibility(true);
            _settingsPanel.info.onClick.AddListener(OpenInfoPanel);
        });
        
        
        exitButton.onClick.AddListener(() =>
        {
            Extensions.PlaySFX("buttonClick");

            SaveState();
            Application.Quit();
        });
        
        resetButton.onClick.AddListener(() =>
        {            
            Extensions.PlaySFX("buttonClick");

            _model = GameModel.Default();
            CreateGame();
            SaveState();
        });

        OpenInfoPanel();
    }

    private void OpenInfoPanel()
    {
        _infoPanel.gameObject.SetActive(true);
        _infoPanel.SwitchVisibility(true);
    }

    private void CreateGame()
    {
        if(verticalScroll.childCount > 0 || horizontalScroll.childCount > 0)
            DestroyGame();
        
        InstantiateHorizontalScroll();
        InstantiateVerticalScroll();
    }

    private void InstantiateHorizontalScroll()
    {
        for (int i = 0; i < GameModel.NumberOfElementsInHorizontalScroll + 1; i++)
        {
            var element = Instantiate(_horizontalScrollElementPrefab, horizontalScroll);

            element.image.sprite = AssetLoader.HorizontalImages.FirstOrDefault(img => img.name == GameModel.ImagesOfHorizontalElements[i % 3]);
        }
        
        horizontalScroll.DOMoveX(_model.HorizontalScrollPosition, Single.MinValue);
    }

    private void InstantiateVerticalScroll()
    {
        for (int i = 0; i < GameModel.NumberOfColumnsInVerticalScroll; i++)
        {
            var column = Instantiate(_verticalScrollColumnPrefab, verticalScroll);
            var elementsForThisColumn = _model.verticalElementsModels.Where(model => model.columnIndex == i).ToList();
            for (int j = 0; j < elementsForThisColumn.Count(); j++)
            {
                column.InstantiateElement(elementsForThisColumn[j]);
            }
        }
        
        verticalScroll.DOMoveY(_model.VerticalScrollPosition, Single.MinValue);

    }

    private void SaveState()
    {
        _model.HorizontalScrollPosition = horizontalScroll.position.x;
        _model.VerticalScrollPosition = verticalScroll.position.y;
        _model.verticalElementsModels = verticalScroll.GetComponentsInChildren<VerticalScrollElement>()
            .Select(el => el.model).OrderBy(model => (model.columnIndex, model.inColumnIndex)).ToList();

        File.WriteAllText(Application.persistentDataPath + "/GameState.json", JsonUtility.ToJson(_model));
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus)
            SaveState();
    }

    private void LoadCacheOrCreateNewGameModel()
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
        
        var verticalElements = verticalScroll.GetComponentsInChildren<VerticalScrollColumn>();
        for (int i = 0; i < verticalElements.Length; i++)
            Destroy(verticalElements[i].gameObject);
    }
    

    
}
