using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputController : MonoBehaviour
{
    public PlayerInputControllerEventManager eventManager { get; private set; } = new PlayerInputControllerEventManager();


    // Clicks
    protected void CheckLeftClick()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
            LeftClick(Input.mousePosition);
    }
    public void LeftClick(Vector3 mousePosition)
    {
        eventManager.onLeftClick.OnEvent(mousePosition);
    }

    protected void CheckRightClick()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (Input.GetMouseButtonDown(1))
            RightClick(Input.mousePosition);
    }
    public void RightClick(Vector3 mousePosition)
    {
        eventManager.onRightClick.OnEvent(mousePosition);
    }


    // Hover
    protected void CheckHover()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        Hover(Input.mousePosition);
    }
    public void Hover(Vector3 mousePosition)
    {
        eventManager.onHover.OnEvent(mousePosition);
    }


    // Keyboard
    protected void CheckKeyboardArrows()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        if (moveX != 0 || moveY != 0)
        {
            PressKeyboardArrows(moveX, moveY);
        }
    }
    public void PressKeyboardArrows(float moveX, float moveY)
    {
        eventManager.onPressKeyboardArrows.OnEvent(moveX, moveY);
    }

    protected void CheckKeyPress()
    {

    }
    public void KeyPress(KeyCode key)
    {
        
    }


    // Scroll
    protected void CheckScroll()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        float moveZ = Input.GetAxis("Mouse ScrollWheel");
        if (moveZ != 0)
            Scroll(moveZ);
    }
    public void Scroll(float moveZ)
    {
        eventManager.onScroll.OnEvent(moveZ);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckLeftClick();
        CheckRightClick();
        CheckHover();
        CheckKeyboardArrows();
        CheckScroll();
    }
}
