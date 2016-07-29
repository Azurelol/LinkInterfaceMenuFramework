/******************************************************************************/
/*!
@file   Menu.cs
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
  @class Menu Base class for all menu components.
  */
  /**************************************************************************/
  public abstract class Menu : Window
  {
    public Window RootWindow;
    public bool Launch; 

    /**************************************************************************/
    /*!
    @brief  Initializes the Script.
    */
    /**************************************************************************/
    protected override void OnWindowInitialize()
    {
      // Called on the menu subclass
      InitializeMenu();

      if (Launch) Enabled = true;
    }

    protected abstract void InitializeMenu();

    protected override void OnLinkOpened()
    {
      DisplayRoot(false);
    }

    protected override void OnLinkClosed()
    {
      DisplayRoot(true);
    }   
    
    /**************************************************************************/
    /*!
    @brief Opens the menu.
    */
    /**************************************************************************/
    protected override void OnWindowOpen()
    {
      // Open the root window and display it
      if (RootWindow) RootWindow.Open();
      DisplayRoot(true);
      // Announce the gamestate change
      OnMenuOpen();      
    }

    protected override void OnWindowClose()
    {
      if (RootWindow) RootWindow.Close();
      OnMenuClose();      
    }

    protected override void OnInterfaceCancel()
    {
      
    }

    protected abstract void OnMenuOpen();
    protected abstract void OnMenuClose();

    /**************************************************************************/
    /*!
    @brief Displays the root window.
    */
    /**************************************************************************/
    void DisplayRoot(bool display)
    {
      if (!RootWindow)
        return;

      if (display)
      {
        RootWindow.Show();
      }
      else
      {
        RootWindow.Hide();
      }
    }    

  }
}