using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;
        private Animator firePower_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        public GameObject bulletPrefab_1;
        public GameObject bulletPrefab_2;
        public GameObject bulletPrefab_3;
        public Transform shootOrigin;
        public float rocketSpeed;
        public float dashSpeed;


        private Vector2 boostSpeed = new Vector2(100,0);
        private bool canBoost = true;
        
        public float boostTime = 0.25f;
        public int boostPower = 1000;
        private bool dashing = false;

        public int JumpsAvailable = 1;
        private int jumpCounter=0;

        GameController gc;        

        private int currentChargeLvl =0;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = transform.Find("sprites").GetComponent<Animator>();
            firePower_Anim = transform.Find("sprites").transform.Find("shootPower").GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            if(GameObject.FindGameObjectWithTag("GameController")!=null)
            {
                gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>(); 
            }
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);
            if(m_Grounded){
                jumpCounter=0;
            }
            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
            
        }


        public void Move(float move, bool crouch, bool jump,bool shoot,int shoot_lvl,float firePower,bool dash,bool pointingUp)
        {
            
            // If crouching, check to see if the character can stand up

            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);
            m_Anim.SetBool("PointingUp",pointingUp);
            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                if(m_Grounded)
                {
                    move = (crouch ? move*m_CrouchSpeed : move);    
                }
                

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                if(dashing)
                {
                    m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, 0f);
                }
                else
                {
                    m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);   
                }

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                jumpCounter++;
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
            else
            {
                if(jump && jumpCounter<JumpsAvailable)
                {
                    jumpCounter++;
                    m_Grounded = false;
                    m_Anim.SetBool("Ground", false);
                    m_Rigidbody2D.velocity= new Vector2(0f,0f);
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                }
            }

            
            firePower_Anim.SetFloat("firePower", firePower);
            

            

            if(shoot && shoot_lvl>0)
            {
                Debug.Log("shoot");
                Debug.Log(shoot_lvl);
                if(pointingUp)
                {
                    ShootBulletUp(shoot_lvl);    
                }
                else
                {
                    ShootBullet(shoot_lvl);
                }
                
            }
            if(dash)
            {
                Debug.Log("move Dash");
                goDash();
            }
           //  if(!shoot && m_Anim.GetBool("Shoot"))
           // {
           //      m_Anim.SetBool("Shoot", shoot);
           // } 


            if(currentChargeLvl != shoot_lvl){
                gc.updateChargeLvl(shoot_lvl);
                currentChargeLvl = shoot_lvl;
            }

        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void ShootBullet(int shoot_lvl)
        {
            GameObject Clone=null;
            //Clone = (Instantiate(bulletPrefab, transform.position,transform.rotation)) as GameObject;
            m_Anim.SetBool("Shoot",true);
            StartCoroutine(stopShoot());
            Vector2 thrust;
            float local_rocketSpeed=rocketSpeed;
            Debug.Log(shoot_lvl);
            if(shoot_lvl==1){
                Clone = (Instantiate(bulletPrefab_1, shootOrigin.position,Quaternion.Euler(new Vector3(0,0,0))));
                local_rocketSpeed = rocketSpeed;
            }
            else {
                if(shoot_lvl ==2){
                    Clone = (Instantiate(bulletPrefab_2, shootOrigin.position,Quaternion.Euler(new Vector3(0,0,0))));
                    local_rocketSpeed = (float)(rocketSpeed*0.75);
                }
                else{
                        if(shoot_lvl ==3){
                            Clone = (Instantiate(bulletPrefab_3, shootOrigin.position,Quaternion.Euler(new Vector3(0,0,0))));
                            local_rocketSpeed = (float)(rocketSpeed*0.5);    
                        }
                        
                }
            }
            // Clone = (Instantiate(bulletPrefab_1, shootOrigin.position,Quaternion.Euler(new Vector3(0,0,0))));
            
            if(m_FacingRight)
            {
                thrust = new Vector2(local_rocketSpeed, 0);
            }
            else
            {                
                thrust = new Vector2(-local_rocketSpeed, 0);
                Vector3 theScale = Clone.transform.localScale;
                theScale.x *= -1;
                Clone.transform.localScale = theScale;                
            }
            
            Clone.GetComponent<Rigidbody2D>().AddForce(thrust * 10);
            
            
            
        }
        public void ShootBulletUp(float firePower)
        {
            GameObject Clone;
            //Clone = (Instantiate(bulletPrefab, transform.position,transform.rotation)) as GameObject;
            m_Anim.SetBool("ShootUp",true);
            StartCoroutine(stopShoot());
            Clone = (Instantiate(bulletPrefab_1, shootOrigin.position,Quaternion.Euler(new Vector3(0,0,90))));
            Vector2 thrust;
                          
            thrust = new Vector2(0, rocketSpeed);
            //thrust = new Vector2(rocketSpeed, 0);
                          
            Clone.GetComponent<Rigidbody2D>().AddForce(thrust * 5);
            
            
        }
        public void pushArea(bool _pushing)
        {
            m_Anim.SetBool("pushing", _pushing);
        }
        IEnumerator stopShoot(){
            yield return new WaitForSeconds(0.3f);
            m_Anim.SetBool("Shoot",false);
            m_Anim.SetBool("ShootUp",false);
        }

        public void goDash()
        {
            
            //Clone = (Instantiate(bulletPrefab, transform.position,transform.rotation)) as GameObject;            
            StartCoroutine( Boost() );
            m_Anim.SetBool("dashing",true);
            dashing= true;
            StartCoroutine(stopDash());
            
            
            
        }

        IEnumerator stopDash(){
            yield return new WaitForSeconds(boostTime-0.1f);            
            m_Anim.SetBool("dashing",false);
            dashing=false;
        }

        IEnumerator Boost(){
            float time=0; //create float to store the time this coroutine is operating            
            //canBoost = false; //set canBoost to false so that we can't keep boosting while boosting
     
            while(boostTime > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
            {
                time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
                if(m_FacingRight)
                {
                    m_Rigidbody2D.AddForce(new Vector2(boostPower,0f));
                }
                else
                {                
                    m_Rigidbody2D.AddForce(new Vector2(-boostPower,0f));                
                }

                //m_Rigidbody2D.velocity = boostSpeed; //set our rigidbody velocity to a custom velocity every frame, so that we get a steady boost direction like in Megaman
                yield return 0; //go to next frame
            }

        }
    }
}
