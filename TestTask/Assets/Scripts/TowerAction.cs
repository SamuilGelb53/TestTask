using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAction : MonoBehaviour
{
    [Tooltip("Данные башни")]
    [SerializeField] public TowerData _towerData;
    [Tooltip("Таймер стрельбы")]
    private float _timer;

    //Инициализируем данные башни, устанавливаем таймер
    internal void Initialize(TowerData towerData)
    {
        _towerData = towerData;
        _timer = Time.deltaTime;
    }

    public void FixedUpdate()
    {
        //Ищем все объекты в которые может стрелять эта башня (на случай если будут летающие, или другие особые типы противников) в радиусе
        Collider[] _colliders = Physics.OverlapSphere(transform.position, _towerData._Range, _towerData._WhatIsEnemy);
        if(_colliders.Length > 0)
        {
            //Для дебага нарисовал линию к первому в списке бота и Стреляем в него
            Debug.DrawLine(transform.position, _colliders[0].transform.position, Color.yellow);
            Shot(_colliders[0]);
        }
    }

    //Стрельба
    private void Shot(Collider collider)
    {
        //Если таймер стрельбы (задержка между выстрелами) позволяет
        if(Time.time > _timer)
        {
            //Создаем пулю, инициализируем данные
            GameObject bullet = Instantiate(_towerData._Bullet, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletAction>()._speed = _towerData._BulletSpeed;
            bullet.GetComponent<BulletAction>()._damage = _towerData._Damage;
            bullet.GetComponent<BulletAction>()._Finish = collider.transform;
            _timer = Time.time + _towerData._ShotInterval;
        }
    }

    
}
