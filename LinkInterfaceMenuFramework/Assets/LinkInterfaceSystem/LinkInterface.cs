/******************************************************************************/
/*!
@file   LinkInterface.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using System.Collections.Generic;

namespace LinkInterfaceSystem
{
  /**************************************************************************/
  /*!
  @class LinkInterface 
  */
  /**************************************************************************/
  public abstract class LinkInterface : MonoBehaviour
  {
    [HideInInspector] public bool Tracing = false;
    protected bool Enabled = false;
    public bool Active
    {
      set { this.gameObject.SetActive(value); }
      get { return this.gameObject.activeSelf; }
    }    
    protected LinkController Controller;
    protected Link CurrentLink;

    /**************************************************************************/
    /*!
    @brief  Initializes the LinkInterface.
    */
    /**************************************************************************/
    void Awake()
    {
      this.gameObject.Connect<NavigateEvent>(this.OnNavigateEvent);
      this.gameObject.Connect<ConfirmEvent>(this.OnConfirmEvent);
      this.gameObject.Connect<CancelEvent>(this.OnCancelEvent);
      this.gameObject.Connect<Link.OpenedEvent>(this.OnLinkOpenedEvent);
      this.gameObject.Connect<Link.ClosedEvent>(this.OnLinkClosedEvent);

      // Look for the LinkController among the children
      Controller = GetComponentInChildren<LinkController>();
      if (Controller) Controller.Connect(this);

      // Initialize the interface subclass
      this.OnInterfaceInitialize();

      // If the interface is to be opened by default
      if (Enabled)
      {
        this.Open();
      }
      // Disable it by default
      else
        Active = false;
    }

    /*========================================================================= 
                                  EVENTS
      ========================================================================*/
    void OnNavigateEvent(NavigateEvent e)
    {
      if (!CurrentLink)
        return;

      // If there is a link active, redirect output
      if (RedirectInput<NavigateEvent>(e))
        return;

      CurrentLink = CurrentLink.Navigate(e.Direction);
      if (Tracing) Trace.Script("Current link is now '" + CurrentLink.gameObject.name + "'", this);
    }

    void OnConfirmEvent(ConfirmEvent e)
    {
      if (!CurrentLink)
        return;

      // If there is a link active, redirect output
      if (RedirectInput<ConfirmEvent>(e))
        return;

      if (Tracing) Trace.Script("Opened '" + CurrentLink.name + "'");
      CurrentLink.Open();
    }

    void OnCancelEvent(CancelEvent e)
    {
      // If there is a link active, redirect output
      if (RedirectInput<CancelEvent>(e))
        return;

      this.OnInterfaceCancel();      
    }

    void OnLinkOpenedEvent(Link.OpenedEvent e)
    {
      OnLinkOpened();
    }

    void OnLinkClosedEvent(Link.ClosedEvent e)
    {
      OnLinkClosed();
    }

    /**************************************************************************/
    /*!
    @brief Redirects input events to the window if there is one open.
    @param e The input event. 
    @return True if a window was open and the input was redirected, false otherwise.
    */
    /**************************************************************************/
    bool RedirectInput<U>(U inputEvent) where U : Stratus.Event
    {
      if (CurrentLink && CurrentLink.Active)
      {
        CurrentLink.gameObject.Dispatch<U>(inputEvent);
        return true;
      }

      return false;
    }    

    /**************************************************************************/
    /*!
    @brief Registers all links (children with a Link component).
    */
    /**************************************************************************/
    //void RegisterLinks()
    //{
    //  if (!Links)
    //    return;
    //
    //  // Look for a controller among children
    //  Controller = GetComponentInChildren<LinkController>();
    //
    //  // Add all available links
    //  var availableLInks = Links.GetComponentsInChildren<Link>(true);
    //  foreach (var link in availableLInks)
    //  {
    //    Add(link);
    //    link.Interface = this;
    //  }
    //}

    /**************************************************************************/
    /*!
    @brief Opens the link interface. This will call the subclass OnOpen as well.
    */
    /**************************************************************************/
    public void Open()
    {
      if (Tracing) Trace.Script("Opening interface!", this);
      Active = true;

      // Perhaps allow the interface to select which will be its active window?
      if (Controller && Controller.Links.Count > 0) CurrentLink = Controller.Links[0];
      // If there's a link available, select it
      if (CurrentLink) CurrentLink.Select();

      this.OnInterfaceOpen();
    }

    /**************************************************************************/
    /*!
    @brief Closes the link interface. This will call the subclass OnClose as well.
    */
    /**************************************************************************/
    public void Close()
    {
      if (Tracing) Trace.Script("Closing!", this);
      this.OnInterfaceClose();
      Active = false;
    }

    protected abstract void OnInterfaceInitialize();
    protected abstract void OnInterfaceOpen();
    protected abstract void OnInterfaceClose();
    protected abstract void OnInterfaceCancel();
    //protected abstract void OnInterfaceLinkDescription(Link.DescriptionEvent e);

    protected virtual void OnLinkOpened() { }
    protected virtual void OnLinkClosed() { }

  }



}
