﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UI.Interface
{
    public interface IUIView
    {
        void SetController(IUIController controller);
        void ToggleUIView(bool value);
    }
}