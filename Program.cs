using bonheur.UserInterface;
using SFML.Audio;
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
        public static Font boldfont = new Font("CascadiaMono-Bold.otf");
        public static Font regularfont = new Font("CascadiaMonoPL-Regular.otf");

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

        ////////////// Level 1
        private static Layer entitiesLayer = new Layer();
        private static Layer characterLayer = new Layer();
        private static float CharacterSpeed = 400;
        public static Entity character;
        private static Vector2f center;
        private static Animator CamMovement = new Animator();

        private static string[] map =
        {
            "L000000000000000000R",
            "J000000000000000000K",
            "J000000000000000000K",
            "J000000000000000000K",
            "J0000000000000FGH00K",
            "J000000000000000000K",
            "J000000000000000000K",
            "J000000000000000000K",
            "J000000000000000000K",
            "J000000000000000000K",
            "J00000000000BBB0000K",
            "J000000000000B00000K",
            "J000000000000B00000K",
            "J000000000000B00000K",
            "NBBBBBBBBBBBBBBBBBBM"
        };

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
            characterLayer = new Layer();
            characterLayer.IsActive = false;

            Title t = new Title("Start", regularfont);
            t.SetCharacterSize(17);
            button = new Button(new Vector2f(100, 500), t, new Vector2f(100, 40), ChangeState);
            app.UIs.Add(button);
            titles.Objects.Add(button);

            ///// level choice
            titlelevel = new Title("Choose the level", boldfont, new Vector2f(40, 40));
            titlelevel.SetCharacterSize(22);
            titlelevel.IsActive = false;
            titles.Objects.Add(titlelevel);

            Title t2 = new Title("F#ck go back", regularfont);
            t2.SetCharacterSize(15);
            backtomenu = new Button(new Vector2f(620, 40), t2, new Vector2f(140, 40), StateBack);
            app.UIs.Add(backtomenu);
            backtomenu.IsActive = false;
            app.UIs.Add(backtomenu);
            titles.Objects.Add(backtomenu);

            Title t3 = new Title("", regularfont);
            t3.SetCharacterSize(17);
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

            // Level 1
            app.layers.Add(entitiesLayer);
            app.layers.Add(characterLayer);
            entitiesLayer.IsActive = false;

            CamMovement.Time = 1f;
            CamMovement.To = 1;

            Texture sheet = new Texture(@"resources\TX Tileset Ground.png");

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (map[j].ToCharArray()[i] == 'L')
                    {
                        Sprite sprite = new Sprite(sheet, new IntRect(64, 0, 32, 32));
                        Entity grass = new Entity(new Vector2f(i * 64, j * 64), new Vector2f(64, 64), sprite);
                        grass.SetSpriteScale(2);
                        app.entities.Add(grass);
                        entitiesLayer.Objects.Add(grass);
                        //grass.ShowCollider = true;
                        Physics.AddStaticObject(grass);
                    }
                    else if (map[j].ToCharArray()[i] == 'J')
                    {
                        Sprite sprite = new Sprite(sheet, new IntRect(64, 32, 32, 32));
                        Entity grass = new Entity(new Vector2f(i * 64, j * 64), new Vector2f(64, 64), sprite);
                        grass.SetSpriteScale(2);
                        app.entities.Add(grass);
                        entitiesLayer.Objects.Add(grass);
                        //grass.ShowCollider = true;
                        Physics.AddStaticObject(grass);
                    }
                    else if (map[j].ToCharArray()[i] == 'R')
                    {
                        Sprite sprite = new Sprite(sheet, new IntRect(0, 0, 32, 32));
                        Entity grass = new Entity(new Vector2f(i * 64, j * 64), new Vector2f(64, 64), sprite);
                        grass.SetSpriteScale(2);
                        app.entities.Add(grass);
                        entitiesLayer.Objects.Add(grass);
                        //grass.ShowCollider = true;
                        Physics.AddStaticObject(grass);
                    }
                    else if (map[j].ToCharArray()[i] == 'K')
                    {
                        Sprite sprite = new Sprite(sheet, new IntRect(0, 32, 32, 32));
                        Entity grass = new Entity(new Vector2f(i * 64, j * 64), new Vector2f(64, 64), sprite);
                        grass.SetSpriteScale(2);
                        app.entities.Add(grass);
                        entitiesLayer.Objects.Add(grass);
                        //grass.ShowCollider = true;
                        Physics.AddStaticObject(grass);
                    }
                    else if (map[j].ToCharArray()[i] == 'F')
                    {
                        Sprite sprite = new Sprite(sheet, new IntRect(0, 384, 32, 32));
                        Entity grass = new Entity(new Vector2f(i * 64, j * 64), new Vector2f(64, 64), sprite);
                        grass.SetSpriteScale(2);
                        app.entities.Add(grass);
                        entitiesLayer.Objects.Add(grass);
                        //grass.ShowCollider = true;
                        Physics.AddStaticObject(grass);
                    }
                    else if (map[j].ToCharArray()[i] == 'G')
                    {
                        Sprite sprite = new Sprite(sheet, new IntRect(32, 384, 32, 32));
                        Entity grass = new Entity(new Vector2f(i * 64, j * 64), new Vector2f(64, 64), sprite);
                        grass.SetSpriteScale(2);
                        app.entities.Add(grass);
                        entitiesLayer.Objects.Add(grass);
                        //grass.ShowCollider = true;
                        Physics.AddStaticObject(grass);
                    }
                    else if (map[j].ToCharArray()[i] == 'H')
                    {
                        Sprite sprite = new Sprite(sheet, new IntRect(64, 384, 32, 32));
                        Entity grass = new Entity(new Vector2f(i * 64, j * 64), new Vector2f(64, 64), sprite);
                        grass.SetSpriteScale(2);
                        app.entities.Add(grass);
                        entitiesLayer.Objects.Add(grass);
                        //grass.ShowCollider = true;
                        Physics.AddStaticObject(grass);
                    }
                    else if (map[j].ToCharArray()[i] == 'N')
                    {
                        Sprite sprite = new Sprite(sheet, new IntRect(0, 192, 32, 32));
                        Entity grass = new Entity(new Vector2f(i * 64, j * 64), new Vector2f(64, 64), sprite);
                        grass.SetSpriteScale(2);
                        app.entities.Add(grass);
                        entitiesLayer.Objects.Add(grass);
                        //grass.ShowCollider = true;
                        Physics.AddStaticObject(grass);
                    }
                    else if (map[j].ToCharArray()[i] == 'B')
                    {
                        Sprite sprite = new Sprite(sheet, new IntRect(32, 0, 32, 32));
                        Entity grass = new Entity(new Vector2f(i * 64, j * 64), new Vector2f(64, 64), sprite);
                        grass.SetSpriteScale(2);
                        app.entities.Add(grass);
                        entitiesLayer.Objects.Add(grass);
                        //grass.ShowCollider = true;
                        Physics.AddStaticObject(grass);
                    }
                    else if (map[j].ToCharArray()[i] == 'M')
                    {
                        Sprite sprite = new Sprite(sheet, new IntRect(64, 192, 32, 32));
                        Entity grass = new Entity(new Vector2f(i * 64, j * 64), new Vector2f(64, 64), sprite);
                        grass.SetSpriteScale(2);
                        app.entities.Add(grass);
                        entitiesLayer.Objects.Add(grass);
                        //grass.ShowCollider = true;
                        Physics.AddStaticObject(grass);
                    }
                }
            }

            // Running scripts
            Console.WriteLine("Running scripts...");
            app.RunScripts();
            Console.WriteLine("All scripts started.");

            while (app.renderWindow.IsOpen) // Updates frames
            {
                app.Clear();

                if (currentState == CurrentState.GameWorld)
                    Physics.Update(app.deltaTime);

                center = new Vector2f(character.aabb.Position.X - app.ResX / 2 + character.aabb.Size.X, character.aabb.Position.Y - app.ResY / 2 + character.aabb.Size.Y);

                if (app.camera.Position != center)
                {
                    app.camera.Position = CamMovement.Lerp(app.camera.Position, center, app.deltaTime * 4);
                }

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
            if (character != null && app.renderWindow.HasFocus() && currentState == CurrentState.GameWorld)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                {
                    character.FlipHorizontally = true;
                    character.Velocity = new Vector2f(character.Velocity.X - CharacterSpeed, character.Velocity.Y);
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                {
                    character.FlipHorizontally = false;
                    character.Velocity = new Vector2f(character.Velocity.X + CharacterSpeed, character.Velocity.Y);
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                {
                    Jump();
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                {
                    character.Velocity = new Vector2f(character.Velocity.X, character.Velocity.Y + CharacterSpeed);
                }
            }
        }

        static Clock stepsClock = new Clock();

        static void Jump()
        {
            character.Velocity = new Vector2f(character.Velocity.X, character.Velocity.Y - 400);
        }

        /// <summary>
        /// Checking in-game triggers.
        /// </summary>
        static void CheckTriggers()
        {
            
        }

        /// <summary>
        /// Adding entity to the entities list.
        /// </summary>
        /// <param name="entity"></param>
        public static void AddEntity(Entity entity)
        {
            app.entities.Add(entity);
            entitiesLayer.Objects.Add(entity);
        }

        /// <summary>
        /// Setting character.
        /// </summary>
        /// <param name="entity"></param>
        public static void SetCharacter(Entity entity)
        {
            character = entity;
            characterLayer.Objects.Add(entity);
        }

        private static void ChangeState()
        {
            currentState = CurrentState.LevelChoice;
            title.IsActive = false;
            bottomtitle.IsActive = false;
            button.IsActive = false;
            entitiesLayer.IsActive = false;
            characterLayer.IsActive = false;

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
            titlelevel.IsActive = false;
            characterLayer.IsActive = false;
            Console.WriteLine("current state changed. now: main menu");
        }

        private static void Open1Level()
        {
            currentState = CurrentState.GameWorld;
            titlelevel.IsActive = false;
            images.IsActive = false;
            backtomenu.IsActive = false;

            entitiesLayer.IsActive = true;
            characterLayer.IsActive = true;

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