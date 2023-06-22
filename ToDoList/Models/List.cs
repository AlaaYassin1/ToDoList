using DbManagerStandard;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ToDoList.Models
{
    public class List
    {
        static dbAccess con;
        static OracleConnection aOracleConnection;


        public static PriorityQueue<TaskToDo, int> GetData(int id)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return GetData(id, CmdTrans, aOracleConnection);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Close();
            }
        }

        public static List<Category> GetCategory()
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return GetCategory(CmdTrans, aOracleConnection);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Close();
            }
        }
        public static int InsertData(int id, TaskToDo task)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return InsertData(id, task, CmdTrans, aOracleConnection);
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                Close();
            }
        }

        public static int UpdateData(int id, TaskToDo task)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return UpdateData(id, task, CmdTrans, aOracleConnection);
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                Close();
            }
        }

        public static int DeleteData(int id)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return DeleteData(id, CmdTrans, aOracleConnection);
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                Close();
            }
        }

        public static List<TaskToDo> filterByCategory(int id, string order)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return filterByCategory(order, id, CmdTrans, aOracleConnection);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Close();
            }
        }

        public static List<TaskToDo> SortOrder(string order)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return SortOrder(order, CmdTrans, aOracleConnection);
            }
            catch (Exception)
            {
                return null;
            }

            {
                Close();
            }
        }
        public static PriorityQueue<TaskToDo, int> GetData(int ID, OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            List<TaskToDo> lst = new List<TaskToDo>();
            PriorityQueue<TaskToDo, int> queue = new PriorityQueue<TaskToDo, int>();

            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"SELECT * FROM TASK";
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string title = dt.Rows[i]["Title"].ToString();
                        DateTime TASKDATE = Convert.ToDateTime(dt.Rows[i]["TASKDATE"].ToString());
                        int id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                        int isCompleted = Convert.ToInt32(dt.Rows[i]["COLUMN2"].ToString());
                        int priority = Convert.ToInt32(dt.Rows[i]["PRIORITY"].ToString());
                        string Desc = dt.Rows[i]["COLUMN1"].ToString();

                        queue.Enqueue(new TaskToDo() { Id = id, title = title, Date = TASKDATE, description = Desc, IsCompleted = isCompleted, priority = priority }, priority);
                        //   lst.Add(new TaskToDo() { Id = id, title = title, Date = TASKDATE, description = Desc, IsCompleted = isCompleted,priority= priority });

                    }
                }
                return queue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public static int InsertData(int ID, TaskToDo task, OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            int r = 0;
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"INSERT INTO TASK (ID, TITLE, COLUMN1, TASKDATE, COLUMN2,CATEGORYID,PRIORITY) 
				                VALUES (Task_seq.nextval,:title,:des,:dateTask,:isComp,:catId,:priority)";
                cmd.CommandText = cmdText;

                // cmd.Parameters.Add("id", task.Id);
                cmd.Parameters.Add("title", task.title);
                cmd.Parameters.Add("des", task.description);
                cmd.Parameters.Add("dateTask", task.Date);
                cmd.Parameters.Add("isComp", task.IsCompleted);
                cmd.Parameters.Add("catId", 3);
                cmd.Parameters.Add("priority", 3);

                r = cmd.ExecuteNonQuery();
                CmdTrans.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return r;
        }
        public static int UpdateData(int ID, TaskToDo task, OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            int r = 0;
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"update task set  title=:Title,column1=:Des,column2=:IsComp where id=:ID";
                cmd.CommandText = cmdText;

                cmd.Parameters.Add("Title", task.title);
                cmd.Parameters.Add("Des", task.description);
                cmd.Parameters.Add("IsComp", task.IsCompleted);
                cmd.Parameters.Add("ID", task.Id);
                //  cmd.Parameters.Add("date", task.Date);

                r = cmd.ExecuteNonQuery();
                CmdTrans.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return r;
        }


        public static int DeleteData(int ID, OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            int r = 0;
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"Delete from task where id=:ID";
                cmd.CommandText = cmdText;

                cmd.Parameters.Add("ID", ID);

                r = cmd.ExecuteNonQuery();
                CmdTrans.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return r;
        }

        public static List<TaskToDo> SortOrder(string order, OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            List<TaskToDo> lst = new List<TaskToDo>();
            int r = 0;
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = "";
                if (order == "desc")
                {
                    cmdText = @"select * from Task order by ID desc";
                }
                else
                {
                    cmdText = @"select * from Task order by ID asc";
                }
                cmd.CommandText = cmdText;

                r = cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string title = dt.Rows[i]["Title"].ToString();
                        DateTime TASKDATE = Convert.ToDateTime(dt.Rows[i]["TASKDATE"].ToString());
                        int id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                        int isCompleted = Convert.ToInt32(dt.Rows[i]["COLUMN2"].ToString());
                        string Desc = dt.Rows[i]["COLUMN1"].ToString();

                        lst.Add(new TaskToDo() { Id = id, title = title, Date = TASKDATE, description = Desc, IsCompleted = isCompleted });

                    }
                }
                CmdTrans.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return lst;
        }


        public static List<Category> GetCategory(OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            List<Category> lst = new List<Category>();

            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"SELECT * FROM category";
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string name = dt.Rows[i]["Name"].ToString();
                        int id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                        lst.Add(new Category() { Id = id, Name = name });

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }


        public static List<TaskToDo> filterByCategory(string order, int Catid, OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            List<TaskToDo> lst = new List<TaskToDo>();
            PriorityQueue<TaskToDo, int> queue = new PriorityQueue<TaskToDo, int>();

            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = "";
                if ((Catid == 0 || Catid == -1) && order == "asc")
                {
                    cmdText = @"select * from task order by ID asc";
                }
                else if ((Catid == 0 || Catid == -1) && order == "desc")
                {
                    cmdText = @"select * from task order by ID desc";

                }
                else if (order == "asc")
                {
                    cmdText = @"select * from task where categoryID=:id order by ID asc";
                }
                else if (order == "desc")
                {
                    cmdText = @"select * from task where categoryID=:id order by ID desc";
                }
                cmd.CommandText = cmdText;
                cmd.Parameters.Add("id", Catid);
                //cmd.Parameters.Add("orderId", order);
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string title = dt.Rows[i]["Title"].ToString();
                        DateTime TASKDATE = Convert.ToDateTime(dt.Rows[i]["TASKDATE"].ToString());
                        int id = Convert.ToInt32(dt.Rows[i]["Id"].ToString());
                        int isCompleted = Convert.ToInt32(dt.Rows[i]["COLUMN2"].ToString());
                        string Desc = dt.Rows[i]["COLUMN1"].ToString();
                        int priority = Convert.ToInt32(dt.Rows[i]["PRIORITY"].ToString());
                        int catId = Convert.ToInt32(dt.Rows[i]["CATEGORYID"].ToString());


                        queue.Enqueue(new TaskToDo() { Id = id, title = title, Date = TASKDATE, description = Desc, IsCompleted = isCompleted, CategoryId = catId, priority = priority }, priority);


                    }
                    while (queue.Count > 0)
                    {
                        var que = queue.Dequeue();
                        lst.Add(new TaskToDo() { Id = que.Id, title = que.title, Date = que.Date, description = que.description, IsCompleted = que.IsCompleted, CategoryId = que.CategoryId, priority = que.priority });

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        static void Open()
        {
            con = new dbAccess();
            aOracleConnection = con.get_con();
        }

        static void Close()
        {
            con.Close(aOracleConnection);
        }
    }
}
