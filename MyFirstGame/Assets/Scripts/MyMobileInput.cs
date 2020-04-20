using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

#if UNITY_STANDALONE_WIN
#endif

#if UNITY_EDITOR_WIN
#endif

public class MyMobileInput : MonoBehaviour
{


    // Use this for initialization
    void Start ()
    {
#if UNITY_ANDROID
        Handheld.Vibrate(); // Вибрация

        // определение языка системы
        if (Application.systemLanguage == SystemLanguage.Russian)
            print(Application.systemLanguage);

        // Определение платформы
       //   if (Application.platform == RuntimePlatform.Android)
          print(Application.platform);

        Screen.sleepTimeout = SleepTimeout.NeverSleep; // Управление подсветкой

        // Доступ к камере


        WebCamTexture camTexture = new WebCamTexture();
        GetComponent<MeshRenderer>().material.mainTexture = camTexture;
        camTexture.Play();

#endif

    }

    // Update is called once per frame
    void Update()
    {
        // Проверка касания
        if (Input.touchCount > 0)
        {
            Debug.Log(Input.touchCount); // количество прикосновений
            if (Input.touches[0].phase == TouchPhase.Began) //Палец коснулся экрана.
            {
                Debug.Log("Began");
            }

            else if (Input.touches[0].phase == TouchPhase.Ended)// Палец был снят с экрана.
            {
                Debug.Log("Ended");
            }

            else if (Input.touches[0].phase == TouchPhase.Moved)// Палец перемещается по экрану
            {
                Debug.Log("Moved");
            }

            else if (Input.touches[0].phase == TouchPhase.Stationary)// Палец прикоснулся к экрану, но не сдвинулся.
            {
                Debug.Log("Stationary");
            }

            Collider col = GetComponent<Collider>();

            // Определение, в какую сторону сдвинулся палец:
            Vector2 delta = Input.GetTouch(0).deltaPosition;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                if (delta.x > 0)
                {
                    Debug.Log("Right");
                }
                else
                {
                    Debug.Log("Left");
                }
            }
            else
            {
                if (delta.y > 0)
                {
                    Debug.Log("Up");
                }
                else
                {
                    Debug.Log("Down");
                }
            }

        }
    }
}
