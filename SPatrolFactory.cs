using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SPatrolFactory : MonoBehaviour {
    public RectGenerator reGen;
    private List<GameObject> used;
    private List<GameObject> free;
    int PatrolCount = 0;

    [Header("Patrol Prefab")]
    public GameObject PatrolPrefab;

    void Start() {
        reGen = Singleton<RectGenerator>.Instance;
        used = new List<GameObject> ();
        free = new List<GameObject> ();
    }

    public void GenPatrol(int count) {
        for (int i = 0; i < count; i++) {
            SPatrolData tempSpData = ScriptableObject.CreateInstance<SPatrolData>();
            tempSpData.speed = Random.Range(5f, 15f);
            tempSpData.RectPoints = reGen.GetRandomRect();

            GameObject tempPatrol = null;
            if (free.Count > 0) {
                Debug.Log("Regenerate from Free");
                tempPatrol = free.ToArray () [free.Count - 1];
                free.RemoveAt(free.Count - 1);
            } else {
                Debug.Log("Generate new GameObject");
                PatrolCount++;
                tempPatrol = Instantiate (PatrolPrefab) as GameObject;
                tempPatrol.name = "Patrol" + PatrolCount.ToString();
            }
            tempPatrol.GetComponent<SPatrol>().SetFromData(tempSpData).ClearCallbacks().InitColor().InitPosition().StartPatrol();

            var spatrol = tempPatrol.GetComponent<SPatrol>();
            spatrol.OnCollisionPlayer += GameEvents.CollidePlayer;
            spatrol.OnDiscoverPlayer += GameEvents.StartChasing;
            spatrol.OnLeavePlayer += GameEvents.LeavePlayer;

            used.Add(tempPatrol);
        }
    }

    public void ReleaseAllPatrols() {
        Debug.Log("Release All");
        for (int i = used.Count - 1; i >= 0; i--) {
            GameObject obje = used[i];
            obje.GetComponent<SPatrol>().InitColor().InitPosition().isEnabled = false;
            free.Add(obje);
            used.RemoveAt(i);
        }
    }
}

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    protected static T instance;

    public static T Instance {
        get {
            if (instance == null) {
                instance = (T) FindObjectOfType (typeof (T));
                if (instance == null) {
                    Debug.LogError ("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
                }
            }
            return instance;
        }
    }
}
