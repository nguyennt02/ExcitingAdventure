using TarodevController;
using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMove : MonoBehaviour, IPlayerMoveCtrl
{
    [SerializeField] private ScriptableStats _stats;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private Vector2 _frameVelocity;

    private float _time;

    public Vector2 FrameVelocity
    {
        get => _frameVelocity;
        set => _frameVelocity = value;
    }
    public event Action<GameObject> GroundedChanged;
    public event Action<GameObject> WalledChanged;
    public event Action<Vector2> Runed;
    public event Action Jumped;
    public event Action<bool> Dashed;
    public event Action JumpWalled;
    public event Action<bool> Holded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();

        _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
    }
    private void Start()
    {
        _input = InputManager.Instance;
    }
    private void Update()
    {
        _time += Time.deltaTime;
        GatherInput();
    }
    private void FixedUpdate()
    {
        CheckCollisions();
        HandleJump();
        HandleDirection();
        HandleGravity();
        ApplyMovement();
    }

    #region CHECK COLLISIONS
    //CheckGround
    private float _frameLeftGrounded = float.MinValue;
    private bool _cachedQueryStartInColliders;
    private bool _grounded;
    private bool _walled;
    private bool _TouchingTheWall;

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;
        CheckGround();
        CheckWall();
        Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
    }
    private void CheckGround()
    {
        // Ground and Ceiling
        RaycastHit2D groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.GrounderDistance, ~_stats.PlayerLayer);
        bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, ~_stats.PlayerLayer);

        // Hit a Ceiling
        if (ceilingHit)
        {
            if (_Dashing)
            {
                _TouchingTheWall = true;
            }
            _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);
        }

        // Landed on the Ground
        if (!_grounded && groundHit )
        {
            _grounded = true;
            _coyoteUsable = true;
            _bufferedJumpUsable = true;
            _endedJumpEarly = false;
            GroundedChanged?.Invoke(groundHit.collider.gameObject);
        }
        // Left the Ground
        else if (_grounded && !groundHit)
        {
            _grounded = false;
            _frameLeftGrounded = _time;
            GroundedChanged?.Invoke(null);
        }

        if (_grounded)
        {
            _DashNumber = 0;
            if (time > 0)
                time -= Time.deltaTime*2;
        }
    }
    private void CheckWall()
    {
        Vector2 direction = Vector2.right * transform.localScale.x;

        RaycastHit2D wallHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, direction.normalized, _stats.GrounderDistance, ~_stats.PlayerLayer);
        
        if (!_walled && wallHit)
        {
            if (_Dashing)
            {
                _TouchingTheWall = true;
            }
            _walled = true;
            WalledChanged?.Invoke(wallHit.collider.gameObject);
        }
        else if (_walled && !wallHit)
        {
            _walled = false;
            WalledChanged?.Invoke(null);
        }
    }
    #endregion

    #region INPUT
    private IInput _input;
    private FrameInput _frameInput;
    private void GatherInput()
    {
        if (_stats._canInput)
        {
            _frameInput = new FrameInput
            {
                JumpDown = _input.JumpDown,
                JumpHeld = _input.JumpHeld,
                Dash = _input.Dash,
                Hold = _input.Hold,
                Move = _input.Move
            };
        }
        else
        {
            _frameInput = new FrameInput {};
        }

        if (_stats.SnapInput)
        {
            _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
            _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
        }

        if (_frameInput.JumpDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _time;
        }
        if (_frameInput.Dash && _Dashing)
        {
            _timeDashWasPressed = _time;
        }
    }
    #endregion
    #region JUMP
    private bool _endedJumpEarly;
    private bool _jumpToConsume;
    private bool _bufferedJumpUsable;
    private bool _coyoteUsable;
    private float _timeJumpWasPressed;

    private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
    private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

    private void HandleJump()
    {
        if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (_grounded || CanUseCoyote) ExecuteJump();

        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        _frameVelocity.y = _stats.JumpPower;
        Jumped?.Invoke();
    }
    #endregion
    #region RUN
    private void HandleDirection()
    {
        Runed?.Invoke(_frameInput.Move);
        if (!_stats._canInput)
        {
            _frameVelocity.x = 0;
            return;
        }
        if (_frameInput.Move.x == 0)
        {
            var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
        }
    }
    #endregion
    private void HandleGravity()
    {
        bool isHold = false;
        if (!_grounded && _walled && _frameInput.JumpDown && _frameInput.Hold)
        {
            float xJumpWall = Mathf.Sign(transform.localScale.x);
            Vector3 from = new Vector3(-xJumpWall, _frameInput.Move.y, 0) * _stats.JumpWallPower;
            JumpWalled?.Invoke();
            StartCoroutine(Dash(_stats.TimeJumpWall, from));
        }
        else if (!_grounded && _walled && _frameInput.Hold)
        {
            isHold = true;
            HandleHold();
        }
        else if (!_walled &&
                (_frameInput.Dash || HasBufferedDash) && 
                !_Dashing && 
                _DashNumber < _stats.MaxDashNumber &&
                _stats._canDash)
        {
            _DashNumber++;
            HandleDash();
        }
        else if (_grounded && _frameVelocity.y <= 0f)
        {
            _frameVelocity.y = _stats.GroundingForce;
        }
        else
        {
            Gravity();
        }
        Holded?.Invoke(isHold);
    }
    #region DASH
    private bool _Dashing = false;
    private int _DashNumber = 0;
    private float _timeDashWasPressed = 0;
    private bool HasBufferedDash => _timeDashWasPressed != 0 && _time < _timeDashWasPressed + _stats.TimeDash;

    private void HandleDash()
    {
        var direction = Direction() * _stats.DashPower;
        Dashed?.Invoke(true);
        StartCoroutine(Dash(_stats.TimeDash, direction));
    }
    private Vector3 Direction()
    {
        if (_frameInput.Move != Vector2.zero)
        {
            return _frameInput.Move.normalized;
        }
        else
        {
            Vector2 direction = Vector2.right * transform.localScale.x;
            return direction.normalized;
        }
    }
    private IEnumerator Dash(float duration, Vector3 from, float elapsed = 0f)
    {
        _Dashing = true;
        Vector3 to = Vector3.zero;

        while (elapsed < duration && !_TouchingTheWall)
        {
            _frameVelocity = Vector3.Lerp(from, to, (elapsed) / duration);
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        _frameVelocity = to;
        _TouchingTheWall = false;
        _Dashing = false;
        Dashed?.Invoke(_Dashing);
    }
    #endregion
    #region HOLD
    float time = 0;
    float maxTime = 10f;

    private void HandleHold()
    {
        if (time < maxTime)
        {
            Vector3 from = new Vector3(_frameVelocity.x, 0, 0);
            Vector3 to = new Vector3(_frameVelocity.x, -0.2f, 0);

            JumpWalling(from, to, maxTime);
        }
        else if (time < maxTime * 2)
        {
            Vector3 from = new Vector3(_frameVelocity.x, -0.2f, 0);
            Vector3 to = new Vector3(_frameVelocity.x, -_stats.MaxWallFallSpeed, 0);

            JumpWalling(from, to, maxTime * 2);
        }
        else
        {
            _frameVelocity = new Vector3(_frameVelocity.x, -_stats.MaxWallFallSpeed, 0);
        }
        if (_frameInput.Move.y != 0)
        {
            _frameVelocity.y += _frameInput.Move.y * _stats.SpeedWall;
        }
    }
    #endregion
    #region JUMP WALL
    private void JumpWalling(Vector3 from, Vector3 to, float maxTime)
    {
        _frameVelocity = Vector3.Lerp(from, to, (time) / maxTime);
        time += Time.fixedDeltaTime;
    }
    #endregion
    private void Gravity()
    {
        var inAirGravity = _stats.FallAcceleration;
        if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
        _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
    }
    private void ApplyMovement() => _rb.velocity = _stats._canMove ? _frameVelocity : Vector2.zero;
}
public struct FrameInput
{
    public bool JumpDown;
    public bool JumpHeld;
    public bool Dash;
    public bool Hold;
    public Vector2 Move;
}
public interface IPlayerMoveCtrl
{
    public event Action<GameObject> GroundedChanged;

    public event Action<GameObject> WalledChanged;

    public event Action<Vector2> Runed;

    public event Action Jumped;
    public Vector2 FrameVelocity { get; set; }

    public event Action<bool> Dashed;

    public event Action JumpWalled;

    public event Action<bool> Holded;
}
