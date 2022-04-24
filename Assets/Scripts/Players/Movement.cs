using UnityEngine;

public class Movement
{
    private readonly Animator animator;
    private readonly Collider2D collider2D;
    private readonly float coyoteTime;
    private readonly float fallLimit;
    private readonly float paralyzed;
    private readonly LayerMask platformLayerMask;
    private readonly Rigidbody2D rigidbody2D;
    private readonly float verticalTick;
    private float coyoteCounter;
    private float fallCounter;
    private float paralyzedCounter;
    private float verticalTickCounter;

    public Movement(Collider2D collider2D, Rigidbody2D rigidbody2D, Animator animator, LayerMask platformLayerMask,
        float moveVelocity,
        float jumpVelocity, float coyoteTime, float verticalTick, float paralyzed, float fallLimit)
    {
        this.collider2D = collider2D;
        this.rigidbody2D = rigidbody2D;
        this.animator = animator;
        this.platformLayerMask = platformLayerMask;
        this.verticalTick = verticalTick;
        this.paralyzed = paralyzed;
        this.fallLimit = fallLimit;

        this.coyoteTime = coyoteTime;
        MoveVelocity = moveVelocity;
        JumpVelocity = jumpVelocity;
        coyoteCounter = coyoteTime;
        fallCounter = fallLimit;

        LookEnum = LookEnum.Straight;
        animator.SetInteger("look", (int) LookEnum);
    }

    public float JumpVelocity { get; }
    public float MoveVelocity { get; }
    public LookEnum LookEnum { get; private set; }

    public bool IsParalyzed
    {
        get => paralyzedCounter > 0;
        set
        {
            if (value)
            {
                paralyzedCounter = paralyzed;
                return;
            }

            paralyzedCounter -= Time.deltaTime;
        }
    }

    public bool IsFallInLimit
    {
        get => fallCounter <= 0;
        set
        {
            if (value)
            {
                fallCounter = fallLimit;
                return;
            }

            fallCounter -= Time.deltaTime;

            if (fallCounter < 0)
            {
                rigidbody2D.bodyType = RigidbodyType2D.Static;
                animator.SetBool("death", true);
            }
        }
    }

    public bool IsCoyoteJump
    {
        get => coyoteCounter > 0;
        set
        {
            if (value)
            {
                coyoteCounter = 0;
                return;
            }

            if (IsGrounded)
                coyoteCounter = coyoteTime;
            else
                coyoteCounter -= Time.deltaTime;
        }
    }

    public bool IsGrounded
    {
        get
        {
            var hit2D = Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size - new Vector3(0.01f, 0f, 0f),
                0, Vector2.down, 0.01f,
                platformLayerMask);
            return hit2D.collider != null;
        }
    }

    public void VerticalLook(float verticalInput)
    {
        if (verticalInput > 0.01f)
            verticalTickCounter += Time.deltaTime;
        else if (verticalInput < -0.01f) verticalTickCounter -= Time.deltaTime;

        var enumValue = (int) LookEnum;
        if (enumValue == 1 && verticalTickCounter < 0 || enumValue == 5 && verticalTickCounter > 0)
            verticalTickCounter = 0;

        if (verticalTickCounter > verticalTick)
        {
            enumValue++;
            verticalTickCounter = -verticalTick;
        }
        else if (verticalTickCounter < -verticalTick)
        {
            enumValue--;
            verticalTickCounter = verticalTick;
        }
        else
        {
            return;
        }

        animator.SetInteger("look", enumValue);
        LookEnum = (LookEnum) enumValue;
    }
}