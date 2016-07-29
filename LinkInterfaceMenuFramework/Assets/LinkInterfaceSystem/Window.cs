/******************************************************************************/
/*!
@file   Window.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace LinkInterfaceSystem
{
  /**************************************************************************/
  /*!
  @class Window 
  */
  /**************************************************************************/
  public class Window : LinkInterface
  {
    //------------------------------------------------------------------------/
    // Events
    //------------------------------------------------------------------------/
    public class RedirectInputEvent : Stratus.Event { public Window Window; }
    public class RegisterEvent : Stratus.Event { public Window Window; }

    public class OpenEvent : Stratus.Event { public Window Window; }
    public class CloseEvent : Stratus.Event { public Window Window; }

    public class OpenedEvent : Stratus.Event { public Window Window; }
    public class ClosedEvent : Stratus.Event { public Window Window; }

    public class DescriptionEvent : Stratus.Event
    {
      public string Title;
      public string Description;
      public DescriptionEvent(string title, string description)
      {
        Title = title;
        Description = description;
      }
    }
    //public class UpdateHelpEvent : Stratus.Event
    //{
    //  public string Text;
    //}
    //------------------------------------------------------------------------/
    public string Title;
    public string Subtitle;
    [HideInInspector] public Sprite Icon;
    //------------------------------------------------------------------------/
    [HideInInspector] public Menu Menu;
    //public List<Window> SubWindows = new List<Window>();

    /**************************************************************************/
    /*!
    @brief  Initializes the Script.
    */
    /**************************************************************************/
    protected override void OnInterfaceInitialize()
    {
      // Subscribe to window-specific events
      this.Subscribe();

      // Initialize the window subclass
      this.OnWindowInitialize();
    }

    void Subscribe()
    {
      this.gameObject.Connect<OpenEvent>(this.OnOpenEvent);
      this.gameObject.Connect<CloseEvent>(this.OnCloseEvent);
    }
    
    void OnOpenEvent(OpenEvent e)
    {
      Open();
    }

    void OnCloseEvent(CloseEvent e)
    {
      Close();
    }
        
    protected override void OnInterfaceOpen()
    {
      this.OnWindowOpen();
    }

    protected override void OnInterfaceClose()
    {
      this.OnWindowClose();
    }

    protected override void OnInterfaceCancel()
    {
      this.OnWindowCancel();
    }

    public void Show()
    {
      Active = true;
    }
    
    public void Hide()
    {
      Active = false;
    }
    

    protected virtual void OnWindowInitialize() { }
    protected virtual void OnWindowOpen() { }
    protected virtual void OnWindowClose() { }
    protected virtual void OnWindowCancel()
    {
      // Default behavior for a window when cancelled is to close
      this.Close();
    }

  }

}