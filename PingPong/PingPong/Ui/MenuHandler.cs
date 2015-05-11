using PingPong.Menus;
using PingPong.Ui.Transitions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.Ui
{

    public enum TransitionType {
        NONE,
        SLIDELEFT,
        SLIDERIGHT,
        SLIDEUP,
        SLIDEDOWN
    }

    public class MenuHandler
    {

        public Game1 game;
        private Menu oldMenu;
        private Menu activeMenu;
        private Dictionary<String, Menu> menus;
        private Dictionary<TransitionType, Transition> transitions;

        private Transition activeTransition;

        public MenuHandler(Game1 game)
        {
            this.game = game;
            menus = new Dictionary<String, Menu>();
            transitions = new Dictionary<TransitionType, Transition>();
            transitions.Add(TransitionType.NONE, new None());
            transitions.Add(TransitionType.SLIDELEFT, new SlideLeft());
            transitions.Add(TransitionType.SLIDERIGHT, new SlideRight());
            transitions.Add(TransitionType.SLIDEDOWN, new SlideDown());
            transitions.Add(TransitionType.SLIDEUP, new SlideUp());

            registerMenu("None", new EmptyMenu());
            changeTo("None");
        }

        public void registerMenu(String name, Menu menu)
        {
            menu.addHandler(this);
            menus.Add(name, menu);
        }

        public bool changeTo(String name)
        {
            return changeTo(name, TransitionType.NONE);
        }

        public bool changeTo(String name, TransitionType transition) {
            Menu temp = menus[name];

            if (temp != null)
            {
                oldMenu = activeMenu;
                temp.LoadContent(game.Content);
                temp.PostLoadContent(game.Content);
                activeTransition = getTransition(transition);
                activeTransition.currentStatus = Status.PRESETUP;
                activeMenu = temp;
            }

            return false;
        }

        public void updateMenu(GameTime gameTime)
        {
            activeMenu.Update(gameTime, game.Window);

            if (oldMenu != null && oldMenu.loaded && activeTransition != null && activeTransition.currentStatus == Status.DONE)
            {
                activeTransition.transition(oldMenu, activeMenu, gameTime);
                oldMenu.Unload();
                activeTransition = null;
            }

            if (activeTransition != null && activeTransition.currentStatus == Status.IDLE)
            {
                activeTransition.transition(oldMenu, activeMenu, gameTime);
            }

            if (activeTransition != null && (activeTransition.currentStatus == Status.SETUP || activeTransition.currentStatus == Status.PRESETUP || activeTransition.currentStatus == Status.RUNNING))
            {
                if (oldMenu != null) oldMenu.Update(gameTime, game.Window);
                activeTransition.transition(oldMenu, activeMenu, gameTime);
            }
        }

        public void drawMenu(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (activeTransition != null && (activeTransition.currentStatus == Status.PRESETUP || activeTransition.currentStatus == Status.SETUP))
            {
                oldMenu.Draw(gameTime, spriteBatch);
            }
            else
            {
                activeMenu.Draw(gameTime, spriteBatch);
            }

            if (activeTransition != null && activeTransition.currentStatus == Status.RUNNING)
            {
                oldMenu.Draw(gameTime, spriteBatch);
            }
        }

        private Transition getTransition(TransitionType tran)
        {
            return transitions[tran];
        }

    }
}
