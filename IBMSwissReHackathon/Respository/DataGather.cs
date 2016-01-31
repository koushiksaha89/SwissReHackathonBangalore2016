using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using System.Linq;
using System.Web.Script;
using System.Web.Script.Serialization;
using System.Collections;
using Newtonsoft.Json;
using IBMSwissReHackathon.TwitterApiClasses;

namespace IBMSwissReHackathon.Respository
{
    public static class TwitterDataGather
    {
        public static List<TwitterClassKnowmySeller> GetData()
        {
            List<TwitterClassKnowmySeller> a = new List<TwitterClassKnowmySeller>();
            try
            {
                TwitterApiMainClass r = new TwitterApiMainClass();
                string jsondata = null;
                WebRequest req = WebRequest.Create(@"https://cdeservice.mybluemix.net:443/api/v1/messages/search?q=%23wsretail&size=5");
                req.Method = "GET";
                req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("8620faa4-71a5-4ae9-9678-e7a17057649e:hZpz4E1nvh"));
                req.Credentials = new NetworkCredential("8620faa4-71a5-4ae9-9678-e7a17057649e", "hZpz4E1nvh");

                //TwitterApiResponse response = GetData(req);

                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

                Stream receiveStream = resp.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(receiveStream, encode);
                Console.WriteLine("\r\nResponse stream received.");
                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                Console.WriteLine("HTML...\r\n");
                while (count > 0)
                {
                    // Dumps the 256 characters on a string and displays the string to the console.
                    String str = new String(read, 0, count);
                    //Console.Write(str);
                    count = readStream.Read(read, 0, 256);
                    jsondata = jsondata + str;
                }
                Console.WriteLine("");
                // Releases the resources of the response.
                resp.Close();
                // Releases the resources of the Stream.
                readStream.Close();
                r = JsonConvert.DeserializeObject<TwitterApiMainClass>(jsondata);


                for (int i = 0; i < r.Tweets.Count(); i++)
                {
                    TwitterClassKnowmySeller tk = new TwitterClassKnowmySeller();
                    tk.city = r.Tweets[i].Cde.Author.Location.City;
                            tk.Country = r.Tweets[i].Cde.Author.Location.Country;
                    tk.Evidence = r.Tweets[i].Cde.Content.Sentiment.Evidence.ToString();
                    tk.polarity = r.Tweets[i].Cde.Content.Sentiment.Polarity;
                            tk.PostedTime = r.Tweets[i].Message.PostedTime.ToString();
                    tk.body = r.Tweets[i].Message.Body;
                    a.Add(tk);
                    
                }
                

                return a;
                //Console.Read();
                //Dictionary<string, object> obj = deserializeJson(jsondata);
                //Dictionary<string, string> resultSet = new Dictionary<string, string>();
                //resultSet=resolveEntry(obj);
                //resolveEntry(obj, "related");
            }
            catch (Exception ex)
            {
                return a;
            }
        }
        public static Dictionary<string, object> deserializeJson(this string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> dictionary =
                serializer.Deserialize<Dictionary<string, object>>(json);
            return dictionary;
        }

        public static Dictionary<string,string> resolveEntry(Dictionary<string, object> dic)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            // Each entry in the main dictionary is [Table-Key , Table-Value]
            foreach (KeyValuePair<string, object>entry in dic)
            {
                if (entry.Key == "tweets")
                {
                    
                    foreach (KeyValuePair<string, object> item in (ICollection)entry.Value)
                    {
                        if (item.Key == "cde")
                        {
                            foreach (KeyValuePair<string, object> subitem in (ICollection)item.Value)
                            {
                                if (subitem.Key == "author")
                                {
                                    foreach (KeyValuePair<string, object> subitem1 in (ICollection)subitem.Value)
                                    {
                                        if (subitem1.Key == "location")
                                        {
                                            foreach (KeyValuePair<string, object> subsubitem in (ICollection)subitem1.Value)
                                            {
                                                if (subsubitem.Key == "city" || subsubitem.Key == "country")
                                                    res.Add(subsubitem.Key, subsubitem.Value.ToString());

                                            }
                                        }
                                    }
                                }
                                else if (subitem.Key == "content")
                                {
                                    foreach (KeyValuePair<string, object> subitem1 in (ICollection)subitem.Value)
                                    {
                                        if (subitem1.Key == "sentiment")
                                        {
                                            foreach (KeyValuePair<string, object> subsubitem in (ICollection)subitem1.Value)
                                            {
                                                if (subsubitem.Key == "polarity")
                                                    res.Add(subsubitem.Key, subsubitem.Value.ToString());

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (item.Key == "message")
                        {
                            foreach (KeyValuePair<string, object> subitem in (ICollection)item.Value)
                            {
                                if (subitem.Key == "postedTime")
                                    res.Add(subitem.Key, subitem.Value.ToString());
                                if (subitem.Key == "body")
                                    res.Add(subitem.Key, subitem.Value.ToString());
                                if (subitem.Key == "id")
                                    res.Add(subitem.Key, subitem.Value.ToString());

                            }
                        }

                    }
                }
            }


                return res;
        }
    }
}