/******************************************************************************/
/*!
@file   GenericMenu.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using LinkInterfaceSystem;

namespace Demonstration
{
  /**************************************************************************/
  /*!
  @class GenericMenu 
  */
  /**************************************************************************/
  [RequireComponent(typeof(MenuInput))]
  public class GenericMenu : Menu
  {
    protected override void InitializeMenu()
    {      
    }

    protected override void OnMenuClose()
    {
    }

    protected override void OnMenuOpen()
    {
    }


  }

}