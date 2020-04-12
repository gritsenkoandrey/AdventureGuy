using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private Text _nameText;
    [SerializeField] private Text _dialogText;
    [SerializeField] private Animator _animatorTextDialog;
    [SerializeField] private Animator _animatorStartDialog;

    private Queue<string> _sentences; // Класс Queue<T> представляет обычную очередь, работающую по алгоритму FIFO ("первый вошел - первый вышел")

    #endregion


    #region UnityMethod

    private void Start()
    {
        _sentences = new Queue<string>();
    }

    #endregion


    #region Method

    internal void StartDialog(Dialog dialog)
    {
        // при запуске игры, анимация FirstDialog "выкл", после нажатия кнопки StartTutorial, анимация SecondDialog "вкл".
        // анимации отличаются параметром в Canvas Group - Alpha, где 0 - отображение "выкл", 1 - отображение "вкл".

        _animatorTextDialog.SetBool("isFirst", true);
        _animatorStartDialog.SetBool("isStart", true);

        _nameText.text = dialog._name;
        _sentences.Clear();
        foreach(string sentence in dialog._sentences)
        {
            _sentences.Enqueue(sentence); // Enqueue: добавляет элемент в конец очереди
        }
        DisplayNextSentence();
    }

    private void DisplayNextSentence() // вывод следующей буквы
    {
        if(_sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        string sentence = _sentences.Dequeue(); // Dequeue: извлекает и возвращает первый элемент очереди

        //_dialogText.text = sentence; // StartCoroutine(TypeSentence(sentence)) замена данному способу
        StartCoroutine(TypeSentence(sentence));
    }

    private void EndDialog()
    {
        _animatorTextDialog.SetBool("isFirst", false);
        _animatorStartDialog.SetBool("isStart", false);
    }

    #endregion


    #region IEnumerator

    // Интерфейс позволяет тексту в поле _dialogText появляться не сразу, а по одной букве
    IEnumerator TypeSentence(string sentence)
    {
        _dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            _dialogText.text += letter;
            yield return null;
        }
    }

    #endregion
}