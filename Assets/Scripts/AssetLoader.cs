using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AssetLoader : MonoBehaviour
{

    [SerializeField] private Slider progressbar;
    private List<AssetBundleCreateRequest> loadProgress = new List<AssetBundleCreateRequest>();
    public static Sprite[] VerticalImages;
    public static Sprite[] HorizontalImages;


    IEnumerator Start()
    {
        DontDestroyOnLoad(this);
        
        string path = Application.streamingAssetsPath + "/testtaskbundle/";
        var horizontalImagesRequest = AssetBundle.LoadFromFileAsync(path + "horizontal-images");
        
        loadProgress.Add(horizontalImagesRequest);

        yield return horizontalImagesRequest;


        var loadedHorizontalImages = horizontalImagesRequest.assetBundle;
        
        var verticalImagesRequest = AssetBundle.LoadFromFileAsync(path + "vertical-images");
        
        loadProgress.Add(verticalImagesRequest);

        yield return verticalImagesRequest;


        var loadedVerticalImages = verticalImagesRequest.assetBundle;

        if (loadedVerticalImages == null || loadedHorizontalImages == null)
        {
            Debug.LogError("Failed load asset bundles");
            yield break;
        }

        VerticalImages = loadedVerticalImages.LoadAllAssets<Sprite>();
        HorizontalImages = loadedHorizontalImages.LoadAllAssets<Sprite>();
        
        loadedHorizontalImages.Unload(false);
        loadedVerticalImages.Unload(false);
        //yield return new WaitForSeconds(3);

        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        
        

    }
    

    private void Update()
    {
        var progress = 0f;
        foreach (var proccess in loadProgress)
        {
            progress += proccess.progress;
        }
        progressbar.value = progress;
    }
}
