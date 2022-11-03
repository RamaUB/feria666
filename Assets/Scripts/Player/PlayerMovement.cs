using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;
    public float walkSpeed      = 6f;
    public bool isAllowedToMove = true;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb             = GetComponent<Rigidbody2D>();
        isAllowedToMove = true;
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(Input.GetAxis("Horizontal") * walkSpeed, Input.GetAxis("Vertical") * walkSpeed);

        if (isAllowedToMove)
        {
            if(_rb.velocity.x < 0) 
            {
                _spriteRenderer.sprite = westSprite;
            }
            if (_rb.velocity.x > 0) 
            {
                _spriteRenderer.sprite = eastSprite;
            }
            if (_rb.velocity.y < 0) 
            {
                _spriteRenderer.sprite = southSprite;
            }
            if (_rb.velocity.y > 0) 
            {
                _spriteRenderer.sprite = northSprite;
            }   
        }   
    }
}