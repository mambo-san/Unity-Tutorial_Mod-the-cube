using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;

    public Vector3 verticalDirection = Vector3.up;
    private float maxY = 7.5f;
    private float minY = 1.0f;

    bool growingX = true;
    bool growingY = true;
    bool growingZ = true;
    private float scaleRateX = 0.5f;
    private float scaleRateY = 0.5f;
    private float scaleRateZ = 0.5f;
    private float maxScale = 6.0f;
    private float minScale = 1.0f;

    public Vector3 rotationRate = new Vector3(10.0f, 5.0f, 15.0f);

    void Start()
    {
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * 1.3f;

        Material material = Renderer.material;

        material.color = new Color(0.5f, 1.0f, 0.3f, 0.4f);
        InvokeRepeating("AssignRandomScales", 5, 3);
        
    }
        void Update()
    {
        bool dirChanged = false;
        //Move position
        if (transform.position.y > maxY)
        {
            verticalDirection = Vector3.down;
            dirChanged = true;
        }
        else if (transform.position.y < minY)
        {
            verticalDirection = Vector3.up;
            dirChanged = true;
        }
        transform.Translate(verticalDirection * Time.deltaTime, Space.World);

        //Slowly change the scale
        growingX = IsGrowing(transform.localScale.x, growingX);
        growingY = IsGrowing(transform.localScale.y, growingY);
        growingZ = IsGrowing(transform.localScale.z, growingZ);
        ScaleAxis('X', growingX);
        ScaleAxis('Y', growingY);
        ScaleAxis('Z', growingZ);

        //Rotate (give the "bouncing look" when it changes direction)
        if (dirChanged)
        {
            rotationRate = rotationRate * -1;
        }
        transform.Rotate(rotationRate * Time.deltaTime);
    }

    void AssignRandomScales()
    {
        scaleRateX = Random.Range(0.1f, 3.0f);
        scaleRateY = Random.Range(0.1f, 3.0f);
        scaleRateZ = Random.Range(0.1f, 3.0f);
    }

    bool IsGrowing(float currentScale, bool currentlyGrowing)
    {
        bool result = currentlyGrowing;
        if (currentScale < minScale)
        {
            result = true;
        }
        else if (currentScale > maxScale)
        {
            result = false;
        }
        return result;
    }

    void ScaleAxis(char target, bool growing)
    {
        Vector3 scaleValues = Vector3.zero;
        switch (target)
        {
            case 'X':
                scaleValues += new Vector3(scaleRateX * Time.deltaTime, 0, 0);
                break;
            case 'Y':
                scaleValues += new Vector3(0, scaleRateY * Time.deltaTime, 0);
                break;
            case 'Z':
                scaleValues += new Vector3(0, 0, scaleRateY * Time.deltaTime);
                break;
        }

        if (growing)
        {
            transform.localScale += scaleValues;
        }
        else
        {
            transform.localScale -= scaleValues; ;
        }
    }
}
