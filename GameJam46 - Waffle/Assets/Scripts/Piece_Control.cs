using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_Control : MonoBehaviour {

	Square[] squares;
	Square selectedSqaure;

	GameManager gM;

	public Color optionColor;
	public bool isActivePiece;
	public static Piece_Control activePiece;
    

	// Use this for initialization
	void Start()
	{
		gM  = FindObjectOfType<GameManager>();



		FindPieceSquares();
	}


	public void FindPossibleMoves(){
		if (isActivePiece)
		{

			squares = FindObjectsOfType<Square>();

			foreach (Square optionSquare in squares)
			{
				
					if (optionSquare.transform.position.x == transform.position.x && optionSquare.transform.position.y != transform.position.y)
					{
						if (optionSquare.hasPiece == false)
						{
							optionSquare.isActive = true;
							SpriteRenderer squareSP = optionSquare.GetComponent<SpriteRenderer>();
							squareSP.material.color = optionColor;
							optionSquare.activePiece = this;
						}
					}


				    if (optionSquare.transform.position.y == transform.position.y && optionSquare.transform.position.x != transform.position.x)
				{
					if (optionSquare.hasPiece == false)
					{
						optionSquare.isActive = true;
						SpriteRenderer squareSP = optionSquare.GetComponent<SpriteRenderer>();
						squareSP.material.color = optionColor;
						optionSquare.activePiece = this;
					}
				}
				else if (optionSquare.transform.position == transform.position)
				{
					optionSquare.isActive = false;
				}
			}
		}

	}


	public void FindPossibleMoves2(){

		if(isActivePiece){
			squares = FindObjectsOfType<Square>();
			int sNum = 2;

			for (int i = 0; i < squares.Length; i++){
				if(squares[i].transform.position.y == transform.position.y + sNum && squares[i].transform.position.x == transform.position.x){
					if(squares[i].hasPiece == true){
						break;
					}
					squares[i].isActive = true;
					SpriteRenderer squareSP = squares[i].GetComponent<SpriteRenderer>();
                    squareSP.material.color = optionColor;
					squares[i].activePiece = this;
					sNum += 2;
					i = 0;
				}

			}
			for (int i = 0; i < squares.Length; i++)
            {
                if (squares[i].transform.position.y == transform.position.y - sNum && squares[i].transform.position.x == transform.position.x)
                {
                    if (squares[i].hasPiece == true)
                    {
                        break;
                    }
                    squares[i].isActive = true;
                    SpriteRenderer squareSP = squares[i].GetComponent<SpriteRenderer>();
                    squareSP.material.color = optionColor;
                    squares[i].activePiece = this;
                    sNum += 2;
                    i = 0;
                }

            }
			for (int i = 0; i < squares.Length; i++)
            {
                if (squares[i].transform.position.x == transform.position.x + sNum && squares[i].transform.position.y == transform.position.y)
                {
                    if (squares[i].hasPiece == true)
                    {
                        break;
                    }
                    squares[i].isActive = true;
                    SpriteRenderer squareSP = squares[i].GetComponent<SpriteRenderer>();
                    squareSP.material.color = optionColor;
                    squares[i].activePiece = this;
                    sNum += 2;
                    i = 0;
                }

            }
			for (int i = 0; i < squares.Length; i++)
            {
                if (squares[i].transform.position.x == transform.position.x - sNum && squares[i].transform.position.y == transform.position.y)
                {
                    if (squares[i].hasPiece == true)
                    {
                        break;
                    }
                    squares[i].isActive = true;
                    SpriteRenderer squareSP = squares[i].GetComponent<SpriteRenderer>();
                    squareSP.material.color = optionColor;
                    squares[i].activePiece = this;
                    sNum += 2;
                    i = 0;
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
			optionSqaure.hasPiece = false;
			optionSqaure.isSelected = false;
			optionSqaure.activePiece = null;
			Collider2D coll = optionSqaure.GetComponent<Collider2D>();
			coll.enabled = true;
			optionSqaure.ResetSquareColor();
		}

	}

	public void SetAsActivePiece(){
		isActivePiece = true;
		activePiece = this;
		//FindPossibleMoves();
		FindPossibleMoves2();
	}
    
}
