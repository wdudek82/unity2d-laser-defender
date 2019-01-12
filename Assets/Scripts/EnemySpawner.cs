using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waveConfigs;
    [SerializeField] private float delayBetweenWaves = 0;
    [SerializeField] private int startingWave = 0;
    [SerializeField] private bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = 0; waveIndex < waveConfigs.Count; waveIndex++)
        {
            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigs[waveIndex]));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = startingWave; enemyCount < waveConfig.NumberOfEnemies; enemyCount++)
        {
            var newEnemy = Instantiate(
                waveConfig.EnemyPrefab,
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity
            );
            newEnemy.GetComponent<EnemyPathing>().WaveConfig = waveConfig;
            yield return new WaitForSeconds(waveConfig.TimeBetweenSpawns);
        }

        yield return new WaitForSeconds(delayBetweenWaves);
    }
}