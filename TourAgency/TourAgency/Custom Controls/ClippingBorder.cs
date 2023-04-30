using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace TourAgency
{
    public class ClippingBorder : Border
    {
        private RectangleGeometry _clipRect = new();
        private object? _oldClip;

        protected override void OnRender(DrawingContext dc)
        {
            OnApplyChildClip();
            base.OnRender(dc);
        }

        public override UIElement Child
        {
            get => base.Child;
            set
            {
                if (Child != value)
                {
                    Child?.SetValue(ClipProperty, _oldClip);

                    if (value != null)
                        _oldClip = value.ReadLocalValue(ClipProperty);
                    else _oldClip = null;

                    base.Child = value;
                }
            }
        }

        protected virtual void OnApplyChildClip()
        {
            UIElement child = Child;
            if (child != null)
            {
                _clipRect.RadiusX = _clipRect.RadiusY = Math.Max(0.0, CornerRadius.TopLeft - (BorderThickness.Left * 0.5));
                _clipRect.Rect = new Rect(Child.RenderSize);
                child.Clip = _clipRect;
            }
        }
    }
}
