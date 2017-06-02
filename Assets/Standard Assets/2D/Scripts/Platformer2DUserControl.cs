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
        
        public float fireRate = 0.5f;
        float nextFire = 0;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
            
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.DownArrow);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                // Read the jump input in Update so button presses aren't missed.
                //shoot();
                if(Time.time>nextFire){
                    nextFire= Time.time+fireRate;
                    m_Shoot = true;
                    //m_Character.ShootBullet();
                }
            }
            m_Character.Move(h, crouch, m_Jump,m_Shoot);
            m_Jump = false;
            m_Shoot = false;
        }
        void shoot()
        {
            if(Time.time>nextFire){
                nextFire= Time.time+fireRate;
                m_Shoot = true;
                m_Character.ShootBullet();
            }
        }
    }
}
