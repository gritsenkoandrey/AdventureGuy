using UnityEngine;
using UnityEngine.SceneManagement;


public class Coin : MonoBehaviour
{
    #region Fields

    [SerializeField] private int _loadingScene;
    private float _timeLoadindScene = 1.0f;

    private Character _character;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D coin)
    {
        _character = coin.GetComponent<Character>();
        if(_character)
        {
            _character.AudioGetCoin();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Invoke(nameof(Scene), _timeLoadindScene);
        }
    }

    #endregion


    #region Method

    private void Scene()
    {
        SceneManager.LoadScene(_loadingScene);
    }

    #endregion
}