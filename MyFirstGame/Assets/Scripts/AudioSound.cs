using UnityEngine;


public class AudioSound : MonoBehaviour
{
    // класс под AudioSource создал, но пока не реализовал в проекте

    #region Fields

    public static AudioSound _audioSound;
    private AudioSource _audio;
    [SerializeField] private AudioClip _audioClipBulletCharacter;
    [SerializeField] private AudioClip _audioClipJumpCharacter;
    [SerializeField] private AudioClip _audioClipHeart;
    [SerializeField] private AudioClip _audioClipJumpForce;
    [SerializeField] private AudioClip _audioClipCoin;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _audioSound = this;
        _audio = GetComponent<AudioSource>();
    }

    #endregion


    #region Method

    internal void AudioBulletCharacter()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            _audio.PlayOneShot(_audioClipBulletCharacter);
        }
    }

    internal void AudioJumpCharacter()
    {
        if(Input.GetButtonDown("Jump"))
        {
            _audio.PlayOneShot(_audioClipJumpCharacter);
        }
    }

    internal void AudioGetHearth()
    {
        _audio.PlayOneShot(_audioClipHeart);
    }

    internal void AudioGetPowerJump()
    {
        _audio.PlayOneShot(_audioClipJumpForce);
    }

    internal void AudioGetCoin()
    {
        _audio.PlayOneShot(_audioClipCoin);
    }

    #endregion
}