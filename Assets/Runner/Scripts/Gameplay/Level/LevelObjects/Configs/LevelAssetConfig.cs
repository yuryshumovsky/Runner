using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Runner.Scripts.Gameplay.Level.LevelObjects.Configs
{
    /// <summary>
    /// A scriptable object that stores configuration data for a level asset.
    /// </summary>
    [CreateAssetMenu(fileName = "LevelAssetConfig", menuName = "Create 'LevelAssetConfig'", order = 0)]
    public class LevelAssetConfig : ScriptableObject
    {
        public string assetKey;
        public AssetReference assetReference;
    }
}