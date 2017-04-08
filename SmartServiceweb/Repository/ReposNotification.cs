using SmartServiceweb.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

public class ReposNotification
{

    #region notification
    public static void sendAndroidPush(List<AddBlogData> objBlogData)
    {
        try
        {
            string ApplicationID;
            string SENDER_ID;
            var userData=objBlogData.FirstOrDefault().Userinfo.FirstOrDefault();
            string GCMID = userData.GCMId;
            string srtDesc=objBlogData.FirstOrDefault().textContent;
            string Messages = string.Empty;
            if (srtDesc != null && srtDesc != string.Empty)
            {
                Messages = userData.FirstName + " " + userData.LastName + " : " + srtDesc;
            }
            else
            {
                Messages = userData.FirstName + " " + userData.LastName + " : (Image)" ;
            }


            ApplicationID = "AIzaSyD_Nz1tMXadvxgwli8KYV_mOkEOu8eYALI";
            SENDER_ID = "982524787977";
            // var value = msg; //message text box
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send"); tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", ApplicationID)); tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                  
            using (var streamWriter = new StreamWriter(tRequest.GetRequestStream()))
            {
                Random rnd = new Random();
                string month = rnd.Next(1, 9999999).ToString();
                string postData = "{\"data\": {\"title\":\" Smart Services \", \"notId\":\"" + month + "\", \"content-available\" :\"1\",\"time\": \"" + System.DateTime.Now.ToString() + "\",\"style\":\"inbox\",\"summaryText\":\"There are %n% notification\",\"message\":\"" + Messages + "\",\"registration_ids\":[\"" + GCMID + "\"]}";
            
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)tRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
        catch (Exception ex)
        {

        }

    }


    #endregion

    
}
#region ["Extension Method for Convertion Datatable to Tolist"]

public static class Extensions
{
    public static List<T> ToList<T>(this DataTable table) where T : new()
    {
        IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
        List<T> result = new List<T>();

        foreach (var row in table.Rows)
        {
            var item = CreateItemFromRow<T>((DataRow)row, properties);
            result.Add(item);
        }

        return result;
    }

    private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
    {
        T item = new T();
        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(System.DayOfWeek))
            {
                DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString());
                property.SetValue(item, day, null);
            }
            else
            {
                property.SetValue(item, row[property.Name], null);
            }
        }
        return item;
    }
}

#endregion


#region ["Notification Properties"]
public class Notification
{
    public int UserID { get; set; }
    public string GCMId { get; set; }
    public string Message { get; set; }
}
#endregion






