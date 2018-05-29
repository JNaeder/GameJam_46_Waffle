using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {

	public Color hoverColor, startColor, activeColor, selectedColor, enemyColor;

    public bool isActive, hasPiece, hasEnemy, isSelected;


	public Piece_Control activePiece;
    public Enemy enemyPiece;
    public Piece_Control playerPiece;


	GameManager gM;
	SpriteRenderer sP;
    SFXPlayer sfx;

	// Use this for initialization
	void Start () {
		sP = GetComponent<SpriteRenderer>();
		gM = FindObjectOfType<GameManager>();
        sfx = FindObjectOfType<SFXPlayer>();

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
        sfx.UnDoHasPlayed();
	}


	public void ResetSquareColor(){
		sP.material.color = startColor;

	}

	void MovePiece(){
		if (gM.isMovingPiece)
		{

			if (isSelected)
            {
                if (Input.GetMouseButtonDown(0))
                {
					Invoke("RestartPlayerTurn", 0.2f);
                }


            }
			if (isActive)
			{
				
				sP.material.color = hoverColor;
                sfx.PlayHoverSound();

				if (Input.GetMouseButtonDown(0))
				{
                    //print("Player Moves Piece");
                    sfx.PlayMoveSound();
					activePiece.transform.position = this.transform.position;
					activePiece.ClearAllSquares();
                    if (hasEnemy) {
                        hasEnemy = false;
                        enemyPiece.GetComponent<Enemy>().enabled = false;
                        Destroy(enemyPiece.gameObject);
                        print("Destroy Enemy on " + gameObject.name);

                    }

					Piece_Control[] piece = FindObjectsOfType<Piece_Control>();
					foreach(Piece_Control optionalPiece in piece){
                       
                        optionalPiece.isActivePiece = false;
					}
                    Invoke("StartEnemyTurn", 2.0f);
                }
			}




		}
	}

    void StartEnemyTurn() {
        gM.EnemyTurn();

    }

	void RestartPlayerTurn(){
		sfx.PlayClickSound();
		Piece_Control[] player = FindObjectsOfType<Piece_Control>();
		foreach (Piece_Control p in player){
			p.ClearAllSquares();
		}
		gM.PlayerTurn();

	}

	public void ChoosePiece(){
		if (gM.isChoosingPiece)
		{
			if (hasPiece)
			{
				sP.material.color = hoverColor;
                sfx.PlayHoverSound();

				if (Input.GetMouseButtonDown(0))
				{
					Invoke("ChangeModes", 0.1f);
					sP.material.color = hoverColor;
					isSelected = true;
                    sfx.PlayClickSound();
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

	void ChangeModes(){

		gM.isChoosingPiece = false;
        gM.isMovingPiece = true;
	}



	void MouseExitColor(){
		if (gM.isChoosingPiece)
        {
            if (isActive && !hasEnemy)
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
            if (hasEnemy && isActive)
            {
                sP.material.color = enemyColor;
            }
        }
	}
}
