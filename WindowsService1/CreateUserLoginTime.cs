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
    class CreateUserLoginTime
    {
        public void InsertUserLoginTime()
        {
            Timer timer1 = new Timer();

            timer1.AutoReset = true;
            timer1.Interval = 4*60* 60 * 1000; // 60 seconds
            timer1.Elapsed += new ElapsedEventHandler(this.CreateUserLoginTimeer);
            timer1.Start();
        }

        public void CreateUserLoginTimeer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            if (DateTime.Now > DateTime.Today && DateTime.Now < DateTime.Today.AddHours(6))
            {
                DapperDll dapperDll = new DapperDll(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
                dapperDll.DeleteUserLoginTime();
                int count = dapperDll.GetTodayUserLoginTime().Count();
                if (count > 0)
                {

                }
                else
                {
                    DateTime start = DateTime.Today.AddDays(-5);
                    DateTime end = DateTime.Today;
                    List<Log> userLogs = dapperDll.GetUserLog(start, end);
                    List<UserInfo> userIds = dapperDll.GetUserInfoId();
                    List<UserLoginTime> userLoginTimes = new List<UserLoginTime>();
                    foreach (var item in userIds)
                    {
                        UserLoginTime userLoginTime = new UserLoginTime();
                        userLoginTime.CreateTime = DateTime.Now;
                        userLoginTime.Time = userLogs.Count(t => t.openId == item.OpenId || t.userInfoId == item.Id);
                        userLoginTime.UserID = item.Id;
                        userLoginTimes.Add(userLoginTime);

                    }
                    dapperDll.InsertUserLoginTime(userLoginTimes);
                }

            }



        }
    }
}
