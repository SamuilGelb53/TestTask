using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    [Tooltip("Точки для пути движения")]
    [SerializeField] public List<Transform> _path = new List<Transform>();
    [Tooltip("Главный объект, содержащий точки для пути движения")]
    [SerializeField] public Transform _pathHolder;
    [Tooltip("Текущая точка")]
    [SerializeField] internal int _currentPoint = 0;
    [Tooltip("Кординаты следующей точки")]
    [SerializeField] internal Vector3 _pointToMove;
    [Tooltip("Кординаты предыдущей точки")]
    [SerializeField] internal Vector3 _previousPoint;
    [Tooltip("Расстояние для смены точки")]
    [Range(0.01f, 1f)]
    [SerializeField] public float _allowableDistance = 0.5f;
    [Tooltip("Таймер движения")]
    [SerializeField] internal float _timer;
    [Tooltip("Относительное расстояние")]
    [SerializeField] public float _relativeDistance;
    [Tooltip("Enemy Data объект с данными врага")]
    [SerializeField] public EnemyData _data;
    [Tooltip("Данные игрока")]
    [SerializeField] private PlayerData _playerData;

    //Инициализируем данные (после спавна нового бота)
    public void Initialize(EnemyData data, Transform pathHolder)
    {
        _pathHolder = pathHolder;
        _data = data;
        //Инициализируем данные игрока, если еще не задано
        if (_playerData == null)
        {
            _playerData = FindObjectOfType<PlayerData>();
        }
        //Если точки пути не заданы, задаем, и указываем следующую точку и текущую
        if (_path.Count == 0)
            if (_pathHolder != null)
            {
                for (int i = 0; i < _pathHolder.childCount; i++)
                {
                    _path = new List<Transform>(_pathHolder.GetComponentsInChildren<Transform>());
                    _path.RemoveAt(0);
                    _pointToMove = _path[0].position;
                    _previousPoint = transform.position;
                    _relativeDistance = _data._MovingSpeed / Vector3.Distance(_previousPoint, _pointToMove);
                }
            }
            else
            {
                Debug.LogError("Put path in list or path holder in variable");
            }
        else
        {
            _pointToMove = _path[0].position;
            _previousPoint = transform.position; 
            _relativeDistance = _data._MovingSpeed / Vector3.Distance(_previousPoint, _pointToMove);
        }
    }

    public void FixedUpdate()
    {
        //Проверяем дистанцию до точки движения, если еще далеко - идем 
        if (Vector3.Distance(transform.position, _pointToMove) > _allowableDistance)
        {
            transform.position = Vector3.Lerp(_previousPoint, _pointToMove, _timer);
            transform.LookAt(_pointToMove);
            _timer += Time.deltaTime * _relativeDistance;
        }
        else
        {
            //Если есть следующая точка, меняем текущую и следующую, меняем относительный множитель скорости движения в зависимости от расстояния
            if (_currentPoint + 1 < _path.Count)
            {
                _previousPoint = _path[_currentPoint].position;
                _currentPoint += 1;
                _pointToMove = _path[_currentPoint].position;
                _timer = 0;
                _relativeDistance = _data._MovingSpeed / Vector3.Distance(_previousPoint, _pointToMove);
            }
            else
            {
                //Если точек больше нет - мы добрались до замка игрока
                _playerData._lives -= _data._Damage;
                Destroy(gameObject);
                //Debug.Log("Damage");
            }
        }
    }
}
