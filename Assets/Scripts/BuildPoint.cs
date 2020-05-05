using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    [Tooltip("Список возможных башен")]
    [SerializeField] public List<TowerData> _towers = new List<TowerData>();
    [Tooltip("Данные игрока")]
    [SerializeField] public PlayerData _playerData;
    [Tooltip("Построено ли здание")]
    [SerializeField] public bool _builded;
    [Tooltip("Текущая башня")]
    private GameObject _tower;
    [Tooltip("Данные текущей башни")]
    [SerializeField] private TowerData _currentData;

    //Постройка башни
    public bool BuildTower(int tower)
    {
        try
        {
            _currentData = _towers[tower];
            if (_currentData._BuildPrice <= _playerData._coins)
            {
                _playerData._coins -= _currentData._BuildPrice;
                _tower = Instantiate(_currentData._Tower, transform.position, Quaternion.identity, transform);
                _tower.transform.localPosition = Vector3.zero;
                TowerAction _action = _tower.AddComponent<TowerAction>();
                _action.Initialize(_currentData);
                _builded = true;
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
        return _builded;
    }

    //Улучшение башни
    internal bool UpgradeTower()
    {
        try
        {
            if (_tower != null)
            {
                GameObject _newTower;
                _currentData = _currentData._NextLevel;
                if (_playerData._coins >= _currentData._BuildPrice)
                {
                    _playerData._coins -= _currentData._BuildPrice;
                    _newTower = Instantiate(_currentData._Tower, transform.position, Quaternion.identity, transform);
                    _newTower.transform.localPosition = Vector3.zero;
                    TowerAction _action = _newTower.AddComponent<TowerAction>();
                    _action.Initialize(_currentData);
                    _builded = true;
                    Destroy(_tower);
                    _tower = _newTower;
                }
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        return false;
    }
    //Удаление башни
    internal void DeleteTower()
    {
        try
        {
            if (_playerData != null)
            {
                if (_currentData != null)
                {
                    _playerData._coins += _currentData._BuildPrice / 2;
                    _currentData = null;
                    Destroy(_tower);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            throw;
        }
        
    }
}
