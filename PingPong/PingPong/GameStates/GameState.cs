using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameObjects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong.GameStates
{
    public abstract class GameState
    {

        public GameStateHandler handler;
        public String name;
        protected Dictionary<String, Property> properties;
        public Property[] props;

        public GameState(String name)
        {
            this.name = name;
            properties = new Dictionary<string,Property>();
        }

        public void initState()
        {
            init();
        }
        public abstract void init();
        public abstract void update(GameTime gameTime);
        public abstract void draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void unload();

        public void addHandler(GameStateHandler handler)
        {
            this.handler = handler;
        }

        public void add(Property prop)
        {
            properties.Add(prop.name, prop);
        }

        public T get<T>(String name)
        {
            return getProp<T>(name).get();
        }

        public Property<T> getProp<T>(String name)
        {
            return get(name) as Property<T>;
        }

        public Property get(String name)
        {
            return properties[name];
        }

        public void set<T>(String name, T value)
        {
            if (properties.ContainsKey(name) && !(value is Property))
            {
                getProp<T>(name).set(value);
            }
            else if (properties.ContainsKey(name))
            {
                properties[name] = value as Property;
            }
            
        }

        public void transfer(GameState state)
        {
            foreach (KeyValuePair<String, Property> item in properties)
            {
                if (state.properties.ContainsKey(item.Key))
                {
                    state.set(item.Key, item.Value);
                }
            }

            properties.Clear();
            properties = null;

        }

        public ContentManager Content { get { return handler.game.Content; } }
        public GameWindow Window { get { return handler.game.Window; } }

    }
}
