using UnityEngine;
using UnityEngine.SceneManagement;


public class Coin : MonoBehaviour
{
    #region Fields

    [SerializeField] private int _loadingScene;
    private float _timeLoadindScene = 0.3f;

    private Character _character;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter2D(Collider2D coin)
    {
        _character = coin.GetComponent<Character>();
        if(_character)
        {
            AudioSound._audioSound.AudioGetCoin();
            Invoke(nameof(Scene), _timeLoadindScene);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    #endregion


    #region Method

    public void Scene()
    {
        SceneManager.LoadScene(_loadingScene);
    }

    #endregion
}