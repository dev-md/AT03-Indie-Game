using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void MenuButtonAction();
    
    [SerializeField] private Color defaultColour;
    [SerializeField] private Color selectedColour;
    [SerializeField] private Color hoverColour;
    [SerializeField] private UnityEvent onActive;

    private Image image;
    private bool mouseOver = false;
    //------------------
    private MenuNav instance;

    public event MenuButtonAction ActiveEvent = delegate { };
    public event MenuButtonAction SelectEvent = delegate { };

    private void Awake()
    {
        TryGetComponent(out image);
        transform.parent.TryGetComponent(out instance);

        image.color = defaultColour;

    }

    private void Update()
    {
        if(mouseOver == true && Input.GetButtonDown("Fire1"))
        {
            if(instance.SelectedButton == this)
            {
                Active();
            }
            else
            {
                Select();
            }    
        }
    }

    public void Select()
    {
        SelectEvent.Invoke();
    }

    public void Active()
    {
        ActiveEvent.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        if(instance.SelectedButton != this)
        {
            image.color = hoverColour;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        if(image.color == hoverColour && this != instance.SelectedButton)
        {
            image.color = defaultColour;
        }
    }

    public void OnSelect()
    {
        if(instance.SelectedButton != null)
        {
            instance.SelectedButton.image.color = instance.SelectedButton.defaultColour;
        }
        instance.SelectedButton = this;
        image.color = selectedColour;
    }

    public void OnActive()
    {
        onActive.Invoke();
    }

    private void OnEnable()
    {
        ActiveEvent += OnActive;
        SelectEvent += OnSelect;
    }
    private void OnDisable()
    {
        ActiveEvent -= OnActive;
        SelectEvent -= OnSelect;
    }
}
