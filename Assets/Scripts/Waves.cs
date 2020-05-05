using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [Tooltip("Список волн")]
    [SerializeField] public List<WaveObject> _waves = new List<WaveObject>();
    [Tooltip("Текущий тип")]
    private int _types;
    [Tooltip("Кол-во созданых противников конкретного типа")]
    private int _created;
    [Tooltip("Текущая волна")]
    private int _currentWave;
    [Tooltip("Держатель пути движения (родитель всех точек)")]
    [SerializeField] public Transform _pathHolder;
    [Tooltip("Время между волнами")]
    [SerializeField] public float _waveDelay = 5f;
    [Tooltip("Объект текста волн")]
    [SerializeField] public TextMeshProUGUI _wavesText;

    public void Start()
    {
        //Начинаем первую волну при запуске
        StartCoroutine(WaitNexWave());
    }

    public IEnumerator Spawn()
    {
        //Ожидаем время след. спавна
        yield return new WaitForSeconds(_waves[_currentWave]._spawnSpeed);
        //Если еще не заспавнено достаточно ботов спавним еще
        if (_waves[_currentWave]._countOfEnemys[_types] > _created)
        {
            //Создаем бота, инициализируем данные
            GameObject enemy = Instantiate(_waves[_currentWave]._enemys[_types]._Object, transform.position, Quaternion.identity, transform);
            BotInspector inspector = enemy.AddComponent<BotInspector>();
            inspector._enemyData = _waves[_currentWave]._enemys[_types];
            BotMovement movement = enemy.AddComponent<BotMovement>();
            movement.Initialize(_waves[_currentWave]._enemys[_types], _pathHolder);
            _created++;
            //Запускаем новый спавн
            StartCoroutine(Spawn());
        }
        else
        {
            //Если еще не все типы в волне заспавнены переходим к следующему
            if (_types + 1 < _waves[_currentWave]._countOfEnemys.Count)
            {
                _types++;
                _created = 0;
                StartCoroutine(Spawn());
            }
            //Иначе проверяем, все ли волны прошли
            else
            {
                if (_waves.Count > _currentWave + 1)
                {
                    _currentWave++;
                    _types = 0;
                    _created = 0;
                    //Запускаем новую волну через "Время между волнами"
                    StartCoroutine(WaitNexWave());
                }
            }
        }
    }

    //Ожидаем спавна след. волны и запускаем
    private IEnumerator WaitNexWave()
    {
        yield return new WaitForSeconds(_waveDelay);
        StartCoroutine(Spawn());
        //Меняем текст связаный с полнами
        _wavesText.text = "Wave: " + (_currentWave + 1) + "/" + _waves.Count;
    }
}
