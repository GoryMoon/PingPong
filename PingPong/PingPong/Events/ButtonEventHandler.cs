using PingPong.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingPong.Events
{
    public delegate void onClick(Button btn);

    public class ButtonEventHandler
    {
        private HashSet<Button> buttons = new HashSet<Button>();
        public event onClick Click;

        public ButtonEventHandler(HashSet<Button> buttons)
        {
            foreach (Button btn in buttons)
            {
                addButton(btn);
            }
        }

        public void addButton(Button btn)
        {
            btn.Click += new EventHandler(delegate(object button, EventArgs e) { if (Click != null) { Click(button as Button); } });
            buttons.Add(btn);
        }

        public void removeButton(Button btn)
        {
            btn.Click -= new EventHandler(delegate(object button, EventArgs e) { if (Click != null) { Click(button as Button); } });
        }

        public void Unload()
        {
            foreach (Button btn in buttons)
            {
                addButton(btn);
            }
            buttons = null;
        }

    }
}
