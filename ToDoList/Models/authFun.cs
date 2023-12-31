﻿using DbManagerStandard;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using ToDoList.Services;

namespace ToDoList.Models
{
    public class authFun
    {
        static dbAccess con;
        static OracleConnection aOracleConnection;
        public static Users currentUser = null;
        public static List<UserRole> currenUserRole = new List<UserRole> { };
        public static List<User_Permission> currentUserPermission = new List<User_Permission> { };
        public static List<Users> GetData()
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return GetData(CmdTrans, aOracleConnection);
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
        public static List<Screen> GetPages()
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return GetPages(CmdTrans, aOracleConnection);
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
        public static List<User_Permission> getUser_Permission()
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return getUser_Permission(CmdTrans, aOracleConnection);
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

        public static List<UserRole> getPermissions()
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return getPermissions(CmdTrans, aOracleConnection);
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

        public static int InsertData(InputRegisterVM registerVM)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return InsertData(registerVM, CmdTrans, aOracleConnection);
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

        public static int check_Permission(int id)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return check_Permission(id, CmdTrans, aOracleConnection);
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
        public static Users LoginUser(InputLoginVM inputLoginVM)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return LoginUser(inputLoginVM, CmdTrans, aOracleConnection);
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
        public static int InsertData(InputRegisterVM registerVM, OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            List<Users> users = GetData();
            int r = 0;
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                if (users.Count > 0)
                {
                    foreach (Users user in users)
                    {
                        if (user.email.Equals(registerVM.Email))
                        {
                            return r;
                        }
                    }
                }
                var cmdText = @"insert into users(id,email,firstname,lastname,password,ISADMIN)
                                values(list_seq.nextval,:email,:fname,:lname,:pass,:ISADMIN)";
                cmd.CommandText = cmdText;
                //@"select list_seq.nextval from dual"
                // cmd.Parameters.Add("id",);
                cmd.Parameters.Add("email", registerVM.Email);
                cmd.Parameters.Add("fname", registerVM.FirstName);
                cmd.Parameters.Add("lname", registerVM.LastName);
                cmd.Parameters.Add("pass", registerVM.Password);

                if (registerVM.IsAdmin == true)
                {
                    cmd.Parameters.Add("ISADMIN", 1);
                }
                else
                {
                    cmd.Parameters.Add("ISADMIN", 0);
                }

                r = cmd.ExecuteNonQuery();
                CmdTrans.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return r;
        }

        public static List<Users> GetData(OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            List<Users> lst = new List<Users>();
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"SELECT * FROM Users";
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string email = dt.Rows[i]["EMAIL"].ToString();
                        int id = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                        string fname = dt.Rows[i]["FIRSTNAME"].ToString();
                        string lname = dt.Rows[i]["LASTNAME"].ToString();
                        string pass = dt.Rows[i]["PASSWORD"].ToString();
                        //  char isAdmin = Convert.ToChar(dt.Rows[i]["ISADMIN"]);

                        lst.Add(new Users() { Id = id, email = email, fName = fname, lName = lname, Password = pass });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }


        public static List<Screen> GetPages(OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            List<Screen> lst = new List<Screen>();
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"SELECT * FROM Pages";
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string name = dt.Rows[i]["PAGENAME"].ToString();
                        int id = Convert.ToInt32(dt.Rows[i]["ID"].ToString());

                        lst.Add(new Screen() { Id = id, Name = name });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public static List<UserRole> getPermissions(OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            List<UserRole> lst = new List<UserRole>();
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"select * from UserRole";
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string command = dt.Rows[i]["TYPE"].ToString();
                        int id = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                        int pageId = Convert.ToInt32(dt.Rows[i]["PAGEID"].ToString());
                        lst.Add(new UserRole() { Id = id, pageId = pageId, commandType = command });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public static List<User_Permission> getUser_Permission(OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            List<User_Permission> lst = new List<User_Permission>();
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"select * from User_Permission";
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int commandId = Convert.ToInt32(dt.Rows[i]["COMMANDID"].ToString());
                        int id = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                        int userId = Convert.ToInt32(dt.Rows[i]["USERID"].ToString());
                        lst.Add(new User_Permission() { Id = id, userId = userId, commandId = commandId });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public static int check_Permission(int id, OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {

            int r = 0; //no permission in this page
            List<Screen> screens = GetPages();
            List<UserRole> userRoles = getPermissions();
            List<User_Permission> User_Permissions = getUser_Permission();
            currentUserPermission.Clear();
            currenUserRole.Clear();
            foreach (Screen screen in screens)
            {
                if (screen.Id == id)
                {
                    r = 1; // it means there is permissions in this page
                    if (currentUser != null)
                    {
                        foreach (User_Permission permissions in User_Permissions)
                        {
                            if (permissions.userId == currentUser.Id)
                            {
                                currentUserPermission.Add(permissions);
                            }
                        }
                        foreach (User_Permission permissions in currentUserPermission)
                        {
                            foreach (UserRole userRole in userRoles)
                            {
                                if (permissions.commandId == userRole.Id && userRole.pageId == id)
                                {
                                    currenUserRole.Add(userRole);
                                }
                            }
                        }
                    }
                    else { return r = 0; } //you must sign in..!
                }
            }
            return r;
        }

        public static Users LoginUser(InputLoginVM inputLoginVM, OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            List<Users> users = GetData();
            List<UserRole> userRoles = getPermissions();
            List<User_Permission> User_Permissions = getUser_Permission();

            int r = 0;
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                if (users.Count > 0)
                {
                    foreach (Users user in users)
                    {
                        if (user.email.Equals(inputLoginVM.Email) && user.Password.Equals(inputLoginVM.Password))
                        {
                            currentUser = new Users() { };
                            currentUser.email = inputLoginVM.Email;
                            currentUser.Password = inputLoginVM.Password;
                            currentUser.Id = user.Id;
                            currentUser.lName = user.lName;
                            currentUser.fName = user.fName;
                            currentUser.IsAdmin = user.IsAdmin;



                            //  r = 1;
                            return currentUser;
                        }
                    }



                }
                else
                {
                    r = 0;
                    return null;
                }

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            return null;
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
