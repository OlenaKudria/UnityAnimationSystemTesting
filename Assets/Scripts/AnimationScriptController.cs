using UnityEngine;

namespace Core.Scripts
{
    public class AnimationScriptController : MonoBehaviour
    {
        private Animator _animator;
        private float _velocity = 0.0f;
        public float acceleration = 0.1f;
        public float deceleration = 0.5f;
        private static readonly int VelocityHash = Animator.StringToHash("Velocity") ;
        void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        void Update()
        {
            bool forwardPressed = Input.GetKey(KeyCode.W);
            bool runPressed = Input.GetKey(KeyCode.LeftShift);

            if (forwardPressed && _velocity < 1)
                _velocity += Time.deltaTime * acceleration;
            
            if (!forwardPressed && _velocity > 0.0f)
                _velocity -= Time.deltaTime * deceleration;

            if (!forwardPressed && _velocity < 0.0f)
                _velocity = 0.0f;
            
            _animator.SetFloat(VelocityHash, _velocity);
        }
    }
}
