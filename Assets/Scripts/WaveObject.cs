using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Waves/Wave", fileName = "New Wave")]
public class WaveObject : ScriptableObject
{
    [Tooltip("Противники")]
    [SerializeField] public List<EnemyData> _enemys = new List<EnemyData>(3);
    [Tooltip("Колво противников")]//Можно было сделать отдельный класс
    [SerializeField] public List<int> _countOfEnemys = new List<int>(3);
    [Tooltip("Скорость появления ботов")]
    [SerializeField] public float _spawnSpeed = 1f;


}
