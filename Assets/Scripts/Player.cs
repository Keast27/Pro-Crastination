using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Player : MonoBehaviour
{
    public float maxSpeed = 20f;
    private bool facingRight = true;
    private bool sit = true;
    private bool coffee = false;
    private int penalty = 0;
    Rigidbody2D rigidBody;
    Collider2D collide;
    Animator animator;
    SpriteRenderer spriteRender;
    private Sprite[] stevewalk;
    private Sprite[] coffeewalk;
    private Sprite[] spillcoffee1;
    private Sprite[] spillcoffee2;
    private Sprite[] stainwalk;
    private Sprite[] desk;
    private Sprite[] deskNoCoffee;
    private Sprite[] deskSit;
    private Sprite[] deskSitNoCoffee;
    private Sprite[] deskSitStained;
    private Sprite currentwalk;
    private Sprite currentdesk;

    private RuntimeAnimatorController stevewalkAnim;
    private RuntimeAnimatorController coffeewalkAnim;
    private RuntimeAnimatorController spillcoffee1Anim;
    private RuntimeAnimatorController spillcoffee2Anim;
    private RuntimeAnimatorController stainwalkAnim;
    private RuntimeAnimatorController deskAnim;
    private RuntimeAnimatorController deskNoCoffeeAnim;
    private RuntimeAnimatorController deskSitAnim;
    private RuntimeAnimatorController deskSitNoCoffeeAnim;
    private RuntimeAnimatorController deskSitStainedAnim;
    private RuntimeAnimatorController currentAnim;
    private RuntimeAnimatorController currentDeskAnim;
 
    public float displayedTime = 0;

    public Vector2 teleportPosition;
    Vector2 returnPosition;

    public Text keepReading;
    public GameObject textBox;
    public GameObject choiceBox;

 

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        collide = GetComponent<Collider2D>();
        spriteRender = GetComponent<SpriteRenderer>();

        stevewalk = Resources.LoadAll<Sprite>("Sprites/SteveWalk");
        coffeewalk = Resources.LoadAll<Sprite>("Sprites/SteveWalkCoffee");
        spillcoffee1 = Resources.LoadAll<Sprite>("Sprites/CoffeeSpill1");
        spillcoffee2 = Resources.LoadAll<Sprite>("Sprites/CoffeeSpill2");
        stainwalk = Resources.LoadAll<Sprite>("Sprites/SteveWalkStain");

        desk = Resources.LoadAll<Sprite>("Sprites/Desk");
        deskNoCoffee = Resources.LoadAll<Sprite>("Sprites/DeskNoCoffee");
        deskSit = Resources.LoadAll<Sprite>("Sprites/SteveSits");
        deskSitNoCoffee = Resources.LoadAll<Sprite>("Sprites/SteveSitsNoCoffee");
        deskSitStained = Resources.LoadAll<Sprite>("Sprites/SteveSitsStain");

        currentwalk = stevewalk[0];
        currentdesk = deskSit[0];

        stevewalkAnim = Resources.Load<RuntimeAnimatorController>("Sprites/SteveWalk_0");
        coffeewalkAnim = Resources.Load<RuntimeAnimatorController>("Sprites/SteveWalkCoffee_0");
        spillcoffee1Anim = Resources.Load<RuntimeAnimatorController>("Sprites/CoffeeSpill1_0");
        spillcoffee2Anim = Resources.Load<RuntimeAnimatorController>("Sprites/CoffeeSpill2_0");
        stainwalkAnim = Resources.Load<RuntimeAnimatorController>("Sprites/SteveWalkStain_0");

        deskAnim = Resources.Load<RuntimeAnimatorController>("Sprites/Desk_0");
        deskNoCoffeeAnim = Resources.Load<RuntimeAnimatorController>("Sprites/DeskNoCoffee_0");
        deskSitAnim = Resources.Load<RuntimeAnimatorController>("Sprites/SteveSits_0");
        deskSitNoCoffeeAnim = Resources.Load<RuntimeAnimatorController>("Sprites/SteveSitsNoCoffee_0");
        deskSitStainedAnim = Resources.Load<RuntimeAnimatorController>("Sprites/SteveSitsStain_0");

        currentAnim = stevewalkAnim;
        currentDeskAnim = deskNoCoffeeAnim;
    }
   
    // Update is called once per frame
    void Update()
    {
        Move();
        KeepReading();
    }

    // Moves player using rigid body
    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        animator.enabled = true;

        if (moveX == 0 && moveY == 0)
        {
            if (!sit)
            {
                spriteRender.sprite = currentwalk;
                animator.enabled = false;
            }
        }
        else
        {
            sit = false;
            changeSpriteWalk(currentwalk, currentAnim);

            rigidBody.velocity = new Vector2(moveX * maxSpeed, rigidBody.velocity.y);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, moveY * maxSpeed);
            if (moveX > 0 && !facingRight)
            {
                Flip();
            }
            else if (moveX < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Goes to next line when space is pressed
    void KeepReading()
    {
        if (textBox.activeSelf || choiceBox.activeSelf)
        {
            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll; // Stops movement when text is being displayed
            displayedTime += Time.deltaTime;
            if (textBox.activeSelf)
            {
                // Prevents going to next line immediately after conversation starts
                if (Input.GetKeyDown(KeyCode.Space) && displayedTime >= 0.5f)
                    keepReading.GetComponent<Button>().onClick.Invoke();
            }
            if (choiceBox.activeSelf)
            {
                FindObjectOfType<DialogueManager>().MakeChoice();
            }
        }
        // Allows movement
        else
        {
            rigidBody.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            displayedTime = 0;
        }
    }

    public void Teleport()
    {
        Debug.Log("first " + rigidBody.position);
        returnPosition = rigidBody.position;
        transform.position = teleportPosition;
        rigidBody.MovePosition(teleportPosition);
        Debug.Log(rigidBody.position);
    }

    private void CoffeeStatus()
    {
        if (Math.Abs(rigidBody.velocity.x) > 0.8 || Math.Abs(rigidBody.velocity.y) > 0.8)
        {
            if (currentwalk != stevewalk[0])
            {
                penalty++;
                if (penalty == 10)
                {
                    if (currentwalk == coffeewalk[0])
                    {
                        changeSpriteWalk(spillcoffee1[0], spillcoffee1Anim);
                    }
                    else if (currentwalk == spillcoffee1[0])
                    {
                        changeSpriteWalk(spillcoffee2[0], spillcoffee2Anim);
                    }
                    else if (currentwalk == spillcoffee2[0])
                    {
                        changeSpriteWalk(stainwalk[0], stainwalkAnim);
                    }
                    penalty = 0;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentwalk == stevewalk[0] && collision.gameObject.name == "WaterCooler_0")
        {
            changeSpriteWalk(coffeewalk[0], coffeewalkAnim);
        }
        if (currentwalk == stevewalk[0] && collision.gameObject.name == "Desk" && !coffee)
        {
            changeSpriteDesk(deskNoCoffee[0], deskNoCoffeeAnim, collision);
        }
        if (currentwalk == stainwalk[0] && collision.gameObject.name == "Desk" && !coffee)
        {
            changeSpriteDesk(deskNoCoffee[0], deskNoCoffeeAnim, collision);
        }
        if (currentwalk == stevewalk[0] && collision.gameObject.name == "Desk" && coffee)
        {
            changeSpriteDesk(desk[0], deskAnim, collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentwalk == stevewalk[0] && collision.gameObject.name == "WaterCooler_0")
        {
            changeSpriteWalk(coffeewalk[0], coffeewalkAnim);
        }
        if (currentwalk == stevewalk[0] && collision.gameObject.name == "Desk" && !coffee)
        {
            changeSpriteDesk(deskSitNoCoffee[0], deskSitNoCoffeeAnim, collision);
            sit = true;
        }
        if (currentwalk == stainwalk[0] && collision.gameObject.name == "Desk" && !coffee)
        {
            changeSpriteDesk(deskSitStained[0], deskSitStainedAnim, collision);
            sit = true;
        }
        if (currentwalk == coffeewalk[0] || currentwalk == spillcoffee1[0] || currentwalk == spillcoffee2[0])
        {
            if (collision.gameObject.name == "Desk")
            {
                changeSpriteWalk(stevewalk[0], stevewalkAnim);
                changeSpriteDesk(deskSit[0], deskSitAnim, collision);
                coffee = true;
                sit = true;
            }
        }
    }

    private void changeSpriteWalk(Sprite sprite, RuntimeAnimatorController controller)
    {
        spriteRender.sprite = sprite;
        currentwalk = sprite;
        animator.runtimeAnimatorController = controller;
        currentAnim = controller;
    }

    private void changeSpriteDesk(Sprite sprite, RuntimeAnimatorController controller, Collider2D collision)
    {
        currentdesk = sprite;
        currentDeskAnim = controller;
        collision.GetComponent<SpriteRenderer>().sprite = sprite;
        collision.GetComponent<Animator>().runtimeAnimatorController = controller;
    }
}




