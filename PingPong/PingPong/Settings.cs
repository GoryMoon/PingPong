using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.GameObjects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace PingPong
{
    public class Settings
    {
        string path;
        string dirPath;

        public Dictionary<String, String> props;

        public Settings(string settingsName, string path)
        {
            this.dirPath = Path.Combine(path, Path.GetDirectoryName(settingsName));
            this.path = Path.Combine(path, settingsName);
            props = new Dictionary<String, String>();
            load();
            addDefaultSettings();
        }

        public Settings(string settingsName)
            : this(settingsName, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData))
        {

        }

        public void load()
        {
            if (!File.Exists(path))
            {
                createDefaultSettings();
            }

            string textData = File.ReadAllText(path);

            if (textData != null && textData != "")
            {
                string[] data = textData.Split(',');

                if (data.Length > 0)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        string[] propData = data[i].Split(':');

                        if (propData.Length > 0 && propData.Length < 3)
                        {
                            add(propData[0], propData[1]);
                            Log.debug("Settings: {0} {1}", propData[0], propData[1]);
                        }
                    }
                }
            }
            else
            {
                createDefaultSettings();
            }
        }

        public void addDefaultSettings()
        {
            add("P1-U", (int)Keys.W);
            add("P1-D", (int)Keys.S);
            add("P2-U", (int)Keys.Up);
            add("P2-D", (int)Keys.Down);
            add("fullscreen", false);
            add("res", "1280x720");
        }

        public void createDefaultSettings()
        {
            addDefaultSettings();
            try
            {
                Directory.CreateDirectory(dirPath);
            }
            catch (Exception e)
            {
                throw;
            }

            save();
        }

        public void save()
        {
            String save = "";
            int i = 0;

            foreach (KeyValuePair<String, String> pair in props)
            {
                save += (i++ > 0 ? "," : "") + pair.Key + ":" + pair.Value;
            }

            File.WriteAllText(path, save);
        }

        public void add(String name, object prop)
        {
            if (!props.ContainsKey(name))
            {
                props.Add(name, prop.ToString());   
            }
        }

        public void set(String name, object prop)
        {
            props[name] = prop.ToString();
        }

        public String get(String name)
        {
            return props[name];
        }

        public Keys getKey(String name)
        {
            return ((Keys)Enum.Parse(typeof(Keys), get(name), false));
        }

        public Point getResolution(String name)
        {
            string[] vals = get(name).Split('x');

            if (vals.Length < 2)
	        {
                createDefaultSettings();
                return getResolution(name);
	        }

            return new Point(Convert.ToInt32(vals[0]), Convert.ToInt32(vals[1]));
        }

    }
}
