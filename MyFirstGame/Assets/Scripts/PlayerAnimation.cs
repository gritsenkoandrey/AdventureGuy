using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        AnimRun();
        AnimJump();
    }

    private void AnimRun()
    {
        if (Input.GetButton("Horizontal"))
        {
            _anim.SetBool("isRanning", true);
        }
        else
        {
            _anim.SetBool("isRanning", false);
        }
    }

    private void AnimJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _anim.SetTrigger("Jump");
        }
    }
}