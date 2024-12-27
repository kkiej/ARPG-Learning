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

    public string keyJRight;
    public string keyJLeft;
    public string keyJUp;
    public string keyJDown;
    
    [Header("===== Output signals =====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;

    // 1. pressing signal
    public bool run;
    // 2. trigger once signal
    public bool jump;
    private bool lastJump;
    public bool attack;
    private bool lastAttack;
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
        Jup = (Input.GetKey(keyJUp) ? 1.0f : 0f) - (Input.GetKey(keyJDown) ? 1.0f : 0f);
        Jright = (Input.GetKey(keyJRight) ? 1.0f : 0f) - (Input.GetKey(keyJLeft) ? 1.0f : 0f);
        
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0.0f) - (Input.GetKey(keyDown) ? 1.0f : 0.0f);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0.0f) - (Input.GetKey(keyLeft) ? 1.0f : 0.0f);

        if (!inputEnabled)
        {
            targetDup = 0;
            targetDright = 0;
        }
        
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
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
        
        bool newAttack = Input.GetKey(keyC);
        if (newAttack != lastAttack && newAttack)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
        lastAttack = newAttack;
    }

    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - input.y * input.y / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - input.x * input.x / 2.0f);
        
        return output;
    }
}