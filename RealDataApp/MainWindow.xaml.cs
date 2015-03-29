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
using System.Collections.ObjectModel;
using System.Threading;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

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
            ocdt.Add(tickDataTable);
            infoTxtBoxInit();
        }


        Run infoTxt = new Run();
        private XApi api;
        DataTable tickDataTable = new DataTable();
        ObservableCollection<DataTable> ocdt = new ObservableCollection<DataTable>();
        ObservableCollection<string> allquotes = new ObservableCollection<string>();
        ObservableCollection<string> quotesToAdd = new ObservableCollection<string>();
        Type t = typeof(DepthMarketDataField);
        //List<string> quotesToAdd = new List<string>();
        //List<string> allquotes = new List<string>();
        MySqlConnection sqlCon = new MySqlConnection();

        private void dataTableInit()
        {
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
            string connStr = "server=localhost;user=root;port=3306;database=data1;password=leran299;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                showInfo("数据库连接状态: 开始连接数据库...");
                conn.Open();
                

                sqlCon = conn;

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = sqlCon;
                string dbsql = "CREATE DATABASE IF NOT EXISTS data1;";
                cmd.CommandText = dbsql;
                cmd.ExecuteNonQuery();
                showInfo("数据库连接状态: 连接数据库成功");
            }
            catch (Exception ex)
            {
                showInfo("连接数据库发生错误: " + ex.Message);
            }
            

            


            //conn.Close();
            
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
            //showInfo("OnConnectionStatus回调开始");
            showInfo("ctp连接状态: " + status + userLogin.ErrorMsg());

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
                    default:
                        break;
                }
            }
            tickDataTable.Rows.Add(row);


            string sql = "INSERT INTO " + marketData.InstrumentID +" (";

            foreach (var v in t.GetFields())
            {
                sql += v.Name + ",";
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += ") VALUES (";
            foreach (var v in t.GetFields())
            {
                if (v.FieldType.ToString() == "System.String")
                {
                    sql += "'" + row[v.Name].ToString() + "',";
                }
                else
                {
                    sql += row[v.Name].ToString() + ",";
                }


            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += ");";
            showInfo(sql);
            MySqlCommand cmd = new MySqlCommand(sql, sqlCon);
            cmd.ExecuteNonQuery();

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
            listboxInit();

        }

        private void listboxInit()
        {
            List<string> ss = getAllID();
            ss.Sort();
            foreach (string str in ss)
            {
                allquotes.Add(str);
            }
            //allquotes.Sort();
            this.quotesList.ItemsSource = allquotes;
            this.quotesListToAdd.ItemsSource = quotesToAdd;
        }

        private void ListBoxItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            quotesToAdd.Add((sender as ListBoxItem).Content.ToString());
            allquotes.Remove((sender as ListBoxItem).Content.ToString());
        }

        private void ListBoxItem_MouseDoubleClick1(object sender, RoutedEventArgs e)
        {
            allquotes.Add((sender as ListBoxItem).Content.ToString());
            quotesToAdd.Remove((sender as ListBoxItem).Content.ToString());
        }


        private void ctpInit()
        {
            //MessageBox.Show(System.Environment.CurrentDirectory);
            
            try
            {
                showInfo("ctp连接状态: 初始化ctp api...");
                api = new XApi(System.AppDomain.CurrentDomain.BaseDirectory + "QuantBox_CTP_Quote.dll");
                showInfo("ctp连接状态: 初始化ctp api 成功！");
                showInfo("ctp连接状态: 初始化ctp参数及设置回调函数...");
                api.Server.BrokerID = "2030";
                api.Server.Address = "tcp://asp-sim2-md1.financial-trading-platform.com:26213";

                
                api.OnConnectionStatus += OnConnectionStatus;
                api.OnRtnDepthMarketData += OnRtnDepthMarketData;

                showInfo("ctp连接状态: 初始化ctp参数及设置回调函数 成功！");
                showInfo("ctp连接状态: 开始连接ctp...");
                api.Connect();
            }
            catch (Exception ex)
            {
                showInfo("发生错误: " + ex.Message);
            }


            //Thread.Sleep(3000);
            //showInfo("begin dispose");
            //api.Dispose();

            //Thread.Sleep(5 * 1000);
        }

        /// <summary>
        /// 订阅行情+数据库创建表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void getDataBt_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = sqlCon;
            //创建表
            
            foreach (string q in quotesToAdd)
            {
                try
                {
                    string sql = "CREATE TABLE IF NOT EXISTS " + q + "(id INT NOT NULL AUTO_INCREMENT, PRIMARY KEY (id));";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    //创建列
                    
                    foreach (var v in t.GetFields())
                    {
                        string colSql = string.Empty;
                        if (v.FieldType.ToString() == "System.String")
                        {
                            colSql = "ALTER TABLE " + q + " ADD COLUMN " + v.Name + " VARCHAR(20)";
                        }
                        else if (v.FieldType.ToString() == "System.Int32")
                        {
                            colSql = "ALTER TABLE " + q + " ADD COLUMN " + v.Name + " INT";
                        }
                        else
                        {
                            colSql = "ALTER TABLE " + q + " ADD COLUMN " + v.Name + " DOUBLE";
                        }
                        cmd.CommandText = colSql;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    showInfo("创建表出错" +ex.Message);
                }

                foreach (string quote in quotesToAdd)
                {
                    api.Subscribe(quote, "");
                }

            }


            //之后修改onrtndepth

        }

        private void showInfo(string info)
        {
            try
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.infoTxt.Text += DateTime.Now.ToLongTimeString() + " " + info + System.Environment.NewLine;
                    this.infoTxtBox.ScrollToEnd();
                }));
            }
            catch (Exception ex)
            {
                showInfo(ex.Message);
            }
            
           
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



        private void infoTxtBoxInit()
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
            
            //tickDataGrid.AutoGenerateColumns = false;

            //tickDataGrid.Items.Clear();

            //tickDataGrid.ItemsSource = tickDataTable.AsDataView();
            //tickDataGrid.AutoGenerateColumns = true;
            //Type t = typeof(DepthMarketDataField);
            //if (t == null)
            //    return;
            //foreach (var v in t.GetFields())
            //{
            //    DataGridTextColumn dgtc = new DataGridTextColumn();
            //    dgtc.Header = v.Name;
            //    Binding b = new Binding();
            //    b.Source = ocdt[ocdt.Count - 1].Columns[v.Name];

            //    dgtc.Binding = b;
            //    tickDataGrid.Columns.Add(dgtc);

            //}
        }

        void ocdt_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MySqlCommand cmd = new MySqlCommand();
            //string sql = "ALTER TABLE table1 ADD COLUMN kk VARCHAR(30)";
            //cmd.CommandText = sql;
            //cmd.Connection = sqlCon;
            //cmd.ExecuteNonQuery();
            //sqlCon.Close();

            //Type t = typeof(DepthMarketDataField);
            //foreach (var v in t.GetFields())
            //{
            //    for (int i = 0; i < tickDataTable.Rows.Count; i++)
            //    {

            //        showInfo(v.Name + " " + tickDataTable.Rows[i][v.Name].ToString() + " " +v.FieldType);
            //    }

            //}

            foreach (string str in quotesToAdd)
            {
                showInfo(str);
            }



        }

        private List<string> getAllID()
        {
            List<string> allId = new List<string>();
            string url = "http://money.finance.sina.com.cn/d/api/openapi_proxy.php/?__s=[[%22qhhq%22,%22qbhy%22,%22zdf%22,1000]]&callback=getData.futures_qhhq_gnqh";
            string strMsg = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                strMsg = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();
                response.Close();
            }
            catch
            { }
            string pattern = @"\[\[";
            Regex regex = new Regex(pattern);
            string[] strPart = regex.Split(strMsg);
            if (strPart.Length >= 2)
            {
                string[] subStr = strPart[1].Split('[');
                foreach (string str in subStr)
                {
                    string[] finalStr = str.Split(',');
                    if (finalStr.Length > 2)
                    {
                        string idStr = finalStr[1].Trim('"');
                        allId.Add(idStr);
                    }
                }
                List<string> idAfterAjust = idAjust(allId);
                return idAfterAjust;
                //return allId;
            }
            else
            {
                MessageBox.Show("获取合约失败");
                return allId;
            }

        }


        private List<string> idAjust(List<string> originalId)
        {
            List<string> newAllId = new List<string>();
            foreach (string id in originalId)
            {
                string newId = id;
                if (id.Contains("SR") || id.Contains("TA") || id.Contains("CF") || id.Contains("RI") || id.Contains("WH") || id.Contains("OI") || id.Contains("FG") || id.Contains("TC") || id.Contains("MA") || id.Contains("RM"))
                {
                    if (id.Length > 3)
                    {
                        newId = id.Remove(2, 1);
                    }
                }
                else if (id.Contains("IF") || id.Contains("TF"))
                {

                }
                else
                {
                    newId = id.ToLower();
                }
                newAllId.Add(newId);
            }
            return newAllId;
        }


    }
}
