using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Block))]
public class BlockView : MonoBehaviour
{
    [SerializeField] private TMP_Text _view;
    private Block _block;

    private void Awake()
    {
        _block = GetComponent<Block>();
    }
    private void OnEnable()
    {
        _block.FillingUpdate += OnFillingUpdate;
    }
    private void OnDisable()
    {
        _block.FillingUpdate -= OnFillingUpdate;
    }
    private void OnFillingUpdate(int leftToFilling)
    {
        _view.text = leftToFilling.ToString();
    }

}
