using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInspector : MonoBehaviour
{
    [Tooltip("От чего будем получать урон")]
    [SerializeField] private LayerMask _whatIsMatter;
    [Tooltip("Расстояние в котором будем 'Ловить' пули")]
    [SerializeField] [Range(0.1f, 3f)] private float _distance = 0.2f;
    [Tooltip("Данные противника")]
    [SerializeField] private EnemyData _EnemyData;
    public EnemyData _enemyData
    {
        get { return _EnemyData; }
        set 
        { 
            _EnemyData = value;
            _healthmount = _enemyData._HealthAmount;
        }
    }
    [Tooltip("Здоровье бота")]
    [SerializeField] private float _healthmount = 0;


    public void FixedUpdate()
    {
        //Находим все колайдеры в _distance радиусе на слоях _whatIsMatter
        Collider[] _colliders = Physics.OverlapSphere(transform.position, _distance, _whatIsMatter);
        foreach (Collider item in _colliders)
        {
            //Проверяем какая это пуля (возможно расширить, чтобы сделать замедление или что-то похожее) (switch-case)
            if (item.tag == "Bullet")
                GetDamage(item.GetComponent<BulletAction>()._damage);
            else
                Debug.LogWarning("Some incorrect object in radius " + _distance + ", please fix LayerMask 'What Is Matter'");
        }
    }

    //Получение урона, проверка на смерть добавление монет
    internal void GetDamage(float v)
    {
        _healthmount -= v;
        if (_healthmount <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<PlayerData>()._coins += _enemyData._Cost;
        }
    }
}
