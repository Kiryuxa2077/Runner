using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;
    private BoxCollider2D _characterCollider;

    public bool isGrounded;

    private Rigidbody2D _playerRigidbody;
    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
       
        

        if (_playerRigidbody == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
    }

    private void Awake()
    {
        _characterCollider = transform.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        MovePlayer();
        IsGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();

        if(isGrounded == false)
            Debug.Log("Not at the ground");
    }
    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        //_playerRigidbody.AddForce(new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y));

        _playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y);
    }
    private void Jump() => _playerRigidbody.velocity = new Vector2(0, jumpPower);//.AddForce(transform.up * jumpPower);//

    private void IsGrounded()
    {
        var raycastHit2d = Physics2D.BoxCast(_characterCollider.bounds.center, _characterCollider.bounds.size, 0f, Vector2.down, .1f, _groundLayerMask);
        isGrounded = raycastHit2d.collider != null;
    }
}
