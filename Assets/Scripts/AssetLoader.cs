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
    public static AudioClip[] AudioClips;


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

        var audioClipsRequest = AssetBundle.LoadFromFileAsync(path + "music");
        
        loadProgress.Add(audioClipsRequest);

        yield return audioClipsRequest;

        var loadedAudioClips = audioClipsRequest.assetBundle;

        if (loadedVerticalImages == null || loadedHorizontalImages == null || loadedAudioClips == null)
        {
            Debug.LogError("Failed load asset bundles");
            yield break;
        }

        VerticalImages = loadedVerticalImages.LoadAllAssets<Sprite>();
        HorizontalImages = loadedHorizontalImages.LoadAllAssets<Sprite>();
        AudioClips = loadedAudioClips.LoadAllAssets<AudioClip>();
        
        loadedHorizontalImages.Unload(false);
        loadedVerticalImages.Unload(false);
        loadedAudioClips.Unload(false);
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
