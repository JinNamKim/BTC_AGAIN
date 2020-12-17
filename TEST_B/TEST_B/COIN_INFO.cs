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
        BINANCE,
        BITTREX,
        HUOBI,
        BITHUMB
    }


    /*
     * ETH,NEO,MTL,LTC,XRP,OMG,SNT,
     * WAVES,XEM,QTUM,GLM,LSK,STEEM,
     * XLM,ARDR,KMD,ARK,STORJ,GRS,REP,
     * ADA,POWR,ICX,EOS,TRX,SC,ONT,DCR,
     * ZIL,POLY,ZRX,LOOM,BCH,ADX,BAT,IOST,
     * CVC,IOTA,OST,ONG,GAS,ELF,THETA,QKC,ENJ,
     * TFUEL,MANA,ANKR,AERGO,ATOM,MBL,HBAR,STPT,
     * VET,CHZ,STMX,HIVE,KAVA,LINK,XTZ,JST,SXP,DOT,SRM,KNC
     * */

    public enum 종목 : Int32
    {
        BTC = 0,
        DCR,
        XRP,        //리플
        ZIL,        //질리카
        XLM,         //스텔라루멘
        ADA,         //에이다
        CVC,         //시빅
        XEM,             //넴
        //RFR,         //리퍼리움
        ETH,
        NEO,
        MTL,
        LBC,
        TFUEL,
        ENJ,
        LTC,
        OMG,
        SNT,
        WAVES,
        QTUM,
        GLM,
        LSK,
        STEEM,
        ARDR,
        KMD,
        ARK, STORJ, GRS, REP,
        POWR, ICX, EOS, TRX, SC, ONT,
        POLY, ZRX, LOOM, BCH, ADX, BAT, IOST,
        IOTA, OST, ONG, GAS, ELF, THETA, QKC,
        MANA, ANKR, AERGO, ATOM, MBL, HBAR, STPT,
        VET, CHZ, STMX, HIVE, KAVA, LINK, XTZ, JST, SXP, DOT, SRM, KNC,
        AAVE,


        //바이낸스
        //AAVE, ADA, ANT, ATOM, BAT, BCH, BEL, BNT, BTCDOWN, BTCUP, BUSD, BZRX, CHR, COMP, COS, CREAM, DIA, DOCK, DOGE, DOT, EOS, ETC, FIO, HARD, HBAR, IDEX, IOTA, IRIS, JST, KAVA,
        //KNC, KSM, LINK, LTC, LUNA, NMR, OCEAN, OMG, PAXG, QTUM, REN, REP, RSR, RUNE, RVN, SAND, SNX, SRM, SUSHI, SWRV, SXP, TCT, TFUEL, THETA, TRB, UMA, UNI, VET, VTHO, WAVES, WBTC, WING,
        //WNXM, WRX, XLM, XRP, XTZ, YFII, YFI, ZRX
        ANT, BEL, BNT, BTCDOWN, BTCUP, BUSD, BZRX, CHR, COMP, COS, CREAM, DIA, DOCK, DOGE,   ETC, FIO, HARD,  IDEX,  IRIS,  
        KSM,   LUNA, NMR, OCEAN,  PAXG,  REN,  RSR, RUNE, RVN, SAND, SNX,  SUSHI, SWRV,  TCT,   TRB, UMA, UNI,  VTHO,  WBTC, WING,
        WNXM, WRX, YFII, YFI
        //바이낸스

    }
    public class COIN_INFO
    {
        public 종목 종목;
        public 거래소 거래소;
        public double 현재가_원;
        public double 현재가_BTC;
        public string URL;

        public bool CHECK; 
    }
}
