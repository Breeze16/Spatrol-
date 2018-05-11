using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {
	float score;
	public int patrolNum = 5;
	private int EscaptedPatrolNum = 0;
	public SPatrolFactory patrolFac;

	public bool isOver = false;
	
	void RestartGame() {
		score = 0;
		EscaptedPatrolNum = 0;
		isOver = false;
		patrolFac.GenPatrol(patrolNum);
	}

	// Use this for initialization
	void Start () {
		patrolFac = Singleton<SPatrolFactory>.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GameOver() {
		Debug.Log("Game Over");
		isOver = true;
		patrolFac.ReleaseAllPatrols();
	}

	public void AddScore(float tempScore) {
		if (EscaptedPatrolNum < patrolNum) {
			score += tempScore;
			if (++EscaptedPatrolNum == patrolNum) {
				GameOver();
			}
		}
		
	}

	void OnGUI() {
		// display the score
		if (GUI.Button(new Rect(20, 20, 20, 20), "S")) {
			RestartGame();
		}

		GUI.Button(new Rect(50, 20, 150, 20), "Score : " + score.ToString());
	}
}
