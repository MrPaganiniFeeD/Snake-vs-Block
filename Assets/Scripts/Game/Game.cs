using Snake;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Game : MonoBehaviour
    {
        private const string Snake = "Snake";
        private const string SceneName = "SampleScene";

        [SerializeField] private Transform _spawnPoint;

        private SnakeRoot _snake;
        
        private void Awake()
        {
            StartGame();
        }

        private void StartGame()
        {
            SpawnSnake();
            InitSpawnTracker();
        }

        private void InitSpawnTracker()
        {
           SnakeTracker snakeTracker = Camera.main.gameObject.GetComponent<SnakeTracker>();
           SnakeHead snakeHead = _snake.GetComponentInChildren<SnakeHead>();
           snakeTracker.Construct(snakeHead);
        }

        private void SpawnSnake()
        {
            GameObject original = Resources.Load<GameObject>(Snake);
            GameObject prefab = Instantiate(original, _spawnPoint.position, Quaternion.identity, null);
            _snake = prefab.GetComponent<SnakeRoot>();
            _snake.Death += EndGame;
        }

        private void EndGame() => 
            SceneManager.LoadScene(SceneName);

        private void OnDestroy()
        {
            _snake.Death -= EndGame;
        }
    }
}
