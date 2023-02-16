using UnityEngine;

public class TwoDAnimationStateController : MonoBehaviour
{
    private Animator _animator;
    private float _velocityZ = 0.0f;
    private float _velocityX = 0.0f;
    private const float MaximumWalkVelocity = 0.5f;
    private const float MaximumRunVelocity = 2.0f;
    private static readonly int VelocityXHash = Animator.StringToHash("VelocityX");
    private static readonly int VelocityZHash = Animator.StringToHash("VelocityZ");
    
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void AccelerationVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, float currentMaxVelocity)
    {
        if (forwardPressed && _velocityZ < currentMaxVelocity)
            _velocityZ += Time.deltaTime * acceleration;
        
        if (leftPressed && _velocityX > -currentMaxVelocity)
            _velocityX -= Time.deltaTime * acceleration;
        
        if (rightPressed && _velocityX < currentMaxVelocity)
            _velocityX += Time.deltaTime * acceleration;
    }

    void DecelerationVelocity(bool forwardPressed, bool leftPressed, bool rightPressed)
    {
        if (!forwardPressed && _velocityZ > 0.0f)
            _velocityZ -= Time.deltaTime * deceleration;
        
        if (!leftPressed && _velocityX < 0.0f)
            _velocityX += Time.deltaTime * deceleration;
        
        if (!rightPressed && _velocityX > 0.0f)
            _velocityX -= Time.deltaTime * deceleration;
    }

    void ResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed)
    {
        if (!forwardPressed && _velocityZ < 0.0f)
            _velocityZ = 0.0f;

        if(!rightPressed && !leftPressed && _velocityX != 0.0f && (_velocityX > -0.05f && _velocityX < 0.05f))
            _velocityX = 0.0f;
    }

    void LockVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed,
        float currentMaxVelocity)
    {
        //lock forward
        if (forwardPressed && runPressed && _velocityZ > currentMaxVelocity)
            _velocityZ = currentMaxVelocity;
        else if (forwardPressed && _velocityZ > currentMaxVelocity)
        {
            _velocityZ -= Time.deltaTime * deceleration;
            if (_velocityZ > currentMaxVelocity && _velocityZ < (currentMaxVelocity + 0.05f))
                _velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed && _velocityZ < currentMaxVelocity && _velocityZ > (currentMaxVelocity - 0.05f))
            _velocityZ = currentMaxVelocity;
        
        //lock leftPressed
        if (leftPressed && runPressed && _velocityX < -currentMaxVelocity)
            _velocityX = -currentMaxVelocity;
        else if (leftPressed && _velocityX < -currentMaxVelocity)
        {
            _velocityX += Time.deltaTime * deceleration;
            if (_velocityX < -currentMaxVelocity && _velocityX > (-currentMaxVelocity - 0.05f))
                _velocityX = -currentMaxVelocity;
        }
        else if (leftPressed && _velocityX > -currentMaxVelocity && _velocityX < (-currentMaxVelocity + 0.05f))
            _velocityX = -currentMaxVelocity;
        
        //lock leftPressed
        if (rightPressed && runPressed && _velocityX > currentMaxVelocity)
            _velocityX = currentMaxVelocity;
        else if (rightPressed && _velocityX > currentMaxVelocity)
        {
            _velocityX -= Time.deltaTime * deceleration;
            if (_velocityX > currentMaxVelocity && _velocityX < (currentMaxVelocity + 0.05f))
                _velocityX = currentMaxVelocity;
        }
        else if (rightPressed && _velocityX < currentMaxVelocity && _velocityX > (currentMaxVelocity - 0.05f))
            _velocityX = currentMaxVelocity;
    }
    
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = runPressed ? MaximumRunVelocity : MaximumWalkVelocity;

        AccelerationVelocity(forwardPressed, leftPressed, rightPressed, currentMaxVelocity);
        DecelerationVelocity(forwardPressed, leftPressed, rightPressed);

        ResetVelocity(forwardPressed, leftPressed, rightPressed);
        LockVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        
        _animator.SetFloat(VelocityZHash, _velocityZ);
        _animator.SetFloat(VelocityXHash, _velocityX);
    }
}
