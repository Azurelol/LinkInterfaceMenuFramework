/******************************************************************************/
/*!
@file   LinkNavigationEvents.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using Stratus;

namespace LinkInterfaceSystem
{
  /**************************************************************************/
  /*!
  @brief  Used to specify the navigation for links.
  */
  /**************************************************************************/
  public enum Direction { Up, Down, Left, Right }

  /**************************************************************************/
  /*!
  @brief  Used to specify the axis to be used for navigation
  */
  /**************************************************************************/
  public enum NavigationAxis { Horizontal, Vertical }

  /**************************************************************************/
  /*!
  @class NavigateEvent Contains the navigation data received from the input.
  */
  /**************************************************************************/
  public class NavigateEvent : Stratus.Event
  {
    public Direction Direction;
    public NavigateEvent(Direction dir) { Direction = dir; }
  }

  /**************************************************************************/
  /*!
  @class ConfirmEvent Used for confirming the current link. 
  */
  /**************************************************************************/
  public class ConfirmEvent : Stratus.Event { }

  /**************************************************************************/
  /*!
  @class ConfirmEvent Used for cancelling the current link. 
  */
  /**************************************************************************/
  public class CancelEvent : Stratus.Event { }

}