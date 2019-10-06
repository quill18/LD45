using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PhysicsObject : MonoBehaviour
{
    // Modified from: https://learn.unity.com/tutorial/live-session-2d-platformer-character-controller

    public float minGroundNormalY = .65f;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);


    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.1f;


    public float DashLength = 0;
    float dashLeft;
    bool dashAvailable = true;

    public bool CanDash { get { return DashLength > 0; } }
    public bool isDashing { get { return dashLeft > 0; } }

    protected void StartDash()
    {
        if (dashAvailable)
        {
            dashLeft = DashLength;
        }
    }

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    void FixedUpdate()
    {
        dashLeft -= Time.deltaTime;
        if (isDashing)
        {
            velocity.y = 0;
        }
        else
        {
            velocity += Physics2D.gravity * Time.deltaTime;
        }
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);

        if (grounded)
            dashAvailable = true;
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                //Debug.Log(modifiedDistance);
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }

        if(isDashing)
        {
            StartCoroutine(WallDestruction());
        }

        Vector2 v = move.normalized * distance;
        rb2d.position = rb2d.position + v;
    }

    IEnumerator WallDestruction()
    {
        Vector2 pos = transform.position;
        Vector2 dir = rb2d.velocity.x < 0 ? Vector2.left : Vector2.right;
        float WallDestHeight = GetComponentInChildren<Collider2D>().bounds.size.y;
        float WallDestDist = 1;

        bool tryAgain = false;

        do
        {
            tryAgain = false;

            RaycastHit2D[] hits = Physics2D.BoxCastAll(pos, new Vector2(1.0f, WallDestHeight), 0, dir, WallDestDist);
            //Debug.Log("LENGTH: " + hits.Length);

            foreach (RaycastHit2D hit in hits)
            {
                //Debug.Log("HIT: " + hit.collider.name);

                DestructibleTerrain dt = hit.collider.GetComponentInParent<DestructibleTerrain>();
                if (dt == null)
                    continue;

                Tilemap tilemap = dt.GetComponentInParent<Tilemap>();

                Vector3Int tilePos = tilemap.WorldToCell(hit.point);
                if (tilemap.GetTile(tilePos) != null)
                {
                    tilemap.SetTile(tilePos, null);

                    if(dt.DebrisPrefab != null)
                    {
                        Instantiate(dt.DebrisPrefab, hit.point, Quaternion.identity);
                    }

                    // Because terrain is a single collider, we need to 
                    // repeat the cast to see if any other tiles would be hit.
                    tryAgain = true;
                    yield return null; // wait a frame
                }
            }
        } while (tryAgain);
    }
}
