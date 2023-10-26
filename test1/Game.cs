using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;

using ImGuiNET;
using System.Diagnostics;

using Emgu.CV;
using System.Runtime.InteropServices;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using System.Drawing;


using WS_ENGINE_BASE;

namespace testOne {
    public class Game : GameWindow {

        public static float WindowWidth;
        public static float WindowHeight;
        public static float CameraWidth;
        public static float CameraHeight;

        public float camWidth = 800f;
        public float camHeight = 600f;

        public int FBO;
        public int framebufferTexture;

        ImGuiController UIController;

        public static Engine engine;


        public Game(int width, int height, string title, string fontPath, float fontSize)
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            {
                Title = title,
                Size = new Vector2i(width, height),
                WindowBorder = WindowBorder.Resizable,
                StartVisible = false,
                StartFocused = true,
                WindowState = WindowState.Normal,
                API = ContextAPI.OpenGL,
                Profile = ContextProfile.Core,
                APIVersion = new Version(3, 3)
            })
        {
            // Center the window
            this.CenterWindow();
            WindowHeight = Size.Y;
            WindowWidth = Size.X;
            CameraHeight = Size.Y;
            CameraWidth = Size.X;

            UIController = new ImGuiController((int)WindowWidth, (int)WindowHeight, fontPath, fontSize);

            string frag = @"..\..\dotnet_opentk_base\WS_ENGINE_BASE\shader.frag";
            string vert = @"..\..\dotnet_opentk_base\WS_ENGINE_BASE\shader.vert";

            engine = new Engine(width, height, Size, vert, frag);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FBO);
            engine.RenderFrame(args);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            UIController.Update(this, (float)args.Time);
            ImGui.DockSpaceOverViewport();
            GUI.WindowOnOffs();
            GUI.LoadOCCTWindow(ref camWidth, ref camHeight, ref framebufferTexture);
            UIController.Render();
            ImGuiController.CheckGLError("End of frame");

            SwapBuffers();
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            int status = -2;
            engine.UpdateFrame(e, KeyboardState, MouseState, IsFocused, Size, ref status);

            if (status == -1)
            {
                Close();
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            engine.MouseWheel(e, KeyboardState);
        }

        protected override void OnLoad()
        {
            //GUI.LoadTheme();
            base.OnLoad();

            this.VSync = VSyncMode.On;
            this.IsVisible = true;

            this.GenFBO(WindowWidth, WindowHeight);
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            engine.Unload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            WindowWidth = e.Width;
            WindowHeight = e.Height;

            UIController.WindowResized((int)WindowWidth, (int)WindowHeight);

            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);

            engine.Resize(e);
        }

        public void GenFBO(float CamWidth, float CamHeight)
        {
            FBO = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FBO);

            // Color Texture
            framebufferTexture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, framebufferTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb16f, (int)CamWidth, (int)CamHeight, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);



            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            engine.Load();
            
            // Attach color to FBO
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, framebufferTexture, 0);

            var fboStatus = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (fboStatus != FramebufferErrorCode.FramebufferComplete)
            {
                Console.WriteLine("Framebuffer error: " + fboStatus);
            }
        }

        
    }
}
