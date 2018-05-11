using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectGenerator : MonoBehaviour {

    public float PositionRange = 20.0f;
    public float defaultSideLength = 5.0f;
    public float yPosition = 1.0f;

    public List<Vector3> GetRandomRect(int sides = 4, float SideLength = 0) {
        List<Vector3> tempV = new List<Vector3> ();

        // generate random Rect
        if (SideLength == 0) {
            SideLength = defaultSideLength;
        }
        SideLength = Random.Range(10f, 20f);
        Vector3 ldown = new Vector3 (Random.Range(-PositionRange, PositionRange), yPosition, Random.Range(-PositionRange, PositionRange));
        Vector3 rdown = ldown + Vector3.right * SideLength;
        Vector3 lup = ldown + Vector3.forward * SideLength;
        Vector3 rup = rdown + Vector3.forward * SideLength;

        Vector3 tmp = ldown + Vector3.forward * SideLength * Random.Range(0f, 1f);
        tempV.Add(tmp);
        tmp = leftUp + Vector3.right * SideLength * Random.Range(0f, 1f);
        tempV.Add(tmp);
        tmp = rup + Vector3.forward * (-SideLength) * Random.Range(0f, 1f);
        tempV.Add(tmp);

        if (sides >= 4) {
            tmp = rdown + Vector3.right * (-SideLength) * Random.Range(0f, 0.5f);
            tempV.Add(tmp);
            if (sides == 5) {
                tmp = rdown + Vector3.right * (-SideLength) * Random.Range(0.5f, 1f);
                tempV.Add(tmp);
            }
        }
        
        tempVurn tempV;
    }
    
}
