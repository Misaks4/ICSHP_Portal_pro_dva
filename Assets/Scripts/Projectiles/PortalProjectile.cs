using UnityEngine;

public class PortalProjectile
{
    private readonly Vector2 bounds;
    private readonly Collider2D portalCollider2D;
    private readonly LayerMask portalHitLayerMask;
    private readonly GameObject portalObject;
    private readonly float portalOffSet;
    private readonly Collider2D projectileCollider2D;
    private readonly GameObject projectileObject;
    private readonly float range;
    private readonly float speed;
    private readonly float starvation;

    private Vector2 direction;
    private bool hit;
    private float lifetime;

    public PortalProjectile(GameObject projectileObject, GameObject portalObject, LayerMask portalHitLayerMask,
        Vector2 bounds, float range, float portalOffSet, float speed, float starvation)
    {
        this.projectileObject = projectileObject;
        this.portalObject = portalObject;
        this.portalHitLayerMask = portalHitLayerMask;
        this.bounds = bounds;
        this.range = range;
        this.portalOffSet = portalOffSet;
        this.speed = speed;
        this.starvation = starvation;

        projectileCollider2D = this.projectileObject.GetComponent<CircleCollider2D>();
        portalCollider2D = this.portalObject.GetComponent<CapsuleCollider2D>();

        this.projectileObject.SetActive(false);
    }

    public void Update()
    {
        if (hit) return;
        projectileObject.transform.Translate(speed * Time.deltaTime * direction.x, speed * Time.deltaTime * direction.y,
            0);

        lifetime += Time.deltaTime;
        if (lifetime > starvation)
            Deactivate();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 6) return;
        hit = true;
        CreatePortal();
        Deactivate();
    }

    public void FireProjectile(Vector2 direction)
    {
        lifetime = 0;
        this.direction = direction;

        if (Physics2D.BoxCast(new Vector3(projectileCollider2D.transform.position.x,
                    projectileCollider2D.transform.position.y, projectileCollider2D.transform.position.z),
                bounds, 0, new Vector2(direction.x, direction.y), 0.1f,
                portalHitLayerMask).collider != null)
            return;
        projectileObject.SetActive(true);
        hit = false;
        projectileCollider2D.enabled = true;
    }

    private void Deactivate()
    {
        projectileCollider2D.enabled = false;
        projectileObject.SetActive(false);
    }

    private void DeactivatePortal()
    {
        portalCollider2D.enabled = false;
        portalObject.SetActive(false);
    }

    private void CreatePortal()
    {
        while (CollisionCheck())
            projectileCollider2D.transform.position = new Vector3(
                projectileCollider2D.transform.position.x - direction.x / 100,
                projectileCollider2D.transform.position.y - direction.y / 100,
                projectileCollider2D.transform.position.z);

        float portalZAngle;
        Vector3 portalPosition;
        try
        {
            portalZAngle = GetPortalAngle(out portalPosition);
        }
        catch (UnityException)
        {
            //Debug.Log(e.Message);
            return;
        }

        DeactivatePortal();

        portalObject.transform.eulerAngles = new Vector3(0f, 0.0f, portalZAngle);
        portalObject.transform.position = portalPosition;

        portalCollider2D.enabled = true;
        portalObject.SetActive(true);
    }


    private bool CollisionCheck()
    {
        var hit2D = Physics2D.BoxCast(new Vector3(projectileCollider2D.transform.position.x,
                projectileCollider2D.transform.position.y, projectileCollider2D.transform.position.z),
            bounds, 0, new Vector2(direction.x, direction.y), 0.01f,
            portalHitLayerMask);

        return hit2D.collider != null;
    }

    private float GetPortalAngle(out Vector3 portalPosition)
    {
        FitPortal(new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y)));
        if (Physics2D.BoxCast(projectileCollider2D.transform.position, bounds, 0, new Vector2(direction.x, 0),
                range * 2, portalHitLayerMask).collider != null)
        {
            portalPosition = new Vector3(
                projectileCollider2D.transform.position.x + Mathf.Sign(direction.x) * -portalOffSet,
                projectileCollider2D.transform.position.y, projectileCollider2D.transform.position.z);
            switch (Mathf.Sign(direction.x))
            {
                case 1:
                    return 180;
                case -1:
                    return 0;
            }
        }

        if (Physics2D.BoxCast(projectileCollider2D.transform.position, bounds, 0, new Vector2(0, direction.y),
                range * 2, portalHitLayerMask).collider != null)
        {
            portalPosition = new Vector3(projectileCollider2D.transform.position.x,
                projectileCollider2D.transform.position.y + Mathf.Sign(direction.y) * -portalOffSet,
                projectileCollider2D.transform.position.z);
            switch (Mathf.Sign(direction.y))
            {
                case -1:
                    return 90;
                case 1:
                    return 270;
            }
        }


        throw new UnityException("No collider found.");
    }

    private void FitPortal(Vector2 direction)
    {
        while (true)
        {
            var sX = 0f;
            var sY = 0f;

            if (Physics2D.BoxCast(projectileCollider2D.transform.position, bounds, 0, new Vector2(direction.x, 0),
                    range, portalHitLayerMask).collider != null)
                sX = direction.x;
            if (Physics2D.BoxCast(projectileCollider2D.transform.position, bounds, 0, new Vector2(0, direction.y),
                    range, portalHitLayerMask).collider != null)
                sY = direction.y;

            if (sX == 0 && sY == 0) break;
            projectileCollider2D.transform.position = new Vector3(
                projectileCollider2D.transform.position.x + range * sY,
                projectileCollider2D.transform.position.y + range * sX,
                projectileCollider2D.transform.position.z);
            break;
        }

        var xAbs = Mathf.Abs(direction.x);
        var yAbs = Mathf.Abs(direction.y);
        if (Physics2D.BoxCast(projectileCollider2D.transform.position, bounds, 0, new Vector2(direction.x, 0),
                range * 2, portalHitLayerMask).collider != null)
            yAbs = 0;

        if (Physics2D.BoxCast(projectileCollider2D.transform.position, bounds, 0, new Vector2(0, direction.y),
                range * 2, portalHitLayerMask).collider != null)
            xAbs = 0;

        if (yAbs == 0 && xAbs == 0) yAbs = Mathf.Abs(direction.y);
        var max = 0;
        while (true)
        {
            if (max++ == 50) throw new UnityException("Maximum number of iterations reached.");
            var leftDown = false;
            var rightUp = false;
            if (Physics2D.BoxCast(projectileCollider2D.transform.position, bounds, 0, new Vector2(-yAbs, -xAbs),
                    portalObject.transform.localScale.y / 2, portalHitLayerMask).collider != null ||
                Physics2D.BoxCast(
                    new Vector2(
                        projectileCollider2D.transform.position.x - yAbs * portalObject.transform.localScale.y / 2,
                        projectileCollider2D.transform.position.y - xAbs * portalObject.transform.localScale.y / 2),
                    bounds, 0, new Vector2(direction.x, direction.y), range * 2,
                    portalHitLayerMask).collider == null)
                leftDown = true;

            if (Physics2D.BoxCast(projectileCollider2D.transform.position, bounds, 0, new Vector2(yAbs, xAbs),
                    portalObject.transform.localScale.y / 2, portalHitLayerMask).collider != null ||
                Physics2D.BoxCast(
                    new Vector2(
                        projectileCollider2D.transform.position.x + yAbs * portalObject.transform.localScale.y / 2,
                        projectileCollider2D.transform.position.y + xAbs * portalObject.transform.localScale.y / 2)
                    , bounds, 0, new Vector2(direction.x, direction.y), range * 2,
                    portalHitLayerMask).collider == null)
                rightUp = true;

            if (leftDown && rightUp) throw new UnityException("Not enough space for portal to be created.");
            if (leftDown)
                projectileCollider2D.transform.position = new Vector3(
                    projectileCollider2D.transform.position.x + 0.01f * yAbs,
                    projectileCollider2D.transform.position.y + 0.01f * xAbs,
                    projectileCollider2D.transform.position.z);
            else if (rightUp)
                projectileCollider2D.transform.position = new Vector3(
                    projectileCollider2D.transform.position.x - 0.01f * yAbs,
                    projectileCollider2D.transform.position.y - 0.01f * xAbs,
                    projectileCollider2D.transform.position.z);
            else
                break;
        }
    }
}