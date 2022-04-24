using System;
using UnityEngine;

public class Player
{
    private readonly KeyBind keyBind;
    private readonly Movement movement;
    private readonly GameObject playerObject;
    private readonly Projectile portalProjectile;
    private readonly Rigidbody2D rigidbody;
    private readonly Transform transform;

    public Player(GameObject playerObject, KeyBind keyBind, Movement movement, Projectile portalProjectile)
    {
        this.playerObject = playerObject;
        this.keyBind = keyBind;
        this.movement = movement;
        this.portalProjectile = portalProjectile;

        rigidbody = this.playerObject.GetComponent<Rigidbody2D>();
        transform = this.playerObject.transform;
    }


    public void Update()
    {
        if (rigidbody.bodyType == RigidbodyType2D.Static)
        {
            return;
        }

        movement.IsCoyoteJump = false;
        movement.IsFallInLimit = movement.IsGrounded;
        if (movement.IsFallInLimit)
        {
            return;
        }

        Move();
        Jump();
        if (Input.GetButtonUp(keyBind.Jump) && rigidbody.velocity.y > 0)
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
        Fire();
    }

    private void Move()
    {
        if (rigidbody.isKinematic)
        {
            rigidbody.isKinematic = false;
            movement.IsParalyzed = true;
            movement.IsFallInLimit = true;
        }

        movement.VerticalLook(Input.GetAxis(keyBind.Vertical));
        var horizontalInput = Input.GetAxis(keyBind.Horizontal);
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,
                transform.localScale.z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y,
                transform.localScale.z);

        if (movement.IsParalyzed)
        {
            if (transform.eulerAngles != Vector3.zero && !movement.IsGrounded) return;
            movement.IsParalyzed = false;
            return;
        }

        transform.eulerAngles = Vector3.zero;
        rigidbody.velocity = new Vector2(horizontalInput * movement.MoveVelocity, rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (!Input.GetButtonDown(keyBind.Jump) || !movement.IsCoyoteJump) return;
        if (movement.IsGrounded || movement.IsCoyoteJump)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, movement.JumpVelocity);
            movement.IsCoyoteJump = true;
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown(keyBind.Fire) && portalProjectile.CooldownTimer > portalProjectile.FireCooldown)
        {
            Vector2 fireVector;
            switch (movement.LookEnum)
            {
                case LookEnum.None:
                    return;
                case LookEnum.Straight:
                    fireVector = new Vector2(Mathf.Sign(transform.localScale.x), 0f);
                    break;
                case LookEnum.Up:
                    fireVector = new Vector2(0f, Mathf.Sign(transform.localScale.y));
                    break;
                case LookEnum.Down:
                    fireVector = new Vector2(0f, -Mathf.Sign(transform.localScale.y));
                    break;
                case LookEnum.SlopeUp:
                    fireVector = new Vector2(Mathf.Sign(transform.localScale.x) / 1.5f,
                        Mathf.Sign(transform.localScale.y) / 1.5f);
                    break;
                case LookEnum.SlopeDown:
                    fireVector = new Vector2(Mathf.Sign(transform.localScale.x) / 1.5f,
                        -Mathf.Sign(transform.localScale.y) / 1.5f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            portalProjectile.CooldownTimer = 0;
            portalProjectile.ProjectileObject.transform.position = portalProjectile.FirePoint.position;
            portalProjectile.FireProjectile(fireVector);
        }

        portalProjectile.CooldownTimer += Time.deltaTime;
    }

    public void Death()
    {
       
    }
}