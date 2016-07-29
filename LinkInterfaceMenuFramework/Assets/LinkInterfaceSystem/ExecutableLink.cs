/******************************************************************************/
/*!
@file   ExecutableLink.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using UnityEngine.Events;
using System;

namespace LinkInterfaceSystem
{
  /**************************************************************************/
  /*!
  @class ExecutableLink 
  */
  /**************************************************************************/
  public class ExecutableLink : Link
  {
    public UnityEvent Method = new UnityEvent();
     
    public override void OnSelect()
    {
    }
    public override void OnDeselect()
    {
    }
    
    public override void OnOpen()
    {      
      Method.Invoke();
      this.Close();
    }

    public override void OnClose()
    {
    }
    
  }

}