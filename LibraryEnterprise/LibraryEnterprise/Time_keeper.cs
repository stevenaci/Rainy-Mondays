/* File: Time_keeper.cs
 * Author: Steven Carino
 * 
 * Purpose:
 * Carries out logic of updating patron fees.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace LibraryEnterprise
{
    public class Time_keeper : System.Web.UI.Page
    {
        String query;
        String con_string;

        int delta;
        public Time_keeper()
        {
            con_string = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;
            // find delta of time between using software
            this.find_delta();
        }

        public void find_delta()
        {
            delta = 0;
            query = "select DATEDIFF(day, l.lastaccessed, CURRENT_TIMESTAMP) from dbo.lastaccessed l";
            // find the difference between dates
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand(query))
            {
                connection.Open();
                command.Connection = connection;
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    delta = rdr.GetInt32(0);
                }
            }
        }

        public void update_fees()
        {
            // update all patron fees
            query = "select COUNT(c.checkout_id), patron_id from dbo.checkouts c where c.date_in IS NULL group by patron_id;";
            using (SqlConnection connection = new SqlConnection(con_string))
            using (SqlCommand command = new SqlCommand(query))
            {
                connection.Open();
                command.Connection = connection;
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {// has the rows for how many overdue books each patron has
                    //iterate through patrons in list

                    //multiply by delta dates
                    Decimal balance = (delta) * rdr.GetInt32(0) * 0.25M;
                    query = "UPDATE patrons set account_balance=(account_balance + @balance) where patron_id=@patronid;";

                    SqlConnection con2 = new SqlConnection(con_string);
                    con2.Open();
                    SqlCommand cmd2 = new SqlCommand(query);
                    cmd2.Connection = con2;
                    cmd2.Parameters.AddWithValue("@balance", balance);
                    cmd2.Parameters.AddWithValue("@patronid", rdr.GetInt32(1));
                    cmd2.ExecuteScalar();
                }
            }
        }
        public void update_date()
        {
            query = "UPDATE dbo.lastaccessed set lastaccessed = @curdate where singleton = 0 ;";
            SqlConnection con = new SqlConnection(con_string);
            con.Open();
            SqlCommand cmd = new SqlCommand(query);
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@curdate", DateTime.Now);
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();
        }
    }
}