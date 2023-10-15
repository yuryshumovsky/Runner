using System;
using System.Collections.Generic;
using Runner.Scripts.Gameplay.Level.LevelObjects.Configs;
using Runner.Scripts.Gameplay.ObjectActivity;
using Runner.Scripts.Misc.Loader;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Runner.Scripts.Configs
{
    /// <summary>
    /// Stores the game's configuration settings.
    /// </summary>
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Create game config file")]
    public class GameConfig : ScriptableObject
    {
        public LoadingScreen loadingScreenPrefab;
        public string GameSceneName = "Gameplay";
        
        public List<PlatformConfig> levelPlatforms;
        public AssetReference characterAsset;
        
        public List<LevelObjectConfig> levelObjects;
    }

    [Serializable]
    public class PlatformConfig
    {
        public AssetReference platformView;
        //public PlatformType type;
    }
    
    [Serializable]
    public class LevelObjectConfig
    {
        public string ID;
        public LevelAssetConfig levelAssetConfig;
        public Color Color;
        public List<LevelItemBaseActivity> takeActivities;
    }
    
    
}