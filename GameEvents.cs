using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GameEvents {
    public static void StartChasing(SPatrol spatrol, Transform user) {
        if (Singleton<SceneController>.Instance.isGameOver || spatrol.hasBeenEscapedFrom) return;
        Debug.Log(spatrol.name + " Start Chasing Player");
        spatrol.hasDiscoverAPlayer = true;
        spatrol.chasingPlayer = user;
    }

    public static void LeavePlayer(SPatrol spatrol) {
        if (Singleton<SceneController>.Instance.isGameOver || spatrol.hasBeenEscapedFrom) {
            return;
        }
        Debug.Log (spatrol.name + " Leave Player");
        spatrol.SetEscapedColor();
        Singleton<SceneController>.Instance.AddScore(spatrol.spatroleed);
        spatrol.hasDiscoverAPlayer = false;
        spatrol.chasingPlayer = null;
        spatrol.hasBeenEscapedFrom = true;
    }

    public static void CollidePlayer(SPatrol spatrol) {
        if (Singleton<SceneController>.Instance.isGameOver || spatrol.hasBeenEscapedFrom) {
            return;
        }
        Debug.Log(spatrol.name + " Collide Player");
        Singleton<SceneController>.Instance.GameOver();
    }
}
