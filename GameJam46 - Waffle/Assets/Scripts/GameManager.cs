using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


	public bool isChoosingPiece, isMovingPiece;
    public static int turnNum;

    EnemyAI eAI;

	// Use this for initialization
	void Start () {
        eAI = FindObjectOfType<EnemyAI>();

        turnNum++;
        PlayerTurn();
	}


    public void PlayerTurn() {
        //print("It is Player Turn number " + turnNum);
        isChoosingPiece = true;
        isMovingPiece = false;
        ClearEverythingAndLookForEverything();


       // Square[] squares = FindObjectsOfType<Square>();
       // foreach (Square s in squares) {
            //s.hasPiece = false;
       // }


       // Piece_Control[] pieces = FindObjectsOfType<Piece_Control>();
        //for (int i = 0; i < pieces.Length; i++) {
            //pieces[i].FindPieceSquares();
        //}
        

        

    }

    public void EnemyTurn() {
        //print("It is enemy turn number " + turnNum);
        ClearEverythingAndLookForEverything();
        eAI.EnemyTurn();
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
