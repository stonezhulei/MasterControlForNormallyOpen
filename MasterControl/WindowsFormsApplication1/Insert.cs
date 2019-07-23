using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace CloseValve
{
    using Newtonsoft.Json;
    public class Insert
    {
        string strconn = "data source=.;initial catalog=WBLT;user id=sa;password=123;MultipleActiveResultSets=True;App=EntityFramework";
        //string strconn = "metadata=res://*/Db.DB.csdl|res://*/Db.DB.ssdl|res://*/Db.DB.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=192.168.250.253;initial catalog=WBLT;persist security info=True;user id=sa;multipleactiveresultsets=True;application name=EntityFramework&quot";


      //  string strconn = "data source=192.168.250.253;initial catalog=WBLT;user id=sa;password=123;MultipleActiveResultSets=True;App=EntityFramework";
        public string Add(SubmitData data)
        {
            using (SqlConnection con = new SqlConnection(strconn))
            {
                try
                {
                    con.Open();
                    SqlParameter[] prs = new SqlParameter[6];
                    prs[0] = new SqlParameter("@pID", System.Data.SqlDbType.NVarChar);
                    prs[1] = new SqlParameter("@EndTime", System.Data.SqlDbType.DateTime);
                    prs[2] = new SqlParameter("@ErrorCode", System.Data.SqlDbType.Int);
                    prs[3] = new SqlParameter("@ErrorMsg", System.Data.SqlDbType.NVarChar);
                    prs[4] = new SqlParameter("@Data", System.Data.SqlDbType.Text);
                    prs[5] = new SqlParameter("@Results", System.Data.SqlDbType.Text);

                    prs[0].Value = data.pID;
                    prs[1].Value = data.EndTime;
                    prs[2].Value = data.ErrorCode;
                    prs[3].Value = data.ErrorMsg;
                    prs[4].Value = JsonConvert.SerializeObject(data.Data);
                    prs[5].Value = JsonConvert.SerializeObject(data.Result);

                    string strIn = "insert into CloserValve (pID,EndTime,ErrorCode,ErrorMsg,Data,Results) values ( @pID , @EndTime , @ErrorCode , @ErrorMsg , @Data ,@Results )";



                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = strIn;
                    cmd.Parameters.AddRange(prs);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = con;
                    return cmd.ExecuteNonQuery() > 0 ? null : "存储失败!";

                }
                catch (Exception ex)
                {
                    return ex.Message;
                    //throw;
                }
                return null;
            }
        }
    }
}
