using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float minForce = 16.0f, maxForce = 21.0f;
    private float minTorqueRot = -10.0f, maxTorqueRot = 10.0f;

    private float xRange = 4, ySpawnPos = -6;

    private GameManager gameManager; //como no es una variable de esta clase, no hace falta poner la barra baja

    public int pointValue;

    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        _rigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos(); //z = 0
        
        //Para acceder a un objeto/script en el juego, se puede hacer, además de con el tag, así:
        //gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManager = FindObjectOfType<GameManager>(); //utiliza recursos, asi que no abusar de esto

    }

    /// <summary>
    /// Genera un vector aleastorio en 3D
    /// </summary>
    /// <returns>Fuerza aleatoria para arriba</returns>
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    /// <summary>
    /// Genera un número aleatorio
    /// </summary>
    /// <returns>Valor aleastorio entre maxtorque y mintorque</returns>
    private float RandomTorque()
    {
        return Random.Range(minTorqueRot, maxTorqueRot);
    }

    /// <summary>
    /// Genera una posición aleatoria
    /// </summary>
    /// <returns>Posicion aleatoria en 3D con la coordenada z = 0</returns>
    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    
    private void OnMouseOver()
    {
        if(gameManager.gameState == GameManager.GameState.inGame)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
        

        //if (gameObject.CompareTag("Bad"))
        //{

        //    gameManager.GameOver();

        //}

    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("KillZone"))
        {
            if(gameObject.CompareTag("Good"))
            {
                Destroy(gameObject);
                if (pointValue > 0)
                {
                    gameManager.GameOver();
                }
            }

        
            

        }
    }


}
