using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using static System.ComponentModel.Design.ObjectSelectorEditor;
namespace DBMS.SQL.EmployeeSales

{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        public Form1()
        {
            InitializeComponent();
        }

        private void connectDB()
        {
            string server = @".\sqlexpress"; // ชื่อเซิร์ฟเวอร์
            string db = "northwind"; // ชื่อฐานข้อมูล
            string strCon = string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=True;Encrypt=False", server, db);
            conn = new SqlConnection(strCon); // สร้าง Connection ด้วย Connection String
            //string strCon = 
            conn.Open(); // เปิดการเชื่อมต่อ
        }

        private void dusconnectDB()
        {
            conn.Close();
        }

        private void showdata(string sql, DataGridView dgv)
        {
            da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgv.DataSource = ds.Tables[0];
            dgvOrders.DataSource = ds.Tables[0];

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connectDB();
            //string sqlQuery = "SELECT o.OrderID, o.OrderDate, FORMAT(o.RequiredDate, 'dd-MM-yyyy'), s.CompanyName AS ShipperCompany,  CONCAT(e.TitleOfCourtesy, e.FirstName, ' ', e.LastName) as employeeName, c.CompanyName AS CustomerCompany,  c.Phone,  SUM(od.Quantity * od.UnitPrice * (1 - od.Discount)) AS totalCash FROM [Order Details] od JOIN Orders o ON o.OrderID = od.OrderID  JOIN Shippers s ON s.ShipperID = o.ShipVia  JOIN Employees e ON e.EmployeeID = o.EmployeeID  JOIN Customers c ON c.CustomerID = o.CustomerID GROUP BY  o.OrderID,  o.OrderDate, o.RequiredDate,  s.CompanyName, CONCAT(e.TitleOfCourtesy, e.FirstName, ' ', e.LastName), c.CompanyName, c.Phone";
            string sqlQuery = "SELECT * FROM EmployeesList ORDER BY EmployeeID ASC;";
            showdata(sqlQuery, dgvOrders);
        }

        private void dgvOrders_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            // ตรวจสอบว่าคลิกที่คอลัมน์ที่ต้องการหรือไม่
            if (e.RowIndex != -1)
            {
                int id = Convert.ToInt32(dgvOrders.CurrentRow.Cells[0].Value);
                //String sqlQuery = " select p.ProductID, p.ProductName, SUM(od.Quantity) [qty], od.UnitPrice, CONCAT(od.Discount * 100, '%') AS Discount, " +
                //                  " od.Quantity * od.UnitPrice [total price], od.Quantity * od.UnitPrice * (1 - od.Discount) [Discount], " +
                //                  " (od.Quantity * od.UnitPrice) - (od.Quantity * od.UnitPrice * (1 - od.Discount)) [net price] " +
                //                  " from Orders o " +
                //                  " join [Order Details] od on o.OrderID = od.OrderID " +
                //                  " join Products p on p.ProductID = od.ProductID " +
                //                  " where o.OrderID = @id " +
                //                  " group by p.ProductID, p.ProductName, od.UnitPrice, od.Discount, od.Quantity";
                String sqlQuery = "SELECT * FROM Orderlist ORDER BY EmployeeID ASC";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dgcDetails.DataSource = ds.Tables[0];
            }
        }
    }
}
