using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    Enemy[] enemies;

	

    public void EnemyTurn() {

        enemies = FindObjectsOfType<Enemy>();
        //print("There are " + enemies.Length + " left.");
        if (enemies.Length > 0)
        {
            
            foreach (Enemy e in enemies)
            {
                //e.FindPieceSquares();
            }

            int enemyNum = Random.Range(0, enemies.Length);
            enemies[enemyNum].MoveEnemy();

            foreach (Enemy e in enemies)
            {
                //e.ClearAllEnemyPieces();
            }

            foreach (Enemy e in enemies)
            {
                //e.FindPieceSquares();
            }

        }
        else {
            print("Win!");
        }

    }
	
}
