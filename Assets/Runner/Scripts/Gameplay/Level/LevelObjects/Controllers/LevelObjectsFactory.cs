using System.Collections.Generic;
using Runner.Scripts.Configs;
using Runner.Scripts.Gameplay.Level.LevelObjects.Data;
using Runner.Scripts.Gameplay.Level.LevelObjects.ViewElements;
using UnityEngine;

namespace Runner.Scripts.Gameplay.Level.LevelObjects.Controllers
{
    /// <summary>
    /// A factory for level objects.
    /// </summary>
    public class LevelObjectsFactory
    {
        private Dictionary<string, LevelObjectView.Factory> _factoryDict;
        private readonly List<LevelObjectConfig> _levelObjects;

        public LevelObjectsFactory(
            Dictionary<string, LevelObjectView.Factory> dict,
            List<LevelObjectConfig> levelObjects)
        {
            _factoryDict = dict;
            _levelObjects = levelObjects;
        }

        public (LevelObjectView, string) GetNewOne()
        {
            bool spawn = Random.value < 0.15f;

            if (spawn)
            {
                int randomIndex = Random.Range(0, _levelObjects.Count);
                LevelObjectConfig objectConfig = _levelObjects[randomIndex];

                LevelObjectView.Factory factory = _factoryDict[objectConfig.levelAssetConfig.assetKey];

                LevelObjectView itemView = factory.Create(new LevelObjectParams() { });
                itemView.ID = objectConfig.ID;

                return (itemView, objectConfig.ID);
            }

            return (null, null);
        }
    }
}