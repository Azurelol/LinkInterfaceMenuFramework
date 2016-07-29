/******************************************************************************/
/*!
@file   Link.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace LinkInterfaceSystem
{
  /**************************************************************************/
  /*!
  @class Link 
  */
  /**************************************************************************/
  [RequireComponent(typeof(Image))]
  public abstract class Link : MonoBehaviour
  {
    //------------------------------------------------------------------------/
    [Serializable]
    public class LinkNavigation
    {
      public enum NavigationMode { Automatic, Manual }
      public NavigationMode Mode = NavigationMode.Automatic;
      public Link OnNavigateUp;
      public Link OnNavigateDown;
      public Link OnNavigateLeft;
      public Link OnNavigateRight;
    }
        
    //------------------------------------------------------------------------/
    // Events
    public class DescriptionEvent : Stratus.Event
    {
      public enum Type { Title, Description, Help }
      public string Title;
      public string Subtitle;
      public string Description;
      //public string Help;      
      public DescriptionEvent(string description) { Description = description; }
      public DescriptionEvent() { }
    }

    public class OpenedEvent : Stratus.Event {}
    public class ClosedEvent : Stratus.Event {}

    public enum NavigationMode { Automatic, Manual }
    //------------------------------------------------------------------------/
    public bool Tracing = false;

    // The neighbouring links
    public LinkNavigation Navigation;
    // The description of this link
    public string Description;
    // Whether this link has been activated
    [HideInInspector] public bool Active;
    // The interface this link has been registered to
    [HideInInspector] public LinkInterface Interface;

    bool MouseEnabled = false;
    //------------------------------------------------------------------------/
    public Image Image { get { return GetComponent<Image>(); } }
    // Colors
    public Color SelectedColor = Color.red;
    public Color ActiveColor = Color.blue;
    Color DefaultColor;
    // Other
    Vector2 DefaultScale;

    /**************************************************************************/
    /*!
    @brief  Initializes the Link.
    */
    /**************************************************************************/
    void Start()
    {
      // Subscribe to input events
      this.gameObject.Connect<NavigateEvent>(this.OnNavigateEvent);
      this.gameObject.Connect<ConfirmEvent>(this.OnConfirmEvent);
      this.gameObject.Connect<CancelEvent>(this.OnCancelEvent);
      // Save the default settings
      DefaultColor = this.Image.color;
      DefaultScale = this.transform.localScale;
      // If navigation is set to automatic, look for neighbors
      if (Navigation.Mode == LinkNavigation.NavigationMode.Automatic)
        this.AssignNeighbors();

      this.OnInitialize();
    }


    /**************************************************************************/
    /*!
    @brief Looks for this link's neighbors amongst the parent's children.
    */
    /**************************************************************************/
    void AssignNeighbors()
    {
      // Reset the neighbors
      Navigation.OnNavigateUp = null; Navigation.OnNavigateDown = null;
      Navigation.OnNavigateLeft = null; Navigation.OnNavigateRight = null;

      // Look for neighboring links
      float closestRight = 0.0f, closestLeft = 0.0f, closestUp = 0.0f, closestDown = 0.0f;

      var links = transform.parent.GetComponentsInChildren<Link>();
      foreach (var link in links)
      {
        var horizontalDistance = link.transform.position.x - transform.position.x;
        var verticalDistance = link.transform.position.y - transform.position.y;

        // Right 
        if (horizontalDistance > 0 )
        {
          if (closestRight == 0.0f || 
              horizontalDistance < closestRight)
          {           
            closestRight = horizontalDistance;
            Navigation.OnNavigateRight = link;
          }
        }
        // Left
        if (horizontalDistance < 0)
        {
          if (closestLeft == 0 || 
            horizontalDistance > closestLeft)
          {
            closestLeft = horizontalDistance;
            Navigation.OnNavigateLeft = link;
          }
        }
        // Up
        if (verticalDistance > 0)
        {
          if (closestUp == 0.0f ||
            verticalDistance < closestUp)
          {
            closestUp = verticalDistance;
            Navigation.OnNavigateUp = link;
          }
        }
        // Down
        if (verticalDistance < 0)
        {
          if (closestDown == 0.0f || 
            verticalDistance > closestDown)
          {
            closestDown = verticalDistance;
            Navigation.OnNavigateDown = link;
          }
        }
      }

    }

    /**************************************************************************/
    /*!
    @brief Navigates this link.
    @param dir The direction of navigation.
    */
    /**************************************************************************/
    public Link Navigate(Direction dir)
    {
      // We will start by pointing at the current link
      Link link = this;

      // Deselect this link
      link.Deselect();

      switch (dir)
      {
        case Direction.Up:
          if (Navigation.OnNavigateUp)
            link = Navigation.OnNavigateUp;
          break;
        case Direction.Down:
          if (Navigation.OnNavigateDown)
            link = Navigation.OnNavigateDown;
          break;
        case Direction.Left:
          if (Navigation.OnNavigateLeft)
            link = Navigation.OnNavigateLeft;
          break;
        case Direction.Right:
          if (Navigation.OnNavigateRight)
            link = Navigation.OnNavigateRight;
          break;
      }

      // Select the new link
      link.Select();

      return link;
    }

    //************
    // Events
    //------------

    //public override void OnPointerDown(PointerEventData data)
    //{
    //  if (!MouseEnabled)
    //    return;
    //  this.Open();
    //}
    //
    //public override void OnPointerEnter(PointerEventData data)
    //{
    //  if (!MouseEnabled)
    //    return;
    //  this.Select();
    //}
    //
    //public override void OnPointerExit(PointerEventData data)
    //{
    //  if (!MouseEnabled)
    //    return;
    //  this.Deselect();
    //}

    void OnNavigateEvent(NavigateEvent e)
    {
      OnNavigate(e.Direction);
    }

    void OnConfirmEvent(ConfirmEvent e)
    {
      OnConfirm();
    }

    void OnCancelEvent(CancelEvent e)
    {
      OnCancel();
    }

    //************
    // Methods
    //------------

    public void Select()
    {
      Image.CrossFadeColor(this.SelectedColor, 0.5f, true, true);
      this.OnDescribe();
      this.OnSelect();
    }    
    public void Deselect()
    {
      Image.CrossFadeColor(this.DefaultColor, 0.5f, true, true);
      this.OnDeselect();
    }

    public void Open()
    {
      Active = true;
      Image.CrossFadeColor(this.ActiveColor, 0.5f, true, true);
      this.Interface.gameObject.Dispatch<OpenedEvent>(new OpenedEvent());
      this.OnOpen();
    }

    public void Close()
    {
      Image.CrossFadeColor(this.SelectedColor, 0.5f, true, true);
      this.Interface.gameObject.Dispatch<ClosedEvent>(new ClosedEvent());      
      Active = false;
    }

    void Highlight(bool enabled)
    {
      if (enabled)
      {
        Image.CrossFadeColor(this.SelectedColor, 0.5f, true, true);
      }
      else
      {
        Image.CrossFadeColor(this.DefaultColor, 0.5f, true, true);
      }
    }

    //************
    // Subclass
    //------------

    public abstract void OnSelect();
    public abstract void OnDeselect();
    public abstract void OnOpen();
    public abstract void OnClose();

    protected virtual void OnDescribe()
    {
      this.Interface.gameObject.Dispatch<DescriptionEvent>(new DescriptionEvent(this.Description));
    }
    protected virtual void OnInitialize() {}
    protected virtual void OnNavigate(Direction dir) {}
    protected virtual void OnConfirm() {}
    protected virtual void OnCancel() {}


  }

}