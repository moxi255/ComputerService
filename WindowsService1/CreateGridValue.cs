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
    class CreateGridValue
    {
        public void InsertGridValue()
        {
            Timer timer1 = new Timer();

            timer1.AutoReset = true;
            timer1.Interval = 2*60* 60 * 1000; // 60 seconds
            timer1.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer1.Start();
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
           

            DapperDll dapperDll = new DapperDll(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
            dapperDll.DeleteGridvalue();
            DateTime WeekOFFirstDay = DateTime.Today.AddDays(-7);
            DateTime WeekOFLastDay = DateTime.Today;
            List<Gridding> griddings = dapperDll.GetGrid();
            List<SysDict> sysDicts = dapperDll.findAllVillsgr();

            List<UserInfo> gridUsers = dapperDll.findGridUser();
            List<Log> logs = dapperDll.GetUserLog(WeekOFFirstDay, WeekOFLastDay);
            List<Problem> problemAlls = dapperDll.findProblem();
            List<Problem> problems = problemAlls.Where(t => t.GridId != null && t.CreateTime > WeekOFFirstDay && t.CreateTime < WeekOFLastDay.AddDays(1) && t.Delete == false).ToList();
            List<GridVillager> All = dapperDll.findGridVillagerNumber();
            List<TodaySatisfaction> sum = dapperDll.GetTodaySatisfaction();
            List<WGZL.Models.Task> tasks = dapperDll.findTask(DateTime.Today.AddDays(-30), DateTime.Today);
            List<WorkHistory> workHistories = dapperDll.findWorkHistory(DateTime.Today.AddDays(-30), DateTime.Today);
            List<GridValue> GridValues = new List<GridValue>();
            foreach (SysDict item in sysDicts)
            {
               
               
                GridValue gridValue = new GridValue();
                gridValue.HealthKey = item.DictKey.ToString();
                
                //镇计算
                List<long> gridding = new List<long>();
                if (item.DictKey.Count() == 12)
                {
                    gridding=griddings.Where(t => t.ThirdAreaKey == item.DictKey).Select(t => t.Id).ToList();
                }
                else if (item.DictKey.Count() == 9)
                {
                    gridding = griddings.Where(t => t.SecondAreaKey == item.DictKey).Select(t => t.Id).ToList();
                }
                else if (item.DictKey.Count() == 6)
                {
                    gridding = griddings.Select(t => t.Id).ToList();
                }
               
                int gridUsersNumber = gridUsers.Count(t => t.GridId != null && gridding.Contains((long)t.GridId));

                List<long> griduserIds = gridUsers.Select(t => t.Id).ToList();
                //当前村村民数
                int AllNumber = (int)All.Where(t => gridding.Contains(t.grid)).Sum(t=>t.number) ;
                TimeSpan span = WeekOFLastDay.Subtract(WeekOFFirstDay);
                int dayDiff = span.Days;
                
                List<UserData> userLogDatas = new List<UserData>();
                for (int i = 0; i < dayDiff; i++)
                {
                    UserData userData = new UserData();
                    DateTime beginday = WeekOFFirstDay.AddDays(i);
                    DateTime endday = WeekOFLastDay.AddDays(i + 1);
                    //WLog.WriteLog(item.Id + "a" + DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss:ffff"));
                    int logsNumber = logs.Count(t => t.GridId!=null&& (gridding.Contains((long)t.GridId) )&& t.CreateTime > beginday && t.CreateTime < endday);
                    //WLog.WriteLog(item.Id + "b" + DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss:ffff"));
                    if (gridUsersNumber == 0)
                    {
                        userData.PunchRate = 0;
                    }
                    else
                    {
                        userData.PunchRate = ((decimal)logsNumber / gridUsersNumber);
                    }
                    int problemsNumber = problems.Count(t => gridding.Contains((long)t.GridId) && t.CreateTime > beginday && t.CreateTime < endday); ;
                    if (AllNumber == 0)
                    {
                        userData.problemRate = 0;
                    }
                    else
                    {
                        if (problemsNumber == 0)
                        {
                            problemsNumber = 1;
                        }
                        userData.problemRate = ((decimal)problemsNumber / AllNumber);
                    }
                    userData.dateTime = beginday;
                    userLogDatas.Add(userData);

                }
                gridValue.AllNumber = AllNumber;
                gridValue.GridNumber = gridUsersNumber;
                
                //int gridUsersNumber = _context.UserInfo.AsNoTracking().Count(t => griddings.Contains((long)t.GridId));
                //int logsNumber = logs.Count(t => griddings.Contains((long)t.GridId));
                decimal PunchRate;
                decimal problemRate;
                decimal OkRate;
                //问题处理率
                decimal problemDealRate;
                int finishTaskCount;
                PunchRate = userLogDatas.Sum(t => t.PunchRate) / 7;
                problemRate = userLogDatas.Sum(t => t.problemRate) / 7;
                Decimal sumNumber = sum.Where(t => gridding.Contains((long)t.GridId)).Sum(t => t.Rate);
                if (gridding.Count() == 0)
                {
                    OkRate = 0;
                }
                else
                {
                    OkRate = (decimal)sumNumber / gridding.Count;
                }
                gridValue.PunchRate = PunchRate;
                gridValue.problemRate = problemRate;
                gridValue.OkRate = OkRate;
                List<Problem> myproblem = problemAlls.Where(t => t.GridId != null && gridding.Contains((long)t.GridId)).ToList();
                int finish = myproblem.Count(t => t.Status == 3 || t.Status == 4);
                if (myproblem.Count() == 0)
                {
                    problemDealRate = (decimal)0.1;
                }
                else
                {
                    if (finish == 0)
                    {
                        finish = 1;
                    }
                    problemDealRate = (decimal)finish / myproblem.Count;
                }
               
                gridValue.FinishProblem = finish;
                gridValue.AllProblem = myproblem.Count;
                gridValue.problemDealRate = problemDealRate;
                int taskCount = tasks.Count(t => gridding.Contains(t.GridID));
                int workCount = workHistories.Count(t => gridding.Contains(t.GridId));
                finishTaskCount = taskCount + workCount;
                if (finishTaskCount == 0)
                {
                    finishTaskCount = 1;
                }

                gridValue.finishTaskCount = finishTaskCount;


                gridValue.HealthValue = Math.Round(((PunchRate * OkRate * problemRate * finishTaskCount * problemDealRate) * 100), 2).ToString();

                // log.Debug(item.Name + "" + PunchRate + " | " + OkRate + " | " + problemRate + " | " + finishTaskCount + " | " + problemDealRate + " | " + mapData.value);

                if (gridValue.HealthValue == "0"||gridValue.HealthValue == "0.0")
                {
                    gridValue.HealthValue = "0.00";
                }
                gridValue.CreateTime = DateTime.Now;
                GridValues.Add(gridValue);


            }
            foreach (Gridding item in griddings)
            {
                GridValue gridValue = new GridValue();
                gridValue.HealthKey = item.Id.ToString();

                //镇计算
                List<long> gridding = new List<long>();
                gridding.Add(item.Id);
                int gridUsersNumber = gridUsers.Count(t => t.GridId != null && gridding.Contains((long)t.GridId));

                //当前村村民数
                int AllNumber = All.Count(t => gridding.Contains(t.grid));
                TimeSpan span = WeekOFLastDay.Subtract(WeekOFFirstDay);
                int dayDiff = span.Days;
                List<long> griduserIds=gridUsers.Select(t => t.Id).ToList();
                List<UserData> userLogDatas = new List<UserData>();
                for (int i = 0; i < dayDiff; i++)
                {
                    
                    UserData userData = new UserData();
                    DateTime beginday = WeekOFFirstDay.AddDays(i);
                    DateTime endday = WeekOFLastDay.AddDays(i + 1);
                    int logsNumber = logs.Count(t => t.GridId != null && (gridding.Contains((long)t.GridId)) && t.CreateTime > beginday && t.CreateTime < endday);
                    if (gridUsersNumber == 0)
                    {
                        userData.PunchRate = 0;
                    }
                    else
                    {
                        userData.PunchRate = ((decimal)logsNumber / gridUsersNumber);
                    }
                    int problemsNumber = problems.Count(t => t.GridId != null && gridding.Contains((long)t.GridId) && t.CreateTime > beginday && t.CreateTime < endday); ;
                    if (AllNumber == 0)
                    {
                        userData.problemRate = 0;
                    }
                    else
                    {
                        if (problemsNumber == 0)
                        {
                            problemsNumber = 1;
                        }
                        userData.problemRate = ((decimal)problemsNumber / AllNumber);
                    }
                    userData.dateTime = beginday;
                    userLogDatas.Add(userData);

                }
                gridValue.AllNumber = AllNumber;
                gridValue.GridNumber = gridUsersNumber;

                //int gridUsersNumber = _context.UserInfo.AsNoTracking().Count(t => griddings.Contains((long)t.GridId));
                //int logsNumber = logs.Count(t => griddings.Contains((long)t.GridId));
                decimal PunchRate;
                decimal problemRate;
                decimal OkRate;
                //问题处理率
                decimal problemDealRate;
                int finishTaskCount;
                PunchRate = userLogDatas.Sum(t => t.PunchRate) / 7;
                problemRate = userLogDatas.Sum(t => t.problemRate) / 7;
                Decimal sumNumber = sum.Where(t =>  gridding.Contains((long)t.GridId)).Sum(t => t.Rate);
                if (gridding.Count() == 0)
                {
                    OkRate = 0;
                }
                else
                {
                    OkRate = (decimal)sumNumber / gridding.Count;
                }
                gridValue.PunchRate = PunchRate;
                gridValue.problemRate = problemRate;
                gridValue.OkRate = OkRate;
                List<Problem> myproblem = problemAlls.Where(t => t.GridId != null && gridding.Contains((long)t.GridId)).ToList();
                int finish = myproblem.Count(t => t.Status == 3 || t.Status == 4);
                if (myproblem.Count() == 0)
                {
                    problemDealRate = (decimal)0.1;
                }
                else
                {
                    if (finish == 0)
                    {
                        finish = 1;
                    }
                    problemDealRate = (decimal)finish / myproblem.Count;
                }
                gridValue.FinishProblem = finish;
                gridValue.AllProblem = myproblem.Count;
                gridValue.problemDealRate = problemDealRate;
                int taskCount = tasks.Count(t => gridding.Contains(t.GridID));
                int workCount = workHistories.Count(t => gridding.Contains(t.GridId));
                finishTaskCount = taskCount + workCount;
                if (finishTaskCount == 0)
                {
                    finishTaskCount = 1;
                }

                gridValue.finishTaskCount = finishTaskCount;


                gridValue.HealthValue = Math.Round(((PunchRate * OkRate * problemRate * finishTaskCount * problemDealRate) * 100), 2).ToString();

                // log.Debug(item.Name + "" + PunchRate + " | " + OkRate + " | " + problemRate + " | " + finishTaskCount + " | " + problemDealRate + " | " + mapData.value);
                if (gridValue.HealthValue == "0" || gridValue.HealthValue == "0.0")
                {
                    gridValue.HealthValue = "0.00";
                }
                gridValue.CreateTime = DateTime.Now;

                GridValues.Add(gridValue);


            }
            dapperDll.InsertGridvalue(GridValues);
        }





        public void test()
        {
            // TODO: Insert monitoring activities here.


            DapperDll dapperDll = new DapperDll(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
            dapperDll.DeleteGridvalue();
            DateTime WeekOFFirstDay = DateTime.Today.AddDays(-7);
            DateTime WeekOFLastDay = DateTime.Today;
            List<Gridding> griddings = dapperDll.GetGrid();
            List<SysDict> sysDicts = dapperDll.findAllVillsgr();

            List<UserInfo> gridUsers = dapperDll.findGridUser();
            List<Log> logs = dapperDll.GetUserLog(WeekOFFirstDay, WeekOFLastDay);
            List<Problem> problemAlls = dapperDll.findProblem();
            List<Problem> problems = problemAlls.Where(t => t.GridId != null && t.CreateTime > WeekOFFirstDay && t.CreateTime < WeekOFLastDay.AddDays(1) && t.Delete == false).ToList();
            List<GridVillager> All = dapperDll.findGridVillagerNumber();
            List<TodaySatisfaction> sum = dapperDll.GetTodaySatisfaction();
            List<WGZL.Models.Task> tasks = dapperDll.findTask(DateTime.Today.AddDays(-30), DateTime.Today);
            List<WorkHistory> workHistories = dapperDll.findWorkHistory(DateTime.Today.AddDays(-30), DateTime.Today);
            List<GridValue> GridValues = new List<GridValue>();
            foreach (SysDict item in sysDicts)
            {
                if(item.DictKey== "004001001030")
                {

                }

                GridValue gridValue = new GridValue();
                gridValue.HealthKey = item.DictKey.ToString();

                //镇计算
                List<long> gridding = new List<long>();
                if (item.DictKey.Count() == 12)
                {
                    gridding = griddings.Where(t => t.ThirdAreaKey == item.DictKey).Select(t => t.Id).ToList();
                }
                else if (item.DictKey.Count() == 9)
                {
                    gridding = griddings.Where(t => t.SecondAreaKey == item.DictKey).Select(t => t.Id).ToList();
                }
                else if (item.DictKey.Count() == 6)
                {
                    gridding = griddings.Select(t => t.Id).ToList();
                }

                int gridUsersNumber = gridUsers.Count(t => t.GridId != null && gridding.Contains((long)t.GridId));

                List<long> griduserIds = gridUsers.Select(t => t.Id).ToList();
                //当前村村民数
                int AllNumber = (int)All.Where(t => gridding.Contains(t.grid)).Sum(t => t.number);
                TimeSpan span = WeekOFLastDay.Subtract(WeekOFFirstDay);
                int dayDiff = span.Days;

                List<UserData> userLogDatas = new List<UserData>();
                for (int i = 0; i < dayDiff; i++)
                {
                    UserData userData = new UserData();
                    DateTime beginday = WeekOFFirstDay.AddDays(i);
                    DateTime endday = WeekOFLastDay.AddDays(i + 1);
                    //WLog.WriteLog(item.Id + "a" + DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss:ffff"));
                    int logsNumber = logs.Count(t => t.GridId != null && (gridding.Contains((long)t.GridId)) && t.CreateTime > beginday && t.CreateTime < endday);
                    //WLog.WriteLog(item.Id + "b" + DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss:ffff"));
                    if (gridUsersNumber == 0)
                    {
                        userData.PunchRate = 0;
                    }
                    else
                    {
                        userData.PunchRate = ((decimal)logsNumber / gridUsersNumber);
                    }
                    int problemsNumber = problems.Count(t => gridding.Contains((long)t.GridId) && t.CreateTime > beginday && t.CreateTime < endday); ;
                    if (AllNumber == 0)
                    {
                        userData.problemRate = 0;
                    }
                    else
                    {
                        if (problemsNumber == 0)
                        {
                            problemsNumber = 1;
                        }
                        userData.problemRate = ((decimal)problemsNumber / AllNumber);
                    }
                    userData.dateTime = beginday;
                    userLogDatas.Add(userData);

                }
                gridValue.AllNumber = AllNumber;
                gridValue.GridNumber = gridUsersNumber;

                //int gridUsersNumber = _context.UserInfo.AsNoTracking().Count(t => griddings.Contains((long)t.GridId));
                //int logsNumber = logs.Count(t => griddings.Contains((long)t.GridId));
                decimal PunchRate;
                decimal problemRate;
                decimal OkRate;
                //问题处理率
                decimal problemDealRate;
                int finishTaskCount;
                PunchRate = userLogDatas.Sum(t => t.PunchRate) / 7;
                problemRate = userLogDatas.Sum(t => t.problemRate) / 7;
                Decimal sumNumber = sum.Where(t => gridding.Contains((long)t.GridId)).Sum(t => t.Rate);
                if (gridding.Count() == 0)
                {
                    OkRate = 0;
                }
                else
                {
                    OkRate = (decimal)sumNumber / gridding.Count;
                }
                gridValue.PunchRate = PunchRate;
                gridValue.problemRate = problemRate;
                gridValue.OkRate = OkRate;
                List<Problem> myproblem = problemAlls.Where(t => t.GridId != null && gridding.Contains((long)t.GridId)).ToList();
                int finish = myproblem.Count(t => t.Status == 3 || t.Status == 4);
                if (myproblem.Count() == 0)
                {
                    problemDealRate = (decimal)0.1;
                }
                else
                {
                    if (finish == 0)
                    {
                        finish = 1;
                    }
                    problemDealRate = (decimal)finish / myproblem.Count;
                }

                gridValue.FinishProblem = finish;
                gridValue.AllProblem = myproblem.Count;
                gridValue.problemDealRate = problemDealRate;
                int taskCount = tasks.Count(t => gridding.Contains(t.GridID));
                int workCount = workHistories.Count(t => gridding.Contains(t.GridId));
                finishTaskCount = taskCount + workCount;
                if (finishTaskCount == 0)
                {
                    finishTaskCount = 1;
                }

                gridValue.finishTaskCount = finishTaskCount;


                gridValue.HealthValue = Math.Round(((PunchRate * OkRate * problemRate * finishTaskCount * problemDealRate) * 100), 2).ToString();

                // log.Debug(item.Name + "" + PunchRate + " | " + OkRate + " | " + problemRate + " | " + finishTaskCount + " | " + problemDealRate + " | " + mapData.value);

                if (gridValue.HealthValue == "0" || gridValue.HealthValue == "0.0")
                {
                    gridValue.HealthValue = "0.00";
                }
                gridValue.CreateTime = DateTime.Now;
                GridValues.Add(gridValue);


            }
            foreach (Gridding item in griddings)
            {
                GridValue gridValue = new GridValue();
                gridValue.HealthKey = item.Id.ToString();

                //镇计算
                List<long> gridding = new List<long>();
                gridding.Add(item.Id);
                int gridUsersNumber = gridUsers.Count(t => t.GridId != null && gridding.Contains((long)t.GridId));

                //当前村村民数
                int AllNumber = All.Count(t => gridding.Contains(t.grid));
                TimeSpan span = WeekOFLastDay.Subtract(WeekOFFirstDay);
                int dayDiff = span.Days;
                List<long> griduserIds = gridUsers.Select(t => t.Id).ToList();
                List<UserData> userLogDatas = new List<UserData>();
                for (int i = 0; i < dayDiff; i++)
                {

                    UserData userData = new UserData();
                    DateTime beginday = WeekOFFirstDay.AddDays(i);
                    DateTime endday = WeekOFLastDay.AddDays(i + 1);
                    int logsNumber = logs.Count(t => t.GridId != null && (gridding.Contains((long)t.GridId)) && t.CreateTime > beginday && t.CreateTime < endday);
                    if (gridUsersNumber == 0)
                    {
                        userData.PunchRate = 0;
                    }
                    else
                    {
                        userData.PunchRate = ((decimal)logsNumber / gridUsersNumber);
                    }
                    int problemsNumber = problems.Count(t => t.GridId != null && gridding.Contains((long)t.GridId) && t.CreateTime > beginday && t.CreateTime < endday); ;
                    if (AllNumber == 0)
                    {
                        userData.problemRate = 0;
                    }
                    else
                    {
                        if (problemsNumber == 0)
                        {
                            problemsNumber = 1;
                        }
                        userData.problemRate = ((decimal)problemsNumber / AllNumber);
                    }
                    userData.dateTime = beginday;
                    userLogDatas.Add(userData);

                }
                gridValue.AllNumber = AllNumber;
                gridValue.GridNumber = gridUsersNumber;

                //int gridUsersNumber = _context.UserInfo.AsNoTracking().Count(t => griddings.Contains((long)t.GridId));
                //int logsNumber = logs.Count(t => griddings.Contains((long)t.GridId));
                decimal PunchRate;
                decimal problemRate;
                decimal OkRate;
                //问题处理率
                decimal problemDealRate;
                int finishTaskCount;
                PunchRate = userLogDatas.Sum(t => t.PunchRate) / 7;
                problemRate = userLogDatas.Sum(t => t.problemRate) / 7;
                Decimal sumNumber = sum.Where(t => gridding.Contains((long)t.GridId)).Sum(t => t.Rate);
                if (gridding.Count() == 0)
                {
                    OkRate = 0;
                }
                else
                {
                    OkRate = (decimal)sumNumber / gridding.Count;
                }
                gridValue.PunchRate = PunchRate;
                gridValue.problemRate = problemRate;
                gridValue.OkRate = OkRate;
                List<Problem> myproblem = problemAlls.Where(t => t.GridId != null && gridding.Contains((long)t.GridId)).ToList();
                int finish = myproblem.Count(t => t.Status == 3 || t.Status == 4);
                if (myproblem.Count() == 0)
                {
                    problemDealRate = (decimal)0.1;
                }
                else
                {
                    if (finish == 0)
                    {
                        finish = 1;
                    }
                    problemDealRate = (decimal)finish / myproblem.Count;
                }
                gridValue.FinishProblem = finish;
                gridValue.AllProblem = myproblem.Count;
                gridValue.problemDealRate = problemDealRate;
                int taskCount = tasks.Count(t => gridding.Contains(t.GridID));
                int workCount = workHistories.Count(t => gridding.Contains(t.GridId));
                finishTaskCount = taskCount + workCount;
                if (finishTaskCount == 0)
                {
                    finishTaskCount = 1;
                }

                gridValue.finishTaskCount = finishTaskCount;


                gridValue.HealthValue = Math.Round(((PunchRate * OkRate * problemRate * finishTaskCount * problemDealRate) * 100), 2).ToString();

                // log.Debug(item.Name + "" + PunchRate + " | " + OkRate + " | " + problemRate + " | " + finishTaskCount + " | " + problemDealRate + " | " + mapData.value);
                if (gridValue.HealthValue == "0" || gridValue.HealthValue == "0.0")
                {
                    gridValue.HealthValue = "0.00";
                }
                gridValue.CreateTime = DateTime.Now;

                GridValues.Add(gridValue);


            }
            dapperDll.InsertGridvalue(GridValues);
        }

        }
}
