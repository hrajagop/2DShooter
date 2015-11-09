using System.IO;
using UnityEngine;
using UnityEditor;

public class BuildLogic 
{
    public static BuildTarget Target { get; set; }
    public static BuildAssetBundleOptions Option { get; set; }

    [MenuItem("AssetBundles/Build/Option+Target")]
    public static void AssetBundleBuildWithOptionAndTarget()
    {
        GetRidOfEffectFromNoneOption();
        VisualizeOptionValues();
        VizualizeTargetValue();
        BuildPipeline.BuildAssetBundles(CombineURL(), Option, Target);
    }

    private static string CombineURL()
    {
        PathEnum path;

        if ((Option & BuildAssetBundleOptions.ChunkBasedCompression) != 0)
            path = PathEnum.BundlesChunkBased;
        else if ((Option & BuildAssetBundleOptions.UncompressedAssetBundle) != 0)
            path = PathEnum.BundlesUncompressed;
        else
            path = PathEnum.BundlesDefault;

        var parentDir = Directory.GetParent(Application.dataPath);
        var outputPath = Path.Combine(parentDir.ToString(), path.ToString());
        Directory.CreateDirectory(outputPath);

        return outputPath;
    }

    static void GetRidOfEffectFromNoneOption()
    {
        //Ugly hack to get rid of "BuildAssetBundleOptions.None" enumeration element in EnumMaskFiled
        Option = (BuildAssetBundleOptions) ((int) Option >> 1);
    }

    //Only for testing purposes

    static void VizualizeTargetValue()
    {
        Debug.Log("Target: " + Target);
    }

    private static void VisualizeOptionValues()
    {
        if ((Option & BuildAssetBundleOptions.UncompressedAssetBundle) != BuildAssetBundleOptions.None)
            Debug.Log("Option: UncompressedAssetBundle");

        if ((Option & BuildAssetBundleOptions.DisableWriteTypeTree) != BuildAssetBundleOptions.None)
            Debug.Log("Option: DisableWriteTypeTree");

        if ((Option & BuildAssetBundleOptions.DeterministicAssetBundle) != BuildAssetBundleOptions.None)
            Debug.Log("Option: DeterministicAssetBundle");

        if ((Option & BuildAssetBundleOptions.ForceRebuildAssetBundle) != BuildAssetBundleOptions.None)
            Debug.Log("Option: ForceRebuildAssetBundle");

        if ((Option & BuildAssetBundleOptions.IgnoreTypeTreeChanges) != BuildAssetBundleOptions.None)
            Debug.Log("Option: IgnoreTypeTreeChanges");

        if ((Option & BuildAssetBundleOptions.AppendHashToAssetBundleName) != BuildAssetBundleOptions.None)
            Debug.Log("Option: AppendHashToAssetBundleName");

        #if UNITY_5_3
            if ((Option & BuildAssetBundleOptions.ChunkBasedCompression) != BuildAssetBundleOptions.None)
               Debug.Log("Option: ChunkBasedCompression");
        #endif
    }
}