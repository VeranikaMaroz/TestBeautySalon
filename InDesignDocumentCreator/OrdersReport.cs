using System;
using System.Linq;
using System.Net.Http;
using InDesign;
using BeautySalonWebApi.Models;

namespace InDesignDocumentCreator
{
    public class OrdersReport
    {
        public static void CreateOrdersDocument(InDesign.Application myApp, string templatePath, string reportPath, string exportPath)
        {
            Order[] orders = GetOrdersReportData();
            if (orders == null || !orders.Any())
            {
                Console.WriteLine("Could not load report data");
                return;
            }

            InDesign.Document document = (InDesign.Document)myApp.Open(templatePath);
            try
            {
                foreach(var order in orders)
                {
                    FillOrderPage(order, document, orders.Length == 1);
                }

                // Delete first template page
                ((InDesign.Page)document.Pages[1]).Delete();

                document.Export(InDesign.idExportFormat.idPDFType, exportPath);
                document.SaveACopy(reportPath);

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred while creating document:  " + templatePath);
                Console.WriteLine("Exception:  " + e.ToString());
            }
            finally
            {
                document.Close(InDesign.idSaveOptions.idNo);
            }
        }

        private static void FillOrderPage(Order order, Document document, bool isOnePage)
        {
            InDesign.Page page = (InDesign.Page)document.Pages[1];
            if(!isOnePage)
            {
                page = page.Duplicate(idLocationOptions.idAtEnd, page);
            }

            for (int i = 1; i <= page.TextFrames.Count; i++)
            {
                InDesign.TextFrame textFrame = (InDesign.TextFrame)page.TextFrames[i];

                // Date and Customer text
                if (((string)textFrame.Contents).Contains("{0:"))
                {
                    textFrame.Contents = ((string)textFrame.Contents).Replace("{0:Order.Date}", order.Date.ToShortDateString());
                    textFrame.Contents = ((string)textFrame.Contents).Replace("{0:Order.Customer.Name}", order.Customer.Name);
                }

                // Table
                if ((string)textFrame.Contents == "\u0016" || textFrame.Name == "Table")
                {
                    var table = (InDesign.Table)textFrame.Tables[1];

                    // Total Row
                    var totalRow = (InDesign.Row)table.Rows[table.Rows.Count];
                    ((InDesign.Cell)totalRow.Cells[4]).Contents = order.TotalPrice.ToString();

                    var index = 0;
                    foreach(var detail in order.OrderDetails)
                    {
                        var row = (table.Rows.Count <= index + 2) ? // Если строка последняя
                                (InDesign.Row)table.Rows.Add(idLocationOptions.idAfter, (InDesign.Row)table.Rows[index + 1]) :
                                (InDesign.Row)table.Rows[index + 2];

                        // Услуга
                        ((InDesign.Cell)row.Cells[1]).Contents = detail.Product.Name;
                        // Количество
                        ((InDesign.Cell)row.Cells[2]).Contents = detail.Quantity.ToString();
                        // Цена
                        ((InDesign.Cell)row.Cells[3]).Contents = detail.Product.Price.ToString();
                        // Стоимость
                        ((InDesign.Cell)row.Cells[4]).Contents = (detail.Quantity * detail.Product.Price).ToString();

                        index++;
                    }
                }
            }
        }

        private static Order[] GetOrdersReportData()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:4200/api/");
                    //HTTP GET
                    var responseTask = client.GetAsync("order");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Order[]>();
                        readTask.Wait();

                        return readTask.Result;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not load report data");
                Console.WriteLine("Exception:  " + e.Message.ToString() + "   " + e.ToString());
            }
            return null;
        }
    }
}