using bonheur.UserInterface;
using Microsoft.CodeAnalysis.FlowAnalysis;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static System.Net.Mime.MediaTypeNames;

namespace bonheur
{
    public enum CurrentState
    {
        MainMenu,
        LevelChoice,
        GameWorld
    }
    public class Program
    {
        private static App app = new App();

        public static CurrentState currentState = CurrentState.MainMenu;
        public static Font boldfont = new Font("intelone-mono-font-family-bold.otf");
        public static Font regularfont = new Font("intelone-mono-font-family-regular.otf");

        public static bool changed = false;

        ////////////// Main Menu
        private static Title title = new Title();
        private static Title bottomtitle = new Title();
        private static Button button = new Button();

        ////////////// Level Choice
        private static Title titlelevel = new Title();
        private static Sprite lvl1 = new Sprite();
        private static Sprite lvl2 = new Sprite();
        private static Sprite lvl3 = new Sprite();
        private static Layer images = new Layer();
        private static Button backtomenu = new Button();

        private static Button level1 = new Button();
        private static Button level2 = new Button();
        private static Button level3 = new Button();

        static void Main(string[] args)
        {
            ////// main menu
            title = new Title("bonheur", boldfont, new Vector2f(100, 80));
            bottomtitle = new Title("prototype", regularfont, new Vector2f(100, 164));

            title.SetCharacterSize(76);
            bottomtitle.SetCharacterSize(28);

            //Layers
            Layer titles = new Layer();
            app.layers.Add(titles);
            titles.Objects.Add(title);
            titles.Objects.Add(bottomtitle);
            images = new Layer();
            app.layers.Add(images);

            Title t = new Title("Start", regularfont);
            t.SetCharacterSize(16);
            button = new Button(new Vector2f(100, 500), t, new Vector2f(100, 40), ChangeState);
            app.UIs.Add(button);
            titles.Objects.Add(button);

            ///// level choice
            titlelevel = new Title("Choose the level", boldfont, new Vector2f(40, 40));
            titlelevel.SetCharacterSize(22);
            titlelevel.IsActive = false;
            titles.Objects.Add(titlelevel);

            Title t2 = new Title("F#ck go back", regularfont);
            t2.SetCharacterSize(16);
            backtomenu = new Button(new Vector2f(620, 40), t2, new Vector2f(140, 40), StateBack);
            app.UIs.Add(backtomenu);
            backtomenu.IsActive = false;
            app.UIs.Add(backtomenu);
            titles.Objects.Add(backtomenu);

            Title t3 = new Title("", regularfont);
            t3.SetCharacterSize(16);
            level1 = new Button(new Vector2f(40, 90), t3, new Vector2f(120, 120), Open1Level);
            level1.IsActive = false;
            app.UIs.Add(level1);
            titles.Objects.Add(level1);

            Title t4 = new Title("", regularfont);
            t4.SetCharacterSize(16);
            level2 = new Button(new Vector2f(180, 90), t3, new Vector2f(120, 120), Open1Level);
            level2.IsActive = false;
            app.UIs.Add(level2);
            titles.Objects.Add(level2);

            Title t5 = new Title("", regularfont);
            t5.SetCharacterSize(16);
            level3 = new Button(new Vector2f(320, 90), t3, new Vector2f(120, 120), Open1Level);
            level3.IsActive = false;
            app.UIs.Add(level3);
            titles.Objects.Add(level3);

            lvl1 = new Sprite(new Texture(@"resources\placeholder.png"));
            lvl1.Position = new Vector2f(40, 90);
            images.Objects.Add(lvl1);
            lvl2 = new Sprite(new Texture(@"resources\placeholder.png"));
            lvl2.Position = new Vector2f(180, 90);
            images.Objects.Add(lvl2);
            lvl3 = new Sprite(new Texture(@"resources\placeholder.png"));
            lvl3.Position = new Vector2f(320, 90);
            images.Objects.Add(lvl3);
            images.IsActive = false;

            // Running scripts
            Console.WriteLine("Running scripts...");
            app.RunScripts();
            Console.WriteLine("All scripts started.");

            while (app.renderWindow.IsOpen) // Updates frames
            {
                app.Clear();

                CheckTriggers();

                Input();

                app.Display();
            }
        }

        /// <summary>
        /// Setting app title.
        /// </summary>
        /// <param name="title">New app title.</param>
        public static void SetTitle(string title)
        {
            app.renderWindow.SetTitle(title);
        }

        /// <summary>
        /// Checking inputs.
        /// </summary>
        static void Input()
        {
            
        }

        /// <summary>
        /// Checking in-game triggers
        /// </summary>
        static void CheckTriggers()
        {

        }

        private static void ChangeState()
        {
            currentState = CurrentState.LevelChoice;
            title.IsActive = false;
            bottomtitle.IsActive = false;
            button.IsActive = false;

            level1.IsActive = true;
            level2.IsActive = true;
            level3.IsActive = true;
            titlelevel.IsActive = true;
            images.IsActive = true;
            backtomenu.IsActive = true;
            Console.WriteLine("current state changed. now: level choice");
        }

        private static void StateBack()
        {
            currentState = CurrentState.MainMenu;
            title.IsActive = true;
            bottomtitle.IsActive = true;
            button.IsActive = true;

            titlelevel.IsActive = false;
            images.IsActive = false;
            backtomenu.IsActive = false;
            level1.IsActive = false;
            level2.IsActive = false;
            level3.IsActive = false;
            Console.WriteLine("current state changed. now: main menu");
        }

        private static void Open1Level()
        {
            currentState = CurrentState.GameWorld;
            titlelevel.IsActive = false;
            images.IsActive = false;
            backtomenu.IsActive = false;

            titlelevel.IsActive = false;
            images.IsActive = false;
            backtomenu.IsActive = false;
            level1.IsActive = false;
            level2.IsActive = false;
            level3.IsActive = false;
            Console.WriteLine("current state changed. now: first level");
        }
    }
}