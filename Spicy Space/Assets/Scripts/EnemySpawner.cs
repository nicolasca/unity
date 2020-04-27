using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;
 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAllWaves());

    }

    private IEnumerator SpawnAllWaves()
    {
        for(var waveConfigCount = 0; waveConfigCount <= waveConfigs.Count; waveConfigCount++)
        {
            if (waveConfigCount  < waveConfigs.Count)
            {
                var currentEnemyCoRountine = StartCoroutine(SpawnAllEnemies(waveConfigs[waveConfigCount]));
                yield return currentEnemyCoRountine;
            } else if (looping)
            {
                waveConfigCount = -1;
            }
           
            
        }
    }

    private IEnumerator SpawnAllEnemies(WaveConfig waveConfig)
    {
        for (var enemycount = 0; enemycount < waveConfig.GetNumberOfEnemies(); enemycount++)
        {
            var newEnemy = Instantiate(
               waveConfig.GetEnemyPrefab(),
               waveConfig.GetWaypoints()[0].transform.position,
               waveConfig.GetEnemyPrefab().transform.rotation
            ) ;
            Debug.Log(newEnemy.transform.rotation);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }

    }
}
