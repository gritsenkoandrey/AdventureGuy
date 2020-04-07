using UnityEngine;

public class PowerJump : MonoBehaviour
{
    #region Fields

    private float _bonusJumpForce = 2F;
    private float _normalJumpForce = 6.5F;
    private Character _character;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D jump)
    {
        _character = jump.GetComponent<Character>();
        _character.JumpForce += _bonusJumpForce;
        Invoke(nameof(NormalJumpForce), 1F);
        Destroy(gameObject);

        //if (_character)
        //{
        //    _character.JumpForce += _bonusJumpForce;
        //    Invoke(nameof(NormalJumpForce), 1F);
        //    Destroy(gameObject);
        //}
    }

    #endregion

    #region Methods
    private void NormalJumpForce()
    {
        //_character = GetComponent<Character>();
        _character.JumpForce = _normalJumpForce;
        Debug.Log("Jump");
    }

    #endregion
}