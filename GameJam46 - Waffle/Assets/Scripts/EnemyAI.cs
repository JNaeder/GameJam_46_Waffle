using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

	Enemy[] enemies;
	Enemy enemyToMove, enemyToMoveKing;

	List<Enemy> enemyKings;
	List<Enemy> enemyOther;

	private void Start()
	{
		enemyKings = new List<Enemy>();
		enemyOther = new List<Enemy>();
	}



	public void EnemyTurn()
	{

		enemies = FindObjectsOfType<Enemy>();

		if (enemies.Length > 0)
		{
			foreach (Enemy e in enemies)
			{
				e.FindPossibleSpaces();


				if (e.gameObject.tag != "King")
				{


					if (e.possibleMovementsWithKing.Count > 0)
					{
						enemyToMoveKing = e;
					}
					else if (e.possibleMovementsWithRook.Count > 0)
					{
						enemyToMove = e;
					}
					else if (e.possibleMovementsWithBishop.Count > 0)
					{
						enemyToMove = e;
					}
					else if (e.possibleMovemenstWithEnemy.Count > 0)
					{
						enemyToMove = e;
					}

				}
				else
				{
					if (GameManager.enemyScore < 30)
					{
						e.FindPossibleSpaces();
						if (e.possibleMovementsWithKing.Count > 0)
						{
							enemyToMoveKing = e;
						}
						else if (e.possibleMovementsWithRook.Count > 0)
						{
							enemyToMove = e;
						}
						else if (e.possibleMovementsWithBishop.Count > 0)
						{
							enemyToMove = e;
						}
						else if (e.possibleMovemenstWithEnemy.Count > 0)
						{
							enemyToMove = e;
						}
					}
				}

			}

			if (enemyToMoveKing != null){
				enemyToMoveKing.MoveEnemy();
			}
			 else if(enemyToMove != null){
				enemyToMove.MoveEnemy();            
			} else {

				foreach(Enemy e in enemies){
					if(e.gameObject.tag == "King"){
						enemyKings.Add(e);                  
					} else {
						enemyOther.Add(e);
					}
				}

				if(GameManager.enemyScore < 30){
					int enemyNum = Random.Range(0, enemies.Length);
                    enemies[enemyNum].MoveEnemy();
				} else {
					int enemyNum = Random.Range(0, enemyOther.Count);
					enemyOther[enemyNum].MoveEnemy();
                    
				}
			}
		}


		enemyToMove = null;
		foreach (Enemy e in enemies)
        {
			e.sqaureWithPlayer = null;
			e.possibleMovements.Clear();
			e.possibleMovemenstWithEnemy.Clear();
			e.possibleMovementsWithKing.Clear();
			e.possibleMovementsWithRook.Clear();
			e.possibleMovementsWithBishop.Clear();
			enemyKings.Clear();
			enemyOther.Clear();
            
            
        }
		}

	}


