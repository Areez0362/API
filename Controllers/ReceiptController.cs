using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models.Entities;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController : Controller
    {
        private readonly ItemDBContext itemdbContext;

        public ReceiptController(ItemDBContext itemdbContext)
        {
            this.itemdbContext = itemdbContext;

        }

        [HttpGet]
        [Route("/api/Receipt/GetReceipt/{id:Guid}")]
        [ActionName("GetReceiptInfo")]
        public async Task<IActionResult> GetReceiptInfo([FromRoute] Guid id)
        {
            var innerJoin = from s in itemdbContext.Receipts // outer sequence
                            join st in itemdbContext.SoldItems //inner sequence 
                            on s.ReceiptID equals st.ReceiptID
                            where s.ReceiptID == id 
                            select new
                            {
                                ReceiptID = s.ReceiptID,
                                TotalAmount = s.TotalAmount,
                                Date = s.Date,
                                ItemID = st.ItemID,
                                Price = st.Price,
                                Name = st.Name,
                                Quantity = st.Quantity

                            };
            if (innerJoin == null)
                return NotFound();
            
            return Ok(await innerJoin.ToListAsync());

        }

        [HttpGet]
        [Route("/api/Receipt/GetDailyEarnings/{Date:DateTime}")]
        [ActionName("GetDailyEarnings")]
        public async Task<IActionResult> GetDailyEarnings([FromRoute] DateTime Date)
        {
            var e = from s in itemdbContext.Receipts
                    where s.Date == Date     // yyyy-mm-dd 2022-10-01
                    select s;
            int num=0;
            foreach(var i in e)
            {
                num += i.TotalAmount;
            }
            
            if(e==null) return NotFound();
            await Task.Delay(1);
            return Ok(num);
        }

        [HttpGet]
        [Route("/api/Receipt/GetMonthlyEarnings/{MonthNumber:int}")]
        [ActionName("GetMonthlyEarnings")]
        public async Task<IActionResult> GetMonthlyEarnings([FromRoute] int MonthNumber)
        {
            var e = from s in itemdbContext.Receipts
                    where s.Date.Month == MonthNumber     // yyyy-mm-dd 2022-10-01
                    select s;
            int num = 0;
            foreach (var i in e)
            {
                num += i.TotalAmount;
            }

            if (e == null) return NotFound();
            await Task.Delay(1);
            return Ok(num);
        }

        [HttpGet]
        [Route("/api/Receipt/GetYearlyEarnings/{Year:int}")]
        [ActionName("GetYearlyEarnings")]
        public async Task<IActionResult> GetYearlyEarnings([FromRoute] int Year)
        {
            var e = from s in itemdbContext.Receipts
                    where s.Date.Year == Year     // yyyy-mm-dd 2022-10-01
                    select s;
            int num = 0;
            foreach (var i in e)
            {
                num += i.TotalAmount;
            }

            if (e == null) return NotFound();
            await Task.Delay(1);
            return Ok(num);
        }

        [HttpGet]
        [Route("/api/Receipt/GetWeeklyEarnings/{WeekStartingDay:int}")]
        [ActionName("GetWeeklyEarnings")]
        public async Task<IActionResult> GetWeeklyEarnings([FromRoute] int WeekStartingDay)
        {
            int m = DateTime.Now.Month;
            var e = from s in itemdbContext.Receipts
                    where s.Date.Month == m     // yyyy-mm-dd 2022-10-01
                    select s;
            int num = 0,c=0;
            foreach (var i in e)
            {
                if (i.Date.Day >= WeekStartingDay && i.Date.Day < WeekStartingDay+7)
                {
                    num += i.TotalAmount;
                    Console.WriteLine(i.TotalAmount+" "+i.Date.ToString());
                    c++;
                }
                if (c == 7)
                break;
            }

            if (e == null) return NotFound();
            await Task.Delay(1);
            return Ok(num);
        }


    }
}
