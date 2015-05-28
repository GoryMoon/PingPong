using PingPong.Menus;
using PingPong.Ui.Transitions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

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
        private String lastMenu;

        private Dictionary<String, Menu> menus;
        private Dictionary<TransitionType, Transition> transitions;

        private Stopwatch sw = new Stopwatch();

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

            setupEmptyMenu();
        }

        private void setupEmptyMenu()
        {
            registerMenu("None", new EmptyMenu(game));
            activeMenu = menus["None"];
            activeMenu.LoadContent(game.Content);
            activeMenu.PostLoadContent(game.Content);
        }

        public void registerMenu(String name, Menu menu)
        {
            Log.debug("MenuHandler: Registring menu [{0}]", name);
            menu.addHandler(this);
            menus.Add(name, menu);
        }

        public bool returnToLast(TransitionType transition)
        {
            return changeTo(lastMenu, transition);
        }

        public bool returnToLast()
        {
            return changeTo(lastMenu, TransitionType.NONE);
        }

        public bool changeTo(String name)
        {
            return changeTo(name, TransitionType.NONE);
        }

        public bool changeTo(String name, TransitionType transition) {

            if (!isTransitionRunning)
            {
                Menu temp = menus[name];

                if (temp != null && activeTransition == null)
                {
                    oldMenu = activeMenu;
                    Log.debug("MenuHandler: Changing Menu to [{0}]", name);
                    Log.debug("MenuHandler: Menu [{0}] started loading", name);

                    sw.Restart();
                    temp.LoadContent(game.Content);
                    temp.PostLoadContent(game.Content);
                    sw.Stop();
                    Log.debug("MenuHandler: Menu [{0}] loaded in {1}", name, sw.Elapsed);

                    activeTransition = getTransition(transition);
                    Log.debug("MenuHandler: Running transition {0}", transition.ToString());
                    activeTransition.currentStatus = Status.PRESETUP;
                    activeMenu = temp;
                }
            }

            return false;
        }

        public void updateMenu(GameTime gameTime)
        {
            activeMenu.Update(gameTime, game.Window);

            if (oldMenu != null && oldMenu.loaded && activeTransition != null && activeTransition.currentStatus == Status.DONE)
            {
                activeTransition.transition(oldMenu, activeMenu, gameTime);

                lastMenu = menus.FirstOrDefault(x => x.Value == oldMenu).Key;
                Log.debug("MenuHandler: Menu [{0}] is unloading", oldMenu.name);
                oldMenu.Unload();

                Log.debug("MenuHandler: Done running transition {0}", transitions.FirstOrDefault(x => x.Value == activeTransition).Key.ToString());
                activeTransition = null;
                activeMenu.transitionDone();
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
                if (oldMenu != null) oldMenu.Draw(gameTime, spriteBatch);
            }
            else
            {
                activeMenu.Draw(gameTime, spriteBatch);
            }

            if (activeTransition != null && activeTransition.currentStatus == Status.RUNNING)
            {
                if (oldMenu != null) oldMenu.Draw(gameTime, spriteBatch);
            }
        }

        private Transition getTransition(TransitionType tran)
        {
            return transitions[tran];
        }

        public float WindowWidth { get { return Game1.WindowWidth; } }
        public float WindowHeight { get { return Game1.WindowHeight; } }

        public bool isTransitionRunning { get { return activeTransition != null && (activeTransition.currentStatus == Status.RUNNING || activeTransition.currentStatus == Status.PRESETUP  || activeTransition.currentStatus == Status.SETUP || activeTransition.currentStatus == Status.IDLE  || activeTransition.currentStatus == Status.DONE); } }
        public bool canBeUnPaused { get { return activeMenu.canUnPause; } }
    }
}
