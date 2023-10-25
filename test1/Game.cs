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

        // public CameraInput test;

        ImGuiController UIController;

        // List<string[]> logData = new List<string[]>();


        // Stopwatch timer;
        // Shader? shader;

        // public static float[] vertices =
        // {
        //     //Position          Texture coordinates
        //     1.0f,  1.0f, 0.0f, 1.0f, 1.0f, // top right
        //     1.0f, -1.0f, 0.0f, 1.0f, 0.0f, // bottom right
        //     -1.0f, -1.0f, 0.0f, 0.0f, 0.0f, // bottom left
        //     -1.0f,  1.0f, 0.0f, 0.0f, 1.0f  // top left
        // };

        // public static uint[] indices =
        // {
        //     0, 1, 3,
        //     1, 2, 3
        // };

        // int VertexArrayObject;
        // int elementBufferObject;
        // int vertexBufferObject;

        // public Texture texture;

        // private Mat currentImage;

        Engine engine;


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

            // log("DEBUG", "message1");
            // log("DEBUG", "message2");
            // log("DEBUG", "message3");
            // log("DEBUG", "message4");

            // this.test = new CameraInput(1);
            // test.captureImage();

            // String imagePath = @"webcam.jpg";
            // currentImage = CvInvoke.Imread(imagePath, Emgu.CV.CvEnum.ImreadModes.AnyColor);

            // texture = Texture.LoadFromFile(imagePath);

            // OCCTProxy myOCCTProxy = new OCCTProxy();
            // if (!myOCCTProxy.InitGLViewer())
            // {
            //     Console.WriteLine("error during init");
            // } else {
            //     Console.WriteLine("OCCT GL Initialized");
            // }

            // this.timer = new Stopwatch();
            // this.timer.Start();

            string frag = @"C:\Users\wes\github-repos\dotnet_opentk_base\WS_ENGINE_BASE\shader.frag";
            string vert = @"C:\Users\wes\github-repos\dotnet_opentk_base\WS_ENGINE_BASE\shader.vert";

            engine = new Engine(width, height, Size, vert, frag);
        }


        // public void ImageCallbackDelegate(Mat image)
        // {

        //     this.currentImage = image;

        //     shader.Dispose();
        //     this.GenFBONew(WindowWidth, WindowHeight);

        // }

        // public void log(string level, string message)
        // {
        //     this.logData.Add(new string[] {level, DateTime.Now.ToString("HH:mm:ss"), message});
        // }

        

        


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            // if (shader != null)
            // {
            //     if (captureLive)
            //     {
            //         this.capture();
            //     }

                
                
                


                

                // texture.Use(TextureUnit.Texture0);
                // shader.Use();
                // GL.BindVertexArray(VertexArrayObject);
                // GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
                


                //GL.Disable(EnableCap.DepthTest);
                

                // GL.DeleteVertexArray(VertexArrayObject);

                // VertexArrayObject = GL.GenVertexArray();
                // GL.BindVertexArray(VertexArrayObject);

                // GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
                // GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
                // // 3. then set our vertex attributes pointers
                // //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

                //     var vertexLocation = shader.GetAttribLocation("aPosition");
                //     GL.EnableVertexAttribArray(vertexLocation);
                //     GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

                //     int texCoordLocation = shader.GetAttribLocation("aTexCoord");
                //     GL.EnableVertexAttribArray(texCoordLocation);
                //     GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));


                // GL.EnableVertexAttribArray(0);

                // double timeValue = timer.Elapsed.TotalMilliseconds / 30;
                // float mod = (float)Math.Sin(timeValue) / 2.0f + 0.5f;

                // int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "vertexColor");
                // GL.Uniform4(vertexColorLocation, 
                //     colorPicked.X * mod,
                //     colorPicked.Y * mod,
                //     colorPicked.Z * mod,
                //     colorPicked.W * mod);

                // GL.ActiveTexture(TextureUnit.Texture0);
                // GL.BindTexture(TextureTarget.Texture2D, framebufferTexture);
                

                //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

                
            // }


            
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

            

            //GL.Clear(ClearBufferMask.ColorBufferBit);

            

            

                      
            
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

            this.VSync = VSyncMode.On;
            this.IsVisible = true;

            this.GenFBO(WindowWidth, WindowHeight);
            // FBO = GL.GenFramebuffer();
            // GL.BindFramebuffer(FramebufferTarget.Framebuffer, FBO);
            // framebufferTexture = GL.GenTexture();
            // GL.BindTexture(TextureTarget.Texture2D, framebufferTexture);

            

            // GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, framebufferTexture, 0);
            // GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            base.OnLoad();

            

            
        }

        protected override void OnUnload()
        {
            // if (shader != null)
            // {
            //     shader.Dispose();
            // }

            

            engine.Unload();

            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            WindowWidth = e.Width;
            WindowHeight = e.Height;

            // GL.DeleteFramebuffer(FBO);
            // GenFBO(WindowWidth, WindowHeight);

            UIController.WindowResized((int)WindowWidth, (int)WindowHeight);

            // GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);

            engine.Resize(e);
        }


        public int FBO; //RBO;
        public int framebufferTexture;

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
            
            // 3. then set our vertex attributes pointers
            // GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            // GL.EnableVertexAttribArray(0);

            


            
            // Attach color to FBO
             GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, framebufferTexture, 0);

            



            var fboStatus = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (fboStatus != FramebufferErrorCode.FramebufferComplete)
            {
                Console.WriteLine("Framebuffer error: " + fboStatus);
            }
        }


        // public void GenFBONew(float CamWidth, float CamHeight)
        // {
        //     FBO = GL.GenFramebuffer();
        //     GL.BindFramebuffer(FramebufferTarget.Framebuffer, FBO);

        //     // Color Texture
        //     framebufferTexture = GL.GenTexture();
        //     GL.BindTexture(TextureTarget.Texture2D, framebufferTexture);
        //     GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb16f, (int)CamWidth, (int)CamHeight, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);
        //     GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        //     GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);
        //     GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        //     GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);



        //     GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        //     VertexArrayObject = GL.GenVertexArray();
        //     GL.BindVertexArray(VertexArrayObject);

        //         vertexBufferObject = GL.GenBuffer();
        //         GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        //         GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        //         elementBufferObject = GL.GenBuffer();
        //         GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        //         GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

        //     shader = new Shader("shader.vert", "shader.frag");

        //     var vertexLocation = shader.GetAttribLocation("aPosition");
        //     GL.EnableVertexAttribArray(vertexLocation);
        //     GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

        //     var texCoordLocation = shader.GetAttribLocation("aTexCoord");
        //     GL.EnableVertexAttribArray(texCoordLocation);
        //     GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

        //     // GCHandle handle1 = GCHandle.Alloc(currentImage);
        //     // IntPtr parameter = (IntPtr) handle1;
            
        //     var buffer = new VectorOfByte();

        //     CvInvoke.Imencode(".jpg", currentImage, buffer);

        //     byte[] image = buffer.ToArray();

        //     texture = Texture.LoadFromMemory(image, currentImage.Width, currentImage.Height);

        //     // handle1.Free();

        //     texture.Use(TextureUnit.Texture0);
            
        //     // 3. then set our vertex attributes pointers
        //     // GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        //     // GL.EnableVertexAttribArray(0);

            


            
        //     // Attach color to FBO
        //      GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, framebufferTexture, 0);

            



        //     var fboStatus = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
        //     if (fboStatus != FramebufferErrorCode.FramebufferComplete)
        //     {
        //         Console.WriteLine("Framebuffer error: " + fboStatus);
        //     }
        // }


        // int counter = 0;

        // public static int a = 0;
        // public static int b = 0;

        // public VideoCapture camera;

        // public static bool captureLive = false;

        // public void capture()
        // {
        //     test.captureImage();
        //     Mat? current = test.getImage();

        //     if (current != null)
        //     {
        //         this.currentImage = current;
        //     }

            
        //     //Console.WriteLine("Capture");

        //     //var filename = "webcam.jpg";
        //     // using 

        //     // var image = capture.QueryFrame(); //take a picture


        //     // Mat imgGrayscale = new Mat(image.Size, DepthType.Cv8U, 1);
        //     // Mat imgBlurred = new Mat(image.Size, DepthType.Cv8U, 1);
        //     // Mat imgCanny = new Mat(image.Size, DepthType.Cv8U, 1);

        //     // CvInvoke.CvtColor(image, imgGrayscale, ColorConversion.Bgr2Gray);

        //     // CvInvoke.GaussianBlur(imgGrayscale, imgBlurred, new Size(5, 5), 1.5);

        //     // CvInvoke.Canny(imgBlurred, imgCanny, a, b);

        //     // currentImage = imgCanny;
        //     //image.Save(filename);

        //     shader.Dispose();
            

        //     // this.GenFBONew(WindowWidth, WindowHeight);

        // }

        public float camWidth = 800f;
        public float camHeight = 600f;

        // public static float angle = 0.0f;
        // public static System.Numerics.Vector4 colorPicked = new System.Numerics.Vector4(0.0f);
    }
}
