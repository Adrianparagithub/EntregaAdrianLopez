using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables
    public float speed = 5.5f;
    public float fireRate = 0.25f;
    public int lives = 3;
    public int maxLives = 4;
    public int shieldsAmount = 3;
    public float canFire = 0.0f; //Time to fire again
    public float shieldDuration = 5.0f;
    

    //TAREA
    public GameObject lifePanel; // Referencia al panel de vidas
    public GameObject lifeImagePrefab; // Prefab de la imagen de vida

    public GameObject BulletPref;
    public GameObject shield;
    public int actualWeapon = 0;

    //TAREA
    public bool doubleShotActive = false;
    private float doubleShotDuration = 5.0f;

    //To use audio
    public AudioManager audioManager;
    public AudioSource actualAudio;

    public enum ShipState
    {
        FullHealth,
        SlightlyDamaged,
        Damaged,
        HeavilyDamaged,
        Destroyed
    }

    public ShipState shipState;
    public List<Sprite> shipSprites = new List<Sprite>();

    public LifeManager lifeManager;

    private void Start()
    {
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckBoundaries();
        ChangeWeapon();
        UseShields();
        Fire();
        
    }
    void ChangeShipState()
    {
        var currentState = shipState;
        Debug.Log(currentState);

        var newSprite = shipSprites.Find(x => x.name == currentState.ToString());

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite;

        switch (currentState)
        {
            case ShipState.FullHealth:
                shipState = ShipState.SlightlyDamaged;
                break;
            case ShipState.SlightlyDamaged:
                shipState = ShipState.Damaged;
                break;
            case ShipState.Damaged:
                shipState = ShipState.HeavilyDamaged;
                break;
            case ShipState.HeavilyDamaged:
                shipState = ShipState.Destroyed;
                break;
            case ShipState.Destroyed:
                break;
        }
    }
    void UseShields()
    {
        if (Input.GetKeyDown(KeyCode.Z) && shieldsAmount > 0)
        {
            shieldsAmount--;
            shield.SetActive(true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (shield.activeSelf)
        {
            shieldDuration -= Time.deltaTime;
            if (shieldDuration < 0)
            {
                shield.SetActive(false);
                shieldDuration = 5.0f;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    

    //Character Movement, use WASD keys to move the player
    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

    }

    void CheckBoundaries()
    {
        //Check for boundaries of the game, use Main Camera to set the boundaries
        var cam = Camera.main;
        float xMax = cam.orthographicSize * cam.aspect;
        float yMax = cam.orthographicSize;
        if (transform.position.x > xMax)
        {
            transform.position = new Vector3(-xMax, transform.position.y, 0);
        }
        else if (transform.position.x < -xMax)
        {
            transform.position = new Vector3(xMax, transform.position.y, 0);
        }
        if (transform.position.y > yMax)
        {
            transform.position = new Vector3(transform.position.x, -yMax, 0);
        }
        else if (transform.position.y < -yMax)
        {
            transform.position = new Vector3(transform.position.x, yMax, 0);
        }
    }

    //Player Fire
    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            switch (BulletPref.name)
            {
                case "Bullet":
                    if (doubleShotActive)
                    {
                        // Doble disparo TAREA
                        Instantiate(BulletPref, transform.localPosition + new Vector3(-0.2f, 0.4f, 0), Quaternion.identity);
                        Instantiate(BulletPref, transform.localPosition + new Vector3(0.2f, 0.4f, 0), Quaternion.identity);
                    }

                    else
                    { 
                    Instantiate(BulletPref, transform.localPosition + new Vector3(0, 0.4f, 0), Quaternion.identity);

                    canFire = Time.time + fireRate;
                    actualAudio.Play();
                    }
                    break;

                case "Missile":
                    var bullet1 = Instantiate(BulletPref, transform.localPosition + new Vector3(0, 0.4f, 0), Quaternion.identity);
                    bullet1.GetComponent<Missile>().direction = Vector2.up;

                    var bullet2 = Instantiate(BulletPref, transform.localPosition + new Vector3(0.5f,0.4f, 0), Quaternion.identity);
                    bullet2.GetComponent<Missile>().direction = new Vector2(0.5f, 1);

                    var bullet3 = Instantiate(BulletPref, transform.localPosition + new Vector3(-0.5f, 0.4f, 0), Quaternion.identity);
                    bullet3.GetComponent<Missile>().direction = new Vector2(-0.5f, 1);

                    canFire = Time.time + fireRate;
                    actualAudio.pitch = Random.Range(1f, 1.5f);
                    actualAudio.Play();

                    break;
                case "EnergyBall":
                    var bullet4 = Instantiate(BulletPref, transform.localPosition + new Vector3(-0.25f, 0.4f, 0), Quaternion.identity);
                    bullet4.GetComponent<EnergyBall>().direction = new Vector2(-1, 0);
                    var bullet5 = Instantiate(BulletPref, transform.localPosition + new Vector3(0.25f, 0.4f, 0), Quaternion.identity);
                    bullet5.GetComponent<EnergyBall>().direction = new Vector2(1, 0);

                    canFire = Time.time + fireRate;
                    actualAudio.Play();

                    break;
                case "Projectile":
                    Instantiate(BulletPref, transform.localPosition + new Vector3(0, 0.4f, 0), Quaternion.identity);

                    canFire = Time.time + fireRate;
                    actualAudio.Play();

                    break;
            }

        }
    }


    //public GameObject BulletPref; ----> esta es la bala que se va a disparar

    public List<Bullet> bullets;

    public void ChangeWeapon()
    {
        //For changing weapons, use the number keys 1, 2, 3

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BulletPref = bullets[0].gameObject;
            actualWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BulletPref = bullets[1].gameObject;
            actualWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BulletPref = bullets[2].gameObject;
            actualWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            BulletPref = bullets[3].gameObject;
            actualWeapon = 3;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                //Destroy the enemy
                Destroy(collision.gameObject);
                ChangeShipState();
                
                if (lives > 1)
                {
                    lives--;
                    Debug.Log("Lives: " + lives);
                    lifeManager.LoseLife();
                }
                else
                {
                    lives--;
                    lifeManager.LoseLife();
                    //Destroy the player
                    Destroy(this.gameObject);
                   
                }
            }
        }
    }

    public void ActivateDoubleShot(float doubleShotDuration)
    {
        doubleShotActive = true;
        StartCoroutine(DoubleShotPowerDownRoutine()); // Rutina para desactivar el power-up después de un tiempo
    }

    // Rutina para desactivar el doble disparo después de un tiempo
    private IEnumerator DoubleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(doubleShotDuration);
        doubleShotActive = false;
    }

    public void AddLife()
    {
        if (lives < maxLives)
        {
            lives++; // Incrementa las vidas

            // Crea una nueva imagen de vida y agrégala al panel
            GameObject newLifeImage = Instantiate(lifeImagePrefab, lifePanel.transform);
        }
    }
}
