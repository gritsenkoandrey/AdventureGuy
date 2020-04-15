using UnityEngine;


public class Heart : MonoBehaviour
{
    #region Fields

    private int _healthPlus = 1;

    private Character _character;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D heart)
    {
        _character = heart.GetComponent<Character>();
        if (_character)
        {
            _character.Health = _character.Health + _healthPlus;
            _character.AudioGetHearth();
            Destroy(gameObject);
        }
    }

    #endregion
}