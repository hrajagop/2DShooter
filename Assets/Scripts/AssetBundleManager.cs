using UnityEngine;
using System.Collections.Generic;
using System.Linq;

static public class AssetBundleManager
{
    static private Dictionary<string, AssetBundleRef> dictAssetBundleRefs;
    static AssetBundleManager()
    {
        dictAssetBundleRefs = new Dictionary<string, AssetBundleRef>();
    }
    
    private class AssetBundleRef
    {
        public AssetBundle assetBundle = null;
        public readonly int version;
        public readonly string url;
        public AssetBundleRef(string strUrlIn, int intVersionIn)
        {
            url = strUrlIn;
            version = intVersionIn;
        }
    };
    
    public static AssetBundle GetAssetBundle(string url, int version)
    {
        AssetBundleRef abRef;
        return dictAssetBundleRefs.TryGetValue(url + version, out abRef) ? abRef.assetBundle : null;
    }

    public static List<AssetBundle> GetAllBundles()
    {
        return dictAssetBundleRefs.Select(bundleRef => bundleRef.Value.assetBundle).ToList();
    }

    public static void AddBundle(string url, int version, AssetBundle bundle)
    {
        var keyName = url + version;
        if (dictAssetBundleRefs.ContainsKey(keyName))
        {
            Debug.Log(keyName + " is already loaded");
            return;
        }
        var abRef = new AssetBundleRef(url, version) {assetBundle = bundle};
        dictAssetBundleRefs.Add(keyName, abRef);
        Debug.Log(keyName + " added");
    }

    public static void UnloadAll()
    {
        foreach (var entry in dictAssetBundleRefs)
        {
            entry.Value.assetBundle.Unload(true);
            entry.Value.assetBundle = null;
        }
        dictAssetBundleRefs.Clear();
    }
}