﻿using UnityEngine;


public class MovingPlatformY : MonoBehaviour
{
    [SerializeField] private Transform _point;

    [SerializeField] private float _speed;
    [SerializeField] private float _range;

    internal float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    private bool _isMovingUp;

    private void Update()
    {
        MovingPlatform();
    }

    private void MovingPlatform()
    {
        if (transform.position.y > _point.position.y + _range)
        {
            _isMovingUp = false;
        }

        else if (transform.position.y < _point.position.y - _range)
        {
            _isMovingUp = true;
        }

        if (_isMovingUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + _speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - _speed * Time.deltaTime);
        }
    }
}