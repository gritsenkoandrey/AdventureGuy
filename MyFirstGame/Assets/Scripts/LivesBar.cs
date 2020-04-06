using UnityEngine;


public class LivesBar : MonoBehaviour
{
    #region Fields
    // создаем массив из 5 элементов (сердец)
    private Transform[] _hearts = new Transform[5];
    private Character _character;
    #endregion


    #region UnityMethods
    private void Awake()
    {
        _character = FindObjectOfType<Character>();

        for (int i = 0; i < _hearts.Length; i++)
        {
            _hearts[i] = transform.GetChild(i);
        }
    }
    #endregion


    #region Methods
    // метод который будет изменять количество сердец
    public void Refresh()
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i < _character.Health)
                _hearts[i].gameObject.SetActive(true);
            else
                _hearts[i].gameObject.SetActive(false);
        }
    }
    #endregion
}