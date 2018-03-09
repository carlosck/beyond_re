using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool m_Shoot;
        private bool m_Dash;
        private bool m_PointingUp;
        private bool canControl = true;
        public float fireRate = 0.5f;
        public float dashRate = 5.0f;
        float nextFire = 0;
        float nextDash = 0;

        private float FirePower = 0f;
        public float maxFirePower = 100f;
        public float firePowerIncrements = 10f;
        private bool holdingPower= false;
        public float firePowerIncrementsRate = 0.2f;
        float nextFireIncrement = 0;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if(canControl){
                if (!m_Jump)
                {
                    // Read the jump input in Update so button presses aren't missed.
                    m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
                }    
            }
            
            
        }


        private void FixedUpdate()
        {
            if(canControl){
                bool crouch = Input.GetKey(KeyCode.DownArrow);
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                int shoot_lvl =0;
                // Pass all parameters to the character control script.
                
                if (CrossPlatformInputManager.GetButton("Fire1"))
                {
                    
                    holdingPower= true;
                    if(Time.time>nextFireIncrement){
                        nextFireIncrement= Time.time+firePowerIncrementsRate;
                        FirePower += firePowerIncrements;
                        if(FirePower>maxFirePower)
                        {
                         FirePower = maxFirePower;   
                        }                        
                    }


                    // if(Time.time>nextFire){
                    //     nextFire= Time.time+fireRate;
                    //     m_Shoot = true;                        
                    // }
                }
                else
                {
                    if(holdingPower){
                        if(Time.time>nextFire){
                            nextFire= Time.time+fireRate;
                            m_Shoot = true;                        
                        }
                        holdingPower= false;
                        shoot_lvl =1;
                        Debug.Log("FirePower");
                        Debug.Log(FirePower);
                        if(FirePower>33 && FirePower < 66){
                           shoot_lvl =2;
                        }
                        else{
                            if(FirePower>=66 )
                            {
                                shoot_lvl =3;
                            }
                        }
                        FirePower=0;
                    }

                }

                if (CrossPlatformInputManager.GetButtonDown("Fire2"))
                {
                    
                    if(Time.time>nextDash){
                        nextDash= Time.time+dashRate;
                        m_Dash = true;
                    }

                }
                if (CrossPlatformInputManager.GetButton("Vertical"))
                {
                    if(Input.GetAxis("Vertical") > 0)
                    {
                        Debug.Log("up");
                        m_PointingUp= true;    
                    }
                    
                }               
                
                m_Character.Move(h, crouch, m_Jump,m_Shoot,shoot_lvl,FirePower,m_Dash,m_PointingUp);
                m_Jump = false;
                m_Shoot = false; 

                m_Dash = false;
                m_PointingUp = false;
            }
            // Read the inputs.
            
        }
        

        public void setControl(bool control)
        {
            canControl= control;
        }
    }
}
