using System.Windows.Forms;
using OpenGL;

namespace Tiea306
{
    public partial class Simulaattori : Form
    {
        Kappale[] kappaleet;
        public Simulaattori(Kappale[] kappaleet)
        {
            //Alustetaan kappalelista
            this.kappaleet = kappaleet;
            InitializeComponent();
        }

        private void glControl1_ContextCreated(object sender, GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;
            Gl.Enable(EnableCap.DepthTest);
            Gl.DepthFunc(DepthFunction.Always);
            Gl.MatrixMode(MatrixMode.Projection);
        }

        private void glControl1_Render(object sender, GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;
            Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.Clear(ClearBufferMask.DepthBufferBit);
            //Piirretään piste jokaisen kappaleen sijaintiin
            Gl.Begin(PrimitiveType.Points);
            
            Gl.Color3(1.0f, 1.0f, 1.0f);
            foreach(Kappale k in kappaleet)
            {
                Vertex3d s = k.Sijainti;                
                Gl.Vertex3(s.x, s.y, s.z);                
            }
            Gl.End();
            //Syvyystestausta varten, että näen milloin saan sen toimimaan. Vihreän ei pitäisi olla keskellä.
            Gl.Begin(PrimitiveType.Triangles);
            //Punainen
            Gl.Color3(1.0f, 0.0f, 0.0f);
            Gl.Vertex3(0.0f, 0.0f, 0.3f);
            Gl.Vertex3(0.5f, 1.0f, 0.3f);
            Gl.Vertex3(1.0f, 0.0f, 0.3f);
            //Vihreä
            Gl.Color3(0.0f, 1.0f, 0.0f);
            Gl.Vertex3(0.2f, 0.0f, 0.5f);
            Gl.Vertex3(0.7f, 1.0f, 0.5f);
            Gl.Vertex3(1.2f, 0.0f, 0.5f);
            //Sininen
            Gl.Color3(0.0f, 0.0f, 1.0f);
            Gl.Vertex3(0.0f, -0.2f, 0.0f);
            Gl.Vertex3(0.5f, 0.8f, 0.0f);
            Gl.Vertex3(1.0f, -0.2f, 0.0f);

            Gl.End();
        }
    }
}
