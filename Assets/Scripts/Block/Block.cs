using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Block : MonoBehaviour
{
    [SerializeField] private Vector2Int _destroyPriceRange;

    public event UnityAction<int> FillingUpdate;

    private int _destroyPrice;
    private int _filling;

    public int LeftToFille => _destroyPrice - _filling;

    private void Start()
    {
        _destroyPrice = Random.Range(_destroyPriceRange.x, _destroyPriceRange.y);
        FillingUpdate?.Invoke(LeftToFille);
    }


    public void Fill()
    {
        _filling++;
        FillingUpdate?.Invoke(LeftToFille);
        if (_filling == _destroyPrice)
        {
            Destroy(gameObject);
        }
    }

}
