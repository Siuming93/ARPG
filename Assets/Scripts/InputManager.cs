using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Transform Joystick;
    public Transform Background;
    public float Distance;

    protected Vector2 OldPosition;
    protected bool StillShow;
    protected float HalfWidth;

    private void Start()
    {
        Background.gameObject.SetActive(false);

        HalfWidth = Screen.width/2;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StillShow = true;
            firstIn = true;
        }
        if (StillShow)
            JoystickUpdate();
        if (Input.GetMouseButtonUp(0))
        {
            StillShow = false;
            EndTouch();
        }
    }

    private bool firstIn;

    public InputManager(Transform background)
    {
        Background = background;
    }

    private void JoystickUpdate()
    {
        var pos = Input.mousePosition;
        if (firstIn)
        {
            //记录初始位置
            SetBackGround(pos);
            firstIn = false;
        }
        //改变joystick位置
        SetJoystick(pos);
    }

    private void SetBackGround(Vector2 pos)
    {
        if (pos.x > HalfWidth)
        {
            return;
        }
        else
        {
            OldPosition = pos;
            Background.position = OldPosition;
            Joystick.localPosition = Vector3.zero;

            Background.gameObject.SetActive(true);
        }
    }

    private void SetJoystick(Vector2 pos)
    {
        var deltaMove = pos - OldPosition;


        if (deltaMove.magnitude > Distance)
        {
            deltaMove = Distance/deltaMove.magnitude*deltaMove;
        }

        Joystick.localPosition = deltaMove;
    }

    private void EndTouch()
    {
        Background.gameObject.SetActive(false);
    }
}