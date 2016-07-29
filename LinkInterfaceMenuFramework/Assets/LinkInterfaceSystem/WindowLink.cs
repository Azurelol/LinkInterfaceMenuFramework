/******************************************************************************/
/*!
@file   WindowLink.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using System;

namespace LinkInterfaceSystem
{
  /**************************************************************************/
  /*!
  @class WindowLink 
  */
  /**************************************************************************/  
  public class WindowLink : Link
  {    
    public Window Target;
    string Title { get { return Target.Title; } }
    string Subtitle { get { return Target.Subtitle; } }

    protected override void OnInitialize()
    {      
    }

    void OnWindowDescriptionEvent(Window.DescriptionEvent e)
    {
      // Redirect to the parent
      Interface.gameObject.Dispatch<Window.DescriptionEvent>(e);
    }

    public override void OnSelect()
    {      
      //this.Target.Describe();
    }

    //protected override void OnDescribe()
    //{
    //  var descriptionEvent = new DescriptionEvent();
    //  descriptionEvent.Title = this.Title;
    //  descriptionEvent.Subtitle = this.Subtitle;
    //  this.Interface.gameObject.Dispatch<DescriptionEvent>(descriptionEvent);
    //}

    public override void OnDeselect()
    {
      
    }

    public override void OnOpen()
    {
      Active = true;
      // Request the window to open
      Target.gameObject.Dispatch<Window.OpenEvent>(new Window.OpenEvent());
      // Inform the interface a window has been opened
      // Interface.gameObject.Dispatch<Window.OpenedEvent>(new Window.OpenedEvent());
    }

    public override void OnClose()
    {
      //Target.gameObject.Dispatch<Window.CloseEvent>(new Window.CloseEvent());
    }

    protected override void OnNavigate(Direction dir)
    {
      Forward<NavigateEvent>(new NavigateEvent(dir));
    }

    protected override void OnConfirm()
    {
      Forward<ConfirmEvent>(new ConfirmEvent());
    }

    protected override void OnCancel()
    {
      Forward<CancelEvent>(new CancelEvent());

      // If the window has been deactivated, deactivate this link as well
      if (Target.Active == false)
      {
        this.Close();
      }

    }

    

    void Forward<T>(T forwardedEvent) where T : Stratus.Event
    {
      this.Target.gameObject.Dispatch<T>(forwardedEvent);
    }
  }

}