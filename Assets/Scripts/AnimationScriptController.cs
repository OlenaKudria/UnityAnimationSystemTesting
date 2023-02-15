using UnityEngine;

namespace Core.Scripts
{
    public class AnimationScriptController : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
       

        void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        void Update()
        {
            bool forwardPressed = Input.GetKey("w");
            bool runPressed = Input.GetKey("left shift");
            bool isWalking = _animator.GetBool(IsWalking);
            bool isRunning = _animator.GetBool(IsRunning);
            
            if (!isWalking && forwardPressed)
                _animator.SetBool(IsWalking, true);
            
            if (isWalking && !forwardPressed)
                _animator.SetBool(IsWalking, false);
            
            if(!isRunning && forwardPressed && runPressed)
                _animator.SetBool(IsRunning, true);
            
            if(isRunning && (!forwardPressed || !runPressed))
                _animator.SetBool(IsRunning, false);
                
        }
    }
}
