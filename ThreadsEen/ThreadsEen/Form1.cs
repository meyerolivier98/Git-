using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace ThreadsEen
{
    public partial class Form1 : Form
    {
        DataTable dtAdd;
        DataRow drToAdd;
        DataTable dtTimes;
        DataRow drToTimes;
        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private int threadFunction(int[] inArr)
        {
            return 0;
        }
        int number;
        private int maxarrival = 0;
        private int quantum = 2;
        private int iTwo = 0;
        private int listtotal = 1;
        private int numklik = 0;
        private int itel = 0;
        private int waittel = 0;
        private string processname;
        private bool done = false;
        private int maxburst = 10;
        private int Turntime = 0;
        private int Waittime = 0;
        private int Reactime = 0;

        private string ConnectionString = "Integrated Security=SSPI;" +"Initial Catalog=;" + "Data Source=(LocalDB)\\MSSQLLocalDB;";
        private SqlDataReader reader = null;
        private SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;Connect Timeout=30");
        private SqlCommand cmd = null;
        private string sql = null;

        private void setUpdatagridviewAdd()
        {
            dtAdd = new DataTable();
            dtAdd.Columns.AddRange(new DataColumn[4] {
                new DataColumn("Process", typeof(string)),
                new DataColumn("Arrival Time", typeof(int)),
                new DataColumn("Burst Time", typeof(int)),
                new DataColumn("Priority", typeof(int))});
            this.dataGridViewSkedulering.DataSource = dtAdd;
            dataGridViewSkedulering.Columns[0].Width = 60;
            dataGridViewSkedulering.Columns[1].Width = 60;
            dataGridViewSkedulering.Columns[2].Width = 80;
            dataGridViewSkedulering.Columns[3].Width = 80;
        }

        private void setUpdatagridviewTimes()
        {
            dtTimes = new DataTable();
            dtTimes.Columns.AddRange(new DataColumn[6] {
                new DataColumn("Process", typeof(string)),
                new DataColumn("Arrival Time", typeof(int)),
                new DataColumn("Burst Time", typeof(int)),
                new DataColumn("Turnaround Time", typeof(int)),
                new DataColumn("Wait Time", typeof(int)),
                new DataColumn("Reaction time", typeof(int))});
            this.dataGridViewTimes.DataSource = dtTimes;
            dataGridViewTimes.Columns[0].Width = 60;
            dataGridViewTimes.Columns[1].Width = 60;
            dataGridViewTimes.Columns[2].Width = 80;
            dataGridViewTimes.Columns[3].Width = 80;
            dataGridViewTimes.Columns[4].Width = 80;
            dataGridViewTimes.Columns[5].Width = 80;
        }
          public void CreateSqlDatabase(string filename)
          {
              string databaseName = System.IO.Path.GetFileNameWithoutExtension(filename);
              using (conn)
              {
                conn.Open();
                  using (var command = conn.CreateCommand())
                  {
                      command.CommandText =
                          String.Format("CREATE DATABASE {0} ON PRIMARY (NAME={0}, FILENAME='{1}')", databaseName, filename);
                      command.ExecuteNonQuery();

                      command.CommandText =
                          String.Format("EXEC sp_detach_db '{0}', 'true'", databaseName);
                      command.ExecuteNonQuery();

                   // command.CommandText = String.Format("if exists(select * from sys.tables where name like 'Processes')"+
                    //                                    "drop table Processes create table Processes(Process char(5)))");
                   // command.ExecuteNonQuery();

                  }
                sql = "CREATE TABLE myTable" +
           "(Process CHAR(5) PRIMARY KEY," +
           "ArrivalTime INTEGER, BurstTime INTEGER)";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                sql = "INSERT INTO myTable(Process, ArrivalTime, BurstTime) " +
                "VALUES ('A', 0, 5 ) ";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
          }

       /* private void ExecuteSQLStmt(string sql)
         {
             if (conn.State == ConnectionState.Open)
                 conn.Close();
             ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Meyer\\Desktop\\IT\\3de Jaar\\Skedulering\\ThreadsEen\\ThreadsEen\bin\\Debug\\mydb.mdf;Integrated Security=True;Connect Timeout=30";
             conn.ConnectionString = ConnectionString;
             conn.Open();
             cmd = new SqlCommand(sql, conn);
             try
             {
                 cmd.ExecuteNonQuery();
             }
             catch (SqlException ae)
             {
                 MessageBox.Show(ae.Message.ToString());
             }
         }

         private void CreateDB()
         {
             // Create a connection  
             conn = new SqlConnection(ConnectionString);
             // Open the connection  
             if (conn.State != ConnectionState.Open)
                 conn.Open();
            string sql = "CREATE DATABASE mydb ON PRIMARY" + "(Name=test_data, filename = "
                + Application.StartupPath + "'\\mydb.mdf'";
             ExecuteSQLStmt(sql);
         }

         private void CreateTableDB()
         {
             // Open the connection  
             if (conn.State == ConnectionState.Open)
                 conn.Close();
             ConnectionString = "Integrated Security=SSPI;" +"Initial Catalog=mydb;" +"Data Source=localhost;";
             conn.ConnectionString = ConnectionString;
             conn.Open();
             sql = "CREATE TABLE myTable" +
             "(Process CHAR(5) PRIMARY KEY," +
             "ArrivalTime INTEGER, BurstTime INTEGER)";
         }*/

        private void addtoSkedulering(string pname)
        {
            int arrivaltime = Convert.ToInt32(numArrival.Value);
            int bursttime = Convert.ToInt32(num1.Value);
            Random random = new Random();
            Thread tRun = new Thread(() =>
            {
                /*sql = "INSERT INTO myTable(Process, ArrivalTime, BurstTime) " +
                "VALUES (@pname, @arrivaltime,@bursttime) ";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                */
               /* conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Table values('" + pname.ToString() + "','" + arrivaltime.ToString() + "','" + bursttime.ToString() + "')";
                cmd.ExecuteNonQuery();
                conn.Close();*/
                drToAdd = dtAdd.NewRow();
                    drToTimes = dtTimes.NewRow();
                    drToAdd["Process"] = pname;
                    drToAdd["Arrival Time"] = arrivaltime.ToString();
                    drToAdd["Burst Time"] = bursttime.ToString();
                    drToTimes["Process"] = pname;
                    drToTimes["Arrival Time"] = arrivaltime.ToString();
                    drToTimes["Burst Time"] = bursttime.ToString();
                int priority = random.Next(0, 5);
                if (numklik == 1)
                {
                    drToAdd["Priority"] = priority.ToString();
                }
                else
                {
                    for (int i = 0; i < dataGridViewSkedulering.RowCount-1; i++)
                        do
                        {
                            priority = random.Next(0, 5);
                            drToAdd["Priority"] = priority.ToString();
                        } while (dataGridViewSkedulering.Rows[i].Cells["Priority"].Value.ToString() == priority.ToString());
                }
                
                dtAdd.Rows.Add(drToAdd);
                    dtAdd.AcceptChanges();
                dtTimes.Rows.Add(drToTimes);
                dtTimes.AcceptChanges();
            });
            tRun.Start();
        }

        private void addProcesses()
        {
            if (numklik == 0)
            {
                setUpdatagridviewAdd();
                setUpdatagridviewTimes();
            }
            number = Convert.ToInt32(num1.Value);
            for (int i = 0; i < number; i++)
            {
                if (txtLetter.Text == "")
                    txtLetter.Text = "Z";
                    if (numklik == 0)
                    {
                        processname = txtLetter.Text;
                    }
                    else
                    {
                        processname = txtLetter.Text;
                        for (int k = 0; k < dataGridViewSkedulering.RowCount - 1; k++)
                            if (dataGridViewSkedulering.Rows[k].Cells["Process"].Value.ToString() == processname || processname == "")
                                while (dataGridViewSkedulering.Rows[k].Cells["Process"].Value.ToString() == processname || processname == "")
                                {
                                    if (dataGridViewSkedulering.Rows[k].Cells["Process"].Value.ToString() == processname)
                                        processname = Interaction.InputBox("Please enter another process name", "Invalid Process name", "");
                                        txtLetter.Text = processname;
                                }
                    }
                // MessageBox.Show(letter[i].ToString());
                dataGridViewSkedulering.DataSource = dtAdd;
            }
            addtoSkedulering(processname);
            numklik++;
            if (numklik == 10)
            {
                listBox3.Items.Add("MAX Processes");
                btnStart.Enabled = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
             addProcesses();
          
        }

        private void btnRunbut_Click(object sender, EventArgs e)
        {

            maxarrival = dtAdd.AsEnumerable().Max(row => Convert.ToInt32(row["Arrival Time"]));
            maxburst = dtAdd.AsEnumerable().Sum(row => Convert.ToInt32(row["Burst Time"]));
            int maxruntime = maxarrival;
            progressBar1.Maximum = maxburst;
            itel = 0;
            //***********************Round Robin*********************************
            if (rbRoundrobin.Checked == true)
            {
                int i = 0;
                quantum =Convert.ToInt32(numericUpDown1.Value);
                lblq.Text = "Quantum = "+quantum.ToString();
                int p = 0;
                Thread tRun = new Thread(() =>
                {
                    while (dtAdd.AsEnumerable().Sum(row => Convert.ToInt32(row["Burst Time"]))!=0)
                    for ( i = 0; i < dataGridViewSkedulering.RowCount - 1; i++)
                    {
                            done = false;
                            iTwo = 0;
                            //waittel = 0;
                            p++;
                            //int.Parse(dataGridViewSkedulering.Rows[i].Cells["Burst Time"].Value.ToString())
                            for (int j = 0; j <= int.Parse(dataGridViewSkedulering.Rows[i].Cells["Burst Time"].Value.ToString()); j++)
                            {
                                if (int.Parse(dataGridViewSkedulering.Rows[i].Cells["Arrival Time"].Value.ToString()) <= itel)
                                {
                                    if (done == false)
                                    {
                                        if ((int.Parse(dataGridViewSkedulering.Rows[i].Cells["Burst Time"].Value.ToString()) - 1) != -1)
                                        {
                                            Thread.Sleep(500);
                                            listBox3.Items.Add(dataGridViewSkedulering.Rows[i].Cells["Process"].Value.ToString() + listtotal.ToString());
                                            dataGridViewSkedulering.Rows[i].Cells["Burst Time"].Value = (int.Parse(dataGridViewSkedulering.Rows[i].Cells["Burst Time"].Value.ToString()) - 1).ToString();
                                            iTwo++;
                                            if (iTwo == quantum)
                                                done = true;
                                            listtotal++;
                                            itel++;
                                            lbltime.Text = itel.ToString();
                                            progressBar1.Increment(+1);
                                            
                                        }
                                        else
                                        {
                                            waittel++;
                                            if(p < dataGridViewSkedulering.RowCount - 1)
                                            dataGridViewTimes.Rows[i].Cells["Reaction Time"].Value = waittel.ToString();
                                            else
                                            {
                                                Waittime = waittel;
                                                dataGridViewTimes.Rows[i].Cells["Wait Time"].Value = Waittime.ToString();
                                            }
                                            itel++;
                                            lbltime.Text = itel.ToString();
                                            progressBar1.Increment(+1);
                                        }
                                       
                                    }
                                }
                                   
                            }
                            /* Turnaround Time", typeof(int)),
                             new DataColumn("Wait Time", typeof(int)),
                             new DataColumn("Reaction time", typeof(int))});
                             
                              else if(p>=dtAdd.Rows.Count && done==true)
                             {
                                 itel++;
                                 lbltime.Text = itel.ToString();
                                 progressBar1.Increment(+1);
                             }*/
                        }
                    for (i = 0; i < dataGridViewSkedulering.RowCount - 1; i++)
                    {
                        if (dataGridViewTimes.Rows[i].Cells["Wait Time"].Value != null)
                        {
                            Turntime += Convert.ToInt32(dataGridViewTimes.Rows[i].Cells["Wait Time"].Value);
                            dataGridViewTimes.Rows[i].Cells["Turnaround Time"].Value = Turntime.ToString() + dataGridViewTimes.Rows[i].Cells["Burst Time"].Value;
                        }
                        else
                        {
                            dataGridViewTimes.Rows[i].Cells["Wait Time"].Value = "0";
                            dataGridViewTimes.Rows[i].Cells["Turnaround Time"].Value = dataGridViewTimes.Rows[i].Cells["Burst Time"].Value;
                        }
                        }
                        if (progressBar1.Value == itel)
                    {
                        btnRunbut.Enabled = false;
                        btnStart.Enabled = false;
                        listBox3.Items.Add("!!!DONE!!!");
                    }
                });
                tRun.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // CreateDB();
            // CreateTableDB();
            var filename = Application.StartupPath+"\\mydb.mdf";
            if (!System.IO.File.Exists(filename))
            {
                CreateSqlDatabase(filename);
            }
        }
    }
}
