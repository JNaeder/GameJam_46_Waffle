using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_Control : MonoBehaviour {

	Square[] squares;
	Square selectedSqaure;

	public Color optionColor, enemyColor;
	public bool isActivePiece;
	public static Piece_Control activePiece;
    public int maxDist;
    

	// Use this for initialization
	void Start()
	{

	}
    


	public void FindPossibleMoves2(){

		if(isActivePiece){
			squares = FindObjectsOfType<Square>();
			int yUpNum = 2;
            int yDownNum = 2;
            int xUpNum = 2;
            int xDownNum = 2;

			for (int i = 0; i < squares.Length; i++){
				if(squares[i].transform.position.y == transform.position.y + yUpNum && squares[i].transform.position.x == transform.position.x){
                    if (yUpNum >= (maxDist + 1) * 2) {
                        break;
                    }
                    if (squares[i].hasPiece == true)
                    {
                        break;
                    }
                    if (squares[i].hasEnemy == true)
                    {
                        SpriteRenderer squareSP = squares[i].GetComponent<SpriteRenderer>();
                        squareSP.material.color = enemyColor;
                        squares[i].isActive = true;
                        squares[i].activePiece = this;
                        break;
                    }
                    else if(squares[i].hasPiece == false)
                    {
                        squares[i].isActive = true;
                        SpriteRenderer squareSP = squares[i].GetComponent<SpriteRenderer>();
                        squareSP.material.color = optionColor;
                        squares[i].activePiece = this;
                        yUpNum += 2;
                        i = 0;
                    }
				}

			}
			for (int i = 0; i < squares.Length; i++)
            {
                if (squares[i].transform.position.y == transform.position.y - yDownNum && squares[i].transform.position.x == transform.position.x)
                {
                    if (yDownNum >= (maxDist + 1) * 2)
                    {
                        break;
                    }
                    if (squares[i].hasPiece == true)
                    {
                        break;
                    }
                    if (squares[i].hasEnemy == true)
                    {
                        SpriteRenderer squareSP = squares[i].GetComponent<SpriteRenderer>();
                        squareSP.material.color = enemyColor;
                        squares[i].isActive = true;
                        squares[i].activePiece = this;

                        break;
                    }
                    else if (squares[i].hasPiece == false)
                    {
                        squares[i].isActive = true;
                        SpriteRenderer squareSP = squares[i].GetComponent<SpriteRenderer>();
                        squareSP.material.color = optionColor;
                        squares[i].activePiece = this;
                        yDownNum += 2;
                        i = 0;
                    }
                }

            }
			for (int i = 0; i < squares.Length; i++)
            {
                if (squares[i].transform.position.x == transform.position.x + xUpNum && squares[i].transform.position.y == transform.position.y)
                {
                    if (xUpNum >= (maxDist + 1) * 2)
                    {
                        break;
                    }
                    if (squares[i].hasPiece == true)
                    {
                        break;
                    }
                    if (squares[i].hasEnemy == true)
                    {
                        SpriteRenderer squareSP = squares[i].GetComponent<SpriteRenderer>();
                        squareSP.material.color = enemyColor;
                        squares[i].isActive = true;
                        squares[i].activePiece = this;
                        break;
                    }
                    else if (squares[i].hasPiece == false)
                    {
                        squares[i].isActive = true;
                        SpriteRenderer squareSP = squares[i].GetComponent<SpriteRenderer>();
                        squareSP.material.color = optionColor;
                        squares[i].activePiece = this;
                        xUpNum += 2;
                        i = 0;
                    }
                }

            }
			for (int i = 0; i < squares.Length; i++)
            {
                if (squares[i].transform.position.x == transform.position.x - xDownNum && squares[i].transform.position.y == transform.position.y)
                {
                    if (xDownNum >= (maxDist + 1) * 2)
                    {
                        break;
                    }
                    if (squares[i].hasPiece == true)
                    {
                        break;
                    }
                    if (squares[i].hasEnemy == true)
                    {
                        SpriteRenderer squareSP = squares[i].GetComponent<SpriteRenderer>();
                        squareSP.material.color = enemyColor;
                        squares[i].isActive = true;
                        squares[i].activePiece = this;
                        break;
                    }
                    else if (squares[i].hasPiece == false)
                    {
                        squares[i].isActive = true;
                        SpriteRenderer squareSP = squares[i].GetComponent<SpriteRenderer>();
                        squareSP.material.color = optionColor;
                        squares[i].activePiece = this;
                        xDownNum += 2;
                        i = 0;
                    }
                }

            }




			}





		}




	public void FindPieceSquares(){
		    
			squares = FindObjectsOfType<Square>();
			foreach (Square optionSqaure in squares)
			{
				if (optionSqaure.transform.position == transform.position)
				{
					optionSqaure.hasPiece = true;
				}
			}

            


	}
    
	public void ClearAllSquares(){
		foreach(Square optionSqaure in squares){
			optionSqaure.isActive = false;
			//optionSqaure.hasPiece = false;
			optionSqaure.isSelected = false;
			optionSqaure.activePiece = null;
           // optionSqaure.playerPiece = null;
			Collider2D coll = optionSqaure.GetComponent<Collider2D>();
			coll.enabled = true;
			optionSqaure.ResetSquareColor();
		}

	}

	public void SetAsActivePiece(){
		isActivePiece = true;
		activePiece = this;
		FindPossibleMoves2();
	}
    
}
