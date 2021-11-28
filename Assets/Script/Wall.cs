/**
Saidahmed Saidahmed
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // access Wall
    public Wall wall;
    // manipulate Wall size
    // set resolution size 2048 x 2048
    public wallSize size = new wallSize(2048, 2048);
    void Start(){
        // renderer 
        var r = GetComponent<Renderer>();
        // render the Wall aka the walls
        wall = new Wall((int)size.x, (int)size.y);
        // set walls as the Walls
        r.material.mainTexture = wall;
    }
} // end