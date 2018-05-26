using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {

	public Color hoverColor, startColor, activeColor, selectedColor;

	public bool isActive, hasPiece, isSelected;


	public Piece_Control activePiece;


	GameManager gM;
	SpriteRenderer sP;
	Collider2D coll;

	// Use this for initialization
	void Start () {
		sP = GetComponent<SpriteRenderer>();
		coll = GetComponent<Collider2D>();
		gM = FindObjectOfType<GameManager>();

		sP.material.color = startColor;


	}


	private void OnMouseOver()
	{
		ChoosePiece();
		MovePiece();
	}


	private void OnMouseExit()
	{
		MouseExitColor();
	}


	public void ResetSquareColor(){
		sP.material.color = startColor;

	}

	void MovePiece(){
		if (gM.isMovingPiece)
		{
			if (isActive)
			{
				
				sP.material.color = hoverColor;

				if (Input.GetMouseButtonDown(0))
				{
					activePiece.transform.position = this.transform.position;
					activePiece.ClearAllSquares();

					Piece_Control[] piece = FindObjectsOfType<Piece_Control>();
					foreach(Piece_Control optionalPiece in piece){
						optionalPiece.FindPieceSquares();
					}
					gM.isMovingPiece = false;
					gM.isChoosingPiece = true;

				}
			}


		}
	}

	void ChoosePiece(){
		if (gM.isChoosingPiece)
		{
			if (hasPiece)
			{
				sP.material.color = hoverColor;

				if (Input.GetMouseButtonDown(0))
				{
					gM.isChoosingPiece = false;
					gM.isMovingPiece = true;
					coll.enabled = false;
					sP.material.color = selectedColor;
					isSelected = true;
					Piece_Control[] piece = FindObjectsOfType<Piece_Control>();
					foreach (Piece_Control optionalPiece in piece)
					{
						if (optionalPiece.transform.position == transform.position)
						{
							optionalPiece.SetAsActivePiece();
						}
					}

				}
			}
		}

	}



	void MouseExitColor(){
		if (gM.isChoosingPiece)
        {
            if (isActive)
            {
                sP.material.color = activeColor;
            }
            if (hasPiece)
            {

                sP.material.color = startColor;
            }
            if (isSelected)
            {
                sP.material.color = selectedColor;
            }
        }

        if (gM.isMovingPiece)
        {
            if (isActive)
            {
                sP.material.color = activeColor;
            }
        }
	}
}
