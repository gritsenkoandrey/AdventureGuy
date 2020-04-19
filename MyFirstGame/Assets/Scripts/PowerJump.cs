using UnityEngine;


public class PowerJump : MonoBehaviour
{
    #region Fields

    private float _plusJumpForce = 2.0f;

    private Character _character;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D jumpForce)
    {
        _character = jumpForce.GetComponent<Character>();
        if(_character)
        {
            AudioSound._audioSound.AudioGetPowerJump();
            _character.JumpForce += _plusJumpForce;
            Destroy(gameObject);
        }
    }

    #endregion
}