/******************************************************************************/
/*!
@file   MainMenu.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using Stratus;

namespace Demonstration
{
  /**************************************************************************/
  /*!
  @class MainMenu 
  */
  /**************************************************************************/
  public class MainMenu : GenericMenu
  {
    public void StartGame()
    {
      Trace.Script("Starting game!", this);
    }   

    public void EndGame()
    {
      Trace.Script("Ending game!", this);
    }


  }

}