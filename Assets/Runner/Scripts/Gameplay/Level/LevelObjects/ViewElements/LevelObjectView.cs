using System;
using Runner.Scripts.Gameplay.Interfaces;
using Runner.Scripts.Gameplay.Level.LevelObjects.Data;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.Gameplay.Level.LevelObjects.ViewElements
{
    /// <summary>
    /// Level Object View
    /// </summary>
    public class LevelObjectView : MonoBehaviour, IPoolable<LevelObjectParams, IMemoryPool>, IDisposable, ILevelItemView
    {
        public Action<LevelObjectView> OnTriggerPlayer;

        private IMemoryPool _pool;
        private MeshRenderer _meshRenderer;
        private Collider _collider;

        public Vector3 Position
        {
            set { transform.position = value; }
            get => transform.position;
        }

        public string ID { get; set; }

        public void HideAndDeactivate()
        {
            MeshRenderer.enabled = false;
            Collider.enabled = false;
        }

        private MeshRenderer MeshRenderer
        {
            get
            {
                if (_meshRenderer == null)
                {
                    _meshRenderer = GetComponent<MeshRenderer>();
                }

                if (_meshRenderer == null)
                {
                    Debug.LogError($"Error: MeshRenderer is null in {name}");
                }

                return _meshRenderer;
            }
        }

        private Collider Collider
        {
            get
            {
                if (_collider == null)
                {
                    _collider = GetComponent<Collider>();
                }

                if (_meshRenderer == null)
                {
                    Debug.LogError($"Error: Collider is null in {name}");
                }

                return _collider;
            }
        }

        public Color Color
        {
            set
            {
                if (!_meshRenderer)
                {
                    _meshRenderer = GetComponent<MeshRenderer>();
                }

                _meshRenderer.material.color = value;
            }
        }

        public void OnDespawned()
        {
            //
        }

        public void OnSpawned(LevelObjectParams objectParams, IMemoryPool pool)
        {
            _pool = pool;
            gameObject.SetActive(true);
        }

        public void Dispose()
        {
            gameObject.SetActive(false);
            _pool?.Despawn(this);
        }

        public class Factory : PlaceholderFactory<LevelObjectParams, LevelObjectView>
        {
        }

        public class FacadePool : MonoPoolableMemoryPool<LevelObjectParams, IMemoryPool, LevelObjectView>
        {
        }

        private void OnCollisionEnter(Collision collision)
        {
            //
        }

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerPlayer?.Invoke(this);
        }
    }
}