using UnityEngine;

public class PowerJump : MonoBehaviour
{
    #region Fields

    private float _plusJumpForce = 2F;

    private Character _character;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _character = collider.GetComponent<Character>();
        if(_character)
        {
            _character.JumpForce += _plusJumpForce;
            _character.AudioGetPowerJump();
            Destroy(gameObject);
        }
    }

    #endregion
}