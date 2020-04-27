using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waveConfig.GetWaypoints()[waypointIndex].transform.position;
      
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfigParam)
    {
        this.waveConfig = waveConfigParam;
    }

    private void Move()
    {
        if (waypointIndex < waveConfig.GetWaypoints().Count)
        {
            var moveThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            var targetPosition = waveConfig.GetWaypoints()[waypointIndex].transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex += 1;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
