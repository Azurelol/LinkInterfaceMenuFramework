/******************************************************************************/
/*!
@file   HelpWindow.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using LinkInterfaceSystem;
using UnityEngine.UI;

namespace Demonstration
{

  /**************************************************************************/
  /*!
  @class HelpWindow 
  */
  /**************************************************************************/
  public class HelpWindow : Window
  {
    public Image MuchHelpSuchDemo;
    
    public void FlipUp()
    {
      MuchHelpSuchDemo.transform.rotation = Quaternion.Euler(0, 0, 180);      
    }

    public void FlipDown()
    {
      MuchHelpSuchDemo.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

  }

}