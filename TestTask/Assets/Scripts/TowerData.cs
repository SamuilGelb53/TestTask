using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Towers/Tower", fileName = "New Tower")]

public class TowerData : ScriptableObject
{
    [Tooltip("Цена постройки")]
    [SerializeField] private int _buildPrice;
    public int _BuildPrice
    {
        get { return _buildPrice; }
        set { _buildPrice = value; }
    }

    [Tooltip("Дистанция атаки")]
    [SerializeField] private float _range;
    public float _Range
    {
        get { return _range; }
        set { _range = value; }
    }

    [Tooltip("Интервал между выстрелами")]
    [SerializeField] private float _shotInterval;
    public float _ShotInterval
    {
        get { return _shotInterval; }
        set { _shotInterval = value; }
    }

    [Tooltip("Наносимый урон")]
    [SerializeField] private float _damage;
    public float _Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    [Tooltip("Скорость пули")]
    [SerializeField] private float _bulletSpeed;

    public float _BulletSpeed
    {
        get { return _bulletSpeed; }
        set { _bulletSpeed = value; }
    }

    [Tooltip("Доступные противники")]//На случай летающих или еще каких нибудь уникальных противников
    [SerializeField] private LayerMask _whatIsEnemy;

    public LayerMask _WhatIsEnemy
    {
        get { return _whatIsEnemy; }
        set { _whatIsEnemy = value; }
    }

    [Tooltip("TowerData следующего уровня башни")]
    [SerializeField] private TowerData _nextLevel;

    public TowerData _NextLevel
    {
        get { return _nextLevel; }
        set { _nextLevel = value; }
    }

    [Space]
    [Header("Game object, visible part")]
    [Space]
    [Tooltip("Снаряд")]
    [SerializeField] private GameObject _bullet;
    public GameObject _Bullet
    {
        get { return _bullet; }
        set { _bullet = value; }
    }

    [Tooltip("Башня")]
    [SerializeField] private GameObject _tower;
    public GameObject _Tower
    {
        get { return _tower; }
        set { _tower = value; }
    }

    

}
