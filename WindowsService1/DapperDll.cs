using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WGZL.Models;
using WindowsService1;

namespace WGZL.Data
{
    public class DapperDll
    {
        private string _sqlStr = "Data Source=127.0.0.1;Database=wgzl;User ID=root;Password=123456;pooling=true;port=3306;sslmode=none;CharSet=utf8;";
        public DapperDll(string sqlstr)
        {
            this._sqlStr = sqlstr;

        }
        public List<ConsultEvaluate> GetConsultEvaluates()
        {
            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {

                var result = connection.Query<ConsultEvaluate>("select * from ConsultEvaluate where CreateTime<@CreateTime", new { CreateTime = DateTime.Now });
                return result.ToList();
            }
        }
        public void InsertConsultEvaluates(TodaySatisfaction todaySatisfaction)
        {
            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {
                var result = connection.Execute("Insert into TodaySatisfaction(CreateTime,GridId, Rate) values (@CreateTime, @GridId, @Rate)", todaySatisfaction);
            }
        }
        /// <summary>
        /// 查询今天满意度
        /// </summary>
        /// <returns></returns>
        public List<TodaySatisfaction> GetTodaySatisfaction()
        {
            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {
                var result = connection.Query<TodaySatisfaction>("select * from TodaySatisfaction where CreateTime>@CreateTime", new { CreateTime = DateTime.Today });
                return result.ToList();
            }
        }
        public void DeleteTodaySatisfaction()
        {
            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {
                var result = connection.Execute("DELETE FROM todaysatisfaction where todaysatisfaction.CreateTime<@CreateTime", new { CreateTime = DateTime.Today.AddDays(-7) });
            }
        }
        /// <summary>
        /// 查找所有的网格
        /// </summary>
        /// <returns></returns>
        public List<Gridding> GetGrid()
        {

            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {

                var result = connection.Query<Gridding>("select * from Gridding");
                return result.ToList();
            }
        }
        public List<UserInfo> GetUserInfoId()
        {

            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {

                var result = connection.Query<UserInfo>("select ID, OpenId from UserInfo where IsDelete=false");
                return result.ToList();
            }
        }
        public void DeleteUserLoginTime()
        {

            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {

                var result = connection.Execute("DELETE FROM UserLoginTime where UserLoginTime.CreateTime<@CreateTime", new { CreateTime = DateTime.Today.AddDays(-7) });
            }
        }
        /// <summary>
        /// 得到今天用户登录时间
        /// </summary>
        /// <returns></returns>
        public List<UserLoginTime> GetTodayUserLoginTime()
        {
            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {
                var result = connection.Query<UserLoginTime>("select * from UserLoginTime where CreateTime>@CreateTime", new { CreateTime = DateTime.Today });
            return result.ToList();
            }
        }
        /// <summary>
        /// 获取用户登录日志
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public List<Log> GetUserLog(DateTime Start,DateTime End)
        {
            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {
                var result = connection.Query<Log>("select * from Log where CreateTime>@Start and CreateTime<@End  and Village=false", new { Start = Start, End = End });
                return result.ToList();
            }  

           
        }

        public void InsertUserLoginTime(List<UserLoginTime> userLoginTimes)
        {
            IDbConnection connection = new MySqlConnection(_sqlStr);

            connection.Execute("insert into UserLoginTime(CreateTime,UserID,Time) values (@CreateTime, @UserID, @Time)", userLoginTimes);
          
        }

        /// <summary>
        /// 查找所有的村
        /// </summary>
        /// <returns></returns>
        public List<SysDict> findAllVillsgr()
        {

            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {
                var result = connection.Query<SysDict>("SELECT * from sysdict where DictKey like '004001%' and(LENGTH(DictKey) >= 6 and  LENGTH(DictKey) <= 12)");
                return result.ToList();
            }
        }

        /// <summary>
        /// 查找网格员
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> findGridUser()
        {
            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {
                var result = connection.Query<UserInfo>("SELECT * from userinfo where userRoleId like '08d7bf46-2dd8-ca88-6c74-77fa78fb64cd' and  IsDelete = FALSE and GridID is not null");
                return result.ToList();
            }
        }

        /// <summary>
        /// 查询所有问题
        /// </summary>
        /// <returns></returns>
        public List<Problem> findProblem()
        {
            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {
                var result = connection.Query<Problem>("SELECT * from problem where problem.`Delete`=false and GridID  is not null");
                return result.ToList();
            }
        }
        /// <summary>
        /// 查找网格所有的村民
        /// </summary>
        /// <returns></returns>
        public List<GridVillager> findGridVillagerNumber()
        {
            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {
                var result = connection.Query<GridVillager>("SELECT villager.GridID as grid,count(*) as number from Villager where Status = 1 and IsDelete = false and userRoleId='08d7bf46-f335-d8cc-e6c3-8b9c18cdaaa7' GROUP BY villager.GridID");
                return result.ToList();
            }
        }
        /// <summary>
        /// 工作记录
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public List<Models.Task> findTask(DateTime Start, DateTime End)
        {
            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {
                var result = connection.Query<Models.Task>("select * from Task where CreateTime>@Start &&CreateTime<@End", new { Start = Start, End = End });
                return result.ToList();
            }
        }
        /// <summary>
        /// 工作任务
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public List<WorkHistory> findWorkHistory(DateTime Start, DateTime End)
        {
            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {
                var result = connection.Query<WorkHistory>("select * from WorkHistory where CreateTime>@Start &&CreateTime<@End", new { Start = Start, End = End });
                return result.ToList();
            }
        }
        /// <summary>
        /// 工作任务
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public void DeleteGridvalue()
        {
            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {
                var result = connection.Execute("DELETE FROM gridvalue where gridvalue.CreateTime<@CreateTime", new { CreateTime = DateTime.Today.AddDays(-3) });
            }
        }
        /// <summary>
        /// 工作任务
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public void InsertGridvalue(List<GridValue> gridValues)
        {
            using (IDbConnection connection = new MySqlConnection(_sqlStr))
            {
                connection.Execute("insert into gridvalue(CreateTime,finishTaskCount,problemDealRate,AllProblem,FinishProblem,GridNumber,OkRate,problemRate,PunchRate,AllNumber,HealthValue,HealthKey) values (@CreateTime,@finishTaskCount,@problemDealRate,@AllProblem,@FinishProblem,@GridNumber,@OkRate,@problemRate,@PunchRate,@AllNumber,@HealthValue,@HealthKey)", gridValues);
            }
        }
    }
}
