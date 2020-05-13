using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    [Tooltip("Колличество денег")]
    [SerializeField] private int _Coins;
    public int _coins
    {
        get { return _Coins; }
        set
        {
            _Coins = value;
            //При любом изменении значений будет обновлятся текст
            UpdateText();
        }
    }
    [Tooltip("Количество жизней")]
    [SerializeField] private float _Lives = 15;
    public float _lives
    {
        get { return _Lives; }
        set
        {
            _Lives = value;
            //При любом изменении значений будет обновлятся текст, если жизни закончились - перезапустить уровень
            if (_Lives <= 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            UpdateText();
        }
    }
    [Tooltip("Текст меш для данных пользователя")]
    [SerializeField] private TextMeshProUGUI _playerDataText;

    public void Awake()
    {
        _coins = _Coins;
        _lives = _Lives;
    }

    //Обновление текста
    public void UpdateText()
    {
        _playerDataText.text = "Lives: " + (int)_lives + "\nCoins: " + (int)_coins;
    }
}
