using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    private Vector3 direction;
    public float gravity = -20f;
    public float strength = 5f;
    private AudioSource audioSource;
    public AudioClip flapSound;
    public AudioClip gameOverSound1;
    public AudioClip gameOverSound2;
    public AudioClip pointSound;

    public float deathHeight = -3.25f;
    public float fallSpeed = 2.0f;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            direction = Vector3.up * strength;
            audioSource.PlayOneShot(flapSound);
        }

        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime; 
    }

    public void Reset()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void AnimateSprite()
    {
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            audioSource.PlayOneShot(gameOverSound1);
            audioSource.PlayOneShot(gameOverSound2);
            FindObjectOfType<GameManager>().GameOver();
        } else if (other.gameObject.tag == "Scoring")
        {
            audioSource.PlayOneShot(pointSound);
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}
