namespace Graphical
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Chessty;
    using Chessty.Abstract;
    using Chessty.Enumeration;
    using Chessty.Movement;
    using Chessty.Pieces;
    using Chessty.Pieces.Black;
    using Chessty.Pieces.White;
    using Chessty.Structure;
    using Graphical.Properties;
    using Board;
    public partial class ChessWindowForm : Form
    {
        private Dictionary<Tuple<int, int, Color>, PictureBox> relations = new Dictionary<Tuple<int, int, Color>, PictureBox>();

        private readonly Dictionary<Tuple<int, int, Color>, PictureBox> relationsWhite = new Dictionary<Tuple<int, int, Color>, PictureBox>();
        private readonly Dictionary<Tuple<int, int, Color>, PictureBox> relationsBlack = new Dictionary<Tuple<int, int, Color>, PictureBox>();

        private readonly string path = AppDomain.CurrentDomain.BaseDirectory;

        private delegate Move MoveDelegate(Ply nextMove);
        private Dictionary<PieceIdentifiers, MoveDelegate> MovesTable = new Dictionary<PieceIdentifiers, MoveDelegate>();


        private Game Game { get; set; }

        private SquareUI SquareSelected { get; set; }
        private SquareUI PreviousLastSquareMoved { get; set; }

        #region Board click handlers

        private void pictureBox32_Click(object sender, EventArgs e)
        {
            TapSquare(0, 4);
        }
        private void pictureBox31_Click(object sender, EventArgs e)
        {
            TapSquare(1, 4);
        }
        private void pictureBox30_Click(object sender, EventArgs e)
        {
            TapSquare(2, 4);
        }
        private void pictureBox29_Click(object sender, EventArgs e)
        {
            TapSquare(3, 4);
        }
        private void pictureBox28_Click(object sender, EventArgs e)
        {
            TapSquare(4, 4);
        }
        private void pictureBox27_Click(object sender, EventArgs e)
        {
            TapSquare(5, 4);
        }
        private void pictureBox26_Click(object sender, EventArgs e)
        {
            TapSquare(6, 4);
        }
        private void pictureBox25_Click(object sender, EventArgs e)
        {
            TapSquare(7, 4);
        }
        private void pictureBox40_Click(object sender, EventArgs e)
        {
            TapSquare(0, 3);
        }
        private void pictureBox39_Click(object sender, EventArgs e)
        {
            TapSquare(1, 3);
        }
        private void pictureBox38_Click(object sender, EventArgs e)
        {
            TapSquare(2, 3);
        }
        private void pictureBox37_Click(object sender, EventArgs e)
        {
            TapSquare(3, 3);
        }
        private void pictureBox36_Click(object sender, EventArgs e)
        {
            TapSquare(4, 3);
        }
        private void pictureBox35_Click(object sender, EventArgs e)
        {
            TapSquare(5, 3);
        }
        private void pictureBox34_Click(object sender, EventArgs e)
        {
            TapSquare(6, 3);
        }
        private void pictureBox33_Click(object sender, EventArgs e)
        {
            TapSquare(7, 3);
        }
        private void pictureBox48_Click(object sender, EventArgs e)
        {
            TapSquare(0, 2);
        }
        private void pictureBox47_Click(object sender, EventArgs e)
        {
            TapSquare(1, 2);
        }
        private void pictureBox46_Click(object sender, EventArgs e)
        {
            TapSquare(2, 2);
        }
        private void pictureBox45_Click(object sender, EventArgs e)
        {
            TapSquare(3, 2);
        }
        private void pictureBox44_Click(object sender, EventArgs e)
        {
            TapSquare(4, 2);
        }
        private void pictureBox43_Click(object sender, EventArgs e)
        {
            TapSquare(5, 2);
        }
        private void pictureBox42_Click(object sender, EventArgs e)
        {
            TapSquare(6, 2);
        }
        private void pictureBox41_Click(object sender, EventArgs e)
        {
            TapSquare(7, 2);
        }
        private void pictureBox56_Click(object sender, EventArgs e)
        {
            TapSquare(0, 1);
        }
        private void pictureBox55_Click(object sender, EventArgs e)
        {
            TapSquare(1, 1);
        }
        private void pictureBox54_Click(object sender, EventArgs e)
        {
            TapSquare(2, 1);
        }
        private void pictureBox53_Click(object sender, EventArgs e)
        {
            TapSquare(3, 1);
        }
        private void pictureBox52_Click(object sender, EventArgs e)
        {
            TapSquare(4, 1);
        }
        private void pictureBox51_Click(object sender, EventArgs e)
        {
            TapSquare(5, 1);
        }
        private void pictureBox50_Click(object sender, EventArgs e)
        {
            TapSquare(6, 1);
        }
        private void pictureBox49_Click(object sender, EventArgs e)
        {
            TapSquare(7, 1);
        }
        private void pictureBox64_Click(object sender, EventArgs e)
        {
            TapSquare(0, 0);
        }
        private void pictureBox63_Click(object sender, EventArgs e)
        {
            TapSquare(1, 0);
        }
        private void pictureBox62_Click(object sender, EventArgs e)
        {
            TapSquare(2, 0);
        }
        private void pictureBox61_Click(object sender, EventArgs e)
        {
            TapSquare(3, 0);
        }
        private void pictureBox60_Click(object sender, EventArgs e)
        {
            TapSquare(4, 0);
        }
        private void pictureBox59_Click(object sender, EventArgs e)
        {
            TapSquare(5, 0);
        }
        private void pictureBox58_Click(object sender, EventArgs e)
        {
            TapSquare(6, 0);
        }
        private void pictureBox57_Click(object sender, EventArgs e)
        {
            TapSquare(7, 0);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            TapSquare(0, 7);
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            TapSquare(1, 7);
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            TapSquare(2, 7);
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            TapSquare(3, 7);
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            TapSquare(4, 7);
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            TapSquare(5, 7);
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            TapSquare(6, 7);
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            TapSquare(7, 7);
        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            TapSquare(7, 6);
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            TapSquare(6, 6);
        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            TapSquare(5, 6);
        }
        private void pictureBox12_Click(object sender, EventArgs e)
        {
            TapSquare(4, 6);
        }
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            TapSquare(3, 6);
        }
        private void pictureBox14_Click(object sender, EventArgs e)
        {
            TapSquare(2, 6);
        }
        private void pictureBox15_Click(object sender, EventArgs e)
        {
            TapSquare(1, 6);
        }
        private void pictureBox16_Click(object sender, EventArgs e)
        {
            TapSquare(0, 6);
        }
        private void pictureBox17_Click(object sender, EventArgs e)
        {
            TapSquare(7, 5);
        }
        private void pictureBox18_Click(object sender, EventArgs e)
        {
            TapSquare(6, 5);
        }
        private void pictureBox19_Click(object sender, EventArgs e)
        {
            TapSquare(5, 5);
        }
        private void pictureBox20_Click(object sender, EventArgs e)
        {
            TapSquare(4, 5);
        }
        private void pictureBox21_Click(object sender, EventArgs e)
        {
            TapSquare(3, 5);
        }
        private void pictureBox22_Click(object sender, EventArgs e)
        {
            TapSquare(2, 5);
        }
        private void pictureBox23_Click(object sender, EventArgs e)
        {
            TapSquare(1, 5);
        }
        private void pictureBox24_Click(object sender, EventArgs e)
        {
            TapSquare(0, 5);
        }

        #endregion

        public ChessWindowForm()
        {
            this.InitializeComponent();

            this.LoadGraphicalComponents();
            this.LoadOpeningsIntoGame();
            this.LoadInitialSettings();
            this.LoadMovesTable();

            this.Draw();
        }

        private void LoadGraphicalComponents()
        {
            #region Graphical relations
            this.relationsWhite.Add(new Tuple<int, int, Color>(0, 7, pictureBox1.BackColor), pictureBox1);
            this.relationsWhite.Add(new Tuple<int, int, Color>(1, 7, pictureBox2.BackColor), pictureBox2);
            this.relationsWhite.Add(new Tuple<int, int, Color>(2, 7, pictureBox3.BackColor), pictureBox3);
            this.relationsWhite.Add(new Tuple<int, int, Color>(3, 7, pictureBox4.BackColor), pictureBox4);
            this.relationsWhite.Add(new Tuple<int, int, Color>(4, 7, pictureBox5.BackColor), pictureBox5);
            this.relationsWhite.Add(new Tuple<int, int, Color>(5, 7, pictureBox6.BackColor), pictureBox6);
            this.relationsWhite.Add(new Tuple<int, int, Color>(6, 7, pictureBox7.BackColor), pictureBox7);
            this.relationsWhite.Add(new Tuple<int, int, Color>(7, 7, pictureBox8.BackColor), pictureBox8);

            this.relationsWhite.Add(new Tuple<int, int, Color>(0, 6, pictureBox16.BackColor), pictureBox16);
            this.relationsWhite.Add(new Tuple<int, int, Color>(1, 6, pictureBox15.BackColor), pictureBox15);
            this.relationsWhite.Add(new Tuple<int, int, Color>(2, 6, pictureBox14.BackColor), pictureBox14);
            this.relationsWhite.Add(new Tuple<int, int, Color>(3, 6, pictureBox13.BackColor), pictureBox13);
            this.relationsWhite.Add(new Tuple<int, int, Color>(4, 6, pictureBox12.BackColor), pictureBox12);
            this.relationsWhite.Add(new Tuple<int, int, Color>(5, 6, pictureBox11.BackColor), pictureBox11);
            this.relationsWhite.Add(new Tuple<int, int, Color>(6, 6, pictureBox10.BackColor), pictureBox10);
            this.relationsWhite.Add(new Tuple<int, int, Color>(7, 6, pictureBox9.BackColor), pictureBox9);

            this.relationsWhite.Add(new Tuple<int, int, Color>(0, 5, pictureBox24.BackColor), pictureBox24);
            this.relationsWhite.Add(new Tuple<int, int, Color>(1, 5, pictureBox23.BackColor), pictureBox23);
            this.relationsWhite.Add(new Tuple<int, int, Color>(2, 5, pictureBox22.BackColor), pictureBox22);
            this.relationsWhite.Add(new Tuple<int, int, Color>(3, 5, pictureBox21.BackColor), pictureBox21);
            this.relationsWhite.Add(new Tuple<int, int, Color>(4, 5, pictureBox20.BackColor), pictureBox20);
            this.relationsWhite.Add(new Tuple<int, int, Color>(5, 5, pictureBox19.BackColor), pictureBox19);
            this.relationsWhite.Add(new Tuple<int, int, Color>(6, 5, pictureBox18.BackColor), pictureBox18);
            this.relationsWhite.Add(new Tuple<int, int, Color>(7, 5, pictureBox17.BackColor), pictureBox17);

            this.relationsWhite.Add(new Tuple<int, int, Color>(0, 4, pictureBox32.BackColor), pictureBox32);
            this.relationsWhite.Add(new Tuple<int, int, Color>(1, 4, pictureBox31.BackColor), pictureBox31);
            this.relationsWhite.Add(new Tuple<int, int, Color>(2, 4, pictureBox30.BackColor), pictureBox30);
            this.relationsWhite.Add(new Tuple<int, int, Color>(3, 4, pictureBox29.BackColor), pictureBox29);
            this.relationsWhite.Add(new Tuple<int, int, Color>(4, 4, pictureBox28.BackColor), pictureBox28);
            this.relationsWhite.Add(new Tuple<int, int, Color>(5, 4, pictureBox27.BackColor), pictureBox27);
            this.relationsWhite.Add(new Tuple<int, int, Color>(6, 4, pictureBox26.BackColor), pictureBox26);
            this.relationsWhite.Add(new Tuple<int, int, Color>(7, 4, pictureBox25.BackColor), pictureBox25);

            this.relationsWhite.Add(new Tuple<int, int, Color>(0, 3, pictureBox40.BackColor), pictureBox40);
            this.relationsWhite.Add(new Tuple<int, int, Color>(1, 3, pictureBox39.BackColor), pictureBox39);
            this.relationsWhite.Add(new Tuple<int, int, Color>(2, 3, pictureBox38.BackColor), pictureBox38);
            this.relationsWhite.Add(new Tuple<int, int, Color>(3, 3, pictureBox37.BackColor), pictureBox37);
            this.relationsWhite.Add(new Tuple<int, int, Color>(4, 3, pictureBox36.BackColor), pictureBox36);
            this.relationsWhite.Add(new Tuple<int, int, Color>(5, 3, pictureBox35.BackColor), pictureBox35);
            this.relationsWhite.Add(new Tuple<int, int, Color>(6, 3, pictureBox34.BackColor), pictureBox34);
            this.relationsWhite.Add(new Tuple<int, int, Color>(7, 3, pictureBox33.BackColor), pictureBox33);

            this.relationsWhite.Add(new Tuple<int, int, Color>(0, 2, pictureBox48.BackColor), pictureBox48);
            this.relationsWhite.Add(new Tuple<int, int, Color>(1, 2, pictureBox47.BackColor), pictureBox47);
            this.relationsWhite.Add(new Tuple<int, int, Color>(2, 2, pictureBox46.BackColor), pictureBox46);
            this.relationsWhite.Add(new Tuple<int, int, Color>(3, 2, pictureBox45.BackColor), pictureBox45);
            this.relationsWhite.Add(new Tuple<int, int, Color>(4, 2, pictureBox44.BackColor), pictureBox44);
            this.relationsWhite.Add(new Tuple<int, int, Color>(5, 2, pictureBox43.BackColor), pictureBox43);
            this.relationsWhite.Add(new Tuple<int, int, Color>(6, 2, pictureBox42.BackColor), pictureBox42);
            this.relationsWhite.Add(new Tuple<int, int, Color>(7, 2, pictureBox41.BackColor), pictureBox41);

            this.relationsWhite.Add(new Tuple<int, int, Color>(0, 1, pictureBox56.BackColor), pictureBox56);
            this.relationsWhite.Add(new Tuple<int, int, Color>(1, 1, pictureBox55.BackColor), pictureBox55);
            this.relationsWhite.Add(new Tuple<int, int, Color>(2, 1, pictureBox54.BackColor), pictureBox54);
            this.relationsWhite.Add(new Tuple<int, int, Color>(3, 1, pictureBox53.BackColor), pictureBox53);
            this.relationsWhite.Add(new Tuple<int, int, Color>(4, 1, pictureBox52.BackColor), pictureBox52);
            this.relationsWhite.Add(new Tuple<int, int, Color>(5, 1, pictureBox51.BackColor), pictureBox51);
            this.relationsWhite.Add(new Tuple<int, int, Color>(6, 1, pictureBox50.BackColor), pictureBox50);
            this.relationsWhite.Add(new Tuple<int, int, Color>(7, 1, pictureBox49.BackColor), pictureBox49);

            this.relationsWhite.Add(new Tuple<int, int, Color>(0, 0, pictureBox64.BackColor), pictureBox64);
            this.relationsWhite.Add(new Tuple<int, int, Color>(1, 0, pictureBox63.BackColor), pictureBox63);
            this.relationsWhite.Add(new Tuple<int, int, Color>(2, 0, pictureBox62.BackColor), pictureBox62);
            this.relationsWhite.Add(new Tuple<int, int, Color>(3, 0, pictureBox61.BackColor), pictureBox61);
            this.relationsWhite.Add(new Tuple<int, int, Color>(4, 0, pictureBox60.BackColor), pictureBox60);
            this.relationsWhite.Add(new Tuple<int, int, Color>(5, 0, pictureBox59.BackColor), pictureBox59);
            this.relationsWhite.Add(new Tuple<int, int, Color>(6, 0, pictureBox58.BackColor), pictureBox58);
            this.relationsWhite.Add(new Tuple<int, int, Color>(7, 0, pictureBox57.BackColor), pictureBox57);

            this.relationsBlack.Add(new Tuple<int, int, Color>(0, 7, pictureBox1.BackColor), pictureBox57);
            this.relationsBlack.Add(new Tuple<int, int, Color>(1, 7, pictureBox2.BackColor), pictureBox58);
            this.relationsBlack.Add(new Tuple<int, int, Color>(2, 7, pictureBox3.BackColor), pictureBox59);
            this.relationsBlack.Add(new Tuple<int, int, Color>(3, 7, pictureBox4.BackColor), pictureBox60);
            this.relationsBlack.Add(new Tuple<int, int, Color>(4, 7, pictureBox5.BackColor), pictureBox61);
            this.relationsBlack.Add(new Tuple<int, int, Color>(5, 7, pictureBox6.BackColor), pictureBox62);
            this.relationsBlack.Add(new Tuple<int, int, Color>(6, 7, pictureBox7.BackColor), pictureBox63);
            this.relationsBlack.Add(new Tuple<int, int, Color>(7, 7, pictureBox8.BackColor), pictureBox64);

            this.relationsBlack.Add(new Tuple<int, int, Color>(0, 6, pictureBox16.BackColor), pictureBox49);
            this.relationsBlack.Add(new Tuple<int, int, Color>(1, 6, pictureBox15.BackColor), pictureBox50);
            this.relationsBlack.Add(new Tuple<int, int, Color>(2, 6, pictureBox14.BackColor), pictureBox51);
            this.relationsBlack.Add(new Tuple<int, int, Color>(3, 6, pictureBox13.BackColor), pictureBox52);
            this.relationsBlack.Add(new Tuple<int, int, Color>(4, 6, pictureBox12.BackColor), pictureBox53);
            this.relationsBlack.Add(new Tuple<int, int, Color>(5, 6, pictureBox11.BackColor), pictureBox54);
            this.relationsBlack.Add(new Tuple<int, int, Color>(6, 6, pictureBox10.BackColor), pictureBox55);
            this.relationsBlack.Add(new Tuple<int, int, Color>(7, 6, pictureBox9.BackColor), pictureBox56);

            this.relationsBlack.Add(new Tuple<int, int, Color>(0, 5, pictureBox24.BackColor), pictureBox41);
            this.relationsBlack.Add(new Tuple<int, int, Color>(1, 5, pictureBox23.BackColor), pictureBox42);
            this.relationsBlack.Add(new Tuple<int, int, Color>(2, 5, pictureBox22.BackColor), pictureBox43);
            this.relationsBlack.Add(new Tuple<int, int, Color>(3, 5, pictureBox21.BackColor), pictureBox44);
            this.relationsBlack.Add(new Tuple<int, int, Color>(4, 5, pictureBox20.BackColor), pictureBox45);
            this.relationsBlack.Add(new Tuple<int, int, Color>(5, 5, pictureBox19.BackColor), pictureBox46);
            this.relationsBlack.Add(new Tuple<int, int, Color>(6, 5, pictureBox18.BackColor), pictureBox47);
            this.relationsBlack.Add(new Tuple<int, int, Color>(7, 5, pictureBox17.BackColor), pictureBox48);

            this.relationsBlack.Add(new Tuple<int, int, Color>(0, 4, pictureBox32.BackColor), pictureBox33);
            this.relationsBlack.Add(new Tuple<int, int, Color>(1, 4, pictureBox31.BackColor), pictureBox34);
            this.relationsBlack.Add(new Tuple<int, int, Color>(2, 4, pictureBox30.BackColor), pictureBox35);
            this.relationsBlack.Add(new Tuple<int, int, Color>(3, 4, pictureBox29.BackColor), pictureBox36);
            this.relationsBlack.Add(new Tuple<int, int, Color>(4, 4, pictureBox28.BackColor), pictureBox37);
            this.relationsBlack.Add(new Tuple<int, int, Color>(5, 4, pictureBox27.BackColor), pictureBox38);
            this.relationsBlack.Add(new Tuple<int, int, Color>(6, 4, pictureBox26.BackColor), pictureBox39);
            this.relationsBlack.Add(new Tuple<int, int, Color>(7, 4, pictureBox25.BackColor), pictureBox40);

            this.relationsBlack.Add(new Tuple<int, int, Color>(0, 3, pictureBox40.BackColor), pictureBox25);
            this.relationsBlack.Add(new Tuple<int, int, Color>(1, 3, pictureBox39.BackColor), pictureBox26);
            this.relationsBlack.Add(new Tuple<int, int, Color>(2, 3, pictureBox38.BackColor), pictureBox27);
            this.relationsBlack.Add(new Tuple<int, int, Color>(3, 3, pictureBox37.BackColor), pictureBox28);
            this.relationsBlack.Add(new Tuple<int, int, Color>(4, 3, pictureBox36.BackColor), pictureBox29);
            this.relationsBlack.Add(new Tuple<int, int, Color>(5, 3, pictureBox35.BackColor), pictureBox30);
            this.relationsBlack.Add(new Tuple<int, int, Color>(6, 3, pictureBox34.BackColor), pictureBox31);
            this.relationsBlack.Add(new Tuple<int, int, Color>(7, 3, pictureBox33.BackColor), pictureBox32);

            this.relationsBlack.Add(new Tuple<int, int, Color>(0, 2, pictureBox48.BackColor), pictureBox17);
            this.relationsBlack.Add(new Tuple<int, int, Color>(1, 2, pictureBox47.BackColor), pictureBox18);
            this.relationsBlack.Add(new Tuple<int, int, Color>(2, 2, pictureBox46.BackColor), pictureBox19);
            this.relationsBlack.Add(new Tuple<int, int, Color>(3, 2, pictureBox45.BackColor), pictureBox20);
            this.relationsBlack.Add(new Tuple<int, int, Color>(4, 2, pictureBox44.BackColor), pictureBox21);
            this.relationsBlack.Add(new Tuple<int, int, Color>(5, 2, pictureBox43.BackColor), pictureBox22);
            this.relationsBlack.Add(new Tuple<int, int, Color>(6, 2, pictureBox42.BackColor), pictureBox23);
            this.relationsBlack.Add(new Tuple<int, int, Color>(7, 2, pictureBox41.BackColor), pictureBox24);

            this.relationsBlack.Add(new Tuple<int, int, Color>(0, 1, pictureBox56.BackColor), pictureBox9);
            this.relationsBlack.Add(new Tuple<int, int, Color>(1, 1, pictureBox55.BackColor), pictureBox10);
            this.relationsBlack.Add(new Tuple<int, int, Color>(2, 1, pictureBox54.BackColor), pictureBox11);
            this.relationsBlack.Add(new Tuple<int, int, Color>(3, 1, pictureBox53.BackColor), pictureBox12);
            this.relationsBlack.Add(new Tuple<int, int, Color>(4, 1, pictureBox52.BackColor), pictureBox13);
            this.relationsBlack.Add(new Tuple<int, int, Color>(5, 1, pictureBox51.BackColor), pictureBox14);
            this.relationsBlack.Add(new Tuple<int, int, Color>(6, 1, pictureBox50.BackColor), pictureBox15);
            this.relationsBlack.Add(new Tuple<int, int, Color>(7, 1, pictureBox49.BackColor), pictureBox16);

            this.relationsBlack.Add(new Tuple<int, int, Color>(0, 0, pictureBox64.BackColor), pictureBox8);
            this.relationsBlack.Add(new Tuple<int, int, Color>(1, 0, pictureBox63.BackColor), pictureBox7);
            this.relationsBlack.Add(new Tuple<int, int, Color>(2, 0, pictureBox62.BackColor), pictureBox6);
            this.relationsBlack.Add(new Tuple<int, int, Color>(3, 0, pictureBox61.BackColor), pictureBox5);
            this.relationsBlack.Add(new Tuple<int, int, Color>(4, 0, pictureBox60.BackColor), pictureBox4);
            this.relationsBlack.Add(new Tuple<int, int, Color>(5, 0, pictureBox59.BackColor), pictureBox3);
            this.relationsBlack.Add(new Tuple<int, int, Color>(6, 0, pictureBox58.BackColor), pictureBox2);
            this.relationsBlack.Add(new Tuple<int, int, Color>(7, 0, pictureBox57.BackColor), pictureBox1);

            this.relations = this.relationsWhite;
            #endregion
            #region Graphical style
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox3.BorderStyle = BorderStyle.FixedSingle;
            pictureBox4.BorderStyle = BorderStyle.FixedSingle;
            pictureBox5.BorderStyle = BorderStyle.FixedSingle;
            pictureBox6.BorderStyle = BorderStyle.FixedSingle;
            pictureBox7.BorderStyle = BorderStyle.FixedSingle;
            pictureBox8.BorderStyle = BorderStyle.FixedSingle;
            pictureBox9.BorderStyle = BorderStyle.FixedSingle;
            pictureBox10.BorderStyle = BorderStyle.FixedSingle;
            pictureBox11.BorderStyle = BorderStyle.FixedSingle;
            pictureBox12.BorderStyle = BorderStyle.FixedSingle;
            pictureBox13.BorderStyle = BorderStyle.FixedSingle;
            pictureBox14.BorderStyle = BorderStyle.FixedSingle;
            pictureBox15.BorderStyle = BorderStyle.FixedSingle;
            pictureBox16.BorderStyle = BorderStyle.FixedSingle;
            pictureBox17.BorderStyle = BorderStyle.FixedSingle;
            pictureBox18.BorderStyle = BorderStyle.FixedSingle;
            pictureBox19.BorderStyle = BorderStyle.FixedSingle;
            pictureBox20.BorderStyle = BorderStyle.FixedSingle;
            pictureBox21.BorderStyle = BorderStyle.FixedSingle;
            pictureBox22.BorderStyle = BorderStyle.FixedSingle;
            pictureBox23.BorderStyle = BorderStyle.FixedSingle;
            pictureBox24.BorderStyle = BorderStyle.FixedSingle;
            pictureBox25.BorderStyle = BorderStyle.FixedSingle;
            pictureBox26.BorderStyle = BorderStyle.FixedSingle;
            pictureBox27.BorderStyle = BorderStyle.FixedSingle;
            pictureBox28.BorderStyle = BorderStyle.FixedSingle;
            pictureBox29.BorderStyle = BorderStyle.FixedSingle;
            pictureBox30.BorderStyle = BorderStyle.FixedSingle;
            pictureBox31.BorderStyle = BorderStyle.FixedSingle;
            pictureBox32.BorderStyle = BorderStyle.FixedSingle;
            pictureBox33.BorderStyle = BorderStyle.FixedSingle;
            pictureBox34.BorderStyle = BorderStyle.FixedSingle;
            pictureBox35.BorderStyle = BorderStyle.FixedSingle;
            pictureBox36.BorderStyle = BorderStyle.FixedSingle;
            pictureBox37.BorderStyle = BorderStyle.FixedSingle;
            pictureBox38.BorderStyle = BorderStyle.FixedSingle;
            pictureBox39.BorderStyle = BorderStyle.FixedSingle;
            pictureBox40.BorderStyle = BorderStyle.FixedSingle;
            pictureBox41.BorderStyle = BorderStyle.FixedSingle;
            pictureBox42.BorderStyle = BorderStyle.FixedSingle;
            pictureBox43.BorderStyle = BorderStyle.FixedSingle;
            pictureBox44.BorderStyle = BorderStyle.FixedSingle;
            pictureBox45.BorderStyle = BorderStyle.FixedSingle;
            pictureBox46.BorderStyle = BorderStyle.FixedSingle;
            pictureBox47.BorderStyle = BorderStyle.FixedSingle;
            pictureBox48.BorderStyle = BorderStyle.FixedSingle;
            pictureBox49.BorderStyle = BorderStyle.FixedSingle;
            pictureBox50.BorderStyle = BorderStyle.FixedSingle;
            pictureBox51.BorderStyle = BorderStyle.FixedSingle;
            pictureBox52.BorderStyle = BorderStyle.FixedSingle;
            pictureBox53.BorderStyle = BorderStyle.FixedSingle;
            pictureBox54.BorderStyle = BorderStyle.FixedSingle;
            pictureBox55.BorderStyle = BorderStyle.FixedSingle;
            pictureBox56.BorderStyle = BorderStyle.FixedSingle;
            pictureBox57.BorderStyle = BorderStyle.FixedSingle;
            pictureBox58.BorderStyle = BorderStyle.FixedSingle;
            pictureBox59.BorderStyle = BorderStyle.FixedSingle;
            pictureBox60.BorderStyle = BorderStyle.FixedSingle;
            pictureBox61.BorderStyle = BorderStyle.FixedSingle;
            pictureBox62.BorderStyle = BorderStyle.FixedSingle;
            pictureBox63.BorderStyle = BorderStyle.FixedSingle;
            pictureBox64.BorderStyle = BorderStyle.FixedSingle;
            #endregion
        }

        private void LoadInitialSettings()
        {
            this.LoadInitialSelectedSquare();
            this.LoadInitialEvaluation();
        }

        private void LoadInitialEvaluation()
        {
            this.checkPositionalEvaluation.Checked = true;
        }

        private void LoadInitialSelectedSquare()
        {
            this.SquareSelected = SquareUI.GetEmptyInstance();
        }

        private void LoadOpeningsIntoGame()
        {
            var openings = this.GetOpenings();
            this.Game = new Game(openings);
        }

        private void LoadMovesTable()
        {
            this.MovesTable.Add(PieceIdentifiers.Pawn, this.GetPawnMove);
            this.MovesTable.Add(PieceIdentifiers.King, this.GetKingMove);
        }

        private IEnumerable<Sequence> GetOpenings()
        {
            var openingFiles = this.GetOpeningFiles();
            var fileManager = new FileManager();

            return openingFiles.Select(f => fileManager.ReadOpeningFromFile(string.Concat(this.path, f))).ToList();
        }

        private List<string> GetOpeningFiles()
        {
            return new List<string> { "ruilopez.txt", "sicilianad6.txt", "sicilianadc7.txt", @"sicilianadcf6.txt", @"sicilianae5.txt" };
        }


        private void TapSquare(int column, int row)
        {
            if (!this.CheckOrientationBoard.Checked)
            {
                column = 7 - column;
                row = 7 - row;
            }

            this.SelectOrMove(column, row);
        }

        private void EditBoard(int column, int row)
        {
            var square = Game.Board.GetSquare(column, row);

            if (this.SquareSelected.HasPiece)
            {
                ApplyMove(
                    this.SquareSelected.Column,
                    this.SquareSelected.Row,
                    square.Column - this.SquareSelected.Column,
                    square.Row - this.SquareSelected.Row);

                this.SquareSelected.HasPiece = false;

            }
            else if (square.CurrentPiece != null)
            {
                this.SquareSelected.HasPiece = true;

                this.SquareSelected.Column = column;
                this.SquareSelected.Row = row;
            }
        }

        private bool CanEatPieceAt(Square square)
        {
            return square.CurrentPiece == null
                            || (Game.Turn == 1 && square.CurrentPiece.Color.Equals(PieceColor.Black))
                            || (Game.Turn == -1 && square.CurrentPiece.Color.Equals(PieceColor.White));
        }

        private void SelectOrMove(int column, int row)
        {
            if (this.EditMode.Checked)
            {
                this.EditBoard(column, row);
                this.Draw();
            }
            else
            {
                this.EatOrMovePiece(column, row);
            }
        }

        public void EatOrMovePiece(int column, int row)
        {
            if (this.SquareSelected.HasPiece)
            {
                var squareTo = Game.Board.GetSquare(column, row);
                try
                {
                    if (this.CanEatPieceAt(squareTo))
                    {
                        this.DoMove(
                            this.SquareSelected.Column,
                            this.SquareSelected.Row,
                            squareTo.Column - this.SquareSelected.Column,
                            squareTo.Row - this.SquareSelected.Row);

                        this.SquareSelected.HasPiece = false;
                    }
                    else
                    {
                        Console.WriteLine("Cannot move there.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else
            {
                this.MoveTo(column, row);
            }
        }

        private void MoveTo(int column, int row)
        {
            var square = Game.Board.GetSquare(column, row);

            if (this.IsValidSquareToMove(square))
            {
                this.SquareSelected.HasPiece = true;
                this.SquareSelected.Column = column;
                this.SquareSelected.Row = row;
            }
        }

        private bool IsValidSquareToMove(Square square)
        {
            return square.CurrentPiece != null &&
                ((Game.Turn == 1 && square.CurrentPiece.Color.Equals(PieceColor.White))
                || (Game.Turn == -1 && square.CurrentPiece.Color.Equals(PieceColor.Black)));
        }

        private void Draw()
        {
            var squares = this.Game.Board.Squares.SelectMany(s => s);
            foreach (var square in squares)
            {
                var relation = this.relations.FirstOrDefault(r => r.Key.Item1 == square.Column && r.Key.Item2 == square.Row);
                if (this.relations.ContainsKey(relation.Key))
                {
                    var pb = this.relations[relation.Key];

                    if (square.CurrentPiece == null)
                    {
                        pb.Image = null;
                        continue;
                    }

                    pb.Image = SelectImage(square.CurrentPiece);
                }
            }
        }



        private Image SelectImage(Piece currentPiece)
        {
            var startUri = string.Concat(this.path, this.ColorPiece(currentPiece.Color));

            if (currentPiece is Pawn)
            {
                return Image.FromFile(string.Concat(startUri, "pawn.png"));
            }

            if (currentPiece is Rock)
            {
                return Image.FromFile(string.Concat(startUri, "rock.png"));
            }

            if (currentPiece is Bishop)
            {
                return Image.FromFile(string.Concat(startUri, "bishop.png"));
            }

            if (currentPiece is King)
            {
                return Image.FromFile(string.Concat(startUri, "king.png"));
            }

            if (currentPiece is Knight)
            {
                return Image.FromFile(string.Concat(startUri, "knight.png"));
            }

            if (currentPiece is Queen)
            {
                return Image.FromFile(string.Concat(startUri, "queen.png"));
            }

            return null;
        }

        private string ColorPiece(PieceColor color)
        {
            return color.Equals(PieceColor.White) ? "White" : "Black";
        }


        private void PlayerDoMove(int column, int row, int columnMove, int rowMove)
        {
            this.ApplyMove(column, row, columnMove, rowMove);
            this.Draw();
        }
        private async Task CpuDoMove()
        {
            await this.CpuMoves();
            if (this.Game.AiManager.IsCheck(this.Game.Board, Game.Turn, Game.Turn == 1))
            {
                MessageBox.Show(Resources.ChessWindowForm_DoMove_Check_);
            }

            this.Draw();
        }

        private async void DoMove(int column, int row, int columnMove, int rowMove)
        {
            this.PlayerDoMove(column, row, columnMove, rowMove);

            await this.CpuDoMove();
        }

        private IList<KeyValuePair<string, Dictionary<string, Sequence>>> GetMatchingOpenings(string currentOpening)
        {
            var matchingOpenings = Game.Sequences.Where(s => s.Value.ContainsKey(currentOpening));
            var keyValuePairs_ = matchingOpenings as IList<KeyValuePair<string, Dictionary<string, Sequence>>> ?? matchingOpenings;

            return keyValuePairs_.ToList();
        }

        private Ply GetNextMoveFromRandomOpening(string currentOpening, IList<KeyValuePair<string, Dictionary<string, Sequence>>> matchingOpenings)
        {
            var randomOpening = (new Random()).Next(0, matchingOpenings.Count);

            var sequenceSelected = matchingOpenings[randomOpening].Value;
            var ply = sequenceSelected[currentOpening].GetFittingPly(Game.CurrentPlySequence);

            return ply;
        }


        private PawnMoveType GetPawnType(Ply nextMove)
        {
            if (Math.Abs(nextMove.R1 - nextMove.R0) == 2)
            {
                return PawnMoveType.AdvanceTwo;
            }
            else if (nextMove.C0 < nextMove.C1)
            {
                return PawnMoveType.EatRight;
            }
            else if (nextMove.C0 > nextMove.C1)
            {
                return PawnMoveType.EatLeft;
            }

            return PawnMoveType.AdvanceOne;
        }

        private Move GetPawnMove(Ply nextMove)
        {
            PawnMoveType pawnType = this.GetPawnType(nextMove);

            return new PawnMove(pawnType, nextMove.C1 - nextMove.C0, nextMove.R1 - nextMove.R0);
        }

        private Move GetKingMove(Ply nextMove)
        {
            return new KingMove(nextMove.C1 - nextMove.C0, nextMove.R1 - nextMove.R0, Math.Abs(nextMove.C1 - nextMove.C0) > 1);
        }

        private Move GetCpuMove(Piece piece, Square squareFrom, Square squareTo, Ply nextMove)
        {
            MoveDelegate cpuMove;

            var moveMethod = this.MovesTable.TryGetValue((PieceIdentifiers)piece.PieceIdentifier, out cpuMove);

            return cpuMove != null ? cpuMove(nextMove) : new Move(squareTo.Column - squareFrom.Column, squareTo.Row - squareFrom.Row);
        }

        private Operator GetOptionCpuForMatchingOpening(string currentOpening, IList<KeyValuePair<string, Dictionary<string, Sequence>>> matchingOpenings)
        {
            var nextMove = this.GetNextMoveFromRandomOpening(currentOpening, matchingOpenings);

            var squareFrom = Game.Board.GetSquare(nextMove.C0, nextMove.R0);
            var squareTo = Game.Board.GetSquare(nextMove.C1, nextMove.R1);

            Move move = this.GetCpuMove(squareFrom.CurrentPiece, squareFrom, squareTo, nextMove);

            return new Operator(move, squareFrom);
        }

        private GamePhase GetPhaseGame()
        {
            return Game.AiManager.GamePhase == GamePhase.Opening
                ? GamePhase.MiddleGame
                : Game.AiManager.GamePhase;
        }

        private void PrintPreviousLastSquare()
        {
            if (PreviousLastSquareMoved != null)
            {
                var nextSquare =
                    this.relations.FirstOrDefault(
                        c =>
                            c.Key.Item1 == PreviousLastSquareMoved.Column &&
                            c.Key.Item2 == PreviousLastSquareMoved.Row);

                nextSquare.Value.BackColor = nextSquare.Key.Item3;
            }
        }

        private async Task CpuMoves()
        {
            Game.Turn = -Game.Turn;

            Operator optionCpu = null;

            if (Game.AiManager.GamePhase == GamePhase.Opening)
            {
                var currentOpening = Game.CurrentPlySequence.ToString();
                var matchingOpenings = this.GetMatchingOpenings(currentOpening);

                if (matchingOpenings.Any())
                {
                    optionCpu = this.GetOptionCpuForMatchingOpening(currentOpening, matchingOpenings);
                }
                else
                {
                    Game.AiManager.GamePhase = this.GetPhaseGame();
                }
            }

            if (optionCpu == null)
            {
                Game.AiManager.GamePhase = this.GetPhaseGame();

                this.SetText(Resources.ChessWindowForm_Dashes_);
                this.SetText(Resources.ChessWindowForm_CpuMoves_);

                optionCpu = await this.Search(Game.Board, Game.Turn, Convert.ToInt32(this.TextDepthSearch.Text));
            }

            if (optionCpu != null)
            {
                ApplyMove(optionCpu.Square.Column, optionCpu.Square.Row, optionCpu.Move.Column, optionCpu.Move.Row);
                this.PrintPreviousLastSquare();

                var finalRow = optionCpu.Square.Row + optionCpu.Move.Row;
                var finalColumn = optionCpu.Square.Column + optionCpu.Move.Column;

                var square = this.relations.FirstOrDefault(c => c.Key.Item1 == finalColumn && c.Key.Item2 == finalRow);

                PreviousLastSquareMoved = new SquareUI
                {
                    Column = optionCpu.Square.Column + optionCpu.Move.Column,
                    Row = optionCpu.Square.Row + optionCpu.Move.Row
                };

                square.Value.BackColor = Color.LightBlue;

                this.SetText(Resources.ChessWindowForm_MoveDone_ + (optionCpu.Square.Column + optionCpu.Move.Column) + "," + (optionCpu.Square.Row + optionCpu.Move.Row));

                Draw();
            }
            else
            {
                MessageBox.Show(Game.Turn == -1 ? "White wins!" : "Black wins!");
            }

            Game.Turn = -Game.Turn;
        }

        private void SetText(string message)
        {
            this.TextActions.Text = this.TextActions.Text.Insert(0, string.Format("\r\n{0}:{1}:{2} - {3}",
                DateTime.Now.TimeOfDay.Hours.ToString().PadLeft(2, '0'), DateTime.Now.TimeOfDay.Minutes.ToString().PadLeft(2, '0'), DateTime.Now.TimeOfDay.Seconds.ToString().PadLeft(2, '0'), message));
        }

        private async Task<Operator> Search(Chessty.Board board, int turn, int v)
        {
            long milliseconds = 0;
            loadingPicture.Visible = true;
            btnUndo.Visible = false;

            var searchTask = new Task<Operator>(() =>
             {
                 var stopwatch = new Stopwatch();
                 stopwatch.Start();

                 var moveFound = Game.AiManager.Search(Game.Board, Game.Turn, v);

                 stopwatch.Stop();
                 milliseconds = stopwatch.ElapsedMilliseconds;

                 return moveFound;
             });

            searchTask.Start();
            var result = await searchTask;

            this.SetText(string.Concat("Time for the last move: ", milliseconds.ToString(CultureInfo.InvariantCulture), " miliseconds"));

            loadingPicture.Visible = false;
            btnUndo.Visible = true;

            return result;
        }

        private void ApplyMove(int column, int row, int columnMove, int rowMove)
        {
            var square = this.Game.Board.GetSquare(column, row);
            var piece = square.CurrentPiece;

            Move move;
            if (piece is Pawn)
            {
                var pawnMoveType = PawnMoveType.AdvanceOne;
                switch (Math.Abs(rowMove))
                {
                    case 2:
                        pawnMoveType = PawnMoveType.AdvanceTwo;
                        break;

                    case 1:
                        switch (columnMove)
                        {
                            case 1:
                                pawnMoveType = PawnMoveType.EatRight;
                                break;
                            case -1:
                                pawnMoveType = PawnMoveType.EatLeft;
                                break;
                        }

                        break;
                }

                move = new PawnMove(pawnMoveType, columnMove, rowMove);
            }
            else
            {
                if (piece is King && (columnMove == 2 || columnMove == -2))
                {
                    move = new KingMove(columnMove, rowMove, true);
                }
                else
                {
                    move = new Move(columnMove, rowMove);
                }
            }

            var operator1 = new Operator(move, square);

            var play = new MoveDefinition
            {
                Board = this.Game.Board,
                IsInCheck = this.Game.AiManager.IsCheck(this.Game.Board, this.Game.Turn, this.Game.Turn == 1),
                Move = move,
                Square = square
            };

            if (this.Game.AiManager.GetMovePriority(play) != 0)
            {
                var longHash = this.Game.Board.HashBoard;

                var nextSquare = Game.Board.GetSquare(square.Column + operator1.Move.Column, square.Row + operator1.Move.Row);

                var nextPiece = nextSquare.CurrentPiece;
                var sourcePiece = square.CurrentPiece;

                bool queening, disCastling;
                GamePhase gamePhase = this.Game.AiManager.GamePhase;
                bool[] previousValues;
                this.ApplyMove(operator1, out queening, out disCastling, out previousValues);

                this.Game.UndoTable.Add(
                    this.Game.UndoTable.Count + 1,
                    new Tuple<Operator, Square, Square, Piece, bool[], Piece, bool, Tuple<bool, GamePhase, long>>(
                        operator1,
                        nextSquare,
                        square,
                        nextPiece,
                        previousValues,
                        sourcePiece,
                        queening,
                        new Tuple<bool, GamePhase, long>(disCastling, gamePhase, longHash)));

                btnUndo.Enabled = true;

                var length = this.Game.CurrentPlySequence.GetPlies().Count;

                var newPly =
                    new Ply(
                        square.Column,
                        square.Row,
                        square.Column + operator1.Move.Column,
                        square.Row + operator1.Move.Row);

                this.Game.CurrentPlySequence.Push(newPly);

                this.Game.PreviousPlySequence.CutTo(length);
                this.Game.PreviousPlySequence.Push(newPly);

            }
            else
            {
                this.SquareSelected.HasPiece = false;
            }
        }

        private delegate void SetCastleSettingsDelegate(Square squareFutureRock, Square squareRock, out bool discastle);

        private void ApplyMove(Operator operator1, out bool queening, out bool disCastling, out bool[] previousValues)
        {
            disCastling = false;
            previousValues = null;
            queening = false;

            Piece pieceToReset;
            var color = operator1.Square.CurrentPiece.Color;

            var nextSquare = this.Game.Board.GetSquare(
                operator1.Square.Column + operator1.Move.Column,
                operator1.Square.Row + operator1.Move.Row);

            var currentPieceValue = operator1.Square.CurrentPiece.Value;
            var colorIsWhite = color == PieceColor.White;
            var whiteIsZero = colorIsWhite ? 0 : 7;
            var kingColorPosition = colorIsWhite ? Game.Board.WhiteKingPosition : Game.Board.BlackKingPosition;

            this.Game.AiManager.XorSquare(this.Game.Board, operator1.Square.Column, operator1.Square.Row, operator1.Square.CurrentPiece);

            if (currentPieceValue == PieceValue.King)
            {
                previousValues = new[] { (operator1.Square.CurrentPiece as King).Castle };

                SetCastleSettingsDelegate setCastleSettings = (Square squareRock, Square squareFutureRock, out bool discastle) =>
                {
                    squareFutureRock.CurrentPiece = squareRock.CurrentPiece;

                    squareRock.CurrentPiece = null;
                    if (colorIsWhite)
                    {
                        this.Game.Board.WhiteHasCastled = true;
                        this.Game.Board.WhiteSquaresWithPieces.Remove(squareRock.Identifier);
                    }
                    else
                    {
                        this.Game.Board.BlackHasCastled = true;
                        this.Game.Board.BlackSquaresWithPieces.Remove(squareRock.Identifier);
                    }

                    discastle = true;
                };

                switch (operator1.Move.Column)
                {
                    case 2:
                        var squareRock1 = this.Game.Board.GetSquare(7, whiteIsZero);
                        var squareFutureRock1 = this.Game.Board.GetSquare(5, whiteIsZero);
                        bool dis1;
                        setCastleSettings(squareRock1, squareFutureRock1, out dis1);
                        disCastling = dis1;
                        break;
                    case -2:
                        var squareRock = this.Game.Board.GetSquare(0, whiteIsZero);
                        var squareFutureRock = this.Game.Board.GetSquare(3, whiteIsZero);
                        bool dis;
                        setCastleSettings(squareRock, squareFutureRock, out dis);
                        disCastling = dis;
                        break;
                }

                (kingColorPosition.CurrentPiece as King).Castle = false;
            }
            else if (operator1.Square.CurrentPiece is Rock)
            {
                (kingColorPosition.CurrentPiece as King).Castle = false;

                previousValues = new[]
                {
                    (kingColorPosition.CurrentPiece as King).Castle,
                    (operator1.Square.CurrentPiece as Rock).Developed
                };
            }
            else
                switch (currentPieceValue)
                {
                    case PieceValue.Bishop:
                        previousValues = new[] { (operator1.Square.CurrentPiece as Bishop).Developed };
                        break;
                    case PieceValue.Knight:
                        previousValues = new[] { (operator1.Square.CurrentPiece as Knight).Developed };
                        break;
                    case PieceValue.Queen:
                        previousValues = new[] { (operator1.Square.CurrentPiece as Queen).Developed };
                        break;

                    default:
                        if (operator1.Square.CurrentPiece is Pawn)
                        {
                            var pawn = operator1.Square.CurrentPiece as Pawn;
                            previousValues = new[]
                            {
                                pawn.IsPassantLeft, pawn.IsPassantRight, pawn.AdvanceTwo
                            };
                            if (operator1.Square.Row == (colorIsWhite ? 6 : 1))
                            {
                                operator1.Square.CurrentPiece = colorIsWhite
                                    ? (Queen)new WhiteQueen(operator1.Square)
                                    : (Queen)new BlackQueen(operator1.Square);

                                if (colorIsWhite)
                                {
                                    this.Game.Board.MaterialWhite += (int)PieceValue.Queen;
                                }
                                else
                                {
                                    this.Game.Board.MaterialBlack += (int)PieceValue.Queen;
                                }

                                queening = true;
                            }
                        }

                        break;
                }

            if (nextSquare.CurrentPiece != null)
            {
                if (colorIsWhite)
                {
                    this.Game.Board.MaterialBlack -= (int)nextSquare.CurrentPiece.Value;
                }
                else
                {
                    this.Game.Board.MaterialWhite -= (int)nextSquare.CurrentPiece.Value;
                }
            }

            if (colorIsWhite)
            {
                this.Game.Board.WhiteSquaresWithPieces.Remove(operator1.Square.Identifier);
                this.Game.Board.WhiteSquaresWithPieces.Add(nextSquare.Identifier, nextSquare);

                this.Game.Board.BlackSquaresWithPieces.Remove(nextSquare.Identifier);
            }
            else
            {
                this.Game.Board.BlackSquaresWithPieces.Remove(operator1.Square.Identifier);
                this.Game.Board.BlackSquaresWithPieces.Add(nextSquare.Identifier, nextSquare);

                this.Game.Board.WhiteSquaresWithPieces.Remove(nextSquare.Identifier);
            }

            operator1.Operate(this.Game.Board, out pieceToReset);

            if (pieceToReset != null)
            {
                this.Game.AiManager.XorSquare(this.Game.Board, nextSquare.Column, nextSquare.Row, pieceToReset);
            }

            this.Game.AiManager.XorSquare(this.Game.Board, nextSquare.Column, nextSquare.Row, nextSquare.CurrentPiece);

            if (this.Game.Board.MaterialWhite < 21700 && this.Game.Board.MaterialWhite < 21700)
            {
                this.Game.AiManager.GamePhase = GamePhase.EndGame;
                this.Game.AiManager.WhiteKingMatrix = this.Game.AiManager.kingTableEndGameWhite;
                this.Game.AiManager.BlackKingMatrix = this.Game.AiManager.kingTableEndGameBlack;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private async void cpuWhite_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCpuWhite.Checked)
            {
                Game.Turn = -1;
                Game.CurrentPlySequence = new Sequence();
                await CpuMoves();

            }
            else
            {
                Game.Turn = 1;
            }

            Draw();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            this.relations = this.CheckOrientationBoard.Checked ? this.relationsWhite : this.relationsBlack;
            Draw();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            var whiteKing = Game.Board.WhiteKingPosition.CurrentPiece as King;
            whiteKing.Castle = checkBox8.Checked;
            Game.Board.WhiteHasCastled = !checkBox8.Checked;
        }

        private void newGame_Click(object sender, EventArgs e)
        {
            var form2 = new ChessWindowForm();
            form2.Game.AiManager.TranspositionTable = Game.AiManager.TranspositionTable;
            form2.Show();
        }

        private void restartGame_Click(object sender, EventArgs e)
        {
            var tranpositionTable = this.Game.AiManager.TranspositionTable;
            var refutationTable = this.Game.AiManager.KillerMovesTable;

            var openings = this.GetOpenings();
            this.Game = new Game(openings)
            {
                AiManager =
                {
                    TranspositionTable = tranpositionTable,
                    KillerMovesTable = refutationTable
                }
            };

            this.Draw();
        }

        private async void btnUndo_Click(object sender, EventArgs e)
        {
            if (this.Game.UndoTable.Count > 0)
            {
                if (this.Game.Turn == 1)
                {
                    this.UndoPly();
                    this.UndoPly();
                }
                else
                {
                    if (this.Game.UndoTable.Count == 1)
                    {
                        this.UndoPly();
                        this.Draw();
                        await this.CpuMoves();
                    }
                    else
                    {
                        this.UndoPly();
                        this.UndoPly();
                        this.Draw();
                        await this.CpuMoves();
                    }
                }

                this.Draw();
            }
        }

        private void UndoPly()
        {
            int key = this.Game.UndoTable.Count;
            this.Game.CurrentPlySequence.Pop();
            var undoEntry = this.Game.UndoTable[key];

            this.Game.AiManager.UndoMove(
                this.Game.Board,
                undoEntry.Item1,
                undoEntry.Item2,
                undoEntry.Item3,
                undoEntry.Item4,
                undoEntry.Item5 != null ? undoEntry.Item5.ToList() : new List<bool>(),
                undoEntry.Item6,
                undoEntry.Item7,
                undoEntry.Rest.Item2,
                undoEntry.Rest.Item3,
                undoEntry.Rest.Item1
                );

            this.Game.UndoTable.Remove(key);

            if ((this.Game.UndoTable.Count == 1 && this.Game.UndoTable.Count == 0)
                || (this.Game.UndoTable.Count == -1 && this.Game.UndoTable.Count == 1))
            {
                btnUndo.Enabled = false;
            }
        }

        private void btnHeuristic_Click(object sender, EventArgs e)
        {
            this.SetText("Evaluation:\r\nWhite: " +
                this.Game.AiManager.Heuristic(
                this.Game.Board, 0,
                this.Game.AiManager.GetLegalCandidates(
                                    this.Game.Board, true,
                                    this.Game.AiManager.IsCheck(this.Game.Board, 0, true), 0).Count(),
                true, this.Game.AiManager.IsCheck(this.Game.Board, 1, false), Guid.NewGuid()) / 100
                    + "\r\nBlack: " +
                this.Game.AiManager.Heuristic(
                this.Game.Board, 1,
                this.Game.AiManager.GetLegalCandidates(this.Game.Board, false,
                    this.Game.AiManager.IsCheck(this.Game.Board, 1, false), 1).Count(), false, this.Game.AiManager.IsCheck(this.Game.Board, 0, true),
                    Guid.NewGuid()) / 100
                    );

            this.SetText("board is now " + Game.Board.HashBoard);
        }

        private void btnTt_CheckedChanged(object sender, EventArgs e)
        {
            this.Game.AiManager.EnableTranspositionTable = btnTt.Checked;
        }

        private void checkPositionalEvaluation_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}