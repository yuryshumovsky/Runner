using System.Collections.Generic;
using System.Linq;
using Runner.Scripts.Gameplay.Level.Platform.Data;
using Runner.Scripts.Gameplay.Level.Platform.ViewElements;
using UnityEngine;

namespace Runner.Scripts.Gameplay.Level.Platform.Controllers
{
    public class PlatformLevelFactory
    {
        private Dictionary<string, PlatformView.Factory> _factoryDict;

        public PlatformLevelFactory(Dictionary<string, PlatformView.Factory> dict)
        {
            _factoryDict = dict;
        }

        public (PlatformView, string) GetNewOne()
        {
            int factoryRandomIndex = Random.Range(0, _factoryDict.Count);
            KeyValuePair<string, PlatformView.Factory> keyValuePair = _factoryDict.ElementAt(factoryRandomIndex);

            PlatformView.Factory factory = keyValuePair.Value;

            return (factory.Create(new PlatformParams() { }), keyValuePair.Key);
        }
    }
}