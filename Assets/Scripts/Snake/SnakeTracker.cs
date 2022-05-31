using UnityEngine;

namespace Snake
{
    public class SnakeTracker : MonoBehaviour
    {
        [SerializeField] private SnakeHead _snakeHead;
        [SerializeField] private float _speed;
        [SerializeField] private float offsetY;

        public void Construct(SnakeHead snakeHead) => 
            _snakeHead = snakeHead;

        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, GetTargetPosition(), _speed * Time.fixedDeltaTime);
        }

        private Vector3 GetTargetPosition() => 
            new Vector3(transform.position.x, _snakeHead.transform.position.y + offsetY, transform.position.z);
    }
}
