using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;
    public float transitionTime = .05f;
    public bool moveDiagonally = true;
    public bool mouseRotate = true;
    public bool keyboardRotate = false;
    private float Speed = 5;
    public float turningSpeed = 180;
    //public int stamina = 100;

    private Player player;

    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Confined;
        player = GetComponent<Player>();
    }

    void Update()
    {
        float v = Input.GetAxis("Horizontal");

        anim.SetBool("RightClick", false);
        anim.SetBool("LeftClick", false);
        anim.SetBool("ZButton", false);
        anim.SetBool("SButton", false);
        anim.SetBool("WheelButtonPowerKick", false);
        anim.SetBool("WheelButtonTiredKick", false);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            anim.SetBool("LeftClick", true);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            anim.SetBool("RightClick", true);
        }
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
        {
            Speed = 5;
            float vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
            transform.Translate(0, 0, vertical);
            anim.SetBool("ZButton", true);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Speed = 1;
            float vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
            transform.Translate(0, 0, vertical);            
            anim.SetBool("SButton", true);
        }
        if (Input.GetKey(KeyCode.Mouse2))
        {
            if (player.GetStamina() >= 75)
            {
                player.RemoveStamina(75);
                anim.SetBool("WheelButtonPowerKick", true);
            }
            else
            {
                anim.SetBool("WheelButtonTiredKick", true);
            }
        }
        if (mouseRotate)
            this.transform.Rotate(Vector3.up * (Input.GetAxis("Mouse X")) * Mathf.Sign(v), Space.World);
    }

    public bool IsPunching()
    {
        if (anim.GetBool("WheelButtonPowerKick") || anim.GetBool("WheelButtonTiredKick") || anim.GetBool("RightClick") || anim.GetBool("LeftClick"))
            return true;
        return false;
    }
}

