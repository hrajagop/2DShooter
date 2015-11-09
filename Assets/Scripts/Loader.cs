using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.Experimental.Networking;

public class Loader : MonoBehaviour
{
    public PathEnum path { get; set; }

    const string kDefaultURL = "http://10.45.5.228:8080/Testing/BundlesDefault";
    const string kChunkedURL = "http://10.45.5.228:8080/Testing/BundlesChunkBased";
    const string kUncompressedURL = "http://10.45.5.228:8080/Testing/BundlesUncompressed";

    private AssetBundleManifest manifest;

    private AssetLoader unityLoaderScript;

    void Awake()
    {
        unityLoaderScript = GetComponent<AssetLoader>();
        path = PathEnum.BundlesChunkBased;
    }

    void Start()
    {
        LoadBundlesFromWeb();
    }

    public void LoadBundlesFromWeb()
    {
        StartCoroutine(LoadingFromWebRequest());
    }

    IEnumerator LoadingFromWebRequest()
    {
        yield return StartCoroutine(LoadManifestFromWebRequest());

        foreach (var bundleName in manifest.GetAllAssetBundles())
        {
            var url = GetBundleURL(bundleName);
            var www = UnityWebRequest.GetAssetBundle(url, (uint)Random.Range(1, 65535), 0);
            yield return www.Send();
            var assetBundle = ((DownloadHandlerAssetBundle)(www.downloadHandler)).assetBundle;

            AddLoadedBundleToManager(assetBundle, bundleName);
        }

        unityLoaderScript.LoadMenuScene();
    }

    IEnumerator LoadManifestFromWebRequest()
    {
        var url = GetManifestURL();
        var www = new UnityWebRequest(url) { downloadHandler = new DownloadHandlerAssetBundle(url, 0) };
        yield return www.Send();

        var assetBundle = ((DownloadHandlerAssetBundle)(www.downloadHandler)).assetBundle;
        var assets = assetBundle.LoadAllAssets();

        AddLoadedBundleToManager(assetBundle, assets[0].name);

        manifest = (AssetBundleManifest)assets[0];
    }

    static void AddLoadedBundleToManager(AssetBundle bundle, string name)
    {
        if (!isBundleLoaded(bundle, name))
            return;

        AssetBundleManager.AddBundle(name, 0, bundle);
    }

    static bool isBundleLoaded(AssetBundle bundle, string name)
    {
        if (bundle == null)
        {
            Debug.LogWarning(string.Format("Bundle {0} is not loaded", name));
            return false;    
        }

        Debug.Log(string.Format("Bundle {0} is loaded", name));
        return true;        
    }

    string GetManifestURL()
    {
        return GetURL() + path;
    }

    string GetBundleURL(string bundleType)
    {
        return GetURL() + bundleType;
    }

    string GetURL()
    {
        switch (path)
        {
            case PathEnum.BundlesChunkBased:
                 return kChunkedURL + Path.AltDirectorySeparatorChar;
                
            case PathEnum.BundlesUncompressed:
                 return kUncompressedURL + Path.AltDirectorySeparatorChar;

            default:
                return kDefaultURL + Path.AltDirectorySeparatorChar;
        }
    }
}
