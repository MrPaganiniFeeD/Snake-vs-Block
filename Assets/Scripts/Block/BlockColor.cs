using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlockColor : MonoBehaviour
{
    [SerializeField] private Color[] _colors;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor(GetRandomColor());

    }
    private void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }
    private Color GetRandomColor()
    {
        return _colors[Random.Range(0, _colors.Length)];
    }

}
