using UnityEngine;
using System.Collections;

public class OrbController : MonoBehaviour
{
    public bool isControlsActive = false;

    protected int platformLayerMask = 1 << 8;
    protected int backGroundLayerMask = 1 << 9;


    [SerializeField]
    protected LayerMask layerMask;
    public bool isGrounded = false;

    [SerializeField]
    [Range(0, 10)]
    protected float gravity = 5f;
    [SerializeField]
    [Range(0, 25)]
    protected float moveAcceleration = 3f;
    [SerializeField]
    [Range(0, 30)]
    protected float jumpSpeed = 8f;
    [SerializeField]
    [Range(0, 20)]
    protected float stopDeceleration = 6f;
    [SerializeField]
    [Range(0, 20)]
    protected float maxMoveSpeed = 5f;
    [SerializeField]
    [Range(0, 20)]
    protected float maxFallSpeed = 5f;

    protected float xVelocity = 0;
    protected float yVelocity = 0;

    protected float slowPenalty = 0.65f;


    
}
