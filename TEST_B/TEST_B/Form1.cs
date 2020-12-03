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

        double UPBIT_BTC_KRW = 1;

        COIN_INFO 업비트_BTC = new COIN_INFO();

        COIN_INFO 업비트_DCR = new COIN_INFO();
        COIN_INFO 바이낸스_DCR = new COIN_INFO();

        COIN_INFO 업비트_XRP = new COIN_INFO();
        COIN_INFO 바이낸스_XRP = new COIN_INFO();

        public Form1()
        {
            InitializeComponent();
            init();
        }


        public COIN_INFO SET_COIN_INFO(거래소 거래소, 종목 종목)
        {
            COIN_INFO temp = new COIN_INFO();
            temp.거래소 = 거래소;
            temp.종목 = 종목;

            return temp;
        }

        public void init()
        {
            try
            {
                myLog.init();
                myLog.write("프로그램 스타트");
                datagridview_init();

                업비트_BTC = SET_COIN_INFO(거래소.UPBIT, 종목.BTC);

                업비트_DCR = SET_COIN_INFO(거래소.UPBIT, 종목.DCR);
                바이낸스_DCR = SET_COIN_INFO(거래소.BINANCE, 종목.DCR);

                업비트_XRP = SET_COIN_INFO(거래소.UPBIT, 종목.XRP);
                바이낸스_XRP = SET_COIN_INFO(거래소.BINANCE, 종목.XRP);




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
            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 0, 0, "BTC(KRW)");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double result =  CALL_API_UPBIT("https://api.upbit.com/v1/ticker?markets=KRW-DCR");
            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 1, 1, string.Format("{0:#,###}", result));

            업비트_DCR.현재가_원 = result;

            double result_ = CALL_API_UPBIT("https://api.upbit.com/v1/ticker?markets=KRW-XRP");
            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 1, 2, string.Format("{0:#,###}", result_));

            업비트_XRP.현재가_원 = result_;

        }

        public double CALL_API_UPBIT(string URL)
        {
            string retult = BINANCE_CALLER.callWebClient(URL);
            Console.WriteLine(retult);

            var JARR = JArray.Parse(retult);

            string LAST_PRICE = JARR[0]["trade_price"].ToString();

            return Convert.ToDouble(LAST_PRICE);
        }

        public double CALL_API_BINANCE(string URL)
        {
            string retult = BINANCE_CALLER.callWebClient(URL);
            Console.WriteLine(retult);

            var JARR = JObject.Parse(retult);

            string LAST_PRICE = JARR["asks"][0][0].ToString();
            
            return Convert.ToDouble(LAST_PRICE);
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
            double API_RESULT = CALL_API_BINANCE("https://api.binance.com/api/v3/depth?symbol=DCRBTC&limit=5");
            double result = UPBIT_BTC_KRW * API_RESULT;

            바이낸스_DCR.현재가_원 = result;

            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 2, 1, string.Format("{0:#,##0.#######}", result));


            double API_RESULT_ = CALL_API_BINANCE("https://api.binance.com/api/v3/depth?symbol=XRPBTC&limit=5");
            double result_ = UPBIT_BTC_KRW * API_RESULT_;
            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 2, 2, string.Format("{0:#,##0.#######}", result_));
            바이낸스_XRP.현재가_원 = result_;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            double result = CALL_API_UPBIT("https://api.upbit.com/v1/ticker?markets=KRW-BTC");
            UPBIT_BTC_KRW = result;
            업비트_BTC.현재가_원 = UPBIT_BTC_KRW;

            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 1, 0, string.Format("{0:#,###}", result));

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //myLog.Log_Write(this, "TEST");
            
            myLog.write("TEST");
        }

        private void button5_Click(object sender, EventArgs e)
        {

            double 차액 = 업비트_DCR.현재가_원 - 바이낸스_DCR.현재가_원;

            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 3, 1, string.Format("{0:#,###}", 차액));

            double 차액_ = 업비트_XRP.현재가_원 - 바이낸스_XRP.현재가_원;
            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 3, 2, string.Format("{0:#,###0.##}", 차액_));
        }
    }
}
