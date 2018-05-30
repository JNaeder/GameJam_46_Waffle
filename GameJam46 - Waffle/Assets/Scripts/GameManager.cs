using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


	public bool isChoosingPiece, isMovingPiece;
    public static int turnNum;

    EnemyAI eAI;
	bool gameOver;
	public static int playerKingNum, enemyKingNum, enemyScore;

	public GameObject winScreen, loseScreen;


	// Use this for initialization
	void Start () {
        eAI = FindObjectOfType<EnemyAI>();

        turnNum++;
        PlayerTurn();
	}


	public void PlayerTurn() {
        //print("It is Player Turn number " + turnNum);
        ClearEverythingAndLookForEverything();
		CheckForWinOrLose();
		if (!gameOver)
		{
			Invoke("ResetIsChoosing", 0.5f);
		}

        

        

    }

    public void EnemyTurn() {
        //print("It is enemy turn number " + turnNum);
        ClearEverythingAndLookForEverything();
		CheckForWinOrLose();
		CheckEnemyScore();
		print("Enemy Power is " + enemyScore);
		if (!gameOver)
		{
			eAI.EnemyTurn();
		}
        
    }

	void ResetIsChoosing(){
		isChoosingPiece = true;
        isMovingPiece = false;

	}


    public void ClearEverythingAndLookForEverything() {
        Square[] squares = FindObjectsOfType<Square>();
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Piece_Control[] players = FindObjectsOfType<Piece_Control>();



        foreach (Square s in squares) {
            s.hasEnemy = false;
            s.hasPiece = false;
            s.enemyPiece = null;
            s.playerPiece = null;

        }



        foreach (Enemy e in enemies) {
            for (int i = 0; i < squares.Length; i++) {
                if (squares[i].transform.position == e.transform.position) {
                    squares[i].hasEnemy = true;
                    squares[i].enemyPiece = e;
                }
            }
        }

        foreach (Piece_Control p in players)
        {
            for (int i = 0; i < squares.Length; i++)
            {
                if (squares[i].transform.position == p.transform.position) {
                    squares[i].hasPiece = true;
                    squares[i].playerPiece = p;

                }

            }
        }




    }


    
	void CheckForWinOrLose(){
		enemyKingNum = 0;
		playerKingNum = 0;

		Enemy[] enemies = FindObjectsOfType<Enemy>();
		Piece_Control[] players = FindObjectsOfType<Piece_Control>();
        
		foreach(Enemy e in enemies){
			if(e.gameObject.tag == "King"){
				enemyKingNum++;
			}
		}

		foreach(Piece_Control p in players){
			if(p.gameObject.tag == "King"){
				playerKingNum++;
			}

		}

		//print("Player King Number is " + playerKingNum + " Enemy King Number Is " + enemyKingNum);
		if(enemyKingNum == 0){
			print("Win!");
			gameOver = true;
			winScreen.SetActive(true);

		} else if(playerKingNum == 0){
			print("Lose");
			gameOver = true;
			loseScreen.SetActive(true);
		}
        



	}

	void CheckEnemyScore(){
		Enemy[] enemies = FindObjectsOfType<Enemy>();
		enemyScore = 0;
		foreach (Enemy e in enemies){
			enemyScore += e.weight;

		}



	}


	public void RestartGame(){
		SceneManager.LoadScene(1);

	}


	public void QuitGame(){
		print("Quit Game");
		Application.Quit();

	}
}
