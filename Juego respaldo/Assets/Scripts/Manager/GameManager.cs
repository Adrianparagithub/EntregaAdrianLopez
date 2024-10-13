using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime = 1.5f;
    public float time = 0.0f;
    public GameObject enemyPrefab1;
    public float spawnTime1 = 1.5f;
    public float time1 = 0.0f;
    public float totalTime = 0.0f;
    public Player player;

    [Header("Text's")]
    public TextMeshProUGUI liveText;
    public TextMeshProUGUI shieldsText;
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    public int score = 0;

    [Header("UI")]
    public Image bulletImage;
    public List<Sprite> bulletSprites;

    [Header("Power Up")]
    public GameObject powerUpPrefab;
    public float powerUpSpawnTime = 15.0f; // Tiempo en segundos para que aparezca el power-up
    private float powerUpTimer = 0.0f;

    [Header("Extra Life")]
    public GameObject extraLifePrefab; // El prefab de la vida extra
    public float extraLifeSpawnTime = 30.0f; // Tiempo entre apariciones de vidas extras
    private float extraLifeTimer = 0.0f;


    // Update is called once per frame
    void Update()
    {
        CreateEnemy();
        CreatePowerUp();    //TAREA
        CreateExtraLife(); //TAREA
        UpdateCanvas();
        ChangeBulletImage(player.actualWeapon);
        totalTime += Time.deltaTime;

    }
    void UpdateCanvas()
    {
        liveText.text = "vidas ";
        shieldsText.text = "escudo: " + player.shieldsAmount;
        weaponText.text = "arma: " + player.BulletPref.name;
        scoreText.text = "puntaje: " + score.ToString();
        //truncate the time to no show decimals
        timeText.text = "tiempo: " + totalTime.ToString("F0");
    }

    private void CreateEnemy()
    {
        time += Time.deltaTime;
        if (time > spawnTime)
        {
            var cam = Camera.main;
            float xMax = cam.orthographicSize * cam.aspect;
            float yMax = cam.orthographicSize;
            Vector3 pos = new Vector3(Random.Range(-xMax, xMax), yMax, 0);
            Instantiate(enemyPrefab, pos, Quaternion.identity);
            time = 0.0f;
        }

        time1 += Time.deltaTime;
        if (time1 > spawnTime1)
        {
            var cam = Camera.main;
            float xMax = cam.orthographicSize * cam.aspect;
            float yMax = cam.orthographicSize;
            Vector3 pos = new Vector3(Random.Range(-xMax, xMax), yMax, 0);
            Instantiate(enemyPrefab1, pos, Quaternion.identity);
            time1 = 0.0f; // Reinicia el temporizador para el segundo enemigo
        }
    }

    public void AddScore(int value)
    {
        score += value;
        if (score >= 20)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void ChangeBulletImage(int index)
    {
        Debug.Log("ChangeBulletImage: " + index);
        bulletImage.sprite = bulletSprites[index];
    }

    void CreatePowerUp()
    {
        powerUpTimer += Time.deltaTime;
        if (powerUpTimer > powerUpSpawnTime)
        {
            var cam = Camera.main;
            float xMax = cam.orthographicSize * cam.aspect;
            float yMax = cam.orthographicSize;
            Vector3 pos = new Vector3(Random.Range(-xMax, xMax), yMax, 0);
            Instantiate(powerUpPrefab, pos, Quaternion.identity);
            powerUpTimer = 0.0f; // Reinicia el temporizador
        }
    } //TAREA

    void CreateExtraLife()
    {
        extraLifeTimer += Time.deltaTime;
        if (extraLifeTimer > extraLifeSpawnTime)
        {
            var cam = Camera.main;
            float xMax = cam.orthographicSize * cam.aspect;
            float yMax = cam.orthographicSize;
            Vector3 pos = new Vector3(Random.Range(-xMax, xMax), yMax, 0);
            Instantiate(extraLifePrefab, pos, Quaternion.identity); // Instanciar vida extra
            extraLifeTimer = 0.0f;
        }
    } //TAREA
}

