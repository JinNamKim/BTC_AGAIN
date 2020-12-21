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
using Telegram.Bot;

namespace TEST_B
{
    public partial class Form1 : Form
    {

        int DELAY = 100;

        int ROW = 3+3;
        int 차액_PERCENTAGE = 4+3;
        int ROW_리턴금액 = 5 + 3;
        int ROW_퍼센테이지변동 = 6 + 3;

        public CALL_API BINANCE_CALLER = new CALL_API();

        public Thread th_up;
        public Thread th_binan;

        public Thread Main_thread;
        public Thread Sub_thread;

        public Thread UPBIT_thread;
        public Thread BINANCE_thread;
        public Thread BITTREX_thread;
        public Thread HUOBI_thread;
        public Thread BITHUMB_thread;

        public Thread DIFF_thread;

        double UPBIT_BTC_KRW = 1;

        List<List<COIN_INFO>> TOTAL_LIST = new List<List<COIN_INFO>>();

        List<COIN_INFO> UPBIT_INFO_LIST = new List<COIN_INFO>();
        List<COIN_INFO> BINANCE_INFO_LIST = new List<COIN_INFO>();
        List<COIN_INFO> BITTREX_INFO_LIST = new List<COIN_INFO>();
        List<COIN_INFO> HUOBI_INFO_LIST = new List<COIN_INFO>();
        List<COIN_INFO> BITHUMB_INFO_LIST = new List<COIN_INFO>();


        /*
        COIN_INFO 업비트_BTC = new COIN_INFO();

        COIN_INFO 업비트_DCR = new COIN_INFO();
        COIN_INFO 바이낸스_DCR = new COIN_INFO();
        COIN_INFO 비트렉스_DCR = new COIN_INFO();

        COIN_INFO 업비트_XRP = new COIN_INFO();
        COIN_INFO 바이낸스_XRP = new COIN_INFO();
        COIN_INFO 비트렉스_XRP = new COIN_INFO();

        COIN_INFO 업비트_ZIL = new COIN_INFO();
        COIN_INFO 바이낸스_ZIL = new COIN_INFO();
        COIN_INFO 비트렉스_ZIL = new COIN_INFO();

        COIN_INFO 업비트_XLM = new COIN_INFO();
        COIN_INFO 바이낸스_XLM = new COIN_INFO();
        COIN_INFO 비트렉스_XLM = new COIN_INFO();

        COIN_INFO 업비트_ADA = new COIN_INFO();
        COIN_INFO 바이낸스_ADA = new COIN_INFO();
        COIN_INFO 비트렉스_ADA = new COIN_INFO();

        COIN_INFO 업비트_CVC = new COIN_INFO();
        COIN_INFO 바이낸스_CVC = new COIN_INFO();
        COIN_INFO 비트렉스_CVC = new COIN_INFO();

        COIN_INFO 업비트_XEM = new COIN_INFO();
        COIN_INFO 바이낸스_XEM = new COIN_INFO();
        COIN_INFO 비트렉스_XEM = new COIN_INFO();

        COIN_INFO 업비트_ETH = new COIN_INFO();
        COIN_INFO 바이낸스_ETH = new COIN_INFO();
        COIN_INFO 비트렉스_ETH = new COIN_INFO();

        COIN_INFO 업비트_NEO = new COIN_INFO();
        COIN_INFO 바이낸스_NEO = new COIN_INFO();
        COIN_INFO 비트렉스_NEO = new COIN_INFO();

        COIN_INFO 업비트_MTL = new COIN_INFO();
        COIN_INFO 바이낸스_MTL = new COIN_INFO();
        COIN_INFO 비트렉스_MTL = new COIN_INFO();

        COIN_INFO 업비트_LBC = new COIN_INFO();
        COIN_INFO 바이낸스_LBC = new COIN_INFO();
        COIN_INFO 비트렉스_LBC = new COIN_INFO();

        COIN_INFO 업비트_TFUEL = new COIN_INFO();
        COIN_INFO 바이낸스_TFUEL = new COIN_INFO();
        COIN_INFO 비트렉스_TFUEL = new COIN_INFO();

        COIN_INFO 업비트_ENJ = new COIN_INFO();
        COIN_INFO 바이낸스_ENJ = new COIN_INFO();
        COIN_INFO 비트렉스_ENJ = new COIN_INFO();

        COIN_INFO 업비트_LTC = new COIN_INFO();
        COIN_INFO 바이낸스_LTC = new COIN_INFO();
        COIN_INFO 비트렉스_LTC = new COIN_INFO();
        */

        /*
        COIN_INFO 업비트_RFR = new COIN_INFO();
        COIN_INFO 바이낸스_RFR = new COIN_INFO();
        COIN_INFO 비트렉스_RFR = new COIN_INFO();
        */


        public Form1()
        {
            InitializeComponent();
            init();
        }


        public void SET_COIN_INFO_V2()
        {

            for (int i = 0; i < Enum.GetNames(typeof(종목)).Length; i++)
            {
                COIN_INFO temp_1 = new COIN_INFO();
                COIN_INFO temp_2 = new COIN_INFO();
                COIN_INFO temp_3 = new COIN_INFO();
                COIN_INFO temp_4 = new COIN_INFO();
                COIN_INFO temp_5 = new COIN_INFO();

                temp_1.거래소 = 거래소.UPBIT;
                temp_2.거래소 = 거래소.BINANCE;
                temp_3.거래소 = 거래소.BITTREX;
                temp_4.거래소 = 거래소.HUOBI;
                temp_5.거래소 = 거래소.BITHUMB;



                //temp_1.종목 = (종목)Enum.GetNames(typeof(종목)).GetValue(i);
                temp_1.종목 = (종목)Enum.ToObject(typeof(종목), i);
                temp_2.종목 = (종목)Enum.ToObject(typeof(종목), i);
                temp_3.종목 = (종목)Enum.ToObject(typeof(종목), i);
                temp_4.종목 = (종목)Enum.ToObject(typeof(종목), i);
                temp_5.종목 = (종목)Enum.ToObject(typeof(종목), i);

                //temp_1.URL = string.Format("https://api.upbit.com/v1/ticker?markets=KRW-{0}", temp_1.종목.ToString());
                temp_1.URL = string.Format("https://api.upbit.com/v1/candles/minutes/3?market=KRW-{0}&count=1", temp_1.종목.ToString());
                UPBIT_INFO_LIST.Add(temp_1);

                temp_2.URL = string.Format("https://api.binance.com/api/v3/depth?symbol={0}BTC&limit=5", temp_2.종목.ToString());
                BINANCE_INFO_LIST.Add(temp_2);

                temp_3.URL = string.Format("https://api.bittrex.com/api/v1.1/public/getticker?market=BTC-{0}", temp_3.종목.ToString());
                BITTREX_INFO_LIST.Add(temp_3);

                temp_4.URL = string.Format("https://api.huobi.pro/market/depth?symbol={0}btc&type=step0",  temp_4.종목.ToString().ToLower());
                HUOBI_INFO_LIST.Add(temp_4);

                temp_5.URL = string.Format("https://api.bithumb.com/public/orderbook/{0}_KRW", temp_5.종목.ToString());
                BITHUMB_INFO_LIST.Add(temp_5);
            }

            
        }

        public COIN_INFO SET_COIN_INFO(ref COIN_INFO Info, 거래소 거래소, 종목 종목)
        {
            COIN_INFO temp = new COIN_INFO();
            temp.거래소 = 거래소;
            temp.종목 = 종목;


            Info.거래소 = 거래소;
            Info.종목 = 종목;

            if (temp.거래소 == 거래소.UPBIT)
            {
                temp.URL = string.Format("https://api.upbit.com/v1/ticker?markets=KRW-{0}", temp.종목.ToString());
                Info.URL = string.Format("https://api.upbit.com/v1/ticker?markets=KRW-{0}", temp.종목.ToString());
                UPBIT_INFO_LIST.Add(Info);
            }
            else if(temp.거래소 == 거래소.BINANCE)
            {
                temp.URL = string.Format("https://api.binance.com/api/v3/depth?symbol={0}BTC&limit=5", temp.종목.ToString());
                Info.URL = string.Format("https://api.binance.com/api/v3/depth?symbol={0}BTC&limit=5", temp.종목.ToString());
                BINANCE_INFO_LIST.Add(Info);
            }
            else if(temp.거래소 == 거래소.BITTREX)
            {
                //https://api.bittrex.com/api/v1.1/public/getticker?market=BTC-xrp
                temp.URL = string.Format("https://api.bittrex.com/api/v1.1/public/getticker?market=BTC-{0}", temp.종목.ToString());
                Info.URL = string.Format("https://api.bittrex.com/api/v1.1/public/getticker?market=BTC-{0}", temp.종목.ToString());
                BITTREX_INFO_LIST.Add(Info);
            }

            else if (temp.거래소 == 거래소.HUOBI)
            {
                //https://api.bittrex.com/api/v1.1/public/getticker?market=BTC-xrp
                temp.URL = string.Format("https://api.huobi.pro/market/depth?symbol={0}btc&type=step0", temp.종목.ToString());
                Info.URL = string.Format("https://api.huobi.pro/market/depth?symbol={0}btc&type=step0", temp.종목.ToString());
                HUOBI_INFO_LIST.Add(Info);
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


                SET_COIN_INFO_V2();

                //업비트_BTC = SET_COIN_INFO(ref 업비트_BTC, 거래소.UPBIT, 종목.BTC);
                

                /*
                업비트_DCR = SET_COIN_INFO(ref 업비트_DCR, 거래소.UPBIT, 종목.DCR);
                바이낸스_DCR = SET_COIN_INFO(ref 바이낸스_DCR, 거래소.BINANCE, 종목.DCR);
                비트렉스_DCR = SET_COIN_INFO(ref 비트렉스_DCR, 거래소.BITTREX, 종목.DCR);
                

                업비트_XRP = SET_COIN_INFO(ref 업비트_XRP, 거래소.UPBIT, 종목.XRP);
                바이낸스_XRP = SET_COIN_INFO(ref 바이낸스_XRP, 거래소.BINANCE, 종목.XRP);
                비트렉스_XRP = SET_COIN_INFO(ref 비트렉스_XRP, 거래소.BITTREX, 종목.XRP);

                업비트_ZIL = SET_COIN_INFO(ref 업비트_ZIL, 거래소.UPBIT, 종목.ZIL);
                바이낸스_ZIL = SET_COIN_INFO(ref 바이낸스_ZIL, 거래소.BINANCE, 종목.ZIL);
                비트렉스_ZIL = SET_COIN_INFO(ref 비트렉스_ZIL,거래소.BITTREX, 종목.ZIL);

                업비트_XLM = SET_COIN_INFO(ref 업비트_XLM, 거래소.UPBIT, 종목.XLM);
                바이낸스_XLM = SET_COIN_INFO(ref 바이낸스_XLM, 거래소.BINANCE, 종목.XLM);
                비트렉스_XLM = SET_COIN_INFO(ref 비트렉스_XLM, 거래소.BITTREX, 종목.XLM);

                업비트_ADA = SET_COIN_INFO(ref 업비트_ADA, 거래소.UPBIT, 종목.ADA);
                바이낸스_ADA = SET_COIN_INFO(ref 바이낸스_ADA, 거래소.BINANCE, 종목.ADA);
                비트렉스_ADA = SET_COIN_INFO(ref 비트렉스_ADA, 거래소.BITTREX, 종목.ADA);

                업비트_CVC = SET_COIN_INFO(ref 업비트_CVC, 거래소.UPBIT, 종목.CVC);
                바이낸스_CVC = SET_COIN_INFO(ref 바이낸스_CVC, 거래소.BINANCE, 종목.CVC);
                비트렉스_CVC = SET_COIN_INFO(ref 비트렉스_CVC, 거래소.BITTREX, 종목.CVC);

                업비트_XEM = SET_COIN_INFO(ref 업비트_XEM, 거래소.UPBIT, 종목.XEM);
                바이낸스_XEM = SET_COIN_INFO(ref 바이낸스_XEM, 거래소.BINANCE, 종목.XEM);
                비트렉스_XEM = SET_COIN_INFO(ref 비트렉스_XEM, 거래소.BITTREX, 종목.XEM);

                업비트_ETH = SET_COIN_INFO(ref 업비트_ETH, 거래소.UPBIT, 종목.ETH);
                바이낸스_ETH = SET_COIN_INFO(ref 바이낸스_ETH, 거래소.BINANCE, 종목.ETH);
                비트렉스_ETH = SET_COIN_INFO(ref 비트렉스_ETH, 거래소.BITTREX, 종목.ETH);


                업비트_NEO = SET_COIN_INFO(ref 업비트_NEO, 거래소.UPBIT, 종목.NEO);
                바이낸스_NEO = SET_COIN_INFO(ref 바이낸스_NEO, 거래소.BINANCE, 종목.NEO);
                비트렉스_NEO = SET_COIN_INFO(ref 비트렉스_NEO, 거래소.BITTREX, 종목.NEO);

                업비트_MTL = SET_COIN_INFO(ref 업비트_MTL, 거래소.UPBIT, 종목.MTL);
                바이낸스_MTL = SET_COIN_INFO(ref 바이낸스_MTL, 거래소.BINANCE, 종목.MTL);
                비트렉스_MTL = SET_COIN_INFO(ref 비트렉스_MTL, 거래소.BITTREX, 종목.MTL);

                업비트_LBC = SET_COIN_INFO(ref 업비트_LBC, 거래소.UPBIT, 종목.LBC);
                바이낸스_LBC = SET_COIN_INFO(ref 바이낸스_LBC, 거래소.BINANCE, 종목.LBC);
                비트렉스_LBC = SET_COIN_INFO(ref 비트렉스_LBC, 거래소.BITTREX, 종목.LBC);

                업비트_TFUEL = SET_COIN_INFO(ref 업비트_TFUEL, 거래소.UPBIT, 종목.TFUEL);
                바이낸스_TFUEL = SET_COIN_INFO(ref 바이낸스_TFUEL, 거래소.BINANCE, 종목.TFUEL);
                비트렉스_TFUEL = SET_COIN_INFO(ref 비트렉스_TFUEL, 거래소.BITTREX, 종목.TFUEL);

                업비트_ENJ = SET_COIN_INFO(ref 업비트_ENJ, 거래소.UPBIT, 종목.ENJ);
                바이낸스_ENJ = SET_COIN_INFO(ref 바이낸스_ENJ, 거래소.BINANCE, 종목.ENJ);
                비트렉스_ENJ = SET_COIN_INFO(ref 비트렉스_ENJ, 거래소.BITTREX, 종목.ENJ);

                업비트_LTC = SET_COIN_INFO(ref 업비트_LTC, 거래소.UPBIT, 종목.LTC);
                바이낸스_LTC = SET_COIN_INFO(ref 바이낸스_LTC, 거래소.BINANCE, 종목.LTC);
                비트렉스_LTC = SET_COIN_INFO(ref 비트렉스_LTC, 거래소.BITTREX, 종목.LTC);
                */

                /*
                업비트_RFR = SET_COIN_INFO(거래소.UPBIT, 종목.RFR);
                바이낸스_RFR = SET_COIN_INFO(거래소.BINANCE, 종목.RFR);
                비트렉스_RFR = SET_COIN_INFO(거래소.BITTREX, 종목.RFR);
                */

                th_up = new Thread(new ThreadStart(up_th));
                th_binan = new Thread(new ThreadStart(binan_th));

                th_up.Start();
                th_binan.Start();

                TOTAL_LIST.Add(UPBIT_INFO_LIST);
                TOTAL_LIST.Add(BINANCE_INFO_LIST);
                TOTAL_LIST.Add(BITTREX_INFO_LIST);
                TOTAL_LIST.Add(HUOBI_INFO_LIST);
                TOTAL_LIST.Add(BITHUMB_INFO_LIST);

                Main_thread = new Thread(new ThreadStart(F_Main));

                Sub_thread = new Thread(new ThreadStart(F_Sub));

                UPBIT_thread = new Thread(new ThreadStart(F_UPBIT));
                BINANCE_thread = new Thread(new ThreadStart(F_BINANCE));
                BITTREX_thread = new Thread(new ThreadStart(F_BITTREX));
                HUOBI_thread = new Thread(new ThreadStart(F_HUOBI));
                BITHUMB_thread= new Thread(new ThreadStart(F_BITHUMB));

                DIFF_thread = new Thread(new ThreadStart(F_DIFF));
                //Main_thread.Start();

            }
            catch(Exception ex)
            {
                
            }
            finally
            {

            }
        }

        public void F_DIFF()
        {
            try
            {
                while (true)
                {
                    for (int i = 0; i < Enum.GetNames(typeof(종목)).Length; i++)
                    {
                        COIN_INFO temp_upbit = UPBIT_INFO_LIST.Find(x => x.종목 == (종목)Enum.ToObject(typeof(종목), i));
                        COIN_INFO temp_binance = BINANCE_INFO_LIST.Find(x => x.종목 == (종목)Enum.ToObject(typeof(종목), i));
                        COIN_INFO temp_bittrex = BITTREX_INFO_LIST.Find(x => x.종목 == (종목)Enum.ToObject(typeof(종목), i));
                        COIN_INFO temp_huobi = HUOBI_INFO_LIST.Find(x => x.종목 == (종목)Enum.ToObject(typeof(종목), i));

                        //if(temp_upbit.CHECK && temp_binance.CHECK && temp_bittrex.CHECK && temp_huobi.CHECK)
                        if(true)
                        {
                            GET_DIFF(TOTAL_LIST, Enum.GetNames(typeof(종목)).GetValue(i).ToString());
                            temp_upbit.CHECK = false;
                            temp_binance.CHECK = false;
                            temp_bittrex.CHECK = false;
                            temp_huobi.CHECK = false;
                        }



                    }
                      


                    //Calc_Diff();
                    //Thread.Sleep(DELAY * 1);
                    
                }
            }
            catch (Exception ex)
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
                    Thread.Sleep(DELAY * 1);

                    Call_BINANCE();
                    Thread.Sleep(DELAY * 1);

                    /*
                    Call_BITTREX();
                    Thread.Sleep(DELAY * 1);

                    Call_HUOBI(); 
                    Thread.Sleep(DELAY * 1);


                    Calc_Diff();
                    Thread.Sleep(DELAY * 1);
                    */
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void F_Sub()
        {
            try
            {
                while (true)
                {
                    /*
                    Thread.Sleep(1000 * 1);

                    Call_UPBIT();
                    Thread.Sleep(DELAY * 1);

                    Call_BINANCE();
                    Thread.Sleep(DELAY * 1);
                    */
                    Call_BITTREX();
                    Thread.Sleep(DELAY * 1);

                    Call_HUOBI();
                    Thread.Sleep(DELAY * 1);


                    Calc_Diff();
                    Thread.Sleep(DELAY * 1);

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void F_UPBIT()
        {
            try
            {
                while (true)
                {
                    DELAY = Convert.ToInt32(textBox5.Text);
                    Call_UPBIT();
                    Thread.Sleep(DELAY * 1);

                    //Calc_Diff();
                    //Thread.Sleep(DELAY * 1);

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void F_BINANCE()
        {
            try
            {
                while (true)
                {
                    Call_BINANCE();
                    Thread.Sleep(DELAY * 1);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void F_BITTREX()
        {
            try
            {
                while (true)
                {
                    Call_BITTREX();
                    Thread.Sleep(DELAY * 1);

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void F_HUOBI()
        {
            try
            {
                while (true)
                {
                    Call_HUOBI();
                    Thread.Sleep(DELAY * 1);

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void F_BITHUMB()
        {
            try
            {
                while (true)
                {
                    Call_BITHUMB();
                    Thread.Sleep(DELAY * 1);

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void Call_UPBIT()
        {
            try
            {
                for(int i = 0; i < UPBIT_INFO_LIST.Count(); i++)
                {
                    try
                    {
                        Thread.Sleep(DELAY * 1);
                        GET_COIN_INFOMATION(UPBIT_INFO_LIST[i]);
                    }
                    catch(Exception ex)
                    {

                    }
                    finally
                    {
                        UPBIT_INFO_LIST[i].CHECK = true;
                    }

                }
            }
            catch(Exception ex)
            {

            }
            
        }

        public void Call_BITTREX()
        {
            try
            {

                for (int i = 0; i < BITTREX_INFO_LIST.Count(); i++)
                {
                    try
                    {
                        Thread.Sleep(DELAY * 1);
                        GET_COIN_INFOMATION(BITTREX_INFO_LIST[i]);
                    }
                    catch(Exception ex)
                    {

                    }
                    finally
                    {
                        BITTREX_INFO_LIST[i].CHECK = true;
                    }
                }
                

            }
            catch (Exception ex)
            {

            }

        }


        public void Call_BINANCE()
        {
            try
            {

                for (int i = 0; i < BINANCE_INFO_LIST.Count(); i++)
                {
                    try
                    {
                        Thread.Sleep(DELAY * 1);
                        GET_COIN_INFOMATION(BINANCE_INFO_LIST[i]);
                    }
                    catch(Exception ex)
                    {

                    }
                    finally
                    {
                        BINANCE_INFO_LIST[i].CHECK = true;
                    }
                    
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void Call_HUOBI()
        {
            try
            {

                for (int i = 0; i < HUOBI_INFO_LIST.Count(); i++)
                {
                    try
                    {
                        Thread.Sleep(DELAY * 1);
                        GET_COIN_INFOMATION(HUOBI_INFO_LIST[i]);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        HUOBI_INFO_LIST[i].CHECK = true;
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }

        public void Call_BITHUMB()
        {
            try
            {

                for (int i = 0; i < BITHUMB_INFO_LIST.Count(); i++)
                {
                    try
                    {
                        Thread.Sleep(DELAY * 1);
                        GET_COIN_INFOMATION(BITHUMB_INFO_LIST[i]);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        BITHUMB_INFO_LIST[i].CHECK = true;
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }

        public void Calc_Diff()
        {
            try
            {

                for (int i = 0; i < Enum.GetNames(typeof(종목)).Length; i++)
                {
                    GET_DIFF(TOTAL_LIST, Enum.GetNames(typeof(종목)).GetValue(i).ToString());
                    //Thread.Sleep(DELAY * 1);
                }
                
            }
            catch(Exception ex)
            {

            }
        }


        public void SET_MAX_GRID()
        {
            try
            {

            }
            catch(Exception ex)
            {

            }
        }

        public double GET_MAX(COIN_INFO UP, COIN_INFO BINAN, COIN_INFO BITTREX)
        {
            double result = 0;
            try
            {
                result = Math.Max(UP.현재가_원, BINAN.현재가_원);
                result = Math.Max(result, BITTREX.현재가_원);
                return result;
            }
            catch(Exception ex)
            {
                return result;
            }
        }

        public double GET_MAX(COIN_INFO UP, COIN_INFO BINAN, COIN_INFO BITTREX, COIN_INFO HUOBI)
        {
            double result = 0;
            try
            {
                result = Math.Max(UP.현재가_원, BINAN.현재가_원);
                result = Math.Max(result, BITTREX.현재가_원);
                result = Math.Max(result, HUOBI.현재가_원);
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public double GET_MIN(COIN_INFO UP, COIN_INFO BINAN, COIN_INFO BITTREX)
        {
            double result = 0;
            try
            {
                if(BINAN.현재가_원 == 0)
                {
                    //BINAN.현재가_원 = 99999999;
                    result = Math.Min(UP.현재가_원, BITTREX.현재가_원);
                }

                else if (BITTREX.현재가_원 == 0)
                {
                    //BITTREX.현재가_원 = 99999999;
                    result = Math.Min(UP.현재가_원, BINAN.현재가_원);
                }
                else
                {
                    result = Math.Min(UP.현재가_원, BINAN.현재가_원);
                    result = Math.Min(result, BITTREX.현재가_원);
                }

                
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public double GET_MIN(COIN_INFO UP, COIN_INFO BINAN, COIN_INFO BITTREX, COIN_INFO HUOBI)
        {
            double result = 0;
            try
            {
                if (BINAN.현재가_원 == 0 && BITTREX.현재가_원 == 0 && HUOBI.현재가_원 == 0)
                {
                    //BINAN.현재가_원 = 99999999;
                    //result = Math.Min(UP.현재가_원, BITTREX.현재가_원);
                    result = UP.현재가_원;
                }
                else if (BINAN.현재가_원 == 0 && BITTREX.현재가_원 != 0 && HUOBI.현재가_원 != 0)
                {
                    result = Math.Min(UP.현재가_원, BITTREX.현재가_원);
                    result = Math.Min(result, HUOBI.현재가_원);
                }
                else if (BINAN.현재가_원 != 0 && BITTREX.현재가_원 == 0 && HUOBI.현재가_원 != 0)
                {
                    result = Math.Min(UP.현재가_원, BINAN.현재가_원);
                    result = Math.Min(result, HUOBI.현재가_원);
                }
                else if (BINAN.현재가_원 != 0 && BITTREX.현재가_원 != 0 && HUOBI.현재가_원 == 0)
                {
                    result = Math.Min(UP.현재가_원, BINAN.현재가_원);
                    result = Math.Min(result, BITTREX.현재가_원);
                }
                else if (BINAN.현재가_원 != 0 && BITTREX.현재가_원 == 0 && HUOBI.현재가_원 == 0)
                {
                    result = Math.Min(UP.현재가_원, BINAN.현재가_원);
                }
                else if (BINAN.현재가_원 == 0 && BITTREX.현재가_원 != 0 && HUOBI.현재가_원 == 0)
                {
                    result = Math.Min(UP.현재가_원, BITTREX.현재가_원);
                }
                else if (BINAN.현재가_원 == 0 && BITTREX.현재가_원 == 0 && HUOBI.현재가_원 != 0)
                {
                    result = Math.Min(UP.현재가_원, HUOBI.현재가_원);
                }
                else if (BINAN.현재가_원 != 0 && BITTREX.현재가_원 != 0 && HUOBI.현재가_원 != 0)
                {
                    result = Math.Min(UP.현재가_원, BINAN.현재가_원);
                    result = Math.Min(result, BITTREX.현재가_원);
                    result = Math.Min(result, HUOBI.현재가_원);
                }

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public void GET_DIFF(List<List<COIN_INFO>> TOTAL, string str_종목)
        {
            int UP_INDEX = 0;
            int BINAN_INDEX = 0;
            int BITTREX_INDEX = 0;
            int HUOBI_INDEX = 0;


            double 차액 = 0;
            double 퍼센테이지 = 0;

            double MAX = 0;
            double MIN = 0;

            double 기준금액 = Convert.ToDouble(textBox2.Text);
            double 매수수량 = 0;
            double 리턴금액 = 0;

            //UP
            int count = TOTAL[0].Count();
            for(int i = 0; i < count; i++)
            {
                if (string.Format("{0}", TOTAL[0][i].종목)  == str_종목)
                {
                    //UP = TOTAL[0][i].현재가_원;
                    UP_INDEX = i;
                    break;
                }
            }
            //UP

            //BINAN
            count = TOTAL[1].Count();
            for (int i = 0; i < count; i++)
            {
                if (string.Format("{0}", TOTAL[1][i].종목) == str_종목)
                {
                    //BINAN = TOTAL[1][i].현재가_원;
                    BINAN_INDEX = i;
                    break;
                }
            }
            //BINAN

            //BITTREX
            count = TOTAL[2].Count();
            for (int i = 0; i < count; i++)
            {
                if (string.Format("{0}", TOTAL[2][i].종목) == str_종목)
                {
                    //BITTREX = TOTAL[2][i].현재가_원;
                    BITTREX_INDEX = i;
                    break;
                }
            }
            //BITTREX

            //HUOBI
            count = TOTAL[3].Count();
            for (int i = 0; i < count; i++)
            {
                if (string.Format("{0}", TOTAL[3][i].종목) == str_종목)
                {
                    //BITTREX = TOTAL[2][i].현재가_원;
                    HUOBI_INDEX = i;
                    break;
                }
            }
            //HUOBI


            for (int i = 0; i < Enum.GetNames(typeof(종목)).Length; i++)
            {
                if (Enum.GetNames(typeof(종목)).GetValue(i).ToString() == str_종목 && str_종목 != "BTC")
                {
                    MAX = GET_MAX(TOTAL[0][UP_INDEX], TOTAL[1][BINAN_INDEX], TOTAL[2][BITTREX_INDEX], TOTAL[3][HUOBI_INDEX]);
                    MIN = GET_MIN(TOTAL[0][UP_INDEX], TOTAL[1][BINAN_INDEX], TOTAL[2][BITTREX_INDEX], TOTAL[3][HUOBI_INDEX]);


                    InvokeFunction.DataGridView_Rows_SetBackColor(dataGridView1, 1, Convert.ToInt32(i), Color.White);
                    InvokeFunction.DataGridView_Rows_SetBackColor(dataGridView1, 2, Convert.ToInt32(i), Color.White);
                    InvokeFunction.DataGridView_Rows_SetBackColor(dataGridView1, 3, Convert.ToInt32(i), Color.White);
                    InvokeFunction.DataGridView_Rows_SetBackColor(dataGridView1, 4, Convert.ToInt32(i), Color.White);

                    if (MAX == TOTAL[0][UP_INDEX].현재가_원)
                    {
                        InvokeFunction.DataGridView_Rows_SetBackColor(dataGridView1, 1, Convert.ToInt32(i), Color.PaleVioletRed);
                    }
                    else if (MAX == TOTAL[1][BINAN_INDEX].현재가_원)
                    {
                        InvokeFunction.DataGridView_Rows_SetBackColor(dataGridView1, 2, Convert.ToInt32(i), Color.PaleVioletRed);
                    }
                    else if (MAX == TOTAL[2][BITTREX_INDEX].현재가_원)
                    {
                        InvokeFunction.DataGridView_Rows_SetBackColor(dataGridView1, 3, Convert.ToInt32(i), Color.PaleVioletRed);
                    }
                    else if (MAX == TOTAL[3][HUOBI_INDEX].현재가_원)
                    {
                        InvokeFunction.DataGridView_Rows_SetBackColor(dataGridView1, 4, Convert.ToInt32(i), Color.PaleVioletRed);
                    }

                    if (MIN == TOTAL[0][UP_INDEX].현재가_원)
                    {
                        InvokeFunction.DataGridView_Rows_SetBackColor(dataGridView1, 1, Convert.ToInt32(i), Color.LightSkyBlue);
                    }
                    else if (MIN == TOTAL[1][BINAN_INDEX].현재가_원)
                    {
                        InvokeFunction.DataGridView_Rows_SetBackColor(dataGridView1, 2, Convert.ToInt32(i), Color.LightSkyBlue);
                    }
                    else if (MIN == TOTAL[2][BITTREX_INDEX].현재가_원)
                    {
                        InvokeFunction.DataGridView_Rows_SetBackColor(dataGridView1, 3, Convert.ToInt32(i), Color.LightSkyBlue);
                    }
                    else if (MIN == TOTAL[3][HUOBI_INDEX].현재가_원)
                    {
                        InvokeFunction.DataGridView_Rows_SetBackColor(dataGridView1, 4, Convert.ToInt32(i), Color.LightSkyBlue);
                    }

                    차액 = MAX - MIN;
                    //InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW, Convert.ToInt32(Enum.GetNames(typeof(종목)).GetValue(i)), string.Format("{0:#,###0.##}", 차액));
                    InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW, Convert.ToInt32(i), string.Format("{0:#,###0.##}", 차액));

                    //퍼센테이지 = Math.Abs(차액) / 업비트_XRP.현재가_원 * 100;
                    퍼센테이지 = Math.Abs(차액) / TOTAL[0][UP_INDEX].현재가_원 * 100;
                    //InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(Enum.GetNames(typeof(종목)).GetValue(i)), string.Format("{0:#,###0.##}", 퍼센테이지));
                    InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(i), string.Format("{0:#,###0.##}", 퍼센테이지));

                    InvokeFunction.DataGridView_Rows_SetBackColor(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(i), Color.White);
                    if (퍼센테이지 >= 1)
                    {
                        InvokeFunction.DataGridView_Rows_SetBackColor(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(i), Color.GreenYellow);
                    }

                    매수수량 = 기준금액 / MIN;
                    리턴금액 = 매수수량 * MAX;
                    //InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW_리턴금액, Convert.ToInt32(Enum.GetNames(typeof(종목)).GetValue(i)), string.Format("{0:#,###0}", 리턴금액));
                    InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW_리턴금액, Convert.ToInt32(i), string.Format("{0:#,###0}", 리턴금액));
                    break;
                }
                
            }
        }


        /*
        public void GET_COIN_DIFF(종목 종목)
        {
            try
            {
                
                double 차액 = 0;
                double 퍼센테이지 = 0;

                double MAX = 0;
                double MIN = 0;

                double 기준금액 = Convert.ToDouble(textBox2.Text);
                double 매수수량 = 0;
                double 리턴금액 = 0;

                switch (종목)
                {
                    case 종목.DCR:
                        차액 = 업비트_DCR.현재가_원 - 바이낸스_DCR.현재가_원;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));

                        퍼센테이지 = Math.Abs(차액) / 업비트_DCR.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));


                        break;

                    case 종목.XRP:
                        MAX = GET_MAX(업비트_XRP, 바이낸스_XRP, 비트렉스_XRP);
                        MIN = GET_MIN(업비트_XRP, 바이낸스_XRP, 비트렉스_XRP);

                        차액 = MAX - MIN;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));

                        퍼센테이지 = Math.Abs(차액) / 업비트_XRP.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));

                        매수수량 = 기준금액 / MIN;
                        리턴금액 = 매수수량 * MAX;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW_리턴금액, Convert.ToInt32(종목), string.Format("{0:#,###0}", 리턴금액));

                        break;

                    case 종목.ZIL:
                        //차액 = 업비트_ZIL.현재가_원 - 바이낸스_ZIL.현재가_원;

                        MAX = GET_MAX(업비트_ZIL, 바이낸스_ZIL, 비트렉스_ZIL);
                        MIN = GET_MIN(업비트_ZIL, 바이낸스_ZIL, 비트렉스_ZIL);

                        차액 = MAX - MIN;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));

                        퍼센테이지 = Math.Abs(차액) / 업비트_ZIL.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));

                        매수수량 = 기준금액 / MIN;
                        리턴금액 = 매수수량 * MAX;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW_리턴금액, Convert.ToInt32(종목), string.Format("{0:#,###0}", 리턴금액));
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

                    case 종목.CVC:
                        차액 = 업비트_CVC.현재가_원 - 바이낸스_CVC.현재가_원;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));

                        퍼센테이지 = Math.Abs(차액) / 업비트_CVC.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));
                        break;

                    case 종목.XEM:
                        차액 = 업비트_XEM.현재가_원 - 바이낸스_XEM.현재가_원;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));

                        퍼센테이지 = Math.Abs(차액) / 업비트_XEM.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));
                        break;

                    case 종목.ETH:

                        MAX = GET_MAX(업비트_ETH, 바이낸스_ETH, 비트렉스_ETH);
                        MIN = GET_MIN(업비트_ETH, 바이낸스_ETH, 비트렉스_ETH);

                        차액 = MAX - MIN;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 차액));

                        퍼센테이지 = Math.Abs(차액) / 업비트_ETH.현재가_원 * 100;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 차액_PERCENTAGE, Convert.ToInt32(종목), string.Format("{0:#,###0.##}", 퍼센테이지));

                        매수수량 = 기준금액 / MIN;
                        리턴금액 = 매수수량 * MAX;
                        InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW_리턴금액, Convert.ToInt32(종목), string.Format("{0:#,###0}", 리턴금액));
                        break;



                }
            }
            catch (Exception ex)
            {

            }
        }
        */
      


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


            Set_Data_Grid();

        }

        public void Set_Data_Grid()
        {
            for(int i = 0; i < Enum.GetNames(typeof(종목)).Length; i++)
            {
                InvokeFunction.DataGridView_Rows_Add(dataGridView1);
                InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 0, i, string.Format(Enum.GetNames(typeof(종목)).GetValue(i).ToString()));
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //double result =  CALL_API_UPBIT("https://api.upbit.com/v1/ticker?markets=KRW-DCR");
            //InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 1, 1, string.Format("{0:#,###}", result));

            //업비트_DCR.현재가_원 = result;

            //GET_COIN_INFOMATION(업비트_DCR);

            //double result_ = CALL_API_UPBIT("https://api.upbit.com/v1/ticker?markets=KRW-XRP");
            //InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 1, 2, string.Format("{0:#,###}", result_));

            //업비트_XRP.현재가_원 = result_;
            //GET_COIN_INFOMATION(업비트_XRP);

        }

        public double CALL_API_UPBIT(string URL)
        {
            string retult = BINANCE_CALLER.callWebClient(URL);
            //Console.WriteLine(retult);

            var JARR = JArray.Parse(retult);

            string LAST_PRICE = JARR[0]["trade_price"].ToString();

            return Convert.ToDouble(LAST_PRICE);
        }

        public double CALL_API_BINANCE(string URL)
        {
            string retult = BINANCE_CALLER.callWebClient(URL);
            //Console.WriteLine(retult);

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
                //Console.WriteLine(retult);

                var JARR = JArray.Parse(retult);

                string LAST_PRICE = JARR[0]["trade_price"].ToString();

                string low_price = JARR[0]["low_price"].ToString();

                result = Convert.ToDouble(LAST_PRICE);

                double d_low_price = Convert.ToDouble(low_price);

                double per = (result - d_low_price) / d_low_price * 100;
                //InvokeFunction.DataGridView_Rows_SetText(dataGridView1, Convert.ToInt32(coinInfo.거래소), ROW_퍼센테이지변동, string.Format("{0:#,###.##}", per));
                //InvokeFunction.DataGridView_Rows_SetText(dataGridView1, Convert.ToInt32(coinInfo.거래소), 10, string.Format("{0}", "TEST"));
                InvokeFunction.DataGridView_Rows_SetText(dataGridView1, ROW_퍼센테이지변동, Convert.ToInt32(coinInfo.종목), string.Format("{0:#,###0.##}", per));
                if(per > Convert.ToDouble(textBox4.Text))
                {
                    InvokeFunction.ListBoxAdd(listBox1, string.Format("{0:HH:mm:ss} - {1} : 1분 변동 : {2:#,##0.###0}%", DateTime.Now, coinInfo.종목.ToString(), per));
                }


                if (coinInfo.종목 == 종목.BTC)
                {
                    UPBIT_BTC_KRW = result;
                    InvokeFunction.Label_Set_Text(label2, string.Format("{0:#,###}", UPBIT_BTC_KRW));
                }
                    

                //return result;
            }
            else if(coinInfo.거래소 == 거래소.BINANCE)
            {
                string retult = BINANCE_CALLER.callWebClient(coinInfo.URL);
                //Console.WriteLine(retult);

                var JARR = JObject.Parse(retult);

                //string LAST_PRICE = JARR["asks"][0][0].ToString();
                string LAST_PRICE = JARR["bids"][0][0].ToString();

                //double result = UPBIT_BTC_KRW * API_RESULT;

                //double result = Convert.ToDouble(LAST_PRICE) * UPBIT_BTC_KRW;
                result = Convert.ToDouble(LAST_PRICE) * UPBIT_BTC_KRW;

                //return Convert.ToDouble(LAST_PRICE);
                //return result;
            }
            else if(coinInfo.거래소 == 거래소.BITTREX)
            {
                string retult = BINANCE_CALLER.callWebClient(coinInfo.URL);
                //Console.WriteLine(retult);

                var JARR = JObject.Parse(retult);

                string LAST_PRICE = JARR["result"]["Bid"].ToString();

                result = Convert.ToDouble(LAST_PRICE) * UPBIT_BTC_KRW;
            }

            else if(coinInfo.거래소 == 거래소.HUOBI)
            {
                string retult = BINANCE_CALLER.callWebClient(coinInfo.URL);
                //Console.WriteLine(retult);

                var JARR = JObject.Parse(retult);

                string LAST_PRICE = JARR["tick"]["bids"][0][0].ToString();

                result = Convert.ToDouble(LAST_PRICE) * UPBIT_BTC_KRW;
            }

            else if (coinInfo.거래소 == 거래소.BITHUMB)
            {
                string retult = BINANCE_CALLER.callWebClient(coinInfo.URL);
                //Console.WriteLine(retult);

                var JARR = JObject.Parse(retult);

                string LAST_PRICE = JARR["data"]["bids"][0][0].ToString();

                result = Convert.ToDouble(LAST_PRICE) * UPBIT_BTC_KRW;
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
                Sub_thread.Abort();

                UPBIT_thread.Abort();
                BINANCE_thread.Abort();
                BITTREX_thread.Abort();
                HUOBI_thread.Abort();

                DIFF_thread.Abort();
            }
            catch(Exception ex)
            {
                myLog.LOG_CLOSE();

                Main_thread.Abort();
                Sub_thread.Abort();

                UPBIT_thread.Abort();
                BINANCE_thread.Abort();
                BITTREX_thread.Abort();
                HUOBI_thread.Abort();

                DIFF_thread.Abort();
            }
        }

        public void GET_COIN_INFOMATION(COIN_INFO coininfo)
        {
            try
            {
                coininfo.현재가_원 = CALL_API(coininfo);
                Draw_GridView(coininfo);
            }
            catch(Exception ex)
            {

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            //GET_COIN_INFOMATION(바이낸스_DCR);
            
            //GET_COIN_INFOMATION(바이낸스_XRP);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //GET_COIN_INFOMATION(업비트_BTC);
            //GET_COIN_INFOMATION(비트렉스_XRP);

        }
        
        public void Draw_GridView(COIN_INFO coinInfo)
        {
            try
            {
                InvokeFunction.DataGridView_Rows_SetFontColor(dataGridView1, Convert.ToInt32(coinInfo.거래소), Convert.ToInt32(coinInfo.종목), Color.Red);
                InvokeFunction.DataGridView_Rows_SetText(dataGridView1, Convert.ToInt32(coinInfo.거래소), Convert.ToInt32(coinInfo.종목), string.Format("{0:#,###0.##}", coinInfo.현재가_원));
                InvokeFunction.DataGridView_Rows_SetFontColor(dataGridView1, Convert.ToInt32(coinInfo.거래소), Convert.ToInt32(coinInfo.종목), Color.Black);
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
            //Main_thread.Start();
            //Sub_thread.Start();

            UPBIT_thread.Start();
            BINANCE_thread.Start();
            BITTREX_thread.Start();
            HUOBI_thread.Start();
            //BITHUMB_thread.Start();

            DIFF_thread.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            //double 차액 = 업비트_DCR.현재가_원 - 바이낸스_DCR.현재가_원;

            //InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 3, 1, string.Format("{0:#,###}", 차액));

            //double 차액_ = 업비트_XRP.현재가_원 - 바이낸스_XRP.현재가_원;
            //InvokeFunction.DataGridView_Rows_SetText(dataGridView1, 3, 2, string.Format("{0:#,###0.##}", 차액_));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double result;
            result = Convert.ToDouble(textBox1.Text) * UPBIT_BTC_KRW;
            label1.Text = string.Format("{0:#,###0.##}", result);


        }

        private void button7_Click(object sender, EventArgs e)
        {
            //private static TelegramBotClient Bot;
        


        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                Find_GridView(textBox3.Text);
            }
            catch(Exception ex)
            {

            }
            
        }

        public void Find_GridView(string str_종목)
        {
            try
            {
                string str = str_종목;
                int idx = 0;
                for (int i = 0; i < Enum.GetNames(typeof(종목)).Length; i++)
                {
                    string jong = Enum.GetNames(typeof(종목)).GetValue(i).ToString();
                    if (str == jong || str.ToUpper() == jong)
                    {
                        idx = i;
                        break;
                    }
                }
                InvokeFunction.DataGridView_Rows_Select(dataGridView1, idx, 0);
            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            InvokeFunction.ListBoxClear(listBox1);
        }
    }
}
