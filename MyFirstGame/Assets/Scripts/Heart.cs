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
            AudioSound._audioSound.AudioGetHearth();
            _character.Health += _healthPlus;
            Destroy(gameObject);
        }
    }

    #endregion
}