using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BanSach
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int Standard = 0;
        int IsSV = 0;
        double TotalInCome = 0;
        SqlConnection cn;
        SqlCommand cmd;
        string sql = @"Data Source=H114\SQLEXPRESS;Initial Catalog=Pratice;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt = new DataTable();


        void loadata()
        {
           cmd=cn.CreateCommand();
           cmd.CommandText = "Select * from ThongKe";
           adapter.SelectCommand = cmd;
           dt.Clear();
           adapter.Fill(dt);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Yêu cầu nhập tên");
            }
            if (txtQuanity.Text == "" && txtName.Text!="")
            {
                MessageBox.Show("Yêu cầu nhập số nguyên dương");
            }
            if (txtQuanity.Text != "")
            {
                try
                {
                    int a = int.Parse(txtQuanity.Text);
                    if (a < 0)
                        MessageBox.Show("Yêu cầu là số nguyên dương");
                    else
                    {
                        Standard += 1;
                        double NormalPrice = 200000 * a;
                        double SVPrice = 200000 * a  - (0.05*20000*a);
                        if(cbIsSV.Checked)
                        {
                            txtTotal.Text = SVPrice.ToString();
                            IsSV += 1;
                            TotalInCome += NormalPrice;
                        }
                        else
                        {
                            txtTotal.Text = NormalPrice.ToString();
                            TotalInCome += SVPrice;
                        }    
                    }    
                }
                catch (Exception)
                {
                    MessageBox.Show("Yêu cầu là số nguyên dương");
                }
            }
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            foreach(Control c in this.groupBox1.Controls)
            {
                if(c is TextBox)
                {
                    c.Text = "";
                }  
                
            }
            cbIsSV.Checked = false;
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                Application.ExitThread();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            txtKH.Text = Standard.ToString();
            txtSV.Text = IsSV.ToString();
            txtSum.Text = TotalInCome.ToString();
            cn = new SqlConnection(sql);
            cn.Open();
            cmd =cn.CreateCommand();
            cmd.CommandText = "insert into ThongKe(TongSoKH, LaSV, InCome) values ('" + txtKH.Text + "','" + txtSV.Text + "','" + txtSum.Text + "')";
            cmd.ExecuteNonQuery();
            loadata();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
