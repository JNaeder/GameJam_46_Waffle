using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    Square[] squares;
    GameManager gM;
    EnemyAI eAI;
    Square  thisSquare;
    SFXPlayer sfx;
	Vector3 transPos;

    public float maxDist;
	public bool isStraight, isDiagonal;
	public int weight;

    public List<Vector3> possibleMovements;
    public List<Vector3> possibleMovemenstWithEnemy;
	public List<Vector3> possibleMovementsWithBishop;
	public List<Vector3> possibleMovementsWithRook;
	public List<Vector3> possibleMovementsWithKing;
	public Square sqaureWithPlayer, squareWithBishop, squareWithRook, squareWithKing;

	// Use this for initialization
	void Start () {
        gM = FindObjectOfType<GameManager>();
        eAI = FindObjectOfType<EnemyAI>();
        possibleMovements = new List<Vector3>();
        possibleMovemenstWithEnemy = new List<Vector3>();
        sfx = FindObjectOfType<SFXPlayer>();


        FindPieceSquares();
	}
	

    public void FindPieceSquares()
    {

        squares = FindObjectsOfType<Square>();
        foreach (Square s in squares)
        {
            if (s.transform.position == transform.position)
            {
                s.enemyPiece = this;
                s.hasEnemy = true;
            }
        }

        Piece_Control[] playerPieces = FindObjectsOfType<Piece_Control>();
        foreach (Piece_Control p in playerPieces) {
            for (int i = 0; i < squares.Length; i++) {
                if (squares[i].transform.position == p.transform.position) {
                    squares[i].playerPiece = p;
                }

            }
        }
    }

    public void ClearAllEnemyPieces() {
        //print("Cleared Enemies Spaces");
        squares = FindObjectsOfType<Square>();
        foreach (Square s in squares) {
            s.enemyPiece = null;
            s.hasEnemy = false;
            s.playerPiece = null;
        }

    }

    


    public void MoveEnemy()
    {
        //print("Enemy Moves");
        sfx.PlayEnemeyMoveSound();

        //Find The Square The Piece Started On
        Square[] squares = FindObjectsOfType<Square>();
        for (int i = 0; i < squares.Length; i++)
        {
            if (squares[i].transform.position == transform.position)
            {
                thisSquare = squares[i];
            }
        }
        

        transPos = transform.position;
		if(possibleMovementsWithKing.Count != 0){
			AttackKing();
		} 
		    else if(possibleMovementsWithRook.Count != 0){
			AttackRook();
		}
		    else if(possibleMovementsWithBishop.Count!= 0){

			AttackBishop();
		}
        
            else if (possibleMovemenstWithEnemy.Count != 0) {
			AttackPawn();
        }


        //Can't Find Any Posisble Moves Look again
        else if (possibleMovements.Count == 0)
        {
            //print("Find Enemy Again");
			possibleMovemenstWithEnemy.Clear();
            possibleMovements.Clear();
            eAI.EnemyTurn();
        }

        //Move Piece Without Attacking
        else {
            //print("Possible Movements " + possibleMovements.Count + " for " + gameObject.name);
            int randSpace = Random.Range(0, possibleMovements.Count);
            transPos = possibleMovements[randSpace];
            transform.position = transPos;
            print(gameObject.name + " Moves from " + thisSquare + " to " + possibleMovements[randSpace] + " from a possible " + possibleMovements.Count + " Moves");


			GameManager.turnNum++;
			Invoke("StartPlayerTurn", 0.5f);
            possibleMovemenstWithEnemy.Clear();
            possibleMovements.Clear();
        }

       

    }

    

    void StartPlayerTurn() {
        sqaureWithPlayer = null;
        gM.PlayerTurn();

    }


    public void FindPossibleSpaces() {

		if(isStraight){
			Straight();

		}
		if(isDiagonal){
			Diagonal();
		}
          


        }

	void Straight(){
		squares = FindObjectsOfType<Square>();
        int yUpNum = 2;
        int yDownNum = 2;
        int xUpNum = 2;
        int xDownNum = 2;

        for (int i = 0; i < squares.Length; i++)
        {
            // Looking for Pieces Up
            if (squares[i].transform.position.y == transform.position.y + yUpNum && squares[i].transform.position.x == transform.position.x)
            {
                // print(squares[i].gameObject.name  + " " + squares[i].hasEnemy);
                if (yUpNum >= (maxDist + 1) * 2)
                {
                    break;
                }
                if (squares[i].hasPiece == true)
                {
					//Add to list of possible movements, and has enemy, make priority for movement
					if (squares[i].playerPiece.gameObject.tag == "King")
                    {
                        possibleMovementsWithKing.Add(squares[i].transform.position);
                        squareWithKing = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Rook")
                    {
                        possibleMovementsWithRook.Add(squares[i].transform.position);
						squareWithRook = squares[i];
                        break;
                    }


                    else if (squares[i].playerPiece.gameObject.tag == "Bishop")
                    {
                        possibleMovementsWithBishop.Add(squares[i].transform.position);
                        squareWithBishop = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Pawn")
                    {
                        possibleMovemenstWithEnemy.Add(squares[i].transform.position);
                        sqaureWithPlayer = squares[i];
                        break;
                    }


                }
                if (squares[i].hasEnemy == true)
                {
                    break;
                }
                else
                {
                    //Add to list of possible, low priority
                    //print("Find Space");
                    possibleMovements.Add(squares[i].transform.position);
                    yUpNum += 2;
                    i = 0;
                }
            }

        }
        for (int i = 0; i < squares.Length; i++)
        {
            // Looking for Pieces Down
            if (squares[i].transform.position.y == transform.position.y - yDownNum && squares[i].transform.position.x == transform.position.x)
            {
                // print(squares[i].gameObject.name + " " + squares[i].hasEnemy);
                if (yDownNum >= (maxDist + 1) * 2)
                {
                    break;
                }
                if (squares[i].hasPiece == true)
                {
                    //Add to list of possible movements, and has enemy, make priority for movement
					if (squares[i].playerPiece.gameObject.tag == "King")
                    {
                        possibleMovementsWithKing.Add(squares[i].transform.position);
                        squareWithKing = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Rook")
                    {
                        possibleMovementsWithRook.Add(squares[i].transform.position);
                        squareWithRook = squares[i];
                        break;
                    }


                    else if (squares[i].playerPiece.gameObject.tag == "Bishop")
                    {
                        possibleMovementsWithBishop.Add(squares[i].transform.position);
                        squareWithBishop = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Pawn")
                    {
                        possibleMovemenstWithEnemy.Add(squares[i].transform.position);
                        sqaureWithPlayer = squares[i];
                        break;
                    }
                }
                if (squares[i].hasEnemy == true)
                {

                    break;
                }
                else
                {
                    //Add to list of possible, low priority
                    //print("Find Space");
                    possibleMovements.Add(squares[i].transform.position);
                    yDownNum += 2;
                    i = 0;
                }
            }

        }
        for (int i = 0; i < squares.Length; i++)
        {
            // Looking for Pieces Right
            if (squares[i].transform.position.x == transform.position.x + xUpNum && squares[i].transform.position.y == transform.position.y)
            {
                if (xUpNum >= (maxDist + 1) * 2)
                {
                    break;
                }
                //print(squares[i].gameObject.name + " " + squares[i].hasEnemy);
                if (squares[i].hasPiece == true)
                {
					if (squares[i].playerPiece.gameObject.tag == "King")
                    {
                        possibleMovementsWithKing.Add(squares[i].transform.position);
                        squareWithKing = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Rook")
                    {
                        possibleMovementsWithRook.Add(squares[i].transform.position);
                        squareWithRook = squares[i];
                        break;
                    }


                    else if (squares[i].playerPiece.gameObject.tag == "Bishop")
                    {
                        possibleMovementsWithBishop.Add(squares[i].transform.position);
                        squareWithBishop = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Pawn")
                    {
                        possibleMovemenstWithEnemy.Add(squares[i].transform.position);
                        sqaureWithPlayer = squares[i];
                        break;
                    }

                }

                if (squares[i].hasEnemy == true)
                {

                    break;
                }

                else
                {
                    //Add to list of possible, low priority
                    //print("Find Space");
                    possibleMovements.Add(squares[i].transform.position);
                    xUpNum += 2;
                    i = 0;
                }

            }

        }
        for (int i = 0; i < squares.Length; i++)
        {
            // Looking for Pieces Left
            if (squares[i].transform.position.x == transform.position.x - xDownNum && squares[i].transform.position.y == transform.position.y)
            {
                //print(squares[i].gameObject.name + " " + squares[i].hasEnemy);
                if (xDownNum >= (maxDist + 1) * 2)
                {
                    break;
                }
                if (squares[i].hasPiece == true)
                {
                    //Add to list of possible movements, and has enemy, make priority for movement
					if (squares[i].playerPiece.gameObject.tag == "King")
                    {
                        possibleMovementsWithKing.Add(squares[i].transform.position);
                        squareWithKing = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Rook")
                    {
                        possibleMovementsWithRook.Add(squares[i].transform.position);
                        squareWithRook = squares[i];
                        break;
                    }


                    else if (squares[i].playerPiece.gameObject.tag == "Bishop")
                    {
                        possibleMovementsWithBishop.Add(squares[i].transform.position);
                        squareWithBishop = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Pawn")
                    {
                        possibleMovemenstWithEnemy.Add(squares[i].transform.position);
                        sqaureWithPlayer = squares[i];
                        break;
                    }
                }
                if (squares[i].hasEnemy == true)
                {
                    break;
                }
                else
                {
                    //Add to list of possible, low priority
                    //print("Find Space");
                    possibleMovements.Add(squares[i].transform.position);
                    xDownNum += 2;
                    i = 0;
                }
            }

        }
	}

	void Diagonal(){

		squares = FindObjectsOfType<Square>();
        int yUpNum = 2;
        int yDownNum = 2;
        int xUpNum = 2;
        int xDownNum = 2;

        for (int i = 0; i < squares.Length; i++)
        {
            // Looking for Pieces Up
			if (squares[i].transform.position.y == transform.position.y + yUpNum && squares[i].transform.position.x == transform.position.x + yUpNum)
            {
                // print(squares[i].gameObject.name  + " " + squares[i].hasEnemy);
                if (yUpNum >= (maxDist + 1) * 2)
                {
                    break;
                }
                if (squares[i].hasPiece == true)
                {
                    //Add to list of possible movements, and has enemy, make priority for movement

					if (squares[i].playerPiece.gameObject.tag == "King")
                    {
                        possibleMovementsWithKing.Add(squares[i].transform.position);
                        squareWithKing = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Rook")
                    {
                        possibleMovementsWithRook.Add(squares[i].transform.position);
                        squareWithRook = squares[i];
                        break;
                    }


                    else if (squares[i].playerPiece.gameObject.tag == "Bishop")
                    {
                        possibleMovementsWithBishop.Add(squares[i].transform.position);
                        squareWithBishop = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Pawn")
                    {
                        possibleMovemenstWithEnemy.Add(squares[i].transform.position);
                        sqaureWithPlayer = squares[i];
                        break;
                    }
                }
                if (squares[i].hasEnemy == true)
                {
                    break;
                }
                else
                {
                    //Add to list of possible, low priority
                    possibleMovements.Add(squares[i].transform.position);
                    yUpNum += 2;
                    i = 0;
                }
            }

        }
        for (int i = 0; i < squares.Length; i++)
        {
            // Looking for Pieces Down
			if (squares[i].transform.position.y == transform.position.y - yDownNum && squares[i].transform.position.x == transform.position.x - yDownNum)
            {
                // print(squares[i].gameObject.name + " " + squares[i].hasEnemy);
                if (yDownNum >= (maxDist + 1) * 2)
                {
                    break;
                }
                if (squares[i].hasPiece == true)
                {
                    //Add to list of possible movements, and has enemy, make priority for movement
					if (squares[i].playerPiece.gameObject.tag == "King")
                    {
                        possibleMovementsWithKing.Add(squares[i].transform.position);
                        squareWithKing = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Rook")
                    {
                        possibleMovementsWithRook.Add(squares[i].transform.position);
                        squareWithRook = squares[i];
                        break;
                    }


                    else if (squares[i].playerPiece.gameObject.tag == "Bishop")
                    {
                        possibleMovementsWithBishop.Add(squares[i].transform.position);
                        squareWithBishop = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Pawn")
                    {
                        possibleMovemenstWithEnemy.Add(squares[i].transform.position);
                        sqaureWithPlayer = squares[i];
                        break;
                    }
                }
                if (squares[i].hasEnemy == true)
                {

                    break;
                }
                else
                {
                    //Add to list of possible, low priority
                    possibleMovements.Add(squares[i].transform.position);
                    yDownNum += 2;
                    i = 0;
                }
            }

        }
        for (int i = 0; i < squares.Length; i++)
        {
            // Looking for Pieces Right
			if (squares[i].transform.position.x == transform.position.x + xUpNum && squares[i].transform.position.y == transform.position.y - xUpNum)
            {
                if (xUpNum >= (maxDist + 1) * 2)
                {
                    break;
                }
                //print(squares[i].gameObject.name + " " + squares[i].hasEnemy);
                if (squares[i].hasPiece == true)
                {
					if (squares[i].playerPiece.gameObject.tag == "King")
                    {
                        possibleMovementsWithKing.Add(squares[i].transform.position);
                        squareWithKing = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Rook")
                    {
                        possibleMovementsWithRook.Add(squares[i].transform.position);
                        squareWithRook = squares[i];
                        break;
                    }


                    else if (squares[i].playerPiece.gameObject.tag == "Bishop")
                    {
                        possibleMovementsWithBishop.Add(squares[i].transform.position);
                        squareWithBishop = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Pawn")
                    {
                        possibleMovemenstWithEnemy.Add(squares[i].transform.position);
                        sqaureWithPlayer = squares[i];
                        break;
                    }

                }

                if (squares[i].hasEnemy == true)
                {

                    break;
                }

                else
                {
                    //Add to list of possible, low priority
                    possibleMovements.Add(squares[i].transform.position);
                    xUpNum += 2;
                    i = 0;
                }

            }

        }
        for (int i = 0; i < squares.Length; i++)
        {
            // Looking for Pieces Left
			if (squares[i].transform.position.x == transform.position.x - xDownNum && squares[i].transform.position.y == transform.position.y + xDownNum)
            {
                //print(squares[i].gameObject.name + " " + squares[i].hasEnemy);
                if (xDownNum >= (maxDist + 1) * 2)
                {
                    break;
                }
                if (squares[i].hasPiece == true)
                {
                    //Add to list of possible movements, and has enemy, make priority for movement
					if (squares[i].playerPiece.gameObject.tag == "King")
                    {
                        possibleMovementsWithKing.Add(squares[i].transform.position);
                        squareWithKing = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Rook")
                    {
                        possibleMovementsWithRook.Add(squares[i].transform.position);
                        squareWithRook = squares[i];
                        break;
                    }


                    else if (squares[i].playerPiece.gameObject.tag == "Bishop")
                    {
                        possibleMovementsWithBishop.Add(squares[i].transform.position);
                        squareWithBishop = squares[i];
                        break;
                    }
                    else if (squares[i].playerPiece.gameObject.tag == "Pawn")
                    {
                        possibleMovemenstWithEnemy.Add(squares[i].transform.position);
                        sqaureWithPlayer = squares[i];
                        break;
                    }
                }
                if (squares[i].hasEnemy == true)
                {
                    break;
                }
                else
                {
                    //Add to list of possible, low priority
                    possibleMovements.Add(squares[i].transform.position);
                    xDownNum += 2;
                    i = 0;
                }
            }

        }



	}


	void AttackPawn(){
		print(gameObject.name + " Attacks Pawn on " + sqaureWithPlayer + " From " + thisSquare + " with a possible " + possibleMovemenstWithEnemy.Count + " moves with enemy");

        if (sqaureWithPlayer.playerPiece != null)
        {
            Destroy(sqaureWithPlayer.playerPiece.gameObject);
        }
        else
        {
            //eAI.EnemyTurn();
            print("Weird Thing Happened " + sqaureWithPlayer);
        }

		//Move The Piece
		//int randSpace = Random.Range(0, possibleMovemenstWithEnemy.Count);
		transPos = sqaureWithPlayer.transform.position;
        transform.position = transPos;


        GameManager.turnNum++;
        Invoke("StartPlayerTurn", 0.5f);
        possibleMovemenstWithEnemy.Clear();
		possibleMovementsWithBishop.Clear();
		possibleMovementsWithRook.Clear();
		possibleMovementsWithKing.Clear();
        possibleMovements.Clear();

	}

	void AttackBishop(){
		print(gameObject.name + " Attacks Bishop on " + squareWithBishop + " From " + thisSquare + " with a possible " + possibleMovementsWithBishop.Count + " moves with enemy");

		if (squareWithBishop.playerPiece != null)
        {
			Destroy(squareWithBishop.playerPiece.gameObject);
        }
        else
        {
            //eAI.EnemyTurn();
			print("Weird Thing Happened " + squareWithBishop);
        }

		//Move The Piece
		//int randSpace = Random.Range(0, possibleMovementsWithBishop.Count);
		transPos = squareWithBishop.transform.position;
        transform.position = transPos;


        GameManager.turnNum++;
        Invoke("StartPlayerTurn", 0.5f);
		possibleMovemenstWithEnemy.Clear();
        possibleMovementsWithBishop.Clear();
        possibleMovementsWithRook.Clear();
        possibleMovementsWithKing.Clear();
        possibleMovements.Clear();
       
	}

	void AttackRook(){
		print(gameObject.name + " Attacks Rook on " + squareWithRook + " From " + thisSquare + " with a possible " + possibleMovementsWithRook.Count + " moves with enemy");

		if (squareWithRook.playerPiece != null)
        {
            Destroy(squareWithRook.playerPiece.gameObject);
        }
        else
        {
            //eAI.EnemyTurn();
			print("Weird Thing Happened " + squareWithRook);
        }

		//Move The Piece
		//int randSpace = Random.Range(0, possibleMovementsWithRook.Count);
		transPos = squareWithRook.transform.position;
        transform.position = transPos;


        GameManager.turnNum++;
        Invoke("StartPlayerTurn", 0.5f);
        possibleMovemenstWithEnemy.Clear();
        possibleMovementsWithBishop.Clear();
        possibleMovementsWithRook.Clear();
        possibleMovementsWithKing.Clear();
        possibleMovements.Clear();

	}

	void AttackKing(){
		print(gameObject.name + " Attacks King on " + squareWithKing + " From " + thisSquare + " with a possible " + possibleMovementsWithKing.Count + " moves with enemy");

		if (squareWithKing.playerPiece != null)
        {
			Destroy(squareWithKing.playerPiece.gameObject);
        }
        else
        {
            //eAI.EnemyTurn();
            print("Weird Thing Happened " + squareWithKing);
        }

		//Move The Piece
		//int randSpace = Random.Range(0, possibleMovementsWithKing.Count);
		transPos = squareWithKing.transform.position;
        transform.position = transPos;


        GameManager.turnNum++;
        Invoke("StartPlayerTurn", 0.5f);
        possibleMovemenstWithEnemy.Clear();
        possibleMovementsWithBishop.Clear();
        possibleMovementsWithRook.Clear();
        possibleMovementsWithKing.Clear();
        possibleMovements.Clear();

	}


    
}
