using CsvHelper.Configuration.Attributes;
using pit38_tasty_ibkr.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pit38_tasty_ibkr.Model
{
    public class TradeIBKR
    {
        const string dateFormat = "dd/MM/yyyy";

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
        [Name("Put/Call")]
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
        [Name("Date/Time")]
        public string DateTime { get; set; }
        public string ReportDate { get; set; }
        public string SettleDate { get; set; }
        public DateTime SettleDateT => System.DateTime.ParseExact(SettleDate, dateFormat, CultureInfo.InvariantCulture);

        public string TradeDate { get; set; }
        public DateTime TradeDateT => System.DateTime.ParseExact(TradeDate, dateFormat, CultureInfo.InvariantCulture);
        public string Exchange { get; set; }
        [Name("Buy/Sell")]
        public string BuySell { get; set; }
        public string NetCash { get; set; }
        public string NetCashWithBillable { get; set; }
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

        public string Commission { get; set; }
        public string BrokerExecutionCommission { get; set; }
        public string BrokerClearingCommission { get; set; }
        public string ThirdPartyExecutionCommission { get; set; }
        public string ThirdPartyClearingCommission { get; set; }
        public string ThirdPartyRegulatoryCommission { get; set; }

        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Amount { get; set; }
        public string Proceeds { get; set; }
        public string OtherCommission { get; set; }
        public decimal CommissionNum => Math.Abs(Commission.ConvertToDecimal());
        public decimal BrokerExecutionCommissionNum => Math.Abs(BrokerExecutionCommission.ConvertToDecimal());
        public decimal BrokerClearingCommissionNum => Math.Abs(BrokerClearingCommission.ConvertToDecimal());
        public decimal ThirdPartyExecutionCommissionNum => Math.Abs(ThirdPartyExecutionCommission.ConvertToDecimal());
        public decimal ThirdPartyClearingCommissionNum => Math.Abs(ThirdPartyClearingCommission.ConvertToDecimal());
        public decimal ThirdPartyRegulatoryCommissionNum => Math.Abs(ThirdPartyRegulatoryCommission.ConvertToDecimal());
        public decimal OtherCommissionNum => Math.Abs(OtherCommission.ConvertToDecimal());
        public decimal QuantityNum => Math.Abs(Quantity.ConvertToDecimal());
        public decimal PriceNum => Math.Abs(Price.ConvertToDecimal());
        public decimal AmountNum => Math.Abs(Amount.ConvertToDecimal());
        public decimal ProceedsNum => Math.Abs(Proceeds.ConvertToDecimal());

        public decimal GetCommitions()
        {
            return CommissionNum + BrokerExecutionCommissionNum + BrokerClearingCommissionNum + ThirdPartyExecutionCommissionNum + ThirdPartyClearingCommissionNum + ThirdPartyRegulatoryCommissionNum + OtherCommissionNum;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Symbol: {Symbol}");
            sb.AppendLine($"ClientAccountID: {ClientAccountID}");
            sb.AppendLine($"AccountAlias: {AccountAlias}");
            sb.AppendLine($"Model: {Model}");
            sb.AppendLine($"CurrencyPrimary: {CurrencyPrimary}");
            sb.AppendLine($"AssetClass: {AssetClass}");
            sb.AppendLine($"SubCategory: {SubCategory}");
            sb.AppendLine($"Description: {Description}");
            sb.AppendLine($"Conid: {Conid}");
            sb.AppendLine($"SecurityID: {SecurityID}");
            sb.AppendLine($"SecurityIDType: {SecurityIDType}");
            sb.AppendLine($"CUSIP: {CUSIP}");
            sb.AppendLine($"ISIN: {ISIN}");
            sb.AppendLine($"FIGI: {FIGI}");
            sb.AppendLine($"ListingExchange: {ListingExchange}");
            sb.AppendLine($"UnderlyingConid: {UnderlyingConid}");
            sb.AppendLine($"UnderlyingSymbol: {UnderlyingSymbol}");
            sb.AppendLine($"UnderlyingSecurityID: {UnderlyingSecurityID}");
            sb.AppendLine($"UnderlyingListingExchange: {UnderlyingListingExchange}");
            sb.AppendLine($"Issuer: {Issuer}");
            sb.AppendLine($"IssuerCountryCode: {IssuerCountryCode}");
            sb.AppendLine($"Multiplier: {Multiplier}");
            sb.AppendLine($"Strike: {Strike}");
            sb.AppendLine($"Expiry: {Expiry}");
            sb.AppendLine($"PutCall: {PutCall}");
            sb.AppendLine($"PrincipalAdjustFactor: {PrincipalAdjustFactor}");
            sb.AppendLine($"TransactionType: {TransactionType}");
            sb.AppendLine($"TradeID: {TradeID}");
            sb.AppendLine($"OrderID: {OrderID}");
            sb.AppendLine($"ExecID: {ExecID}");
            sb.AppendLine($"BrokerageOrderID: {BrokerageOrderID}");
            sb.AppendLine($"OrderReference: {OrderReference}");
            sb.AppendLine($"VolatilityOrderLink: {VolatilityOrderLink}");
            sb.AppendLine($"ClearingFirmID: {ClearingFirmID}");
            sb.AppendLine($"OrigTradePrice: {OrigTradePrice}");
            sb.AppendLine($"OrigTradeDate: {OrigTradeDate}");
            sb.AppendLine($"OrigTradeID: {OrigTradeID}");
            sb.AppendLine($"ExtExecID: {ExtExecID}");
            sb.AppendLine($"BlockID: {BlockID}");
            sb.AppendLine($"OrderTime: {OrderTime}");
            sb.AppendLine($"DateTime: {DateTime}");
            sb.AppendLine($"ReportDate: {ReportDate}");
            sb.AppendLine($"SettleDate: {SettleDate}");
            sb.AppendLine($"TradeDate: {TradeDate}");
            sb.AppendLine($"Exchange: {Exchange}");
            sb.AppendLine($"BuySell: {BuySell}");
            sb.AppendLine($"Quantity: {Quantity}");
            sb.AppendLine($"Price: {Price}");
            sb.AppendLine($"Amount: {Amount}");
            sb.AppendLine($"Proceeds: {Proceeds}");
            sb.AppendLine($"NetCash: {NetCash}");
            sb.AppendLine($"NetCashWithBillable: {NetCashWithBillable}");
            sb.AppendLine($"Commission: {Commission}");
            sb.AppendLine($"BrokerExecutionCommission: {BrokerExecutionCommission}");
            sb.AppendLine($"BrokerClearingCommission: {BrokerClearingCommission}");
            sb.AppendLine($"ThirdPartyExecutionCommission: {ThirdPartyExecutionCommission}");
            sb.AppendLine($"ThirdPartyClearingCommission: {ThirdPartyClearingCommission}");
            sb.AppendLine($"ThirdPartyRegulatoryCommission: {ThirdPartyRegulatoryCommission}");
            sb.AppendLine($"OtherCommission: {OtherCommission}");
            sb.AppendLine($"CommissionCurrency: {CommissionCurrency}");
            sb.AppendLine($"Tax: {Tax}");
            sb.AppendLine($"Code: {Code}");
            sb.AppendLine($"OrderType: {OrderType}");
            sb.AppendLine($"LevelOfDetail: {LevelOfDetail}");
            sb.AppendLine($"TraderID: {TraderID}");
            sb.AppendLine($"IsAPIOrder: {IsAPIOrder}");
            sb.AppendLine($"AllocatedTo: {AllocatedTo}");
            sb.AppendLine($"AccruedInterest: {AccruedInterest}");
            sb.AppendLine($"RFQID: {RFQID}");
            sb.AppendLine($"SerialNumber: {SerialNumber}");
            sb.AppendLine($"DeliveryType: {DeliveryType}");
            sb.AppendLine($"CommodityType: {CommodityType}");
            sb.AppendLine($"Fineness: {Fineness}");
            sb.AppendLine($"Weight: {Weight}");

            return sb.ToString();
        }
    }
}
