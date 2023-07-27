using EasyAudioSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HarropCharlie.MusicGame
{
    /*
     * Code sourced from Dawnosaur Youtube https://www.youtube.com/watch?v=KbtcEVCM7bw
    */
    public class PlayerCharacterController : MonoBehaviour
    {
        public delegate void PlayerInvisibleAction();
        public static event PlayerInvisibleAction OnPlayerInvisible;

        public static Transform playerParent;

        [SerializeField] private InputActionReference move, jump;

        [Header("Run")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float decceleration;
        [SerializeField] private float velPower;
        [SerializeField] private float frictionAmount;

        [Header("Jump")]
        [SerializeField] private float jumpForce;
        [Range(0, 1)]
        [SerializeField] private float jumpCutMulitplier;
        [SerializeField] private float jumpCoyoteTime;
        [SerializeField] private float jumpbufferTime;
        [SerializeField] private float fallGravityMultiplier;
        private float gravityScale;
        private float lastGroundedTime;
        private float lastJumpTime;
        private bool isJumping;

        [Header("Checks")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Vector2 groundCheckSize;
        [SerializeField] private LayerMask GroundLayer;

        private Rigidbody2D _rB2D;
        private float _moveInput;

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private ParticleSystem _particleSystem;

        private void OnEnable()
        {
            jump.action.performed += OnJump;
            jump.action.canceled += JumpCut;

            move.action.performed += WalkSound;
            move.action.canceled += WalkSound;
        }

        private void OnDisable()
        {
            jump.action.performed -= OnJump;
            jump.action.canceled -= JumpCut;

            move.action.performed -= WalkSound;
            move.action.canceled -= WalkSound;
        }

        private void Start()
        {
            _rB2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            gravityScale = _rB2D.gravityScale;

            playerParent = transform.parent;
        }

        private void Update()
        {
            #region Inputs
            _moveInput = move.action.ReadValue<float>();
            #endregion

            #region Checks
            if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, GroundLayer))
            {
                lastGroundedTime = jumpCoyoteTime;
            }

            if (_rB2D.velocity.y < 0)
            {
                isJumping = false;
            }
            #endregion

            #region Jump
            if (lastGroundedTime > 0 && lastJumpTime > 0 && !isJumping)
            {
                Jump();
            }
            #endregion

            #region Timers
            lastGroundedTime -= Time.deltaTime;
            lastJumpTime -= Time.deltaTime;
            #endregion

            #region Visuals
            flipDirectionFacing();
            MoveAnimations();
            #endregion
        }

        private void FixedUpdate()
        {
            #region Run
            float targetSpeed = _moveInput * moveSpeed;

            float speedDiff = targetSpeed - _rB2D.velocity.x;

            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

            float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velPower) * Mathf.Sign(speedDiff);

            _rB2D.AddForce(movement * Vector2.right);
            #endregion

            #region Friction

            #endregion

            #region Jump Gravity
            if (_rB2D.velocity.y < 0)
            {
                _rB2D.gravityScale = gravityScale * fallGravityMultiplier;
            }
            else
            {
                _rB2D.gravityScale = gravityScale;
            }
            #endregion
        }

        private void Jump()
        {
            _rB2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            lastGroundedTime = 0;
            lastJumpTime = 0;
            isJumping = true;
            FindObjectOfType<AudioManager>().Play("Jump");

        }

        private void OnJump(InputAction.CallbackContext ctx)
        {
            lastJumpTime = jumpbufferTime;
        }

        private void JumpCut(InputAction.CallbackContext obj)
        {
            Debug.Log("Jump Cut");
            if (_rB2D.velocity.y > 0 && isJumping)
            {
                _rB2D.AddForce(Vector2.down * _rB2D.velocity.y * (1 - jumpCutMulitplier), ForceMode2D.Impulse);
            }

            lastJumpTime = 0;
        }

        private void flipDirectionFacing()
        {
            if (_moveInput < 0 )
            {
                _spriteRenderer.flipX = false;
            }
            else if ( _moveInput > 0 )
            {
                _spriteRenderer.flipX = true;
            }
        }

        private void MoveAnimations()
        {
            if (_moveInput != 0 )
            {
                _animator.SetBool("isWalking", true);
                _particleSystem.Play();
            }
            if (_moveInput == 0 )
            {
                _animator.SetBool("isWalking", false);
                _particleSystem.Stop();
            }

            if (isJumping)
            {
                _animator.SetBool("isJumping", true);
            }

            if (!isJumping)
            {
                _animator.SetBool("isJumping", false);
            }
        }

        private void OnBecameInvisible()
        {
            StartCoroutine(Respawn()); 
        }

        private IEnumerator Respawn()
        {
            yield return new WaitForEndOfFrame();
            OnPlayerInvisible();
        }

        private void WalkSound(InputAction.CallbackContext ctx)
        {
            if (ctx.performed && !isJumping)
            {
                FindObjectOfType<AudioManager>().Play("Footsteps");
            }
            if (ctx.canceled || isJumping)
            {
                FindObjectOfType<AudioManager>().Pause("Footsteps");
            }
        }
    }
}