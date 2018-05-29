using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

	Enemy[] enemies;
	Enemy enemyToMove;


	public void EnemyTurn()
	{

		enemies = FindObjectsOfType<Enemy>();

		if (enemies.Length > 0)
		{
			foreach (Enemy e in enemies)
			{
				e.FindPossibleSpaces();
				if(e.possibleMovemenstWithEnemy.Count > 0){
					enemyToMove = e;

				}             
			}


			if(enemyToMove != null){
				enemyToMove.MoveEnemy();            
			} else {

				int enemyNum = Random.Range(0, enemies.Length);
                enemies[enemyNum].MoveEnemy();
			}
		}



        else {
				print("Win!");
			}


		enemyToMove = null;
		foreach (Enemy e in enemies)
        {
			e.possibleMovements.Clear();
			e.possibleMovemenstWithEnemy.Clear();
            
        }
		}

	}


