using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInput : MonoBehaviour
{
    // Variable
    [Header("===== Key settings =====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;
    
    [Header("===== Output signals =====")]
    public float dirUp;
    public float dirRight;
    public float Dmag;
    public Vector3 Dvec;

    // 1. pressing signal
    public bool run;
    // 2. trigger once signal
    public bool jump;
    private bool lastJump;
    // 3. double trigger

    [Header("===== Others =====")]
    public bool inputEnabled = true;
    
    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0.0f) - (Input.GetKey(keyDown) ? 1.0f : 0.0f);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0.0f) - (Input.GetKey(keyLeft) ? 1.0f : 0.0f);

        if (!inputEnabled)
        {
            targetDup = 0;
            targetDright = 0;
        }
        
        dirUp = Mathf.SmoothDamp(dirUp, targetDup, ref velocityDup, 0.1f);
        dirRight = Mathf.SmoothDamp(dirRight, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(dirRight, dirUp));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;
        
        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;

        run = Input.GetKey(keyA);

        bool newJump = Input.GetKey(keyB);
        if (newJump != lastJump && newJump)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

        lastJump = newJump;
    }

    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - input.y * input.y / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - input.x * input.x / 2.0f);
        
        return output;
    }
}