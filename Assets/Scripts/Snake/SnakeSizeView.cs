using TMPro;
using UnityEngine;

namespace Snake
{
    [RequireComponent(typeof(SnakeRoot))]
    public class SnakeSizeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _view;

        private SnakeRoot _snakeRoot;

        private void Awake()
        {
            _snakeRoot = GetComponent<SnakeRoot>();
            OnSizeUpdate(_snakeRoot.CurrentSize);
        }
        private void OnEnable()
        {
            _snakeRoot.SizeUpdate += OnSizeUpdate;
        }
        private void OnDisable()
        {
            _snakeRoot.SizeUpdate -= OnSizeUpdate;
        }
        private void OnSizeUpdate(int size) => 
            _view.text = size.ToString();
    }
}
