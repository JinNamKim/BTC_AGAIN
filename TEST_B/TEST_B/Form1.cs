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

        public Thread Main_thread;

        double UPBIT_BTC_KRW = 1;

        COIN_INFO 업비트_BTC = new COIN_INFO();

        COIN_INFO 업비트_DCR = new COIN_INFO();
        COIN_INFO 바이낸스_DCR = new COIN_INFO();

        COIN_INFO 업비트_XRP = new COIN_INFO();
        COIN_INFO 바이낸스_XRP = new COIN_INFO();

        COIN_INFO 업비트_ZIL = new COIN_INFO();
        COIN_INFO 바이낸스_ZIL = new COIN_INFO();

        COIN_INFO 업비트_XLM = new COIN_INFO();
        COIN_INFO 바이낸스_XLM = new COIN_INFO();

        COIN_INFO 업비트_ADA = new COIN_INFO();
        COIN_INFO 바이낸스_ADA = new COIN_INFO();

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

            if(temp.거래소 == 거래소.UPBIT)
            {
                temp.URL = string.Format("https://api.upbit.com/v1/ticker?markets=KRW-{0}", temp.종목.ToString());
            }
            else if(temp.거래소 == 거래소.BINANCE)
            {
                temp.URL = string.Format("https://api.binance.com/api/v3/depth?symbol={0}BTC&limit=5", temp.종목.ToString());
            }

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

                업비트_ZIL = SET_COIN_INFO(거래소.UPBIT, 종목.ZIL);
                바이낸스_ZIL = SET_COIN_INFO(거래소.BINANCE, 종목.ZIL);

                업비트_XLM = SET_COIN_INFO(거래소.UPBIT, 종목.XLM);
                바이낸스_XLM = SET_COIN_INFO(거래소.BINANCE, 종목.XLM);

                업비트_ADA = SET_COIN_INFO(거래소.UPBIT, 종목.ADA);
                바이낸스_ADA = SET_COIN_INFO(거래소.BINANCE, 종목.ADA);


                th_up = new Thread(new ThreadStart(up_th));
                th_binan = new Thread(new ThreadStart(binan_th));

                th_up.Start();
                th_binan.Start();

                Main_thread = new Thread(new ThreadStart(F_Main));
                //Main_thread.Start();

            }
            catch(Exception ex)
            {
                
            }
            finally
            {

            }
        }

        public void F_Main()
        {
            try
            {
                while(true)
                {
                    Thread.Sleep(1000 * 1);

                    Call_UPBIT();
                    Thread.Sleep(200 * 1);

                    Call_BINANCE();
                    Thread.Sleep(200 * 1);

                    Calc_Diff();
                    Thread.Sleep(200 * 1);

                }
            }
            catch(Exception ex)
            {

            }
        }

        public void Call_UPBIT()
        {
            try
            {
                Thread.Sleep(200 * 1);
                GET_COIN_INFOMATION(업비트_BTC);

                Thread.Sleep(200 * 1);
                GET_COIN_INFOMATION(업비트_DCR);

                Thread.Sleep(200 * 1);
                GET_COIN_INFOMATION(업비트_XRP);

                Thread.Sleep(200 * 1);
                GET_COIN_INFOMATION(업비트_ZIL);

                Thread.Sleep(200 * 1);
                GET_COIN_INFOMATION(업비트_XLM);

                Thread.Sleep(200 * 1);
                GET_COIN_INFOMATION(업비트_ADA);
            }
            catch(Exception ex)
            {

            }
            
        }

        public void Call_BINANCE()
        {
            try
            {
                Thread.Sleep(200 * 1);
                GET_COIN_INFOMATION(바이낸스_DCR);

                Thread.Sleep(200 * 1);
                GET_COIN_INFOMATION(바이낸스_XRP);

                Thread.Sleep(200 * 1);
                GET_COIN_INFOMATION(바이낸스_ZIL);

                Thread.Sleep(200 * 1);
                GET_COIN_INFOMATION(바이낸스_XLM);

                Thread.Sleep(200 * 1);
                GET_COIN_INFOMATION(바이낸스_ADA);
            }
            catch (Exception ex)
            {

            }
        }

        public void Calc_Diff()
        {
            try
            {
                GET_COIN_DIFF(종목.DCR);
                Thread.Sleep(200 * 1);

                GET_COIN_DIFF(종목.XRP);
                Thread.Sleep(200 * 1);

                GET_COIN_DIFF(종목.ZIL);
                Thread.Sleep(200 * 1);

                GET_COIN_DIFF(종목.XLM);
                Thread.Sleep(200 * 1);

                GET_COIN_DIFF(종목.ADA);
                Thread.Sleep(200 * 1);
            }
            catch(Exception ex)
            {

            }
        }

        public void GET_COIN_DIFF(종목 종목)
        {
            try
            {
                int ROW = 3;
                int 차액_PERCENTAGE = 4;
                double 차액 = 0;
                double 퍼센테이지 = 0;
                switch (종목)
                {
                    case 종목.DCR:
                        차액 = 업비트_DCR.현재가_원 - 바이낸스_DCR.현재가_원;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));

                        퍼센테이지 = Math.Abs(차액) / 업비트_DCR.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));
                        break;

                    case 종목.XRP:
                        차액 = 업비트_XRP.현재가_원 - 바이낸스_XRP.현재가_원;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));

                        퍼센테이지 = Math.Abs(차액) / 업비트_XRP.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));
                        break;

                    case 종목.ZIL:
                        차액 = 업비트_ZIL.현재가_원 - 바이낸스_ZIL.현재가_원;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));

                        퍼센테이지 = Math.Abs(차액) / 업비트_ZIL.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));
                        break;

                    case 종목.XLM:
                        차액 = 업비트_XLM.현재가_원 - 바이낸스_XLM.현재가_원;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));

                        퍼센테이지 = Math.Abs(차액) / 업비트_XLM.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));
                        break;

                    case 종목.ADA:
                        차액 = 업비트_ADA.현재가_원 - 바이낸스_ADA.현재가_원;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));

                        퍼센테이지 = Math.Abs(차액) / 업비트_ADA.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void GET_PERCENTAGE(종목 종목)
        {
            try
            {
                int 차액_X = 3;
                int 차액_PERCENTAGE = 4;
                double 차액 = 0;
                double 퍼센테이지 = 0;
                double MAX = 0;
                double MIN = 0;
                switch (종목)
                {
                    case 종목.DCR:
                        if(업비트_DCR.현재가_원 >= 바이낸스_DCR.현재가_원)
                        {
                            MAX = 업비트_DCR.현재가_원;
                            MIN = 바이낸스_DCR.현재가_원;
                        }
                        else
                        {
                            MIN = 업비트_DCR.현재가_원;
                            MAX = 바이낸스_DCR.현재가_원;
                        }
                        차액 = 업비트_DCR.현재가_원 - 바이낸스_DCR.현재가_원;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_X, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));

                        퍼센테이지 = Math.Abs(차액) / 업비트_DCR.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));
                        break;

                    case 종목.XRP:
                        차액 = 업비트_XRP.현재가_원 - 바이낸스_XRP.현재가_원;
                        퍼센테이지 = Math.Abs(차액) / 업비트_XRP.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_X, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));
                        break;

                    case 종목.ZIL:
                        차액 = 업비트_ZIL.현재가_원 - 바이낸스_ZIL.현재가_원;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_X, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));

                        퍼센테이지 = Math.Abs(차액) / 업비트_ZIL.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));
                        break;

                    case 종목.XLM:
                        차액 = 업비트_XLM.현재가_원 - 바이낸스_XLM.현재가_원;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_X, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));

                        퍼센테이지 = Math.Abs(차액) / 업비트_XLM.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));
                        break;
                }
            }
            catch (Exception ex)
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
            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 0, 0, "BTC(KRW)");

            InvokeFunction.DataGridView_Rows_Add(dataGridView1);
            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 0, Convert.ToInt32(종목.DCR), string.Format(종목.DCR.ToString()));

            InvokeFunction.DataGridView_Rows_Add(dataGridView1);
            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 0, Convert.ToInt32(종목.XRP), string.Format(종목.XRP.ToString()));

            InvokeFunction.DataGridView_Rows_Add(dataGridView1);
            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 0, Convert.ToInt32(종목.ZIL), string.Format(종목.ZIL.ToString()));

            InvokeFunction.DataGridView_Rows_Add(dataGridView1);
            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 0, Convert.ToInt32(종목.XLM), string.Format(종목.XLM.ToString()));

            InvokeFunction.DataGridView_Rows_Add(dataGridView1);
            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 0, Convert.ToInt32(종목.ADA), string.Format(종목.ADA.ToString()));

            for (int i = 1; i <= Enum.GetNames(typeof(종목)).Length; i++)
            {
                //InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 0, Convert.ToInt32(Enum.GetNames(typeof(종목).i ), string.Format(종목.DCR.ToString()));
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //double result =  CALL_API_UPBIT("https://api.upbit.com/v1/ticker?markets=KRW-DCR");
            //InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 1, 1, string.Format("{0:#,###}", result));

            //업비트_DCR.현재가_원 = result;

            GET_COIN_INFOMATION(업비트_DCR);

            //double result_ = CALL_API_UPBIT("https://api.upbit.com/v1/ticker?markets=KRW-XRP");
            //InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 1, 2, string.Format("{0:#,###}", result_));

            //업비트_XRP.현재가_원 = result_;
            GET_COIN_INFOMATION(업비트_XRP);

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

            //double result = UPBIT_BTC_KRW * API_RESULT;

            double result = Convert.ToDouble(LAST_PRICE) * UPBIT_BTC_KRW;

            //return Convert.ToDouble(LAST_PRICE);
            return result;
        }

        public double CALL_API(COIN_INFO coinInfo)
        {
            double result = -111;
            if(coinInfo.거래소 == 거래소.UPBIT)
            {
                string retult = BINANCE_CALLER.callWebClient(coinInfo.URL);
                Console.WriteLine(retult);

                var JARR = JArray.Parse(retult);

                string LAST_PRICE = JARR[0]["trade_price"].ToString();
                result = Convert.ToDouble(LAST_PRICE);

                if (coinInfo.종목 == 종목.BTC)
                    UPBIT_BTC_KRW = result;

                //return result;
            }
            else if(coinInfo.거래소 == 거래소.BINANCE)
            {
                string retult = BINANCE_CALLER.callWebClient(coinInfo.URL);
                Console.WriteLine(retult);

                var JARR = JObject.Parse(retult);

                string LAST_PRICE = JARR["asks"][0][0].ToString();

                //double result = UPBIT_BTC_KRW * API_RESULT;

                //double result = Convert.ToDouble(LAST_PRICE) * UPBIT_BTC_KRW;
                result = Convert.ToDouble(LAST_PRICE) * UPBIT_BTC_KRW;

                //return Convert.ToDouble(LAST_PRICE);
                //return result;
            }
            return result;
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
                Main_thread.Abort();
            }
            catch(Exception ex)
            {
                myLog.LOG_CLOSE();
                Main_thread.Abort();
            }
        }

        public void GET_COIN_INFOMATION(COIN_INFO coininfo)
        {
            coininfo.현재가_원 = CALL_API(coininfo);
            Draw_GridView(coininfo);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            GET_COIN_INFOMATION(바이낸스_DCR);
            
            GET_COIN_INFOMATION(바이낸스_XRP);
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            GET_COIN_INFOMATION(업비트_BTC);

        }
        
        public void Draw_GridView(COIN_INFO coinInfo)
        {
            try
            {
                InvokeFunction.DataGridView_Rows_SetText(dataGridView1, Convert.ToInt32(coinInfo.거래소), Convert.ToInt32(coinInfo.종목), string.Format("{0:#,###0.##}", coinInfo.현재가_원));
            }
            catch(Exception ex)
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //myLog.Log_Write(this, "TEST");

            //myLog.write("TEST");
            Main_thread.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            double 차액 = 업비트_DCR.현재가_원 - 바이낸스_DCR.현재가_원;

            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 3, 1, string.Format("{0:#,###}", 차액));

            double 차액_ = 업비트_XRP.현재가_원 - 바이낸스_XRP.현재가_원;
            InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 3, 2, string.Format("{0:#,###0.##}", 차액_));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double result;
            result = Convert.ToDouble(textBox1.Text) * UPBIT_BTC_KRW;
            label1.Text = string.Format("{0:#,###0.##}", result);


        }
    }
}
