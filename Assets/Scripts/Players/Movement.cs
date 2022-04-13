using UnityEngine;

public class Movement
{
    private readonly Collider2D collider2D;
    private readonly float coyoteTimeIni;
    private readonly LayerMask platformLayerMask;
    private readonly float verticalTickIni;
    private float coyoteCounter;
    private float verticalTickCounter;

    public Movement(Collider2D collider2D, LayerMask platformLayerMask, float moveVelocity,
        float jumpVelocity, float coyoteTimeIni, float verticalTickIni)
    {
        this.collider2D = collider2D;
        this.platformLayerMask = platformLayerMask;
        this.verticalTickIni = verticalTickIni;

        this.coyoteTimeIni = coyoteTimeIni;
        MoveVelocity = moveVelocity;
        JumpVelocity = jumpVelocity;
        coyoteCounter = coyoteTimeIni;

        verticalTickCounter = 0;
        Paralyzed = 0;
        LookEnum = LookEnum.Straight;
    }

    public float JumpVelocity { get; }
    public float MoveVelocity { get; }
    public float Paralyzed { get; set; }
    public LookEnum LookEnum { get; private set; }

    public bool CoyoteJump
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
                coyoteCounter = coyoteTimeIni;
            else
                coyoteCounter -= Time.deltaTime;
        }
    }

    public bool IsGrounded
    {
        get
        {
            var hit2D = Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size, 0, Vector2.down, 0.01f,
                platformLayerMask);
            return hit2D.collider != null;
        }
    }

    public bool IsLeftWall
    {
        get
        {
            var hit2D = Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size - new Vector3(0.01f, 0.01f),
                0,
                Vector2.left, 0.1f,
                platformLayerMask);
            return hit2D.collider != null;
        }
    }

    public bool IsRightWall
    {
        get
        {
            var hit2D = Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size - new Vector3(0.01f, 0.01f),
                0,
                Vector2.right, 0.1f,
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

        if (verticalTickCounter > verticalTickIni)
        {
            enumValue++;
            verticalTickCounter = -verticalTickIni;
        }
        else if (verticalTickCounter < -verticalTickIni)
        {
            enumValue--;
            verticalTickCounter = verticalTickIni;
        }
        else
        {
            return;
        }

        LookEnum = (LookEnum) enumValue;
    }
}