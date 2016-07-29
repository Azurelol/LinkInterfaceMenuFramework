/******************************************************************************/
/*!
@file   MenuInput.cs
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
  @class MenuInput 
  */
  /**************************************************************************/
  public class MenuInput : MonoBehaviour
  {
    public KeyCode UpKey = KeyCode.W;
    public KeyCode DownKey = KeyCode.S;
    public KeyCode LeftKey = KeyCode.A;
    public KeyCode RightKey = KeyCode.D;
    public KeyCode ConfirmKey = KeyCode.E;
    public KeyCode CancelKey = KeyCode.Tab;
    
    void Update()
    {
      if (Input.GetKeyDown(this.UpKey)) Navigate(LinkInterfaceSystem.Direction.Up);
      if (Input.GetKeyDown(this.DownKey)) Navigate(LinkInterfaceSystem.Direction.Down);
      if (Input.GetKeyDown(this.LeftKey)) Navigate(LinkInterfaceSystem.Direction.Left);
      if (Input.GetKeyDown(this.RightKey)) Navigate(LinkInterfaceSystem.Direction.Right);

      if (Input.GetKeyDown(this.ConfirmKey)) this.Confirm();
      if (Input.GetKeyDown(this.CancelKey)) this.Cancel();
    }

    void Navigate(LinkInterfaceSystem.Direction dir)
    {
      this.gameObject.Dispatch<LinkInterfaceSystem.NavigateEvent>(new LinkInterfaceSystem.NavigateEvent(dir));
    }

    void Confirm()
    {
      this.gameObject.Dispatch<LinkInterfaceSystem.ConfirmEvent>(new LinkInterfaceSystem.ConfirmEvent());
    }

    void Cancel()
    {
      this.gameObject.Dispatch<LinkInterfaceSystem.CancelEvent>(new LinkInterfaceSystem.CancelEvent());
    }

  }

}