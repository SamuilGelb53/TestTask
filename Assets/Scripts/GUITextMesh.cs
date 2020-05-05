using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITextMesh : MonoBehaviour
{
    [Tooltip("Главная камера")]
    [SerializeField] public Camera _cam;
    [Tooltip("Кординаты клика")]
    [SerializeField] public Vector3 _vec;
    [Tooltip("Родитель кнопок постройки")]
    [SerializeField] public Transform _buildButtons;
    [Tooltip("Родитель кнопок улучшения/удаления")]
    [SerializeField] public Transform _upgradeButtons;
    //На самом деле я плохо знаком с UI и раньше работал только с OnGUI
    [Tooltip("Стандартная площадь монитора, для изменяемого интерфейса")]
    [SerializeField] private const int _size = 360000;
    [Tooltip("Масштаб относительно стандартного размера")]
    [SerializeField] public float _relativeSize;
    [Tooltip("Текущая точка постройки")]
    [SerializeField] public BuildPoint _currentPoint;
    [Tooltip("Отображение кнопок улучшения/удаления")]
    [SerializeField] private bool _activeUpgrade;
    public bool _ActiveUpgrade
    {
        get { return _activeUpgrade; }
        set 
        { 
            _activeUpgrade = value;
            _upgradeButtons.gameObject.SetActive(_activeUpgrade);
            _upgradeButtons.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
    [Tooltip("Отображение кнопок постройки")]
    [SerializeField] private bool _activeBuild = false;
    public bool _ActiveBuild
    {
        get { return _activeBuild; }
        set 
        { 
            _activeBuild = value;
            _buildButtons.gameObject.SetActive(_activeBuild);
            _buildButtons.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    //Инициализируем размеры под экран и убираем кнопки
    public void Start()
    {
        int siz = Screen.width * Screen.height;
        _relativeSize = Mathf.Sqrt(siz / _size);
        _buildButtons.localScale = Vector3.one * _relativeSize;
        _buildButtons.gameObject.SetActive(_ActiveBuild);
        _upgradeButtons.localScale = Vector3.one * _relativeSize;
        _upgradeButtons.gameObject.SetActive(_ActiveUpgrade);

    }

    void Update()
    {
        //Определяем важные слои
        int layer_mask = (1 << 9)|(1<<8);
        //При клике
        if (Input.GetButtonDown("Fire1"))
        {
            if (!_ActiveBuild && !_ActiveUpgrade)
            {
                _vec = Input.mousePosition;
                Ray _ray = _cam.ScreenPointToRay(_vec);
                RaycastHit _hit;
                //Пускаем луч в сцену от камеры
                if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, layer_mask))
                {
                    //Берем компонент BuildPoint и определяем строить/улучшать
                    _currentPoint = _hit.transform.GetComponent<BuildPoint>();
                    if(_currentPoint._builded)
                    {
                        _ActiveUpgrade = true;
                        _ActiveBuild = false;
                        _upgradeButtons.position = new Vector3(_vec.x, _vec.y, _upgradeButtons.position.z);
                    }
                    else
                    {
                        _ActiveBuild = true;
                        _ActiveUpgrade = false;
                        _buildButtons.position = new Vector3(_vec.x, _vec.y, _buildButtons.position.z);
                    }
                }
            }
            //Если клик далеко, хотя открыто окно (постройки/улучшений) - закрыть открытые окна
            if (Mathf.Abs(Vector3.Distance(Input.mousePosition, _vec)) > 90 * _relativeSize && (_ActiveBuild ||_ActiveUpgrade))
            {
                _vec = Vector3.zero;
                _ActiveBuild = false;
                _ActiveUpgrade = false;
                _currentPoint = null;
            }
        }
    }


    //вызов Постройки башни
    public void BuildTower(int t)
    {
        if(_currentPoint != null)
        {
            if(_currentPoint.BuildTower(t))
            {
                _ActiveBuild = false;
                _currentPoint = null;
            }
            else
            {
                Debug.Log("NotEnoughtMoney");
            }
        }
    }

    //вызов Улучшения башни
    public void UpgradeTower()
    {
        if(_currentPoint != null)
        {
            _currentPoint.UpgradeTower();
            _ActiveUpgrade = false;
            _currentPoint = null;
        }
    }
    //Вызов удаления башни
    public void DeleteTower()
    {
        if(_currentPoint != null)
        {
            _currentPoint.DeleteTower();
            _ActiveUpgrade = false;
            _currentPoint._builded = false;
            _currentPoint = null;
        }
    }
}
