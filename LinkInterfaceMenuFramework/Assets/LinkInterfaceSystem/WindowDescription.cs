/******************************************************************************/
/*!
@file   WindowDescription.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using UnityEngine.UI;

namespace LinkInterfaceSystem
{
  /**************************************************************************/
  /*!
  @class WindowDescription 
  */
  /**************************************************************************/
  public class WindowDescription : MonoBehaviour
  {
    public Text TitleText, SubtitleText, DescriptionText;

    /**************************************************************************/
    /*!
    @brief  Initializes the Script.
    */
    /**************************************************************************/
    void Start()
    {
      this.gameObject.Connect<Link.DescriptionEvent>(this.OnLinkDescriptionEvent);
    }

    void OnLinkDescriptionEvent(Link.DescriptionEvent e)
    {
      if (e.Title != null) UpdateTitle(e.Title);
      if (e.Subtitle != null) UpdateSubtitle(e.Subtitle);
      if (e.Description != null) UpdateDescription(e.Description);
    }

    void OnWindowDescriptionEvent(Window.DescriptionEvent e)
    {
      UpdateTitle(e.Title);
      UpdateSubtitle(e.Description);
    }

    //void OnWindowUpdateHelpEvent(Window.UpdateHelpEvent e)
    //{
    //  UpdateHelp(e.Text);
    //}

    void UpdateTitle(string text)
    {
      if (!TitleText)
        return;

      TitleText.text = text;
    }

    void UpdateSubtitle(string text)
    {
      if (!SubtitleText)
        return;

      SubtitleText.text = text;
    }

    void UpdateDescription(string text)
    {
      if (!DescriptionText)
        return;

      DescriptionText.text = text;
    }



  }

}