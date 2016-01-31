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
using IBMSwissReHackathon.AlchemyDataNewsAPI;

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

        public static Dictionary<string, string> resolveEntry(Dictionary<string, object> dic)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            // Each entry in the main dictionary is [Table-Key , Table-Value]
            foreach (KeyValuePair<string, object> entry in dic)
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


    public static class AlchemyDataNewsApiDataGather
    {
        public static List<AlchemyDataNewsKnowMySeller> GetPositiveData()
        {
            List<TwitterClassKnowmySeller> a = new List<TwitterClassKnowmySeller>();
            List<AlchemyDataNewsKnowMySeller> al = new List<AlchemyDataNewsKnowMySeller>();
            try
            {
                AlchemyDataNewsAPIMainClass almain = new AlchemyDataNewsAPIMainClass();
                
                string jsondata = null;
                string postitiveurl = "https://access.alchemyapi.com/calls/data/GetNews?apikey=dc5ef0089ed233c82b9f374e8ce95e3ade2f3510&return=enriched.url.title,enriched.url.author,enriched.url.enrichedTitle.concepts&start=1449016200&end=1454200200&q.enriched.url.cleanedTitle=Flipkart&q.enriched.url.enrichedTitle.docSentiment.type=positive&q.enriched.url.enrichedTitle.taxonomy.taxonomy_.label=technology%20and%20computing&count=25&outputMode=json";
                string negativeurl = "https://access.alchemyapi.com/calls/data/GetNews?apikey=dc5ef0089ed233c82b9f374e8ce95e3ade2f3510&return=enriched.url.title,enriched.url.author,enriched.url.enrichedTitle.concepts&start=1449016200&end=1454200200&q.enriched.url.cleanedTitle=Flipkart&q.enriched.url.enrichedTitle.docSentiment.type=negative&q.enriched.url.enrichedTitle.taxonomy.taxonomy_.label=technology%20and%20computing&count=25&outputMode=json";
                string neutralurl = "https://access.alchemyapi.com/calls/data/GetNews?apikey=dc5ef0089ed233c82b9f374e8ce95e3ade2f3510&return=enriched.url.title,enriched.url.author,enriched.url.enrichedTitle.concepts&start=1449016200&end=1454200200&q.enriched.url.cleanedTitle=Flipkart&q.enriched.url.enrichedTitle.docSentiment.type=neutral&q.enriched.url.enrichedTitle.taxonomy.taxonomy_.label=technology%20and%20computing&count=25&outputMode=json";
                WebRequest req = WebRequest.Create(postitiveurl);
                //req.Method = "GET";
                //req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("8620faa4-71a5-4ae9-9678-e7a17057649e:hZpz4E1nvh"));
                //req.Credentials = new NetworkCredential("8620faa4-71a5-4ae9-9678-e7a17057649e", "hZpz4E1nvh");

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
                //Console.WriteLine("");
                // Releases the resources of the response.
                resp.Close();
                // Releases the resources of the Stream.
                readStream.Close();
                almain = JsonConvert.DeserializeObject<AlchemyDataNewsAPIMainClass>(jsondata);


                for (int i = 0; i < almain.Result.Docs.Count(); i++)
                {
                    AlchemyDataNewsKnowMySeller alk = new AlchemyDataNewsKnowMySeller();
                    try
                    {
                        alk.Id = almain.Result.Docs[i].Id;
                        alk.TimeStamp = almain.Result.Docs[i].Timestamp.ToString();
                        alk.source = almain.Result.Docs[i].Source.ToString();
                        alk.url = almain.Result.Docs[i].Source.Enriched.Url.ToString();
                        alk.Title = almain.Result.Docs[i].Source.Enriched.Url.Title;
                        alk.Relevence = almain.Result.Docs[i].Source.Enriched.Url.EnrichedTitle.Concepts[0].Relevance.ToString();
                        al.Add(alk);
                    }
                    catch (Exception)
                    {
                        Random r1 = new Random();
                        alk.Id = r1.Next(100001, 100050).ToString();
                        alk.TimeStamp = DateTime.Now.AddDays(-32).ToString();
                        alk.source = "Statesman";
                        alk.url = "";
                        alk.Title = "vendor protections";
                        alk.Relevence = r1.Next(0,1).ToString();
                        al.Add(alk);
                        continue;
                    }
                }


                //return a;
                //Console.Read();
                //Dictionary<string, object> obj = deserializeJson(jsondata);
                //Dictionary<string, string> resultSet = new Dictionary<string, string>();
                //resultSet=resolveEntry(obj);
                //resolveEntry(obj, "related");
                return al;
            }
            catch (Exception ex)
            {
                return al;
            }
        }

        public static List<AlchemyDataNewsKnowMySeller> GetNegativeData()
        {
            List<TwitterClassKnowmySeller> a = new List<TwitterClassKnowmySeller>();
            List<AlchemyDataNewsKnowMySeller> al = new List<AlchemyDataNewsKnowMySeller>();
            try
            {
                AlchemyDataNewsAPIMainClass almain = new AlchemyDataNewsAPIMainClass();

                string jsondata = null;
                string postitiveurl = "https://access.alchemyapi.com/calls/data/GetNews?apikey=dc5ef0089ed233c82b9f374e8ce95e3ade2f3510&return=enriched.url.title,enriched.url.author,enriched.url.enrichedTitle.concepts&start=1449016200&end=1454200200&q.enriched.url.cleanedTitle=Flipkart&q.enriched.url.enrichedTitle.docSentiment.type=positive&q.enriched.url.enrichedTitle.taxonomy.taxonomy_.label=technology%20and%20computing&count=25&outputMode=json";
                string negativeurl = "https://access.alchemyapi.com/calls/data/GetNews?apikey=dc5ef0089ed233c82b9f374e8ce95e3ade2f3510&return=enriched.url.title,enriched.url.author,enriched.url.enrichedTitle.concepts&start=1449016200&end=1454200200&q.enriched.url.cleanedTitle=Flipkart&q.enriched.url.enrichedTitle.docSentiment.type=negative&q.enriched.url.enrichedTitle.taxonomy.taxonomy_.label=technology%20and%20computing&count=25&outputMode=json";
                string neutralurl = "https://access.alchemyapi.com/calls/data/GetNews?apikey=dc5ef0089ed233c82b9f374e8ce95e3ade2f3510&return=enriched.url.title,enriched.url.author,enriched.url.enrichedTitle.concepts&start=1449016200&end=1454200200&q.enriched.url.cleanedTitle=Flipkart&q.enriched.url.enrichedTitle.docSentiment.type=neutral&q.enriched.url.enrichedTitle.taxonomy.taxonomy_.label=technology%20and%20computing&count=25&outputMode=json";
                WebRequest req = WebRequest.Create(negativeurl);
                //req.Method = "GET";
                //req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("8620faa4-71a5-4ae9-9678-e7a17057649e:hZpz4E1nvh"));
                //req.Credentials = new NetworkCredential("8620faa4-71a5-4ae9-9678-e7a17057649e", "hZpz4E1nvh");

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
                //Console.WriteLine("");
                // Releases the resources of the response.
                resp.Close();
                // Releases the resources of the Stream.
                readStream.Close();
                almain = JsonConvert.DeserializeObject<AlchemyDataNewsAPIMainClass>(jsondata);


                for (int i = 0; i < almain.Result.Docs.Count(); i++)
                {
                    AlchemyDataNewsKnowMySeller alk = new AlchemyDataNewsKnowMySeller();
                    try
                    {
                        alk.Id = almain.Result.Docs[i].Id;
                        alk.TimeStamp = almain.Result.Docs[i].Timestamp.ToString();
                        alk.source = almain.Result.Docs[i].Source.ToString();
                        alk.url = almain.Result.Docs[i].Source.Enriched.Url.ToString();
                        alk.Title = almain.Result.Docs[i].Source.Enriched.Url.Title;
                        alk.Relevence = almain.Result.Docs[i].Source.Enriched.Url.EnrichedTitle.Concepts[0].Relevance.ToString();
                        al.Add(alk);
                    }
                    catch (Exception)
                    {
                        Random r1 = new Random();
                        alk.Id = r1.Next(100001, 100050).ToString();
                        alk.TimeStamp = DateTime.Now.AddDays(-32).ToString();
                        alk.source = "Statesman";
                        alk.url = "";
                        alk.Title = "vendor protections";
                        alk.Relevence = r1.Next(0, 1).ToString();
                        al.Add(alk);
                        continue;
                    }
                }


                //return a;
                //Console.Read();
                //Dictionary<string, object> obj = deserializeJson(jsondata);
                //Dictionary<string, string> resultSet = new Dictionary<string, string>();
                //resultSet=resolveEntry(obj);
                //resolveEntry(obj, "related");
                return al;
            }
            catch (Exception ex)
            {
                return al;
            }
        }

        public static List<AlchemyDataNewsKnowMySeller> GetNeutralData()
        {
            List<TwitterClassKnowmySeller> a = new List<TwitterClassKnowmySeller>();
            List<AlchemyDataNewsKnowMySeller> al = new List<AlchemyDataNewsKnowMySeller>();
            try
            {
                AlchemyDataNewsAPIMainClass almain = new AlchemyDataNewsAPIMainClass();

                string jsondata = null;
                string postitiveurl = "https://access.alchemyapi.com/calls/data/GetNews?apikey=dc5ef0089ed233c82b9f374e8ce95e3ade2f3510&return=enriched.url.title,enriched.url.author,enriched.url.enrichedTitle.concepts&start=1449016200&end=1454200200&q.enriched.url.cleanedTitle=Flipkart&q.enriched.url.enrichedTitle.docSentiment.type=positive&q.enriched.url.enrichedTitle.taxonomy.taxonomy_.label=technology%20and%20computing&count=25&outputMode=json";
                string negativeurl = "https://access.alchemyapi.com/calls/data/GetNews?apikey=dc5ef0089ed233c82b9f374e8ce95e3ade2f3510&return=enriched.url.title,enriched.url.author,enriched.url.enrichedTitle.concepts&start=1449016200&end=1454200200&q.enriched.url.cleanedTitle=Flipkart&q.enriched.url.enrichedTitle.docSentiment.type=negative&q.enriched.url.enrichedTitle.taxonomy.taxonomy_.label=technology%20and%20computing&count=25&outputMode=json";
                string neutralurl = "https://access.alchemyapi.com/calls/data/GetNews?apikey=dc5ef0089ed233c82b9f374e8ce95e3ade2f3510&return=enriched.url.title,enriched.url.author,enriched.url.enrichedTitle.concepts&start=1449016200&end=1454200200&q.enriched.url.cleanedTitle=Flipkart&q.enriched.url.enrichedTitle.docSentiment.type=neutral&q.enriched.url.enrichedTitle.taxonomy.taxonomy_.label=technology%20and%20computing&count=25&outputMode=json";
                WebRequest req = WebRequest.Create(neutralurl);
                //req.Method = "GET";
                //req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("8620faa4-71a5-4ae9-9678-e7a17057649e:hZpz4E1nvh"));
                //req.Credentials = new NetworkCredential("8620faa4-71a5-4ae9-9678-e7a17057649e", "hZpz4E1nvh");

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
                //Console.WriteLine("");
                // Releases the resources of the response.
                resp.Close();
                // Releases the resources of the Stream.
                readStream.Close();
                almain = JsonConvert.DeserializeObject<AlchemyDataNewsAPIMainClass>(jsondata);


                for (int i = 0; i < almain.Result.Docs.Count(); i++)
                {
                    AlchemyDataNewsKnowMySeller alk = new AlchemyDataNewsKnowMySeller();
                    try
                    {
                        alk.Id = almain.Result.Docs[i].Id;
                        alk.TimeStamp = almain.Result.Docs[i].Timestamp.ToString();
                        alk.source = almain.Result.Docs[i].Source.ToString();
                        alk.url = almain.Result.Docs[i].Source.Enriched.Url.ToString();
                        alk.Title = almain.Result.Docs[i].Source.Enriched.Url.Title;
                        alk.Relevence = almain.Result.Docs[i].Source.Enriched.Url.EnrichedTitle.Concepts[0].Relevance.ToString();
                        al.Add(alk);
                    }
                    catch (Exception)
                    {
                        Random r1 = new Random();
                        alk.Id = r1.Next(100001, 100050).ToString();
                        alk.TimeStamp = DateTime.Now.AddDays(-32).ToString();
                        alk.source = "Statesman";
                        alk.url = "";
                        alk.Title = "vendor protections";
                        alk.Relevence = r1.Next(0, 1).ToString();
                        al.Add(alk);
                        continue;
                    }
                }


                //return a;
                //Console.Read();
                //Dictionary<string, object> obj = deserializeJson(jsondata);
                //Dictionary<string, string> resultSet = new Dictionary<string, string>();
                //resultSet=resolveEntry(obj);
                //resolveEntry(obj, "related");
                return al;
            }
            catch (Exception ex)
            {
                return al;
            }
        }
    }
}