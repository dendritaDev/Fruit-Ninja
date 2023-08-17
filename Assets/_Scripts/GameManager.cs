using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        loading,
        inGame,
        gameOver,
    }

    public GameState gameState;

    public List<GameObject> targetPrefabs;
    
    private float spawnRate = 1.0f;

    public TextMeshProUGUI scoreText;
    public Button restartButton;

    private int _score;
    private int Score //esto es el setter/getter de una variable, una variable maneja la variable y la otra variable cambia
    {                 //Entonces en codigo utilizas la que es la manejadora, en este caso Score y esta es la que modifica
                      //el valor real del score. Además permite chequear cosas con el set cuando va a modificarse. P.e aqui miramos que
                      //cuando cambiamos la variable si fuese a tener un resultado menor a 0, le asignamos 0
                      //y con el get lo que hacemos es que cuando se use Score el valor que se retorne de esta variable sea la de _score
        set
        {
            //_score = Mathf.Max(value, 0); //esto es como un if <0, dale 0. Compara el valor actual y con 0, y se queda con el mayor


            //Tamnbien podriamos utilizar:
            _score = Mathf.Clamp(value, 0, 99999); //esto lo que hace es que si el valor cae fuera de 0, se le de 0 a la variable, y que si el valor
                                                    //es mayor a 99999 se le de el valor de 99999. 
            
            //Es decir,^esto, sería lo mismo que:
                //if(_score < 0)
                //{
                //    _score = 0;
                //}
                //else if(_score >99999)
                //{
                //    _score = 99999;
                //}
        }

        get
        {
            return _score;
        }
    }

    public TextMeshProUGUI gameOverText;

    public GameObject titleScreen;
    const string MAX_SCORE = "MAX_SCORE";

    private int numberOfLives = 4;
    public List<GameObject> lives;
    IEnumerator SpawnTarget()
    {
        while(gameState == GameState.inGame)
        {
            yield return new WaitForSeconds(spawnRate);

            int index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index]);
        }
    }

    private void Start()
    {
        ShowMaxScore();
    }

    /// <summary>
    /// Método que inicia la partida cambiando el vlaor del estado del juego
    /// </summary>
    /// <param name="difficulty"> Numero entero que modifica la dificultad del juego</param>
    public void StartGame(int difficulty)
    {
        gameState = GameState.inGame;
        titleScreen.gameObject.SetActive(false);

        spawnRate /= difficulty;
        numberOfLives -= difficulty;

        for(int i = 0; i < numberOfLives; i++)
        {
            lives[i].SetActive(true);
        }

        StartCoroutine(SpawnTarget());

        Score = 0;
        UpdateScore(0);
    }

    public void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);
        scoreText.text = "Max Score: \n" + maxScore;
    }

    /// <summary>
    /// Actualizar la puntuación del jugador y mostrarlo por la pantalla
    /// </summary>
    /// <param name="scoreToAdd"> Número de puntos a añadir a la puntuación global</param>
    public void UpdateScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        scoreText.text = "Score : \n " + Score;
    }

    public void GameOver()
    {
        numberOfLives--;

        if(numberOfLives >= 0)
        {
            Image heartImage = lives[numberOfLives].GetComponent<Image>();
            Color tempColor = heartImage.color;
            tempColor.a = 0.3f;

            heartImage.color = tempColor;
        }
      

        if(numberOfLives <= 0)
        {
            SetMaxScore();

            gameState = GameState.gameOver;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }

    }

    private void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt(MAX_SCORE, 0);

        if (Score > maxScore)
        {
            PlayerPrefs.SetInt(MAX_SCORE, Score);

            //si hay nueva puntuacion maxima, podriamos hacer un efecto de tirar cohetes o algo asi, 
            //para darle feedback al player o algo
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameState = GameState.inGame;
    }
}
