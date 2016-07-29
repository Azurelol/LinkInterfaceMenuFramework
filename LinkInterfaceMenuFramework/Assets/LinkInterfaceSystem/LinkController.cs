/******************************************************************************/
/*!
@file   LinkController.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using System.Collections.Generic;
using System;

namespace LinkInterfaceSystem
{
  /**************************************************************************/
  /*!
  @class LinkController 
  */
  /**************************************************************************/
  public class LinkController : MonoBehaviour
  {
    [HideInInspector] public List<Link> Links = new List<Link>();
    public Link.LinkStyle Style;
    LinkInterface Interface;
    public bool Tracing = false;

    /**************************************************************************/
    /*!
    @brief  Initializes the LinkController script.
    */
    /**************************************************************************/
    void Awake()
    {
      //Interface = transform.parent.GetComponent<LinkInterface>();
      //if (!Interface)
      //{
      //  throw new ArgumentException("This LinkController is missing a parent LinkInterface!");
      //}
      //// Add all available links
      //RegisterLinks();
    }

    /**************************************************************************/
    /*!
    @brief  Connects the controller with the Link Interface.
    */
    /**************************************************************************/
    public void Connect(LinkInterface linkInterface) 
    {
      if (Tracing) Trace.Script("Connected to '" + linkInterface.gameObject.name + "'", this);
      Interface = linkInterface;
      RegisterLinks();
    }

    /**************************************************************************/
    /*!
    @brief Registers all links (children with a Link component).
    */
    /**************************************************************************/
    void RegisterLinks()
    {
      // Add all available links
      var availableLInks = this.GetComponentsInChildren<Link>(true);
      foreach (var link in availableLInks)
      {
        Add(link);
        link.Interface = Interface;
        // Override the link's style
        link.Style = this.Style;
      }
    }


    /**************************************************************************/
    /*!
    @brief Adds a link to the interface.
    @param window The link to be added.
    */
    /**************************************************************************/
    protected void Add(Link link)
    {
      if (Tracing) Trace.Script("Added '" + link.gameObject.name + "' !", this);
      Links.Add(link);
    }

    /**************************************************************************/
    /*!
    @brief Sets the default style on all registered links.
    */
    /**************************************************************************/
    void SetDefaultStyle()
    {
      foreach(var link in Links)
      {

      }
    }
    

  }

}