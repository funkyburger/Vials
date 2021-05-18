using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vials.Shared
{
    public class Vial
    {
        public static readonly int Length = 4;

        private IList<Color> _colors;
        public IEnumerable<Color> Colors { 
            get 
            {
                return _colors;
            } 
            set 
            {
                if(value == null)
                {
                    _colors = new List<Color>();
                }
                else
                {
                    _colors = new List<Color>(value);
                }
            } 
        }
        public bool IsSelected { get; set; }
        public bool IsOption { get; set; }

        public Color TopColor { 
            get 
            { 
                if(!_colors.Any())
                {
                    return Color.None;
                }

                return _colors.Last();
            }
        }

        public bool IsFull => _colors.Count() >= Length;
        public bool IsEmpty => !_colors.Any();

        public bool IsComplete
        {
            get
            {
                if (!IsFull)
                {
                    return false;
                }

                var current = (Color)default;

                foreach(var color in _colors)
                {
                    if(current == default)
                    {
                        current = color;
                    }
                    else
                    {
                        if(current != color)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public Vial()
        {
            _colors = new List<Color>();
        }

        public Vial(IEnumerable<Color> colors)
        {
            _colors = new List<Color>();
            Stack(colors);
        }

        public void Stack(Color color)
        {
            if (IsFull)
            {
                throw new Exception("Vial is already full.");
            }
            else if(color == default)
            {
                throw new Exception("'None' cannot be stacked as a color.");
            }

            _colors.Add(color);
        }

        public void Stack(IEnumerable<Color> colors)
        {
            foreach(var color in colors)
            {
                Stack(color);
            }
        }

        public Color Pop()
        {
            var poped = _colors.Last();

            _colors.RemoveAt(_colors.Count() - 1);
            
            return poped;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach(var color in _colors)
            {
                builder.Append(color.ToString());
            }

            return builder.ToString();
        }
    }
}
