using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPatrol : MonoBehaviour {
    public bool isEnabled = false;
    public float spatroleed = 1.0f;

    public bool hasBeenEscapedFrom = false;
    public int curPointNo;
    private Vector3 nextTarget; 
    public List<Vector3> RectPoints; 
    public bool hasDiscoverAPlayer = false;
    public Transform chasingPlayer;
    public Action<SPatrol, Transform> OnDiscoverPlayer;
    public Action<SPatrol> OnCollisionPlayer;
    public Action<SPatrol> OnLeavePlayer;

    public SPatrol SetFromData(SPatrolData spatrol) {
        this.RectPoints = spatrol.RectPoints;
        this.spatroleed = spatrol.spatroleed;
        this.hasBeenEscapedFrom = false;
        return this;
    }

    public SPatrol StartPatrol() {
        hasDiscoverAPlayer = false;
        chasingPlayer = null;
        nextTarget = RectPoints[curPointNo];
        isEnabled = true;
        return this;
    }

    public SPatrol InitPosition() {
        transform.position = RectPoints[0];
        return this;
    }

    public SPatrol ClearCallbacks() {
        OnCollisionPlayer = null;
        OnLeavePlayer = null;
        OnDiscoverPlayer = null;
        return this;
    }

    public SPatrol InitColor() {
        Renderer render = GetComponent<Renderer>();
        render.material.shader = Shader.Find("Transpatrolarent/Diffuse");
        render.material.color = Color.red;

        return this;
    }

    public SPatrol SetEscapedColor() {
        Renderer render = GetComponent<Renderer>();
        render.material.shader = Shader.Find("Transpatrolarent/Diffuse");
        render.material.color = Color.green;

        return this;
    }

    public void ChangeDirection() {
        if (++curPointNo == RectPoints.Count) {
            curPointNo = curPointNo - RectPoints.Count;
        }

        nextTarget = RectPoints[curPointNo];
    }

    // Use this for initialization
    void Start () {
        isEnabled = true;
        curPointNo = 0;
    }
    
    // Update is called once per frame
    void Update () {
        if (isEnabled) {
            float step = spatroleed * Time.deltaTime;
            Vector3 tar = hasDiscoverAPlayer ? chasingPlayer.position : nextTarget;
            transform.localPosition = Vector3.MoveTowards(transform.position, tar, step);
            
            if (hasDiscoverAPlayer == false) {
                if (Vector3.Distance(transform.position, nextTarget) < 0.5f) {
                    ChangeDirection();
                }
            }
        }
    }

    void OnCollisionEnter(Collision obj) {
        if (obj.gameObject.tag.Contains("Player")) {
            if (OnCollisionPlayer != null)
                OnCollisionPlayer(this);
        } 
        else {
            nextTarget = RectPoints[++curPointNo];
        }
    }

    void OnTriggerEnter(Collider obj) {
        if (obj.gameObject.tag.Contains("Player")) {
            if (OnDiscoverPlayer != null)
                OnDiscoverPlayer(this, obj.transform);
        } 
    }

    void OnTriggerExit(Collider obj) {
        if (obj.gameObject.tag.Contains("Player")) {
            if (OnLeavePlayer != null)
                OnLeavePlayer(this);
        }
    }
}
