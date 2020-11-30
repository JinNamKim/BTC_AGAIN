using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace TEST_B
{
    public partial class Form1 : Form
    {

        public CALL_API BINANCE_CALLER = new CALL_API();
        public Thread th_up;
        public Thread th_binan;

        public Form1()
        {
            InitializeComponent();
            init();
            
        }

        public void init()
        {
            try
            {
                myLog.init();
                myLog.write("프로그램 스타트");
                datagridview_init();
                th_up = new Thread(new ThreadStart(up_th));
                th_binan = new Thread(new ThreadStart(binan_th));

                th_up.Start();
                th_binan.Start();

            }
            catch(Exception ex)
            {

            }
            finally
            {

            }
        }

        public void up_th()
        {

        }

        public void binan_th()
        {

        }

        public void datagridview_init()
        {
            //dataGridView1.Rows.Add();
            InvokeFunction.DataGridView_Rows_Add(dataGridView1);
            InvokeFunction.DataGridView_Rows_Add(dataGridView1);
            InvokeFunction.DataGridView_Rows_Add(dataGridView1);
            InvokeFunction.DataGridView_Rows_Add(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //BINANCE
            //https://binance-docs.github.io/apidocs/spot/en/#order-book

            string ur = "https://api.binance.com/api/v3/depth?symbol=LTCBTC&limit=5";
            //string ur = "https://api.upbit.com/v1/orderbook?markets=krw-btc";
        
            string retult = BINANCE_CALLER.callWebClient(ur);
            Console.WriteLine(retult);

            var json = new JObject();

            var json5 = JObject.Parse(retult);
            //Console.WriteLine(json5.ToString());
            Console.WriteLine(json5["bids"]);

            Console.WriteLine(json5["bids"][0]);

        }

        public static string callWebClient(string targetURL)
        {
            string result = string.Empty;
            try
            {
                WebClient client = new WebClient();

                //특정 요청 헤더값을 추가해준다. 
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                using (Stream data = client.OpenRead(targetURL))
                {
                    using (StreamReader reader = new StreamReader(data))
                    {
                        string s = reader.ReadToEnd();
                        result = s;

                        reader.Close();
                        data.Close();
                    }
                }

            }
            catch (Exception e)
            {
                //통신 실패시 처리로직
                Console.WriteLine(e.ToString());
            }
            return result;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                myLog.LOG_CLOSE();
            }
            catch(Exception ex)
            {
                myLog.LOG_CLOSE();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("TEST");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows[1].SetValues("KKK");
            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 1, 1, "TESSSS");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
