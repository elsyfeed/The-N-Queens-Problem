using System.Diagnostics;
using System.Drawing.Imaging;

namespace QueensGambit
{
    public partial class MainForm : Form
    {
        private int N;

        bool InDamage(int x1, int y1, int x2, int y2)
        {
            if ((x1 == x2) || (y1 == y2))
                return true;


            int tx, ty;

            tx = x1 - 1;
            ty = y1 - 1;
            while ((tx >= 1) && (ty >= 1))
            {
                if ((tx == x2) && (ty == y2))
                    return true;
                tx--;
                ty--;
            }

            tx = x1 + 1;
            ty = y1 + 1;
            while ((tx <= N) && (ty <= N))
            {
                if ((tx == x2) && (ty == y2))
                    return true;
                tx++;
                ty++;
            }

            tx = x1 + 1;
            ty = y1 - 1;
            while ((tx <= N) && (ty >= 1))
            {
                if ((tx == x2) && (ty == y2))
                    return true;
                tx++;
                ty--;
            }

            tx = x1 - 1; ty = y1 + 1;
            while ((tx >= 1) && (ty <= N))
            {
                if ((tx == x2) && (ty == y2))
                    return true;
                tx--;
                ty++;
            }

            return false;
        }

        bool ChekDamage(int[] M, int p)
        {
            int px, py, x, y;
            px = M[p];
            py = p;

            for (int i = 1; i <= p - 1; i++)
            {
                x = M[i];
                y = i;
                if (InDamage(x, y, px, py))
                    return true;
            }

            return false;
        }

        void InitDataGridView(int N)
        {
            dataGridView1.Columns.Clear();

            for (int i = 0; i < N; i++)
            {
                dataGridView1.Columns.Add($"{i + 1}" + i.ToString(), i.ToString());


                dataGridView1.Columns[i].Width = 20;
            }

            dataGridView1.Rows.Add(N);

            for (int i = 0; i < N; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }

            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                if (e.ColumnIndex % 2 == 0)
                {
                    e.CellStyle.BackColor = Color.White;
                    e.CellStyle.ForeColor = Color.Black;
                }
                else
                {
                    e.CellStyle.BackColor = Color.Black;
                    e.CellStyle.ForeColor = Color.White;
                }
            }
            else
            {
                if (e.ColumnIndex % 2 == 0)
                {
                    e.CellStyle.BackColor = Color.Black;
                    e.CellStyle.ForeColor = Color.White;
                }
                else
                {
                    e.CellStyle.BackColor = Color.White;
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }

        void ShowDataGridView(string s, int N)
        {
            string xs, ys;
            int x, y;
            int j;

            for (int i = 0; i < N; i++)
            {
                for (j = 0; j < N; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = "";
                }
            }

            j = 0;
            for (int i = 0; i < N; i++)
            {
                xs = "";
                while (s[j] != ',')
                {
                    xs = xs + s[j].ToString();
                    j++;
                }
                j++;

                ys = "";
                while (s[j] != '-')
                {
                    ys = ys + s[j].ToString();
                    j++;
                }
                j++;

                x = Convert.ToInt32(xs);
                y = Convert.ToInt32(ys);

                dataGridView1.Rows[y - 1].Cells[x - 1].Value = "♕";
            }
        }

        void AddToListBox(int[] M, int N)
        {
            string s = "";
            for (int i = 1; i <= N; i++)
                s = s + M[i].ToString() + "," + i.ToString() + "-";

            listBox1.Items.Add(s);
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count <= 0)
                return;

            int num = listBox1.SelectedIndex;
            string s = listBox1.Items[num].ToString();
            ShowDataGridView(s, N);
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (listBox1.Items.Count <= 0) return;

            int num;
            string s;
            num = listBox1.SelectedIndex;
            s = listBox1.Items[num].ToString();
            ShowDataGridView(s, N);
        }
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            const int MaxN = 20;
            int[] M = new int[MaxN];
            int p;
            int k;

            int number;
            if (int.TryParse(comboBox1.Text, out number))
            {
                N = Convert.ToInt32(comboBox1.Text);
                InitDataGridView(N);
                dataGridView1.CellFormatting += dataGridView1_CellFormatting;

                if (N > 10)
                {


                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.Width = 70;


                    }
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Height = 70;
                    }

                    dataGridView1.Width = 70 * N;
                    dataGridView1.Height = 70 * N;


                    dataGridView1.DefaultCellStyle.Font = new Font("Arial", 33, FontStyle.Bold);
                }
                else
                {
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.Width = 85;


                    }
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Height = 85;
                    }
                    dataGridView1.Width = 85 * N;
                    dataGridView1.Height = 85 * N;
                    dataGridView1.DefaultCellStyle.Font = new Font("Arial", 45, FontStyle.Bold);
                }
                dataGridView1.CurrentCell = null;

                dataGridView1.RowHeadersVisible = false;
                dataGridView1.ColumnHeadersVisible = false;

                listBox1.Items.Clear();


                p = 1;
                M[p] = 0;
                M[0] = 0;
                k = 0;


                while (p > 0)
                {
                    M[p] = M[p] + 1;
                    if (p == N)
                    {
                        if (M[p] > N)
                        {
                            while (M[p] > N) p--;
                        }
                        else
                        {
                            if (!ChekDamage(M, p))
                            {

                                AddToListBox(M, N);
                                k++;
                                p--;
                            }
                        }
                    }
                    else
                    {
                        if (M[p] > N)
                        {
                            while (M[p] > N) p--;
                        }
                        else
                        {
                            if (!ChekDamage(M, p))
                            {
                                p++;
                                M[p] = 0;
                            }
                        }
                    }
                }

                if (k > 0)
                {
                    listBox1.SelectedIndex = 0;
                    listBox1_Click(sender, e);
                    label2.Text = "The number of solutions reaches: " + k.ToString();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
            dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));

            string imagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "N Queens Problem solution.png");
            bitmap.Save(imagePath, ImageFormat.Png);
            Process.Start("explorer.exe", "/select, \"" + imagePath + "\"");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
