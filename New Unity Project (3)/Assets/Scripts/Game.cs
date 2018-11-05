using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public Image playerOne;
    public Image playerTwo;
    public Sprite[] playerSprites;
    public Text manageText;
    public Text buttonText;
    public Text enemyText;
    public Image[] stars;
    public Text restartText;
    public Button selectEn;

    private int gameStage = 0;
    private float timer = 0;
    private float bangTimer = 0;
    private float botTimer = 0;
    private bool isPlayerOneShot = false;
    private bool isPlayerTwoShot = false;
    private int countStar1 = 0;
    private int countStar2 = 0;

    public void StarGame()
    {
        selectEn.interactable = false;
        manageText.text = "ready";
        playerOne.sprite = playerSprites[1];
        playerTwo.sprite = playerSprites[1];
        gameStage = 1;
        if(restartText.text == "RESTART")
        {
            Restart();
        }
    }

    // Use this for initialization
    void Start () {
        
	}

    // Update is called once per frame
    public void Update()
    {
        if (gameStage == 1)
        {
            if (timer >= 1.0f)
            {
                manageText.text = "steady";
                bangTimer = Random.Range(1.0f, 3.0f);
                gameStage = 2;
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else if (gameStage == 2)
        {
            if (timer >= bangTimer)
            {
                manageText.text = "!bang!";
                bangTimer = 0;
                gameStage = 3;
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        if(gameStage == 3 && enemyText.text == "Computer")
        {
            botTimer = Random.Range(0, 3.0f);
            timer += Time.deltaTime;
            if(timer >= botTimer)
            {
                Shot(2);
            }
        }
    }

    public void Shot(int player)
    {
        if (gameStage == 0) return;
        Image activePlayer = (player == 1) ? playerOne : playerTwo;
        if(gameStage == 3)
        {
            
            Image enemyPlayer = (player == 2) ? playerOne : playerTwo;
            activePlayer.sprite = playerSprites[2];
            enemyPlayer.sprite = playerSprites[3];
            manageText.text = "player " + player + " win round!";
            gameStage = 0;
            isPlayerOneShot = false;
            isPlayerTwoShot = false;
                

            if (manageText.text == "player 1 win round!")
            {
                stars[0].gameObject.SetActive(true);
                countStar1++;
                if (countStar1 > 1)
                {
                    stars[1].gameObject.SetActive(true);
                }
            }
            else
            {
                stars[2].gameObject.SetActive(true);
                countStar2++;
                if (countStar2 > 1)
                {
                    stars[3].gameObject.SetActive(true);
                }
            }


            if (stars[0].gameObject.activeSelf && stars[1].gameObject.activeSelf)
            {
                manageText.text = "Game over! Player " + player + " win!";
                restartText.text = "RESTART";
                countStar1 = 0;
                countStar2 = 0;
                selectEn.interactable = true;
            }
            else if (stars[2].gameObject.activeSelf && stars[3].gameObject.activeSelf)
            {
                manageText.text = "Game over! Player " + player + " win!";
                restartText.text = "RESTART";
                countStar1 = 0;
                countStar2 = 0;
                selectEn.interactable = true;
            }
        }
        else
        {
            if (player == 1) isPlayerOneShot = true;
            if (player == 2) isPlayerTwoShot = true;
            if(isPlayerOneShot && isPlayerTwoShot)
            {
                manageText.text = "Two fast, guys";
                bangTimer = 0;
                gameStage = 0;
                timer = 0;
                isPlayerOneShot = false;
                isPlayerTwoShot = false;
            }
            activePlayer.sprite = playerSprites[4];
        }
    }

    public void Restart()
    {
        stars[0].gameObject.SetActive(false);
        stars[1].gameObject.SetActive(false);
        stars[2].gameObject.SetActive(false);
        stars[3].gameObject.SetActive(false);
        restartText.text = "START";
    }


    public void SelectEnemy()
    {
        if(buttonText.text == "Computer")
        {
            buttonText.text = "Player Two";
            enemyText.text = "Computer";
        }
        else
        {
            buttonText.text = "Computer";
            enemyText.text = "Player Two";
        }
    }
}
