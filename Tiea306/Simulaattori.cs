using System.Windows.Forms;
using OpenGL;

namespace Tiea306
{
    public partial class Simulaattori : Form
    {
        double valovuosi = 63239.7263;
        Kappale[] kappaleet;
        public Simulaattori(Kappale[] kappaleet)
        {
            //Alustetaan kappalelista
            this.kappaleet = kappaleet;
            InitializeComponent();
        }

        private void glControl1_ContextCreated(object sender, GlControlEventArgs e)
        {
            //Oletan että täällä pitäisi tehdä jotain että saisi syvyystestauksen toimimaan
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
                Gl.Vertex3(norm(s)); 
            }
            Gl.End();

            //Syvyystestausta varten, että näen milloin saan sen toimimaan. Vihreän ei pitäisi olla keskellä.
            /*
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
            */

            //Varsinainen laskenta.
            Suora_Laskenta sl = new Suora_Laskenta();
            sl.päivitä(kappaleet);           
        }

        /// <summary>
        /// Skaalaa annetun pisteen niin että se sijoittuu simulaation alueelle.
        /// </summary>
        /// <param name="a">Piste jota skaalataan</param>
        /// <returns></returns>
        private Vertex3d norm(Vertex3d a)
        {
            //TODO: aseta ottamaan kertoimet asetuksista
            double x = (2 * (a.x + valovuosi * 10) / (valovuosi * 10 + valovuosi * 10)) - 1;
            double y = (2 * (a.y + valovuosi * 10) / (valovuosi * 10 + valovuosi * 10)) - 1;
            double z = (2 * (a.z + valovuosi * 10) / (valovuosi * 10 + valovuosi * 10)) - 1;
            return new Vertex3d(x, y, z);
        }
    }
}
