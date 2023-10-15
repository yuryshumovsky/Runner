using System;
using System.Collections;
using Runner.Scripts.Gameplay.Level.Effects.Data;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.Gameplay.Level.Effects.ViewElements
{
    /// <summary>
    /// A class that represents a level effect view.
    /// </summary>
    public class LevelEffectView : MonoBehaviour, IPoolable<LevelEffectParams, IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        private MeshRenderer _meshRenderer;
        private Collider _collider;

        public Vector3 Position
        {
            set { transform.position = value; }
        }

        public void OnDespawned()
        {
            //
        }

        public void OnSpawned(LevelEffectParams objectParams, IMemoryPool pool)
        {
            _pool = pool;
            gameObject.SetActive(true);

            StartCoroutine(DisableCoroutine());
        }

        private IEnumerator DisableCoroutine()
        {
            yield return new WaitForSeconds(3f);
            Dispose();
        }

        public void Dispose()
        {
            gameObject.SetActive(false);
            _pool?.Despawn(this);
        }

        public class Factory : PlaceholderFactory<LevelEffectParams, LevelEffectView>
        {
        }

        public class FacadePool : MonoPoolableMemoryPool<LevelEffectParams, IMemoryPool, LevelEffectView>
        {
        }
    }
}