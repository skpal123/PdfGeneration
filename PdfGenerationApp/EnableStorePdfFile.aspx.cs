using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
namespace PdfGenerationApp
{
    public partial class EnableStorePdfFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cs = System.Configuration.ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString.ToString();
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select * from tblDepartment", con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            PdfPTable pdfPTable=new PdfPTable(GridView1.HeaderRow.Cells.Count);
            foreach (TableCell tableCell in GridView1.HeaderRow.Cells)
            {
                Font font = new Font();
                font.Color = new BaseColor(GridView1.HeaderStyle.ForeColor);
                PdfPCell pdfPCell=new PdfPCell(new Phrase(tableCell.Text,font));
                pdfPCell.BorderColor=new BaseColor(GridView1.HeaderStyle.BackColor);
                pdfPTable.AddCell(pdfPCell);
               
            }
            foreach (GridViewRow gridViewRow in GridView1.Rows)
            {
                
                foreach (TableCell tableCell in gridViewRow.Cells)
                {
                    Font font = new Font();
                    font.Color = new BaseColor(GridView1.RowStyle.ForeColor);
                    PdfPCell pdfPCell=new PdfPCell(new Phrase(tableCell.Text,font));
                    pdfPCell.BorderColor = new BaseColor(GridView1.RowStyle.BackColor);
                    pdfPTable.AddCell(pdfPCell);
                }
            }
            Document pdfDocument=new Document(PageSize.A4,10f,10f,10f,10f);
            PdfWriter.GetInstance(pdfDocument,new FileStream(Server.MapPath("~/PDFdocument/Departments.pdf"),FileMode.Create));
            pdfDocument.Open();
            pdfDocument.Add(pdfPTable);
            pdfDocument.Close();
         
        }
    }
}