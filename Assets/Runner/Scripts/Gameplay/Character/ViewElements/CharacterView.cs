using UnityEngine;

namespace Runner.Scripts.Gameplay.Character.ViewElements
{
    /// <summary>
    /// Represents the game's character
    /// </summary>
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private Rigidbody _rigidbody;
        private Collider _collider;

        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public void SetAnimationKey(string animationKey, bool value)
        {
            _animator.SetBool(animationKey, value);
        }

        public Rigidbody Rigidbody
        {
            get
            {
                if (_rigidbody == null)
                {
                    _rigidbody = GetComponent<Rigidbody>();
                }

                return _rigidbody;
            }
        }

        public Collider Collider
        {
            get
            {
                if (_collider == null)
                {
                    _collider = GetComponent<Collider>();
                }

                return _collider;
            }
        }

        public void Jump(Vector3 force)
        {
            Rigidbody.AddRelativeForce(force);
        }

        public bool UseGravity
        {
            set { Rigidbody.useGravity = value; }
        }
    }
}