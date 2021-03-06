﻿using UnityEngine;
using System.Linq;


public class MoveableMonster : Monster
{
    #region Fields

    [SerializeField] private float _speed = 2.0f;

    private Vector3 _direction;

    #endregion


    #region UnityMethods

    protected override void Start()
    {
        _direction = transform.right;
    }

    protected override void Update()
    {
        Move();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        // этого монстра можно убить только прыгнув на него сверху
        // Mathf.Abs - модуль числа, если мы заходим слева при Х = -число, то модуль Х = +число
        var unit = collider.GetComponent<Unit>();
        if (unit && unit as Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.5f)
            {
                ReceiveDamage();
                Explosion();
            }
            else
            {
                unit.ReceiveDamage();
            }
        }
    }

    #endregion


    #region Methods

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            transform.position + transform.up * 0.1f + transform.right * _direction.x * 0.55f, 0.1f);
        // если в массиве нет элемента, который принадлежит персонажу, то меняем направление движения
        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<Character>()))
            _direction *= -1.0f;

        transform.position = Vector3.MoveTowards(transform.position, 
                transform.position + _direction, _speed * Time.deltaTime);
    }

    #endregion
}