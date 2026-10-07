using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController enemyPrefab;

    [SerializeField] private float startingSpawnInterval = 3f;
    [SerializeField] private float minimumSpawnInterval = 1f;
    [SerializeField] private float spawnDistance = 6f;

    [SerializeField] private int scorePerDifficultyIncrease = 30;
    [SerializeField] private float intervalDecrease = 0.5f;

    private float timer;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= GetCurrentSpawnInterval())
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private float GetCurrentSpawnInterval()
    {
        if (gameManager == null)
            return startingSpawnInterval;

        int difficultyLevel =
            gameManager.Score / scorePerDifficultyIncrease;

        float currentInterval =
            startingSpawnInterval -
            (difficultyLevel * intervalDecrease);

        return Mathf.Max(
            currentInterval,
            minimumSpawnInterval
        );
    }

    private void SpawnEnemy()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        Vector2 spawnPosition =
            (Vector2)transform.position +
            randomDirection * spawnDistance;

        Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}