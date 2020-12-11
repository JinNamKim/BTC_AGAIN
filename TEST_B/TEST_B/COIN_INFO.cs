using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_B
{
    public enum 거래소 : Int32
    {
        UPBIT = 1,
        BINANCE
    }

    public enum 종목 : Int32
    {
        BTC = 0,
        DCR,
        XRP,        //리플
        ZIL,        //질리카
        XLM,         //스텔라루멘
        ADA         //에이다
    }
    public class COIN_INFO
    {
        public 종목 종목;
        public 거래소 거래소;
        public double 현재가_원;
        public double 현재가_BTC;
        public string URL;
    }
}
