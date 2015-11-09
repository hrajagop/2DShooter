using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AssetLoader : MonoBehaviour
{
    private List<GameObject> prefabs;

    public void LoadMenuScene()
    {
		SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void InstantiatePrefab()
    {
        var bundles = AssetBundleManager.GetAllBundles();

        foreach (var bundle in bundles)
        {
            var assets = bundle.LoadAllAssets<GameObject>();
            if (assets.Length != 0)
                prefabs.AddRange(assets);
        }

        foreach (var prefab in prefabs)
        {
            var instance = Instantiate(prefab);
        }
    }
}
