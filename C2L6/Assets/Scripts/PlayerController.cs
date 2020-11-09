using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.5f;
    public float jumpForce = 2.5f;
    public float longIdleTime = 5f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector2 _movement;
    private bool _facingRight = true;
    private bool _isGrounded;
    private bool _isAttacking;
    private float _LongIdleTimer;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {

        if (_isAttacking == false)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            _movement = new Vector2(horizontalInput, 0f);

            if (horizontalInput < 0f && _facingRight == true)
                Flip();
            else if (horizontalInput > 0f && _facingRight == false)
                Flip();
        }

        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if(Input.GetButtonDown("Jump") && _isGrounded == true && _isAttacking == false)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire1") && _isGrounded == true && _isAttacking == false)
        {
            _movement = Vector2.zero;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger("Attack");
        }

    }

    void FixedUpdate()
    {
        float HorizontalVelocity = _movement.normalized.x * speed;
        _rigidbody.velocity = new Vector2(HorizontalVelocity, _rigidbody.velocity.y);
    }

    void LateUpdate()
    {
        _animator.SetBool("Idle", _movement == Vector2.zero);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);

        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            _isAttacking = true;
        else
            _isAttacking = false;

        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            _LongIdleTimer += Time.deltaTime;

            if (_LongIdleTimer >= longIdleTime)
              _animator.SetTrigger("LongIdle");
        }
        else
            _LongIdleTimer = 0f;
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
