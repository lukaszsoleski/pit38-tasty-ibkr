using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr.Model
{
    public class TradeIBKR
    {
        public string Symbol { get; set; }
        public string ClientAccountID { get; set; }
        public string AccountAlias { get; set; }
        public string Model { get; set; }
        public string CurrencyPrimary { get; set; }
        public string AssetClass { get; set; }
        public string SubCategory { get; set; }
        public string Description { get; set; }
        public string Conid { get; set; }
        public string SecurityID { get; set; }
        public string SecurityIDType { get; set; }
        public string CUSIP { get; set; }
        public string ISIN { get; set; }
        public string FIGI { get; set; }
        public string ListingExchange { get; set; }
        public string UnderlyingConid { get; set; }
        public string UnderlyingSymbol { get; set; }
        public string UnderlyingSecurityID { get; set; }
        public string UnderlyingListingExchange { get; set; }
        public string Issuer { get; set; }
        public string IssuerCountryCode { get; set; }
        public string Multiplier { get; set; }
        public string Strike { get; set; }
        public string Expiry { get; set; }
        public string PutCall { get; set; }
        public string PrincipalAdjustFactor { get; set; }
        public string TransactionType { get; set; }
        public string TradeID { get; set; }
        public string OrderID { get; set; }
        public string ExecID { get; set; }
        public string BrokerageOrderID { get; set; }
        public string OrderReference { get; set; }
        public string VolatilityOrderLink { get; set; }
        public string ClearingFirmID { get; set; }
        public string OrigTradePrice { get; set; }
        public string OrigTradeDate { get; set; }
        public string OrigTradeID { get; set; }
        public string ExtExecID { get; set; }
        public string BlockID { get; set; }
        public string OrderTime { get; set; }
        public string DateTime { get; set; }
        public string ReportDate { get; set; }
        public DateTime SettleDate { get; set; }
        public DateTime TradeDate { get; set; }
        public string Exchange { get; set; }
        public string BuySell { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal Proceeds { get; set; }
        public string NetCash { get; set; }
        public string NetCashWithBillable { get; set; }
        public decimal Commission { get; set; }
        public decimal BrokerExecutionCommission { get; set; }
        public decimal BrokerClearingCommission { get; set; }
        public decimal ThirdPartyExecutionCommission { get; set; }
        public decimal ThirdPartyClearingCommission { get; set; }
        public decimal ThirdPartyRegulatoryCommission { get; set; }
        public decimal OtherCommission { get; set; }
        public string CommissionCurrency { get; set; }
        public string Tax { get; set; }
        public string Code { get; set; }
        public string OrderType { get; set; }
        public string LevelOfDetail { get; set; }
        public string TraderID { get; set; }
        public string IsAPIOrder { get; set; }
        public string AllocatedTo { get; set; }
        public string AccruedInterest { get; set; }
        public string RFQID { get; set; }
        public string SerialNumber { get; set; }
        public string DeliveryType { get; set; }
        public string CommodityType { get; set; }
        public string Fineness { get; set; }
        public string Weight { get; set; }


        public decimal GetCommitions()
        {
            return Commission + BrokerExecutionCommission + BrokerClearingCommission + ThirdPartyExecutionCommission + ThirdPartyClearingCommission + ThirdPartyRegulatoryCommission + OtherCommission;
        }
    }
}
