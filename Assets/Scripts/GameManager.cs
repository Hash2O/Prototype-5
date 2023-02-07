using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Variable de type List pour gérer les différents GO du jeu
    public List<GameObject> targets;

    public TextMeshProUGUI scoreText;

    private int score;

    public TextMeshProUGUI gameOverText;

    //Variable pour gérer la fréquence de spawn des GO de la liste
    private float spawnRate = 1.0f;

    public bool isGameActive;

    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        score = 0;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate); //Attendre, ici une seconde
            int index = Random.Range(0, targets.Count); //Désigner aléatoirement l'index d'un des GO de la liste (ici targets.Count = targets.Length si on utilisait un Array)
            Instantiate(targets[index]);                //Instancier l'objet tiré au sort
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score : " + score;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
