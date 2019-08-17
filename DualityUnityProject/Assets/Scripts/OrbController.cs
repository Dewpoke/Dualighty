using UnityEngine;
using System.Collections;

public class OrbController : MonoBehaviour
{
    public bool isControlsActive = true;

    protected int platformLayerMask = 1 << 8;


    [SerializeField]
    protected LayerMask layerMask;
    public bool isGrounded = false;

    protected float gravity = 5f;
    protected float moveAcceleration = 3f;
    protected float jumpSpeed = 8f;
    protected float stopDeceleration = 6f;
    protected float maxMoveSpeed = 5f;
    protected float maxFallSpeed = 5f;

    protected float xVelocity = 0;
    protected float yVelocity = 0;

    
}
