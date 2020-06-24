using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WGZL.Data;
using WGZL.Models;

namespace WindowsService1
{
    class CreateGridHealth
    {

        public void InsertCridHealth()
        {
            Timer timer1 = new Timer();

            timer1.AutoReset = true;
            timer1.Interval = 4*60* 60 * 1000; // 60 seconds
            timer1.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer1.Start();
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            if (DateTime.Now > DateTime.Today && DateTime.Now < DateTime.Today.AddHours(6))
            {

                DapperDll dapperDll = new DapperDll(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
                dapperDll.DeleteTodaySatisfaction();
                int count = dapperDll.GetTodaySatisfaction().Count();
                if (count > 0)
                {

                }
                else
                {
                    List<ConsultEvaluate> consultEvaluates = dapperDll.GetConsultEvaluates();
                    List<Gridding> griddings = dapperDll.GetGrid();
                    foreach (var item in griddings)
                    {
                        TodaySatisfaction todaySatisfaction = new TodaySatisfaction();
                        todaySatisfaction.CreateTime = DateTime.Now;
                        int sum = consultEvaluates.Where(t => t.GridId == item.Id).Count();
                        int Satisfaction = consultEvaluates.Where(t => (t.SatisfactionDegree == 0 || t.SatisfactionDegree == 1) && t.GridId == item.Id).Count();
                        todaySatisfaction.GridId = item.Id;

                        if (sum == 0)
                        {
                            todaySatisfaction.Rate = 1;
                        }
                        else
                        {
                            todaySatisfaction.Rate = (decimal)Satisfaction / sum;
                        }
                        dapperDll.InsertConsultEvaluates(todaySatisfaction);
                    }
                }
            }



        }
    }

    

}
