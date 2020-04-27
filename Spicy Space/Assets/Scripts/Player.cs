using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 500;

    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] GameObject cannonballPrefab;

    [Header("Sound Effects")]
    [SerializeField] AudioClip audioClipDeath;
    [SerializeField] [Range(0, 1f)] float volumeDeath = 1f;
    [SerializeField] AudioClip audioClipShoot;
    [SerializeField] [Range(0, 1f)] float volumeShoot = 1f;
    Coroutine firingCoroutine;


    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBounderies();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    public int GetHealth()
    {
        return health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            health = Mathf.Max(health - damageDealer.GetDamage(), 0); // Avoid negative score
            damageDealer.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
        
    }

    private void Die()
    {
        StartCoroutine(PlayDeathMusicAndWait());
    }

    private IEnumerator PlayDeathMusicAndWait()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        AudioSource.PlayClipAtPoint(audioClipDeath, Camera.main.transform.position, volumeDeath);
        yield return new WaitForSeconds(audioClipDeath.length);
        FindObjectOfType<Level>().LoadGameOver();
    }


    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireContinuously()
    {
        AudioSource.PlayClipAtPoint(audioClipShoot, Camera.main.transform.position, volumeShoot);
        while(true)
        {
            GameObject cannonball =
                Instantiate(cannonballPrefab, transform.position, Quaternion.identity) as GameObject;
            cannonball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);

        }

    }

    private void SetUpMoveBounderies()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPosition, newYPosition);
    }
}
