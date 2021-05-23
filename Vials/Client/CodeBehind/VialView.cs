using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared;
using Vials.Shared.Components;
using Vials.Shared.Events;

namespace Vials.Client.CodeBehind
{
    public class VialView : VialComponentBase, IVialView
    {
        [Inject]
        protected IColorToCssMapper colorToCssMapper { get; set; }

        private Vial vial;
        [Parameter]
        public Vial Vial
        {
            get
            {
                return vial;
            }
            set
            {
                vial = value;
                Render(vial);
            }
        }

        [Parameter]
        public int VialIndex { get; set; }

        [Parameter]
        public IEventHandler ClickHandler { get; set; }

        protected string topClass = "top-empty";
        protected string thirdClass = "third-empty";
        protected string secondClass = "second-empty";
        protected string bottomClass = "bottom-empty";

        protected void HandleClick(MouseEventArgs e)
        {
            ClickHandler.Handle(this, EventType.VialWasClicked);
        }

        private void SetCss(int index, string className)
        {
            if (index == 0)
            {
                bottomClass = className;
            }
            else if (index == 1)
            {
                secondClass = className;
            }
            else if (index == 2)
            {
                thirdClass = className;
            }
            else if (index == 3)
            {
                topClass = className;
            }
            else
            {
                throw new Exception($"Unknown vial level index (index : {index}).");
            }
        }

        private void Render(Vial vial)
        {
            if (vial != null)
            {
                var colors = vial.Colors.ToArray();

                for (int i = 0; i < Vial.Length; i++)
                {
                    if (i < colors.Length)
                    {
                        SetCss(i, colorToCssMapper.Map(i, colors[i]));
                    }
                    else
                    {
                        SetCss(i, colorToCssMapper.Map(i, Color.None));
                    }
                }
            }
            else
            {
                for (int i = 0; i < Vial.Length; i++)
                {
                    SetCss(i, colorToCssMapper.Map(i, Color.None));
                }
            }
        }
    }
}
