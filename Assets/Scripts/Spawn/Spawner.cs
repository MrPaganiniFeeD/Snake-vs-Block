using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform _container;
    [SerializeField] private float _repeatCount;
    [SerializeField] private int _distanceBetweenFullLine;
    [SerializeField] private int _distanceBetweenRandomLine;
    [Header("Block")]
    [SerializeField] private Block _blockTemplate;
    [SerializeField] private int _blockSpawnChance;
    [Header("Bonuse")]
    [SerializeField] private Bonus _bonusTemplate;
    [SerializeField] private int _distanceBetweenBonus;
    [SerializeField] private int _bonusSpawnChane;

    private BlockSpawnPoint[] _blockSpawnPoint;
    private BonuseSpawnPoint[] _bonusSpawnPoint;

    private void Start()
    {
        _blockSpawnPoint = GetComponentsInChildren<BlockSpawnPoint>();
        _bonusSpawnPoint = GetComponentsInChildren<BonuseSpawnPoint>();

        for (int i = 0; i < _repeatCount; i++)
        {
            MoveSpawner(_distanceBetweenBonus);
            GenerateRandomElements(_bonusSpawnPoint, _bonusTemplate.gameObject, _bonusSpawnChane);
            MoveSpawner(_distanceBetweenFullLine);
            GenerateFullLine(_blockSpawnPoint, _blockTemplate.gameObject);
            MoveSpawner(_distanceBetweenRandomLine);
            GenerateRandomElements(_blockSpawnPoint, _blockTemplate.gameObject, _blockSpawnChance);

        }
    }
    private void GenerateFullLine(SpawnPoint[] spawnPoints, GameObject spawnElement)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GenerateGameObject(spawnPoints[i].transform.position, spawnElement);
        }
    }
    private void GenerateRandomElements(SpawnPoint[] spawnPoints, GameObject element, int chance)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if(Random.Range(0,100) < chance)
            {
                GameObject spawnElement = GenerateGameObject(spawnPoints[i].transform.position, element);
            }
        }
    }
    private GameObject GenerateGameObject(Vector3 spawnPoint, GameObject Element)
    {
        spawnPoint.y -= Element.transform.localScale.y;
        return Instantiate(Element, spawnPoint, Quaternion.identity, _container);
    }
    private void MoveSpawner(int distanceY)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + distanceY, transform.position.z);
    }
}
