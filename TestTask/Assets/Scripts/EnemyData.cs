using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Standart Enemy", fileName = "New Enemy")]
public class EnemyData : ScriptableObject
{
    [Tooltip("Колличество здоровья")]
    [SerializeField] private float _healthAmount;
    public float _HealthAmount
    {
        get { return _healthAmount; }
        set { _healthAmount = value; }
    }

    [Tooltip("Колличество здоровья")]
    [Range(1f, 10f)]
    [SerializeField] private float _movingSpeed = 2f;
    public float _MovingSpeed
    {
        get { return _movingSpeed; }
        set { _movingSpeed = value; }
    }

    [Tooltip("Колличество здоровья")]
    [SerializeField] private float _damage;
    public float _Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    [Tooltip("Модель врага")]
    [SerializeField] private GameObject _object;
    public GameObject _Object
    {
        get { return _object; }
        set { _object = value; }
    }

    [Tooltip("Цена смерти")]
    [SerializeField] private int _cost;
    public int _Cost
    {
        get { return _cost; }
        set { _cost = value; }
    }


}
