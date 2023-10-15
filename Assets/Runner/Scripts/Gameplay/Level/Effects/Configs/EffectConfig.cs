using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Runner.Scripts.Gameplay.Level.Effects.Configs
{
    /// <summary>
    /// A scriptable object that stores configuration data for an effect.
    /// </summary>
    [CreateAssetMenu(fileName = "EffectConfig", menuName = "Create 'EffectConfig'", order = 0)]
    public class EffectConfig : ScriptableObject
    {
        public string ID;
        public AssetReferenceGameObject effectReference;
    }
}