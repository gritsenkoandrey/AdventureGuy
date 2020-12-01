using UnityEngine;


public class DialogTrigger : MonoBehaviour
{
    #region Fields

    public Dialog dialog;

    #endregion


    #region Method

    private void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }

    #endregion
}