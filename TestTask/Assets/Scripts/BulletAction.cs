using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    [Tooltip("Точка стремления (куда двигаться)")]
    [SerializeField] private Transform _finish;
    public Transform _Finish
    {
        get { return _finish; }
        set 
        { 
            _finish = value;
            _relativeDistance = _speed / Vector3.Distance(_startPoint, _finish.position);
        }
    }
    [Tooltip("Урон наносимый боту")]
    [SerializeField] internal float _damage;
    [Tooltip("Начальная точка")]
    private Vector3 _startPoint;
    [Tooltip("Скорость пули")]
    [SerializeField] [Range(1f, 10f)] internal float _speed = 10f;
    [Tooltip("Допустимая дистанция")]
    [SerializeField] [Range(0.01f, 1f)] private float _allowableDistance = 0.1f;
    [Tooltip("Таймер движения")]
    private float _timer = 0f;
    [Tooltip("Относительное расстояние")]
    private float _relativeDistance = 0.1f;

    //Инициализируем начальную точку
    public void Awake()
    {
        _startPoint = transform.position;
    }

    public void FixedUpdate()
    {
        //Если конечная точка существует - двигаемся, иначе уничтожаем
        if (_finish != null)
        {
            if (Vector3.Distance(transform.position, _finish.position) > _allowableDistance)
            {
                transform.position = Vector3.Lerp(_startPoint, _finish.position, _timer);
                transform.LookAt(_finish.position);
                _timer += Time.deltaTime * _relativeDistance;
            }
            else
            {
                //Пробуем отослать урон
                try
                {
                    _finish.GetComponent<BotInspector>().GetDamage(_damage);
                    Destroy(gameObject);
                }
                catch (System.Exception ex)
                {
                    Destroy(gameObject);
                    Debug.Log(ex.Message);
                }

            }
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
