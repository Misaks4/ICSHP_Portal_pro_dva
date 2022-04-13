using UnityEngine;

public class Portal
{
    private readonly float portalForce;
    private readonly GameObject portalObject;
    private readonly GameObject secondPortalObject;
    private readonly Transform secondPortalPoint;

    public Portal(GameObject portalObject, GameObject secondPortalObject, Transform secondPortalPoint,
        float portalForce)
    {
        this.portalObject = portalObject;
        this.secondPortalPoint = secondPortalPoint;
        this.secondPortalObject = secondPortalObject;
        this.portalForce = portalForce;
    }

    public void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer < 6) return;

        var collisionBody2D = collision.gameObject.GetComponent<Rigidbody2D>();
        var velocity = Vector2.zero;
        var position = portalObject.transform.position - collision.gameObject.transform.position;
        var portalAngleDiff = portalObject.transform.eulerAngles.z - secondPortalObject.transform.eulerAngles.z;
        //Debug.Log(portalAngleDiff + " " + portalObject.transform.eulerAngles.z);
        switch (portalAngleDiff)
        {
            case 0:
                switch (portalObject.transform.eulerAngles.z)
                {
                    case 0:
                    case 180:
                        position = new Vector3(-position.x, -position.y, position.z);
                        velocity = new Vector2(portalForce * Mathf.Sign(position.x), collisionBody2D.velocity.y);
                        break;
                    case 90:
                    case 270:
                        position = new Vector3(position.x, -position.y, position.z);
                        velocity = new Vector2(collisionBody2D.velocity.x, portalForce * Mathf.Sign(position.y));
                        break;
                }

                break;
            case 180:
            case -180:
                switch (portalObject.transform.eulerAngles.z)
                {
                    case 0:
                    case 180:
                        position = new Vector3(position.x, -position.y, position.z);
                        velocity = new Vector2(portalForce * Mathf.Sign(position.x), collisionBody2D.velocity.y);
                        break;
                    case 90:
                    case 270:
                        position = new Vector3(-position.x, position.y, position.z);
                        velocity = new Vector2(collisionBody2D.velocity.x, portalForce * Mathf.Sign(position.y));
                        break;
                }

                break;
            case 90:
                switch (portalObject.transform.eulerAngles.z)
                {
                    case 90:
                        position = new Vector3(-position.y, position.x, position.z);
                        velocity = new Vector2(portalForce, -collisionBody2D.velocity.x);
                        break;
                    case 180:
                        position = new Vector3(position.y, position.x, position.z);
                        velocity = new Vector2(-collisionBody2D.velocity.y, portalForce);
                        break;
                    case 0:
                        position = new Vector3(position.y, position.x, position.z);
                        velocity = new Vector2(collisionBody2D.velocity.y, portalForce);
                        break;
                    case 270:
                        position = new Vector3(-position.y, position.x, position.z);
                        velocity = new Vector2(-portalForce, -collisionBody2D.velocity.x);
                        break;
                }

                collision.gameObject.transform.eulerAngles = new Vector3(0, 0,
                    collision.gameObject.transform.eulerAngles.z + portalAngleDiff);

                break;
            case -90:
                switch (portalObject.transform.eulerAngles.z)
                {
                    case 90:
                        position = new Vector3(position.y, -position.x, position.z);
                        velocity = new Vector2(-portalForce, collisionBody2D.velocity.x);
                        break;
                    case 180:
                        position = new Vector3(position.y, -position.x, position.z);
                        velocity = new Vector2(-collisionBody2D.velocity.y, -portalForce);
                        break;
                    case 0:
                        position = new Vector3(position.y, -position.x, position.z);
                        velocity = new Vector2(-collisionBody2D.velocity.y, portalForce);
                        break;
                }

                collision.gameObject.transform.eulerAngles = new Vector3(0, 0,
                    collision.gameObject.transform.eulerAngles.z + portalAngleDiff);
                break;
            case -270:
            case 270:
                switch (portalObject.transform.eulerAngles.z)
                {
                    case 0:
                        position = new Vector3(-position.y, position.x, position.z);
                        velocity = new Vector2(collisionBody2D.velocity.y, -portalForce);
                        break;
                }

                collision.gameObject.transform.eulerAngles = new Vector3(0, 0,
                    collision.gameObject.transform.eulerAngles.z + portalAngleDiff);
                break;
            default:
                return;
        }

        collision.gameObject.transform.position = secondPortalPoint.position + position;
        collisionBody2D.isKinematic = true;
        collisionBody2D.velocity = velocity;
    }
}