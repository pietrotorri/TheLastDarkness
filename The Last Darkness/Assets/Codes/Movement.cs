﻿namespace IL3DN
{
    using UnityEngine;
    using Random = UnityEngine.Random;
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AudioSource))]

    public class Movement : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking = false;
        [SerializeField] private float m_WalkSpeed = 2;
        [SerializeField] private float m_RunSpeed = 5;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten = 0.7f;
        [SerializeField] private float m_JumpSpeed = 5;
        [SerializeField] private float m_StickToGroundForce = 10;
        [SerializeField] private float m_GravityMultiplier = 2;
        [SerializeField] private IL3DN_SimpleMouseLook m_MouseLook = default;
        [SerializeField] private float m_StepInterval = 2;

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;

        /// <summary>
        /// Initialize the controller
        /// </summary>
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle / 2f;
            m_Jumping = false;
            m_MouseLook.Init(transform, m_Camera.transform);
        }

        private void Update()
        {
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = Input.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }

        /// <summary>
        /// Plays a sound when Player touches the ground for the first time
        /// </summary>
        private void PlayLandingSound()
        {

            m_NextStep = m_StepCycle + .5f;
        }

        /// <summary>
        /// Move the Player
        /// </summary>
        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;


            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            ProgressStepCycle(speed);

            m_MouseLook.UpdateCursorLock();
        }

        /// <summary>
        /// Plays a jump sound
        /// </summary>


        /// <summary>
        /// Play foot steps sound based on time and velocity
        /// </summary>
        /// <param name="speed"></param>
        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }

        /// <summary>
        /// Plays a random sound for a footstep 
        /// </summary>
        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }

        }

        /// <summary>
        /// Reads user input
        /// </summary>
        /// <param name="speed"></param>
        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            bool waswalking = m_IsWalking;
#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }
        }

        /// <summary>
        /// Moves camera based on player position
        /// </summary>
        private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }

        /// <summary>
        /// Used to determine if a player is in a special area to override  the sounds
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            IL3DN_ChangeWalkingSound soundScript = other.GetComponent<IL3DN_ChangeWalkingSound>();

        }

        /// <summary>
        /// Player exits the special area
        /// </summary>
        /// <param name="other"></param>

    }
}

