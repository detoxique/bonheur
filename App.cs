using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.IO;
using bonheur.UserInterface;

namespace bonheur
{
    class App
    {
        public RenderWindow renderWindow;
        public Clock deltaTimeClock = new Clock();
        public float deltaTime, frames = 0, fps = 0;
        private Clock FPSclock = new Clock();

        public List<Script> scripts = new List<Script>();
        public List<Layer> layers = new List<Layer>();
        public List<UI> UIs = new List<UI>();
        public List<Entity> entities = new List<Entity>();

        public Color Background = new Color(99, 19, 237);

        public App()
        {
            renderWindow = new RenderWindow(new VideoMode(800, 600), "App", Styles.Default, new ContextSettings(1, 0, 4));
            renderWindow.SetFramerateLimit(145);
            Initialize();
        }

        public App(uint width, uint height)
        {
            renderWindow = new RenderWindow(new VideoMode(width, height), "App", Styles.Default, new ContextSettings(1, 0, 4));
            renderWindow.SetFramerateLimit(145);
            Initialize();
        }

        public App(uint width, uint height, string title)
        {
            renderWindow = new RenderWindow(new VideoMode(width, height), title, Styles.Default, new ContextSettings(1, 0, 4));
            renderWindow.SetFramerateLimit(145);
            Initialize();
        }

        public void Initialize()
        {
            // Loading scripts
            try
            {
                foreach (string file in Directory.GetFiles(@"scripts\"))
                {
                    scripts.Add(new Script(file));
                    Console.WriteLine(file);
                }
                Console.WriteLine("All scripts loaded!");
            }
            catch
            {
                Console.WriteLine("Scripts loading error");
            }

            // Events
            renderWindow.Closed += RenderWindow_Closed;
            renderWindow.KeyPressed += RenderWindow_KeyPressed;
            renderWindow.MouseButtonPressed += RenderWindow_MouseButtonPressed;
            renderWindow.MouseButtonReleased += RenderWindow_MouseButtonReleased;
            renderWindow.MouseMoved += RenderWindow_MouseMoved;
            renderWindow.MouseWheelScrolled += RenderWindow_MouseWheelScrolled;
            renderWindow.TextEntered += RenderWindow_TextEntered;
            renderWindow.Resized += RenderWindow_Resized;
            renderWindow.KeyReleased += RenderWindow_KeyReleased;

            Clear();
            Display();
        }

        public void Clear()
        {
            deltaTime = deltaTimeClock.ElapsedTime.AsSeconds();
            deltaTimeClock.Restart();
            renderWindow.DispatchEvents();
            renderWindow.Clear(Background);

            UpdateUI();
            //RunScripts();

            foreach (Entity entity in entities)
            {
                entity.Update(deltaTime);
            }
        }

        public void UpdateUI()
        {
            foreach (UI e in UIs)
            {
                e.Update(deltaTime);
            }
        }

        public void RunScripts()
        {
            foreach (Script script in scripts)
                try
                {
                    script.Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
        }

        public void Display()
        {
            foreach (Layer layer in layers)
            {
                renderWindow.Draw(layer);
            }

            renderWindow.Display();

            frames++;
            if (FPSclock.ElapsedTime.AsSeconds() >= 1)
            {
                FPSclock.Restart();
                fps = frames;
                //Console.WriteLine("FPS: " + fps);
                frames = 0;
            }
        }

        private void RenderWindow_KeyReleased(object? sender, KeyEventArgs e)
        {
            
        }

        private void RenderWindow_Resized(object? sender, SizeEventArgs e)
        {
            
        }

        private void RenderWindow_TextEntered(object? sender, TextEventArgs e)
        {
            foreach (UI el in UIs)
            {
                el.TextEntered(e);
            }
        }

        private void RenderWindow_MouseWheelScrolled(object? sender, MouseWheelScrollEventArgs e)
        {
            foreach (UI el in UIs)
            {
                el.MouseWheel(e.Delta);
            }
        }

        private void RenderWindow_MouseMoved(object? sender, MouseMoveEventArgs e)
        {
            foreach (UI el in UIs)
            {
                el.MouseCheck(e);
            }
        }

        private void RenderWindow_MouseButtonReleased(object? sender, MouseButtonEventArgs e)
        {
            foreach (UI el in UIs)
            {
                el.MouseRelease(e);
            }
        }

        private void RenderWindow_MouseButtonPressed(object? sender, MouseButtonEventArgs e)
        {
            foreach (UI el in UIs)
            {
                el.MouseClick(e);
            }
        }

        private void RenderWindow_KeyPressed(object? sender, KeyEventArgs e)
        {
            foreach (UI el in UIs)
            {
                el.KeyPressed(e);
            }
        }

        private void RenderWindow_Closed(object? sender, EventArgs e)
        {
            renderWindow.Close();
        }
    }
}
