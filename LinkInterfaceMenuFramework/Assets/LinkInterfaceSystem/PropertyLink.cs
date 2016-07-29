/******************************************************************************/
/*!
@file   PropertyLink.cs
@author Christian Sagel
@par    email: ckpsm@live.com
*/
/******************************************************************************/
using UnityEngine;
using Stratus;
using UnityEngine.UI;
using Ludiq.Reflection;
using System;

namespace LinkInterfaceSystem
{
  /**************************************************************************/
  /*!
  @class PropertyLink 
  */
  /**************************************************************************/
  public class PropertyLink : Link
  {
    enum PropertyType { Number, Boolean, Invalid }
    public UnityVariable Property;    
    public NavigationAxis Axis;
    public float Increment = 1.0f;
    public float Minimum = 0.0f;
    public float Maximum = 100.0f;

    Slider Slider { get { return GetComponentInChildren<Slider>(); } }
    Text ValueText { get { return transform.FindChild("Value").GetComponent<Text>(); } }
    PropertyType Type = PropertyType.Invalid;

    public override void OnOpen()
    {
      
    }

    public override void OnClose()
    {
            
    }

    public override void OnSelect()
    {

    }

    public override void OnDeselect()
    {
      
    }

    protected override void OnInitialize()
    {
      ValidateType();
      UpdateVisual();
    }

    protected override void OnNavigate(Direction dir)
    {
      if (Axis == NavigationAxis.Horizontal)
      {
        if (dir == Direction.Right)
        {
          this.ApplyIncrement();
        }
        else if (dir == Direction.Left)
        {
          this.ApplyDecrement();
        }
      }
      else if (Axis == NavigationAxis.Vertical)
      {
        if (dir == Direction.Up)
        {
          this.ApplyIncrement();
        }
        else if (dir == Direction.Down)
        {
          this.ApplyDecrement();
        }
      }
    }

    protected override void OnConfirm()
    {

    }

    protected override void OnCancel()
    {
      this.Close();
    }

    void ApplyIncrement()
    {
      if (Tracing) Trace.Script("Applying increment to '" + Property.name + "'");
      
      if (Type == PropertyType.Number)
      {
        var currentValue = Property.Get<float>();
        var newValue = currentValue + this.Increment;
        if (newValue > Maximum) newValue = Maximum;
        Property.Set(newValue);
      }
      else if (Type == PropertyType.Boolean)
      {
        Property.Set(true);
      }

      UpdateVisual();      
    }

    void ApplyDecrement()
    {
      if (Tracing) Trace.Script("Applying decrement to '" + Property.name + "'");

      if (Type == PropertyType.Number)
      {
        var currentValue = Property.Get<float>();
        var newValue = currentValue - this.Increment;
        if (newValue < Minimum) newValue = Minimum;
        Property.Set(newValue);
      }
      else if (Type == PropertyType.Boolean)
      {
        Property.Set(false);
      }

      UpdateVisual();
    }

    void UpdateVisual()
    {
      ValueText.text = Convert.ToString(Property.Get());
    }

    void ValidateType()
    {
      var type = Property.type;
      if (type == typeof(float) || type == typeof(int))
      {
        Type = PropertyType.Number;
      }
      else if (type == typeof(bool))
      {
        Type = PropertyType.Boolean;
      }
    }
   




  }

}