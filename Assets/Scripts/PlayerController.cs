using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpSpeed;
    public float fallSpeed;
    public float blockJumpTimerDuration = 1.0f;

	public delegate void TapAction ();
	public static event TapAction OnTap;

	private Rect leftside = new Rect(0, 0, Screen.width * 0.3f, Screen.height);
	private Rect rightside = new Rect(Screen.width * 0.7f, 0, Screen.width * 0.3f, Screen.height);
	private Rect center = new Rect (Screen.width * 0.3f, 0, Screen.width * 0.4f, Screen.height);

	private float blockJumpTimer = 0f;
	private Vector2 currentVelocity;
	private Rigidbody2D rb2d;
	private DirectionRaycasting2DCollider raycast;
	public float moveHorizontal;
    private Animator anim;
	public float minSwipeDistY;
    private Vector2 startPos;
	public bool downButton;
    bool leftGround = false;
    bool slowFall = true;

    //sound effects
    public AudioSource jumpSound;
    public AudioSource walkSound;
    public AudioSource fallSound;
    bool switchFoot = false;
    public AudioClip leftFoot;
    public AudioClip rightFoot;

	private float swipeValue;
    //stores the specific block the player touches at an instance in time
    private GameObject currentBlock;

	//particles on jump
	//public GameObject jumpBlockA, jumpBlockB, jumpBlockC;
	public List<GameObject> blockArray;
	private Renderer rend;
	private int jumpRand;
	// Use this for initialization
	void Start () {
		//Reference so I don't have to type this long thing out repeatedly
		rend = GetComponent<Renderer>();
		rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
		raycast = GetComponent<DirectionRaycasting2DCollider> ();
/*
		blockArray.Add(jumpBlockA);
		blockArray.Add(jumpBlockB);
		blockArray.Add(jumpBlockC);
        */
        if(anim == null)
        {
            Debug.Log("No Animator Attached to Player");	
        }
	}
	
	void Update () {
		//get the position of our feet
		Vector3 feetPos = rend.bounds.center;

		if(Input.GetKeyDown(KeyCode.S)) {
			downButton = true;
		} else {
			downButton = false;
		}
#if UNITY_STANDALONE || UNITY_EDITOR
		if(Input.GetKeyDown(KeyCode.S) ) 
				{
				downButton = true;
				} 
			else 
				{
				downButton = false;
		}

		//Uses mouse clicking for testing tapping screen to activate buttons
		/*
		if(Input.GetMouseButtonDown(0)){
			if(center.Contains(Input.mousePosition)){
				if(OnTap != null)
					OnTap();
			}
		}
		*/

		//The jump mechanic
        if (blockJumpTimer > 0) 
		{
			blockJumpTimer -= Time.deltaTime;
		}   
		else if (Input.GetKeyDown(KeyCode.Space))
		{
            anim.SetTrigger("Jump");
            anim.SetBool("Airborne", true);

            slowFall = true;
            //Player can jump if they are falling or reached max height
            if (rb2d.velocity.y <= 0) {
				//Making it directly alter vertical velocity so jump is instantaneous as well
				//as not super powerful.
				rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
				//only on midair jumps
				if(!raycast.collisionDown){
					jumpRand = Random.Range(0, blockArray.Count);
					Instantiate(blockArray[jumpRand], feetPos, Quaternion.identity);
                    jumpSound.Play();
				}
			} 
			//If player tries to jump before apex, they cannot jump for a set time
			else {
				blockJumpTimer = blockJumpTimerDuration;
			}
		}
		//Fixes infinite jump on the ceiling
		if(raycast.collisionUp)
		{
			blockJumpTimer = blockJumpTimerDuration;
		}
        
        //check for landing
        if(!raycast.collisionDown)
        {
            anim.SetBool("Airborne", true);
            
        }
        else if(raycast.collisionDown)
        {
           
            slowFall = true;
            anim.SetBool("Airborne", false);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            slowFall = false;
        }
		//Fixes the wall climb bug for when you get stuck on a wall if you press the direction key on a wall that you are facing	
		if(raycast.collisionLeft && !raycast.collisionDown){
			moveHorizontal = Input.GetKey(KeyCode.A) ? 0 : Input.GetAxis ("Horizontal");
		} else if (raycast.collisionRight && !raycast.collisionDown){
			moveHorizontal = Input.GetKey(KeyCode.D) ? 0 : Input.GetAxis ("Horizontal");
		} else {
			moveHorizontal = Input.GetAxis ("Horizontal");
		}

        //cause player to fall slowly
        float vertVelocity = rb2d.velocity.y;

        if (vertVelocity < 0 && slowFall == true)
        {
            vertVelocity = fallSpeed;
        }
        else if (vertVelocity < 0 && slowFall == false)
        {
            if (vertVelocity == fallSpeed)
            {
                vertVelocity = 0;
            }
        }
        //set player velocity
        currentVelocity = new Vector2(moveHorizontal * speed, vertVelocity);


        rb2d.velocity = currentVelocity;

#else

        float ySpeed = fallSpeed;
		if(swipeValue < 0)
		{
			downButton = true;
			swipeValue = 0;
		}
		else
		{
			downButton = false;
		}
        
		if (Input.touchCount > 0 )
        {
        
			if (leftside.Contains (Input.GetTouch(0).position)) {
				moveHorizontal = (raycast.collisionLeft && !raycast.collisionDown) ? 0 : -1;
			}
			else if (rightside.Contains (Input.GetTouch(0).position)) {
				moveHorizontal = (raycast.collisionRight && !raycast.collisionDown) ? 0 : 1;
			}
			else if (center.Contains (Input.GetTouch(0).position)){
				//Broadcast Tap Screen Event
				if(OnTap != null){
					OnTap();
				}
			}
        
		}
        
		
       //check for landing
        if(!raycast.collisionDown)
        {
            anim.SetBool("Airborne", true);
            
        }
        else if(raycast.collisionDown )
        {
            
            slowFall = true;
            anim.SetBool("Airborne", false);
        }
        //swipe up to move up
        //to double jump, finger has to go past the minimum distance and swipe again.

		if(raycast.collisionUp)
		{
			blockJumpTimer = blockJumpTimerDuration;
		}

		if (blockJumpTimer > 0) 
		{
			blockJumpTimer -= Time.deltaTime;
		}
        else if (Input.touchCount > 0)
        {

            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
					case TouchPhase.Began:
						startPos = touch.position;
						break;
					case TouchPhase.Ended:
                        Vector2 endPos = touch.position;
                        Vector2 swipeVec = endPos-startPos;
                        float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

                        if (swipeDistVertical > minSwipeDistY)
                        {

                            swipeValue = Mathf.Sign(touch.position.y - startPos.y);

                            if (swipeValue > 0)
                            {
                                if (rb2d.velocity.y <= 0)
                                {
                                    slowFall = true;
                                    //calculate jump force
                                    //this returns 1 if player swipes up, 0 if the swipe to the side, and negative if they swipe downward
                                    float jumpMod = Vector2.Dot(Vector2.up, swipeVec.normalized);
                                    ySpeed = jumpSpeed*jumpMod;    //original float ySpeed                     
                                    moveHorizontal = (1-jumpMod) *(jumpSpeed/2)* Mathf.Sign(swipeVec.x);//float xSpeed = jumpSpeed-(jumpSpeed*jumpMod);
                                    //rb2d.velocity = new Vector2(moveHorizontal * speed, ySpeed); /////////////////////rb2d.velocity.x,jumpspeed
                                    //only on midair jumps
                                    if (!raycast.collisionDown)
                                    {
                                        jumpRand = Random.Range(0, blockArray.Count);
                                        Instantiate(blockArray[jumpRand], feetPos, Quaternion.identity);
                                        jumpSound.Play();
                                    }
                                }
                                //If player tries to jump before apex, they cannot jump for a set time
                                else
                                {
                                    blockJumpTimer = blockJumpTimerDuration;
                                }
                            }                   
                            else if (swipeValue<0)
                            {
                                slowFall = false;
                            }
                        }
                        break;
            } //end switch
        }
        else
        {
            if (leftGround == false)
            {
                moveHorizontal = 0;
            }
        }
        currentVelocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        rb2d.velocity = currentVelocity;
#endif




        //set the walking animation variable to the axis, 
        //use that to check if moving and in which direction
        if (anim != null)
        {
            anim.SetFloat("Walking", moveHorizontal);
            if (currentVelocity == new Vector2(0,0))
            {
                anim.SetBool("Idle", true);
            }
            else
            {
                anim.SetBool("Idle", false);
            }
        }


    }

    //Jump restriction removed if player touches anything
    void OnCollisionEnter2D(Collision2D col) {
		blockJumpTimer = 0f;
    }

    //when the player triggers a collider for a gameObject
    void OnTriggerEnter2D (Collider2D col) {
    	currentBlock = col.gameObject;
    	PlayBlockMusic(currentBlock);
    }

    //when the player exits the collider for a gameObject
    void OnTriggerExit2D (Collider2D col) {
    	currentBlock = col.gameObject;
    	StopBlockMusic(currentBlock);
    }

    //plays specified audio clip from the audio source component attached to thisObject
    void PlayBlockMusic (GameObject thisObject) {
        if (thisObject.tag == "MusicBox" && thisObject.name == "MusicBlock")
        {
        	if (!thisObject.GetComponent<AudioSource>().isPlaying) {
        		thisObject.GetComponent<AudioSource>().PlayOneShot(thisObject.GetComponent<AudioSource>().clip, 1.0f);
        	}
        }
    }

    //stops specified audio clip from the audio source component attached to thisObject
    void StopBlockMusic (GameObject thisObject) {
    	if (thisObject.tag == "MusicBox" && thisObject.name == "MusicBlock") {
    		if (thisObject.GetComponent<AudioSource>().isPlaying) {
    			thisObject.GetComponent<AudioSource>().Stop();
    		}
    	}
    }


    public void PlayWalkSound()
    {
        if(switchFoot == false)
        {
            walkSound.clip = leftFoot;
        }
        else
        {
            walkSound.clip = rightFoot;
        }
        walkSound.Play();
        switchFoot = !switchFoot;
    }

    public void PlayFallSound()
    {
        fallSound.Play();
    }
   
 }
