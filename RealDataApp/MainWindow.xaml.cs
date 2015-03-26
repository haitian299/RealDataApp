using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuantBox.XAPI.Callback;
using QuantBox.XAPI;
using QuantBox;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace RealDataApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.tickDataGrid.Loaded += tickDataGrid_Loaded;
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dataTableInit();
            
        }


        Run infoTxt = new Run();
        private XApi api;
        DataTable tickDataTable = new DataTable();



        private void dataTableInit()
        {
            Type t = typeof(DepthMarketDataField);
            if (t == null)
                return;
            foreach (var v in t.GetFields())
            {
                tickDataTable.Columns.Add(v.Name, v.FieldType);
            }
            //this.tickDataGrid.ItemsSource = tickDataTable.DefaultView;
            //string keyColumn = "UpdateTime,TradingDay,InstrumentID,ExchangeID";
            //tickDataTable.PrimaryKey = keyColumn.Split(',').Select(v => tickDataTable.Columns[v]).ToArray();
        }

        private void mysqlInit()
        {
            string connStr = "server=localhost;user=root;database=test;port=3306;password=;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                showInfo("Connecting to MySQL...");
                conn.Open();
                // Perform database operations
            }
            catch (Exception ex)
            {
                showInfo(ex.ToString());
            }
            conn.Close();
            showInfo("Done.");
        }
        


        /// <summary>
        /// OnConnectionStatus回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="status"></param>
        /// <param name="userLogin"></param>
        /// <param name="size1"></param>
        private void OnConnectionStatus(object sender, ConnectionStatus status, ref RspUserLoginField userLogin, int size1)
        {
            showInfo("showing info inside onConnectionStatus now");
            showInfo("//" + status + userLogin.ErrorMsg());

        }

        /// <summary>
        /// 深度行情回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="marketData"></param>

        private void OnRtnDepthMarketData(object sender, ref DepthMarketDataField marketData)
        {
            showInfo("showing info inside depth now");
            showInfo("//" + marketData.InstrumentID);
            showInfo("//" + marketData.ExchangeID);
            showInfo("//" + marketData.LastPrice.ToString());

            var row = tickDataTable.NewRow();

            foreach (var fi in typeof(DepthMarketDataField).GetFields())
            {
                switch (fi.Name)
                {
                    case "InstrumentID":
                        row[fi.Name] = marketData.InstrumentID;
                        break;
                    case "ActionDay":
                        row[fi.Name] = marketData.ActionDay;
                        break;
                    case "AskPrice1":
                        row[fi.Name] = marketData.AskPrice1;
                        break;
                    case "AskPrice2":
                        row[fi.Name] = marketData.AskPrice2;
                        break;
                    case "AskPrice3":
                        row[fi.Name] = marketData.AskPrice3;
                        break;
                    case "AskPrice4":
                        row[fi.Name] = marketData.AskPrice4;
                        break;
                    case "AskPrice5":
                        row[fi.Name] = marketData.AskPrice5;
                        break;
                    case "AskVolume1":
                        row[fi.Name] = marketData.AskVolume1;
                        break;
                    case "AskVolume2":
                        row[fi.Name] = marketData.AskVolume2;
                        break;
                    case "AskVolume3":
                        row[fi.Name] = marketData.AskVolume3;
                        break;
                    case "AskVolume4":
                        row[fi.Name] = marketData.AskVolume4;
                        break;
                    case "AskVolume5":
                        row[fi.Name] = marketData.AskVolume5;
                        break;
                    case "AveragePrice":
                        row[fi.Name] = marketData.AveragePrice;
                        break;
                    case "BidPrice1":
                        row[fi.Name] = marketData.BidPrice1;
                        break;
                    case "BidPrice2":
                        row[fi.Name] = marketData.BidPrice2;
                        break;
                    case "BidPrice3":
                        row[fi.Name] = marketData.BidPrice3;
                        break;
                    case "BidPrice4":
                        row[fi.Name] = marketData.BidPrice4;
                        break;
                    case "BidPrice5":
                        row[fi.Name] = marketData.BidPrice5;
                        break;
                    case "BidVolume1":
                        row[fi.Name] = marketData.BidVolume1;
                        break;
                    case "BidVolume2":
                        row[fi.Name] = marketData.BidVolume2;
                        break;
                    case "BidVolume3":
                        row[fi.Name] = marketData.BidVolume3;
                        break;
                    case "BidVolume4":
                        row[fi.Name] = marketData.BidVolume4;
                        break;
                    case "BidVolume5":
                        row[fi.Name] = marketData.BidVolume5;
                        break;
                    case "ClosePrice":
                        row[fi.Name] = marketData.ClosePrice;
                        break;
                    case "ExchangeID":
                        row[fi.Name] = marketData.ExchangeID;
                        break;
                    case "HighestPrice":
                        row[fi.Name] = marketData.HighestPrice;
                        break;
                    case "LastPrice":
                        row[fi.Name] = marketData.LastPrice;
                        break;
                    case "LowerLimitPrice":
                        row[fi.Name] = marketData.LowerLimitPrice;
                        break;
                    case "LowestPrice":
                        row[fi.Name] = marketData.LowestPrice;
                        break;
                    case "OpenInterest":
                        row[fi.Name] = marketData.OpenInterest;
                        break;
                    case "OpenPrice":
                        row[fi.Name] = marketData.OpenPrice;
                        break;
                    case "PreClosePrice":
                        row[fi.Name] = marketData.PreClosePrice;
                        break;
                    case "PreOpenInterest":
                        row[fi.Name] = marketData.PreOpenInterest;
                        break;
                    case "PreSettlementPrice":
                        row[fi.Name] = marketData.PreSettlementPrice;
                        break;
                    case "SettlementPrice":
                        row[fi.Name] = marketData.SettlementPrice;
                        break;
                    case "Symbol":
                        row[fi.Name] = marketData.Symbol;
                        break;
                    case "TradingDay":
                        row[fi.Name] = marketData.TradingDay;
                        break;
                    case "Turnover":
                        row[fi.Name] = marketData.Turnover;
                        break;
                    case "UpdateMillisec":
                        row[fi.Name] = marketData.UpdateMillisec;
                        break;
                    case "UpdateTime":
                        row[fi.Name] = marketData.UpdateTime;
                        break;
                    case "UpperLimitPrice":
                        row[fi.Name] = marketData.UpperLimitPrice;
                        break;
                    case "Volume":
                        row[fi.Name] = marketData.Volume;
                        break;

                }
            }
            tickDataTable.Rows.Add(row);

        }


        /// <summary>
        /// 连接行情前置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void connectBt_Click(object sender, EventArgs e)
        {

            ctpInit();
            mysqlInit();
        }

        private void ctpInit()
        {
            showInfo("begin");
            showInfo("Initialize api");
            api = new XApi(@"C:\Users\jht\Documents\Visual Studio 2013\Projects\DataApp\DataApp\dll\QuantBox_CTP_Quote.dll");
            showInfo("Initialize server info");
            api.Server.BrokerID = "66666";
            api.Server.Address = "tcp://ctp1-md9.citicsf.com:41213";

            showInfo("set 回调函数");
            api.OnConnectionStatus += OnConnectionStatus;
            api.OnRtnDepthMarketData += OnRtnDepthMarketData;

            showInfo("begin connecting");
            api.Connect();


            //Thread.Sleep(3000);
            //showInfo("begin dispose");
            //api.Dispose();

            //Thread.Sleep(5 * 1000);
        }

        /// <summary>
        /// 订阅行情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void getDataBt_Click(object sender, EventArgs e)
        {
            //showInfo("begin subscribe quotes");
            api.Subscribe("IF1504", "");
        }

        private void showInfo(string info)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.infoTxt.Text += info + System.Environment.NewLine;
                this.infoTxtBox.ScrollToEnd();
            }));
        }

        

       
        /// <summary>
        /// 停止行情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopBt_Click(object sender, EventArgs e)
        {
            api.Dispose();
        }



        private void infoTxtBox_Loaded(object sender, RoutedEventArgs e)
        {
            Paragraph pgf = new Paragraph();
            infoTxt.Text = "";
            pgf.Inlines.Add(infoTxt);
            FlowDocument fd = new FlowDocument();
            fd.Blocks.Add(pgf);
            this.infoTxtBox.Document = fd;
            this.infoTxtBox.IsReadOnly = true;
            this.infoTxtBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private void tickDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            tickDataGrid.AutoGenerateColumns = false;

            tickDataGrid.Items.Clear();
            
            tickDataGrid.ItemsSource = tickDataTable.AsDataView();
            tickDataGrid.AutoGenerateColumns = true;
            //Type t = typeof(DepthMarketDataField);
            //if (t == null)
            //    return;
            //foreach (var v in t.GetFields())
            //{
            //    DataGridTextColumn dgtc = new DataGridTextColumn();
            //    dgtc.Header = v.Name;

            //    Binding b = new Binding();
            //    b.Source = tickDataTable.Columns[v.Name];

            //    dgtc.Binding = b;
            //    tickDataGrid.Columns.Add(dgtc);

            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Type t = typeof(DepthMarketDataField);
            foreach (var v in t.GetFields())
            {
                for (int i = 0; i < tickDataTable.Rows.Count; i++)
                {
                    
                    showInfo( v.Name +" "+ tickDataTable.Rows[i][v.Name].ToString() + " " +tickDataTable.Rows[i][v.Name].GetType());
                }
                    
            }
        }

        
    }
}
