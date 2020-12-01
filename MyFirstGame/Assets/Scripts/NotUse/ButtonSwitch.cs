using UnityEngine;


public class ButtonSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _platform;
    private bool _isClose = false;

    private void Update()
    {
        if(_isClose == true && _platform.transform.position.y > -1.0f)
        {
            _platform.transform.Translate(Vector2.down * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // transform.position.y > -4.0f - чтобы кнопка постоянно не активировалась
        if (collision.tag == "Player" && transform.position.y > -4.0f)
        {
            // Translate - чтобы кнопка опускалась плавно
            transform.Translate(Vector2.down * Time.deltaTime); 
        }

        // _platform - поднимается
        else if(collision.tag == "Player" && _platform.transform.position.y < 2.5f)
        {
            _platform.transform.Translate(Vector2.up * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // поднятие кнопки
        transform.position = new Vector2(transform.position.x, -3.5f);

        _isClose = true;
    }
}