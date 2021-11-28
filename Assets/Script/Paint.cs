/**
Saidahmed Saidahmed
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class Paint : MonoBehaviour
{
    // scale brush
    [SerializeField] private Transform brush;
    // brush size
    [SerializeField] private int brushSize = 5;
    // access renderer
    private Renderer accessRender;
    // get color
    private Color[] colors;
    // brush distance from walls
    private float distance;
    // define touch
    private RaycastHit distFromWall;
    // define wall
    private Wall varWalls;
    // checks the position and last position
    private wallSize checkPos, prevPos;
    // painting bounds
    private bool bounds;
    // rotation
    private Quaternion setRotation;
    
    void Start()
    {
        accessRender = brush.GetComponent<Renderer>();
        // scale brush however much, l x w
        colors = Enumerable.Repeat(accessRender.material.color, brushSize * brushSize).ToArray();
        // checks the distance from the walls
        distance = brush.localScale.y;
    }

    // draw
    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        // checks if brush is touching anything
        if (Physics.Raycast(brush.position, transform.up, out distFromWall, distance))
        {
            // makes sure that its painting on wall
            if (distFromWall.transform.CompareTag("Wall"))
            {
                // if null
                if (varWalls == null)
                {
                    // then its considered touch
                    varWalls = distFromWall.transform.GetComponent<Wall>();
                }
                // where on the wall am i touching
                checkPos = new wallSize(distFromWall.textureCoord.x, distFromWall.textureCoord.y);
                // paint to width bounds
                var x = (int)(checkPos.x * varWalls.wbSize.x - (brushSize / 2));
                // paint to height bounds
                var y = (int)(checkPos.y * varWalls.wbSize.y - (brushSize / 2));
                // stop painting if out of bounds
                if (y < 0 || y > varWalls.wbSize.y || x < 0 || x > varWalls.wbSize.x)
                    return;
                // if we are touching wall
                if (bounds)
                {
                    // then we can draw
                    varWalls.wb.SetPixels(x, y, brushSize, brushSize, colors);
                    // loop to continue drawing
                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        // sets the points of where we can touch on width
                        var lerpX = (int)Mathf.Lerp(prevPos.x, x, f);
                        // sets the points of where we can touch on height
                        var lerpY = (int)Mathf.Lerp(prevPos.y, y, f);
                        // count to draw
                        varWalls.wb.SetPixels(lerpX, lerpY, brushSize, brushSize, colors);
                    }
                    // lock the rotation
                    transform.rotation = setRotation;
                    // update wb
                    varWalls.wb.Apply();
                }
                // set position
                prevPos = new wallSize(x, y);
                // set rotation
                setRotation = transform.rotation;
                // set bounds
                bounds = true;
                return;
            }
        }
        // else wall is null
        varWalls = null;
        // bounds aren't touched
        bounds = false;
    }
}
