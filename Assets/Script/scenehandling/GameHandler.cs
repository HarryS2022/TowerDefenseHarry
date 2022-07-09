using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public Archer ArcherPrefab;
    public Lancer LancerPrefab;
    public Knight KnightPrefab;
    private bool isPlacingCharacter;
    public LayerMask ValidCharacterPlacementLayer;
    private GameObject characterToPlace;
    public int gold = 100;
    public int ArcherPrice = 40;
    public int KnightPrice = 30;
    public int LancerPrice = 25;
    private int cost;
    public int lives = 5;
    public Text GoldText;
    public Text LivesText;

    

    public bool gameOver = false;
    public EnemySpawnHandler spawner;

    private void Start()
    {
        Time.timeScale = 1;
        GoldText.text = gold.ToString();
        LivesText.text = lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {

            if(spawner.remainingEnemies <= 0 || lives <= 0)
            {
                gameOver = true;
                GameOver();
            }

            if (isPlacingCharacter && Input.GetMouseButtonDown(0))
            {
                Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //where is the mouse
                RaycastHit2D hit = Physics2D.Raycast(origin, Vector3.forward, float.PositiveInfinity, ValidCharacterPlacementLayer);
                //detect if you hit something 
                if (hit.collider)
                {
                    Vector2 characterPos = new Vector2(origin.x, findY(hit.collider, origin));
                    //origin is mouse
                    GameObject character = Instantiate(characterToPlace);
                    gold -= cost;
                    GoldText.text = gold.ToString();
                    character.transform.position = characterPos;
                    character.GetComponent<PlayerCharacter>().validBounds = hit.collider;
                }
                isPlacingCharacter = false;
            }

            
        }
    }

    public Transform gameoverMessagePanel;
    public UnityEngine.UI.Text gameOverMessageText;
    void GameOver()
    {
        Time.timeScale = 0;
        //display gameover message
        if(spawner.remainingEnemies <= 0)
        {
            gameOverMessageText.text = "You are a winner";
        }else if (lives <= 0)
        {
            gameOverMessageText.text = "You are a weiner?";
        }
        gameoverMessagePanel.gameObject.SetActive(true);
    }
    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    float findY(Collider2D collider, Vector3 mousePos)
    {
        //find the bottom of collider and return its y value
        GameObject Collider = collider.gameObject;
        return Collider.transform.position.y - (Collider.transform.localScale.y) / 2;
    }

    public void PlaceArcher()
    {
        if (gold >= ArcherPrice)
        {
            isPlacingCharacter = true;
            characterToPlace = ArcherPrefab.gameObject;
            cost = ArcherPrice;
        }
        
    }
    public void PlaceKnight()
    {
        if (gold >= KnightPrice)
        {
            isPlacingCharacter = true;
            characterToPlace = KnightPrefab.gameObject;
            cost = KnightPrice;
        }

    }
    public void PlaceLancer()
    {
        if (gold >= LancerPrice)
        {
            isPlacingCharacter = true;
            characterToPlace = LancerPrefab.gameObject;
            cost = LancerPrice;
        }

    }
}