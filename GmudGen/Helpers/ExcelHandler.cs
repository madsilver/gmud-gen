using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.IO;
using System.Windows;
using Microsoft.Office.Interop.Excel;

namespace GmudGen.Helpers
{
    class ExcelHandler
    {
        private Excel.Application app;
        private Excel.Workbook workbook;
        private string fileName;
        private Excel.Worksheet worksheet;
        private Excel.Sheets sheet;

        /// <summary>
        /// Construtor da classe ExcelHandler
        /// </summary>
        public ExcelHandler(string filename)
        {
            try
            {
                app = new Excel.Application();
                app.Visible = true;
                fileName = filename;
                workbook = app.Workbooks.Open(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                                  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                                  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                                  Type.Missing, Type.Missing);
                worksheet = workbook.Worksheets[1] as Excel.Worksheet;
                sheet = workbook.Worksheets;
            }
            catch (Exception ex)
            {
                string msg = string.Format("Erro na abertura do arquivo {0}.\n{1}", filename, ex.Message);
                MessageBox.Show(msg);
            }
        }

        /// <summary>
        /// Define a sheet
        /// </summary>
        /// <param name="sheetName"></param>
        public void SetSheet(string sheetName)
        {
            try
            {
                worksheet = sheet.get_Item(sheetName);
                worksheet.Select();
            }
            catch (Exception ex)
            {
                string msg = string.Format("Erro na seleção da sheet {0}.\n{1}", sheetName, ex.Message);
                MessageBox.Show(msg);
            }

        }

        /// <summary>
        /// Escreve na célula
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columm"></param>
        /// <param name="value"></param>
        public void Write(int row, int columm, string value)
        {
            try
            {
                worksheet.Cells[row, columm] = value;
            }
            catch (Exception ex)
            {
                char col = Convert.ToChar(columm + 64);
                string cell = new string(col, 1) + row.ToString();

                string msg = string.Format("Erro na escrita da célula {0}: {1}.\n{2}", cell, value, ex.Message);
                MessageBox.Show(msg);
            }

        }

        public string Read(int row, int columm)
        {
            var s = worksheet.Cells[row, columm].Value;
            if (s != null) s = Convert.ToString(s);
            return s;
        }

        public void InsertLine(int row)
        {
            Range line = (Range)worksheet.Rows[row];
            line.Insert();
        }

        /// <summary>
        /// Fecha o arquivo
        /// </summary>
        public void Close()
        {
            try
            {
                workbook.Close(false, this.fileName, false);
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            }
            catch (Exception ex)
            {
                string msg = string.Format("Erro ao finalizar o arquivo {0}.\n{1}", this.fileName, ex.Message);
                MessageBox.Show(msg);
            }
        }
    }
}
