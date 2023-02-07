using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -1;

    public int pointValue;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        //Initialisation
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        //On ajoute une force dirigée vers le haut, de puissance aléatoire
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);

        //Gérer la rotation aléatoire de l'objet une fois lancé en l'air
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    
        //Gènère une position aléatoire de pop pour l'objet (sur x et y, 
        //z n'est pas utilisé ici, en 2D)
        transform.position = new Vector3(Random.Range(-4, 4), -6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Supprimer l'objet avec un clic de la souris
    private void OnMouseDown() {
        if (gameManager.isGameActive)
        {
            if (gameObject.CompareTag("Good"))
            {
                Destroy(gameObject);
                gameManager.UpdateScore(pointValue);
                Instantiate(explosionParticle, transform.position, transform.rotation);
            }
            else
            {
                Debug.Log("BOOOM !");
                Destroy(gameObject);
                gameManager.UpdateScore(pointValue);
                Instantiate(explosionParticle, transform.position, transform.rotation);
            }
        }        
    }

    //Tous les objets qui entrent en contact avec un collider sont supprimés
    //ici, il n'y a que l'objet Sensor qui a un Collider
    //Cela supprime les clones non éliminés par le joueur avec sa souris
    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
        if(!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
