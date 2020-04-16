using UnityEngine;
//using UnityEngine.Events;


public class Heart : MonoBehaviour
{
    #region Fields

    private int _healthPlus = 1;

    private Character _character;

    //public UnityEvent _event;
    #endregion


    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D heart)
    {
        _character = heart.GetComponent<Character>();
        if (_character)
        {
            _character.AudioGetHearth();
            //_character.Hearth++;
            _character.Health += _healthPlus;
            Destroy(gameObject);
        }
    }

    #endregion
}