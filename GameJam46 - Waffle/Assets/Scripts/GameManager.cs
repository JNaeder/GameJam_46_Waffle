using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	public bool isChoosingPiece, isMovingPiece;
    public static int turnNum;
	public Text turnNumText;

    EnemyAI eAI;



	// Use this for initialization
	void Start () {
        eAI = FindObjectOfType<EnemyAI>();

        turnNum++;
        PlayerTurn();
	}

	private void Update()
	{
		turnNumText.text = "Turn: " + turnNum.ToString();
	}


	public void PlayerTurn() {
        //print("It is Player Turn number " + turnNum);
        ClearEverythingAndLookForEverything();
		Invoke("ResetIsChoosing", 0.5f);

        

        

    }

    public void EnemyTurn() {
        //print("It is enemy turn number " + turnNum);
        ClearEverythingAndLookForEverything();
        eAI.EnemyTurn();
        
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
}
